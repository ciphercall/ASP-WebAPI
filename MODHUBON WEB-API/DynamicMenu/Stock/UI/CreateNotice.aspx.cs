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
    public partial class CreateNotice : System.Web.UI.Page
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
                const string formLink = "/Stock/UI/CreateNotice.aspx";
                bool permission = UserPermissionChecker.checkParmit(formLink, "STATUS");
                if (permission)
                {
                    if (!IsPostBack)
                    {
                        txtMonthYear.Text = dbFunctions.Timezone(DateTime.Now).ToString("MMM-yyyy").ToUpper();
                        GridShow();
                    }
                }
                else
                {
                    Response.Redirect("../../DashBoard/UI/Default.aspx");
                }
            }
        }
        protected void GridShow()
        {

            SqlConnection conn = new SqlConnection(dbFunctions.connection);
            conn.Open();
            SqlCommand cmd = new SqlCommand($@"SELECT COMPID, NOTICEYY, NOTICESL, NOTICETP, NOTICE, 
            REPLACE(CONVERT(NVARCHAR,EFDT,103),'/','-') EFDT, REPLACE(CONVERT(NVARCHAR,ETDT,103),'/','-') ETDT, 
            CASE WHEN STATUS='A' THEN 'ACTIVE' ELSE 'INACTIVE' ENd AS STATUS FROM ASL_NOTICE
			WHERE UPPER(SUBSTRING(REPLACE(CONVERT(NVARCHAR,NOTICEDT,106),' ','-'),4,8))='{txtMonthYear.Text}'", conn);
            SqlDataAdapter da = new SqlDataAdapter(cmd);
            DataSet ds = new DataSet();
            da.Fill(ds);
            conn.Close();
            if (ds.Tables[0].Rows.Count > 0)
            {
                gvDetails.DataSource = ds;
                gvDetails.DataBind();
                lblGridMSG.Visible = false;
                TextBox txtNotice = (TextBox)gvDetails.FooterRow.FindControl("txtNotice");
                txtNotice.Focus();
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
                TextBox txtNotice = (TextBox)gvDetails.FooterRow.FindControl("txtNotice");
                txtNotice.Focus();
            }
            TextBox txtEffectFrom = (TextBox)gvDetails.FooterRow.FindControl("txtEffectFrom");
            TextBox txtEffectTo = (TextBox)gvDetails.FooterRow.FindControl("txtEffectTo");
            txtEffectTo.Text = dbFunctions.Timezone(DateTime.Now).ToString("dd-MM-yyyy").ToUpper();
            txtEffectFrom.Text = txtEffectTo.Text;
        }

        private string GenerateId(int year)
        {
            var noticesl = "";
            var maxid = dbFunctions.StringData($@"SELECT MAX(NOTICESL) FROM ASL_NOTICE WHERE NOTICEYY='{year}'");
            if (maxid == "")
            {
                noticesl = year + "0001";
            }
            else
            {
                var id = maxid.Substring(4, 4);
                if (Convert.ToInt32(id) < 9)
                {
                    int maximumid = Convert.ToInt32(id) + 1;
                    noticesl = year + "000" + maximumid;
                }
                else if (Convert.ToInt32(id) < 99)
                {
                    int maximumid = Convert.ToInt32(id) + 1;
                    noticesl = year + "00" + maximumid;
                }
                else if (Convert.ToInt32(id) < 999)
                {
                    int maximumid = Convert.ToInt32(id) + 1;
                    noticesl = year + "0" + maximumid;
                }
                else
                {
                    int maximumid = Convert.ToInt32(id) + 1;
                    noticesl = year + maximumid.ToString();
                }

            }
            return noticesl;
        }
      
        protected void gvDetails_RowCommand(object sender, GridViewCommandEventArgs e)
        {
            iob.UserID = HttpContext.Current.Session["USERID"].ToString();
            iob.Ipaddress = HttpContext.Current.Session["IpAddress"].ToString();
            iob.UserPC = HttpContext.Current.Session["PCName"].ToString();
            iob.ITime = dbFunctions.Timezone(DateTime.Now);

            var ddlNoticeType = (DropDownList)gvDetails.FooterRow.FindControl("ddlNoticeType");
            var txtNotice = (TextBox)gvDetails.FooterRow.FindControl("txtNotice");
            var txtEffectFrom = (TextBox)gvDetails.FooterRow.FindControl("txtEffectFrom");
            var txtEffectTo = (TextBox)gvDetails.FooterRow.FindControl("txtEffectTo");
            var ddlStatus = (DropDownList)gvDetails.FooterRow.FindControl("ddlStatus");

            if (e.CommandName.Equals("AddNew"))
            {
                if (txtNotice.Text == "")
                {
                    lblGridMSG.Visible = true;
                    lblGridMSG.Text = "Notice Details Required .";
                    txtNotice.Focus();
                }
                else if (txtEffectFrom.Text=="")
                {
                    lblGridMSG.Visible = true;
                    lblGridMSG.Text = "Effect From Time NRequired.";
                    txtEffectFrom.Focus();
                }
                else if (txtEffectTo.Text == "")
                {
                    lblGridMSG.Visible = true;
                    lblGridMSG.Text = "Effect To Time Required .";
                    txtEffectTo.Focus();
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
                        iob.TrDt = dbFunctions.Timezone(DateTime.Now);
                        iob.NoticeId = GenerateId(Convert.ToInt16(iob.TrDt.ToString("yyyy")));
                        iob.NoticeYear = iob.TrDt.ToString("yyyy");
                        iob.NoticeType = ddlNoticeType.SelectedValue;
                        iob.Notice = txtNotice.Text;
                        iob.EffectFromDate = DateTime.Parse(txtEffectFrom.Text, dateformat, System.Globalization.DateTimeStyles.AssumeLocal);
                        iob.EffectToDate = DateTime.Parse(txtEffectTo.Text, dateformat, System.Globalization.DateTimeStyles.AssumeLocal);
                        iob.Status = ddlStatus.SelectedValue;

                        string s = dob.InsertNotice(iob);
                        if (s == "")
                        {
                            GridShow();
                            txtNotice.Focus();
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
                var txtNoticeEdit = (TextBox)gvDetails.Rows[e.NewEditIndex].FindControl("txtNoticeEdit");
                txtNoticeEdit.Focus();
            }
        }

        protected void gvDetails_RowUpdating(object sender, GridViewUpdateEventArgs e)
        {
            var lblYearEdit = (Label)gvDetails.Rows[e.RowIndex].FindControl("lblYearEdit");
            var lblnoticeslEdit = (Label)gvDetails.Rows[e.RowIndex].FindControl("lblnoticeslEdit");

            var ddlNoticeTypeEdit = (DropDownList)gvDetails.Rows[e.RowIndex].FindControl("ddlNoticeTypeEdit");
            var txtNoticeEdit = (TextBox)gvDetails.Rows[e.RowIndex].FindControl("txtNoticeEdit");
            var txtEffectFromEdit = (TextBox)gvDetails.Rows[e.RowIndex].FindControl("txtEffectFromEdit");
            var txtEffectToEdit = (TextBox)gvDetails.Rows[e.RowIndex].FindControl("txtEffectToEdit");
            var ddlStatusEdit = (DropDownList)gvDetails.Rows[e.RowIndex].FindControl("ddlStatusEdit");

            if (Session["USERID"] == null)
            {
                Response.Redirect("~/Login/UI/Login.aspx");
            }
            else if (txtNoticeEdit.Text == "")
            {
                lblGridMSG.Visible = true;
                lblGridMSG.Text = "Notice Data Required .";
                txtNoticeEdit.Focus();
            }
            else if (txtEffectFromEdit.Text == "")
            {
                lblGridMSG.Visible = true;
                lblGridMSG.Text = "Effect Date Required .";
                txtEffectFromEdit.Focus();
            }
            else if (txtEffectToEdit.Text == "")
            {
                lblGridMSG.Visible = true;
                lblGridMSG.Text = "Effect Date Required.";
                txtEffectToEdit.Focus();
            }
            else
            {
                iob.UpdUserID = HttpContext.Current.Session["USERID"].ToString();
                iob.UPDIpaddress = HttpContext.Current.Session["IpAddress"].ToString();
                iob.UPDUserPC = HttpContext.Current.Session["PCName"].ToString();
                iob.UpdTime = dbFunctions.Timezone(DateTime.Now);
                
                iob.CompanyId = "101";
                iob.TrDt = dbFunctions.Timezone(DateTime.Now);
                iob.NoticeId = lblnoticeslEdit.Text;
                iob.NoticeYear = lblYearEdit.Text;
                iob.NoticeType = ddlNoticeTypeEdit.SelectedValue;
                iob.Notice = txtNoticeEdit.Text;
                iob.EffectFromDate = DateTime.Parse(txtEffectFromEdit.Text, dateformat, System.Globalization.DateTimeStyles.AssumeLocal);
                iob.EffectToDate = DateTime.Parse(txtEffectToEdit.Text, dateformat, System.Globalization.DateTimeStyles.AssumeLocal);
                iob.Status = ddlStatusEdit.SelectedValue;


                try
                {
                    // logdata add start //
                    string lotileng = HttpContext.Current.Session["Location"].ToString();
                    string ipAddress = HttpContext.Current.Session["IpAddress"].ToString();
                    string logdata = dbFunctions.StringData($@"SELECT CONVERT(NVARCHAR(50),COMPID,103)+'  '+CONVERT(NVARCHAR(50),NOTICEDT,103)+'  
                    '+CONVERT(NVARCHAR(50),NOTICEYY,103)+'  '+NOTICESL+'  '+NOTICETP+'  '+NOTICE+'  '+ISNULL(CONVERT(NVARCHAR(50),EFDT,103),'(NULL)')+'  
                    '+ISNULL(CONVERT(NVARCHAR(50),ETDT,103),'(NULL)')+'  '+STATUS+'  '+ISNULL(USERPC,'(NULL)')+'  '+ISNULL(CONVERT(NVARCHAR(50),USERID,103),'(NULL)')+'  
                    '+ISNULL(CONVERT(NVARCHAR(50),INTIME,103),'(NULL)')+'  '+ISNULL(IPADDRESS,'(NULL)')+'  '+ISNULL(INSLTUDE,'(NULL)')+'  '+ISNULL(UPDUSERPC,'(NULL)')+'  
                    '+ISNULL(CONVERT(NVARCHAR(50),UPDINTIME,103),'(NULL)')+'  '+ISNULL(CONVERT(NVARCHAR(50),UPDUSERID,103),'(NULL)')+'  '+ISNULL(UPDIPADDRESS,'(NULL)')+'  
                    '+ISNULL(UPDLTUDE,'(NULL)') FROM ASL_NOTICE WHERE NOTICEYY='{iob.NoticeYear}' AND NOTICESL='{iob.NoticeId}'");
                    string logid = "UPDATE";
                    string tableid = "ASL_NOTICE";
                    LogDataFunction.InsertLogData(lotileng, logid, tableid, logdata, ipAddress);
                    // logdata add end //
                }
                catch (Exception)
                {
                    //ignore
                }

                string s = dob.UpdateNotice(iob);
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

                var lblYear = (Label)gvDetails.Rows[e.RowIndex].FindControl("lblYear");
                var lblnoticesl = (Label)gvDetails.Rows[e.RowIndex].FindControl("lblnoticesl");

                iob.CompanyId = "101";
                iob.NoticeYear = lblYear.Text;
                iob.NoticeId = lblnoticesl.Text;
                try
                {
                    // logdata add start //
                    string lotileng = HttpContext.Current.Session["Location"].ToString();
                    string ipAddress = HttpContext.Current.Session["IpAddress"].ToString();
                    string logdata = dbFunctions.StringData($@"SELECT CONVERT(NVARCHAR(50),COMPID,103)+'  '+CONVERT(NVARCHAR(50),NOTICEDT,103)+'  
                    '+CONVERT(NVARCHAR(50),NOTICEYY,103)+'  '+NOTICESL+'  '+NOTICETP+'  '+NOTICE+'  '+ISNULL(CONVERT(NVARCHAR(50),EFDT,103),'(NULL)')+'  
                    '+ISNULL(CONVERT(NVARCHAR(50),ETDT,103),'(NULL)')+'  '+STATUS+'  '+ISNULL(USERPC,'(NULL)')+'  '+ISNULL(CONVERT(NVARCHAR(50),USERID,103),'(NULL)')+'  
                    '+ISNULL(CONVERT(NVARCHAR(50),INTIME,103),'(NULL)')+'  '+ISNULL(IPADDRESS,'(NULL)')+'  '+ISNULL(INSLTUDE,'(NULL)')+'  '+ISNULL(UPDUSERPC,'(NULL)')+'  
                    '+ISNULL(CONVERT(NVARCHAR(50),UPDINTIME,103),'(NULL)')+'  '+ISNULL(CONVERT(NVARCHAR(50),UPDUSERID,103),'(NULL)')+'  '+ISNULL(UPDIPADDRESS,'(NULL)')+'  
                    '+ISNULL(UPDLTUDE,'(NULL)') FROM ASL_NOTICE WHERE NOTICEYY='{iob.NoticeYear}' AND NOTICESL='{iob.NoticeId}'");
                    string logid = "DELETE";
                    string tableid = "ASL_NOTICE";
                    LogDataFunction.InsertLogData(lotileng, logid, tableid, logdata, ipAddress);
                    // logdata add end //
                }
                catch (Exception)
                {
                    //ingore
                }
                var result = dob.DeleteNotice(iob);
                if (result == "")
                {
                    GridShow();
                }
            }
        }
        
        protected void txtMonthYear_OnTextChanged(object sender, EventArgs e)
        {
            GridShow();
        }
    }
}