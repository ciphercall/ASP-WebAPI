using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using AlchemyAccounting;

namespace DynamicMenu.Accounts.Report.UI
{
    public partial class BalanceSheetDrillDownReport : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (Session["USERID"] == null)
                Response.Redirect("~/Login/UI/Login.aspx");
            else
            {
                const string formLink = "/Accounts/Report/UI/DrillDownReport.aspx";
                bool permission = UserPermissionChecker.checkParmit(formLink, "STATUS");
                if (permission == true)
                {
                    if (!IsPostBack)
                    {
                        txtacccd.Text = "";
                        DateTime today = dbFunctions.Timezone(DateTime.Now);
                        string td = dbFunctions.Dayformat(today);
                        txtFrom.Text = td;
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
                dbFunctions.txtAdd(@"Select ACCOUNTCD from GL_ACCHART where STATUSCD='P'and ACCOUNTNM = '" + txtHeadNM.Text + "'", txtacccd);
            }
            else
                txtHeadNM.Text = "";
            txtHeadNM.Focus();
        }
        public void Refresh()
        {
            txtHeadNM.Text = "";
            txtFrom.Text = "";
            txtacccd.Text = "";
        }

        protected void btnSearch_Click(object sender, EventArgs e)
        {
            if (txtHeadNM.Text == "" || txtFrom.Text == "")
            {
                Response.Write("<script>alert('Fill Required Data');</script>");
            }
            else
            {
                Session["AccCode"] = txtacccd.Text;
                Session["AccNM"] = txtHeadNM.Text;
                Session["From"] = txtFrom.Text;
                Page.ClientScript.RegisterStartupScript(
                    this.GetType(), "OpenWindow", "window.open('../Report/ReportDrillDownAccount.aspx','_newtab');", true);
            }
        }
    }
}