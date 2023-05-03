using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using AlchemyAccounting;

namespace DynamicMenu.Payroll.Report.UI
{
    public partial class AttendanceAndOtPeriodic : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (Session["USERID"] == null)
                Response.Redirect("~/Login/UI/Login.aspx");
            else
            {
                const string formLink = "/Payroll/Report/UI/AttendanceOtRegisterEmployeeWise.aspx";
                bool permission = UserPermissionChecker.checkParmit(formLink, "STATUS");
                if (permission)
                {
                    if (!IsPostBack)
                    {
                        txtFromDate.Text =dbFunctions.Timezone(DateTime.Now).ToString("dd/MM/yyyy").ToUpper();
                        txtToDate.Text = txtFromDate.Text;
                        //var uTp = HttpContext.Current.Session["USERTYPE"].ToString();
                        //var brCD = HttpContext.Current.Session["BrCD"].ToString();
                        
                       dbFunctions.DropDownAddAllTextWithValue(ddlBranch, "SELECT BRANCHNM, BRANCHCD FROM ASL_BRANCH ORDER BY BRANCHNM");
                        ddlBranch.Focus();
                  
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
            if (txtFromDate.Text == "")
            {
                lblMSG.Visible = true;
                lblMSG.Text = "Select Date.";
                txtFromDate.Focus();
            }
            else if (txtFromDate.Text == "")
            {
                lblMSG.Visible = true;
                lblMSG.Text = "Select Date.";
                txtFromDate.Focus();
            }
            else
            {
                Session["Fdate"] = null;
                Session["Tdate"] = null;
                Session["Fdate"] = txtFromDate.Text;
                Session["Tdate"] = txtToDate.Text;
                Session["BranchName"] = ddlBranch.SelectedItem.Text;
                Session["BranchId"] = ddlBranch.SelectedValue;

                ScriptManager.RegisterStartupScript(this,
                   GetType(), "OpenWindow", "window.open('../Report/rptAttendanceOtPeriodic.aspx','_newtab');", true);

            }
        }
    }
}