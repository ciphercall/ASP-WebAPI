using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using AlchemyAccounting;

namespace DynamicMenu.Payroll.Report.UI
{
    public partial class AttendanceOtRegisterEmployeeWise : System.Web.UI.Page
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
                        var uTp = HttpContext.Current.Session["USERTYPE"].ToString();
                        //var brCD = HttpContext.Current.Session["BrCD"].ToString();
                        //if (uTp == "COMPADMIN")
                        //{
                       dbFunctions.DropDownAddTextWithValue(ddlBranch, "SELECT BRANCHNM, BRANCHCD FROM ASL_BRANCH ORDER BY BRANCHNM");
                        ddlBranch.Focus();
                        //}
                        //else
                        //{
                        //   dbFunctions.DropDownAddTextWithValue(ddlEmployee, "SELECT BRANCHNM, BRANCHCD FROM ASL_BRANCH WHERE BRANCHCD='" + brCD + "' ORDER BY BRANCHNM");
                        //    ddlBranch.Focus();
                        //}
                       dbFunctions.DropDownAddSelectTextWithValue(ddlEmployee, @"SELECT HR_EMP.EMPNM, HR_EMP.EMPID FROM HR_EMP  WHERE HR_EMP.COSTPID = '" + ddlBranch.SelectedValue + "'");

                    }
                }
                else
                {
                    Response.Redirect("../../../DeshBoard/UI/Default.aspx");
                }
            }
        }
        protected void ddlBranch_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (ddlBranch.Text == "--SELECT--")
            {
                lblMSG.Text = "Select Branch Name.";
                lblMSG.Visible = true;
            }
            else
            {
               dbFunctions.DropDownAddSelectTextWithValue(ddlEmployee, @"SELECT HR_EMP.EMPNM, HR_EMP.EMPID FROM HR_EMP  WHERE HR_EMP.COSTPID = '" + ddlBranch.SelectedValue + "'");
                lblMSG.Text = "";
                lblMSG.Visible = false;
                ddlEmployee.Focus();
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
            else if (ddlEmployee.Text == "--SELECT--")
            {
                lblMSG.Visible = true;
                lblMSG.Text = "Select Shift.";
                ddlEmployee.Focus();
            }
            else
            {
                Session["Fdate"] = null;
                Session["Tdate"] = null;
                Session["EmployyeId"] = null;
                Session["EmployyeName"] = null;
                Session["BranchName"] = null;
                Session["Fdate"] = txtFromDate.Text;
                Session["Tdate"] = txtToDate.Text;
                Session["EmployyeId"] = ddlEmployee.SelectedValue;
                Session["EmployyeName"] = ddlEmployee.SelectedItem.Text;
                Session["BranchName"] = ddlBranch.SelectedItem.Text;

                ScriptManager.RegisterStartupScript(this,
                   GetType(), "OpenWindow", "window.open('../Report/rptAttendanceAndOtRegister.aspx','_newtab');", true);

            }
        }
    }
}