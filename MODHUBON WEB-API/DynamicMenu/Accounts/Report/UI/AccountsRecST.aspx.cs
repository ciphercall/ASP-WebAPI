using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using AlchemyAccounting;

namespace DynamicMenu.Accounts.Report.UI
{
    public partial class AccountsRecST : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {

            if (Session["USERID"] == null)
            {
                Response.Redirect("~/Login/UI/Login.aspx");
            }
            else
            {
                string formLink = "/Stock/Report/UI/ClosingStock.aspx";
                bool permission = UserPermissionChecker.checkParmit(formLink, "STATUS");
                if (permission == true)
                {
                    if (!Page.IsPostBack)
                    {
                        string td = dbFunctions.Timezone(DateTime.Now).ToString("dd/MM/yyyy");
                        txtDate.Text = td.ToString();

                    }
                }
                else
                {
                    Response.Redirect("../../DeshBoard/UI/Default.aspx");
                }
            }
        }

        protected void btnSearch_Click(object sender, EventArgs e)
        {
            if (txtDate.Text == "")
            {
                Response.Write("<script>alert('Select Date.');</script>");
            }
            else
            {

                Session["Date"] = txtDate.Text;
                Page.ClientScript.RegisterStartupScript(
                    this.GetType(), "OpenWindow", "window.open('../Report/rptAccountsRecST.aspx','_newtab');", true);
            }
        }

        protected void txtDate_TextChanged(object sender, EventArgs e)
        {
            btnSearch.Focus();
        }


    }
}