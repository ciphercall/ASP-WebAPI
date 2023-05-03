using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using AlchemyAccounting;

namespace DynamicMenu.Stock.Report.UI
{
    public partial class StoreWiseFactoryDetails : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (Session["USERID"] == null)
                Response.Redirect("~/Login/UI/Login.aspx");
            else
            {
                const string formLink = "/Stock/Report/UI/StoreWiseFactoryDetails.aspx";
                bool permission = UserPermissionChecker.checkParmit(formLink, "STATUS");
                if (permission == true)
                {
                    if (!IsPostBack)
                    {
                        string uTp = HttpContext.Current.Session["USERTYPE"].ToString();
                        string brCD = HttpContext.Current.Session["BrCD"].ToString();
                        if (uTp == "COMPADMIN")
                        {
                            dbFunctions.dropDownAdd(ddlStoreFr, "SELECT STORENM FROM STK_STORE");
                        }
                        else
                        {
                            dbFunctions.dropDownAdd(ddlStoreFr, "SELECT STORENM FROM STK_STORE WHERE BRANCHCD ='" + brCD + "'");
                            ddlStoreFr.Enabled = false;
                        }
                        dbFunctions.dropDownAddWithSelect(ddlStoreTo, "SELECT STORENM FROM STK_STORE");
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
            string check = "";
            if (ddlStoreTo.Text == "--SELECT--")
            {
                check = "Select store name.";
                ddlStoreTo.Focus();
            }
            else if (ddlStoreFr.Text == "--SELECT--")
            {
                check = "Select type.";
                ddlStoreFr.Focus();
            }
            else if (ddlStoreFr.Text == ddlStoreTo.Text)
            {
                check = "The store will not be the same";
                ddlStoreFr.Focus();
            }
            else if (ddlType.Text == "--SELECT--")
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

        public string StoreIdFr()
        {
            string storeId = "";
            Label lblStoreid = new Label();
            dbFunctions.lblAdd("SELECT STOREID FROM STK_STORE WHERE STORENM='" + ddlStoreFr.Text + "'", lblStoreid);
            storeId = lblStoreid.Text;
            return storeId;
        }
        public string StoreIdTo()
        {
            string storeId = "";
            Label lblStoreid = new Label();
            dbFunctions.lblAdd("SELECT STOREID FROM STK_STORE WHERE STORENM='" + ddlStoreTo.Text + "'", lblStoreid);
            storeId = lblStoreid.Text;
            return storeId;
        }
        public string Type()
        {
            string type = "";
            Label lblStoreid = new Label();
            if (ddlType.Text == "ISSUE")
                type = "IISS";
            else if (ddlType.Text == "RETURN")
                type = "IRTS";

            return type;
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

                    Session["StoreIdFr"] = StoreIdFr();
                    Session["StoreIdTo"] = StoreIdTo();
                    Session["Type"] = Type();
                    Session["FromDt"] = txtFdt.Text;
                    Session["ToDt"] = txtToDt.Text;
                    Session["TABLE"] = "STK_TRANSCS";
                    if (ddlSumOrDetail.Text == "DETAILS")
                        Page.ClientScript.RegisterStartupScript(
                  this.GetType(), "OpenWindow", "window.open('../Report/rptFactorySaleCenterDetails.aspx','_newtab');", true);
                    else
                    {
                        Page.ClientScript.RegisterStartupScript(
                  this.GetType(), "OpenWindow", "window.open('../Report/rptFactoryWiseTransSummary.aspx','_newtab');", true);
                    }
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