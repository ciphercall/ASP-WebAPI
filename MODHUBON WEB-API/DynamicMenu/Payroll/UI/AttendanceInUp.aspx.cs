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
using DynamicMenu.Payroll.DataAccess;
using DynamicMenu.Payroll.Interface;

namespace DynamicMenu.Payroll.UI
{
    public partial class AttendanceInUp : System.Web.UI.Page
    {
        readonly PayrollDataAcces _dob = new PayrollDataAcces();
        readonly PayrollInterface _iob = new PayrollInterface();
        readonly SqlConnection _conn = new SqlConnection(dbFunctions.connection);
        readonly IFormatProvider _dateformat = new System.Globalization.CultureInfo("fr-FR", true);

        protected void Page_Load(object sender, EventArgs e)
        {
            if (Session["USERID"] == null)
            {
                Response.Redirect("~/Login/UI/Login.aspx");
            }
            else
            {
                const string formLink = "/Payroll/UI/AttendanceInUp.aspx";
                var permission = UserPermissionChecker.checkParmit(formLink, "STATUS");
                if (permission)
                {
                    if (!Page.IsPostBack)
                    {
                        var monyear =dbFunctions.Timezone(DateTime.Now);
                        txtDate.Text = monyear.ToString("dd-MM-yyyy").ToUpper();

                       dbFunctions.DropDownAddSelectTextWithValue(ddlBranch, "SELECT BRANCHNM, BRANCHCD FROM ASL_BRANCH");

                        var uTp = HttpContext.Current.Session["USERTYPE"].ToString();
                        //var brCD = HttpContext.Current.Session["BrCD"].ToString();
                        // if (uTp == "COMPADMIN")
                       dbFunctions.DropDownAddSelectTextWithValue(ddlBranch, "SELECT BRANCHNM, BRANCHCD FROM ASL_BRANCH");
                        //else
                        //{
                        //   dbFunctions.DropDownAddTextWithValue(ddlBranch, "SELECT BRANCHNM, BRANCHCD FROM ASL_BRANCH WHERE BRANCHCD='" + brCD + "'");
                        //    GridShow();
                        //}

                        ddlBranch.Focus();
                    }
                }
                else
                {
                    Response.Redirect("../../DeshBoard/UI/Default.aspx");
                }
            }
        }
        protected void ddlBranch_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (ddlBranch.Text == "--SELECT--")
            {
                lblError.Text = "Select Branch Name.";
                lblError.Visible = true;
            }
            else
            {
                GridShow();
                lblError.Text = "";
                lblError.Visible = false;
            }

        }

        protected void txtMnYear_TextChanged(object sender, EventArgs e)
        {
            if (ddlBranch.Text == "--SELECT--")
            {
                lblError.Text = "Select Branch Name.";
                lblError.Visible = true;
            }
            else
            {
                GridShow();
                lblError.Text = "";
                lblError.Visible = false;
            }
        }
        private void GridShow()
        {
            string date = DateTime.Parse(txtDate.Text, _dateformat, System.Globalization.DateTimeStyles.AssumeLocal).ToString("yyyy-MM-dd");
            _conn.Open();
            SqlCommand cmd = new SqlCommand(@"SELECT HR_EMP.EMPNM, HR_ATREG.TRANSDT, HR_ATREG.EMPID, HR_EMP.SHIFTID,HR_SHIFT.SHIFTNM,
            CONVERT(NVARCHAR, HR_ATREG.TIMEIN, 8) TIMEIN, CONVERT(NVARCHAR, HR_ATREG.TIMEOUT, 8) TIMEOUT,
            CONVERT(NVARCHAR,HR_ATREG.LATEHR, 8) LATEHR, CONVERT(NVARCHAR, HR_ATREG.OTHOUR, 8) OTHOUR
            FROM HR_ATREG INNER JOIN HR_EMP ON HR_ATREG.EMPID = HR_EMP.EMPID INNER JOIN
            ASL_BRANCH ON HR_EMP.COSTPID = ASL_BRANCH.BRANCHCD INNER JOIN
            HR_SHIFT ON HR_EMP.SHIFTID = HR_SHIFT.SHIFTID 
            WHERE ASL_BRANCH.BRANCHCD = '" + ddlBranch.SelectedValue + "' AND HR_ATREG.TRANSDT='" + date + "' ORDER BY HR_ATREG.INSTIME", _conn);

            SqlDataAdapter da = new SqlDataAdapter(cmd);
            DataSet ds = new DataSet();
            da.Fill(ds);
            _conn.Close();
            if (ds.Tables[0].Rows.Count > 0)
            {
                gvdetails.DataSource = ds;
                gvdetails.DataBind();
            }

            else
            {
                ds.Tables[0].Rows.Add(ds.Tables[0].NewRow());
                gvdetails.DataSource = ds;
                gvdetails.DataBind();
                var columncount = gvdetails.Rows[0].Cells.Count;
                gvdetails.Rows[0].Cells.Clear();
                gvdetails.Rows[0].Cells.Add(new TableCell());
                gvdetails.Rows[0].Cells[0].ColumnSpan = columncount;
                gvdetails.Rows[0].Visible = false;
            }
            var ddlEmployeeNameFooter = (DropDownList)gvdetails.FooterRow.FindControl("ddlEmployeeNameFooter");
           dbFunctions.DropDownAddSelectTextWithValue(ddlEmployeeNameFooter, @"SELECT HR_EMP.EMPNM, HR_EMP.EMPID FROM HR_EMP INNER JOIN ASL_BRANCH ON 
            HR_EMP.COSTPID = ASL_BRANCH.BRANCHCD WHERE ASL_BRANCH.BRANCHCD = '" + ddlBranch.SelectedValue + "' EXCEPT " +
            "SELECT HR_EMP.EMPNM, HR_EMP.EMPID FROM HR_EMP INNER JOIN HR_ATREG ON HR_EMP.EMPID = HR_ATREG.EMPID INNER JOIN " +
            "ASL_BRANCH ON HR_EMP.COSTPID = ASL_BRANCH.BRANCHCD WHERE ASL_BRANCH.BRANCHCD = '" + ddlBranch.SelectedValue + "'   AND TRANSDT='" + date + "' " +
            "ORDER BY EMPNM");
            ddlEmployeeNameFooter.Focus();

        }
        protected void ddlEmployeeNameFooter_SelectedIndexChanged(object sender, EventArgs e)
        {
            GridViewRow row = ((GridViewRow)((DropDownList)sender).NamingContainer);
            var ddlEmployeeNameFooter = (DropDownList)row.FindControl("ddlEmployeeNameFooter");
            var lblShiftFooter = (Label)row.FindControl("lblShiftFooter");
            var lblShiftIdFooter = (Label)row.FindControl("lblShiftIdFooter");
            var txtIntimeFooter = (TextBox)row.FindControl("txtIntimeFooter");

            if (ddlEmployeeNameFooter.SelectedValue == "--SELECT--")
            {
                lblError.Text = "Select employee name.";
                lblError.Visible = true;
                ddlEmployeeNameFooter.Focus();
            }
            else
            {
                lblError.Text = "";
                lblError.Visible = false;
                dbFunctions.lblAdd("SELECT SHIFTID from HR_EMP WHERE EMPID='" + ddlEmployeeNameFooter.SelectedValue + "'", lblShiftIdFooter);
                dbFunctions.lblAdd("SELECT SHIFTNM FROM HR_SHIFT WHERE SHIFTID='" + lblShiftIdFooter.Text + "'", lblShiftFooter);
                dbFunctions.lblAdd("SELECT LATEFR FROM HR_SHIFT WHERE SHIFTID='" + lblShiftIdFooter.Text + "'", lblLate);
                txtIntimeFooter.Text =dbFunctions.TimeFormat(dbFunctions.Timezone(DateTime.Now));
                ddlEmployeeNameFooter.Focus();
            }

        }

        protected void txtIntimeFooter_OnTextChanged(object sender, EventArgs e)
        {
            Label lblShiftIdFooter = (Label)gvdetails.FooterRow.FindControl("lblShiftIdFooter");
            TextBox txtIntimeFooter = (TextBox)gvdetails.FooterRow.FindControl("txtIntimeFooter");
            TextBox txtLatetimeFooter = (TextBox)gvdetails.FooterRow.FindControl("txtLatetimeFooter");
            dbFunctions.lblAdd("SELECT LATEFR FROM HR_SHIFT WHERE SHIFTID='" + lblShiftIdFooter.Text + "'", lblLate);
            TimeSpan late = TimeSpan.Parse(lblLate.Text);
            TimeSpan intime = TimeSpan.Parse(txtIntimeFooter.Text);

            //TimeSpan count = TimeSpan.Parse(txtLatetimeFooter.Text);

            if (intime > late)
            {
                txtLatetimeFooter.Text = (intime - late).ToString();
            }
            else
            {
                txtLatetimeFooter.Text = TimeSpan.Parse("00:00:00").ToString();
            }
        }

        protected void txtOuttimeFooter_OnTextChanged(object sender, EventArgs e)
        {
            Label lblShiftIdFooter = (Label)gvdetails.FooterRow.FindControl("lblShiftIdFooter");
            TextBox txtOuttimeFooter = (TextBox)gvdetails.FooterRow.FindControl("txtOuttimeFooter");
            TextBox txtOTHourFooter = (TextBox)gvdetails.FooterRow.FindControl("txtOTHourFooter");
            dbFunctions.lblAdd("SELECT OTFROM FROM HR_SHIFT WHERE SHIFTID='" + lblShiftIdFooter.Text + "'", lblOTHOUR);
            TimeSpan OTHour = TimeSpan.Parse(lblOTHOUR.Text);
            TimeSpan OTFrom = TimeSpan.Parse(txtOuttimeFooter.Text);
            if (OTFrom > OTHour)
            {
                txtOTHourFooter.Text = (OTFrom-OTHour).ToString();
            }
            else
            {
                txtOTHourFooter.Text = TimeSpan.Parse("00:00:00").ToString();
            }
        }

        protected void txtIntimeEdit_OnTextChanged(object sender, EventArgs e)
        {
            Label lblShiftIdEdit = (Label)gvdetails.Rows[gvdetails.EditIndex].FindControl("lblShiftIdEdit");
            TextBox txtIntimeEdit = (TextBox)gvdetails.Rows[gvdetails.EditIndex].FindControl("txtIntimeEdit");
            TextBox txtLatetimeEdit = (TextBox)gvdetails.Rows[gvdetails.EditIndex].FindControl("txtLatetimeEdit");
            dbFunctions.lblAdd("SELECT LATEFR FROM HR_SHIFT WHERE SHIFTID='" + lblShiftIdEdit.Text + "'", lblLate);
            TimeSpan late = TimeSpan.Parse(lblLate.Text);
            TimeSpan intime = TimeSpan.Parse(txtIntimeEdit.Text);

            //TimeSpan count = TimeSpan.Parse(txtLatetimeFooter.Text);

            if (intime > late)
            {
                txtLatetimeEdit.Text = (intime - late).ToString();
            }
            else
            {
                txtLatetimeEdit.Text = TimeSpan.Parse("00:00:00").ToString();
            }
        }
        protected void txtOuttimeEdit_OnTextChanged(object sender, EventArgs e)
        {
            Label lblShiftIdEdit = (Label)gvdetails.Rows[gvdetails.EditIndex].FindControl("lblShiftIdEdit");
            TextBox txtOuttimeEdit = (TextBox)gvdetails.Rows[gvdetails.EditIndex].FindControl("txtOuttimeEdit");
            TextBox txtOTTIMEEdit = (TextBox)gvdetails.Rows[gvdetails.EditIndex].FindControl("txtOTTIMEEdit");
            dbFunctions.lblAdd("SELECT OTFROM FROM HR_SHIFT WHERE SHIFTID='" + lblShiftIdEdit.Text + "'", lblOTHOUR);
            TimeSpan OTHour = TimeSpan.Parse(lblOTHOUR.Text);
            TimeSpan OTFrom = TimeSpan.Parse(txtOuttimeEdit.Text);

            //TimeSpan count = TimeSpan.Parse(txtLatetimeFooter.Text);

            if (OTFrom > OTHour)
            {
                txtOTTIMEEdit.Text = (OTFrom - OTHour).ToString();
            }
            else
            {
                txtOTTIMEEdit.Text = TimeSpan.Parse("00:00:00").ToString();
            }
        }

        protected void gvdetails_RowCommand(object sender, GridViewCommandEventArgs e)
        {
            _iob.UserPC = HttpContext.Current.Session["PCName"].ToString();
            _iob.UserID = Convert.ToInt32(HttpContext.Current.Session["USERID"].ToString());
            _iob.Ipaddress = HttpContext.Current.Session["IpAddress"].ToString();
            _iob.ITime =dbFunctions.Timezone(DateTime.Now);
            var txtLotiLongTude = new TextBox();
            if (Master != null)
                txtLotiLongTude = (TextBox)Master.FindControl("txtLotiLongTude");
            else
                txtLotiLongTude.Text = "";
            _iob.Ltude = txtLotiLongTude.Text;

            try
            {
                var ddlEmployeeNameFooter = (DropDownList)gvdetails.FooterRow.FindControl("ddlEmployeeNameFooter");
                var lblShiftIdFooter = (Label)gvdetails.FooterRow.FindControl("lblShiftIdFooter");
                var txtIntimeFooter = (TextBox)gvdetails.FooterRow.FindControl("txtIntimeFooter");
                var txtOuttimeFooter = (TextBox)gvdetails.FooterRow.FindControl("txtOuttimeFooter");

                var txtLatetimeFooter = (TextBox)gvdetails.FooterRow.FindControl("txtLatetimeFooter");
                var txtOTHourFooter = (TextBox)gvdetails.FooterRow.FindControl("txtOTHourFooter");

                if (Session["USERID"] == null)
                    Response.Redirect("~/Login/UI/Login.aspx");
                else if (txtDate.Text == "")
                    txtDate.Focus();
                else if (txtIntimeFooter.Text == "")
                    txtIntimeFooter.Focus();
                else if (ddlEmployeeNameFooter.SelectedValue == "")
                {
                    ddlEmployeeNameFooter.Focus();
                    lblError.Text = "Select Employee Name";
                    lblError.Visible = true;
                }
                else if (lblShiftIdFooter.Text == "")
                {
                    ddlEmployeeNameFooter.Focus();
                    lblError.Text = "Employee shift id not Found. Please update shift id.";
                    lblError.Visible = true;
                }
                else
                {

                    _iob.TransDT = DateTime.Parse(txtDate.Text, _dateformat, System.Globalization.DateTimeStyles.AssumeLocal);
                    _iob.EMPID = Convert.ToInt32(ddlEmployeeNameFooter.SelectedValue);

                    _iob.SHIFTID = Convert.ToInt32(lblShiftIdFooter.Text);

                    _iob.AttInTime = TimeSpan.Parse(txtIntimeFooter.Text);
                    if (txtOuttimeFooter.Text == "")
                    {
                        _iob.AttOutTime = TimeSpan.Parse("00:00:00");
                        _iob.EntryTypeOut = "";
                    }
                    //else if (txtLatetimeFooter.Text=="")
                    //{
                    //    _iob.attLate = TimeSpan.Parse("00:00:00");
                    //    _iob.EntryTypeOut = "";
                    //}
                    //else if (txtOTHourFooter.Text=="")
                    //{
                    //    _iob.attOTHour = TimeSpan.Parse("00:00:00");
                    //    _iob.EntryTypeOut = "";
                    //}
                    else
                    {
                        _iob.AttOutTime = TimeSpan.Parse(txtOuttimeFooter.Text);
                        _iob.EntryTypeOut = "MANUAL";
                    }
                    _iob.EntryTypeIn = "MANUAL";
                    _iob.attLate = TimeSpan.Parse(txtLatetimeFooter.Text);
                    _iob.attOTHour = TimeSpan.Parse(txtOTHourFooter.Text);

                    if (e.CommandName.Equals("Add"))
                    {
                        if (ddlEmployeeNameFooter.Text == "" || ddlEmployeeNameFooter.Text == "--SELECT--")
                        {
                            ddlEmployeeNameFooter.Focus();
                        }
                        else
                        {
                            _iob.EMPID = Convert.ToInt32(ddlEmployeeNameFooter.SelectedValue);
                            _dob.Insert_HR_Attendance(_iob);
                            GridShow();
                        }
                    }

                }
            }
            catch (Exception eX)
            {
                Response.Write(eX);
            }
        }

        protected void gvdetails_RowDataBound(object sender, GridViewRowEventArgs e)
        {

        }

        protected void gvdetails_RowCancelingEdit(object sender, GridViewCancelEditEventArgs e)
        {
            if (Session["USERID"] == null)
                Response.Redirect("~/Login/UI/Login.aspx");
            else
            {
                gvdetails.EditIndex = -1;
                GridShow();
            }
        }

        protected void gvdetails_RowDeleting(object sender, GridViewDeleteEventArgs e)
        {

            if (Session["USERID"] == null)
                Response.Redirect("~/Login/UI/Login.aspx");
            else
            {
                try
                {
                    var lblEmpId = (Label)gvdetails.Rows[e.RowIndex].FindControl("lblEmpId");

                    _iob.TransDT = DateTime.Parse(txtDate.Text, _dateformat, System.Globalization.DateTimeStyles.AssumeLocal);
                    // logdata add start //
                    try
                    {
                        string ipAdd = HttpContext.Current.Session["IpAddress"].ToString();
                        string lotileng = "";
                        string logdata =dbFunctions.StringData(@"SELECT CONVERT(NVARCHAR(50),TRANSDT,103)+'  '
                        +CONVERT(NVARCHAR(50),EMPID,103)+'  '+ISNULL(CONVERT(NVARCHAR(50),SHIFTID,103),'(NULL)')+'  '
                        +ISNULL(CONVERT(NVARCHAR(50),TIMEIN,8),'(NULL)')+'  '+ISNULL(CONVERT(NVARCHAR(50),TIMEOUT,8),'(NULL)')+'  '
                        +ISNULL(CONVERT(NVARCHAR(50),ENTRYTPI,103),'(NULL)')+'  '+ISNULL(CONVERT(NVARCHAR(50),ENTRYTPO,103),'(NULL)')
                        +'  '+ISNULL(USERPC,'(NULL)')+'  '+ISNULL(CONVERT(NVARCHAR(50),INSUSERID,103),'(NULL)')+'  '
                        +ISNULL(CONVERT(NVARCHAR(50),INSTIME,103),'(NULL)')+'  '+ISNULL(INSIPNO,'(NULL)')+'  '
                        +ISNULL(INSLTUDE,'(NULL)')+'  '+ISNULL(UPDUSERPC,'(NULL)')+'  '+ISNULL(CONVERT(NVARCHAR(50),UPDUSERID,103),'(NULL)')
                        +'  '+ISNULL(CONVERT(NVARCHAR(50),UPDTIME,103),'(NULL)')+'  '+ISNULL(UPDIPNO,'(NULL)')+'  '+ISNULL(UPDLTUDE,'(NULL)') 
                        FROM HR_ATREG WHERE TRANSDT='" + _iob.TransDT + "' AND EMPID='" + lblEmpId.Text + "'");
                        string logid = "DELETE";
                        string tableid = "HR_ATREG";
                        LogDataFunction.InsertLogData(lotileng, logid, tableid, logdata, ipAdd);
                    }
                    catch (Exception)
                    {
                        //ignore
                    }
                    // logdata add END //

                    _conn.Open();
                    SqlCommand cmd = new SqlCommand(@"DELETE FROM HR_ATREG WHERE EMPID='" + lblEmpId.Text + "' AND TRANSDT='" + _iob.TransDT + "'", _conn);
                    cmd.ExecuteNonQuery();
                    _conn.Close();
                    GridShow();
                }
                catch (Exception ex)
                {
                    Response.Write(ex);
                }

            }
        }

        protected void gvdetails_RowEditing(object sender, GridViewEditEventArgs e)
        {
            if (Session["USERID"] == null)
                Response.Redirect("~/Login/UI/Login.aspx");
            else
            {
                gvdetails.EditIndex = e.NewEditIndex;
                GridShow();
                var txtOuttimeEdit = (TextBox)gvdetails.Rows[e.NewEditIndex].FindControl("txtOuttimeEdit");
                txtOuttimeEdit.Focus();
            }
        }

        protected void gvdetails_RowUpdating(object sender, GridViewUpdateEventArgs e)
        {
            if (Session["USERID"] == null)
            {
                Response.Redirect("~/Login/UI/Login.aspx");
            }
            else
            {
                _iob.UPDUserPC = HttpContext.Current.Session["PCName"].ToString();
                _iob.UPDUsername = HttpContext.Current.Session["USERID"].ToString();
                _iob.UPDIpaddress = HttpContext.Current.Session["IpAddress"].ToString();
                _iob.UPDTime =dbFunctions.Timezone(DateTime.Now);
                var txtLotiLongTude = new TextBox();
                if (Master != null)
                    txtLotiLongTude = (TextBox)Master.FindControl("txtLotiLongTude");
                else
                    txtLotiLongTude.Text = "";
                _iob.UPDLtude = txtLotiLongTude.Text;

                var lblEmpIdEdit = (Label)gvdetails.Rows[e.RowIndex].FindControl("lblEmpIdEdit");
                var txtIntimeEdit = (TextBox)gvdetails.Rows[e.RowIndex].FindControl("txtIntimeEdit");
                var txtOuttimeEdit = (TextBox)gvdetails.Rows[e.RowIndex].FindControl("txtOuttimeEdit");

                var txtLatetimeEdit = (TextBox)gvdetails.Rows[e.RowIndex].FindControl("txtLatetimeEdit");
                var txtOTTIMEEdit = (TextBox)gvdetails.Rows[e.RowIndex].FindControl("txtOTTIMEEdit");


                _iob.TransDT = DateTime.Parse(txtDate.Text, _dateformat, System.Globalization.DateTimeStyles.AssumeLocal);
                _iob.EMPID = Convert.ToInt32(lblEmpIdEdit.Text);
                _iob.AttInTime = TimeSpan.Parse(txtIntimeEdit.Text);
                if (txtOuttimeEdit.Text == "")
                {
                    _iob.AttOutTime = TimeSpan.Parse("00:00:00");
                    _iob.EntryTypeOut = "";
                }
                else
                {
                    _iob.AttOutTime = TimeSpan.Parse(txtOuttimeEdit.Text);
                    _iob.EntryTypeOut = "MANUAL";
                }
                _iob.EntryTypeIn = "MANUAL";
                _iob.attLate = TimeSpan.Parse(txtLatetimeEdit.Text);
                _iob.attOTHour = TimeSpan.Parse(txtOTTIMEEdit.Text);

                if (txtDate.Text == "")
                    txtDate.Focus();
                else
                {
                    // logdata add start //
                    try
                    {
                        string ipAdd = HttpContext.Current.Session["IpAddress"].ToString();
                        string lotileng = "";
                        string logdata =dbFunctions.StringData(@"SELECT CONVERT(NVARCHAR(50),TRANSDT,103)+'  '
                        +CONVERT(NVARCHAR(50),EMPID,103)+'  '+ISNULL(CONVERT(NVARCHAR(50),SHIFTID,103),'(NULL)')+'  '
                        +ISNULL(CONVERT(NVARCHAR(50),TIMEIN,8),'(NULL)')+'  '+ISNULL(CONVERT(NVARCHAR(50),TIMEOUT,8),'(NULL)')+'  '
                        +ISNULL(CONVERT(NVARCHAR(50),ENTRYTPI,103),'(NULL)')+'  '+ISNULL(CONVERT(NVARCHAR(50),ENTRYTPO,103),'(NULL)')
                        +'  '+ISNULL(USERPC,'(NULL)')+'  '+ISNULL(CONVERT(NVARCHAR(50),INSUSERID,103),'(NULL)')+'  '
                        +ISNULL(CONVERT(NVARCHAR(50),INSTIME,103),'(NULL)')+'  '+ISNULL(INSIPNO,'(NULL)')+'  '
                        +ISNULL(INSLTUDE,'(NULL)')+'  '+ISNULL(UPDUSERPC,'(NULL)')+'  '+ISNULL(CONVERT(NVARCHAR(50),UPDUSERID,103),'(NULL)')
                        +'  '+ISNULL(CONVERT(NVARCHAR(50),UPDTIME,103),'(NULL)')+'  '+ISNULL(UPDIPNO,'(NULL)')+'  '+ISNULL(UPDLTUDE,'(NULL)') 
                        FROM HR_ATREG WHERE TRANSDT='" + _iob.TransDT + "' AND EMPID='" + _iob.EMPID + "'");
                        string logid = "UPDATE";
                        string tableid = "HR_ATREG";
                        LogDataFunction.InsertLogData(lotileng, logid, tableid, logdata, ipAdd);
                    }
                    catch (Exception)
                    {
                        //ignore
                    }
                    // logdata add END //

                    _dob.Update_HR_Attendance(_iob);
                    gvdetails.EditIndex = -1;
                    GridShow();
                }



            }
        }
    }
}