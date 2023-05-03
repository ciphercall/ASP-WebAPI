using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Data.SqlClient;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using AlchemyAccounting;
using DynamicMenu.Accounts.DataAccess;
using DynamicMenu.Accounts.Interface;
using DynamicMenu.LogData;
using DynamicMenu.Stock.DataAccess;
using DynamicMenu.Stock.Interface;

namespace DynamicMenu.Stock.UI
{
    public partial class PartyInformation : System.Web.UI.Page
    {
        StockDataAcces dob = new StockDataAcces();
        AccountDataAccess sdob = new AccountDataAccess();
        StockInterface iob = new StockInterface();
        AccountInterface siob = new AccountInterface();
        protected void Page_Load(object sender, EventArgs e)
        {
            if (Session["USERID"] == null)
                Response.Redirect("~/Login/UI/Login.aspx");
            else
            {
                const string formLink = "/Stock/UI/PartyInformation.aspx";
                bool permission = UserPermissionChecker.checkParmit(formLink, "STATUS");
                if (permission == true)
                {
                    if (!IsPostBack)
                    {

                    }
                }
                else
                {
                    Response.Redirect("../../DashBoard/UI/Default.aspx");
                }
            }
        }

        public void Refresh()
        {
            txtPartyNameEng.Text = "";
            txtPartyNameBan.Text = "";
            txtAddressBan.Text = "";
            txtAddressEng.Text = "";
            txtMobile1.Text = "";
            txtMobile2.Text = "";
            txtEmail.Text = "";
            txtRemarks.Text = "";
            txtAuthorName.Text = "";
            txtAuthorContactNo.Text = "";
            ddlStatus.SelectedIndex = -1;
            ddlPartyList.SelectedIndex = -1;
        }

        public string GeneratePartyId()
        {
            string maxPartyId = dbFunctions.StringData("SELECT MAX(PARTYID) FROM STK_PARTY WHERE COMPID='101'");
            if (maxPartyId == "")
            {
                return "20010";
            }
            else
            {
                string maxid = maxPartyId.Substring(1, 3);
                if (Convert.ToInt16(maxid) < 9)
                {
                    int id = Convert.ToInt16(maxid) + Convert.ToInt16(1);
                    return "200" + id + "0";
                }
                else if (Convert.ToInt16(maxid) < 99)
                {
                    int id = Convert.ToInt16(maxid) + Convert.ToInt16(1);
                    return "20" + id + "0";
                }
                else if (Convert.ToInt16(maxid) < 999)
                {
                    int id = Convert.ToInt16(maxid) + Convert.ToInt16(1);
                    return "2" + id + "0";
                }
            }
            return null;
        }

        public bool MobileCheck(string mobileno)
        {
            string mobile = dbFunctions.StringData("SELECT MOBNO1 FROM STK_PARTY WHERE MOBNO1='" + mobileno + "'");
            if (mobile == "")
                return true;
            return false;
        }
        public bool MobileCheckForUpdate(string mobileno, string id)
        {
            string mobile = dbFunctions.StringData(@"SELECT MOBNO1 FROM STK_PARTY WHERE 
            MOBNO1='" + mobileno + "' except SELECT MOBNO1 FROM STK_PARTY WHERE PARTYID='" + id + "'");
            if (mobile == "")
                return true;
            return false;
        }
        protected void btnSave_OnClick(object sender, EventArgs e)
        {
            if (txtPartyNameEng.Text == "")
            {
                lblMsg.Text = "Put Party Name.";
                lblMsg.Visible = true;
                txtPartyNameEng.Focus();
            }
            else if (txtMobile1.Text == "")
            {
                lblMsg.Text = "Put Mobile Number.";
                lblMsg.Visible = true;
                txtMobile1.Focus();
            }
            else if (!MobileCheck(txtMobile1.Text))
            {
                lblMsg.Text = "Put Valid Mobile Number. The number Already Used.";
                lblMsg.Visible = true;
                txtMobile1.Focus();
            }
            else
            {
                
                siob.AccCD = "1020201" + GeneratePartyId();
                siob.AccNM = txtPartyNameEng.Text;
                siob.OpDT = dbFunctions.Timezone(DateTime.Now);
                siob.LevelCD = 5;
                siob.ControlCD = "202020100000";
                siob.AccTP = "D";
                //siob.BranchCD = null;
                siob.StatusCD = "P";
                siob.Username = HttpContext.Current.Session["USERID"].ToString();
                siob.Ipaddress = HttpContext.Current.Session["IpAddress"].ToString();
                siob.Userpc = HttpContext.Current.Session["PCName"].ToString();
                siob.InTM = dbFunctions.Timezone(DateTime.Now);

                SqlConnection con = new SqlConnection(dbFunctions.connection);
                con.Open();
                SqlCommand cmdd = new SqlCommand("Select ACCOUNTCD from GL_ACCHART  where ACCOUNTCD='" + siob.AccCD + "'", con);
                SqlDataAdapter dad = new SqlDataAdapter(cmdd);
                DataSet dsd = new DataSet();
                dad.Fill(dsd);
                con.Close();
                if (dsd.Tables[0].Rows.Count > 0)
                {

                    lblMsg.Visible = true;
                    lblMsg.Text = "Already Exist in Chart of Accounts !";
                 //   txtImprtNM.Focus();
                }


                lblMsg.Text = "";
                lblMsg.Visible = false;

                iob.UserID = HttpContext.Current.Session["USERID"].ToString();
                iob.Ipaddress = HttpContext.Current.Session["IpAddress"].ToString();
                iob.UserPC = HttpContext.Current.Session["PCName"].ToString();
                iob.ITime = dbFunctions.Timezone(DateTime.Now);

                iob.CompanyId = "101";
                iob.PartyId = "1020201" + GeneratePartyId();
                iob.UserCode = (Convert.ToInt32(iob.PartyId) + 1).ToString();
                iob.Pstp = "PARTY";
                iob.PartyNameEnglish = txtPartyNameEng.Text;
                iob.PartyNameBangla = txtPartyNameBan.Text;
                iob.AddressEnglish = txtAddressEng.Text;
                iob.AddressBangla = txtAddressBan.Text;
                iob.MobNo = txtMobile1.Text;
                iob.MobNo2 = txtMobile2.Text;
                iob.Email = txtEmail.Text;
                iob.Remarks = txtRemarks.Text;
                iob.Author = txtAuthorName.Text;
                iob.ApContactNo = txtAuthorContactNo.Text;
                iob.Status = ddlStatus.SelectedValue;

                sdob.insert_GL_ACCCHART_DEPOT_DELAR_SHOPKEEPER_IMPORTER_Information(siob);

                iob.PartyId= GeneratePartyId();
                iob.UserCode = (Convert.ToInt32(iob.PartyId) + 1).ToString();

                string s = dob.InsertPartyInformation(iob);
                if (s == "")
                {
                    Response.Write("<script>alert('Save Data Successfully.')</script>");
                    Refresh();
                    txtPartyNameEng.Focus();
                }
                else
                {
                    Response.Write("<script>alert('Enternal Error occured.')</script>");
                }
            }
        }

        protected void LinkBAdd_OnClick(object sender, EventArgs e)
        {
            ddlPartyList.Visible = false;
            btnUpdate.Visible = false;
        }

        protected void updateRecord_OnClick(object sender, EventArgs e)
        {
            ddlPartyList.Visible = true;
            btnUpdate.Visible = true;
            btnSave.Visible = false;

            dbFunctions.DropDownAddSelectTextWithValue(ddlPartyList, "SELECT PARTYNM, PARTYID FROM STK_PARTY");
        }

        protected void ddlPartyList_OnSelectedIndexChanged(object sender, EventArgs e)
        {
            if (ddlPartyList.SelectedValue == "--SELECT--")
            {
                lblMsg.Text = "Select Party Name.";
                lblMsg.Visible = true;
                ddlPartyList.Focus();
                Refresh();
            }
            else
            {
                var data = dbFunctions.DataReaderAdd(@"SELECT PARTYNM, PARTYNMB, ADDRESS, ADDRESSB, MOBNO1, MOBNO2, EMAILID, 
                APNM, APCNO, REMARKS, STATUS FROM STK_PARTY WHERE PARTYID='" + ddlPartyList.SelectedValue + "'");

                txtPartyNameEng.Text = data[0];
                txtPartyNameBan.Text = data[1];
                txtAddressEng.Text = data[2];
                txtAddressBan.Text = data[3];
                txtMobile1.Text = data[4];
                txtMobile2.Text = data[5];
                txtEmail.Text = data[6];
                txtAuthorName.Text = data[7];
                txtAuthorContactNo.Text = data[8];
                txtRemarks.Text = data[9];
                ddlStatus.SelectedIndex = ddlStatus.Items.IndexOf(ddlStatus.Items.FindByValue(data[10]));
            }
        }

        protected void btnUpdate_OnClick(object sender, EventArgs e)
        {
            if (ddlPartyList.SelectedValue == "")
            {
                lblMsg.Text = "Select Party.";
                lblMsg.Visible = true;
                ddlPartyList.Focus();
            }
            if (txtPartyNameEng.Text == "")
            {
                lblMsg.Text = "Put Party Name.";
                lblMsg.Visible = true;
                txtPartyNameEng.Focus();
            }
            else if (txtMobile1.Text == "")
            {
                lblMsg.Text = "Put Mobile Number.";
                lblMsg.Visible = true;
                txtMobile1.Focus();
            }
            else if (!MobileCheckForUpdate(txtMobile1.Text, ddlPartyList.SelectedValue))
            {
                lblMsg.Text = "Put Valid Mobile Number. The number Already Used.";
                lblMsg.Visible = true;
                txtMobile1.Focus();
            }
            else
            {
                lblMsg.Text = "";
                lblMsg.Visible = false;

                iob.UserID = HttpContext.Current.Session["USERID"].ToString();
                iob.Ipaddress = HttpContext.Current.Session["IpAddress"].ToString();
                iob.UserPC = HttpContext.Current.Session["PCName"].ToString();
                iob.ITime = dbFunctions.Timezone(DateTime.Now);

                iob.CompanyId = "101";
                iob.PartyId = ddlPartyList.SelectedValue;

                iob.PartyNameEnglish = txtPartyNameEng.Text;
                iob.PartyNameBangla = txtPartyNameBan.Text;
                iob.AddressEnglish = txtAddressEng.Text;
                iob.AddressBangla = txtAddressBan.Text;
                iob.MobNo = txtMobile1.Text;
                iob.MobNo2 = txtMobile2.Text;
                iob.Email = txtEmail.Text;
                iob.Remarks = txtRemarks.Text;
                iob.Author = txtAuthorName.Text;
                iob.ApContactNo = txtAuthorContactNo.Text;
                iob.Status = ddlStatus.SelectedValue;

                try
                {
                    // logdata add start //
                    string lotileng = HttpContext.Current.Session["Location"].ToString();
                    string ipAddress = HttpContext.Current.Session["IpAddress"].ToString();
                    string logdata = dbFunctions.StringData(@"SELECT CONVERT(NVARCHAR(50),COMPID,103)+'  '+PARTYID+'  '+PARTYNM+'  '+
                    ISNULL(PARTYNMB,'(NULL)')+'  '+ISNULL(ADDRESS,'(NULL)')+'  '+ISNULL(ADDRESSB,'(NULL)')+'  '+MOBNO1+'  '+ISNULL(MOBNO2,'(NULL)')+'  '+
                    ISNULL(EMAILID,'(NULL)')+'  '+ISNULL(APNM,'(NULL)')+'  '+ISNULL(APCNO,'(NULL)')+'  '+ISNULL(REMARKS,'(NULL)')+'  '+STATUS+'  '+
                    ISNULL(USERPC,'(NULL)')+'  '+ISNULL(CONVERT(NVARCHAR(50),USERID,103),'(NULL)')+'  '+ISNULL(CONVERT(NVARCHAR(50),INTIME,103),'(NULL)')+'  '+
                    ISNULL(IPADDRESS,'(NULL)')+'  '+ISNULL(INSLTUDE,'(NULL)')+'  '+ISNULL(UPDUSERPC,'(NULL)')+'  '+
                    ISNULL(CONVERT(NVARCHAR(50),UPDINTIME,103),'(NULL)')+'  '+ISNULL(CONVERT(NVARCHAR(50),UPDUSERID,103),'(NULL)')+'  '+
                    ISNULL(UPDIPADDRESS,'(NULL)')+'  '+ISNULL(UPDLTUDE,'(NULL)') FROM STK_PARTY 
                    WHERE  COMPID='" + iob.CompanyId + "' AND PARTYID='" + iob.PartyId + "'");
                    string logid = "UPDATE";
                    string tableid = "STK_PARTY";
                    LogDataFunction.InsertLogData(lotileng, logid, tableid, logdata, ipAddress);
                    // logdata add end //
                }
                catch (Exception)
                {

                }
                Label lblAccCD=new Label();
                dbFunctions.lblAdd("SELECT ACCOUNTCD FROM GL_ACCHART WHERE ACCOUNTNM='" + ddlPartyList.SelectedItem + "'", lblAccCD);
                 siob.AccCD = lblAccCD.Text;
                siob.AccNM = txtPartyNameEng.Text;
                siob.OpDT = dbFunctions.Timezone(DateTime.Now);
                siob.LevelCD = 5;
                siob.ControlCD = "202020100000";
                siob.AccTP = "D";
                //siob.BranchCD = null;
                siob.StatusCD = "P";


                siob.Username = HttpContext.Current.Session["USERID"].ToString();
                siob.Ipaddress = HttpContext.Current.Session["IpAddress"].ToString();
                siob.Userpc = HttpContext.Current.Session["PCName"].ToString();
                siob.InTM = dbFunctions.Timezone(DateTime.Now);

                sdob.update_GL_ACCCHART_DEPOT_DELAR_SHOPKEEPER_IMPORTER_Information(siob);

                string s = dob.UpdatePartyInformation(iob);
                if (s == "")
                {
                    Response.Write("<script>alert('Data Updated Successfully.')</script>");
                    Refresh();
                    ddlPartyList.Focus();
                }
                else
                {
                    Response.Write("<script>alert('Enternal Error occured.')</script>");
                }
            }
        }
    }
}