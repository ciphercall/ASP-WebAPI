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
    public partial class rptDeliveryOrder : System.Web.UI.Page
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
                //dbFunctions.lblAdd(@"SELECT COMPNM FROM ASL_COMPANY", lblCompNM);
                //dbFunctions.lblAdd(@"SELECT ADDRESS FROM ASL_COMPANY", lblAddress);
                //dbFunctions.lblAdd(@"SELECT CONTACTNO FROM ASL_COMPANY", lblContact);

                //DateTime PrintDate = dbFunctions.Timezone(DateTime.Now);
                //string td = PrintDate.ToString("dd-MMM-yyyy hh:mm tt");
                //lblTime.Text = td;

                string InVDT = Session["InvDate_S"].ToString();
                string InVNo = Session["InvNo_S"].ToString();
                string Memo_S = Session["Memo_S"].ToString();
                //string StoreNM_S = Session["StoreNM_S"].ToString();
                //string StoreID_S = Session["StoreID_S"].ToString();
                string PartyNM_S = Session["PartyNM_S"].ToString();
                string PartyID_S = Session["PartyID_S"].ToString();

                //DateTime InDT = DateTime.Parse(InVDT, dateformat, System.Globalization.DateTimeStyles.AssumeLocal);
                //lblInVDT.Text = InDT.ToString("dd-MMM-yyyy");
                //lblInVNo.Text = InVNo;
                //lblSalesMemoNo.Text = Memo_S;
                //lblSalesTo.Text = PartyNM_S;
                //dbFunctions.lblAdd(@"SELECT ADDRESS FROM STK_PS WHERE PSID='" + PartyID_S + "' ", lblSaleToAdd);
                //    DateTime InDT = DateTime.Parse(InVDT, dateformat, System.Globalization.DateTimeStyles.AssumeLocal);
                //    string inDate = InDT.ToString("yyyy/MM/dd");

                //    string connectionString = ConfigurationManager.ConnectionStrings["AslDbContext"].ConnectionString;
                //    SqlConnection conn = new SqlConnection(connectionString);
                //    conn.Open();
                //SqlCommand cmdIN = new SqlCommand("SELECT distinct STK_TRANS.STOREFR FROM STK_TRANS INNER JOIN STK_ITEM ON STK_TRANS.CATID = STK_ITEM.CATID AND STK_TRANS.ITEMID = STK_ITEM.ITEMID INNER JOIN " +
                //          " STK_ITEMMST ON STK_ITEM.CATID = STK_ITEMMST.CATID INNER JOIN GL_ACCHART ON STK_TRANS.PSID = GL_ACCHART.ACCOUNTCD INNER JOIN STK_STORE ON STK_TRANS.STOREFR COLLATE Latin1_General_CI_AS = STK_STORE.STOREID CROSS JOIN " +
                //          " ASL_COMPANY WHERE (STK_TRANS.TRANSTP = 'SALE') AND (STK_TRANS.TRANSDT = '" + inDate + "') AND (STK_TRANS.TRANSNO = '" + InVNo + "') AND (STK_TRANS.PSID = '" + PartyID_S + "')", conn);
                //SqlDataReader daIN = cmdIN.ExecuteReader();
                //if (daIN.Read())
                //{
                showGrid();
                //}
                //daIN.Close();
                //conn.Close();

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
            string InVNo = Session["InvNo_S"].ToString();
            string Memo_S = Session["Memo_S"].ToString();
            //string StoreNM_S = Session["StoreNM_S"].ToString();
            //string StoreID_S = Session["StoreID_S"].ToString();
            string PartyNM_S = Session["PartyNM_S"].ToString();
            string PartyID_S = Session["PartyID_S"].ToString();

            DateTime InDT = DateTime.Parse(InVDT, dateformat, System.Globalization.DateTimeStyles.AssumeLocal);
            string inDate = InDT.ToString("yyyy/MM/dd");

            conn.Open();
            SqlCommand cmd = new SqlCommand("SELECT  DISTINCT  GL_ACCHART.ACCOUNTNM, STK_STORE.STORENM, STK_TRANS.TRANSTP, CONVERT(nvarchar(20),STK_TRANS.TRANSDT,103) AS TRANSDT, " +
                      " STK_TRANS.TRANSMY, STK_TRANS.TRANSNO, STK_TRANS.INVREFNO,  STK_TRANS.STOREFR, ASL_COMPANY.COMPNM, ASL_COMPANY.ADDRESS, ASL_COMPANY.CONTACTNO " +
                      " FROM STK_TRANS INNER JOIN STK_ITEM ON STK_TRANS.CATID = STK_ITEM.CATID AND STK_TRANS.ITEMID = STK_ITEM.ITEMID INNER JOIN " +
                      " STK_ITEMMST ON STK_ITEM.CATID = STK_ITEMMST.CATID INNER JOIN GL_ACCHART ON STK_TRANS.PSID = GL_ACCHART.ACCOUNTCD INNER JOIN " +
                      " STK_STORE ON STK_TRANS.STOREFR COLLATE Latin1_General_CI_AS = STK_STORE.STOREID CROSS JOIN ASL_COMPANY " +
                      " WHERE     (STK_TRANS.TRANSTP = 'SALE') AND (STK_TRANS.TRANSDT = '" + inDate + "') AND (STK_TRANS.TRANSNO = '" + InVNo + "') AND (STK_TRANS.PSID = '" + PartyID_S + "')", conn);
            SqlDataAdapter da = new SqlDataAdapter(cmd);
            DataSet ds = new DataSet();
            da.Fill(ds);
            conn.Close();
            if (ds.Tables[0].Rows.Count > 0)
            {
                Repeater1.DataSource = ds;
                Repeater1.DataBind();
                Repeater1.Visible = true;

            }
            else
            {
                Repeater1.DataSource = ds;
                Repeater1.DataBind();
                Repeater1.Visible = true;
            }
        }

        protected void Repeater1_ItemDataBound(object sender, RepeaterItemEventArgs e)
        {
            string connectionString = ConfigurationManager.ConnectionStrings["AslDbContext"].ConnectionString;
            SqlConnection conn = new SqlConnection(connectionString);

            string InVDT = Session["InvDate_S"].ToString();
            string InVNo = Session["InvNo_S"].ToString();
            string Memo_S = Session["Memo_S"].ToString();
            //string StoreNM_S = Session["StoreNM_S"].ToString();
            //string StoreID_S = Session["StoreID_S"].ToString();
            string PartyNM_S = Session["PartyNM_S"].ToString();
            string PartyID_S = Session["PartyID_S"].ToString();

            DateTime InDT = DateTime.Parse(InVDT, dateformat, System.Globalization.DateTimeStyles.AssumeLocal);
            string inDate = InDT.ToString("yyyy/MM/dd");

            //Label lblStoreFr = (Label)e.Item.FindControl("lblStoreFr");

            Label lblStoreID = (Label)e.Item.FindControl("lblStoreID");
            string stid = lblStoreID.Text;


            //string STOREFR = daIN["STOREFR"].ToString();
            GridView GridView1 = (GridView)e.Item.FindControl("GridView1");
            DataTable dt = loadData(InVNo, PartyID_S, stid, inDate);
            //ViewState["gridTable"] = dt;
            GridView1.DataSource = dt;
            GridView1.DataBind();

            Label lblInWords = (Label)e.Item.FindControl("lblInWords");

            lblInWords.Text = "";
            decimal dec;
            decimal parseAmount = decimal.Parse(tot_carQtyComma);
            string lblAmount = parseAmount.ToString();
            Boolean ValidInput = Decimal.TryParse(tot_carQtyComma, out dec);
            if (!ValidInput)
            {
                lblInWords.ForeColor = System.Drawing.Color.Red;
                lblInWords.Text = "Enter the Proper Amount...";
                return;
            }
            if (tot_carQtyComma.ToString().Trim() == "")
            {
                lblInWords.ForeColor = System.Drawing.Color.Red;
                lblInWords.Text = "";
                return;
            }
            else
            {
                if (Convert.ToDecimal(tot_carQtyComma) == 0)
                {
                    lblInWords.ForeColor = System.Drawing.Color.Red;
                    lblInWords.Text = "";
                    return;
                }
            }

            string x1 = "";
            string x2 = "";

            if (lblAmount.Contains("."))
            {
                x1 = lblAmount.Trim().Substring(0, lblAmount.Trim().IndexOf("."));
                x2 = lblAmount.Trim().Substring(lblAmount.Trim().IndexOf(".") + 1);
            }
            else
            {
                x1 = lblAmount.Trim();
                x2 = "00";
            }

            if (x1.ToString().Trim() != "")
            {
                x1 = Convert.ToInt64(x1.Trim()).ToString().Trim();
            }
            else
            {
                x1 = "0";
            }

            lblAmount = x1 + "." + x2;

            if (x2.Length > 2)
            {
                lblAmount = Math.Round(Convert.ToDouble(lblAmount), 2).ToString().Trim();
            }

            string AmtConv = SpellAmount.QuantityConvFn(lblAmount.Trim());

            lblInWords.Text = AmtConv.Trim();
        }

        private DataTable loadData(string InNo, string PartyID, string StoreID, string inDt)
        {
            string connectionString = ConfigurationManager.ConnectionStrings["AslDbContext"].ConnectionString;
            SqlConnection conn = new SqlConnection(connectionString);

            //ds = (DataSet)ViewState["currentOrders"];
            DataTable dtGridTable = new DataTable();

            conn.Open();
            SqlCommand cmd = new SqlCommand("SELECT ROW_NUMBER()over(order by STK_TRANS.TRANSSL) as SL, STK_ITEMMST.CATNM, STK_ITEM.ITEMNM + ' || ' + STK_ITEM.ITEMCD AS ITEMNM, GL_ACCHART.ACCOUNTNM, STK_STORE.STORENM, STK_TRANS.TRANSTP, STK_TRANS.TRANSDT, " +
                      " STK_TRANS.TRANSMY, STK_TRANS.TRANSNO, STK_TRANS.INVREFNO, STK_TRANS.STOREFR, STK_TRANS.STORETO, STK_TRANS.PSID, STK_TRANS.LCTP, STK_TRANS.LCID, STK_TRANS.LCDATE, STK_TRANS.REMARKS, STK_TRANS.TRANSSL, STK_TRANS.CATID, STK_TRANS.ITEMID, STK_TRANS.UNITTP, " +
                      " STK_TRANS.CPQTY, STK_TRANS.CQTY, STK_TRANS.PQTY, STK_TRANS.QTY, STK_TRANS.RATE, STK_TRANS.AMOUNT, STK_TRANS.USERPC, STK_TRANS.USERID, STK_TRANS.ACTDTI, STK_TRANS.INTIME, STK_TRANS.IPADDRESS, ASL_COMPANY.COMPNM, ASL_COMPANY.ADDRESS, ASL_COMPANY.CONTACTNO " +
                      " FROM STK_TRANS INNER JOIN STK_ITEM ON STK_TRANS.CATID = STK_ITEM.CATID AND STK_TRANS.ITEMID = STK_ITEM.ITEMID INNER JOIN STK_ITEMMST ON STK_ITEM.CATID = STK_ITEMMST.CATID INNER JOIN " +
                      " GL_ACCHART ON STK_TRANS.PSID = GL_ACCHART.ACCOUNTCD INNER JOIN STK_STORE ON STK_TRANS.STOREFR COLLATE Latin1_General_CI_AS = STK_STORE.STOREID CROSS JOIN " +
                      " ASL_COMPANY " +
                      " WHERE     (STK_TRANS.TRANSTP = 'SALE') AND (STK_TRANS.TRANSDT = '" + inDt + "') AND (STK_TRANS.TRANSNO = '" + InNo + "') AND (STK_TRANS.PSID = '" + PartyID + "') AND (STK_TRANS.STOREFR = '" + StoreID + "') " +
                      " ORDER BY STK_TRANS.TRANSSL", conn);

            SqlDataAdapter adapter = new SqlDataAdapter(cmd);
            adapter.Fill(dtGridTable);
            conn.Close();
            return dtGridTable;
        }

        protected void GridView1_RowDataBound(object sender, GridViewRowEventArgs e)
        {
            if (e.Row.RowType == DataControlRowType.DataRow)
            {
                //string SL = DataBinder.Eval(e.Row.DataItem, "SL").ToString();
                //e.Row.Cells[0].Text = SL;

                //string STORENM = DataBinder.Eval(e.Row.DataItem, "STORENM").ToString();
                //e.Row.Cells[1].Text = "&nbsp;" + STORENM;

                //string ITEMNM = DataBinder.Eval(e.Row.DataItem, "ITEMNM").ToString();
                //e.Row.Cells[2].Text = "&nbsp;" + ITEMNM;

                decimal CQTY = Convert.ToDecimal(DataBinder.Eval(e.Row.DataItem, "CQTY").ToString());
                string cQty = SpellAmount.comma(CQTY);
                //e.Row.Cells[3].Text = cQty;

                decimal CPQTY = Convert.ToDecimal(DataBinder.Eval(e.Row.DataItem, "CPQTY").ToString());
                string cpQty = SpellAmount.comma(CPQTY);
                //e.Row.Cells[4].Text = cpQty;

                decimal QTY = Convert.ToDecimal(DataBinder.Eval(e.Row.DataItem, "QTY").ToString());
                string totQty = SpellAmount.comma(QTY);
                //e.Row.Cells[5].Text = totQty;

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

                tot_carQty = 0;
                tot_pQty = 0;
                tot_tQty = 0;
            }

            //ShowHeader(GridView1);
        }

        //private void ShowHeader(GridView grid)
        //{
        //    if (grid.Rows.Count > 0)
        //    {
        //        grid.UseAccessibleHeader = true;
        //        grid.HeaderRow.TableSection = TableRowSection.TableHeader;
        //        //gridView.HeaderRow.Style["display"] = "table-header-group";
        //    }
        //}

    }
}