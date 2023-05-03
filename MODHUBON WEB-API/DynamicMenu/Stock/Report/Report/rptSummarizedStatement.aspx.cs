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
    public partial class rptSummarizedStatement : System.Web.UI.Page
    {
        IFormatProvider dateformat = new System.Globalization.CultureInfo("fr-FR", true);

        decimal dblTotalAmount = 0;
        string dblTotalAmountComma = "";

        protected void Page_Load(object sender, EventArgs e)
        {
            try
            {
                string uTp = HttpContext.Current.Session["USERTYPE"].ToString();
                string brCD = HttpContext.Current.Session["BrCD"].ToString();
                SqlCommand cmd = new SqlCommand();
                if (uTp == "COMPADMIN")
                {
                    dbFunctions.lblAdd(@"SELECT COMPNM FROM ASL_COMPANY WHERE COMPID ='101'", lblCompNM);
                    dbFunctions.lblAdd(@"SELECT ADDRESS FROM ASL_COMPANY WHERE COMPID ='101'", lblAddress);
                }
                else
                {
                    dbFunctions.lblAdd(@"SELECT COMPNM FROM ASL_COMPANY WHERE COMPID ='" + brCD + "'", lblCompNM);
                    dbFunctions.lblAdd(@"SELECT ADDRESS FROM ASL_COMPANY WHERE COMPID ='" + brCD + "'", lblAddress);
                }

                DateTime PrintDate = dbFunctions.Timezone(DateTime.Now);
                string td = PrintDate.ToString("dd-MMM-yyyy hh:mm tt");
                lblTime.Text = td;

                string Type = Session["Type"].ToString();
                string From = Session["From"].ToString();
                string To = Session["To"].ToString();

                lblType.Text = Type;

                DateTime FDate = DateTime.Parse(From, dateformat, System.Globalization.DateTimeStyles.AssumeLocal);
                lblFDate.Text = FDate.ToString("dd-MMM-yyyy");

                DateTime TDate = DateTime.Parse(To, dateformat, System.Globalization.DateTimeStyles.AssumeLocal);
                lblTDate.Text = TDate.ToString("dd-MMM-yyyy");

                showGrid();
            }
            catch (Exception ex)
            {
                Response.Write(ex.Message);
            }
        }

        public void showGrid()
        {
            string connectionString = ConfigurationManager.ConnectionStrings["AslDbContext"].ConnectionString;
            SqlConnection conn = new SqlConnection(connectionString);

            string Type = Session["Type"].ToString();
            string From = Session["From"].ToString();
            string To = Session["To"].ToString();

            DateTime FDate = DateTime.Parse(From, dateformat, System.Globalization.DateTimeStyles.AssumeLocal);
            string FDT = FDate.ToString("yyyy/MM/dd");

            DateTime TDate = DateTime.Parse(To, dateformat, System.Globalization.DateTimeStyles.AssumeLocal);
            string TDT = TDate.ToString("yyyy/MM/dd");

            conn.Open();
            SqlCommand cmd = new SqlCommand();
            cmd = new SqlCommand("", conn);

            if (Type == "SALE")
            {
                cmd.CommandType = CommandType.Text;
                cmd.CommandText = (" SELECT ROW_NUMBER() over (order by STK_TRANS.PSID) as SL, STK_TRANS.PSID, SUM(STK_TRANS.AMOUNT) AS AMOUNT, GL_ACCHART.ACCOUNTNM " +
                                   " FROM STK_TRANS INNER JOIN GL_ACCHART ON STK_TRANS.PSID = GL_ACCHART.ACCOUNTCD WHERE (STK_TRANS.TRANSTP = 'SALE') " +
                                   " AND (STK_TRANS.TRANSDT BETWEEN '" + FDT + "' AND '" + TDT + "') GROUP BY STK_TRANS.PSID, GL_ACCHART.ACCOUNTNM " +
                                   " order by STK_TRANS.PSID");
            }

            else if (Type == "BUY")
            {
                cmd.CommandType = CommandType.Text;
                cmd.CommandText = (" SELECT ROW_NUMBER() over (order by STK_TRANS.PSID) as SL, STK_TRANS.PSID, SUM(STK_TRANS.AMOUNT) AS AMOUNT, GL_ACCHART.ACCOUNTNM " +
                                   " FROM STK_TRANS INNER JOIN GL_ACCHART ON STK_TRANS.PSID = GL_ACCHART.ACCOUNTCD WHERE (STK_TRANS.TRANSTP = 'BUY') " +
                                   " AND (STK_TRANS.TRANSDT BETWEEN '" + FDT + "' AND '" + TDT + "')  " +
                                   " GROUP BY STK_TRANS.PSID, GL_ACCHART.ACCOUNTNM order by STK_TRANS.PSID");
            }

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
                string SL = DataBinder.Eval(e.Row.DataItem, "SL").ToString();
                e.Row.Cells[0].Text = SL;

                string ACCOUNTNM = DataBinder.Eval(e.Row.DataItem, "ACCOUNTNM").ToString();
                e.Row.Cells[1].Text ="&nbsp;" + ACCOUNTNM;

                decimal AMOUNT = Convert.ToDecimal(DataBinder.Eval(e.Row.DataItem, "AMOUNT").ToString());
                string amnt = SpellAmount.comma(AMOUNT);
                e.Row.Cells[2].Text = amnt;

                dblTotalAmount += AMOUNT;
                dblTotalAmountComma = SpellAmount.comma(dblTotalAmount);

            }

            else if (e.Row.RowType == DataControlRowType.Footer)
            {
                e.Row.Cells[1].Text = "Grand Total :   ";
                e.Row.Cells[1].HorizontalAlign = HorizontalAlign.Right;
                e.Row.Cells[2].Text = dblTotalAmountComma;
                e.Row.Cells[2].HorizontalAlign = HorizontalAlign.Right;
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