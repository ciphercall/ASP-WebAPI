using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using AlchemyAccounting;

namespace DynamicMenu.Accounts.Report.UI
{
    public partial class ReportBankBook : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (Session["USERID"] == null)
                Response.Redirect("~/Login/UI/Login.aspx");
            else
            {
                const string formLink = "/Accounts/Report/UI/RptBankBook.aspx";
                bool permission = UserPermissionChecker.checkParmit(formLink, "STATUS");
                if (permission == true)
                {
                    if (!IsPostBack)
                    {
                        string td = dbFunctions.Timezone(DateTime.Now).ToString("dd/MM/yyyy");
                        txtFrom.Text = td;
                        txtTo.Text = td;
                        dbFunctions.DropDownAddTextWithValue(ddlHeadName, @"SELECT ACCOUNTNM, ACCOUNTCD FROM GL_ACCHART WHERE substring(ACCOUNTCD,1,7) =('1020102') and STATUSCD='P'");
                        ddlHeadName.Focus();
                    }
                }
                else
                {
                    Response.Redirect("../../../DeshBoard/UI/Default.aspx");
                }
            }
        }

     
        protected void btnSearch_Click(object sender, EventArgs e)
        {
            if (ddlHeadName.Text == "" || txtFrom.Text == "" || txtTo.Text == "")
            {
                Response.Write("<script>alert('Fill Required Data');</script>");
            }
            else
            {
                Session["AccCode"] = ddlHeadName.SelectedValue;
                Session["AccNM"] = ddlHeadName.SelectedItem.Text;
                Session["From"] = txtFrom.Text;
                Session["To"] = txtTo.Text;
                Page.ClientScript.RegisterStartupScript(
                    this.GetType(), "OpenWindow", "window.open('../Report/ReportBankBook.aspx','_newtab');", true);
            }
        }
    }
}