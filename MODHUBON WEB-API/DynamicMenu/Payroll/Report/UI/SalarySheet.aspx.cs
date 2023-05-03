using AlchemyAccounting;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace DynamicMenu.Payroll.Report.UI
{
    public partial class SalarySheet : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (Session["USERID"] == null)
                Response.Redirect("~/Login/UI/Login.aspx");
            else
            {
                const string formLink = "/Payroll/Report/UI/SalarySheet.aspx";
                bool permission = UserPermissionChecker.checkParmit(formLink, "STATUS");
                if (permission == true)
                {
                    if (!IsPostBack)
                    {
                        txtDate.Text = dbFunctions.Timezone(DateTime.Now).ToString("MMM-yy").ToUpper();
                        var uTp = HttpContext.Current.Session["USERTYPE"].ToString();
                        //var brCD = HttpContext.Current.Session["BrCD"].ToString();
                        //if (uTp == "COMPADMIN")
                        //{
                        //dbFunctions.DropDownAddAllTextWithValue(ddlBranch, "SELECT BRANCHNM, BRANCHCD FROM ASL_BRANCH");
                        ////}
                        ////else
                        ////{
                        ////   dbFunctions.DropDownAddTextWithValue(ddlBranch, "SELECT BRANCHNM, BRANCHCD FROM ASL_BRANCH WHERE BRANCHCD='" + brCD + "'");
                        ////}
                        //dbFunctions.DropDownAddAllTextWithValue(ddlDepartment, "SELECT DEPTNM, DEPTID FROM HR_DEPT ORDER BY DEPTNM");
                        //ddlBranch.Focus();
                    }
                }
                else
                {
                    Response.Redirect("../../../DeshBoard/UI/Default.aspx");
                }
            }
        }

        protected void btnSubmit_Click(object sender, EventArgs e)
        {
            if (txtDate.Text == "")
            {
                lblMSG.Visible = true;
                lblMSG.Text = "Select Month/Year.";
                txtDate.Focus();
            }
            else
            {
                Session["MonthYear"] = null;

                Session["MonthYear"] = txtDate.Text;
                ScriptManager.RegisterStartupScript(this,
                   this.GetType(), "OpenWindow", "window.open('../Report/rptSalarySheetByEmpGroup.aspx','_newtab');", true);

            }
        }
    }
}