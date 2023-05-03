using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using AlchemyAccounting;

namespace DynamicMenu.Stock.Report.UI
{
    public partial class ItemStockStore : System.Web.UI.Page
    {
        IFormatProvider dateformat = new System.Globalization.CultureInfo("fr-FR", true);
        protected void Page_Load(object sender, EventArgs e)
        {
            if (Session["USERID"] == null)
                Response.Redirect("~/Login/UI/Login.aspx");
            else
            {
                const string formLink = "/Stock/Report/UI/ItemStockStore.aspx";
                bool permission = UserPermissionChecker.checkParmit(formLink, "STATUS");
                if (permission == true)
                {
                    if (!IsPostBack)
                    {
                        DateTime today = DateTime.Today.Date;
                        string td = dbFunctions.Dayformat(today);
                        txtDate.Text = td;

                        txtItemNM.Focus();
                    }
                }
                else
                {
                    Response.Redirect("../../DashBoard/UI/Default.aspx");
                }
            }
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
                    lblGridMsg.Visible = false;
                    string itemCD = txtItemNM.Text.Trim();
                    dbFunctions.lblAdd(@"Select ITEMID from STK_ITEM where  ITEMCD ='" + itemCD + "'", lblItemID);
                    dbFunctions.lblAdd(@"Select ITEMNM from STK_ITEM where  ITEMCD ='" + itemCD + "'", lblItemNm);
                    btnSearch.Focus();
                }
            }
        }

        protected void btnSearch_Click(object sender, EventArgs e)
        {
            if (txtItemNM.Text == "")
            {
                Response.Write("<script>alert('Select an Item.');</script>");
                txtItemNM.Focus();
            }
            else if (txtitemId.Text == "")
            {
                Response.Write("<script>alert('Select Proper Item.');</script>");
                txtItemNM.Focus();
            }

            else
            {
                Session["ItemNM"] = txtItemNM.Text;
                Session["ItemID"] = txtitemId.Text;
                Session["itemcd"] = txtitemId.Text;
                Session["Date"] = txtDate.Text;

                Page.ClientScript.RegisterStartupScript(
                    this.GetType(), "OpenWindow", "window.open('../Report/rptItemStockStore.aspx','_newtab');", true);
            }
        }
    }
}