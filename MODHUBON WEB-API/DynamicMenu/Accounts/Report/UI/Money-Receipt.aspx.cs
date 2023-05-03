using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using AlchemyAccounting;

namespace DynamicMenu.Accounts.Report.UI
{
    public partial class Money_Receipt : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (Session["USERID"] == null)
                Response.Redirect("~/Login/UI/Login.aspx");
            else
            {
                const string formLink = "/Accounts/Report/UI/Money-Receipt.aspx";
                bool permission = UserPermissionChecker.checkParmit(formLink, "STATUS");
                if (permission == true)
                {
                    if (!IsPostBack)
                    {
                        txtacchead.Text = "";
                        //refresh();
                        string td = dbFunctions.Timezone(DateTime.Now).ToString("dd/MM/yyyy");
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
                dbFunctions.txtAdd(@"Select ACCOUNTCD from GL_ACCHART where STATUSCD='P'and ACCOUNTNM = '" + txtHeadNM.Text + "'", txtacchead);
            }
            else
                txtHeadNM.Text = "";
            txtHeadNM.Focus();
        }
        public void Refresh()
        {
            txtHeadNM.Text = "";
            txtFrom.Text = "";
            txtTo.Text = "";
            txtacchead.Text = "";
        }

        protected void btnSearch_Click(object sender, EventArgs e)
        {
            if (txtHeadNM.Text == "" || txtFrom.Text == "" || txtTo.Text == "")
            {
                Response.Write("<script>alert('Fill Required Data');</script>");
            }
            else
            {
                Session["AccCode"] = txtacchead.Text;
                Session["AccNM"] = txtHeadNM.Text;
                Session["From"] = txtFrom.Text;
                Session["To"] = txtTo.Text;
                Page.ClientScript.RegisterStartupScript(
                    this.GetType(), "OpenWindow", "window.open('../Report/rpt-Money-receipts.aspx','_newtab');", true);
            }
        }
    }
}