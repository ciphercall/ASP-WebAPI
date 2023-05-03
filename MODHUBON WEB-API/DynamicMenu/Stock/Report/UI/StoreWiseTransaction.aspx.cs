using System;
using System.Web;
using System.Web.UI.WebControls;
using AlchemyAccounting;

namespace DynamicMenu.Stock.Report.UI
{
    public partial class StoreWiseTransaction : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (Session["USERID"] == null)
                Response.Redirect("~/Login/UI/Login.aspx");
            else
            {
                const string formLink = "/Stock/Report/UI/StoreWiseTransaction.aspx";
                bool permission = UserPermissionChecker.checkParmit(formLink, "STATUS");
                if (permission)
                {
                    if (!IsPostBack)
                    {
                        string uTp = HttpContext.Current.Session["USERTYPE"].ToString();
                        string brCd = HttpContext.Current.Session["BrCD"].ToString();
                        if (uTp == "COMPADMIN")
                        {
                            dbFunctions.dropDownAddWithSelect(ddlStore, "SELECT STORENM FROM STK_STORE");
                        }
                        else
                        {
                            dbFunctions.dropDownAdd(ddlStore, "SELECT STORENM FROM STK_STORE WHERE BRANCHCD ='" + brCd + "'");
                            ddlStore.Enabled = false;
                        }
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
        public string CheckError()
        {
            string check;
            if (ddlStore.Text == "--SELECT--")
            {
                check = "Select store name.";
                ddlStore.Focus();
            }
            else if (ddlType.SelectedItem.Text == "--SELECT--")
            {
                check = "Select type.";
                ddlType.Focus();
            }
            else if (txtFdt.Text == "")
            {
                check = "Select From Date";
                txtFdt.Focus();
            }
            else if (txtToDt.Text == "")
            {
                check = "Select to Date";
                txtToDt.Focus();
            }
            else
            {
                check = "true";
            }
            return check;
        }

        public string StoreId()
        {
            var lblStoreid = new Label();
            dbFunctions.lblAdd("SELECT STOREID FROM STK_STORE WHERE STORENM='" + ddlStore.Text + "'", lblStoreid);
            string storeId = lblStoreid.Text;
            return storeId;
        }
        
        protected void btnSubmit_Click(object sender, EventArgs e)
        {
            if (Session["USERID"] == null)
                Response.Redirect("~/Login/UI/Login.aspx");
            else
            {
                if (CheckError() == "true")
                {
                    lblMsg.Visible = false;

                    Session["StoreId"] = StoreId();
                    Session["Type"] = ddlType.SelectedValue;
                    Session["FromDt"] = txtFdt.Text;
                    Session["ToDt"] = txtToDt.Text;
                    Session["StoreName"] = ddlStore.SelectedItem.Text;
                    Page.ClientScript.RegisterStartupScript(
                  GetType(), "OpenWindow", "window.open('../Report/rptStoreWisetransaction.aspx','_newtab');", true);
                }
                else
                {
                    lblMsg.Text = CheckError();
                    lblMsg.Visible = true;
                }
            }
        }

    }
}