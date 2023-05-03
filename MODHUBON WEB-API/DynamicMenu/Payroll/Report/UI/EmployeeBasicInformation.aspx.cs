using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using AlchemyAccounting;

namespace DynamicMenu.Payroll.Report.UI
{
    public partial class EmployeeBasicInformation : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (Session["USERID"] == null)
                Response.Redirect("~/Login/UI/Login.aspx");
            else
            {
                const string formLink = "/Payroll/Report/UI/EmployeeBasicInformation.aspx";
                bool permission = UserPermissionChecker.checkParmit(formLink, "STATUS");
                if (permission == true)
                {
                    if (!IsPostBack)
                    {
                       dbFunctions.dropDownAddWithSelect(DropDownList1, "SELECT DISTINCT EMPNM FROM HR_EMP");
                    }
                }
                else
                {
                    Response.Redirect("../../../DeshBoard/UI/Default.aspx");
                }
            }
        }
        protected void DropDownList1_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (DropDownList1.Text == "--SELECT--"){
                lblMSG.Text = "Select Employee Name";
                lblMSG.Visible = true;
            }
            else
                dbFunctions.lblAdd("SELECT EMPID FROM HR_EMP WHERE EMPNM='" + DropDownList1.Text + "'", lblEmpID);
        }

        protected void Button1_Click(object sender, EventArgs e)
        {
            Session["EMPID"] = null;
            if (DropDownList1.Text == "ALL")
            {
                Session["EMPID"] = lblEmpID.Text;
                ScriptManager.RegisterStartupScript(this,
                        this.GetType(), "OpenWindow", "window.open('../Report/EmployeeReport.aspx','_newtab');", true);
            }
            else
            {
                Session["EMPID"] = lblEmpID.Text;
                ScriptManager.RegisterStartupScript(this,
                        this.GetType(), "OpenWindow", "window.open('../Report/EmployeeBasicInformationPrint.aspx','_newtab');", true);
            }
        }
    }
}