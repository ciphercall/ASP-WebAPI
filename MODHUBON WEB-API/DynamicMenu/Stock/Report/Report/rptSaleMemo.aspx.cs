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
    public partial class rptSaleMemo : System.Web.UI.Page
    {
        IFormatProvider dateformat = new System.Globalization.CultureInfo("fr-FR", true);

        decimal tot_Amount = 0;
        decimal tot_Dis = 0;
        decimal tot_NetAmt = 0;

        decimal tot_CQTY = 0;
        decimal tot_Qty = 0;

        string tot_CQTYComma = "";
        string tot_QtyComma = "";

        string tot_Amount_comma = "";
        string tot_Dis_comma = "";
        string tot_NetAmt_comma = "";

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

                string InVDT = Session["InvDate_S"].ToString();
                string InVNo = Session["InvNo_S"].ToString();
                string Memo_S = Session["Memo_S"].ToString();
                //string StoreNM_S = Session["StoreNM_S"].ToString();
                //string StoreID_S = Session["StoreID_S"].ToString();
                string PartyNM_S = Session["PartyNM_S"].ToString();
                string PartyID_S = Session["PartyID_S"].ToString();
                string paid_amt = Session["paid_amt"].ToString();
                string due_amt = Session["due_amt"].ToString();

                DateTime InDT = DateTime.Parse(InVDT, dateformat, System.Globalization.DateTimeStyles.AssumeLocal);
                lblInVDT.Text = InDT.ToString("dd-MMM-yyyy");
                lblInVNo.Text = InVNo;
                lblSalesMemoNo.Text = Memo_S;
                lblSalesTo.Text = PartyNM_S;
                //lblSalesFrom.Text = StoreNM_S;
                dbFunctions.lblAdd(@"SELECT ADDRESS FROM STK_PS WHERE PSID='" + PartyID_S + "' ", lblSaleToAdd);
                showGrid();

                string inDate = InDT.ToString("yyyy/MM/dd");
                dbFunctions.lblAdd(@"SELECT DISAMT FROM  STK_TRANSMST WHERE TRANSTP='SALE' AND TRANSNO = '" + InVNo + "' AND TRANSDT ='" + inDate + "' AND PSID = '" + PartyID_S + "'", lblGrossDiscount);
                decimal grDis = Convert.ToDecimal(lblGrossDiscount.Text);
                string grossDisAmt = SpellAmount.comma(grDis);
                lblGrossDiscount.Text = grossDisAmt;

                dbFunctions.lblAdd(@"SELECT LTCOST FROM  STK_TRANSMST WHERE TRANSTP='SALE' AND TRANSNO = '" + InVNo + "' AND TRANSDT ='" + inDate + "' AND PSID = '" + PartyID_S + "'", lblLtCost);
                decimal ltCost = Convert.ToDecimal(lblLtCost.Text);
                string ltCostAmt = SpellAmount.comma(ltCost);
                lblLtCost.Text = ltCostAmt;

                dbFunctions.lblAdd(@"SELECT TOTNET FROM  STK_TRANSMST WHERE TRANSTP='SALE' AND TRANSNO = '" + InVNo + "' AND TRANSDT ='" + inDate + "' AND PSID = '" + PartyID_S + "'", lblNetAmount);
                decimal nAmt = Convert.ToDecimal(lblNetAmount.Text);
                string netAmount = SpellAmount.comma(nAmt);
                lblNetAmount.Text = netAmount;

                decimal pamt, damt = 0;
                if (paid_amt == "" || paid_amt == "0.00")
                    lblPaidAmount.Text = "0.00";
                else
                {
                    pamt = Convert.ToDecimal(paid_amt);
                    lblPaidAmount.Text = SpellAmount.comma(pamt);
                }

                if (due_amt == "" || due_amt == "0.00")
                    lblDueAmount.Text = "0.00";
                else
                {
                    damt = Convert.ToDecimal(due_amt);
                    lblDueAmount.Text = SpellAmount.comma(damt);
                }

                lblInWords.Text = "";
                decimal dec;
                decimal parseAmount = decimal.Parse(lblNetAmount.Text);
                string lblAmount = parseAmount.ToString();
                Boolean ValidInput = Decimal.TryParse(lblNetAmount.Text, out dec);
                if (!ValidInput)
                {
                    lblInWords.ForeColor = System.Drawing.Color.Red;
                    lblInWords.Text = "Enter the Proper Amount...";
                    return;
                }
                if (lblNetAmount.Text.ToString().Trim() == "")
                {
                    lblInWords.ForeColor = System.Drawing.Color.Red;
                    lblInWords.Text = "Amount Cannot Be Empty...";
                    return;
                }
                else
                {
                    if (Convert.ToDecimal(lblNetAmount.Text) == 0)
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
            catch(Exception ex)
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
            SqlCommand cmd = new SqlCommand("SELECT ROW_NUMBER()over(order by STK_TRANS.TRANSSL) as SL, dbo.STK_ITEMMST.CATNM, dbo.STK_ITEM.ITEMNM + ' || ' + STK_ITEM.ITEMCD AS ITEMNM, dbo.GL_ACCHART.ACCOUNTNM, dbo.STK_STORE.STORENM, dbo.STK_TRANS.TRANSTP, " + 
                                " dbo.STK_TRANS.TRANSDT, dbo.STK_TRANS.TRANSMY, dbo.STK_TRANS.TRANSNO, dbo.STK_TRANS.INVREFNO, dbo.STK_TRANS.STOREFR, " +
                                " dbo.STK_TRANS.STORETO, dbo.STK_TRANS.PSID, dbo.STK_TRANS.LCTP, dbo.STK_TRANS.LCID, dbo.STK_TRANS.LCDATE, dbo.STK_TRANS.REMARKS, " +
                                " dbo.STK_TRANS.TRANSSL, dbo.STK_TRANS.CATID, dbo.STK_TRANS.ITEMID, dbo.STK_TRANS.UNITTP, dbo.STK_TRANS.CPQTY, dbo.STK_TRANS.CQTY, " +
                                " dbo.STK_TRANS.PQTY, dbo.STK_TRANS.QTY, dbo.STK_TRANS.RATE, dbo.STK_TRANS.AMOUNT, STK_TRANS.DISCRT, STK_TRANS.DISCAMT, STK_TRANS.NETAMT, dbo.STK_TRANS.USERPC, dbo.STK_TRANS.USERID,  " +
                                " dbo.STK_TRANS.ACTDTI, dbo.STK_TRANS.INTIME, dbo.STK_TRANS.IPADDRESS FROM dbo.STK_TRANS INNER JOIN " +
                                " dbo.STK_ITEM ON dbo.STK_TRANS.CATID = dbo.STK_ITEM.CATID AND dbo.STK_TRANS.ITEMID = dbo.STK_ITEM.ITEMID INNER JOIN " +
                                " dbo.STK_ITEMMST ON dbo.STK_ITEM.CATID = dbo.STK_ITEMMST.CATID INNER JOIN " +
                                " dbo.GL_ACCHART ON dbo.STK_TRANS.PSID = dbo.GL_ACCHART.ACCOUNTCD INNER JOIN " +
                                " dbo.STK_STORE ON dbo.STK_TRANS.STOREFR COLLATE Latin1_General_CI_AS = dbo.STK_STORE.STOREID " +
                                " WHERE (dbo.STK_TRANS.TRANSTP = 'SALE') AND (dbo.STK_TRANS.TRANSDT = '" + inDate + "') AND (dbo.STK_TRANS.TRANSNO = '" + InVNo + "') AND " +
                                " (dbo.STK_TRANS.PSID = '" + PartyID_S + "') " +
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

            }
        }

        protected void GridView1_RowDataBound(object sender, GridViewRowEventArgs e)
        {
            if (e.Row.RowType == DataControlRowType.DataRow)
            {
                decimal cqty = Convert.ToDecimal(DataBinder.Eval(e.Row.DataItem, "CQTY").ToString());
                string CQTY = SpellAmount.comma(cqty);

                decimal qty = Convert.ToDecimal(DataBinder.Eval(e.Row.DataItem, "QTY").ToString());
                string QTY = SpellAmount.comma(qty);

                decimal rt = Convert.ToDecimal(DataBinder.Eval(e.Row.DataItem, "RATE").ToString());
                string RATE = SpellAmount.comma(rt);
                e.Row.Cells[5].Text = RATE;

                decimal amNT = Convert.ToDecimal(DataBinder.Eval(e.Row.DataItem, "AMOUNT").ToString());
                string AMOUNT = SpellAmount.comma(amNT);
                e.Row.Cells[6].Text = AMOUNT;

                decimal disAmt = Convert.ToDecimal(DataBinder.Eval(e.Row.DataItem, "DISCAMT").ToString());
                string DISCAMT = SpellAmount.comma(disAmt);
                e.Row.Cells[7].Text = DISCAMT;

                decimal netAmt = Convert.ToDecimal(DataBinder.Eval(e.Row.DataItem, "NETAMT").ToString());
                string NETAMT = SpellAmount.comma(netAmt);
                e.Row.Cells[8].Text = NETAMT;

                tot_CQTY += cqty;
                tot_CQTYComma = SpellAmount.comma(tot_CQTY);

                tot_Qty += qty;
                tot_QtyComma = SpellAmount.comma(tot_Qty);

                tot_Amount += amNT;
                tot_Amount_comma = SpellAmount.comma(tot_Amount);

                tot_Dis += disAmt;
                tot_Dis_comma = SpellAmount.comma(tot_Dis);

                tot_NetAmt += netAmt;
                tot_NetAmt_comma = SpellAmount.comma(tot_NetAmt);
            }

            else if (e.Row.RowType == DataControlRowType.Footer)
            {
                e.Row.Cells[2].Text = "TOTAL :";
                e.Row.Cells[2].HorizontalAlign = HorizontalAlign.Right;
                e.Row.Cells[3].Text = tot_CQTYComma;
                e.Row.Cells[3].CssClass = "footerCqty";
                e.Row.Cells[4].Text = tot_QtyComma;
                e.Row.Cells[4].HorizontalAlign = HorizontalAlign.Right;
                e.Row.Cells[6].Text = tot_Amount_comma;
                e.Row.Cells[6].HorizontalAlign = HorizontalAlign.Right;
                e.Row.Cells[7].Text = tot_Dis_comma;
                e.Row.Cells[7].HorizontalAlign = HorizontalAlign.Right;
                e.Row.Cells[8].Text = tot_NetAmt_comma;
                e.Row.Cells[8].HorizontalAlign = HorizontalAlign.Right;

                e.Row.Font.Bold = true;

                e.Row.Font.Bold = true;
            }

            //ShowHeader(GridView1);
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