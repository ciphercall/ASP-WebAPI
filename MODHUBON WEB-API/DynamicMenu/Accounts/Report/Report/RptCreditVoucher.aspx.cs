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

namespace AlchemyAccounting.Accounts.Report.Report
{
    public partial class RptCreditVoucher : System.Web.UI.Page
    {
        IFormatProvider dateformat = new System.Globalization.CultureInfo("fr-FR", true);
        protected void Page_Load(object sender, EventArgs e)
        {
            try
            {
                dbFunctions.lblAdd(@"SELECT COMPNM FROM ASL_COMPANY WHERE COMPID='101' ", lblCompNM);
                dbFunctions.lblAdd(@"SELECT ADDRESS FROM ASL_COMPANY WHERE COMPID='101' ", lblAddress);
                
                lblPrintTime.Text = dbFunctions.Timezone(DateTime.Now).ToString("dd-MMM-yy hh:mm tt");
                string VouchNo1 = Session["VouchNo"].ToString();
                dbFunctions.lblAdd("SELECT DISTINCT USERID FROM GL_STRANS WHERE TRANSNO='"+ VouchNo1 + "'", lbluno);
                //string userID = HttpContext.Current.Session["USERID"].ToString();
                // lblUserName.Text = dbFunctions.StringData(@"SELECT USERNM FROM ASL_USERCO WHERE USERID='" + lbluno.Text + "'");
                // lblUserName.Text = lbluno.Text;



                string Mode = "";
                string TransType = Session["TransType"].ToString();


                if (TransType == "MREC")
                {
                    string TransDate = Session["TransDate"].ToString();
                    string VouchNo = Session["VouchNo"].ToString();
                    string TransMode = Session["TransMode"].ToString();
                    string DebitCD = Session["DebitCD"].ToString();
                    string CreditCD = Session["CreditCD"].ToString();
                    string ChequeNo = Session["ChequeNo"].ToString();
                    string ChequeDT = Session["ChequeDT"].ToString();
                    string Remarks = Session["Remarks"].ToString();
                    string Amount = Session["Amount"].ToString();
                    string Inword = Session["Inword"].ToString();
                    string SubDebitCd = DebitCD.Substring(0, 7);

                    if (SubDebitCd == "1020101")
                        Mode = "CREDIT VOUCHER - CASH";
                    else
                        Mode = "CREDIT VOUCHER - BANK";

                    
                    lblTime.Text = TransDate;
                    lblVtype.Text = Mode;
                    lblVNo.Text = VouchNo;
                    lblParticulars.Text = Remarks;
                    string AmountComma = SpellAmount.comma(Convert.ToDecimal(Amount));
                    lblAmount.Text = AmountComma;
                    string TotAmountComma = SpellAmount.comma(Convert.ToDecimal(Amount));
                    lblTotAmount.Text = TotAmountComma;
                    lblRMode.Text = TransMode;
                    lblChequeNo.Text = ChequeNo;
                    lblChequeDT.Text = ChequeDT;
                    lblInWords.Text = Inword;
                    dbFunctions.lblAdd(@"select ACCOUNTNM from GL_ACCHART where STATUSCD='P' and ACCOUNTCD='" + DebitCD + "'", lblReceivedBy);
                    dbFunctions.lblAdd(@"select ACCOUNTNM from GL_ACCHART where STATUSCD='P' and ACCOUNTCD='" + CreditCD + "'", lblReceivedFrom);
                    
                    lblReceiveCrBy.Text = "Received By";
                    lblReceiveCrFrom.Text = "Received From";
                    lblReceiveMode.Text = "Transaction";

                }
                else if (TransType == "MPAY")
                {
                    string TransDate = Session["TransDate"].ToString();
                    string VouchNo = Session["VouchNo"].ToString();
                    string TransMode = Session["TransMode"].ToString();
                    string DebitCD = Session["DebitCD"].ToString();
                    string CreditCD = Session["CreditCD"].ToString();
                    string ChequeNo = Session["ChequeNo"].ToString();
                    string ChequeDT = Session["ChequeDT"].ToString();
                    string Remarks = Session["Remarks"].ToString();
                    string Amount = Session["Amount"].ToString();
                    string Inword = Session["Inword"].ToString();
                    string Credited = Session["CreditCD"].ToString();
                    string SubCreditCd = Credited.Substring(0, 7);
                    if (SubCreditCd == "1020102")
                        Mode = "DEBIT VOUCHER - BANK";
                    else
                        Mode = "DEBIT VOUCHER - CASH";
                    lblTime.Text = TransDate;
                    lblVtype.Text = Mode;
                    lblVNo.Text = VouchNo;
                    lblParticulars.Text = Remarks;
                    string AmountComma = SpellAmount.comma(Convert.ToDecimal(Amount));
                    lblAmount.Text = AmountComma;
                    string TotAmountComma = SpellAmount.comma(Convert.ToDecimal(Amount));
                    lblTotAmount.Text = TotAmountComma;
                    lblRMode.Text = TransMode;
                    lblChequeNo.Text = ChequeNo;
                    lblChequeDT.Text = ChequeDT;
                    lblInWords.Text = Inword;
                    dbFunctions.lblAdd(@"select ACCOUNTNM from GL_ACCHART where STATUSCD='P' and ACCOUNTCD='" + DebitCD + "'", lblReceivedBy);
                    dbFunctions.lblAdd(@"select ACCOUNTNM from GL_ACCHART where STATUSCD='P' and ACCOUNTCD='" + CreditCD + "'", lblReceivedFrom);

                    DateTime TransDT = DateTime.Parse(TransDate, dateformat, System.Globalization.DateTimeStyles.AssumeLocal);
                    lblReceiveCrBy.Text = "Paid To";
                    lblReceiveCrFrom.Text = "Paid From";
                    lblReceiveMode.Text = "Transaction";
                }
                else if (TransType == "JOUR")
                {

                    Mode = "JOURNAL VOUCHER";
                    string TransDate = Session["TransDate"].ToString();
                    string VouchNo = Session["VouchNo"].ToString();
                    string TransMode = Session["TransMode"].ToString();
                    string DebitCD = Session["DebitCD"].ToString();
                    string CreditCD = Session["CreditCD"].ToString();
                    string ChequeNo = Session["ChequeNo"].ToString();
                    string ChequeDT = Session["ChequeDT"].ToString();
                    string Remarks = Session["Remarks"].ToString();
                    string Amount = Session["Amount"].ToString();
                    string Inword = Session["Inword"].ToString();
                    lblTime.Text = TransDate;
                    lblVtype.Text = Mode;
                    lblVNo.Text = VouchNo;
                    lblParticulars.Text = Remarks;
                    string AmountComma = SpellAmount.comma(Convert.ToDecimal(Amount));
                    lblAmount.Text = AmountComma;
                    string TotAmountComma = SpellAmount.comma(Convert.ToDecimal(Amount));
                    lblTotAmount.Text = TotAmountComma;
                    lblRMode.Text = TransMode;
                    lblChequeNo.Text = ChequeNo;
                    lblChequeDT.Text = ChequeDT;
                    lblInWords.Text = Inword;
                    dbFunctions.lblAdd(@"select ACCOUNTNM from GL_ACCHART where STATUSCD='P' and ACCOUNTCD='" + DebitCD + "'", lblReceivedBy);
                    dbFunctions.lblAdd(@"select ACCOUNTNM from GL_ACCHART where STATUSCD='P' and ACCOUNTCD='" + CreditCD + "'", lblReceivedFrom);

                    DateTime TransDT = DateTime.Parse(TransDate, dateformat, System.Globalization.DateTimeStyles.AssumeLocal);
                    lblReceiveCrBy.Text = "Receive To";
                    lblReceiveCrFrom.Text = "Receive From";
                    lblReceiveMode.Text = "Transaction";
                }
                else if (TransType == "CONT")
                {
                    Mode = "CONTRA VOUCHER";
                    string TransDate = Session["TransDate"].ToString();
                    string VouchNo = Session["VouchNo"].ToString();
                    string TransMode = Session["TransMode"].ToString();
                    string DebitCD = Session["DebitCD"].ToString();
                    string CreditCD = Session["CreditCD"].ToString();
                    string ChequeNo = Session["ChequeNo"].ToString();
                    string ChequeDT = Session["ChequeDT"].ToString();
                    string Remarks = Session["Remarks"].ToString();
                    string Amount = Session["Amount"].ToString();
                    string Inword = Session["Inword"].ToString();
                    lblTime.Text = TransDate;
                    lblVtype.Text = Mode;
                    lblVNo.Text = VouchNo;
                    lblParticulars.Text = Remarks;
                    string AmountComma = SpellAmount.comma(Convert.ToDecimal(Amount));
                    lblAmount.Text = AmountComma;
                    string TotAmountComma = SpellAmount.comma(Convert.ToDecimal(Amount));
                    lblTotAmount.Text = TotAmountComma;
                    lblRMode.Text = TransMode;
                    lblChequeNo.Text = ChequeNo;
                    lblChequeDT.Text = ChequeDT;
                    lblInWords.Text = Inword;
                    dbFunctions.lblAdd(@"select ACCOUNTNM from GL_ACCHART where STATUSCD='P' and ACCOUNTCD='" + DebitCD + "'", lblReceivedBy);
                    dbFunctions.lblAdd(@"select ACCOUNTNM from GL_ACCHART where STATUSCD='P' and ACCOUNTCD='" + CreditCD + "'", lblReceivedFrom);

                    DateTime TransDT = DateTime.Parse(TransDate, dateformat, System.Globalization.DateTimeStyles.AssumeLocal);
                    lblReceiveCrBy.Text = "Deposited To";
                    lblReceiveCrFrom.Text = "Withdrawn From";
                    lblReceiveMode.Text = "Transaction";
                }
                else
                {
                    Response.Write("<script>alert('Please Select Transaction Type');</script>");
                    Response.Redirect("~/Accounts/UI/SingleTransaction.aspx");
                }

                string date = Session["TransDate"].ToString();
                string transno = Session["VouchNo"].ToString();
                string dcd = Session["DebitCD"].ToString();
                string ccd = Session["CreditCD"].ToString();
                DateTime dT = DateTime.Parse(date, dateformat, System.Globalization.DateTimeStyles.AssumeLocal);
                string TDT = dT.ToString("yyyy/MM/dd");
                dbFunctions.lblAdd(@"SELECT GL_COSTP.COSTPNM FROM GL_STRANS INNER JOIN GL_COSTP ON GL_STRANS.COSTPID = GL_COSTP.COSTPID
WHERE (GL_STRANS.TRANSDT = '" + TDT + "') AND (GL_STRANS.TRANSNO = " + transno + ") AND (GL_STRANS.DEBITCD = '" + dcd + "') AND (GL_STRANS.CREDITCD = '" + ccd + "')", lblTransFor);
                
                dbFunctions.lblAdd(@"select INTIME from GL_STRANS where TRANSDT ='" + TDT + "' and TRANSNO=" + transno + " and DEBITCD='" + dcd + "' and CREDITCD='" + ccd + "'", lblInTime);
                dbFunctions.lblAdd(@"select USERNM FROM GL_STRANS INNER JOIN ASL_USERCO ON GL_STRANS.USERID = ASL_USERCO.USERID where TRANSDT ='" + TDT + "' and TRANSNO=" + transno + " and DEBITCD='" + dcd + "' and CREDITCD='" + ccd + "'", lblEntryUserName);
                lblUserName.Text = lblEntryUserName.Text;

                DateTime intime = DateTime.Parse(lblInTime.Text);
                lblInTime.Text = intime.ToString("dd-MMM-yy hh:mm tt");


                if (lblTransFor.Text == "")
                {
                    lblTransForName.Visible = false;
                    lblTransforSC.Visible = false;
                }
            }
            catch (Exception ex)
            {
                Response.Write(ex.Message);
            }
        }
    }
}