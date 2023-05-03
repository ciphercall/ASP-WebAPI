using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using AlchemyAccounting;

namespace DynamicMenu.Stock.Report.UI
{
    public partial class YearlySalesParty : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (Session["USERID"] == null)
                Response.Redirect("~/Login/UI/Login.aspx");
            else
            {
                const string formLink = "/Stock/Report/UI/YearlySalesParty.aspx";
                bool permission = UserPermissionChecker.checkParmit(formLink, "STATUS");
                if (permission)
                {
                    if (!IsPostBack)
                    {
                        string td = dbFunctions.Timezone(DateTime.Now).ToString("yyyy");
                        txtFrom.Text = td;
                        dbFunctions.DropDownAddAllTextWithValue(ddlPartyList, "SELECT ITEMNM, ITEMID FROM STK_ITEM");
                        ddlPartyList.Focus();
                    }
                }
                else
                {
                    Response.Redirect("../../DashBoard/UI/Default.aspx");
                }
            }
        }

      

        protected void btnqty_OnClick(object sender, EventArgs e)
        {
            lblErrMsg.Visible = false;



            Session["btnSearct"] = "Qty";
            Session["ItemId"] = ddlPartyList.SelectedValue;
            Session["ItemName"] = ddlPartyList.SelectedItem.Text;

            Session["Year"] = txtFrom.Text;

            ScriptManager.RegisterStartupScript(this,
                        this.GetType(), "OpenWindow", "window.open('../Report/rptYearlySalesParty.aspx','_newtab');", true);

        }

        protected void btnanount_OnClick(object sender, EventArgs e)
        {
            lblErrMsg.Visible = false;



            Session["btnSearct"] = "Amount";
            Session["ItemId"] = ddlPartyList.SelectedValue;
            Session["ItemName"] = ddlPartyList.SelectedItem.Text;

            Session["Year"] = txtFrom.Text;

            ScriptManager.RegisterStartupScript(this,
                        this.GetType(), "OpenWindow", "window.open('../Report/rptYearlySalesParty.aspx','_newtab');", true);

        }
    }
}