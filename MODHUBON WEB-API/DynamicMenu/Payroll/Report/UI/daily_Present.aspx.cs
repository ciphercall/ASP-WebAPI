using AlchemyAccounting;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace DynamicMenu.Payroll.UI
{
    public partial class daily_Present : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (Session["USERID"] == null)
            {
                Response.Redirect("~/Login/UI/Login.aspx");
            }
            else
            {
                const string formLink = "/Payroll/Report/UI/daily_Present.aspx";
                var permission = UserPermissionChecker.checkParmit(formLink, "STATUS");
                if (permission)
                {
                    if (!Page.IsPostBack)
                    {
                        var monyear = dbFunctions.Timezone(DateTime.Now);
                        txtAttDate.Text = monyear.ToString("dd/MM/yyyy");  
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

            if (txtAttDate.Text == "")
            {
                txtAttDate.Focus();
            } 
            else
            {
                Session["Fdate"] = null; 
                Session["Fdate"] = txtAttDate.Text;  
                ScriptManager.RegisterStartupScript(this,
                   GetType(), "OpenWindow", "window.open('../Report/rpr_dailyPresent.aspx','_newtab');", true);

            }
        }
    }
}