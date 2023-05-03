using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using AlchemyAccounting;

namespace DynamicMenu.ASLCompany.Report.Report
{
    public partial class AllUserInformation : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                if (Session["USERID"] == null)
                {
                    Response.Redirect("~/LogIn/UI/LogIn.aspx");
                }
                else
                {
                    if (Session["USERTYPE"].ToString() == "SUPERADMIN")
                    {
                        Session["CompanyIdForReport"] = null;
                        dbFunctions.dropDownAddWithSelect(txtCompanyName, "SELECT COMPNM FROM ASL_COMPANY WHERE COMPID!='100'");
                    }
                    else if (Session["USERTYPE"].ToString() == "COMPADMIN")
                    {
                        lblMsg.Visible = false;
                        if (Session["COMPANYID"] != null)
                        {
                            string companyid = Session["COMPANYID"].ToString();
                            txtCompanyName.Text= dbFunctions.StringData("SELECT COMPNM FROM ASL_COMPANY WHERE COMPID='" + companyid + "'");
                            Session["CompanyIdForReport"] = companyid;
                            lblMsg.Visible = false;
                            txtCompanyName.Visible = false;
                            lblComLabel.Visible = false;
                        }
                    }
                    else
                    {
                        Response.Redirect("~/DeshBoard/UI/Default.aspx");
                    }

                }
            }
        }

        protected void txtCompanyName_TextChanged(object sender, EventArgs e)
        {
            if (Session["USERID"] == null)
            {
                Response.Redirect("~/LogIn/UI/LogIn.aspx");
            }
            else
            {
                Session["CompanyIdForReport"] = null;
                if (txtCompanyName.Text == "--SELECT--")
                {
                    lblMsg.Text = "Select Company Name";
                    lblMsg.Visible = true;
                }
                else
                {
                    string companyId =
                        dbFunctions.StringData("SELECT COMPID FROM ASL_COMPANY WHERE COMPNM='" + txtCompanyName.Text +
                                               "'");
                    if (companyId == "")
                    {
                        txtCompanyName.SelectedIndex = -1;
                        lblMsg.Text = "Select Company Name";
                        lblMsg.Visible = true;
                    }
                    else
                    {
                        Session["CompanyIdForReport"] = companyId;
                        lblMsg.Visible = false;
                    }
                }
            }
        }
    }
}