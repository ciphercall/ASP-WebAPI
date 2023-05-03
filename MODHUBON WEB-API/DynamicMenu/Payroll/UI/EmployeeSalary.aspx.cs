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
//using DynamicMenu.DDSO.Account;
using DynamicMenu.LogData;
using DynamicMenu.Payroll.DataAccess;
using DynamicMenu.Payroll.Interface;

namespace DynamicMenu.Payroll.UI
{
    public partial class EmployeeSalary : System.Web.UI.Page
    {
        IFormatProvider dateformat = new System.Globalization.CultureInfo("fr-FR", true);
        PayrollDataAcces dob = new PayrollDataAcces();
        PayrollInterface iob = new PayrollInterface();
        SqlConnection conn = new SqlConnection(dbFunctions.connection);
        protected void Page_Load(object sender, EventArgs e)
        {
            if (Session["USERID"] == null)
            {
                Response.Redirect("~/Login/UI/Login.aspx");
            }
            else
            {
                const string formLink = "/Payroll/UI/EmployeeSalary.aspx";
                bool permission = UserPermissionChecker.checkParmit(formLink, "STATUS");
                if (permission == true)
                {
                    if (!Page.IsPostBack)
                    {
                        dbFunctions.dropDownAddWithSelect(ddlEmp, "SELECT EMPNM FROM HR_EMP");
                        ddlEmp.Focus();
                    }
                }
                else
                {
                    Response.Redirect("../../DeshBoard/UI/Default.aspx");
                }
            }
        }
        //[System.Web.Services.WebMethodAttribute(), System.Web.Script.Services.ScriptMethodAttribute()]
        //public static string[] GetCompletionPostNM(string prefixText, int count, string contextKey)
        //{

        //    SqlConnection conn = new SqlConnection(dbFunctions.connection);
        //    SqlCommand cmd = new SqlCommand("SELECT POSTNM FROM HR_POST WHERE POSTNM like '" + prefixText + "%'", conn);
        //    SqlDataReader oReader;
        //    conn.Open();
        //    List<String> CompletionSet = new List<string>();
        //    oReader = cmd.ExecuteReader(CommandBehavior.CloseConnection);
        //    while (oReader.Read())
        //        CompletionSet.Add(oReader["POSTNM"].ToString());
        //    return CompletionSet.ToArray();
        //}
        private void gridShow()
        {
            conn.Open();
            SqlCommand cmd = new SqlCommand(@"SELECT HR_POST.POSTNM, HR_EMPSALARY.POSTID, HR_EMPSALARY.SALSTATUS, HR_EMPSALARY.BASICSAL, HR_EMPSALARY.HOUSERENT, HR_EMPSALARY.MEDICAL, 
                      HR_EMPSALARY.TRANSPORT,HR_EMPSALARY.CONVEY, HR_EMPSALARY.RSTAMP, ISNULL(HR_EMPSALARY.OTRTHOUR,0) OTRTHOUR,ISNULL(HR_EMPSALARY.PBONUSRT,0) PBONUSRT, ISNULL(HR_EMPSALARY.OTRTDAY,0) OTRTDAY, HR_EMPSALARY.PFRATE, CONVERT(NVARCHAR(10),HR_EMPSALARY.PFEFDT,103) AS PFEFDT, CONVERT(NVARCHAR(10),HR_EMPSALARY.PFETDT,103) AS PFETDT, CONVERT(NVARCHAR(10),HR_EMPSALARY.JOBEFDT,103) AS JOBEFDT, 
                      CONVERT(NVARCHAR(10),HR_EMPSALARY.JOBETDT,103) AS JOBETDT
                      FROM HR_EMPSALARY INNER JOIN
                      HR_POST ON HR_EMPSALARY.POSTID = HR_POST.POSTID WHERE HR_EMPSALARY.EMPID='" + lblEmpID.Text + "' ORDER BY HR_EMPSALARY.POSTID", conn);

            SqlDataAdapter da = new SqlDataAdapter(cmd);
            DataSet ds = new DataSet();
            da.Fill(ds);
            conn.Close();
            if (ds.Tables[0].Rows.Count > 0)
            {
                gv_Salary.DataSource = ds;
                gv_Salary.DataBind();
            }
            else
            {
                ds.Tables[0].Rows.Add(ds.Tables[0].NewRow());
                gv_Salary.DataSource = ds;
                gv_Salary.DataBind();
                int columncount = gv_Salary.Rows[0].Cells.Count;
                gv_Salary.Rows[0].Cells.Clear();
                gv_Salary.Rows[0].Cells.Add(new TableCell());
                gv_Salary.Rows[0].Cells[0].ColumnSpan = columncount;
                gv_Salary.Rows[0].Visible = false;
            }
            TextBox txtPOSTNMFooter = (TextBox)gv_Salary.FooterRow.FindControl("txtPOSTNMFooter");
            txtPOSTNMFooter.Focus();
        }

        protected void ddlEmp_SelectedIndexChanged(object sender, EventArgs e)
        {
            dbFunctions.lblAdd("SELECT EMPID FROM HR_EMP WHERE EMPNM='" + ddlEmp.Text + "'", lblEmpID);
            gridShow();
        }

        protected void gv_Salary_RowCommand(object sender, GridViewCommandEventArgs e)
        {
            iob.UserPC = HttpContext.Current.Session["PCName"].ToString();
            iob.Username = HttpContext.Current.Session["USERID"].ToString();
            iob.Ipaddress = HttpContext.Current.Session["IpAddress"].ToString();
            iob.ITime = dbFunctions.Timezone(DateTime.Now);
            iob.CmpID = int.Parse(Session["COMPANYID"].ToString());
            iob.UserID = int.Parse(Session["USERID"].ToString());
            if (e.CommandName.Equals("Add"))
            {
                TextBox txtPOSTNMFooter = (TextBox)gv_Salary.FooterRow.FindControl("txtPOSTNMFooter");
                Label lblPOSTIDFooter = (Label)gv_Salary.FooterRow.FindControl("lblPOSTIDFooter");
                DropDownList ddlStatsFooter = (DropDownList)gv_Salary.FooterRow.FindControl("ddlStatsFooter");
                TextBox txtBASICSALFooter = (TextBox)gv_Salary.FooterRow.FindControl("txtBASICSALFooter");
                TextBox txtHOUSERENTFooter = (TextBox)gv_Salary.FooterRow.FindControl("txtHOUSERENTFooter");
                TextBox txtMEDICALFooter = (TextBox)gv_Salary.FooterRow.FindControl("txtMEDICALFooter");
                TextBox txtTRANSPORTFooter = (TextBox)gv_Salary.FooterRow.FindControl("txtTRANSPORTFooter");
                TextBox txtRSTAMPFooter = (TextBox)gv_Salary.FooterRow.FindControl("txtRSTAMPFooter");
                TextBox txtPFRATEFooter = (TextBox)gv_Salary.FooterRow.FindControl("txtPFRATEFooter");
                TextBox txtPFEFDTFooter = (TextBox)gv_Salary.FooterRow.FindControl("txtPFEFDTFooter");
                TextBox txtPFETDTFooter = (TextBox)gv_Salary.FooterRow.FindControl("txtPFETDTFooter");
                TextBox txtJOBEFDTFooter = (TextBox)gv_Salary.FooterRow.FindControl("txtJOBEFDTFooter");
                TextBox txtJOBETDTFooter = (TextBox)gv_Salary.FooterRow.FindControl("txtJOBETDTFooter");
                TextBox txtOTRTHOURFooter = (TextBox)gv_Salary.FooterRow.FindControl("txtOTRTHRFooter");
                TextBox txtPBONUSRTFooter = (TextBox)gv_Salary.FooterRow.FindControl("txtPBONUSRTFooter");
                TextBox txtOTRTDAYFooter = (TextBox)gv_Salary.FooterRow.FindControl("txtOTRTDAYFooter");
                TextBox txtCONVEYFooter = (TextBox)gv_Salary.FooterRow.FindControl("txtCONVEYFooter");
                if (lblPOSTIDFooter.Text == "")
                    txtPOSTNMFooter.Focus();
                else if (ddlStatsFooter.Text == "--SELECT--")
                    ddlStatsFooter.Focus();
                else if (txtBASICSALFooter.Text == "")
                    txtBASICSALFooter.Focus();
                else if (txtHOUSERENTFooter.Text == "")
                    txtHOUSERENTFooter.Focus();
                else if (txtMEDICALFooter.Text == "")
                    txtMEDICALFooter.Focus();
                else if (txtJOBEFDTFooter.Text == "")
                    txtJOBEFDTFooter.Focus();
                else if (txtJOBETDTFooter.Text == "")
                    txtJOBETDTFooter.Focus();
                else if (txtCONVEYFooter.Text == "")
                    txtCONVEYFooter.Focus();
                else
                {

                    if (txtPFEFDTFooter.Text == "")
                    {
                        iob.PFEffectFR = Convert.ToDateTime("01/01/1900");
                    }
                    else
                    {
                        DateTime PFEFDT = DateTime.Parse(txtPFEFDTFooter.Text, dateformat, System.Globalization.DateTimeStyles.AssumeLocal);
                        iob.PFEffectFR = PFEFDT;
                    }
                    if (txtPFETDTFooter.Text == "")
                    {
                        iob.PFEffectTO = Convert.ToDateTime("01/01/1900");
                    }
                    else
                    {
                        DateTime PFETDT = DateTime.Parse(txtPFETDTFooter.Text, dateformat, System.Globalization.DateTimeStyles.AssumeLocal);
                        iob.PFEffectTO = PFETDT;
                    }
                    DateTime JOBEFDT = DateTime.Parse(txtJOBEFDTFooter.Text, dateformat, System.Globalization.DateTimeStyles.AssumeLocal);
                    DateTime JOBETDT = DateTime.Parse(txtJOBETDTFooter.Text, dateformat, System.Globalization.DateTimeStyles.AssumeLocal);
                    iob.PostID = int.Parse(lblPOSTIDFooter.Text);
                    iob.EMPID = int.Parse(lblEmpID.Text);
                    iob.Status = ddlStatsFooter.Text;
                    iob.BasicSal = Decimal.Parse(txtBASICSALFooter.Text);
                    iob.HouseRent = Decimal.Parse(txtHOUSERENTFooter.Text);
                    iob.Medical = Decimal.Parse(txtMEDICALFooter.Text);
                    iob.TrnsPort = Decimal.Parse(txtTRANSPORTFooter.Text);
                    iob.Revenue = Decimal.Parse(txtRSTAMPFooter.Text);
                    iob.PFRate = Decimal.Parse(txtPFRATEFooter.Text);
                    iob.OTRThour = Decimal.Parse(txtOTRTHOURFooter.Text);
                    iob.PresentRT = Decimal.Parse(txtPBONUSRTFooter.Text);
                    iob.OTRTday = Decimal.Parse(txtOTRTDAYFooter.Text);
                    iob.Convey = decimal.Parse(txtCONVEYFooter.Text);
                    iob.JOBEffectFR = JOBEFDT;
                    iob.JOBEffectTO = JOBETDT;
                    dob.INSERT_HR_EMPSALARY(iob);
                    gridShow();
                }
            }
        }

        protected void txtPOSTNMFooter_TextChanged(object sender, EventArgs e)
        {
            TextBox txtPOSTNMFooter = (TextBox)gv_Salary.FooterRow.FindControl("txtPOSTNMFooter");
            Label lblPOSTIDFooter = (Label)gv_Salary.FooterRow.FindControl("lblPOSTIDFooter");
            DropDownList ddlStatsFooter = (DropDownList)gv_Salary.FooterRow.FindControl("ddlStatsFooter");
            if (txtPOSTNMFooter.Text != "")
            {
                dbFunctions.lblAdd("SELECT POSTID FROM HR_POST WHERE POSTNM='" + txtPOSTNMFooter.Text + "'", lblPOSTIDFooter);
                ddlStatsFooter.Focus();
            }
        }

        protected void txtPOSTNMEdit_TextChanged(object sender, EventArgs e)
        {
            GridViewRow row = ((GridViewRow)((TextBox)sender).NamingContainer);
            TextBox txtPOSTNMEdit = (TextBox)row.FindControl("txtPOSTNMEdit");
            DropDownList ddlStatsEdit = (DropDownList)row.FindControl("ddlStatsEdit");
            Label lblPOSTIDEdit = (Label)row.FindControl("lblPOSTIDEdit");
            if (txtPOSTNMEdit.Text != "")
            {
                dbFunctions.lblAdd("SELECT POSTID FROM HR_POST WHERE POSTNM='" + txtPOSTNMEdit.Text + "'", lblPOSTIDEdit);
                ddlStatsEdit.Focus();

            }
        }

        protected void gv_Salary_RowCancelingEdit(object sender, GridViewCancelEditEventArgs e)
        {
            if (Session["USERID"] == null)
                Response.Redirect("~/Login/UI/Login.aspx");
            else
            {
                gv_Salary.EditIndex = -1;
                gridShow();
            }
        }

        protected void gv_Salary_RowDeleting(object sender, GridViewDeleteEventArgs e)
        {
            Label lblPOSTID = (Label)gv_Salary.Rows[e.RowIndex].FindControl("lblPOSTID");
            Label lblStatus = (Label)gv_Salary.Rows[e.RowIndex].FindControl("lblStatus");

            iob.PostID = int.Parse(lblPOSTID.Text);
            iob.Status = lblStatus.Text;
            iob.CmpID = int.Parse(Session["COMPANYID"].ToString());
            iob.EMPID = int.Parse(lblEmpID.Text);

            try
            {
                // logdata add start //
                string ipAdd = HttpContext.Current.Session["IpAddress"].ToString();
                string lotileng = "";
                string logdata = dbFunctions.StringData(@"SELECT CONVERT(NVARCHAR(50),COMPID,103)+'  '+CONVERT(NVARCHAR(50),EMPID,103)+'  '+CONVERT(NVARCHAR(50),POSTID,103)+' 
                    '+SALSTATUS+'  '+ISNULL(CONVERT(NVARCHAR(50),BASICSAL,103),'(NULL)')+'  '+ISNULL(CONVERT(NVARCHAR(50),HOUSERENT,103),'(NULL)')+'  '+ISNULL(CONVERT(NVARCHAR(50),MEDICAL,103),'(NULL)')+'  
                    '+ISNULL(CONVERT(NVARCHAR(50),TRANSPORT,103),'(NULL)')+'  '+ISNULL(CONVERT(NVARCHAR(50),RSTAMP,103),'(NULL)')+' 
                    '+ISNULL(CONVERT(NVARCHAR(50),PFRATE,103),'(NULL)')+'  '+ISNULL(CONVERT(NVARCHAR(50),PFEFDT,103),'(NULL)')+' 
                    '+ISNULL(CONVERT(NVARCHAR(50),PFETDT,103),'(NULL)')+'  '+ISNULL(CONVERT(NVARCHAR(50),JOBEFDT,103),'(NULL)')+' 
                    '+ISNULL(CONVERT(NVARCHAR(50),JOBETDT,103),'(NULL)')+'  '+ISNULL(USERPC,'(NULL)')+'  
                    '+ISNULL(CONVERT(NVARCHAR(50),INSUSERID,103),'(NULL)')+'  '+ISNULL(CONVERT(NVARCHAR(50),INSTIME,103),'(NULL)')+'  
                    '+ISNULL(INSIPNO,'(NULL)')+'  '+ISNULL(INSLTUDE,'(NULL)')+'  '+ISNULL(CONVERT(NVARCHAR(50),UPDUSERID,103),'(NULL)')+' 
                    '+ISNULL(CONVERT(NVARCHAR(50),UPDTIME,103),'(NULL)')+'  '+ISNULL(UPDIPNO,'(NULL)')+'  '+ISNULL(UPDLTUDE,'(NULL)')+' 
                    '+ISNULL(CONVERT(NVARCHAR(50),OTRTHOUR,103),'(NULL)')+'  '+ISNULL(CONVERT(NVARCHAR(50),PBONUSRT,103),'(NULL)')+' 
                    '+ISNULL(CONVERT(NVARCHAR(50),OTRTDAY,103),'(NULL)') FROM HR_EMPSALARY 
                    WHERE POSTID='" + lblPOSTID.Text + "' AND COMPID='" + iob.CmpID + "' AND EMPID='" + lblEmpID.Text + "' AND SALSTATUS='" + lblStatus.Text + "'");
                string logid = "DELETE";
                string tableid = "HR_EMPSALARY";
                LogDataFunction.InsertLogData(lotileng, logid, tableid, logdata, ipAdd);
                // logdata add start //
            }
            catch (Exception)
            {
            }

            dob.DELETE_HR_EMPSALARY(iob);
            gridShow();
        }

        protected void gv_Salary_RowEditing(object sender, GridViewEditEventArgs e)
        {
            if (Session["USERID"] == null)
                Response.Redirect("~/Login/UI/Login.aspx");
            else
            {
                gv_Salary.EditIndex = e.NewEditIndex;
                gridShow();
                var ddlStatsEdit = (DropDownList)gv_Salary.Rows[e.NewEditIndex].FindControl("ddlStatsEdit");
                ddlStatsEdit.Focus();
            }
        }

        protected void gv_Salary_RowUpdating(object sender, GridViewUpdateEventArgs e)
        {
            iob.UPDUserPC = HttpContext.Current.Session["PCName"].ToString();
            iob.UPDUsername = HttpContext.Current.Session["USERID"].ToString();
            iob.UPDIpaddress = HttpContext.Current.Session["IpAddress"].ToString();
            iob.UPDTime = dbFunctions.Timezone(DateTime.Now);
            iob.CmpID = int.Parse(Session["COMPANYID"].ToString());
            iob.UserID = int.Parse(Session["USERID"].ToString());

            var txtPOSTNMEdit = (TextBox)gv_Salary.Rows[e.RowIndex].FindControl("txtPOSTNMEdit");
            var lblPOSTIDEdit = (Label)gv_Salary.Rows[e.RowIndex].FindControl("lblPOSTIDEdit");
            var ddlStatsEdit = (DropDownList)gv_Salary.Rows[e.RowIndex].FindControl("ddlStatsEdit");
            var txtBASICSALEdit = (TextBox)gv_Salary.Rows[e.RowIndex].FindControl("txtBASICSALEdit");
            var txtHOUSERENTEdit = (TextBox)gv_Salary.Rows[e.RowIndex].FindControl("txtHOUSERENTEdit");
            var txtMEDICALEdit = (TextBox)gv_Salary.Rows[e.RowIndex].FindControl("txtMEDICALEdit");
            var txtTRANSPORTEdit = (TextBox)gv_Salary.Rows[e.RowIndex].FindControl("txtTRANSPORTEdit");
            var txtRSTAMPEdit = (TextBox)gv_Salary.Rows[e.RowIndex].FindControl("txtRSTAMPEdit");
            var txtPFRATEEdit = (TextBox)gv_Salary.Rows[e.RowIndex].FindControl("txtPFRATEEdit");
            var txtPFEFDTEdit = (TextBox)gv_Salary.Rows[e.RowIndex].FindControl("txtPFEFDTEdit");
            var txtPFETDTEdit = (TextBox)gv_Salary.Rows[e.RowIndex].FindControl("txtPFETDTEdit");
            var txtJOBEFDTEdit = (TextBox)gv_Salary.Rows[e.RowIndex].FindControl("txtJOBEFDTEdit");
            var txtJOBETDTEdit = (TextBox)gv_Salary.Rows[e.RowIndex].FindControl("txtJOBETDTEdit");
            var txtOTRTHOUREdit = (TextBox)gv_Salary.Rows[e.RowIndex].FindControl("txtOTRTHREdit");
            var txtPBONUSRTEdit = (TextBox)gv_Salary.Rows[e.RowIndex].FindControl("txtPBONUSRTEdit");
            var txtOTRTDAYEdit = (TextBox)gv_Salary.Rows[e.RowIndex].FindControl("txtOTRTDAYEdit");
            var txtConveyEdit = (TextBox)gv_Salary.Rows[e.RowIndex].FindControl("txtConveyEdit");
            if (lblPOSTIDEdit.Text == "")
                txtPOSTNMEdit.Focus();
            else if (ddlStatsEdit.Text == "--SELECT--")
                ddlStatsEdit.Focus();
            else if (txtBASICSALEdit.Text == "")
                txtBASICSALEdit.Focus();
            else if (txtHOUSERENTEdit.Text == "")
                txtHOUSERENTEdit.Focus();
            else if (txtMEDICALEdit.Text == "")
                txtMEDICALEdit.Focus();
            else if (txtJOBEFDTEdit.Text == "")
                txtJOBEFDTEdit.Focus();
            else if (txtJOBETDTEdit.Text == "")
                txtJOBETDTEdit.Focus();
            else if (txtConveyEdit.Text == "")
                txtConveyEdit.Focus();
            else
            {
                if (txtPFEFDTEdit.Text == "")
                {
                    iob.PFEffectFR = Convert.ToDateTime("01/01/1900");
                }
                else
                {
                    DateTime PFEFDT = DateTime.Parse(txtPFEFDTEdit.Text, dateformat, System.Globalization.DateTimeStyles.AssumeLocal);
                    iob.PFEffectFR = PFEFDT;
                }
                if (txtPFETDTEdit.Text == "")
                {
                    iob.PFEffectTO = Convert.ToDateTime("01/01/1900");
                }
                else
                {
                    DateTime PFETDT = DateTime.Parse(txtPFETDTEdit.Text, dateformat, System.Globalization.DateTimeStyles.AssumeLocal);
                    iob.PFEffectTO = PFETDT;
                }
                DateTime JOBEFDT = DateTime.Parse(txtJOBEFDTEdit.Text, dateformat, System.Globalization.DateTimeStyles.AssumeLocal);
                DateTime JOBETDT = DateTime.Parse(txtJOBETDTEdit.Text, dateformat, System.Globalization.DateTimeStyles.AssumeLocal);
                iob.PostID = int.Parse(lblPOSTIDEdit.Text);
                iob.EMPID = int.Parse(lblEmpID.Text);
                iob.Status = ddlStatsEdit.Text;
                iob.BasicSal = Decimal.Parse(txtBASICSALEdit.Text);
                iob.HouseRent = Decimal.Parse(txtHOUSERENTEdit.Text);
                iob.Medical = Decimal.Parse(txtMEDICALEdit.Text);
                iob.TrnsPort = Decimal.Parse(txtTRANSPORTEdit.Text);
                iob.Revenue = Decimal.Parse(txtRSTAMPEdit.Text);
                iob.PFRate = Decimal.Parse(txtPFRATEEdit.Text);
                iob.OTRThour = Decimal.Parse(txtOTRTHOUREdit.Text);
                iob.PresentRT = Decimal.Parse(txtPBONUSRTEdit.Text);
                iob.OTRTday = Decimal.Parse(txtOTRTDAYEdit.Text);
                iob.Convey = decimal.Parse(txtConveyEdit.Text);
                iob.JOBEffectFR = JOBEFDT;
                iob.JOBEffectTO = JOBETDT;

                try
                {
                    // logdata add start //
                    string ipAdd = HttpContext.Current.Session["IpAddress"].ToString();
                    string lotileng = "";
                    string logdata = dbFunctions.StringData(@"SELECT CONVERT(NVARCHAR(50),COMPID,103)+'  '+CONVERT(NVARCHAR(50),EMPID,103)+'  '+CONVERT(NVARCHAR(50),POSTID,103)+' 
                    '+SALSTATUS+'  '+ISNULL(CONVERT(NVARCHAR(50),BASICSAL,103),'(NULL)')+'  '+ISNULL(CONVERT(NVARCHAR(50),HOUSERENT,103),'(NULL)')+'  '+ISNULL(CONVERT(NVARCHAR(50),MEDICAL,103),'(NULL)')+'  
                    '+ISNULL(CONVERT(NVARCHAR(50),TRANSPORT,103),'(NULL)')+'  '+ISNULL(CONVERT(NVARCHAR(50),RSTAMP,103),'(NULL)')+' 
                    '+ISNULL(CONVERT(NVARCHAR(50),PFRATE,103),'(NULL)')+'  '+ISNULL(CONVERT(NVARCHAR(50),PFEFDT,103),'(NULL)')+' 
                    '+ISNULL(CONVERT(NVARCHAR(50),PFETDT,103),'(NULL)')+'  '+ISNULL(CONVERT(NVARCHAR(50),JOBEFDT,103),'(NULL)')+' 
                    '+ISNULL(CONVERT(NVARCHAR(50),JOBETDT,103),'(NULL)')+'  '+ISNULL(USERPC,'(NULL)')+'  
                    '+ISNULL(CONVERT(NVARCHAR(50),INSUSERID,103),'(NULL)')+'  '+ISNULL(CONVERT(NVARCHAR(50),INSTIME,103),'(NULL)')+'  
                    '+ISNULL(INSIPNO,'(NULL)')+'  '+ISNULL(INSLTUDE,'(NULL)')+'  '+ISNULL(CONVERT(NVARCHAR(50),UPDUSERID,103),'(NULL)')+' 
                    '+ISNULL(CONVERT(NVARCHAR(50),UPDTIME,103),'(NULL)')+'  '+ISNULL(UPDIPNO,'(NULL)')+'  '+ISNULL(UPDLTUDE,'(NULL)')+' 
                    '+ISNULL(CONVERT(NVARCHAR(50),OTRTHOUR,103),'(NULL)')+'  '+ISNULL(CONVERT(NVARCHAR(50),PBONUSRT,103),'(NULL)')+' 
                    '+ISNULL(CONVERT(NVARCHAR(50),OTRTDAY,103),'(NULL)') FROM HR_EMPSALARY 
                    WHERE POSTID='" + iob.PostID + "' AND COMPID='" + iob.CmpID + "' AND EMPID='" + lblEmpID.Text + "' AND SALSTATUS='" + iob.Status + "'");
                    string logid = "UPDATE";

                    string tableid = "HR_EMPSALARY";
                    LogDataFunction.InsertLogData(lotileng, logid, tableid, logdata, ipAdd);
                    // logdata add start //
                }
                catch (Exception)
                {
                }

                dob.UPDATE_HR_EMPSALARY(iob);
                gv_Salary.EditIndex = -1;
                gridShow();
            }
        }

        protected void txtJOBETDTFooter_TextChanged(object sender, EventArgs e)
        {
            TextBox txtJOBETDTFooter = (TextBox)gv_Salary.FooterRow.FindControl("txtJOBETDTFooter");
            DateTime Date = DateTime.Parse(txtJOBETDTFooter.Text, dateformat, System.Globalization.DateTimeStyles.AssumeLocal);
            txtJOBETDTFooter.Text = Date.ToString("dd/MM/yy");
        }

        protected void txtJOBETDTEdit_TextChanged(object sender, EventArgs e)
        {
            GridViewRow row = ((GridViewRow)((TextBox)sender).NamingContainer);
            TextBox txtJOBETDTEdit = (TextBox)row.FindControl("txtJOBETDTEdit");
            DateTime Date = DateTime.Parse(txtJOBETDTEdit.Text, dateformat, System.Globalization.DateTimeStyles.AssumeLocal);
            txtJOBETDTEdit.Text = Date.ToString("dd/MM/yy");
        }

        protected void txtJOBEFDTFooter_TextChanged(object sender, EventArgs e)
        {
            TextBox txtJOBEFDTFooter = (TextBox)gv_Salary.FooterRow.FindControl("txtJOBEFDTFooter");
            DateTime Date = DateTime.Parse(txtJOBEFDTFooter.Text, dateformat, System.Globalization.DateTimeStyles.AssumeLocal);
            txtJOBEFDTFooter.Text = Date.ToString("dd/MM/yy");
        }

        protected void txtJOBEFDTEdit_TextChanged(object sender, EventArgs e)
        {
            GridViewRow row = ((GridViewRow)((TextBox)sender).NamingContainer);
            TextBox txtJOBEFDTEdit = (TextBox)row.FindControl("txtJOBEFDTEdit");
            DateTime Date = DateTime.Parse(txtJOBEFDTEdit.Text, dateformat, System.Globalization.DateTimeStyles.AssumeLocal);
            txtJOBEFDTEdit.Text = Date.ToString("dd/MM/yy");
        }
    }
}