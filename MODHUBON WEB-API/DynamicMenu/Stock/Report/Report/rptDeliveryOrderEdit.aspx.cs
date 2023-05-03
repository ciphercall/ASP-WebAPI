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
    public partial class rptDeliveryOrderEdit : System.Web.UI.Page
    {
        IFormatProvider dateformat = new System.Globalization.CultureInfo("fr-FR", true);

        decimal tot_carQty = 0;
        decimal tot_pQty = 0;
        decimal tot_tQty = 0;

        string tot_carQtyComma = "";
        string tot_pQtyComma = "";
        string tot_tQtyComma = "";

        protected void Page_Load(object sender, EventArgs e)
        {
            try
            {
                dbFunctions.lblAdd(@"SELECT COMPNM FROM ASL_COMPANY", lblCompNM);
                dbFunctions.lblAdd(@"SELECT ADDRESS FROM ASL_COMPANY", lblAddress);
                dbFunctions.lblAdd(@"SELECT CONTACTNO FROM ASL_COMPANY", lblContact);

                DateTime PrintDate = dbFunctions.Timezone(DateTime.Now);
                string td = PrintDate.ToString("dd-MMM-yyyy hh:mm tt");
                lblTime.Text = td;

                string InVDT = Session["InvDate_S"].ToString();
                string InVNoEdit = Session["InvNoEdit_S"].ToString();
                string Memo_S = Session["Memo_S"].ToString();
                string StoreNM_S = Session["StoreNM_S"].ToString();
                string StoreID_S = Session["StoreID_S"].ToString();
                string PartyNM_S = Session["PartyNM_S"].ToString();
                string PartyID_S = Session["PartyID_S"].ToString();

                DateTime InDT = DateTime.Parse(InVDT, dateformat, System.Globalization.DateTimeStyles.AssumeLocal);
                lblInVDT.Text = InDT.ToString("dd-MMM-yyyy");
                lblInVNo.Text = InVNoEdit;
                lblSalesMemoNo.Text = Memo_S;
                lblSalesTo.Text = PartyNM_S;
                dbFunctions.lblAdd(@"SELECT ADDRESS FROM STK_PS WHERE PSID='" + PartyID_S + "' ", lblSaleToAdd);
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

            string InVDT = Session["InvDate_S"].ToString();
            string InVNoEdit = Session["InvNoEdit_S"].ToString();
            string Memo_S = Session["Memo_S"].ToString();
            string StoreNM_S = Session["StoreNM_S"].ToString();
            string StoreID_S = Session["StoreID_S"].ToString();
            string PartyNM_S = Session["PartyNM_S"].ToString();
            string PartyID_S = Session["PartyID_S"].ToString();

            DateTime InDT = DateTime.Parse(InVDT, dateformat, System.Globalization.DateTimeStyles.AssumeLocal);
            string inDate = InDT.ToString("yyyy/MM/dd");

            conn.Open();
            SqlCommand cmd = new SqlCommand("SELECT ROW_NUMBER()over(order by STK_TRANS.TRANSSL) as SL, dbo.STK_ITEMMST.CATNM, dbo.STK_ITEM.ITEMNM + ' || ' + STK_ITEM.ITEMCD AS ITEMNM, dbo.GL_ACCHART.ACCOUNTNM, dbo.STK_STORE.STORENM, dbo.STK_TRANS.TRANSTP, " +
                                " dbo.STK_TRANS.TRANSDT, dbo.STK_TRANS.TRANSMY, dbo.STK_TRANS.TRANSNO, dbo.STK_TRANS.INVREFNO, dbo.STK_TRANS.STOREFR, " +
                                " dbo.STK_TRANS.STORETO, dbo.STK_TRANS.PSID, dbo.STK_TRANS.LCTP, dbo.STK_TRANS.LCID, dbo.STK_TRANS.LCDATE, dbo.STK_TRANS.REMARKS, " +
                                " dbo.STK_TRANS.TRANSSL, dbo.STK_TRANS.CATID, dbo.STK_TRANS.ITEMID, dbo.STK_TRANS.UNITTP, dbo.STK_TRANS.CPQTY, dbo.STK_TRANS.CQTY, " +
                                " dbo.STK_TRANS.PQTY, dbo.STK_TRANS.QTY, dbo.STK_TRANS.RATE, dbo.STK_TRANS.AMOUNT, dbo.STK_TRANS.USERPC, dbo.STK_TRANS.USERID,  " +
                                " dbo.STK_TRANS.ACTDTI, dbo.STK_TRANS.INTIME, dbo.STK_TRANS.IPADDRESS FROM dbo.STK_TRANS INNER JOIN " +
                                " dbo.STK_ITEM ON dbo.STK_TRANS.CATID = dbo.STK_ITEM.CATID AND dbo.STK_TRANS.ITEMID = dbo.STK_ITEM.ITEMID INNER JOIN " +
                                " dbo.STK_ITEMMST ON dbo.STK_ITEM.CATID = dbo.STK_ITEMMST.CATID INNER JOIN " +
                                " dbo.GL_ACCHART ON dbo.STK_TRANS.PSID = dbo.GL_ACCHART.ACCOUNTCD INNER JOIN " +
                                " dbo.STK_STORE ON dbo.STK_TRANS.STOREFR COLLATE Latin1_General_CI_AS = dbo.STK_STORE.STOREID " +
                                " WHERE (dbo.STK_TRANS.TRANSTP = 'SALE') AND (dbo.STK_TRANS.TRANSDT = '" + inDate + "') AND (dbo.STK_TRANS.TRANSNO = '" + InVNoEdit + "') AND " +
                                " (dbo.STK_TRANS.STOREFR = '" + StoreID_S + "') AND (dbo.STK_TRANS.PSID = '" + PartyID_S + "') " +
                                " ORDER BY dbo.STK_TRANS.TRANSSL", conn);
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
                string SL = DataBinder.Eval(e.Row.DataItem, "SL").ToString();
                e.Row.Cells[0].Text = SL;

                string ITEMNM = DataBinder.Eval(e.Row.DataItem, "ITEMNM").ToString();
                e.Row.Cells[1].Text = "&nbsp;" + ITEMNM;

                decimal CQTY = Convert.ToDecimal(DataBinder.Eval(e.Row.DataItem, "CQTY").ToString());
                string cQty = SpellAmount.comma(CQTY);
                e.Row.Cells[2].Text = cQty;

                decimal CPQTY = Convert.ToDecimal(DataBinder.Eval(e.Row.DataItem, "CPQTY").ToString());
                string cpQty = SpellAmount.comma(CPQTY);
                e.Row.Cells[3].Text = cpQty;

                decimal QTY = Convert.ToDecimal(DataBinder.Eval(e.Row.DataItem, "QTY").ToString());
                string totQty = SpellAmount.comma(QTY);
                e.Row.Cells[4].Text = totQty;

                tot_carQty += CQTY;
                tot_carQtyComma = SpellAmount.comma(tot_carQty);

                tot_pQty += CPQTY;
                tot_pQtyComma = SpellAmount.comma(tot_pQty);

                tot_tQty += QTY;
                tot_tQtyComma = SpellAmount.comma(tot_tQty);
            }

            else if (e.Row.RowType == DataControlRowType.Footer)
            {
                e.Row.Cells[1].Text = "TOTAL :";
                e.Row.Cells[1].HorizontalAlign = HorizontalAlign.Right;
                e.Row.Cells[2].Text = tot_carQtyComma;
                e.Row.Cells[2].HorizontalAlign = HorizontalAlign.Right;
                e.Row.Cells[3].Text = tot_pQtyComma;
                e.Row.Cells[3].HorizontalAlign = HorizontalAlign.Right;
                e.Row.Cells[4].Text = tot_tQtyComma;
                e.Row.Cells[4].HorizontalAlign = HorizontalAlign.Right;
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