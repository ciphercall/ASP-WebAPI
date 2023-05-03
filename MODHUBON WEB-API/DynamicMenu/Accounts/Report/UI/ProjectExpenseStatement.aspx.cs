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
    public partial class ProjectExpenseStatement : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (Session["USERID"] == null)
                Response.Redirect("~/Login/UI/Login.aspx");
            else
            {
                const string formLink = "/Accounts/Report/UI/ProjectExpenseStatement.aspx";
                bool permission = UserPermissionChecker.checkParmit(formLink, "STATUS");
                if (permission == true)
                {
                    if (!Page.IsPostBack)
                    {
                        DateTime today = DateTime.Today.Date;
                        string td = dbFunctions.Timezone(DateTime.Now).ToString("dd/MM/yyyy");
                        txtFrom.Text = td;
                        txtTo.Text = td;
                        txtProjectNm.Focus();
                    }
                }
                else
                {
                    Response.Redirect("../../../DeshBoard/UI/Default.aspx");
                }
            }
        }


        [System.Web.Services.WebMethodAttribute(), System.Web.Script.Services.ScriptMethodAttribute()]
        public static string[] GetCompletionList(string prefixText, int count, string contextKey)
        {
            string connectionString = ConfigurationManager.ConnectionStrings["AslDbContext"].ConnectionString;
            SqlConnection conn = new SqlConnection(connectionString);
            // Try to use parameterized inline query/sp to protect sql injection
            string uTp = HttpContext.Current.Session["USERTYPE"].ToString();
            string brCD = HttpContext.Current.Session["BrCD"].ToString();
            SqlCommand cmd = new SqlCommand();
            if (uTp == "COMPADMIN")
            {
                cmd = new SqlCommand("SELECT COSTPNM FROM GL_COSTP WHERE COSTPNM LIKE '" + prefixText + "%'", conn);
            }
            else
            {
                cmd = new SqlCommand("SELECT COSTPNM FROM GL_COSTP WHERE COSTPNM LIKE '" + prefixText + "%' AND (CATID ='" + brCD + "' OR CATID IS NULL OR CATID ='')", conn);
            }
            SqlDataReader oReader;
            conn.Open();
            List<String> CompletionSet = new List<string>();
            oReader = cmd.ExecuteReader(CommandBehavior.CloseConnection);
            while (oReader.Read())
                CompletionSet.Add(oReader["COSTPNM"].ToString());
            return CompletionSet.ToArray();
        }

        protected void txtProjectNm_TextChanged(object sender, EventArgs e)
        {
            if (txtProjectNm.Text != "")
            {
                dbFunctions.txtAdd(@"Select COSTPID from GL_COSTP where COSTPNM = '" + txtProjectNm.Text + "'", txtProjectCD);
            }
            else
                txtProjectNm.Text = "";
            txtProjectNm.Focus();
        }

        protected void txtFrom_TextChanged(object sender, EventArgs e)
        {

        }

        protected void txtTo_TextChanged(object sender, EventArgs e)
        {
            btnSearch.Focus();
        }

        protected void btnSearch_Click(object sender, EventArgs e)
        {
            if (txtProjectNm.Text == "" || txtFrom.Text == "" || txtTo.Text == "")
            {
                Response.Write("<script>alert('Fill Required Data');</script>");
            }
            else if (ddlType.SelectedItem.Text == "--SELECT--")
            {
                Response.Write("<script>alert('Select Transaction Type');</script>");
            }
            else
            {
                Session["ProjectCD"] = txtProjectCD.Text;
                Session["ProjectNM"] = txtProjectNm.Text;
                Session["Typecd"] = ddlType.SelectedValue;
                Session["TypeName"] = ddlType.SelectedItem.Text;
                Session["From"] = txtFrom.Text;
                Session["To"] = txtTo.Text;
                Page.ClientScript.RegisterStartupScript(
                    this.GetType(), "OpenWindow", "window.open('../Report/rptProjectExpense.aspx','_newtab');", true);
            }
        }
    }
}