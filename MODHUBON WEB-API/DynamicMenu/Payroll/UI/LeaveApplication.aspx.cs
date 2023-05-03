using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Data.Entity.Core.Common.CommandTrees;
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
    public partial class LeaveApplication : System.Web.UI.Page
    {
        private IFormatProvider dateformat = new System.Globalization.CultureInfo("fr-FR", true);
        private PayrollDataAcces dob = new PayrollDataAcces();
        private PayrollInterface iob = new PayrollInterface();
        private SqlConnection conn = new SqlConnection(dbFunctions.connection);

        protected void Page_Load(object sender, EventArgs e)
        {
            if (Session["USERID"] == null)
            {
                Response.Redirect("~/Login/UI/Login.aspx");
            }
            else
            {
                const string formLink = "/Payroll/UI/LeaveApplication.aspx";
                bool permission = UserPermissionChecker.checkParmit(formLink, "STATUS");
                if (permission == true)
                {
                    if (!Page.IsPostBack)
                    {
                        var Date = dbFunctions.Timezone(DateTime.Now);
                        //var uTp = HttpContext.Current.Session["USERTYPE"].ToString();
                        //var brCD = HttpContext.Current.Session["BrCD"].ToString();
                        dbFunctions.DropDownAddSelectTextWithValue(ddlEMID, "SELECT EMPNM,EMPID FROM HR_EMP");

                        txtDate.Text = Date.ToString("dd/MM/yyyy").ToUpper();
                        txtDate_OnTextChanged(sender, e);
                        Transid();
                        // gridShow();
                    }
                }
                else
                {
                    Response.Redirect("../../DeshBoard/UI/Default.aspx");
                }
            }
        }

        protected void ddlEMID_OnTextChanged(object sender, EventArgs e)
        {
            gridShow();
            DropDownList ddlLvIDFooter = (DropDownList)gvDetails.FooterRow.FindControl("ddlLvIDFooter");
            dbFunctions.DropDownAddSelectTextWithValue(ddlLvIDFooter, "SELECT LEAVENM,LEAVEID FROM HR_LEAVE");
            ddlLvIDFooter.Focus();


        }

        protected void txtDate_OnTextChanged(object sender, EventArgs e)
        {
            if (btnEdit.Text == "Edit")
            {
                DateTime transdate = DateTime.Parse(txtDate.Text, dateformat,
                    System.Globalization.DateTimeStyles.AssumeLocal);
                string date = transdate.ToString("yyyy-MM-dd");

                string varYear = transdate.ToString("yyyy");
                string mon = transdate.ToString("MMM").ToUpper();
                string year = transdate.ToString("yy");
                var mY = mon + "-" + year;
                txtMY.Text = mY;
                Transid();
                ddlEMID.Focus();
            }
            else
            {
                DateTime transdate = DateTime.Parse(txtDate.Text, dateformat,
                    System.Globalization.DateTimeStyles.AssumeLocal);
                string date = transdate.ToString("yyyy-MM-dd");

                string varYear = transdate.ToString("yyyy");
                string mon = transdate.ToString("MMM").ToUpper();
                string year = transdate.ToString("yy");
                var mY = mon + "-" + year;
                txtMY.Text = mY;
                txtTransNo.Visible = false;
                ddlTransNo.Visible = true;
                ddlTransNo.Focus();
                dbFunctions.dropDownAddWithSelect(ddlTransNo,
                    "SELECT DISTINCT TRANSNO FROM HR_LAPPS WHERE TRANSDT='" + date + "'");

            }

        }

        public void Transid()
        {
            DateTime transdate = DateTime.Parse(txtDate.Text, dateformat,
                System.Globalization.DateTimeStyles.AssumeLocal);
            string date = transdate.ToString("yyyy-MM-dd");

            dbFunctions.lblAdd(
                $@"SELECT MAX(TRANSNO) FROM HR_LAPPSMST WHERE TRANSMY='{txtMY.Text}'", lblSTID);
            long sid;

            if (lblSTID.Text == "")
            {
                txtTransNo.Text = "1";
            }
            else
            {
                sid = Convert.ToInt64(lblSTID.Text) + 1;
                txtTransNo.Text = sid.ToString();
            }
        }

        private void gridShow()
        {
            conn.Open();
            var monthyr = "";
            var transno = "";
            if (btnEdit.Text == "Edit")
            {
                monthyr = txtMY.Text;
                transno = txtTransNo.Text;
            }
            else
            {
                if (ddlTransNo.Text == "Select")
                {

                    transno = "0";
                    monthyr = "0";
                }
                else
                {
                    //monthyr = ddlMOnthYear.Text;
                    transno = ddlTransNo.Text;
                }
            }
            SqlCommand cmd =
                new SqlCommand(
                    @"SELECT HR_LEAVE.LEAVEID,HR_LEAVE.LEAVENM, CONVERT(NVARCHAR(10),HR_LAPPS.LEAVEFR,103) LEAVEFR, CONVERT(NVARCHAR(10),HR_LAPPS.LEAVETO,103)LEAVETO, HR_LAPPS.LEAVEDAYS,HR_LAPPS.REASON FROM HR_LAPPS 
                                            INNER JOIN HR_LEAVE ON HR_LAPPS.LEAVEID=HR_LEAVE.LEAVEID WHERE TRANSNO='" +
                    transno + "' AND TRANSMY='" + txtMY.Text + "'", conn);

            SqlDataAdapter da = new SqlDataAdapter(cmd);
            DataSet ds = new DataSet();
            da.Fill(ds);
            conn.Close();
            if (ds.Tables[0].Rows.Count > 0)
            {
                gvDetails.DataSource = ds;
                gvDetails.DataBind();
                DropDownList ddlLvIDFooter = (DropDownList)gvDetails.FooterRow.FindControl("ddlLvIDFooter");
                ddlLvIDFooter.Focus();
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
                gvDetails.Rows[0].Visible = false;

            }
            
        }

        private string CkeckTransNO(string TransMY, Int64 TransNO)
        {
            string result = "false";
            SqlConnection conn = new SqlConnection(dbFunctions.connection);
            conn.Open();
            string script = "SELECT * FROM HR_LAPPSMST WHERE TRANSMY ='" + TransMY + "' AND TRANSNO ='" + TransNO + "'";
            SqlCommand cmd = new SqlCommand(script, conn);
            SqlDataAdapter da = new SqlDataAdapter(cmd);
            DataSet ds = new DataSet();
            da.Fill(ds);
            conn.Close();
            if (ds.Tables[0].Rows.Count < 1)
            {
                result = "true";
            }
            return result;
        }

        protected void gvDetails_OnRowCommand(object sender, GridViewCommandEventArgs e)
        {
            if (Session["UserName"] == null)
                Response.Redirect("~/Login/UI/Login.aspx");
            else
            {
                lblError.Text = "";
                iob.UserPC = HttpContext.Current.Session["PCName"].ToString();
                iob.Username = HttpContext.Current.Session["USERID"].ToString();
                //iob.Ipaddress = Session["IpAddress"].ToString();
                iob.ITime = dbFunctions.Timezone(DateTime.Now);
                
                var txtLotiLongTude = (TextBox)Master.FindControl("txtLotiLongTude");
                var txtIp = (TextBox)Master.FindControl("txtIp");
                iob.Ltude = txtLotiLongTude.Text;
                iob.Ipaddress = txtIp.Text;


                string serial = gvDetails.FooterRow.Cells[0].Text;
                DropDownList ddlLvIDFooter = (DropDownList)gvDetails.FooterRow.FindControl("ddlLvIDFooter");
                TextBox txtLvFRFooter = (TextBox)gvDetails.FooterRow.FindControl("txtLvFRFooter");
                TextBox txtLvTOFooter = (TextBox)gvDetails.FooterRow.FindControl("txtLvTOFooter");
                TextBox txtLvDaysFooter = (TextBox)gvDetails.FooterRow.FindControl("txtLvDaysFooter");
                TextBox txtReasonFooter = (TextBox)gvDetails.FooterRow.FindControl("txtReasonFooter");


                if (txtLvFRFooter.Text == "")
                {
                    lblError.Visible = true;
                    lblError.Text = "Select Leave From Date.";
                    txtLvFRFooter.Focus();
                }
                else if (txtLvTOFooter.Text == "")
                {
                    lblError.Visible = true;
                    lblError.Text = "Select Leave To Date.";
                    txtLvTOFooter.Focus();
                }

                else
                {
                    // Transid();
                    iob.Date = DateTime.Parse(txtDate.Text, dateformat, System.Globalization.DateTimeStyles.AssumeLocal);

                    if (btnEdit.Text == "Edit")
                        iob.TRANSNO = Convert.ToInt64(txtTransNo.Text);
                    else
                        iob.TRANSNO = Convert.ToInt64(ddlTransNo.Text);

                    iob.MonthYear = txtMY.Text;
                    iob.EmployeeID = Convert.ToInt64(ddlEMID.SelectedValue);
                    iob.Remarks = txtRemarks.Text;
                    iob.LEAVEID = Convert.ToInt32(ddlLvIDFooter.SelectedValue);
                    iob.LEAVEFR = DateTime.Parse(txtLvFRFooter.Text, dateformat,
                        System.Globalization.DateTimeStyles.AssumeLocal);
                    iob.LEAVETO = DateTime.Parse(txtLvTOFooter.Text, dateformat,
                        System.Globalization.DateTimeStyles.AssumeLocal);
                    iob.LEAVEDAYS = Convert.ToInt64(txtLvDaysFooter.Text);
                    iob.REASON = txtReasonFooter.Text;

                    if (e.CommandName.Equals("SaveCon"))
                    {
                        if (btnEdit.Text == "Edit")
                        {
                            string Check = CkeckTransNO(txtMY.Text, iob.TRANSNO);

                            if (Check == "true")
                            {
                                Transid();
                                //iob.TRANSNO = Convert.ToInt64(txtTransNo.Text);
                                dob.Insert_HR_LEAVEAPPS_MST(iob);
                                dob.Insert_HR_LEAVEAPPS(iob);
                                lblError.Visible = true;
                                lblError.Text = "Data Successfully Saved. !";
                                ddlEMID_OnTextChanged(sender, e);
                            }
                            else
                            {
                                dob.Insert_HR_LEAVEAPPS(iob);
                                gridShow();
                                lblError.Text = "";
                            }
                        }
                        else
                        {
                            //iob.TRANSNO = Convert.ToInt64(ddlTransNo.Text);
                            dob.Insert_HR_LEAVEAPPS(iob);
                            lblError.Visible = true;
                            lblError.Text = "Data Successfully Saved. !";
                            ddlEMID_OnTextChanged(sender, e);
                        }
                    }
                    else if (e.CommandName.Equals("Complete"))
                    {
                        if (btnEdit.Text == "Edit")
                        {
                            string Check = CkeckTransNO(txtMY.Text, iob.TRANSNO);

                            if (Check == "true")
                            {
                                Transid();
                                //iob.TRANSNO = Convert.ToInt64(txtTransNo.Text);
                                dob.Insert_HR_LEAVEAPPS_MST(iob);
                                dob.Insert_HR_LEAVEAPPS(iob);
                                lblError.Visible = true;
                                lblError.Text = "Data Successfully Saved. !";
                                ddlEMID_OnTextChanged(sender, e);
                            }
                            else
                            {
                                dob.Insert_HR_LEAVEAPPS(iob);
                                gridShow();
                                lblError.Text = "";
                            }
                        }
                        else
                        {
                            //iob.TRANSNO = Convert.ToInt64(ddlTransNo.Text);
                            dob.Insert_HR_LEAVEAPPS(iob);
                            lblError.Visible = true;
                            lblError.Text = "Data Successfully Saved. !";
                            ddlEMID_OnTextChanged(sender, e);
                        }
                    }
                }

            }
        }

        protected void txtLvTOFooter_OnTextChanged(object sender, EventArgs e)
        {
            TextBox txtLvFRFooter = (TextBox)gvDetails.FooterRow.FindControl("txtLvFRFooter");
            TextBox txtLvTOFooter = (TextBox)gvDetails.FooterRow.FindControl("txtLvTOFooter");
            TextBox txtLvDaysFooter = (TextBox)gvDetails.FooterRow.FindControl("txtLvDaysFooter");
            TextBox txtReasonFooter = (TextBox)gvDetails.FooterRow.FindControl("txtReasonFooter");

            DateTime startdate = DateTime.Parse(txtLvFRFooter.Text, dateformat,
                System.Globalization.DateTimeStyles.AssumeLocal);
            DateTime enddate = DateTime.Parse(txtLvTOFooter.Text, dateformat,
                System.Globalization.DateTimeStyles.AssumeLocal);
            string dateto = startdate.ToString("yyyy-MM-dd");
            string datefr = startdate.ToString("yyyy-MM-dd");
            if (enddate >= startdate)
            {
                double a = (enddate - startdate).TotalDays;
                double totaldays = a + 1;
                txtLvDaysFooter.Text = totaldays.ToString();
            }
            else
            {
                lblError.Visible = true;
                lblError.Text = "From date must be smaller than to date !.";
                txtReasonFooter.Focus();
            }
        }

        protected void txtLvTOEdit_OnTextChanged(object sender, EventArgs e)
        {
            TextBox txtLvFREdit = (TextBox)gvDetails.Rows[gvDetails.EditIndex].FindControl("txtLvFREdit");
            TextBox txtLvTOEdit = (TextBox)gvDetails.Rows[gvDetails.EditIndex].FindControl("txtLvTOEdit");
            TextBox txtLvDays = (TextBox)gvDetails.Rows[gvDetails.EditIndex].FindControl("txtLvDays");

            DateTime startdate = DateTime.Parse(txtLvFREdit.Text, dateformat,
                System.Globalization.DateTimeStyles.AssumeLocal);
            DateTime enddate = DateTime.Parse(txtLvTOEdit.Text, dateformat,
                System.Globalization.DateTimeStyles.AssumeLocal);
            string dateto = startdate.ToString("yyyy-MM-dd");
            string datefr = startdate.ToString("yyyy-MM-dd");
            if (enddate >= startdate)
            {
                double a = (enddate - startdate).TotalDays;
                double totaldays = a + 1;
                txtLvDays.Text = totaldays.ToString();
            }
            else
            {
                lblError.Visible = true;
                lblError.Text = "From date must be smaller than to date !.";
                txtLvDays.Focus();
            }
        }

        public void RefreshSession()
        {
            Session["Date"] = "";
            Session["MonthYear"] = "";
            Session["Transno"] = "";
            Session["EmID"] = "";
            Session["Remarks"] = "";
            Session["LeaveID"] = "";
            Session["FromDate"] = "";
            Session["ToDate"] = "";
            Session["LeaveDays"] = "";
            Session["Reason"] = "";
        }

        protected void gvDetails_OnRowDeleting(object sender, GridViewDeleteEventArgs e)
        {
            if (Session["USERID"] == null)
                Response.Redirect("~/Login/UI/Login.aspx");
            else
            {
                try
                {
                    string query = "";
                    SqlCommand comm = new SqlCommand(query, conn);
                    Label lblLbID = (Label)gvDetails.Rows[e.RowIndex].FindControl("lblLbID");
                    if (btnEdit.Text == "Edit")
                    {
                        query = "DELETE FROM HR_LAPPS WHERE LEAVEID = '" + lblLbID.Text + "' AND TRANSNO='" +
                                txtTransNo.Text + "' AND TRANSMY='" + txtMY.Text + "'";
                    }
                    else
                    {
                        query = "DELETE FROM HR_LAPPS WHERE LEAVEID = '" + lblLbID.Text + "' AND TRANSNO='" +
                                ddlTransNo.Text + "' AND TRANSMY='" + txtMY.Text + "'";

                    }

                    SqlCommand cmd = new SqlCommand(query, conn);
                    conn.Open();
                    int Result = cmd.ExecuteNonQuery();
                    conn.Close();
                    if (Result == 1)
                    {
                        gridShow();
                    }
                    gridShow();
                    ddlEMID_OnTextChanged(sender, e);
                }
                catch (Exception ex)
                {
                    Response.Write(ex);
                }
            }
        }

        protected void gvDetails_OnRowEditing(object sender, GridViewEditEventArgs e)
        {
            if (Session["USERID"] == null)
                Response.Redirect("~/Login/UI/Login.aspx");
            else
            {
                Label lblLbID = (Label)gvDetails.Rows[e.NewEditIndex].FindControl("lblLbID");
                string a = lblLbID.Text;
                gvDetails.EditIndex = e.NewEditIndex;
                gridShow();
                DropDownList ddlLvNMEdit = (DropDownList)gvDetails.Rows[e.NewEditIndex].FindControl("ddlLvNMEdit");
                dbFunctions.DropDownAddSelectTextWithValue(ddlLvNMEdit, "SELECT LEAVENM,LEAVEID FROM HR_LEAVE");
                ddlLvNMEdit.Items.FindByValue(a).Selected = true;
            }
        }

        protected void gvDetails_OnRowCancelingEdit(object sender, GridViewCancelEditEventArgs e)
        {
            gvDetails.EditIndex = -1;
            gridShow();
        }

        protected void gvDetails_OnRowUpdating(object sender, GridViewUpdateEventArgs e)
        {
            if (Session["USERID"] == null)
            {
                Response.Redirect("~/Login/UI/Login.aspx");
            }
            else
            {
                Label lblLvIDEdit = (Label)gvDetails.Rows[e.RowIndex].FindControl("lblLvIDEdit");
                TextBox txtLvFREdit = (TextBox)gvDetails.Rows[e.RowIndex].FindControl("txtLvFREdit");
                TextBox txtLvTOEdit = (TextBox)gvDetails.Rows[e.RowIndex].FindControl("txtLvTOEdit");
                TextBox txtReasonEdit = (TextBox)gvDetails.Rows[e.RowIndex].FindControl("txtReasonEdit");
                DateTime fdt = DateTime.Parse(txtLvFREdit.Text, dateformat,
                    System.Globalization.DateTimeStyles.AssumeLocal);
                DateTime tdt = DateTime.Parse(txtLvTOEdit.Text, dateformat,
                    System.Globalization.DateTimeStyles.AssumeLocal);
                string ip = Session["IpAddress"].ToString();
                long INSUSERID = Convert.ToInt64(Session["USERID"].ToString());
                //string INSLTUDE = Session["Location"].ToString();
                DateTime INSTIME = dbFunctions.Timezone(DateTime.Now);

                var INSLTUDE = (TextBox)Master.FindControl("txtLotiLongTude");
                var txtIp = (TextBox)Master.FindControl("txtIp");
                //iob.Ltude = txtLotiLongTude.Text;
               // iob.Ipaddress = txtIp.Text;

                if (btnEdit.Text == "New")
                {
                    SqlCommand cmd =
                        new SqlCommand(
                            "UPDATE HR_LAPPS SET LEAVEFR='" + fdt + "' , LEAVETO='" + tdt + "' , REASON='" +
                            txtReasonEdit.Text + "', UPDUSERID='" + INSUSERID + "',UPDTIME='" + INSTIME + "',UPDIPNO='" +
                            ip + "',UPDLTUDE='" + INSLTUDE + "' WHERE LEAVEID='" + lblLvIDEdit.Text + "' AND TRANSNO='" +
                            ddlTransNo.Text + "' AND TRANSMY='" + txtMY.Text + "'", conn);

                    conn.Open();
                    cmd.ExecuteNonQuery();
                    conn.Close();
                    gvDetails.EditIndex = -1;

                    gridShow();
                    lblError.Visible = true;
                    lblError.Text = "Successfully Updated";
                }
                else
                {
                    SqlCommand cmd =
                        new SqlCommand(
                            "UPDATE HR_LAPPS SET LEAVEFR='" + fdt + "' , LEAVETO='" + tdt + "' , REASON='" +
                            txtReasonEdit.Text + "', UPDUSERID='" + INSUSERID + "',UPDTIME='" + INSTIME + "',UPDIPNO='" +
                            ip + "',UPDLTUDE='" + INSLTUDE + "' WHERE LEAVEID='" + lblLvIDEdit.Text + "' AND TRANSNO='" +
                            txtTransNo.Text + "' AND TRANSMY='" + txtMY.Text + "'", conn);

                    conn.Open();
                    cmd.ExecuteNonQuery();
                    conn.Close();
                    gvDetails.EditIndex = -1;

                    gridShow();
                    lblError.Visible = true;
                    lblError.Text = "Successfully Updated";
                    
                }
                ddlEMID_OnTextChanged(sender, e);
            }
        }

        protected void btnEdit_OnClick(object sender, EventArgs e)
        {
            if (Session["USERID"] == null)
            {
                Response.Redirect("~/Login/UI/Login.aspx");
            }
            else
            {
                if (btnEdit.Text == "Edit")
                {
                    txtDate.Focus();
                    lblError.Visible = false;
                    btnEdit.Text = "New";
                    btnPrint.Visible = true;
                    txtTransNo.Visible = false;
                    ddlTransNo.Visible = true;

                    //  string uTp = HttpContext.Current.Session["UserTp"].ToString();
                    // string brCD = HttpContext.Current.Session["BrCD"].ToString();

                    //if (uTp == "ADMIN")
                    //{
                    //    dbFunctions.DropDownAddWithSelect(ddlTransNo, "SELECT TRANSNO FROM HR_LAPPSMST WHERE TRANSMY ='" + txtMY.Text + "'");
                    //}
                    //else
                    //{
                    //    dbFunctions.DropDownAddWithSelect(ddlInvoice, "SELECT DISTINCT TRANSNO FROM CNF_JOBEXP WHERE TRANSMY ='" + lblMY.Text + "' AND COMPID ='" + brCD + "'");
                    //}
                    //ddlInvoice.Focus();
                }
                else
                {
                    lblError.Visible = false;
                    btnEdit.Text = "Edit";
                    txtTransNo.Visible = true;
                    ddlTransNo.Visible = false;
                    Refresh();
                    // startUp();
                }
            }
        }

        private void Refresh()
        {
            lblError.Visible = false;
            txtRemarks.Text = "";
            ddlEMID.SelectedIndex = -1;
            if (btnEdit.Text == "Edit")
                Transid();
            else
                ddlTransNo.SelectedIndex = -1;
            gridShow();
        }

        protected void ddlTransNo_OnTextChanged(object sender, EventArgs e)
        {
            txtTransNo.Visible = false;
            ddlTransNo.Visible = true;
            lblError.Text = "";
            if (btnEdit.Text == "New")
            {
                try
                {
                    dbFunctions.txtAdd(@"SELECT REMARKS FROM HR_LAPPSMST WHERE TRANSNO='" + ddlTransNo.Text + "'",txtRemarks);
                    //string lblEMNM =
                    //    dbFunctions.StringData(
                    //        @"SELECT E.EMPNM FROM HR_LAPPSMST AS L INNER JOIN HR_EMP E ON L.EMPID=E.EMPID WHERE TRANSNO='" +
                    //        ddlTransNo.Text + "' AND TRANSMY='" + txtMY.Text + "'");
                    //ddlEMID.SelectedIndex = ddlEMID.Items.IndexOf(ddlEMID.Items.FindByText(lblEMNM));
                    //ddlEMID_OnTextChanged(sender, e);

                    string lblEMNM =
                        dbFunctions.StringData(
                            @"SELECT E.EMPID FROM HR_LAPPSMST AS L INNER JOIN HR_EMP E ON L.EMPID=E.EMPID WHERE TRANSNO='" +
                            ddlTransNo.Text + "' AND TRANSMY='" + txtMY.Text + "'");
                    ddlEMID.Text = lblEMNM;
                    ddlEMID_OnTextChanged(sender, e);

                }
                catch (Exception)
                {
                    //ignore
                }
            }
            else
            {
                //ddlTransNo.Focus();
            }
        }

        protected void btnRefresh_OnClick(object sender, EventArgs e)
        {
            Refresh();
            ddlEMID_OnTextChanged(sender, e);
        }

        protected void btnPrint_OnClick(object sender, EventArgs e)
        {
            if (Session["USERID"] == null)
            {
                Response.Redirect("~/Login/UI/Login.aspx");
            }
            else
            {
                Session["Date"] = txtDate.Text;
                Session["MonthYear"] = txtMY.Text;
                Session["EMPNM"] = ddlEMID.SelectedValue;
                Session["Remarks"] = txtRemarks.Text;
                if (btnEdit.Text == "Edit")
                    Session["Transno"] = txtTransNo.Text;
                else
                    Session["Transno"] = ddlTransNo.Text;
                Page.ClientScript.RegisterStartupScript(
                    this.GetType(), "OpenWindow",
                    "window.open('../../Payroll/Report/Report/rptLeaveApplication-mamo.aspx','_newtab');", true);
            }

        }

        protected void btnUpdateLeaveBY_OnClick(object sender, EventArgs e)
        {
            iob.Ipaddress = Session["IpAddress"].ToString();
            iob.UserID = Convert.ToInt32(Session["USERID"].ToString());
            iob.Ltude = Session["Location"].ToString();
            iob.InTime = dbFunctions.Timezone(DateTime.Now);
            iob.UserPC = HttpContext.Current.Session["PCName"].ToString();

            iob.Date = DateTime.Parse(txtDate.Text, dateformat, System.Globalization.DateTimeStyles.AssumeLocal);
            iob.MonthYear = txtMY.Text;
            iob.EmployeeID = Convert.ToInt64(ddlEMID.SelectedValue);
            iob.Remarks = txtRemarks.Text;

            if (btnEdit.Text == "Edit")
                iob.TRANSNO = Convert.ToInt64(txtTransNo.Text);
            else
                iob.TRANSNO = Convert.ToInt64(ddlTransNo.Text);

            dob.Update_HR_LEAVE_APP_MST(iob);
            lblError.Visible = true;
            lblError.Text = "Data Successfully Updated. !";
        }
    }
}

