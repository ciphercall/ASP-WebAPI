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
    public partial class rptItemRegister : System.Web.UI.Page
    {
        IFormatProvider dateformat = new System.Globalization.CultureInfo("fr-FR", true);

        decimal tot_Buy = 0;
        decimal tot_Sale = 0;
        decimal tot_In = 0;
        decimal tot_Out = 0;
        decimal tot_Sret = 0;
        decimal tot_PRet = 0;

        string Tot_BuyComma = "0";
        string Tot_SaleComma = "0";
        string Tot_InComma = "0";
        string Tot_OutComma = "0";
        string Tot_SretComma = "0";
        string Tot_PretComma = "0";

        protected void Page_Load(object sender, EventArgs e)
        {
            string uTp = HttpContext.Current.Session["USERTYPE"].ToString();
            string brCD = HttpContext.Current.Session["BrCD"].ToString();
            SqlCommand cmd = new SqlCommand();
            dbFunctions.lblAdd(@"SELECT COMPNM FROM ASL_COMPANY WHERE COMPID ='101'", lblCompNM);
            dbFunctions.lblAdd(@"SELECT ADDRESS FROM ASL_COMPANY WHERE COMPID ='101'", lblAddress);

            DateTime PrintDate = dbFunctions.Timezone(DateTime.Now);
            string td = PrintDate.ToString("dd-MMM-yyyy hh:mm tt");
            lblTime.Text = td;

            string StoreNM = Session["StoreNm"].ToString();
            string StoreID = Session["StoreID"].ToString();
            string ItemNM = Session["ItemNM"].ToString();
            string ItemCD = Session["itemcd"].ToString();
            string ItemID = Session["ItemID"].ToString();
            string txtFrom = Session["From"].ToString();
            string txtTo = Session["To"].ToString();

            DateTime FDate = DateTime.Parse(txtFrom, dateformat, System.Globalization.DateTimeStyles.AssumeLocal);
            lblFDate.Text = FDate.ToString("dd-MMM-yyyy");
            string FdT = FDate.ToString("yyyy/MM/dd");

            DateTime TDate = DateTime.Parse(txtTo, dateformat, System.Globalization.DateTimeStyles.AssumeLocal);
            lblTDate.Text = TDate.ToString("dd-MMM-yyyy");
            string TdT = TDate.ToString("yyyy/MM/dd");

            lblStNM.Text = StoreNM;
            lblItNM.Text = ItemNM + " || " + ItemCD;

            dbFunctions.lblAdd(@"SELECT B.CLQTY FROM (SELECT SUBSTRING(B.ITEMID,1,5) AS SUBID, B.ITEMID, B.CLQTY, B.AVGRATE, (CASE WHEN B.CLQTY < 0 THEN 0.00 ELSE B.CLQTY * B.AVGRATE END) AS STOCKVALUE, 
                    STK_ITEMMST.SUBNM, STK_ITEM.ITEMNM + ' || ' + STK_ITEM.ITEMCD AS ITEMNM,  ISNULL(STK_ITEM.PQTY,0) AS PQTY, 
                    ISNULL((CASE WHEN STK_ITEM.PQTY = 0 THEN 0 ELSE FLOOR(B.CLQTY/PQTY)END),0) AS CARTONQTY, 
                    ISNULL((B.CLQTY - (CASE WHEN STK_ITEM.PQTY = 0 THEN 0 ELSE (FLOOR(B.CLQTY/PQTY)*PQTY) END)),0) AS PIECES  
                    FROM (SELECT  ITEMID, (SUM(INQTY) + SUM(BQTY) + SUM(SALERET)) - (SUM(OUTQTY) + SUM(SQTY) + SUM(PURCHASERET)) AS CLQTY, 
                    SUM(BQTY) AS BQTY, SUM(BAMT) AS BAMT, SUM(SQTY) AS SQTY,  SUM(SAMT) AS SAMT, 
                    (CASE WHEN SUM(BAMTR) = 0.00 THEN 0.00 ELSE CONVERT(decimal(18, 2), (SUM(BAMTR)) / SUM(BQTYR)) END) AS AVGRATE  
                    FROM ( 
                    SELECT  ITEMID, 0 AS BQTY, 0 AS BAMT, SUM(QTY) AS SQTY, SUM(AMOUNT) AS SAMT, 0 AS INQTY, 0 AS OUTQTY, 0 AS BQTYR, 
                    0 AS SALERET, 0 AS PURCHASERET, 0 AS BAMTR  
                    FROM STK_TRANS AS STK_TRANS_1 WHERE (TRANSDT < '" + FdT + "') AND (TRANSTP = 'SALE') AND " +
                               "(UNITFR = '" + StoreID + "')   AND ITEMID='" + ItemID + "' GROUP BY  ITEMID   UNION SELECT ITEMID, 0 AS BQTY, 0 AS BAMT, " +
                               "0 AS SQTY, 0 AS SAMT, 0 AS INQTY, 0 AS OUTQTY, 0 AS BQTYR, SUM(QTY) AS SALERET, 0 AS PURCHASERET, " +
                               "0 AS BAMTR  FROM STK_TRANS AS STK_TRANS_2  WHERE (TRANSDT < '" + FdT + "') AND (TRANSTP = 'IRTS')  " +
                               "AND (UNITTO = '" + StoreID + "')   AND ITEMID='" + ItemID + "' GROUP BY  ITEMID  UNION SELECT  ITEMID, 0 AS BQTY, " +
                               "SUM(AMOUNT) AS BAMT, 0 AS SQTY, 0 AS SAMT, SUM(QTY) AS INQTY, 0 AS OUTQTY, 0 AS BQTYR, 0 AS SALERET, " +
                               "0 AS PURCHASERET, 0 AS BAMTR  FROM STK_TRANS  WHERE (TRANSDT < '" + FdT + "') AND (TRANSTP = 'BUY') " +
                               "AND (UNITTO = '" + StoreID + "')  AND ITEMID='" + ItemID + "' GROUP BY  ITEMID  UNION  SELECT  ITEMID, 0 AS BQTY, " +
                               "0 AS BAMT, 0 AS SQTY, SUM(AMOUNT) AS SAMT, 0 AS INQTY, SUM(QTY) AS OUTQTY, 0 AS BQTYR, 0 AS SALERET, " +
                               "0 AS PURCHASERET, 0 AS BAMTR  FROM STK_TRANS AS STK_TRANS_1  WHERE (TRANSDT < '" + FdT + "') " +
                               "AND (TRANSTP = 'IISS') AND (UNITFR = '" + StoreID + "') AND ITEMID='" + ItemID + "'  GROUP BY  ITEMID   UNION  " +
                               "SELECT  ITEMID, 0 AS BQTY, 0 AS BAMT, 0 AS SQTY, 0 AS SAMT, 0 AS INQTY, 0 AS OUTQTY,SUM(QTY) " +
                               "AS BQTYR, 0 AS SALERET, 0 AS PURCHASERET, SUM(AMOUNT) AS BAMTR  FROM STK_TRANS  " +
                               "WHERE (TRANSDT < '" + FdT + "') AND (TRANSTP = 'BUY')  AND (UNITTO = '" + StoreID + "') " +
                               "AND ITEMID='" + ItemID + "' GROUP BY  ITEMID ) AS A GROUP BY  ITEMID) AS B " +
                               "INNER JOIN  STK_ITEM ON SUBSTRING(B.ITEMID,1,5) = STK_ITEM.SUBID AND B.ITEMID = STK_ITEM.ITEMID " +
                               "INNER JOIN  STK_ITEMMST ON STK_ITEM.SUBID = STK_ITEMMST.SUBID  WHERE (B.CLQTY <> 0) ) AS B " +
                               "INNER JOIN STK_ITEM ON B.SUBID = STK_ITEM.SUBID AND B.ITEMID = STK_ITEM.ITEMID " +
                               "INNER JOIN STK_ITEMMST ON STK_ITEM.SUBID = STK_ITEMMST.SUBID  WHERE (B.CLQTY <> 0) ", lblOpenBalance);
            if (lblOpenBalance.Text == "")
            {
                lblOpenBalance.Text = "0.00";
            }
            else
                lblOpenBalance.Text = SpellAmount.comma(Convert.ToDecimal(lblOpenBalance.Text));

            showGrid();
        }

        public void showGrid()
        {
            string connectionString = ConfigurationManager.ConnectionStrings["AslDbContext"].ConnectionString;
            SqlConnection conn = new SqlConnection(connectionString);

            string StoreNM = Session["StoreNm"].ToString();
            string StoreID = Session["StoreID"].ToString();
            string ItemNM = Session["ItemNM"].ToString();
            string ItemID = Session["ItemID"].ToString();
            string txtFrom = Session["From"].ToString();
            string txtTo = Session["To"].ToString();

            DateTime FDate = DateTime.Parse(txtFrom, dateformat, System.Globalization.DateTimeStyles.AssumeLocal);
            string FdT = FDate.ToString("yyyy/MM/dd");

            DateTime TDate = DateTime.Parse(txtTo, dateformat, System.Globalization.DateTimeStyles.AssumeLocal);
            string TdT = TDate.ToString("yyyy/MM/dd");

            conn.Open();
            SqlCommand cmd = new SqlCommand(@"SELECT CONVERT(nvarchar(20), TRANSDT, 103) AS TRANSDT,  TRANSNO, BUY, SALE, RTSQTY, ISSQTY, STORENM , ((BUY+RTSQTY)-(SALE+ISSQTY)) AS TOTBLNS
                    FROM         (SELECT     B.TRANSDT, B.TRANSNO, B.BUY, B.SALE, B.RTSQTY, B.ISSQTY, B.SALERET, B.PURCHASERET, B.UNIT, dbo.STK_STORE.STORENM  
                    FROM (SELECT     A.TRANSDT, A.TRANSNO, A.BUY, A.SALE, A.RTSQTY, A.ISSQTY, A.UNIT, A.SALERET, A.PURCHASERET
                    FROM          (
                    SELECT     TRANSDT,  TRANSNO,  QTY AS BUY, 0 AS SALE, 0 AS RTSQTY, 0 AS ISSQTY, 0 AS SALERET, 0 AS PURCHASERET,   UNITTO AS UNIT  
                    FROM         dbo.STK_TRANS AS STK_TRANS_3  
                    WHERE     (TRANSTP = 'BUY') AND (UNITTO = @STOREID) AND (TRANSDT BETWEEN @FDT AND @TDT) AND (ITEMID =@ITEMID)  
                    UNION  
                    SELECT     TRANSDT,  TRANSNO, 0 AS BUY, QTY AS SALE, 0 AS RTSQTY, 0 AS ISSQTY, 0 AS SALERET, 0 AS PURCHASERET,   UNITFR AS UNIT  
                    FROM          dbo.STK_TRANS AS STK_TRANS_4  
                    WHERE      (TRANSTP = 'SALE') AND (UNITFR = @STOREID) AND (TRANSDT BETWEEN @FDT AND @TDT) AND (ITEMID =@ITEMID)  
                    UNION  
                    SELECT     TRANSDT,  TRANSNO,  0 AS BUY, 0 AS SALE, QTY AS RTSQTY, 0 AS ISSQTY, 0 AS SALERET, 0 AS PURCHASERET,   UNITTO AS UNIT  
                    FROM         dbo.STK_TRANS AS STK_TRANS_2  
                    WHERE     (TRANSTP = 'IRTS') AND (UNITTO = @STOREID) AND (TRANSDT BETWEEN @FDT AND @TDT) AND (ITEMID =@ITEMID)  
                    UNION
                    SELECT     TRANSDT,  TRANSNO, 0 AS BUY, 0 AS SALE, 0 AS RTSQTY, QTY AS ISSQTY, 0 AS SALERET, 0 AS PURCHASERET,   UNITTO AS UNIT  
                    FROM          dbo.STK_TRANS AS STK_TRANS_4  
                    WHERE      (TRANSTP = 'IISS') AND (UNITFR = @STOREID) AND (TRANSDT BETWEEN @FDT AND @TDT) AND (ITEMID =@ITEMID)  
                    ) AS A ) AS B LEFT OUTER JOIN  
                    dbo.STK_STORE ON B.UNIT COLLATE Latin1_General_CI_AS = dbo.STK_STORE.STOREID) AS C ORDER BY TRANSDT, TRANSNO", conn);
            cmd.Parameters.AddWithValue("@FDT", FdT);
            cmd.Parameters.AddWithValue("@TDT", TdT);
            cmd.Parameters.AddWithValue("@ITEMID", ItemID);
            cmd.Parameters.AddWithValue("@STOREID", StoreID);
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

                Balance();
            }
            else
            {
                GridView1.DataSource = ds;
                GridView1.DataBind();
                GridView1.Visible = true;
            }
        }

        public void Balance()
        {
            try
            {
                for (int i = 0; i < GridView1.Rows.Count; i++)
                {
                    if (i == 0)
                    {
                        string Buy = GridView1.Rows[i].Cells[3].Text;
                        decimal BuyQty = Convert.ToDecimal(Buy);
                        string Sale = GridView1.Rows[i].Cells[4].Text;
                        decimal SaleQty = Convert.ToDecimal(Sale);
                        string In = GridView1.Rows[i].Cells[5].Text;
                        decimal InQty = Convert.ToDecimal(In);
                        string Out = GridView1.Rows[i].Cells[6].Text;
                        decimal OutQty = Convert.ToDecimal(Out);
                        decimal OpenBal = Convert.ToDecimal(lblOpenBalance.Text);
                        decimal CumBalance = (OpenBal + BuyQty + InQty) - (SaleQty + OutQty);
                        GridView1.Rows[i].Cells[7].Text = SpellAmount.comma(CumBalance);
                        GridView1.FooterRow.Cells[7].Text = GridView1.Rows[i].Cells[7].Text;
                    }
                    else
                    {
                        string BlnC = GridView1.Rows[i - 1].Cells[7].Text;
                        decimal CumulativeBalance = decimal.Parse(BlnC);

                        string Buy = GridView1.Rows[i].Cells[3].Text;
                        decimal BuyQty = Convert.ToDecimal(Buy);
                        string Sale = GridView1.Rows[i].Cells[4].Text;
                        decimal SaleQty = Convert.ToDecimal(Sale);
                        string In = GridView1.Rows[i].Cells[5].Text;
                        decimal InQty = Convert.ToDecimal(In);
                        string Out = GridView1.Rows[i].Cells[6].Text;
                        decimal OutQty = Convert.ToDecimal(Out);

                        decimal Balance = (CumulativeBalance + BuyQty + InQty) - (SaleQty + OutQty);
                        GridView1.Rows[i].Cells[7].Text = SpellAmount.comma(Balance);
                        GridView1.FooterRow.Cells[7].Text = GridView1.Rows[i].Cells[7].Text;
                    }
                }
            }
            catch (Exception ex)
            {
                Response.Write(ex.Message);
            }
        }

        protected void GridView1_RowDataBound(object sender, GridViewRowEventArgs e)
        {
            if (e.Row.RowType == DataControlRowType.DataRow)
            {
                string TRANSDT = DataBinder.Eval(e.Row.DataItem, "TRANSDT").ToString();
                e.Row.Cells[0].Text = TRANSDT;
                //tex = DataBinder.Eval(e.Row.DataItem, "PSID").ToString();

                string PARTICULARS = DataBinder.Eval(e.Row.DataItem, "TRANSNO").ToString();
                e.Row.Cells[1].Text = "&nbsp;" + PARTICULARS;

                string OUTQTY = DataBinder.Eval(e.Row.DataItem, "STORENM").ToString();
                e.Row.Cells[2].Text = OUTQTY;

                decimal BUY = Convert.ToDecimal(DataBinder.Eval(e.Row.DataItem, "BUY").ToString());
                string bQty = SpellAmount.comma(BUY);
                e.Row.Cells[3].Text = bQty;

                decimal SALE = Convert.ToDecimal(DataBinder.Eval(e.Row.DataItem, "SALE").ToString());
                string sQty = SpellAmount.comma(SALE);
                e.Row.Cells[4].Text = sQty;

                decimal INQTY = Convert.ToDecimal(DataBinder.Eval(e.Row.DataItem, "RTSQTY").ToString());
                string iQty = SpellAmount.comma(INQTY);
                e.Row.Cells[5].Text = iQty;

                decimal OUTqty = Convert.ToDecimal(DataBinder.Eval(e.Row.DataItem, "ISSQTY").ToString());
                string oQty = SpellAmount.comma(OUTqty);
                e.Row.Cells[6].Text = oQty;

                decimal TOTQTY = Convert.ToDecimal(DataBinder.Eval(e.Row.DataItem, "TOTBLNS").ToString());
                string tQty = SpellAmount.comma(TOTQTY);
                e.Row.Cells[7].Text = tQty;



                tot_Buy += BUY;
                Tot_BuyComma = SpellAmount.comma(tot_Buy);

                tot_Sale += SALE;
                Tot_SaleComma = SpellAmount.comma(tot_Sale);

                tot_In += INQTY;
                Tot_InComma = SpellAmount.comma(tot_In);

                tot_Out += OUTqty;
                Tot_OutComma = SpellAmount.comma(tot_Out);

                tot_Sret += ((BUY + INQTY) - (SALE + OUTqty));
                Tot_SretComma = SpellAmount.comma(tot_Sret);
            }

            else if (e.Row.RowType == DataControlRowType.Footer)
            {
                e.Row.Cells[2].Text = "TOTAL :";
                e.Row.Cells[2].HorizontalAlign = HorizontalAlign.Right;
                e.Row.Cells[3].Text = Tot_BuyComma;
                e.Row.Cells[3].HorizontalAlign = HorizontalAlign.Right;
                e.Row.Cells[4].Text = Tot_SaleComma;
                e.Row.Cells[4].HorizontalAlign = HorizontalAlign.Right;
                e.Row.Cells[5].Text = Tot_InComma;
                e.Row.Cells[5].HorizontalAlign = HorizontalAlign.Right;
                e.Row.Cells[6].Text = Tot_OutComma;
                e.Row.Cells[6].HorizontalAlign = HorizontalAlign.Right;
                e.Row.Cells[7].Text = Tot_SretComma;
                e.Row.Cells[7].HorizontalAlign = HorizontalAlign.Right;

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