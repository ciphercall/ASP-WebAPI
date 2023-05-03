using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using AlchemyAccounting;

namespace DynamicMenu.Stock.Report.UI
{
    public partial class StoreWisePurchaseDetails : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (Session["USERID"] == null)
                Response.Redirect("~/Login/UI/Login.aspx");
            else
            {
                const string formLink = "/Stock/Report/UI/StoreWisePurchaseDetails.aspx";
                bool permission = UserPermissionChecker.checkParmit(formLink, "STATUS");
                if (permission == true)
                {
                    if (!IsPostBack)
                    {
                        string uTp = HttpContext.Current.Session["USERTYPE"].ToString();
                        string brCD = HttpContext.Current.Session["BrCD"].ToString();
                        if (uTp == "COMPADMIN")
                        {
                            dbFunctions.dropDownAddWithSelect(ddlStore, "SELECT STORENM FROM STK_STORE");
                        }
                        else
                        {
                            dbFunctions.dropDownAdd(ddlStore, "SELECT STORENM FROM STK_STORE WHERE BRANCHCD ='" + brCD + "'");
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
        public string FeildCheck()
        {
            string check = "";

            if (txtFdt.Text == "")
            {
                check = "Select date.";
                txtFdt.Focus();
            }
            else if (ddlStore.Text == "--SELECT--")
            {
                check = "Select Store Name.";
                ddlStore.Focus();
            }
            else if (txtToDt.Text == "")
            {
                check = "Select date.";
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
                    lblStoreID.Text = "";
                    dbFunctions.lblAdd("SELECT STOREID FROM STK_STORE WHERE STORENM='" + ddlStore.Text + "'", lblStoreID);
                    Session["FromDate"] = txtFdt.Text;
                    Session["ToDate"] = txtToDt.Text;
                    Session["StoreId"] = lblStoreID.Text;
                    Session["StoreNm"] = ddlStore.SelectedItem.Text;
                    Session["TYPE"] = ddltype.SelectedValue;
                    Session["TYPEName"] = ddltype.SelectedItem.Text;
                    Page.ClientScript.RegisterStartupScript(
                            this.GetType(), "OpenWindow", "window.open('../Report/rtp-StoreWisePurchaseDetails.aspx','_newtab');", true);
                }
            }
        }
    }
}