using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using AlchemyAccounting;

namespace DynamicMenu.Payroll.Report.UI
{
    public partial class SalaryAdditionDeduction : Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (Session["USERID"] == null)
                Response.Redirect("~/Login/UI/Login.aspx");
            else
            {
                const string formLink = "/Payroll/Report/UI/SalaryAdditionDeduction.aspx";
                bool permission = UserPermissionChecker.checkParmit(formLink, "STATUS");
                if (permission)
                {
                    if (!IsPostBack)
                    {
                        txtDate.Text =dbFunctions.Timezone(DateTime.Now).ToString("MMM-yy").ToUpper();
                        var uTp = HttpContext.Current.Session["USERTYPE"].ToString();
                        //var brCD = HttpContext.Current.Session["BrCD"].ToString();
                        //if (uTp == "COMPADMIN")
                            DropDownAddSelectTextWithValue(ddlBranch, "SELECT BRANCHNM, BRANCHCD FROM ASL_BRANCH");
                        //else
                        //{
                        //    DropDownAddSelectTextWithValue(ddlBranch, "SELECT BRANCHNM, BRANCHCD FROM ASL_BRANCH WHERE BRANCHCD='" + brCD + "'");
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
        public static void DropDownAddSelectTextWithValue(DropDownList ob, String sql)
        {
            var listName = new List<string>();
            var listValue = new List<string>();
            try
            {
                var con = new SqlConnection(dbFunctions.connection);
                con.Open();
                var cmd = new SqlCommand(sql, con);
                var rd = cmd.ExecuteReader();
                listName.Clear();
                listValue.Clear();
                listName.Add("ALL");
                listValue.Add("ALL");
                while (rd.Read())
                {
                    listName.Add(rd[0].ToString());
                    listValue.Add(rd[1].ToString());
                }
                rd.Close();
                ob.Items.Clear();

                ob.Text = "";
                for (var i = 0; i < listName.Count; i++)
                {
                    ob.Items.Add(new ListItem(listName[i].ToUpper(), listValue[i]));
                }
            }
            catch
            {
                // ignored
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
            else
            {
                Session["MonthYear"] = null;
                Session["BranchId"] = null;
                Session["BranchName"] = null;

                Session["MonthYear"] = txtDate.Text;
                Session["BranchId"] = ddlBranch.SelectedValue;
                Session["BranchName"] = ddlBranch.SelectedItem.Text;
                ScriptManager.RegisterStartupScript(this,
                   GetType(), "OpenWindow", "window.open('../Report/rptSalaryAdditionDeduction.aspx','_newtab');", true);

            }
        }
    }
}