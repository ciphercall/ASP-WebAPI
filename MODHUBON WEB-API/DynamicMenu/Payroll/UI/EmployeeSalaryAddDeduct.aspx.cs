using System;
using System.Data;
using System.Data.SqlClient;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using AlchemyAccounting;
using DynamicMenu.LogData;
using DynamicMenu.Payroll.DataAccess;
using DynamicMenu.Payroll.Interface;

namespace DynamicMenu.Payroll.UI
{
    public partial class EmployeeSalaryAddDeduct : Page
    {
        readonly PayrollDataAcces _dob = new PayrollDataAcces();
        readonly PayrollInterface _iob = new PayrollInterface();
        readonly SqlConnection _conn = new SqlConnection(dbFunctions.connection);


        protected void Page_Load(object sender, EventArgs e)
        {
            if (Session["USERID"] == null)
            {
                Response.Redirect("~/Login/UI/Login.aspx");
            }
            else
            {
                const string formLink = "/Payroll/UI/EmployeeSalaryAddDeduct.aspx";
                var permission = UserPermissionChecker.checkParmit(formLink, "STATUS");
                if (permission)
                {
                    if (!IsPostBack)
                    {
                        var monyear =dbFunctions.Timezone(DateTime.Now);
                        txtMnYear.Text = monyear.ToString("MMM-yy").ToUpper();

                       dbFunctions.DropDownAddSelectTextWithValue(ddlBranch, "SELECT BRANCHNM, BRANCHCD FROM ASL_BRANCH");

                        var uTp = HttpContext.Current.Session["USERTYPE"].ToString();
                        //var brCD = HttpContext.Current.Session["BrCD"].ToString();
                        //if (uTp == "COMPADMIN")
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

            _conn.Open();
            SqlCommand cmd = new SqlCommand(@"SELECT HR_SALDRCR.COMPID, HR_SALDRCR.EMPID, HR_EMP.EMPNM, HR_SALDRCR.TRANSMY, HR_SALDRCR.BONUSF, HR_SALDRCR.INCENTIVE, HR_SALDRCR.CONVEY, HR_SALDRCR.MOBILE, 
            HR_SALDRCR.DUEADJ,HR_SALDRCR.BONUSP,HR_SALDRCR.ITAX,HR_SALDRCR.FOODINGADD, HR_SALDRCR.ADVANCE,HR_SALDRCR.OTHOUR, HR_SALDRCR.FOODING, HR_SALDRCR.FINEADJ, HR_SALDRCR.DAYLEAVE, HR_SALDRCR.REMARKS
            FROM HR_SALDRCR INNER JOIN
            HR_EMP ON HR_SALDRCR.EMPID = HR_EMP.EMPID
            WHERE  HR_SALDRCR.TRANSMY='" + txtMnYear.Text + "' AND HR_SALDRCR.BRANCHCD='" + ddlBranch.SelectedValue + "' ORDER BY HR_SALDRCR.INSTIME", _conn);

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
            HR_EMP.COSTPID = ASL_BRANCH.BRANCHCD WHERE ASL_BRANCH.BRANCHCD = '" + ddlBranch.SelectedValue + "' EXCEPT SELECT HR_EMP.EMPNM, HR_EMP.EMPID " +
            "FROM HR_EMP INNER JOIN ASL_BRANCH ON HR_EMP.COSTPID = ASL_BRANCH.BRANCHCD INNER JOIN HR_SALDRCR ON HR_EMP.COMPID = HR_SALDRCR.COMPID " +
            "AND HR_EMP.EMPID = HR_SALDRCR.EMPID WHERE(ASL_BRANCH.BRANCHCD = '" + ddlBranch.SelectedValue + "') AND(TRANSMY = '" + txtMnYear.Text + "') " +
            "ORDER BY HR_EMP.EMPNM");
            ddlEmployeeNameFooter.Focus();

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
                if (Session["USERID"] == null)
                    Response.Redirect("~/Login/UI/Login.aspx");
                else if (txtMnYear.Text == "")
                    txtMnYear.Focus();
                else
                {
                    var ddlEmployeeNameFooter = (DropDownList)gvdetails.FooterRow.FindControl("ddlEmployeeNameFooter");

                    var txtBonusfFooter = (TextBox)gvdetails.FooterRow.FindControl("txtBONUSFFooter");
                    var txtIncentiveFooter = (TextBox)gvdetails.FooterRow.FindControl("txtINCENTIVEFooter");

                    var txtConveyFooter = (TextBox)gvdetails.FooterRow.FindControl("txtCONVEYFooter");
                    var txtMobileFooter = (TextBox)gvdetails.FooterRow.FindControl("txtMOBILEFooter");
                    var txtDueadjFooter = (TextBox)gvdetails.FooterRow.FindControl("txtDUEADJFooter");
                    var txtAdvanceFooter = (TextBox)gvdetails.FooterRow.FindControl("txtADVANCEFooter");

                    var txtFoodingFooter = (TextBox)gvdetails.FooterRow.FindControl("txtFOODINGFooter");
                    var txtFineFooter = (TextBox)gvdetails.FooterRow.FindControl("txtFINEFooter");
                    var txtDayLeaveFooter = (TextBox)gvdetails.FooterRow.FindControl("txtDayLeaveFooter");
                    var txtRemarksFooter = (TextBox)gvdetails.FooterRow.FindControl("txtRemarksFooter");
                    var txtOTHOURFooter = (TextBox)gvdetails.FooterRow.FindControl("txtOTHOURFooter");

                    var txtFOODINGADDFooter = (TextBox)gvdetails.FooterRow.FindControl("txtFOODINGADDFooter");
                    var txtBONUSPFooter = (TextBox)gvdetails.FooterRow.FindControl("txtBONUSPFooter");
                    var txtITAXFooter = (TextBox)gvdetails.FooterRow.FindControl("txtITAXFooter");


                    _iob.CmpID = int.Parse(Session["COMPANYID"].ToString());
                    _iob.TransMonthYear = txtMnYear.Text.ToUpper();
                    _iob.BranchCode = Convert.ToInt64(ddlBranch.SelectedValue);

                    _iob.Bonusf = txtBonusfFooter.Text == "" ? 0 : Convert.ToDecimal(txtBonusfFooter.Text);
                    _iob.Incentive = txtIncentiveFooter.Text == "" ? 0 : Convert.ToDecimal(txtIncentiveFooter.Text);

                    _iob.Convey = txtConveyFooter.Text == "" ? 0 : Convert.ToDecimal(txtConveyFooter.Text);
                    _iob.Mobile = txtMobileFooter.Text == "" ? 0 : Convert.ToDecimal(txtMobileFooter.Text);
                    _iob.Dueadj = txtDueadjFooter.Text == "" ? 0 : Convert.ToDecimal(txtDueadjFooter.Text);
                    _iob.Advance = txtAdvanceFooter.Text == "" ? 0 : Convert.ToDecimal(txtAdvanceFooter.Text);

                    _iob.Fooding = txtFoodingFooter.Text == "" ? 0 : Convert.ToDecimal(txtFoodingFooter.Text);
                    _iob.Fine = txtFineFooter.Text == "" ? 0 : Convert.ToDecimal(txtFineFooter.Text);
                    _iob.DayLeave = txtDayLeaveFooter.Text == "" ? 0 : Convert.ToDecimal(txtDayLeaveFooter.Text);
                    _iob.Remarks = txtRemarksFooter.Text;
                    _iob.Othour = txtOTHOURFooter.Text == "" ? 0 : Convert.ToDecimal(txtOTHOURFooter.Text);

                    _iob.Foodinadd = txtFOODINGADDFooter.Text == "" ? 0 : Convert.ToDecimal(txtFOODINGADDFooter.Text);
                    _iob.BonusP = txtBONUSPFooter.Text == "" ? 0 : Convert.ToDecimal(txtBONUSPFooter.Text);
                    _iob.ITax = txtITAXFooter.Text == "" ? 0 : Convert.ToDecimal(txtITAXFooter.Text);

                    if (e.CommandName.Equals("Add"))
                    {
                        if (ddlEmployeeNameFooter.Text == "" || ddlEmployeeNameFooter.Text == "--SELECT--")
                        {
                            ddlEmployeeNameFooter.Focus();
                        }
                        else
                        {
                            _iob.EMPID = Convert.ToInt32(ddlEmployeeNameFooter.SelectedValue);
                            _dob.Insert_HR_Salary_Add(_iob);
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
                    var lblCompanyId = (Label)gvdetails.Rows[e.RowIndex].FindControl("lblCompanyId");
                    var lblTransMy = (Label)gvdetails.Rows[e.RowIndex].FindControl("lblTransMy");
                    var lblEmpId = (Label)gvdetails.Rows[e.RowIndex].FindControl("lblEmpId");

                    // logdata add start //
                    try
                    {
                        string ipAdd = HttpContext.Current.Session["IpAddress"].ToString();
                        string lotileng = "";
                        string logdata =dbFunctions.StringData(@"SELECT CONVERT(NVARCHAR(50),COMPID,103)+'  '+CONVERT(NVARCHAR(50),EMPID,103)+'  '+TRANSMY+'  '
                        +CONVERT(NVARCHAR(50),BRANCHCD,103)+'  '+ISNULL(CONVERT(NVARCHAR(50),BONUSF,103),'(NULL)')+'  '+ISNULL(CONVERT(NVARCHAR(50),
                        INCENTIVE,103),'(NULL)')+'  '+ISNULL(CONVERT(NVARCHAR(50),CONVEY,103),'(NULL)')+'  '+ISNULL(CONVERT(NVARCHAR(50),MOBILE,103),
                        '(NULL)')+'  '+ISNULL(CONVERT(NVARCHAR(50),DUEADJ,103),'(NULL)')+'  '+ISNULL(CONVERT(NVARCHAR(50),ADVANCE,103),'(NULL)')+'  '
                        +ISNULL(CONVERT(NVARCHAR(50),FOODING,103),'(NULL)')+'  '+ISNULL(CONVERT(NVARCHAR(50),FINEADJ,103),'(NULL)')+'  '+ISNULL(REMARKS,
                        '(NULL)')+'  '+ISNULL(USERPC,'(NULL)')+'  '+ISNULL(CONVERT(NVARCHAR(50),INSUSERID,103),'(NULL)')+'  '+ISNULL(CONVERT(NVARCHAR(50),
                        INSTIME,103),'(NULL)')+'  '+ISNULL(INSIPNO,'(NULL)')+'  '+ISNULL(INSLTUDE,'(NULL)')+'  '+ISNULL(CONVERT(NVARCHAR(50),UPDUSERID,103),
                        '(NULL)')+'  '+ISNULL(CONVERT(NVARCHAR(50),UPDTIME,103),'(NULL)')+'  '+ISNULL(UPDIPNO,'(NULL)')+'  '+ISNULL(UPDLTUDE,'(NULL)')+'  '
                        +ISNULL(CONVERT(NVARCHAR(50),POSTID,103),'(NULL)') FROM HR_SALDRCR WHERE 
                        COMPID='" + lblCompanyId.Text + "' AND EMPID='" + lblEmpId.Text + "' AND TRANSMY='" + lblTransMy.Text + "' AND BRANCHCD='" + ddlBranch.SelectedValue + "'");
                        string logid = "DELETE";
                        string tableid = "HR_SALDRCR";
                        LogDataFunction.InsertLogData(lotileng, logid, tableid, logdata, ipAdd);
                    }
                    catch (Exception)
                    {
                        //ignore
                    }
                    // logdata add END //

                    _conn.Open();
                    SqlCommand cmd = new SqlCommand(@"DELETE FROM HR_SALDRCR WHERE 
                    COMPID='" + lblCompanyId.Text + "' AND EMPID='" + lblEmpId.Text + "' AND TRANSMY='" + lblTransMy.Text + "' AND BRANCHCD='" + ddlBranch.SelectedValue + "'", _conn);
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
                var txtBonusfEdit = (TextBox)gvdetails.Rows[e.NewEditIndex].FindControl("txtBONUSFEdit");
                txtBonusfEdit.Focus();
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

                var lblCompanyIdEdit = (Label)gvdetails.Rows[e.RowIndex].FindControl("lblCompanyIdEdit");
                var lblTransMyEdit = (Label)gvdetails.Rows[e.RowIndex].FindControl("lblTransMyEdit");
                var lblEmpIdEdit = (Label)gvdetails.Rows[e.RowIndex].FindControl("lblEmpIdEdit");

                var txtBonusfFooter = (TextBox)gvdetails.Rows[e.RowIndex].FindControl("txtBONUSFEdit");
                var txtIncentiveFooter = (TextBox)gvdetails.Rows[e.RowIndex].FindControl("txtINCENTIVEEdit");

                var txtConveyFooter = (TextBox)gvdetails.Rows[e.RowIndex].FindControl("txtCONVEYEdit");
                var txtMobileFooter = (TextBox)gvdetails.Rows[e.RowIndex].FindControl("txtMOBILEEdit");
                var txtDueadjFooter = (TextBox)gvdetails.Rows[e.RowIndex].FindControl("txtDUEADJEdit");
                var txtAdvanceFooter = (TextBox)gvdetails.Rows[e.RowIndex].FindControl("txtADVANCEEdit");

                var txtFoodingFooter = (TextBox)gvdetails.Rows[e.RowIndex].FindControl("txtFOODINGEdit");
                var txtFineFooter = (TextBox)gvdetails.Rows[e.RowIndex].FindControl("txtFINEEdit");
                var txtDayLeaveEdit = (TextBox)gvdetails.Rows[e.RowIndex].FindControl("txtDayLeaveEdit");
                var txtRemarksEdit = (TextBox)gvdetails.Rows[e.RowIndex].FindControl("txtRemarksEdit");
                var txtOTHOUREdit = (TextBox)gvdetails.Rows[e.RowIndex].FindControl("txtOTHOUREdit");

                var txtBONUSPEdit = (TextBox)gvdetails.Rows[e.RowIndex].FindControl("txtBONUSPEdit");
                var txtFOODINGADDEdit = (TextBox)gvdetails.Rows[e.RowIndex].FindControl("txtFOODINGADDEdit");
                var txtITAXEdit = (TextBox)gvdetails.Rows[e.RowIndex].FindControl("txtITAXEdit");

                _iob.CmpID = int.Parse(lblCompanyIdEdit.Text);
                _iob.EMPID = Convert.ToInt32(lblEmpIdEdit.Text);
                _iob.TransMonthYear = lblTransMyEdit.Text;
                _iob.BranchCode = Convert.ToInt64(ddlBranch.SelectedValue);
                _iob.Bonusf = txtBonusfFooter.Text == "" ? 0 : Convert.ToDecimal(txtBonusfFooter.Text);
                _iob.Incentive = txtIncentiveFooter.Text == "" ? 0 : Convert.ToDecimal(txtIncentiveFooter.Text);

                _iob.Convey = txtConveyFooter.Text == "" ? 0 : Convert.ToDecimal(txtConveyFooter.Text);
                _iob.Mobile = txtMobileFooter.Text == "" ? 0 : Convert.ToDecimal(txtMobileFooter.Text);
                _iob.Dueadj = txtDueadjFooter.Text == "" ? 0 : Convert.ToDecimal(txtDueadjFooter.Text);
                _iob.Advance = txtAdvanceFooter.Text == "" ? 0 : Convert.ToDecimal(txtAdvanceFooter.Text);

                _iob.Fooding = txtFoodingFooter.Text == "" ? 0 : Convert.ToDecimal(txtFoodingFooter.Text);
                _iob.Fine = txtFineFooter.Text == "" ? 0 : Convert.ToDecimal(txtFineFooter.Text);
                _iob.DayLeave = txtDayLeaveEdit.Text == "" ? 0 : Convert.ToDecimal(txtDayLeaveEdit.Text);
                _iob.Remarks = txtRemarksEdit.Text;
                _iob.Othour = txtOTHOUREdit.Text == "" ? 0 : Convert.ToDecimal(txtOTHOUREdit.Text);

                _iob.Foodinadd = txtFOODINGADDEdit.Text == "" ? 0 : Convert.ToDecimal(txtFOODINGADDEdit.Text);
                _iob.BonusP = txtBONUSPEdit.Text == "" ? 0 : Convert.ToDecimal(txtBONUSPEdit.Text);
                _iob.ITax = txtITAXEdit.Text == "" ? 0 : Convert.ToDecimal(txtITAXEdit.Text);


                if (txtMnYear.Text == "")
                    txtMnYear.Focus();
                else
                {
                    // logdata add start //
                    try
                    {
                        string ipAdd = HttpContext.Current.Session["IpAddress"].ToString();
                        string lotileng = "";
                        string logdata =dbFunctions.StringData(@"SELECT CONVERT(NVARCHAR(50),COMPID,103)+'  '+CONVERT(NVARCHAR(50),EMPID,103)+'  '+TRANSMY+'  '
                        +CONVERT(NVARCHAR(50),BRANCHCD,103)+'  '+ISNULL(CONVERT(NVARCHAR(50),BONUSF,103),'(NULL)')+'  '+ISNULL(CONVERT(NVARCHAR(50),
                        INCENTIVE,103),'(NULL)')+'  '+ISNULL(CONVERT(NVARCHAR(50),CONVEY,103),'(NULL)')+'  '+ISNULL(CONVERT(NVARCHAR(50),MOBILE,103),
                        '(NULL)')+'  '+ISNULL(CONVERT(NVARCHAR(50),DUEADJ,103),'(NULL)')+'  '+ISNULL(CONVERT(NVARCHAR(50),ADVANCE,103),'(NULL)')+'  '
                        +ISNULL(CONVERT(NVARCHAR(50),FOODING,103),'(NULL)')+'  '+ISNULL(CONVERT(NVARCHAR(50),FINEADJ,103),'(NULL)')+'  '+ISNULL(REMARKS,
                        '(NULL)')+'  '+ISNULL(USERPC,'(NULL)')+'  '+ISNULL(CONVERT(NVARCHAR(50),INSUSERID,103),'(NULL)')+'  '+ISNULL(CONVERT(NVARCHAR(50),
                        INSTIME,103),'(NULL)')+'  '+ISNULL(INSIPNO,'(NULL)')+'  '+ISNULL(INSLTUDE,'(NULL)')+'  '+ISNULL(CONVERT(NVARCHAR(50),UPDUSERID,103),
                        '(NULL)')+'  '+ISNULL(CONVERT(NVARCHAR(50),UPDTIME,103),'(NULL)')+'  '+ISNULL(UPDIPNO,'(NULL)')+'  '+ISNULL(UPDLTUDE,'(NULL)')+'  '
                        +ISNULL(CONVERT(NVARCHAR(50),POSTID,103),'(NULL)') FROM HR_SALDRCR WHERE 
                        COMPID='" + _iob.CmpID + "' AND EMPID='" + _iob.EMPID + "' AND TRANSMY='" + _iob.TransMonthYear + "' AND BRANCHCD='" + ddlBranch.SelectedValue + "'");
                        string logid = "UPDATE";
                        string tableid = "HR_SALDRCR";
                        LogDataFunction.InsertLogData(lotileng, logid, tableid, logdata, ipAdd);
                    }
                    catch (Exception)
                    {
                        //ignore
                    }
                    // logdata add END //

                    _dob.Update_HR_Salary_Add(_iob);
                    gvdetails.EditIndex = -1;
                    GridShow();
                }



            }
        }


    }
}