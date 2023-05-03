using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using AlchemyAccounting;

namespace DynamicMenu.Accounts.Report.UI
{
    public partial class TrialBalanceDetails : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (Session["USERID"] == null)
                Response.Redirect("~/Login/UI/Login.aspx");
            else
            {
                const string formLink = "/Accounts/Report/UI/TrialBalanceDetails.aspx";
                bool permission = UserPermissionChecker.checkParmit(formLink, "STATUS");
                if (permission == true)
                {
                    if (!IsPostBack)
                    {
                        string td = dbFunctions.Timezone(DateTime.Now).ToString("dd/MM/yyyy");
                        txtFromDate.Text = td;
                        txtToDate.Text = td;
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
            if (txtFromDate.Text == "")
            {
                Response.Write("<script>alert('Select From Date.');</script>");
            }
            else if (txtToDate.Text == "")
            {
                Response.Write("<script>alert('Select To Date.');</script>");
            }
            else
            {
                Session["FromDate"] = txtFromDate.Text;
                Session["ToDate"] = txtToDate.Text;

                Page.ClientScript.RegisterStartupScript(
                    this.GetType(), "OpenWindow", "window.open('../Report/rptTrialBalanceDetails.aspx','_newtab');", true);
            }
        }
    }
}