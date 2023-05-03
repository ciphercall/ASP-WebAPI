using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using AlchemyAccounting;

namespace DynamicMenu.Stock.Report.UI
{
    public partial class OrderSummaryPartyWise : System.Web.UI.Page
    {
        IFormatProvider dateformat = new System.Globalization.CultureInfo("fr-FR", true);
        protected void Page_Load(object sender, EventArgs e)
        {
            if (Session["USERID"] == null)
                Response.Redirect("~/Login/UI/Login.aspx");
            else
            {
                const string formLink = "/Stock/Report/UI/OrderSummaryPartyWise.aspx";
                bool permission = UserPermissionChecker.checkParmit(formLink, "STATUS");
                if (permission)
                {
                    if (!IsPostBack)
                    {
                        DateTime today = dbFunctions.Timezone(DateTime.Now);
                        string td = dbFunctions.Dayformat(today);
                        txtFrom.Text = td;
                        txtTo.Text = td;
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
        protected void btnSearch_Click(object sender, EventArgs e)
        {
            DateTime FrDT = DateTime.Parse(txtFrom.Text, dateformat, System.Globalization.DateTimeStyles.AssumeLocal);
            DateTime ToDT = DateTime.Parse(txtTo.Text, dateformat, System.Globalization.DateTimeStyles.AssumeLocal);
            if (FrDT > ToDT)
            {
                Response.Write("<script>alert('From Date is Greater than To Date.');</script>");
                btnSearch.Focus();
            }
            else
            {
                Session["From"] = txtFrom.Text;
                Session["To"] = txtTo.Text;
                Session["PartyId"] = ddlPartyList.SelectedValue;
                Session["PartyName"] = ddlPartyList.SelectedItem.Text;

                Page.ClientScript.RegisterStartupScript(
                    this.GetType(), "OpenWindow", "window.open('../Report/RtpOrderSummaryPartyWise.aspx','_newtab');", true);
            }
        }
    }
}