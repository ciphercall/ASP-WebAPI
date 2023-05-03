using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using AlchemyAccounting;

namespace DynamicMenu.Payroll.Report.UI
{
    public partial class LoanAmountPeriodic : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (Session["USERID"] == null)
                Response.Redirect("~/Login/UI/Login.aspx");
            else
            {
                const string formLink = "/Payroll/Report/UI/LoanAmountPeriodic.aspx";
                bool permission = UserPermissionChecker.checkParmit(formLink, "STATUS");
                if (permission == true)
                {
                    if (!IsPostBack)
                    {
                        //txtDate.Text = DbFunctions.Timezone(DateTime.Now).ToString("MMM-yy").ToUpper();
                       // var uTp = HttpContext.Current.Session["USERTYPE"].ToString();
                       // var brCD = HttpContext.Current.Session["BrCD"].ToString();

                        txtFDate.Text = dbFunctions.Timezone(DateTime.Now).ToString("dd/MM/yyyy").ToUpper();
                        txtTDate.Text = txtFDate.Text;

                    }
                }
                else
                {
                    Response.Redirect("../../../DeshBoard/UI/Default.aspx");
                }
            }
        }

        protected void btnSubmit_OnClick(object sender, EventArgs e)
        {
            if (txtFDate.Text == "")
            {
                lblMSG.Visible = true;
                lblMSG.Text = "Select From Date.";
                txtFDate.Focus();
            }
            else if (txtFDate.Text == "")
            {
                lblMSG.Visible = true;
                lblMSG.Text = "Select To Date.";
                txtFDate.Focus();
            }
            else
            {

                Session["Fdate"] = txtFDate.Text;
                Session["Tdate"] = txtTDate.Text;

               ScriptManager.RegisterStartupScript(this,
                   GetType(), "OpenWindow", "window.open('../Report/rptLoanAmountPeriodic.aspx','_newtab');", true);

            }
        }
    }
}