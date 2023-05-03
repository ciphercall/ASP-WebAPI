using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using AlchemyAccounting;

namespace DynamicMenu.Payroll.Report.UI
{
    public partial class EmpolyeePaySlip : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (Session["USERID"] == null)
                Response.Redirect("~/Login/UI/Login.aspx");
            else
            {
                const string formLink = "/Payroll/Report/UI/EmpolyeePaySlip.aspx";
                bool permission = UserPermissionChecker.checkParmit(formLink, "STATUS");
                if (permission == true)
                {
                    if (!IsPostBack)
                    {
                        txtDate.Text =dbFunctions.Timezone(DateTime.Now).ToString("MMM-yy").ToUpper();
                        var uTp = HttpContext.Current.Session["USERTYPE"].ToString();
                        //var brCD = HttpContext.Current.Session["BrCD"].ToString();
                        //if (uTp == "COMPADMIN")
                       dbFunctions.DropDownAddSelectTextWithValue(ddlBranch, "SELECT BRANCHNM, BRANCHCD FROM ASL_BRANCH");
                        //else
                        //{
                        //   dbFunctions.DropDownAddSelectTextWithValue(ddlBranch, "SELECT BRANCHNM, BRANCHCD FROM ASL_BRANCH WHERE BRANCHCD='" + brCD + "'");
                        //}

                        ddlBranch.Focus();
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
               dbFunctions.DropDownAddAllTextWithValue(ddlEmployeeName, @"SELECT HR_EMP.EMPNM, HR_EMP.EMPID FROM HR_EMP INNER JOIN ASL_BRANCH ON 
                        HR_EMP.COSTPID = ASL_BRANCH.BRANCHCD WHERE ASL_BRANCH.BRANCHCD = '" + ddlBranch.SelectedValue + "'");
                ddlEmployeeName.Focus();

                lblMSG.Text = "";
                lblMSG.Visible = false;
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
            else if (ddlBranch.Text == "--SELECT--")
            {
                lblMSG.Visible = true;
                lblMSG.Text = "Select Branch Name.";
                ddlBranch.Focus();
            }
            else
            {
                Session["MonthYear"] = null;
                Session["BranchId"] = null;
                Session["BranchName"] = null;
                Session["EmployeeId"] = null;
                Session["EmployeeName"] = null;

                Session["MonthYear"] = txtDate.Text;
                Session["BranchId"] = ddlBranch.SelectedValue;
                Session["BranchName"] = ddlBranch.SelectedItem.Text;
                Session["EmployeeId"] = ddlEmployeeName.SelectedValue;
                Session["EmployeeName"] = ddlEmployeeName.SelectedItem.Text;
                ScriptManager.RegisterStartupScript(this,
                   this.GetType(), "OpenWindow", "window.open('../Report/rptPaySlip.aspx','_newtab');", true);

            }
        }
    }
}