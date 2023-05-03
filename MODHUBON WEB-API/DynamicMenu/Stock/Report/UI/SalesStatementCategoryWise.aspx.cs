using System;
using AlchemyAccounting;

namespace DynamicMenu.Stock.Report.UI
{
    public partial class SalesStatementCategoryWise : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (Session["USERID"] == null)
                Response.Redirect("~/Login/UI/Login.aspx");
            else
            {
                const string formLink = "/Stock/Report/UI/SalesStatementCategoryWise.aspx";
                bool permission = UserPermissionChecker.checkParmit(formLink, "STATUS");
                if (permission)
                {
                    if (!IsPostBack)
                    {
                        string date = dbFunctions.Timezone(DateTime.Now).ToString("dd/MM/yyyy");
                        txtFdt.Text = date;
                        txtToDt.Text = date;
                    }
                }
                else
                {
                    Response.Redirect("../../DashBoard/UI/Default.aspx");
                }
            }
        }
        public string FeildCheck()
        {
            string check = "false";
            if (ddlType.SelectedItem.Text == "--SELECT--")
            {
                lblmsg.Text = "Select type.";
                ddlType.Focus();
            }
            else if (txtFdt.Text == "")
            {
                txtFdt.Text = "Select date.";
                txtFdt.Focus();
            }
            else if (txtToDt.Text == "")
            {
                txtToDt.Text = "Select date.";
                txtToDt.Focus();
            }
            else
            {
                check = "true";
            }
            return check;
        }


        protected void btnSubmit_Click(object sender, EventArgs e)
        {
            if (Session["USERID"] == null)
            {
                Response.Redirect("~/Login/UI/Login.aspx");
            }
            else
            {
                if (FeildCheck() == "true")
                {
                    Session["Type"] = ddlType.SelectedValue;
                    Session["FromDate"] = txtFdt.Text;
                    Session["ToDate"] = txtToDt.Text;

                    Page.ClientScript.RegisterStartupScript(
               GetType(), "OpenWindow", "window.open('../Report/rpt-Sale-Statement-Category.aspx','_newtab');", true);

                }
            }
        }
    }
}