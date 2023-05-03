using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using AlchemyAccounting;

namespace DynamicMenu.Accounts.Report.Report
{
    public partial class rptChequeIssueInformationVoucher : System.Web.UI.Page
    {
        IFormatProvider dateformat = new System.Globalization.CultureInfo("fr-FR", true);
        protected void Page_Load(object sender, EventArgs e)
        {

            lblAmount.Text = SpellAmount.comma(Convert.ToDecimal(Session["lblAmount"]));
            string AmtConv = SpellAmount.MoneyConvFn (lblAmount.Text);
            lblInWords.Text = AmtConv;
            //   lblamntspell.Text=
            DateTime today = dbFunctions.Timezone(DateTime.Now);
            lblPrintTime.Text = today.ToString("dd/MM/yyyy");

            lblInTime.Text=Session["date"].ToString();
            string dt = Session["date"].ToString();
            lblissueto.Text = Session["lblCreditNM"].ToString();
           
            lblTime.Text = Session["date"].ToString();
            lblChequeNo.Text=Session["lblChequeNo"].ToString();
            lblChequeDT.Text=Session["lblChequeDt"].ToString();
            lblRMode.Text=Session["lblTransMode"].ToString();
            lblTransFor.Text=Session["lblChqBankBr"].ToString();

          //  lblParticulars.Text = Session["banknm"].ToString();
            lblParticulars.Text = Session["lblRemarks"].ToString();
           // lblAmount.Text=Session["lblAmount"].ToString();
           
           // lblTotAmount.Text= Session["lblAmount"].ToString();
         //   lblRemarks.Text=
            lblissuefrm.Text = Session["banknm"].ToString();
            lblVNo.Text=Session["txtTransNo"].ToString();
            string vno=Session["txtTransNo"].ToString();

            DateTime FDate = DateTime.Parse(dt, dateformat, System.Globalization.DateTimeStyles.AssumeLocal);
            string TrDate = FDate.ToString("yyyy-MM-dd");
            dbFunctions.lblAdd(@"select USERNM FROM GL_CHEQUE INNER JOIN ASL_USERCO ON GL_CHEQUE.USERID = ASL_USERCO.USERID where TRANSDT ='" + TrDate + "' and TRANSNO=" + vno + " ", lblunm);
            lblUserName.Text = lblunm.Text;
        }
    }
}