using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Drawing;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using AlchemyAccounting;
using DynamicMenu.LogData;

namespace DynamicMenu.ASLCompany.UI
{
    public partial class CompanyCreation : System.Web.UI.Page
    {
        IFormatProvider _dateformat = new System.Globalization.CultureInfo("fr-FR", true);

        DataAccess.ASLDataAccess dob = new DataAccess.ASLDataAccess();
        Interface.ASLInterface iob = new Interface.ASLInterface();
        SqlConnection con=new SqlConnection(dbFunctions.connection);
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                if (Session["USERID"] == null && Session["USERTYPE"].ToString() == "SUPERADMIN")
                {
                    Response.Redirect("~/LogIn/UI/LogIn.aspx");
                }
                else
                {
                    txtComName.Focus();
                }
            }
        }

        public Int64 MaximumCompanyId()
        {
            Int64 maxCompanyId = 0;
            string maxId = dbFunctions.StringData("SELECT MAX(COMPID) FROM ASL_COMPANY");
            maxCompanyId = Convert.ToInt64(maxId);
            maxCompanyId++;

            return maxCompanyId;
        }

        public string FieldCheck()
        {
            var txtLotiLongTude = (TextBox)Master.FindControl("txtLotiLongTude");
            string checkResult = "";
            if (txtComName.Text == "")
            {
                checkResult = "Please fill Company Name field.";
                txtComName.Focus();
            }
            else if (txtAddress.Text == "")
            {
                checkResult = "Please fill Address field.";
                txtAddress.Focus();
            }
            else if (txtContactNo.Text == "")
            {
                checkResult = "Please fill Contact no field.";
                txtContactNo.Focus();
            }
            else if (txtLotiLongTude.Text == "")
            {
                Response.Redirect(Request.RawUrl);
                checkResult = "Location not Found.";
            }
            else
            {
                checkResult = "true";
            }

            return checkResult;
        }

        protected void btnSubmit_Click(object sender, EventArgs e)
        {
                 
            if (FieldCheck() == "true")
            {
                var txtLotiLongTude = (TextBox)Master.FindControl("txtLotiLongTude");
                var txtIp = (TextBox)Master.FindControl("txtIp");
                iob.LotiLengTudeInsert = txtLotiLongTude.Text;
                //iob.IpAddressInsert =dbFunctions.IpAddress();
                iob.IpAddressInsert = txtIp.Text;
                iob.UserIdInsert = Convert.ToInt64(Session["USERID"].ToString());
                iob.UserPcInsert =dbFunctions.UserPc();
                iob.InTimeInsert = dbFunctions.Timezone(DateTime.Now);

                iob.CompanyId = MaximumCompanyId();
                iob.ComapanyName = txtComName.Text;
                iob.Address = txtAddress.Text;
                iob.ContactNo = txtContactNo.Text;
                iob.EmailId = txtEmailId.Text;
                iob.WebId = txtWebsiteId.Text;
                iob.Status = ddlStatus.SelectedValue;

                string s= dob.INSERT_ASL_COMPANY(iob);
                if(s=="")
                {

                    // logdata add start //
                    string lotileng = iob.LotiLengTudeInsert;
                    string logdata = @"Company Id: "+iob.CompanyId+", Company Name: "+iob.ComapanyName+", Address: "+
                        iob.Address+", Contact No: "+iob.ContactNo+", Email Id: "+iob.EmailId+", WebId: "+iob.WebId+
                        ", Status: "+iob.Status;
                    string logid = "INSERT";
                    string tableid = "ASL_COMPANY";
                    LogDataFunction.InsertLogData(lotileng, logid, tableid, logdata, txtIp.Text);
                    // logdata add start //

                    Refresh();
                    lblMsg.Text = "Company Succesfully Create";
                    lblMsg.Visible = true;
                    lblMsg.ForeColor = Color.Green;
                    Session["CreateCompany"] = iob.CompanyId;
                    Response.Redirect("~/ASLCompany/UI/UserCreate.aspx");
                }
                else
                {
                    lblMsg.Text = "Company does not create.";
                    lblMsg.Visible = true;
                }
                
            }
            else
            {
                lblMsg.Text = FieldCheck();
                lblMsg.Visible = true;
            }
        }

        public void Refresh()
        {
            txtComName.Text = "";
            txtAddress.Text = "";
            txtEmailId.Text = "";
            txtContactNo.Text = "";
            txtWebsiteId.Text = "";
            ddlStatus.SelectedIndex = -1;
            txtComName.Focus();
        }
    }
}