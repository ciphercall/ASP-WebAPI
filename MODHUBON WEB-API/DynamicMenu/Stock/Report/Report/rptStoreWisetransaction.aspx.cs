using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace AlchemyAccounting.Stock.Report.Report
{
    public partial class StoreWisetransaction : System.Web.UI.Page
    {
        IFormatProvider dateformat = new System.Globalization.CultureInfo("fr-FR", true);

        decimal dblTotalAmount = 0;
        string dblTotalAmountComma = "";

        decimal dblTotalQty = 0;
        string dblTotalQtyComma = "";
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

                    string type = Session["Type"].ToString();
                    string fromDate = Session["FromDt"].ToString();
                    string toDate = Session["ToDt"].ToString();
                    string storeName = Session["StoreName"].ToString();
                    lblStore.Text = storeName;
                    lblType.Text = Type(type);

                    DateTime fDate = DateTime.Parse(fromDate, dateformat, System.Globalization.DateTimeStyles.AssumeLocal);
                    lblFDate.Text = fDate.ToString("dd-MMM-yyyy");

                    DateTime date = DateTime.Parse(toDate, dateformat, System.Globalization.DateTimeStyles.AssumeLocal);
                    lblTDate.Text = date.ToString("dd-MMM-yyyy");

                    ShowGrid();
                }
            }
            catch (Exception ex)
            {
                Response.Write(ex.Message);
            }
        }


        public string Type(string type)
        {
            string rtType = "";

            if (type == "SALE")
                rtType = "SALE";
            else if (type == "IRTS")
                rtType = "EXCHANGE";
            else if (type == "IREC")
                rtType = "RECEIVE";
            else if (type == "IISS")
                rtType = "ISSUE";

            return rtType;
        }
        public void ShowGrid()
        {
            string connectionString = ConfigurationManager.ConnectionStrings["AslDbContext"].ConnectionString;
            SqlConnection conn = new SqlConnection(connectionString);

            string store = Session["StoreId"].ToString();
            string type = Session["Type"].ToString();
            string From = Session["FromDt"].ToString();
            string To = Session["ToDt"].ToString();

            DateTime FDate = DateTime.Parse(From, dateformat, System.Globalization.DateTimeStyles.AssumeLocal);
            string FDT = FDate.ToString("yyyy/MM/dd");

            DateTime TDate = DateTime.Parse(To, dateformat, System.Globalization.DateTimeStyles.AssumeLocal);
            string TDT = TDate.ToString("yyyy/MM/dd");

            conn.Open();
            SqlCommand cmd = new SqlCommand();
            cmd = new SqlCommand("", conn);

            cmd.CommandType = CommandType.Text;
            cmd.CommandText = (@"SELECT A.ITEMID, ITEMNM, SUM(QTY) AS QTY, SUM(AMOUNT) AMOUNT, CAST((SUM(AMOUNT)/ SUM(QTY)) as decimal(18,2)) AS RATE
                    FROM(
                    SELECT SUBSTRING(ITEMID,1,5) SUBID, ITEMID, SUM(QTY) AS QTY, SUM(AMOUNT) AMOUNT
                    FROM STK_TRANS
                    WHERE TRANSTP IN ('SALE','IISS')
                    AND   TRANSTP = @TYPE
                    AND   UNITFR  = @STORE
                    AND   TRANSDT BETWEEN @FDT AND @TDT
                    GROUP BY SUBSTRING(ITEMID,1,5), ITEMID
                    UNION
                    SELECT SUBSTRING(ITEMID,1,5) SUBID, ITEMID, SUM(QTY) AS QTY, SUM(AMOUNT) AMOUNT
                    FROM STK_TRANS
                    WHERE TRANSTP IN ('BUY','IRTS')
                    AND   TRANSTP = @TYPE
                    AND   UNITTO  = @STORE
                    AND   TRANSDT BETWEEN @FDT AND @TDT
                    GROUP BY SUBSTRING(ITEMID,1,5), ITEMID
                    ) A INNER JOIN STK_ITEM ON A.ITEMID = STK_ITEM.ITEMID GROUP BY A.ITEMID, ITEMNM");
            cmd.Parameters.AddWithValue("@STORE", store);
            cmd.Parameters.AddWithValue("@TYPE", type);
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
                string ITEMNM = DataBinder.Eval(e.Row.DataItem, "ITEMNM").ToString();
                e.Row.Cells[0].Text = "&nbsp;" + ITEMNM;

                decimal QTY = Convert.ToDecimal(DataBinder.Eval(e.Row.DataItem, "QTY").ToString());
                string qty = SpellAmount.comma(QTY);
                e.Row.Cells[1].Text = qty;

                string RATE = DataBinder.Eval(e.Row.DataItem, "RATE").ToString();
                e.Row.Cells[2].Text = "&nbsp;" + RATE;

                decimal AMOUNT = Convert.ToDecimal(DataBinder.Eval(e.Row.DataItem, "AMOUNT").ToString());
                string amnt = SpellAmount.comma(AMOUNT);
                e.Row.Cells[3].Text = amnt;

                dblTotalAmount += AMOUNT;
                dblTotalAmountComma = SpellAmount.comma(dblTotalAmount);

                dblTotalQty += QTY;
                dblTotalQtyComma = SpellAmount.comma(dblTotalQty);

            }

            else if (e.Row.RowType == DataControlRowType.Footer)
            {
                e.Row.Cells[0].Text = "Grand Total :   ";
                e.Row.Cells[0].HorizontalAlign = HorizontalAlign.Right;
                e.Row.Cells[1].Text = dblTotalQtyComma;
                e.Row.Cells[1].HorizontalAlign = HorizontalAlign.Center;
                e.Row.Cells[3].Text = dblTotalAmountComma;
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
            }
        }

    }
}