using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using AlchemyAccounting;

namespace DynamicMenu.Stock.Report.UI
{
    public partial class OrderSummaryDepartmentWise : System.Web.UI.Page
    {
        IFormatProvider dateformat = new System.Globalization.CultureInfo("fr-FR", true);
        protected void Page_Load(object sender, EventArgs e)
        {
            if (Session["USERID"] == null)
                Response.Redirect("~/Login/UI/Login.aspx");
            else
            {
                const string formLink = "/Stock/Report/UI/OrderSummaryDepartmentWise.aspx";
                bool permission = UserPermissionChecker.checkParmit(formLink, "STATUS");
                if (permission)
                {
                    if (!IsPostBack)
                    {
                        DateTime today = dbFunctions.Timezone(DateTime.Now);
                        string td = dbFunctions.Dayformat(today);
                        txtFrom.Text = td;
                        txtTo.Text = td;
                        dbFunctions.DropDownAddAllTextWithValue(ddlCategory, "SELECT CATNM, CATID FROM STK_ITEMMST");
                        ddlCategory.Focus();
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
                Session["CatId"] = ddlCategory.SelectedValue;
                Session["CatName"] = ddlCategory.SelectedItem.Text;

                Page.ClientScript.RegisterStartupScript(
                    this.GetType(), "OpenWindow", "window.open('../Report/RtpOrderSummaryCategoryyWise.aspx','_newtab');", true);
            }
        }
    }
}