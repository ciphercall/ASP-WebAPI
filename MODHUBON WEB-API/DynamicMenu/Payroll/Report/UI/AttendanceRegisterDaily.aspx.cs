using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using AlchemyAccounting;

namespace DynamicMenu.Payroll.Report.UI
{
    public partial class AttendanceRegisterDaily : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (Session["USERID"] == null)
                Response.Redirect("~/Login/UI/Login.aspx");
            else
            {
                const string formLink = "/Payroll/Report/UI/AttendanceRegisterDaily.aspx";
                bool permission = UserPermissionChecker.checkParmit(formLink, "STATUS");
                if (permission)
                {
                    if (!IsPostBack)
                    {
                        txtDate.Text =dbFunctions.Timezone(DateTime.Now).ToString("dd/MM/yyyy").ToUpper();
                        var uTp = HttpContext.Current.Session["USERTYPE"].ToString();
                        //var brCD = HttpContext.Current.Session["BrCD"].ToString();
                        //if (uTp == "COMPADMIN")
                       dbFunctions.DropDownAddSelectTextWithValue(ddlBranch, "SELECT BRANCHNM, BRANCHCD FROM ASL_BRANCH");
                        //else
                        //{
                        //   dbFunctions.DropDownAddSelectTextWithValue(ddlBranch, "SELECT BRANCHNM, BRANCHCD FROM ASL_BRANCH WHERE BRANCHCD='" + brCD + "'");
                        //}

                       dbFunctions.DropDownAddSelectTextWithValue(ddlShift, "SELECT SHIFTNM, SHIFTID FROM HR_SHIFT ORDER BY SHIFTNM");
                        ddlShift.Focus();
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
                lblMSG.Text = "Select Date.";
                txtDate.Focus();
            }
            else if (ddlBranch.Text == "--SELECT--")
            {
                lblMSG.Visible = true;
                lblMSG.Text = "Select Branch.";
                ddlBranch.Focus();
            }
            else if (ddlShift.Text == "--SELECT--")
            {
                lblMSG.Visible = true;
                lblMSG.Text = "Select Shift.";
                ddlShift.Focus();
            }
            else
            {
                Session["date"] = null;
                Session["BranchId"] = null;
                Session["BranchName"] = null;
                Session["shiftId"] = null;
                Session["shiftName"] = null;

                Session["date"] = txtDate.Text;
                Session["BranchId"] = ddlBranch.SelectedValue;
                Session["BranchName"] = ddlBranch.SelectedItem.Text;
                Session["shiftId"] = ddlShift.SelectedValue;
                Session["shiftName"] = ddlShift.SelectedItem.Text;

                ScriptManager.RegisterStartupScript(this,
                   GetType(), "OpenWindow", "window.open('../Report/rptAttendanceRegisterDaily.aspx','_newtab');", true);

            }
        }
    }
}