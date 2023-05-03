using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using AlchemyAccounting;

namespace DynamicMenu.Stock.Report.Report
{
    public partial class rptDetailsOpProfitLoss : System.Web.UI.Page
    {
        IFormatProvider dateformat = new System.Globalization.CultureInfo("fr-FR", true);

        decimal  dblSalePrice= 0;
        string dblSalePriceComma = "";

        decimal dblAvgCost = 0;
        string dblAvgCostComma = "";

        decimal dblOpeningPl = 0;
        string dblOpeningPlComma = "";
        protected void Page_Load(object sender, EventArgs e)
        {
            try
            {
                if (!IsPostBack)
                {
                    string uTp = HttpContext.Current.Session["USERTYPE"].ToString();
                    string brCD = HttpContext.Current.Session["BrCD"].ToString();
                    SqlCommand cmd = new SqlCommand();
                    dbFunctions.lblAdd(@"SELECT COMPNM FROM ASL_COMPANY WHERE COMPID ='101'", lblCompNM);
                    dbFunctions.lblAdd(@"SELECT ADDRESS FROM ASL_COMPANY WHERE COMPID ='101'", lblAddress);

                    DateTime PrintDate = dbFunctions.Timezone(DateTime.Now);
                    string td = dbFunctions.Timezone(PrintDate).ToString("dd-MMM-yyyy hh:mm tt");
                    lblTime.Text = td;



                    ShowGrid();
                }
            }
            catch (Exception ex)
            {
                Response.Write(ex.Message);
            }
        }

        public void ShowGrid()
        {
            string connectionString = ConfigurationManager.ConnectionStrings["AslDbContext"].ConnectionString;
            SqlConnection conn = new SqlConnection(connectionString);


            string storeid = Session["StoreID"].ToString();
            string From = Session["FromDate"].ToString();
            string To = Session["ToDate"].ToString();
            string storeName = Session["StoreNM"].ToString();

            DateTime fDate = DateTime.Parse(From, dateformat, System.Globalization.DateTimeStyles.AssumeLocal);
            string FDT = fDate.ToString("yyyy/MM/dd");

            DateTime tDate = DateTime.Parse(To, dateformat, System.Globalization.DateTimeStyles.AssumeLocal);
            string TDT = tDate.ToString("yyyy/MM/dd");

            lblStore.Text = storeName;
            lblFDate.Text = fDate.ToString("dd-MMM-yyyy");
            lblTDate.Text = tDate.ToString("dd-MMM-yyyy");


            conn.Open();
            SqlCommand cmd = new SqlCommand();
            cmd = new SqlCommand("", conn);

            cmd.CommandType = CommandType.Text;

            cmd.CommandText = (@"SELECT CONVERT (nvarchar,C.TRANSDT,103 ) TRANSDT, C.TRANSNO, ITEMNM, AUTHOR, C.ITEMCD, C.SALRT, 
            CAST(C.BUYRT AS DECIMAL(18,2)) AS BUYRT, CAST((C.SALRT-C.BUYRT) AS DECIMAL(18,2)) AS OPPL
            FROM(
            SELECT A.TRANSDT, A.TRANSNO, A.ITEMID, A.ITEMCD, A.ITEMSL, A.SALRT, SUM(ISNULL(AMOUNT,0))/SUM(ISNULL(QTY,0)) BUYRT
            FROM(
            SELECT TRANSDT, TRANSNO, ITEMID, ITEMCD, ITEMSL, ISNULL(RATE,0) SALRT
            FROM STK_TRANS
            WHERE TRANSTP = 'SALE' AND UNITFR =@STORE AND TRANSDT BETWEEN @FDT AND @TDT
            ) A LEFT OUTER JOIN STK_TRANS AS B ON A.ITEMID = B.ITEMID AND 
            B.TRANSTP = 'BUY' AND B.TRANSDT <= A.TRANSDT
            GROUP BY A.TRANSDT, A.TRANSNO, A.ITEMID, A.ITEMCD, A.ITEMSL, A.SALRT
            ) C INNER JOIN STK_ITEM AS D ON C.ITEMID = D.ITEMID
            ORDER BY C.ITEMID");

            cmd.Parameters.AddWithValue("@STORE", storeid);
            cmd.Parameters.AddWithValue("@FDT", FDT);
            cmd.Parameters.AddWithValue("@TDT", TDT);
            SqlDataAdapter da = new SqlDataAdapter(cmd);
            DataTable ds = new DataTable();
            da.Fill(ds);
            conn.Close();
            if (ds.Rows.Count > 0)
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
                string TRANSDT = DataBinder.Eval(e.Row.DataItem, "TRANSDT").ToString();
                e.Row.Cells[0].Text = "&nbsp;" + TRANSDT;

                string TRANSNO = DataBinder.Eval(e.Row.DataItem, "TRANSNO").ToString();
                e.Row.Cells[1].Text = "&nbsp;" + TRANSNO;

                string ITEMNM = DataBinder.Eval(e.Row.DataItem, "ITEMNM").ToString();
                e.Row.Cells[2].Text = "&nbsp;" + ITEMNM;

                string AUTHOR = DataBinder.Eval(e.Row.DataItem, "AUTHOR").ToString();
                e.Row.Cells[3].Text = "&nbsp;" + AUTHOR;

                string ITEMCD = DataBinder.Eval(e.Row.DataItem, "ITEMCD").ToString();
                e.Row.Cells[4].Text = "&nbsp;" + ITEMCD;

                decimal SALRT = Convert.ToDecimal(DataBinder.Eval(e.Row.DataItem, "SALRT").ToString());
                string salert = SpellAmount.comma(SALRT);
                e.Row.Cells[5].Text = salert;

                decimal BUYRT = Convert.ToDecimal(DataBinder.Eval(e.Row.DataItem, "BUYRT").ToString());
                string buyrate = SpellAmount.comma(BUYRT);
                e.Row.Cells[6].Text = buyrate;

                decimal OPPL = Convert.ToDecimal(DataBinder.Eval(e.Row.DataItem, "OPPL").ToString());
                string oppl = SpellAmount.comma(OPPL);
                e.Row.Cells[7].Text = oppl;




                dblSalePrice += SALRT;
                dblSalePriceComma = SpellAmount.comma(dblSalePrice);

                dblAvgCost += BUYRT;
                dblAvgCostComma = SpellAmount.comma(dblAvgCost);

                dblOpeningPl += OPPL;
                dblOpeningPlComma = SpellAmount.comma(dblOpeningPl);

            }

            else if (e.Row.RowType == DataControlRowType.Footer)
            {
                e.Row.Cells[4].Text = "Total :   ";
                e.Row.Cells[4].HorizontalAlign = HorizontalAlign.Right;

                e.Row.Cells[5].Text = dblSalePriceComma;
                e.Row.Cells[5].HorizontalAlign = HorizontalAlign.Center;

                e.Row.Cells[6].Text = dblAvgCostComma;
                e.Row.Cells[6].HorizontalAlign = HorizontalAlign.Center;

                e.Row.Cells[7].Text = dblOpeningPlComma;
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
            }
        }
    }
}