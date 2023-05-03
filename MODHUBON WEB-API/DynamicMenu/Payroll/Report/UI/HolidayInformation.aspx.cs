using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using AlchemyAccounting;

namespace DynamicMenu.Payroll.Report.UI
{
    public partial class HolidayInformation : System.Web.UI.Page
    {
        IFormatProvider dateformat = new System.Globalization.CultureInfo("fr-FR", true);
        protected void Page_Load(object sender, EventArgs e)
        {
            if (Session["USERID"] == null)
                Response.Redirect("~/Login/UI/Login.aspx");
            else
            {
                const string formLink = "/Payroll/Report/UI/HolidayInformation.aspx";
                bool permission = UserPermissionChecker.checkParmit(formLink, "STATUS");
                if (permission == true)
                {
                    if (!IsPostBack)
                    {
                        txtFrDT.Text =dbFunctions.Timezone(DateTime.Now).ToString("dd/MM/yyyy");
                        txtToDT.Text =dbFunctions.Timezone(DateTime.Now).ToString("dd/MM/yyyy");
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

            DateTime dateFR = DateTime.Parse(txtFrDT.Text, dateformat, System.Globalization.DateTimeStyles.AssumeLocal);
            DateTime dateTO = DateTime.Parse(txtToDT.Text, dateformat, System.Globalization.DateTimeStyles.AssumeLocal);
            if (dateFR > dateTO)
            {
                lblMSG.Visible = true;
                lblMSG.Text = "From Date Must Be Small to To Date !";
                txtFrDT.Focus();
            }
            else
            {
                lblMSG.Visible = false;
                if (txtFrDT.Text == "")
                    txtFrDT.Focus();
                else if (txtToDT.Text == "")
                    txtToDT.Focus();

                else
                {
                    Session["FrDT"] = null;
                    Session["ToDT"] = null;

                    Session["FrDT"] = txtFrDT.Text;
                    Session["ToDT"] = txtToDT.Text;
                    ScriptManager.RegisterStartupScript(this,
                       this.GetType(), "OpenWindow", "window.open('../Report/HolidayInformationPrint.aspx','_newtab');", true);
                }
            }
        }

    }
}