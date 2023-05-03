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
    public partial class RtpOrderSummaryItemWIse : System.Web.UI.Page
    {
        IFormatProvider dateformat = new System.Globalization.CultureInfo("fr-FR", true);

        decimal totalQty = 0;

        string totalQtyString = "";
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
                try
                {
                    dbFunctions.lblAdd(@"SELECT COMPNM FROM ASL_COMPANY WHERE COMPID ='101'", lblCompNM);
                    dbFunctions.lblAdd(@"SELECT ADDRESS FROM ASL_COMPANY WHERE COMPID ='101'", lblAddress);

                    DateTime PrintDate = dbFunctions.Timezone(DateTime.Now);
                    string td = PrintDate.ToString("dd-MMM-yyyy hh:mm tt");
                    lblTime.Text = td;

                    ShowGrid();
                }
                catch
                {
                }
        }

        public void ShowGrid()
        {
            SqlConnection conn = new SqlConnection(dbFunctions.connection);

            string dateFrom = Session["From"].ToString();
            DateTime datefrom = DateTime.Parse(dateFrom, dateformat, System.Globalization.DateTimeStyles.AssumeLocal);
            string from = datefrom.ToString("yyyy/MM/dd");

            string dateTo = Session["To"].ToString();
            DateTime datato = DateTime.Parse(dateTo, dateformat, System.Globalization.DateTimeStyles.AssumeLocal);
            string to = datato.ToString("yyyy/MM/dd");


            lblDateFrom.Text = datefrom.ToString("dd-MM-yyyy");
            lblDateTo.Text = datato.ToString("dd-MM-yyyy");

            conn.Open();
            SqlCommand cmd = new SqlCommand(@"SELECT A.ITEMID, ITEMNM, ITEMNMB, SUM(ISNULL(QTY,0)) QTY 
            FROM STK_TRANS A INNER JOIN STK_ITEM B ON A.ITEMID = B.ITEMID
            WHERE TRANSTP = 'IORD' AND TRANSDT BETWEEN '" + from + "' AND '" + to + "' GROUP BY A.ITEMID, ITEMNM, ITEMNMB", conn);
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
                string ITEMID = DataBinder.Eval(e.Row.DataItem, "ITEMID").ToString();
                e.Row.Cells[0].Text = "&nbsp;" + ITEMID;
                
                string ITEMNM = DataBinder.Eval(e.Row.DataItem, "ITEMNM").ToString();
                e.Row.Cells[1].Text = "&nbsp;" + ITEMNM;

                string ITEMNMB = DataBinder.Eval(e.Row.DataItem, "ITEMNMB").ToString();
                e.Row.Cells[2].Text = "&nbsp;" + ITEMNMB;

                decimal QTY = Convert.ToDecimal(DataBinder.Eval(e.Row.DataItem, "QTY").ToString());
                string qty = SpellAmount.comma(QTY);
                e.Row.Cells[3].Text = qty;

                totalQty += QTY;
                totalQtyString = SpellAmount.comma(totalQty);
            }

            else if (e.Row.RowType == DataControlRowType.Footer)
            {
                e.Row.Cells[2].Text = "TOTAL :";
                e.Row.Cells[2].HorizontalAlign = HorizontalAlign.Right;
                //e.Row.Cells[1].Text = Tot_CartonQtyComma;
                //e.Row.Cells[1].HorizontalAlign = HorizontalAlign.Right;
                e.Row.Cells[3].Text = totalQtyString;
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