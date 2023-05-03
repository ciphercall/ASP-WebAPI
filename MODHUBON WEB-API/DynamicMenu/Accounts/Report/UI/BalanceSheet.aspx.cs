using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using AlchemyAccounting;

namespace DynamicMenu.Accounts.Report.UI
{
    public partial class BasicInformation : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (Session["USERID"] == null)
                Response.Redirect("~/Login/UI/Login.aspx");
            else
            {
                const string formLink = "/Accounts/Report/UI/BalanceSheet.aspx";
                bool permission = UserPermissionChecker.checkParmit(formLink, "STATUS");
                if (permission == true)
                {
                    if (!IsPostBack)
                    {
                        DateTime today = DateTime.Today.Date;
                        string td = dbFunctions.Timezone(DateTime.Now).ToString("dd/MM/yyyy");
                        txtDate.Text = td;
                    }
                }
                else
                {
                    Response.Redirect("../../../DeshBoard/UI/Default.aspx");
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
                    this.GetType(), "OpenWindow", "window.open('../Report/rptBalanceSheet.aspx','_newtab');", true);
            }
        }



    }
}