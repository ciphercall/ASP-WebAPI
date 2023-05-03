using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace DynamicMenu.Stock.Report.UI
{
    public partial class ItemInformation : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (Session["USERID"] == null)
                Response.Redirect("~/Login/UI/Login.aspx");
            else
            {
                const string formLink = "/Stock/Report/UI/ItemInformation.aspx";
                bool permission = UserPermissionChecker.checkParmit(formLink, "STATUS");
                if (permission == true)
                {
                    if (!IsPostBack)
                    {
                       btnSearch.Focus();
                    }
                }
                else
                {
                    Response.Redirect("../../DashBoard/UI/Default.aspx");
                }
            }
        }

        protected void btnSearch_OnClick(object sender, EventArgs e)
        {
            if (Session["USERID"] == null)
                Response.Redirect("~/Login/UI/Login.aspx");
            else
                Page.ClientScript.RegisterStartupScript(
                  this.GetType(), "OpenWindow", "window.open('../Report/rptItem.aspx','_newtab');", true);
        }
    }
}