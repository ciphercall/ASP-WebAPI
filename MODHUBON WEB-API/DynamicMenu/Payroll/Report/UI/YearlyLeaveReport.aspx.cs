using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using AlchemyAccounting;

namespace DynamicMenu.Payroll.Report.UI
{
    public partial class YearlyLeaveReport : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (Session["USERID"] == null)
                Response.Redirect("~/Login/UI/Login.aspx");
            else
            {
                const string formLink = "/Payroll/Report/UI/YearlyLeaveReport.aspx";
                bool permission = UserPermissionChecker.checkParmit(formLink, "STATUS");
                if (permission == true)
                {
                    if (!IsPostBack)
                    {
                       dbFunctions.dropDownAddWithSelect(ddlYR, "SELECT DISTINCT CONVERT(NVARCHAR(10),LEAVEYY,103) AS LEAVEYY FROM HR_LEAVEYY");
                    }
                }
                else
                {
                    Response.Redirect("../../../DeshBoard/UI/Default.aspx");
                }
            }
        }
        protected void Button1_Click(object sender, EventArgs e)
        {
            if (ddlYR.Text != "--SELECT--")
            {
                Session["YEAR"] = null;
                Session["YEAR"] = ddlYR.Text;
                ScriptManager.RegisterStartupScript(this,
                        this.GetType(), "OpenWindow", "window.open('../Report/yearlyLeaveReportPrint.aspx','_newtab');", true);

            }
            else
                ddlYR.Focus();

        }
    }
}