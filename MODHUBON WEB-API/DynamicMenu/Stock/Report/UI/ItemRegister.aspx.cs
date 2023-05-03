using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using AlchemyAccounting;

namespace DynamicMenu.Stock.Report.UI
{
    public partial class ItemRegister : System.Web.UI.Page
    {
        IFormatProvider dateformat = new System.Globalization.CultureInfo("fr-FR", true);

        protected void Page_Load(object sender, EventArgs e)
        {
            if (Session["USERID"] == null)
                Response.Redirect("~/Login/UI/Login.aspx");
            else
            {
                const string formLink = "/Stock/Report/UI/ItemRegister.aspx";
                bool permission = UserPermissionChecker.checkParmit(formLink, "STATUS");
                if (permission == true)
                {
                    if (!IsPostBack)
                    {
                        string uTp = HttpContext.Current.Session["USERTYPE"].ToString();
                        string brCD = HttpContext.Current.Session["BrCD"].ToString();
                        if (uTp == "COMPADMIN")
                        {
                            dbFunctions.dropDownAdd(ddlStore, "SELECT STORENM FROM STK_STORE");
                            txtItemNM.Focus();
                        }
                        else
                        {
                            dbFunctions.dropDownAdd(ddlStore, "SELECT STORENM FROM STK_STORE WHERE BRANCHCD ='" + brCD + "'");
                            ddlStore.Enabled = false;
                            ddlStore.Focus();
                        }
                        DateTime today = DateTime.Today.Date;
                        string td = dbFunctions.Dayformat(today);
                        txtFrom.Text = td;
                        txtTo.Text = td;
                        dbFunctions.lblAdd(@"SELECT STOREID FROM STK_STORE WHERE STORENM = '" + ddlStore.Text + "'", lblStoreID);
                    }
                }
                else
                {
                    Response.Redirect("../../DashBoard/UI/Default.aspx");
                }
            }
        }
        protected void ddlStore_SelectedIndexChanged(object sender, EventArgs e)
        {
            dbFunctions.lblAdd(@"Select STOREID FROM STK_STORE WHERE STORENM = '" + ddlStore.Text + "'", lblStoreID);
            txtItemNM.Text = "";
            txtItemNM.Focus();
        }

        protected void txtItemNM_TextChanged(object sender, EventArgs e)
        {
            if (txtItemNM.Text == "")
            {
                lblGridMsg.Visible = true;
                lblGridMsg.Text = "Select item name.";
                txtItemNM.Focus();
            }
            else
            {
                if (txtItemNM.Text.Length > 7 && txtItemNM.Text.Length == 8 && txtItemNM.Text == "")
                {
                    txtItemNM.Text = "";
                    lblItemCD.Text = "";
                    lblGridMsg.Visible = true;
                    lblGridMsg.Text = "Select item name.";
                    txtItemNM.Focus();
                }
                else
                {
                    string itemCD = txtItemNM.Text.Trim();
                    dbFunctions.lblAdd(@"Select ITEMID from STK_ITEM where  ITEMCD ='" + itemCD + "'", lblItemID);
                    dbFunctions.lblAdd(@"Select ITEMNM from STK_ITEM where  ITEMCD ='" + itemCD + "'", lblItemNm);
                    btnSearch.Focus();
                }
            }
        }

        protected void txtFrom_TextChanged(object sender, EventArgs e)
        {
            btnSearch.Focus();
        }

        protected void txtTo_TextChanged(object sender, EventArgs e)
        {
            btnSearch.Focus();
        }

        protected void btnSearch_Click(object sender, EventArgs e)
        {
            DateTime FrDT = DateTime.Parse(txtFrom.Text, dateformat, System.Globalization.DateTimeStyles.AssumeLocal);
            DateTime ToDT = DateTime.Parse(txtTo.Text, dateformat, System.Globalization.DateTimeStyles.AssumeLocal);

            if (txtItemNM.Text == "")
            {
                Response.Write("<script>alert('Select an Item.');</script>");
                txtItemNM.Focus();
            }

            else if (FrDT > ToDT)
            {
                Response.Write("<script>alert('From Date is Greater than To Date.');</script>");
                btnSearch.Focus();
            }
            else
            {
                Session["StoreNm"] = ddlStore.Text;
                Session["StoreID"] = lblStoreID.Text;
                Session["ItemNM"] = lblItemNm.Text;
                Session["ItemID"] = lblItemID.Text;
                Session["itemcd"] = txtItemNM.Text;
                Session["From"] = txtFrom.Text;
                Session["To"] = txtTo.Text;

                Page.ClientScript.RegisterStartupScript(
                    this.GetType(), "OpenWindow", "window.open('../Report/rptItemRegister.aspx','_newtab');", true);
            }
        }
    }
}