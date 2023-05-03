using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using AlchemyAccounting;
using DynamicMenu.Payroll.DataAccess;
using DynamicMenu.Payroll.Interface;

namespace DynamicMenu.Payroll.UI
{
    public partial class MachineAttProcess : System.Web.UI.Page
    {
        IFormatProvider dateformat = new System.Globalization.CultureInfo("fr-FR", true);
        PayrollDataAcces dob = new PayrollDataAcces();
        PayrollInterface iob = new PayrollInterface();
        SqlConnection conn = new SqlConnection(dbFunctions.connection);
        protected void Page_Load(object sender, EventArgs e)
        {
            if (Session["USERID"] == null)
            {
                Response.Redirect("~/Login/UI/Login.aspx");
            }
            else
            {
                const string formLink = "/Payroll/UI/MachineAttProcess.aspx";
                bool permission = UserPermissionChecker.checkParmit(formLink, "STATUS");
                if (permission == true)
                {
                    if (!Page.IsPostBack)
                    {
                        var monyear =dbFunctions.Timezone(DateTime.Now);
                        txtAttDate.Text = monyear.ToString("dd-MM-yyyy").ToUpper();
                       dbFunctions.DropDownAddTextWithValue(ddlBranch, @"SELECT DEPTNM, DEPTID FROM HR_DEPT");
                    }
                }
                else
                {
                    Response.Redirect("../../DeshBoard/UI/Default.aspx");
                }
            }
        }

        protected void btnAttProcess_OnClick(object sender, EventArgs e)
        {
            if (Session["USERID"] == null)
            {
                Response.Redirect("~/Login/UI/Login.aspx");
            }
            else
            {
                var departmentid = ddlBranch.SelectedValue;
                var transDate =
                    (DateTime.Parse(txtAttDate.Text, dateformat, System.Globalization.DateTimeStyles.AssumeLocal))
                        .ToString("yyyy/MM/dd");
               dbFunctions.ExecuteQuery($@"DELETE FROM HR_ATREG INNER JOIN HR_EMP ON HR_ATREG.EMPID = HR_EMP.EMPID
                WHERE TRANSDT='{transDate}' AND ENTRYTPI='MENUAL' AND ENTRYTPO='MENUAL' AND HR_EMP.DEPTID='{departmentid}'");


                
            }
        }
    }
}