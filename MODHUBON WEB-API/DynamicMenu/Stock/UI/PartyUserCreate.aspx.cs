using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using AlchemyAccounting;
using DynamicMenu.LogData;
using DynamicMenu.Stock.DataAccess;
using DynamicMenu.Stock.Interface;

namespace DynamicMenu.Stock.UI
{
    public partial class PartyUserCreate : System.Web.UI.Page
    {
        StockDataAcces dob = new StockDataAcces();
        StockInterface iob = new StockInterface();

        IFormatProvider dateformat = new System.Globalization.CultureInfo("fr-FR", true);
        SqlConnection con = new SqlConnection(dbFunctions.connection);
        protected void Page_Load(object sender, EventArgs e)
        {
            if (Session["USERID"] == null)
                Response.Redirect("~/Login/UI/Login.aspx");
            else
            {
                const string formLink = "/Stock/UI/PartyUserCreate.aspx";
                bool permission = UserPermissionChecker.checkParmit(formLink, "STATUS");
                if (permission)
                {
                    if (!IsPostBack)
                    {
                        dbFunctions.DropDownAddSelectTextWithValue(ddlPartyList, "SELECT PARTYNM, PARTYID FROM STK_PARTY");
                        ddlPartyList.Focus();
                    }
                }
                else
                {
                    Response.Redirect("../../DashBoard/UI/Default.aspx");
                }
            }
        }
        protected void ddlPartyList_OnSelectedIndexChanged(object sender, EventArgs e)
        {
            if (ddlPartyList.SelectedValue == "--SELECT--" || ddlPartyList.SelectedValue == "")
            {
                lblErr.Text = "Select Party Name.";
                lblErr.Visible = true;
            }
            else
            {
                lblErr.Visible = false;
                GridShow();
            }
        }
        protected void GridShow()
        {

            SqlConnection conn = new SqlConnection(dbFunctions.connection);
            conn.Open();
            SqlCommand cmd = new SqlCommand(@"SELECT COMPID, PSTP, PSID, USERCD, USERNM, MOBNO1, MOBNO2, LOGINID, LOGINPW, STATUS
            FROM STK_USERPS WHERE PSID='" + ddlPartyList.SelectedValue + "'", conn);
            SqlDataAdapter da = new SqlDataAdapter(cmd);
            DataSet ds = new DataSet();
            da.Fill(ds);
            conn.Close();
            if (ds.Tables[0].Rows.Count > 0)
            {
                gvDetails.DataSource = ds;
                gvDetails.DataBind();
                lblGridMSG.Visible = false;
                TextBox txtUserName = (TextBox)gvDetails.FooterRow.FindControl("txtUserName");
                txtUserName.Focus();
            }

            else
            {
                ds.Tables[0].Rows.Add(ds.Tables[0].NewRow());
                gvDetails.DataSource = ds;
                gvDetails.DataBind();
                int columncount = gvDetails.Rows[0].Cells.Count;
                gvDetails.Rows[0].Cells.Clear();
                gvDetails.Rows[0].Cells.Add(new TableCell());
                gvDetails.Rows[0].Cells[0].ColumnSpan = columncount;
                gvDetails.Rows[0].Cells[0].Text = "No Records Found";
                lblGridMSG.Visible = false;
                TextBox txtUserName = (TextBox)gvDetails.FooterRow.FindControl("txtUserName");
                txtUserName.Focus();
            }
            var countid = dbFunctions.StringData(@"SELECT count(USERCD) FROM STK_USERPS  
            WHERE PSID='" + ddlPartyList.SelectedValue + "' AND COMPID='101' AND PSTP='PARTY'");
            if (Convert.ToInt16(countid) >= 9)
            {
                gvDetails.FooterRow.Visible = false;
            }
        }

        private string GenerateId()
        {
            var maxid = dbFunctions.StringData(@"SELECT CAST(MAX(USERCD) AS INT)+1 FROM STK_USERPS 
            WHERE PSID='" + ddlPartyList.SelectedValue + "' AND COMPID='101' AND PSTP='PARTY'");
            return maxid;
        }
        public bool MobileCheck(string mobileno, string fieldname)
        {
            string mobile = dbFunctions.StringData(@"SELECT " + fieldname + " FROM STK_USERPS WHERE " + fieldname + "='" + mobileno + "'");
            if (mobile == "")
                return true;
            return false;
        }
        public bool MobileCheckForUpdate(string mobileno, string fieldname, string usercode)
        {
            string mobile = dbFunctions.StringData(@"SELECT " + fieldname + " FROM STK_USERPS WHERE " + fieldname + 
                "='" + mobileno + "' except SELECT " + fieldname + " FROM STK_USERPS WHERE USERCD='" + usercode + "'");
            if (mobile == "")
                return true;
            return false;
        }
        protected void gvDetails_RowCommand(object sender, GridViewCommandEventArgs e)
        {
            iob.UserID = HttpContext.Current.Session["USERID"].ToString();
            iob.Ipaddress = HttpContext.Current.Session["IpAddress"].ToString();
            iob.UserPC = HttpContext.Current.Session["PCName"].ToString();
            iob.ITime = dbFunctions.Timezone(DateTime.Now);

            var txtUserName = (TextBox)gvDetails.FooterRow.FindControl("txtUserName");
            var txtMobile1 = (TextBox)gvDetails.FooterRow.FindControl("txtMobile1");
            var txtMobile2 = (TextBox)gvDetails.FooterRow.FindControl("txtMobile2");
            var txtLoginId = (TextBox)gvDetails.FooterRow.FindControl("txtLoginId");
            var txtPassword = (TextBox)gvDetails.FooterRow.FindControl("txtPassword");
            var ddlStatus = (DropDownList)gvDetails.FooterRow.FindControl("ddlStatus");

            if (e.CommandName.Equals("AddNew"))
            {
                if (txtUserName.Text == "")
                {
                    lblGridMSG.Visible = true;
                    lblGridMSG.Text = "User Name Required .";
                    txtUserName.Focus();
                }
                else if (txtMobile1.Text == "")
                {
                    lblGridMSG.Visible = true;
                    lblGridMSG.Text = "Mobile No Required .";
                    txtMobile1.Focus();
                }
                else if (!MobileCheck(txtMobile1.Text, "MOBNO1"))
                {
                    lblGridMSG.Visible = true;
                    lblGridMSG.Text = "Mobile No Duplicate.";
                    txtMobile1.Focus();
                }
                else if (txtLoginId.Text == "")
                {
                    lblGridMSG.Visible = true;
                    lblGridMSG.Text = "Login Id Required .";
                    txtLoginId.Focus();
                }
                else if (!MobileCheck(txtLoginId.Text, "LOGINID"))
                {
                    lblGridMSG.Visible = true;
                    lblGridMSG.Text = "Login  Id Duplicate.";
                    txtLoginId.Focus();
                }
                else if (txtPassword.Text == "")
                {
                    lblGridMSG.Visible = true;
                    lblGridMSG.Text = "Password Required .";
                    txtPassword.Focus();
                }
                else
                {
                    if (Session["USERID"] == null)
                    {
                        Response.Redirect("~/Login/UI/Login.aspx");
                    }
                    else
                    {
                        lblGridMSG.Visible = false;

                        iob.CompanyId = "101";
                        iob.PartyId = ddlPartyList.SelectedValue;

                        iob.UserCode = GenerateId();
                        iob.Pstp = "PARTY";
                        iob.PartyNameEnglish = txtUserName.Text;
                        iob.MobNo = txtMobile1.Text;
                        iob.MobNo2 = txtMobile2.Text;
                        iob.LoginId = txtLoginId.Text;
                        iob.Password = txtPassword.Text;
                        iob.Status = ddlStatus.SelectedValue;

                        string s = dob.InsertPartyUserLogin(iob);
                        if (s == "")
                        {
                            GridShow();
                            txtUserName.Focus();
                        }
                        else
                        {
                            lblGridMSG.Visible = true;
                            lblGridMSG.Text = "Internal Error Occured.";
                        }
                    }
                }
            }

        }
        protected void gvDetails_RowDataBound(object sender, GridViewRowEventArgs e)
        {
            if (e.Row.RowType == DataControlRowType.Footer)
            {

            }
        }

        protected void gvDetails_RowEditing(object sender, GridViewEditEventArgs e)
        {

            if (Session["USERID"] == null)
            {
                Response.Redirect("~/Login/UI/Login.aspx");
            }
            else
            {
                gvDetails.EditIndex = e.NewEditIndex;
                GridShow();
                TextBox txtUserNameEdit = (TextBox)gvDetails.Rows[e.NewEditIndex].FindControl("txtUserNameEdit");
                txtUserNameEdit.Focus();
            }
        }

        protected void gvDetails_RowUpdating(object sender, GridViewUpdateEventArgs e)
        {
            Label lblPsIdEdit = (Label)gvDetails.Rows[e.RowIndex].FindControl("lblPsIdEdit");
            Label lblUserCodeEdit = (Label)gvDetails.Rows[e.RowIndex].FindControl("lblUserCodeEdit");

            TextBox txtUserNameEdit = (TextBox)gvDetails.Rows[e.RowIndex].FindControl("txtUserNameEdit");
            TextBox txtMobile1Edit = (TextBox)gvDetails.Rows[e.RowIndex].FindControl("txtMobile1Edit");
            TextBox txtMobile2Edit = (TextBox)gvDetails.Rows[e.RowIndex].FindControl("txtMobile2Edit");
            TextBox txtLoginIdEdit = (TextBox)gvDetails.Rows[e.RowIndex].FindControl("txtLoginIdEdit");
            TextBox txtPasswordEdit = (TextBox)gvDetails.Rows[e.RowIndex].FindControl("txtPasswordEdit");
            var ddlStatusEdit = (DropDownList)gvDetails.Rows[e.RowIndex].FindControl("ddlStatusEdit");

            if (Session["USERID"] == null)
            {
                Response.Redirect("~/Login/UI/Login.aspx");
            }
            else if (txtUserNameEdit.Text == "")
            {
                lblGridMSG.Visible = true;
                lblGridMSG.Text = "User Name Required .";
                txtUserNameEdit.Focus();
            }
            else if (txtMobile1Edit.Text == "")
            {
                lblGridMSG.Visible = true;
                lblGridMSG.Text = "Mobile No Required .";
                txtMobile1Edit.Focus();
            }
            else if (!MobileCheckForUpdate(txtMobile1Edit.Text, "MOBNO1", lblUserCodeEdit.Text))
            {
                lblGridMSG.Visible = true;
                lblGridMSG.Text = "Mobile No Duplicate.";
                txtMobile1Edit.Focus();
            }
            else if (txtLoginIdEdit.Text == "")
            {
                lblGridMSG.Visible = true;
                lblGridMSG.Text = "Login Id Required .";
                txtLoginIdEdit.Focus();
            }
            else if (!MobileCheckForUpdate(txtLoginIdEdit.Text, "LOGINID", lblUserCodeEdit.Text))
            {
                lblGridMSG.Visible = true;
                lblGridMSG.Text = "Login  Id Duplicate.";
                txtLoginIdEdit.Focus();
            }
            else if (txtPasswordEdit.Text == "")
            {
                lblGridMSG.Visible = true;
                lblGridMSG.Text = "Password Required .";
                txtPasswordEdit.Focus();
            }
            else
            {
                iob.UpdUserID = HttpContext.Current.Session["USERID"].ToString();
                iob.UPDIpaddress = HttpContext.Current.Session["IpAddress"].ToString();
                iob.UPDUserPC = HttpContext.Current.Session["PCName"].ToString();
                iob.UpdTime = dbFunctions.Timezone(DateTime.Now);

               
                try
                {
                    // logdata add start //
                    string lotileng = HttpContext.Current.Session["Location"].ToString();
                    string ipAddress = HttpContext.Current.Session["IpAddress"].ToString();
                    string logdata = dbFunctions.StringData(@"SELECT CONVERT(NVARCHAR(50),COMPID,103)+'  '+PSTP+'  '+PSID+'  '+USERCD+'  '+
                    USERNM+'  '+ISNULL(MOBNO1,'(NULL)')+'  '+ISNULL(MOBNO2,'(NULL)')+'  '+LOGINID+'  '+LOGINPW+'  '+STATUS+'  '+
                    ISNULL(APITOKEN,'(NULL)')+'  '+ISNULL(CONVERT(NVARCHAR(50),APITOKENEXPTM,103),'(NULL)')+'  '+ISNULL(USERPC,'(NULL)')+'  '+
                    ISNULL(CONVERT(NVARCHAR(50),USERID,103),'(NULL)')+'  '+ISNULL(CONVERT(NVARCHAR(50),INTIME,103),'(NULL)')+'  '+
                    ISNULL(IPADDRESS,'(NULL)')+'  '+ISNULL(INSLTUDE,'(NULL)')+'  '+ISNULL(UPDUSERPC,'(NULL)')+'  '+
                    ISNULL(CONVERT(NVARCHAR(50),UPDINTIME,103),'(NULL)')+'  '+ISNULL(CONVERT(NVARCHAR(50),UPDUSERID,103),'(NULL)')+'  '+
                    ISNULL(UPDIPADDRESS,'(NULL)')+'  '+ISNULL(UPDLTUDE,'(NULL)') FROM STK_USERPS");
                    string logid = "UPDATE";
                    string tableid = "STK_USERPS";
                    LogDataFunction.InsertLogData(lotileng, logid, tableid, logdata, ipAddress);
                    // logdata add end //
                }
                catch (Exception)
                {

                }
                iob.CompanyId = "101";
                iob.PartyId = lblPsIdEdit.Text;

                iob.UserCode = lblUserCodeEdit.Text;
                iob.Pstp = "PARTY";
                iob.PartyNameEnglish = txtUserNameEdit.Text;
                iob.MobNo = txtMobile1Edit.Text;
                iob.MobNo2 = txtMobile2Edit.Text;
                iob.LoginId = txtLoginIdEdit.Text;
                iob.Password = txtPasswordEdit.Text;
                iob.Status = ddlStatusEdit.SelectedValue;

                string s = dob.UpdatePartyUserLogin(iob);
                if (s == "")
                {
                    gvDetails.EditIndex = -1;
                    GridShow();
                }
                else
                {
                    lblGridMSG.Visible = true;
                    lblGridMSG.Text = "Internal Error Occured.";
                }
            }
        }
        protected void gvDetails_RowCancelingEdit(object sender, GridViewCancelEditEventArgs e)
        {
            if (Session["USERID"] == null)
            {
                Response.Redirect("~/Login/UI/Login.aspx");
            }
            else
            {
                gvDetails.EditIndex = -1;
                GridShow();
            }
        }

        protected void gvDetails_RowDeleting(object sender, GridViewDeleteEventArgs e)
        {
            if (Session["USERID"] == null)
            {
                Response.Redirect("~/Login/UI/Login.aspx");
            }
            else
            {
                SqlConnection conn = new SqlConnection(dbFunctions.connection);

                //Label lblCatGID = (Label)gvDetails.Rows[e.RowIndex].FindControl("lblCatGID");
                Label lblItemID = (Label)gvDetails.Rows[e.RowIndex].FindControl("lblItemID");

                string check = dbFunctions.StringData(@"select ITEMID from STK_TRANS where ITEMID = '" + lblItemID.Text + "'");

                int result = 0;

                if (check == "")
                {
                    try
                    {
                        // logdata add start //
                        string lotileng = HttpContext.Current.Session["Location"].ToString();
                        string ipAddress = HttpContext.Current.Session["IpAddress"].ToString();
                        string logdata = dbFunctions.StringData(@"");
                        string logid = "DELETE";
                        string tableid = "STK_ITEM";
                        LogDataFunction.InsertLogData(lotileng, logid, tableid, logdata, ipAddress);
                        // logdata add end //
                    }
                    catch (Exception)
                    {

                    }
                    conn.Open();
                    SqlCommand cmd = new SqlCommand(@"", conn);
                    result = cmd.ExecuteNonQuery();
                    conn.Close();
                }

                else
                {
                    Response.Write("<script>alert('This Item has a Transaction.');</script>");
                }

                if (result == 1)
                {
                    GridShow();
                }
            }
        }
    }
}