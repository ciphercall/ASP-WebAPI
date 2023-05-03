using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data;
using System.Configuration;
using AlchemyAccounting;
using System.Data.SqlClient;
using System.Web.Services;
using System.Text;
using System.Collections.Specialized;
using System.Threading;

namespace AlchemyAccounting.Stock.Report.Report
{
    public partial class rptItemStockStore : System.Web.UI.Page
    {
        IFormatProvider dateformat = new System.Globalization.CultureInfo("fr-FR", true);

        decimal tot_CartonQty = 0;
        decimal tot_PcQty = 0;
        decimal tot_ClosingQty = 0;
        decimal tot_StockValue = 0;

        string Tot_CartonQtyComma = "";
        string Tot_PcQtyComma = "";
        string Tot_ClosingQtyComma = "";
        string Tot_StockValueComma = "";

        protected void Page_Load(object sender, EventArgs e)
        {
            try
            {
                string uTp = HttpContext.Current.Session["USERTYPE"].ToString();
                string brCD = HttpContext.Current.Session["BrCD"].ToString();
                SqlCommand cmd = new SqlCommand();

                dbFunctions.lblAdd(@"SELECT COMPNM FROM ASL_COMPANY WHERE COMPID ='101'", lblCompNM);
                dbFunctions.lblAdd(@"SELECT ADDRESS FROM ASL_COMPANY WHERE COMPID ='101'", lblAddress);

                DateTime PrintDate = dbFunctions.Timezone(DateTime.Now);
                string td = PrintDate.ToString("dd-MMM-yyyy hh:mm tt");
                lblTime.Text = td;

                string ItemNM = Session["ItemNM"].ToString();
                string ItemID = Session["ItemID"].ToString();
                string ItemCD = Session["itemcd"].ToString();
                string Date = Session["Date"].ToString();

                DateTime asON = DateTime.Parse(Date, dateformat, System.Globalization.DateTimeStyles.AssumeLocal);
                lblDate.Text = asON.ToString("dd-MMM-yyyy");
                lblItemName.Text = ItemNM + " - " + ItemCD;
                showGrid();
            }
            catch
            {
            }
        }

        public void showGrid()
        {
            string connectionString = ConfigurationManager.ConnectionStrings["AslDbContext"].ConnectionString;
            SqlConnection conn = new SqlConnection(connectionString);

            string ItemNM = Session["ItemNM"].ToString();
            string ItemID = Session["ItemID"].ToString();
            string Date = Session["Date"].ToString();
            DateTime asON = DateTime.Parse(Date, dateformat, System.Globalization.DateTimeStyles.AssumeLocal);
            string aOn = asON.ToString("yyyy/MM/dd");

            conn.Open();
            SqlCommand cmd = new SqlCommand(@"SELECT     B.STORE, B.ITEMID, B.CLQTY, B.AVGRATE, B.CLQTY * B.AVGRATE AS STOCKVALUE, 
                     STK_ITEM.ITEMNM + ' || ' + STK_ITEM.ITEMCD AS ITEMNM, STK_STORE.STORENM  
                     FROM (
                     SELECT STORE, ITEMID, (SUM(ISNULL(INQTY, 0)) + SUM(ISNULL(BQTY, 0)) + SUM(ISNULL(SALERET, 0))) - 
                     (SUM(ISNULL(OUTQTY, 0)) + SUM(ISNULL(SQTY, 0)) + SUM(ISNULL(PURCHASERET, 0))) AS CLQTY,  SUM(ISNULL(BQTY, 0)) AS BQTY, 
                     SUM(ISNULL(BAMT, 0)) AS BAMT, SUM(ISNULL(SQTY, 0)) AS SQTY,  
                     (CASE WHEN SUM(isnull(BAMT, 0)) = 0.00 THEN 0.00 ELSE CONVERT(decimal(18, 2), (SUM(isnull(BAMT, 0))) / SUM(isnull(BQTY, 0))) 
                     END) AS AVGRATE  
                     FROM (
                    SELECT ITEMID, SUM(QTY) AS BQTY, SUM(AMOUNT) AS BAMT, 0 AS SQTY, 0 AS INQTY, 0 AS OUTQTY, 0 AS SALERET, 0 AS PURCHASERET, UNITTO STORE  
                    FROM STK_TRANS AS STK_TRANS_2  
                    WHERE (TRANSDT <= @DATE) AND (TRANSTP = 'BUY')  AND ITEMID=@ITEMID
                    GROUP BY  ITEMID, UNITTO 
                    UNION  
                    SELECT  ITEMID, 0 AS BQTY, 0 AS BAMT, SUM(QTY) AS SQTY, 0 AS INQTY, 0 AS OUTQTY, 
                    0 AS SALERET, 0 AS PURCHASERET, UNITFR STORE  FROM STK_TRANS AS STK_TRANS_1  
                    WHERE (TRANSDT <= @DATE) AND (TRANSTP = 'SALE')  AND ITEMID=@ITEMID
                    GROUP BY  ITEMID, UNITFR  
                    UNION
                    SELECT  ITEMID, 0 AS BQTY, 0 AS BAMT, 0 AS SQTY, 0 AS INQTY, 0 AS OUTQTY, 
                    SUM(QTY) AS SALERET, 0 AS PURCHASERET, UNITTO STORE  FROM STK_TRANS  
                    WHERE (TRANSDT <= @DATE) AND (TRANSTP = 'IRTS')  AND ITEMID=@ITEMID
                    GROUP BY  ITEMID, UNITTO  
                    UNION
                    SELECT  ITEMID, 0 AS BQTY, 0 AS BAMT, 0 AS SQTY, 0 AS INQTY, 0 AS OUTQTY, 
                    0 AS SALERET, SUM(QTY) AS PURCHASERET, UNITFR STORE  FROM STK_TRANS  
                    WHERE (TRANSDT <= @DATE) AND (TRANSTP = 'IRTB')  AND ITEMID=@ITEMID
                    GROUP BY  ITEMID, UNITFR 
                    UNION  
                    SELECT  ITEMID, 0 AS BQTY, 0 AS BAMT, 0 AS SQTY, 0 AS INQTY, SUM(QTY) AS OUTQTY, 
                    0 AS SALERET, 0 AS PURCHASERET, UNITFR STORE  FROM STK_TRANS AS STK_TRANS_1  
                    WHERE (TRANSDT <= @DATE) AND (TRANSTP = 'IISS')  AND ITEMID=@ITEMID
                    GROUP BY  ITEMID, UNITFR  
                    UNION  
                    SELECT  ITEMID, 0 AS BQTY, 0 AS BAMT, 0 AS SQTY, SUM(QTY) AS INQTY, 0 AS OUTQTY,
                    0 AS SALERET, 0 AS PURCHASERET, UNITTO STORE
                    FROM STK_TRANS  
                    WHERE (TRANSDT <= @DATE) AND (TRANSTP = 'IISS') AND ITEMID=@ITEMID
                    GROUP BY  ITEMID, UNITTO) AS A  
                      GROUP BY STORE, ITEMID) AS B 
                      INNER JOIN  STK_ITEM ON  B.ITEMID = STK_ITEM.ITEMID 
                      INNER JOIN  STK_STORE ON B.STORE COLLATE Latin1_General_CI_AS = STK_STORE.STOREID  
                      WHERE     (B.CLQTY <> 0)", conn);
            cmd.Parameters.AddWithValue("@DATE", aOn);
            cmd.Parameters.AddWithValue("@ITEMID", ItemID);
            cmd.CommandTimeout = 0;
            SqlDataAdapter da = new SqlDataAdapter(cmd);
            DataSet ds = new DataSet();
            da.Fill(ds);
            conn.Close();
            if (ds.Tables[0].Rows.Count > 0)
            {
                GridView1.DataSource = ds;
                GridView1.DataBind();
                GridView1.Visible = true;
            }
            else
            {
                GridView1.DataSource = ds;
                GridView1.DataBind();
                GridView1.Visible = true;
            }
        }

        protected void GridView1_RowDataBound(object sender, GridViewRowEventArgs e)
        {
            if (e.Row.RowType == DataControlRowType.DataRow)
            {
                string STORENM = DataBinder.Eval(e.Row.DataItem, "STORENM").ToString();
                e.Row.Cells[0].Text = "&nbsp;" + STORENM;

                //decimal CARTONQTY = Convert.ToDecimal(DataBinder.Eval(e.Row.DataItem, "CARTONQTY").ToString());
                //string carQTY = SpellAmount.comma(CARTONQTY);
                //e.Row.Cells[1].Text = carQTY;

                decimal PIECES = Convert.ToDecimal(DataBinder.Eval(e.Row.DataItem, "AVGRATE").ToString());
                string piQty = SpellAmount.comma(PIECES);
                e.Row.Cells[1].Text = piQty;

                decimal CLQTY = Convert.ToDecimal(DataBinder.Eval(e.Row.DataItem, "CLQTY").ToString());
                string closQTY = SpellAmount.comma(CLQTY);
                e.Row.Cells[2].Text = closQTY;

                decimal STOCKVALUE = Convert.ToDecimal(DataBinder.Eval(e.Row.DataItem, "STOCKVALUE").ToString());
                string stkValue = SpellAmount.comma(STOCKVALUE);
                //if(stkValue=="")
                //    e.Row.Cells[4].Text = "0";
                //else
                e.Row.Cells[3].Text = stkValue;

                //tot_CartonQty += CARTONQTY;
                //Tot_CartonQtyComma = SpellAmount.comma(tot_CartonQty);

                tot_PcQty += PIECES;
                Tot_PcQtyComma = SpellAmount.comma(tot_PcQty);

                tot_ClosingQty += CLQTY;
                Tot_ClosingQtyComma = SpellAmount.comma(tot_ClosingQty);

                tot_StockValue += STOCKVALUE;
                Tot_StockValueComma = SpellAmount.comma(tot_StockValue);
            }

            else if (e.Row.RowType == DataControlRowType.Footer)
            {
                e.Row.Cells[0].Text = "TOTAL :";
                e.Row.Cells[0].HorizontalAlign = HorizontalAlign.Right;
                //e.Row.Cells[1].Text = Tot_CartonQtyComma;
                //e.Row.Cells[1].HorizontalAlign = HorizontalAlign.Right;
                e.Row.Cells[1].Text = Tot_PcQtyComma;
                e.Row.Cells[1].HorizontalAlign = HorizontalAlign.Right;
                e.Row.Cells[2].Text = Tot_ClosingQtyComma;
                e.Row.Cells[2].HorizontalAlign = HorizontalAlign.Right;
                e.Row.Cells[3].Text = Tot_StockValueComma;
                e.Row.Cells[3].HorizontalAlign = HorizontalAlign.Right;
                e.Row.Font.Bold = true;
            }

            ShowHeader(GridView1);
        }

        private void ShowHeader(GridView grid)
        {
            if (grid.Rows.Count > 0)
            {
                grid.UseAccessibleHeader = true;
                grid.HeaderRow.TableSection = TableRowSection.TableHeader;
                //gridView.HeaderRow.Style["display"] = "table-header-group";
            }
        }

    }
}