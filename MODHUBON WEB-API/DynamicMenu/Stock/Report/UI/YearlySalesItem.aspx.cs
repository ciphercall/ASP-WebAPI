using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using AlchemyAccounting;

namespace DynamicMenu.Stock.Report.UI
{
    public partial class YearlySalesItem : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {

            if (Session["USERID"] == null)
                Response.Redirect("~/Login/UI/Login.aspx");
            else
            {
                const string formLink = "/Stock/Report/UI/YearlySalesItem.aspx";
                bool permission = UserPermissionChecker.checkParmit(formLink, "STATUS");
                if (permission)
                {
                    if (!IsPostBack)
                    {
                        string td = dbFunctions.Timezone(DateTime.Now).ToString("yyyy");
                        txtFrom.Text = td;
                        dbFunctions.DropDownAddAllTextWithValue(ddlPartyList, "SELECT PARTYNM, PARTYID FROM STK_PARTY");
                        ddlPartyList.Focus();
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
            lblErrMsg.Visible = false;



            Session["btnSearct"] = "Amount";
            Session["PartyId"] = ddlPartyList.SelectedValue;
            Session["PartyName"] = ddlPartyList.SelectedItem.Text;

            Session["Year"] = txtFrom.Text;

            ScriptManager.RegisterStartupScript(this,
                        this.GetType(), "OpenWindow", "window.open('../Report/rptYearlySalesItem.aspx','_newtab');", true);

        }

        protected void Button1_OnClick(object sender, EventArgs e)
        {
            lblErrMsg.Visible = false;



            Session["btnSearct"] ="Qty";
            Session["PartyId"] = ddlPartyList.SelectedValue;
            Session["PartyName"] = ddlPartyList.SelectedItem.Text;

            Session["Year"] = txtFrom.Text;

            ScriptManager.RegisterStartupScript(this,
                        this.GetType(), "OpenWindow", "window.open('../Report/rptYearlySalesItem.aspx','_newtab');", true);

        }
    }
}