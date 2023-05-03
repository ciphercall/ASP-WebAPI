using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data;
using System.Configuration;
using AlchemyAccounting;
using System.Data.SqlClient;
using System.Web.Services;
using System.Text;
using System.Collections.Specialized;
using DynamicMenu.ASLCompany.DataAccess;
using DynamicMenu.ASLCompany.Interface;
using DynamicMenu.LogData;

namespace DynamicMenu.ASLCompany.UI
{
    public partial class UserCreate : System.Web.UI.Page
    {
        IFormatProvider dateformat = new System.Globalization.CultureInfo("fr-FR", true);

        DataAccess.ASLDataAccess dob = new DataAccess.ASLDataAccess();
        Interface.ASLInterface iob = new Interface.ASLInterface();
        SqlConnection con = new SqlConnection(dbFunctions.connection);
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
                    if (Session["CreateCompany"] != null)
                    {
                        lblCompanyId.Text = Session["CreateCompany"].ToString();
                        dbFunctions.txtAdd("SELECT COMPNM FROM ASL_COMPANY WHERE COMPID='" + lblCompanyId.Text + "'", txtCompanyName);
                        TextBoxFillByCompanyId();
                    }
                    if (Session["USERTYPE"].ToString() == "COMPADMIN")
                    {
                        ListItem removeItem = ddlOpType.Items.FindByValue("COMPADMIN");
                        ddlOpType.Items.Remove(removeItem);
                        lblCompanyId.Text = Session["COMPANYID"].ToString();
                        dbFunctions.txtAdd("SELECT COMPNM FROM ASL_COMPANY WHERE COMPID='" + lblCompanyId.Text + "'", txtCompanyName);
                        txtCompanyName.ReadOnly = true;
                        txtlogInId.ReadOnly = false;
                        dbFunctions.DropDownAddTextWithValue(ddlBranch, "SELECT BRANCHNM, BRANCHCD FROM ASL_BRANCH WHERE COMPID='" + lblCompanyId.Text + "' ORDER BY BRANCHNM");
                        ddlBranch.Focus();
                        //TextBoxFillByCompanyId();
                    }
                    if (Session["USERTYPE"].ToString() == "USERADMIN")
                    {
                        dbFunctions.DropDownAddTextWithValue(ddlBranch, "SELECT BRANCHNM, BRANCHCD FROM ASL_BRANCH WHERE COMPID='" + lblCompanyId.Text + "' ORDER BY BRANCHNM");
                        ListItem removeItem = ddlOpType.Items.FindByValue("COMPADMIN");
                        ddlOpType.Items.Remove(removeItem);
                        txtCompanyName.Focus();
                    }
                    ddlUserName.Visible = false;
                }
            }
        }


        protected void txtCompanyName_TextChanged(object sender, EventArgs e)
        {
            if (txtCompanyName.Text == "")
            {
                lblMsg.Text = "Select Compamy Name.";
                lblMsg.Visible = true;
                Refresh();
            }
            else
            {
                lblCompanyId.Text = "";
                dbFunctions.lblAdd("SELECT COMPID FROM ASL_COMPANY WHERE COMPNM='" + txtCompanyName.Text + "'", lblCompanyId);

                if (lblCompanyId.Text == "")
                {
                    txtCompanyName.Text = "";
                    txtCompanyName.Focus();
                    Refresh();
                }
                else
                {
                    TextBoxFillByCompanyId();
                    dbFunctions.dropDownAddWithSelect(ddlBranch, "SELECT BRANCHNM FROM ASL_BRANCH WHERE COMPID='" + lblCompanyId.Text + "' ORDER BY BRANCHNM ");
                    ddlBranch.Focus();
                }
            }
        }

        public void TextBoxFillByCompanyId()
        {
            if (lblCompanyId.Text != "")
            {
                dbFunctions.txtAdd("SELECT ADDRESS FROM ASL_COMPANY WHERE COMPID='" + lblCompanyId.Text + "'", txtUserAdd);
                dbFunctions.txtAdd("SELECT CONTACTNO FROM ASL_COMPANY WHERE COMPID='" + lblCompanyId.Text + "'", txtMobileNo);
                dbFunctions.txtAdd("SELECT EMAILID FROM ASL_COMPANY WHERE COMPID='" + lblCompanyId.Text + "'", txtEmailId);
            }
        }

        public string UserIdGenarate()
        {
            string maximumId = "";
            string companyId = lblCompanyId.Text;
            string chekcMaxId = dbFunctions.StringData("SELECT MAX(USERID) AS USERID FROM ASL_USERCO WHERE COMPID='" + companyId + "'");
            if (chekcMaxId == "")
            {
                maximumId = companyId + "01";
            }
            else
            {
                int userid = Convert.ToInt16(chekcMaxId.Substring(3, 2));
                userid++;
                if (userid < 10)
                {
                    maximumId = companyId + "0" + userid;
                }
                else
                {
                    maximumId = companyId + userid;
                }
            }

            return maximumId;
        }

        protected void ddlLogInBy_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (ddlLogInBy.SelectedValue == "EMAIL")
                txtlogInId.Text = txtEmailId.Text;
            else if (ddlLogInBy.SelectedValue == "MOBNO")
                txtlogInId.Text = txtMobileNo.Text;
            else
            {
                ddlStatus.SelectedIndex = -1;
                txtlogInId.Text = "";
            }
            if (txtlogInId.Text == "")
            {
                txtlogInId.ReadOnly = false;
                txtlogInId.Focus();
            }
            else
            {
                txtlogInId.ReadOnly = true;
                txtPassword.Focus();
            }
        }

        public void Refresh()
        {
            txtUserName.Text = "";
            txtUserAdd.Text = "";
            txtDepartmentName.Text = "";
            ddlOpType.SelectedIndex = -1;
            txtMobileNo.Text = "";
            txtEmailId.Text = "";
            ddlLogInBy.SelectedIndex = -1;
            txtlogInId.Text = "";
            txtPassword.Text = "";
            ddlStatus.Text = "";
            txtStartTime.Text = "10:00";
            txtEndTime.Text = "18:00";
            txtlogInId.ReadOnly = true;
            txtUserName.Focus();
        }

        protected void btnSubmit_Click(object sender, EventArgs e)
        {
            try
            {
                if (FieldCheck() == "true")
                {
                    var txtIp = (TextBox)Master.FindControl("txtIp");
                    var txtLotiLongTude = (TextBox)Master.FindControl("txtLotiLongTude");
                    iob.IpAddressInsert = txtIp.Text;
                    iob.UserIdInsert = Convert.ToInt64(Session["USERID"].ToString());
                    iob.UserPcInsert = dbFunctions.UserPc();
                    iob.LotiLengTudeInsert = txtLotiLongTude.Text;
                    iob.InTimeInsert = dbFunctions.Timezone(DateTime.Now);

                    iob.CompanyId = Convert.ToInt64(lblCompanyId.Text);
                    iob.CompanyUserId = Convert.ToInt64(UserIdGenarate());
                    iob.UserName = txtUserName.Text.Trim();
                    iob.DepartmentName = txtDepartmentName.Text;
                    iob.OpType = ddlOpType.SelectedValue;
                    iob.Address = txtUserAdd.Text;
                    iob.MobileNo = txtMobileNo.Text.Trim();
                    iob.EmailId = txtEmailId.Text.Trim();
                    iob.LogInBy = ddlLogInBy.SelectedValue;
                    iob.LogInId = txtlogInId.Text.Trim();
                    iob.Password = dbFunctions.Encrypt(txtPassword.Text.Trim());
                    iob.TimeFrom = txtStartTime.Text.Trim();
                    iob.TimeTo = txtEndTime.Text.Trim();
                    iob.Status = ddlStatus.SelectedValue;
                    iob.BranchCode = Convert.ToInt64(ddlBranch.SelectedValue);

                    if (Session["USERID"] == null)
                    {
                        Response.Redirect("~/LogIn/UI/LogIn.aspx");
                    }
                    else
                    {
                        string s = dob.INSERT_ASL_USERCO(iob);
                        if (s == "")
                        {
                            // logdata add start //
                            string lotileng = iob.LotiLengTudeInsert;
                            string logdata = @"Company Id: " + iob.CompanyId + ", User Id: " + iob.CompanyUserId + ", User Name: " +
                            iob.UserName + ", Department Name: " + iob.DepartmentName + ", Operation Type: " + iob.OpType +
                            ", Address: " + iob.Address + ", Mobile No: " + iob.MobileNo + ", Email Id: " + iob.EmailId +
                            ", LogIn By: " + iob.LogInBy + ", LogIn Id: " + iob.LogInId + ", Password: " + iob.Password +
                            ", Time From: " + iob.TimeFrom + ", Time To: " + iob.TimeTo + ", Status: " + iob.Status;
                            string logid = "INSERT";
                            string tableid = "ASL_USERCO";
                            LogDataFunction.InsertLogData(lotileng, logid, tableid, logdata, txtIp.Text);
                            // logdata add start //

                            con.Open();
                            string query = "";
                            if (Session["USERTYPE"].ToString() == "SUPERADMIN")
                                query = "SELECT MODULEID, MENUTP, MENUID FROM ASL_MENU WHERE MODULEID > '02'";
                            else
                                query = @"SELECT     ASL_MENU.MODULEID, ASL_MENU.MENUTP, ASL_MENU.MENUID, ASL_ROLE.STATUS
                                FROM         ASL_ROLE INNER JOIN
                                  ASL_MENU ON ASL_ROLE.MODULEID = ASL_MENU.MODULEID AND ASL_ROLE.MENUID = ASL_MENU.MENUID AND ASL_ROLE.MENUTP = ASL_MENU.MENUTP INNER JOIN
                                  ASL_MENUMST ON ASL_ROLE.MODULEID = ASL_MENUMST.MODULEID AND ASL_MENU.MODULEID = ASL_MENUMST.MODULEID INNER JOIN
                                  ASL_USERCO ON ASL_ROLE.COMPID = ASL_USERCO.COMPID AND ASL_ROLE.USERID = ASL_USERCO.USERID
                                  WHERE ASL_ROLE.USERID='" + iob.CompanyId + "01' AND ASL_ROLE.STATUS='A'";
                            SqlCommand cmd = new SqlCommand(query, con);
                            SqlDataReader dr = cmd.ExecuteReader();
                            foreach (var item in dr)
                            {
                                iob.ModuleId = dr["MODULEID"].ToString();
                                iob.MenuType = dr["MENUTP"].ToString();
                                iob.MenuId = dr["MENUID"].ToString();
                                if (iob.OpType == "COMPADMIN")
                                {
                                    iob.Status = "A";
                                    iob.InsertRole = "A";
                                    iob.UpdateRole = "A";
                                    iob.DeleteRole = "A";
                                }
                                else
                                {
                                    iob.Status = "I";
                                    iob.InsertRole = "I";
                                    iob.UpdateRole = "I";
                                    iob.DeleteRole = "I";
                                }

                                dob.INSERT_ASL_ROLE(iob);
                            }
                            Refresh();
                            lblMsg.Text = "User create Succefully.";
                            lblMsg.Visible = true;
                            lblMsg.ForeColor = Color.Green;
                        }
                    }
                }
                else
                {
                    lblMsg.Text = FieldCheck();
                    lblMsg.Visible = true;
                }
            }
            catch (Exception)
            {

            }
        }

        public string FieldCheck()
        {
            string checkResult = "false";

            if (txtCompanyName.Text == "")
            {
                txtCompanyName.Text = "";
                checkResult = "Select Company Name.";
                txtCompanyName.Focus();
                Refresh();
            }
            else if (ddlBranch.Text == "--SELECT--" && lblBranchCd.Text == "")
            {
                checkResult = "Select branch name.";
                ddlBranch.Focus();
            }
            else if (txtUserName.Text == "")
            {
                txtUserName.Text = "";
                checkResult = "User name is empty.";
                txtUserName.Focus();
            }
            else if (txtUserAdd.Text == "")
            {
                txtUserAdd.Text = "";
                checkResult = "User adress field is empty.";
                txtUserAdd.Focus();
            }
            else if (txtDepartmentName.Text == "")
            {
                txtDepartmentName.Text = "";
                checkResult = "Department name is empty.";
                txtDepartmentName.Focus();
            }
            else if (txtMobileNo.Text == "")
            {
                txtMobileNo.Text = "";
                checkResult = "Mobile no field is empty.";
                txtMobileNo.Focus();
            }
            else if (MobileCheck() == "false")
            {
                checkResult = "Mobile number already used.";
                txtMobileNo.Focus();
            }
            else if (txtEmailId.Text == "")
            {
                txtEmailId.Text = "";
                checkResult = "EmailId field is empty.";
                txtEmailId.Focus();
            }
            else if (EmailCheck() == "false")
            {
                checkResult = "Email already exist.";
                txtEmailId.Focus();
            }
            else if (ddlLogInBy.Text == "--Select Login By--")
            {
                ddlLogInBy.SelectedIndex = -1;
                checkResult = "Select login by.";
                ddlLogInBy.Focus();
            }
            else if (txtlogInId.Text == "")
            {
                ddlLogInBy.SelectedIndex = -1;
                checkResult = "Select login by.";
                ddlLogInBy.Focus();
            }
            else if (LogInByCheck() == "")
            {
                checkResult = "Login Id already used.";
                txtlogInId.Focus();
                txtlogInId.ReadOnly = false;
            }
            else if (txtPassword.Text == "")
            {
                txtPassword.Text = "";
                checkResult = "Password field is empty.";
                txtPassword.Focus();
            }
            else if (ddlStatus.Text == "--Select Status--")
            {
                ddlStatus.SelectedIndex = -1;
                checkResult = "Select Status.";
                ddlStatus.Focus();
            }
            else
            {
                checkResult = "true";
                txtlogInId.ReadOnly = true;
            }
            return checkResult;
        }

        public string EmailCheck()
        {
            string checkResult = "false";
            string email = dbFunctions.StringData("SELECT EMAILID FROM ASL_USERCO WHERE EMAILID='" + txtEmailId.Text + "'");
            if (email == "")
                checkResult = "true";
            return checkResult;
        }
        public string MobileCheck()
        {
            string checkResult = "false";
            string mobile = dbFunctions.StringData("SELECT MOBNO FROM ASL_USERCO WHERE MOBNO='" + txtMobileNo.Text + "'");
            if (mobile == "")
                checkResult = "true";
            return checkResult;
        }
        public string LogInByCheck()
        {
            string checkResult = "false";
            string loginBy = dbFunctions.StringData("SELECT LOGINBY FROM ASL_USERCO WHERE LOGINBY='" + txtlogInId.Text + "'");
            if (loginBy == "")
                checkResult = "true";
            return checkResult;
        }

        protected void ddlBranch_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (ddlBranch.Text == "--SELECT--")
            {
                ddlBranch.Focus();
                lblMsg.Text = "Select branch name.";
                lblMsg.Focus();
            }
            else
            {
                dbFunctions.lblAdd("SELECT BRANCHCD FROM ASL_BRANCH WHERE BRANCHNM ='" + ddlBranch.Text + "' AND COMPID='" + lblCompanyId.Text + "'", lblBranchCd);
                txtUserName.Focus();
            }
        }

        protected void LinkBAdd_Click(object sender, EventArgs e)
        {
            txtUserName.Visible = true;
            ddlUserName.Visible = false;
            btnSubmit.Visible = true;
            btnUpdate.Visible = false;
            ddlBranch.Focus();
            Refresh();
        }

        protected void linkBEdit_Click(object sender, EventArgs e)
        {
            txtUserName.Visible = false;
            ddlUserName.Visible = true;
            btnSubmit.Visible = false;
            btnUpdate.Visible = true;
            dbFunctions.DropDownAddSelectTextWithValue(ddlUserName, "SELECT USERNM,USERID FROM ASL_USERCO WHERE COMPID='101' AND USERID!='10101'");
            ddlUserName.Focus();
        }

        protected void ddlUserName_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (ddlUserName.Text == "--SELECT--")
            {
                lblMsg.Text = "";
                lblMsg.Visible = true;
                Refresh();
            }
            else
            {
                lblMsg.Visible = false;
                con.Open();
                SqlCommand cmdCommand = new SqlCommand("SELECT * FROM ASL_USERCO WHERE USERID='" + ddlUserName.SelectedValue + "'", con);
                SqlDataReader dr = cmdCommand.ExecuteReader();
                while (dr.Read())
                {
                    txtUserAdd.Text = dr["ADDRESS"].ToString();
                    txtDepartmentName.Text = dr["DEPTNM"].ToString();
                    ddlOpType.SelectedValue = dr["OPTP"].ToString();
                    txtMobileNo.Text = dr["MOBNO"].ToString();
                    txtEmailId.Text = dr["EMAILID"].ToString();
                    ddlLogInBy.SelectedValue = dr["LOGINBY"].ToString();
                    txtlogInId.Text = dr["LOGINID"].ToString();
                    txtPassword.Text = dbFunctions.Decrypt(dr["LOGINPW"].ToString());
                    ddlStatus.Text = dr["STATUS"].ToString();
                    txtStartTime.Text = dr["TIMEFR"].ToString();
                    txtEndTime.Text = dr["TIMETO"].ToString();
                    ddlBranch.SelectedValue = dr["BRANCHCD"].ToString();
                }
                dr.Close();
                con.Close();
                txtlogInId.ReadOnly = true;
                // txtEmailId.ReadOnly = false;
                txtPassword.ReadOnly = true;
                //  ddlLogInBy.Enabled = true;
            }
        }

        private bool FeildCheck(string fieldName, string fieldData, string userid)
        {
            var check = dbFunctions.StringData($@"SELECT {fieldName} FROM ASL_USERCO WHERE {fieldName}='{fieldData}' AND USERID <> '{userid}'");
            if (check == "")
                return true;
            return false;
        }

        protected void btnUpdate_Click(object sender, EventArgs e)
        {
            try
            {
                if (FieldCheckForUpdate() == "true")
                {
                    lblMsg.Text = "";
                    var txtIp = (TextBox)Master.FindControl("txtIp");
                    var txtLotiLongTude = (TextBox)Master.FindControl("txtLotiLongTude");
                    iob.IpAddressInsert = txtIp.Text;
                    iob.UserIdInsert = Convert.ToInt64(Session["USERID"].ToString());
                    iob.UserPcInsert = dbFunctions.UserPc();
                    iob.LotiLengTudeInsert = txtLotiLongTude.Text;
                    iob.InTimeInsert = dbFunctions.Timezone(DateTime.Now);

                    iob.CompanyId = Convert.ToInt64(lblCompanyId.Text);
                    iob.CompanyUserId = Convert.ToInt64(ddlUserName.SelectedValue);
                    //iob.UserName = txtUserName.Text.Trim();
                    iob.DepartmentName = txtDepartmentName.Text;
                    iob.OpType = ddlOpType.SelectedValue;
                    iob.Address = txtUserAdd.Text;
                    iob.MobileNo = txtMobileNo.Text.Trim();
                    iob.EmailId = txtEmailId.Text.Trim();
                    iob.LogInBy = ddlLogInBy.SelectedValue;
                    iob.LogInId = txtlogInId.Text.Trim();
                    iob.TimeFrom = txtStartTime.Text.Trim();
                    iob.TimeTo = txtEndTime.Text.Trim();
                    iob.Status = ddlStatus.SelectedValue;
                    iob.BranchCode = Convert.ToInt64(ddlBranch.SelectedValue);

                    if (Session["USERID"] == null)
                    {
                        Response.Redirect("~/LogIn/UI/LogIn.aspx");
                    }
                    else
                    {
                        string s = dob.UPDATE_ASL_USERCO(iob);
                        ddlUserName.SelectedIndex = -1;
                        Refresh();
                        ddlUserName.Focus();
                    }
                }
                else
                {
                    lblMsg.Text = FieldCheckForUpdate();
                    lblMsg.Visible = true;

                }
            }
            catch (Exception)
            {

            }
        }

        public string FieldCheckForUpdate()
        {
            string checkResult = "false";

            if (txtCompanyName.Text == "")
            {
                txtCompanyName.Text = "";
                checkResult = "Select Company Name.";
                txtCompanyName.Focus();
                Refresh();
            }
            else if (ddlBranch.Text == "--SELECT--" && lblBranchCd.Text == "")
            {
                checkResult = "Select branch name.";
                ddlBranch.Focus();
            }
            else if (ddlUserName.Text == "--SELECT--")
            {
                ddlUserName.SelectedIndex = -1;
                checkResult = "User name is empty.";
                ddlUserName.Focus();
            }
            else if (txtUserAdd.Text == "")
            {
                txtUserAdd.Text = "";
                checkResult = "User adress field is empty.";
                txtUserAdd.Focus();
            }
            else if (txtDepartmentName.Text == "")
            {
                txtDepartmentName.Text = "";
                checkResult = "Department name is empty.";
                txtDepartmentName.Focus();
            }
            else if (txtMobileNo.Text == "")
            {
                txtMobileNo.Text = "";
                checkResult = "Mobile no field is empty.";
                txtMobileNo.Focus();
            }
            else if (txtEmailId.Text == "")
            {
                txtEmailId.Text = "";
                checkResult = "EmailId field is empty.";
                txtEmailId.Focus();
            }
            else if (ddlLogInBy.Text == "--Select Login By--")
            {
                ddlLogInBy.SelectedIndex = -1;
                checkResult = "Select login by.";
                ddlLogInBy.Focus();
            }
            else if (txtlogInId.Text == "")
            {
                ddlLogInBy.SelectedIndex = -1;
                checkResult = "Select login by.";
                ddlLogInBy.Focus();
            }
            else if (ddlStatus.Text == "--Select Status--")
            {
                ddlStatus.SelectedIndex = -1;
                checkResult = "Select Status.";
                ddlStatus.Focus();
            }
            else if (!FeildCheck("MOBNO", txtMobileNo.Text, ddlUserName.SelectedValue))
            {
                txtMobileNo.Text = "";
                checkResult = "Mobile no not be duplicate.";
                txtMobileNo.Focus();
            }
            else if (!FeildCheck("EMAILID", txtEmailId.Text, ddlUserName.SelectedValue))
            {
                txtEmailId.Text = "";
                checkResult = "Enmail id not be duplicate.";
                txtEmailId.Focus();
            }
            else if (!FeildCheck("LOGINID", txtlogInId.Text, ddlUserName.SelectedValue))
            {
                checkResult = "Login id not be duplicate.";
                ddlLogInBy.Focus();
            }
            else
            {
                checkResult = "true";
                txtlogInId.ReadOnly = true;
            }
            return checkResult;
        }
    }
}