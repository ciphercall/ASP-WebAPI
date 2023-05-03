using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using AlchemyAccounting;

namespace DynamicMenu.Stock.Report.UI
{
    public partial class ClossingStock : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (Session["USERID"] == null)
                Response.Redirect("~/Login/UI/Login.aspx");
            else
            {
                const string formLink = "/Stock/Report/UI/ClossingStock.aspx";
                bool permission = UserPermissionChecker.checkParmit(formLink, "STATUS");
                if (permission == true)
                {
                    if (!IsPostBack)
                    {
                        string uTp = HttpContext.Current.Session["USERTYPE"].ToString();
                        string brCD = HttpContext.Current.Session["BrCD"].ToString();
                        if (uTp == "COMPADMIN")
                        {
                            dbFunctions.DropDownAddTextWithValue(ddlStore, "SELECT STORENM, STOREID FROM STK_STORE");
                        }
                        else
                        {
                            dbFunctions.DropDownAddTextWithValue(ddlStore, "SELECT STORENM, STOREID FROM STK_STORE WHERE BRANCHCD ='" + brCD + "'");
                            ddlStore.Enabled = false;
                        }
                        DateTime today = DateTime.Today.Date;
                        string td = dbFunctions.Dayformat(today);
                        txtDate.Text = td;
                    }
                }
                else
                {
                    Response.Redirect("../../DashBoard/UI/Default.aspx");
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
                Session["StoreNM"] = ddlStore.SelectedItem.Text;
                Session["StoreID"] = ddlStore.SelectedValue;
                Session["Date"] = txtDate.Text;

                Page.ClientScript.RegisterStartupScript(
                    this.GetType(), "OpenWindow", "window.open('../Report/rptClosingStock.aspx','_newtab');", true);
            }
        }
        protected void btnMinmunStockSearch_Click(object sender, EventArgs e)
        {
            if (txtDate.Text == "")
            {
                Response.Write("<script>alert('Select Date.');</script>");
            }
            else
            {
                Session["StoreNM"] = ddlStore.SelectedItem.Text;
                Session["StoreID"] = ddlStore.SelectedValue;
                Session["Date"] = txtDate.Text;

                Page.ClientScript.RegisterStartupScript(
                    this.GetType(), "OpenWindow", "window.open('../Report/rptMinimumStock.aspx','_newtab');", true);
            }
        }
    }
}