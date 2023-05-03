using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using AlchemyAccounting;

namespace DynamicMenu.Accounts.Report.UI
{
    public partial class RptItemLedgerDepoDelear : System.Web.UI.Page
    {
        IFormatProvider dateformat = new System.Globalization.CultureInfo("fr-FR", true);
        protected void Page_Load(object sender, EventArgs e)
        {
            if (Session["USERID"] == null)
                Response.Redirect("~/Login/UI/Login.aspx");
            else
            {
                const string formLink = "/Accounts/Report/UI/RptItemLedgerDepoDelear.aspx";
                bool permission = UserPermissionChecker.checkParmit(formLink, "STATUS");
                if (permission == true)
                {
                    if (!IsPostBack)
                    {
                        lblAccHeadCD.Text = "";
                        lblAccHeadCD.Visible = false;
                        //refresh();
                        DateTime today = dbFunctions.Timezone(DateTime.Now);
                        string td = dbFunctions.Dayformat(today);
                        txtFrom.Text = td;
                        txtTo.Text = td;
                        txtHeadNM.Focus();
                    }
                }
                else
                {
                    Response.Redirect("../../../DeshBoard/UI/Default.aspx");
                }
            }

        }
        protected void txtHeadNM_TextChanged(object sender, EventArgs e)
        {
            if (txtHeadNM.Text != "")
            {
                dbFunctions.lblAdd(@"Select ACCOUNTCD from GL_ACCHART where STATUSCD='P'and ACCOUNTNM = '" + txtHeadNM.Text + "'", lblAccHeadCD);
            }
            else
                txtHeadNM.Text = "";
            txtHeadNM.Focus();
        }
        public void refresh()
        {
            txtHeadNM.Text = "";
            txtFrom.Text = "";
            txtTo.Text = "";
            lblAccHeadCD.Text = "";
        }

        protected void btnSearch_Click(object sender, EventArgs e)
        {
            if (txtHeadNM.Text == "" || txtFrom.Text == "" || txtTo.Text == "")
            {
                Response.Write("<script>alert('Fill Required Data');</script>");
            }
            else
            {
                Session["AccCode"] = lblAccHeadCD.Text;
                Session["AccNM"] = txtHeadNM.Text;
                Session["From"] = txtFrom.Text;
                Session["To"] = txtTo.Text;
                Page.ClientScript.RegisterStartupScript(
                    this.GetType(), "OpenWindow", "window.open('../Report/ReportLedgerBook.aspx','_newtab');", true);
                //Response.Redirect("~/Accounts/Report/Report/ReportLedgerBook.aspx");
            }
        }
    }
}