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
using DynamicMenu;

namespace AlchemyAccounting.Stock.Report.Report
{
    public partial class rptTransferMemo : System.Web.UI.Page
    {
        IFormatProvider dateformat = new System.Globalization.CultureInfo("fr-FR", true);

        protected void Page_Load(object sender, EventArgs e)
        {
            try
            {
                string uTp = HttpContext.Current.Session["USERTYPE"].ToString();
                string brCD = HttpContext.Current.Session["BrCD"].ToString();

                if (uTp == "COMPADMIN")
                {
                    dbFunctions.lblAdd(@"SELECT COMPNM FROM ASL_COMPANY WHERE COMPID ='101'", lblCompNM);
                    dbFunctions.lblAdd(@"SELECT ADDRESS FROM ASL_COMPANY WHERE COMPID ='101'", lblAddress);
                    dbFunctions.lblAdd(@"SELECT CONTACTNO FROM ASL_COMPANY WHERE COMPID ='101'", lblContact);
                }
                else
                {
                    dbFunctions.lblAdd(@"SELECT COMPNM FROM ASL_COMPANY WHERE COMPID ='" + brCD + "'", lblCompNM);
                    dbFunctions.lblAdd(@"SELECT ADDRESS FROM ASL_COMPANY WHERE COMPID ='" + brCD + "'", lblAddress);
                    dbFunctions.lblAdd(@"SELECT CONTACTNO FROM ASL_COMPANY WHERE COMPID ='" + brCD + "'", lblContact);
                }

                DateTime PrintDate = dbFunctions.Timezone(DateTime.Now);
                string td = PrintDate.ToString("dd-MMM-yyyy hh:mm tt");
                lblTime.Text = td;

                string InvDate_T = Session["InvDate_T"].ToString();
                string InvNo_T = Session["InvNo_T"].ToString();
                string Memo_T = Session["Memo_T"].ToString();
                string StoreNM_T = Session["StoreNM_T"].ToString();
                string StoreID_T = Session["StoreID_T"].ToString();
                string PartyNM_T = Session["PartyNM_T"].ToString();
                string PartyID_T = Session["PartyID_T"].ToString();

                DateTime InDT = DateTime.Parse(InvDate_T, dateformat, System.Globalization.DateTimeStyles.AssumeLocal);
                lblInVDT.Text = InDT.ToString("dd-MMM-yyyy");
                lblInVNo.Text = InvNo_T;
                lblSalesMemoNo.Text = Memo_T;
                lblTransferFrom.Text = StoreNM_T;
                lblTransferTo.Text = PartyNM_T;
                showGrid();

                lblInWords.Text = "";
                decimal dec;
                decimal parseAmount = decimal.Parse(lblTotAmount.Text);
                string lblAmount = parseAmount.ToString();
                Boolean ValidInput = Decimal.TryParse(lblTotAmount.Text, out dec);
                if (!ValidInput)
                {
                    lblInWords.ForeColor = System.Drawing.Color.Red;
                    lblInWords.Text = "Enter the Proper Amount...";
                    return;
                }
                if (lblTotAmount.Text.ToString().Trim() == "")
                {
                    lblInWords.ForeColor = System.Drawing.Color.Red;
                    lblInWords.Text = "Amount Cannot Be Empty...";
                    return;
                }
                else
                {
                    if (Convert.ToDecimal(lblTotAmount.Text) == 0)
                    {
                        lblInWords.ForeColor = System.Drawing.Color.Red;
                        lblInWords.Text = "Amount Cannot Be Empty...";
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

                string AmtConv = SpellAmount.MoneyConvFn(lblAmount.Trim());

                lblInWords.Text = AmtConv.Trim();

            }
            catch (Exception ex)
            {
                Response.Write(ex.Message);
            }
        }

        public void showGrid()
        {
            SqlConnection conn = new SqlConnection(dbFunctions.connection);
            string InvDate_T = Session["InvDate_T"].ToString();
            string InvNo_T = Session["InvNo_T"].ToString();
            string Memo_T = Session["Memo_T"].ToString();
            string StoreNM_T = Session["StoreNM_T"].ToString();
            string StoreID_T = Session["StoreID_T"].ToString();
            string PartyNM_T = Session["PartyNM_T"].ToString();
            string PartyID_T = Session["PartyID_T"].ToString();

            DateTime InDT = DateTime.Parse(InvDate_T, dateformat, System.Globalization.DateTimeStyles.AssumeLocal);
            string inDate = InDT.ToString("yyyy/MM/dd");

            conn.Open();
            SqlCommand cmd = new SqlCommand(" SELECT ROW_NUMBER() over (order by STK_TRANS.TRANSSL) as SL, STK_ITEMMST.CATNM, STK_ITEM.ITEMNM + ' || ' + STK_ITEM.ITEMCD AS ITEMNM, STK_STORE.STORENM, STK_TRANS.TRANSTP, STK_TRANS.TRANSDT, STK_TRANS.TRANSMY, STK_TRANS.TRANSNO, " +
                             " STK_TRANS.INVREFNO, STK_TRANS.STOREFR, STK_TRANS.STORETO, STK_TRANS.PSID, STK_TRANS.LCTP, STK_TRANS.LCID, STK_TRANS.LCDATE, " +  
                             " STK_TRANS.REMARKS, STK_TRANS.TRANSSL, STK_TRANS.CATID, STK_TRANS.ITEMID, STK_TRANS.UNITTP, STK_TRANS.CPQTY, STK_TRANS.CQTY, " + 
                             " STK_TRANS.PQTY, STK_TRANS.QTY, STK_TRANS.RATE, STK_TRANS.AMOUNT, STK_TRANS.USERPC, STK_TRANS.USERID," +
                             " STK_TRANS.INTIME, STK_TRANS.IPADDRESS, STK_STORE_1.STORENM AS STOREFR " +
                             " FROM STK_TRANS INNER JOIN " +
                             " STK_ITEM ON STK_TRANS.CATID = STK_ITEM.CATID AND STK_TRANS.ITEMID = STK_ITEM.ITEMID INNER JOIN " +
                             " STK_ITEMMST ON STK_ITEM.CATID = STK_ITEMMST.CATID INNER JOIN " +
                             " STK_STORE ON STK_TRANS.STORETO COLLATE Latin1_General_CI_AS = STK_STORE.STOREID INNER JOIN " +
                             " STK_STORE AS STK_STORE_1 ON STK_TRANS.STOREFR COLLATE Latin1_General_CI_AS = STK_STORE_1.STOREID " +
                             " WHERE (STK_TRANS.TRANSTP = 'ITRF') AND (STK_TRANS.TRANSDT = '" + inDate + "') AND (STK_TRANS.TRANSNO = '" + InvNo_T + "') " +
                             " AND (STK_TRANS.STOREFR = '" + StoreID_T + "') AND (STK_TRANS.STORETO = '" + PartyID_T + "') " +
                             " ORDER BY STK_TRANS.TRANSSL", conn);
            SqlDataAdapter da = new SqlDataAdapter(cmd);
            DataSet ds = new DataSet();
            da.Fill(ds);
            conn.Close();
            if (ds.Tables[0].Rows.Count > 0)
            {
                GridView1.DataSource = ds;
                GridView1.DataBind();
                GridView1.Visible = true;

                Decimal totQty = 0;
                Decimal a = 0;
                foreach (GridViewRow grid in GridView1.Rows)
                {
                    string Debit = grid.Cells[3].Text;
                    totQty = Convert.ToDecimal(Debit);
                    a += totQty;
                    decimal tot = a;
                    lblTotQTy.Text = tot.ToString();
                }

                Decimal totCqty = 0;
                Decimal c = 0;
                foreach (GridViewRow grid in GridView1.Rows)
                {
                    Label lblCarton = (Label)grid.FindControl("lblCarton");
                    totCqty = decimal.Parse(lblCarton.Text);
                    c += totCqty;
                    decimal tot = c;
                    lblTotCQTy.Text = SpellAmount.comma(tot);
                }

                Decimal totAmount = 0;
                Decimal b = 0;
                foreach (GridViewRow grid in GridView1.Rows)
                {
                    string Amnt = grid.Cells[5].Text;
                    totAmount = decimal.Parse(Amnt);
                    b += totAmount;
                    decimal tot = b;
                    lblTotAmount.Text = SpellAmount.comma(tot);
                }
            }
            else
            {

            }
        }

        protected void GridView1_RowDataBound(object sender, GridViewRowEventArgs e)
        {
            if (e.Row.RowType == DataControlRowType.DataRow)
            {
                decimal amNT = Convert.ToDecimal(DataBinder.Eval(e.Row.DataItem, "AMOUNT").ToString());
                string AMOUNT = SpellAmount.comma(amNT);
                e.Row.Cells[5].Text = AMOUNT;
            }
        }
    }
}