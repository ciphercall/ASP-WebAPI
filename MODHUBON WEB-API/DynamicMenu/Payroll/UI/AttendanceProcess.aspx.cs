using System;
using System.Data;
using System.Data.SqlClient;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using AlchemyAccounting;
using DynamicMenu.Payroll.DataAccess;
using DynamicMenu.Payroll.Interface;

namespace DynamicMenu.Payroll.UI
{
    public partial class AttendanceProcess : Page
    {
        private readonly IFormatProvider _dateformat = new System.Globalization.CultureInfo("fr-FR", true);
        private readonly PayrollDataAcces _dob = new PayrollDataAcces();
        private readonly PayrollInterface _iob = new PayrollInterface();
        private readonly AttendanceProcessModel _attProcess = new AttendanceProcessModel();
        private readonly SqlConnection _conn = new SqlConnection(dbFunctions.connection);

        protected void Page_Load(object sender, EventArgs e)
        {
            if (Session["USERID"] == null)
            {
                Response.Redirect("~/Login/UI/Login.aspx");
            }
            else
            {
                const string formLink = "/Payroll/UI/AttendanceProcess.aspx";
                var permission = UserPermissionChecker.checkParmit(formLink, "STATUS");
                if (permission)
                {
                    if (!Page.IsPostBack)
                    {
                        var monyear = dbFunctions.Timezone(DateTime.Now);
                        txtAttDate.Text = monyear.ToString("dd-MM-yyyy").ToUpper();
                        txtAttDate.Text = monyear.ToString("dd-MM-yyyy").ToUpper();
                        //DbFunctions.DropDownAddTextWithValue(ddlBranch, @"SELECT DEPTNM, DEPTID FROM HR_DEPT");
                    }
                }
                else
                {
                    Response.Redirect("../../DeshBoard/UI/Default.aspx");
                }
            }
        }

        protected void btnSubmit_Click(object sender, EventArgs e)
        {
            if (Session["USERID"] == null)
            {
                Response.Redirect("~/Login/UI/Login.aspx");
            }
            else
            {
                var transDate =
                    (DateTime.Parse(txtAttDate.Text, _dateformat, System.Globalization.DateTimeStyles.AssumeLocal))
                        .ToString("yyyy/MM/dd");

                _iob.UserPC = HttpContext.Current.Session["PCName"].ToString();
                _iob.UserID = Convert.ToInt32(HttpContext.Current.Session["USERID"].ToString());
                _iob.Ipaddress = HttpContext.Current.Session["IpAddress"].ToString();
                _iob.ITime = dbFunctions.Timezone(DateTime.Now);
                var txtLotiLongTude = new TextBox();
                if (Master != null)
                    txtLotiLongTude = (TextBox)Master.FindControl("txtLotiLongTude");
                else
                    txtLotiLongTude.Text = "";
                _iob.Ltude = txtLotiLongTude.Text;

                dbFunctions.StringData("DELETE FROM HR_ATOT WHERE TRANSDT='" + transDate + "'");

                gdProcess.DataSource = null;
                gdProcess.DataBind();


                var holidayCheck =
                   dbFunctions.StringData("SELECT STATUS FROM HR_HOLIDAYS WHERE TRANSDT='" + transDate + "'");

                _conn.Open();
                SqlCommand cmdCommand =
                    new SqlCommand(
                        @"SELECT HR_ATREG.TRANSDT, HR_ATREG.EMPID, HR_ATREG.SHIFTID, HR_ATREG.TIMEIN, HR_ATREG.TIMEOUT, 
                HR_ATREG.ENTRYTPI, HR_ATREG.ENTRYTPO, HR_SHIFT.TIMEFR, HR_SHIFT.TIMETO FROM HR_ATREG INNER JOIN
                HR_SHIFT ON HR_ATREG.SHIFTID = HR_SHIFT.SHIFTID WHERE TRANSDT='" + transDate +
                        "' AND HR_ATREG.TIMEOUT <> '00:00:00.0000000'", _conn);

                SqlDataAdapter da = new SqlDataAdapter(cmdCommand);
                DataSet ds = new DataSet();
                da.Fill(ds);
                _conn.Close();


                if (holidayCheck == "CLOSE")
                {
                    //ignore
                }
                else if (holidayCheck == "OPEN")
                {
                    _iob.Stats = "HOLIDAY";
                    if (ds.Tables[0].Rows.Count > 0)
                    {
                        gdProcess.DataSource = ds;
                        gdProcess.DataBind();
                    }
                    foreach (GridViewRow grid in gdProcess.Rows)
                    {
                        try
                        {
                            _iob.TransDT =
                                (DateTime.Parse(txtAttDate.Text, _dateformat,
                                    System.Globalization.DateTimeStyles.AssumeLocal));
                            _iob.EMPID = Convert.ToInt32(grid.Cells[1].Text);
                            _iob.SHIFTID = Convert.ToInt32(grid.Cells[2].Text);
                            _iob.AttInTime = TimeSpan.Parse(grid.Cells[3].Text);
                            _iob.AttOutTime = TimeSpan.Parse(grid.Cells[4].Text);
                            _iob.ShiftFrTm = TimeSpan.Parse(grid.Cells[7].Text);
                            _iob.ShiftToTm = TimeSpan.Parse(grid.Cells[8].Text);

                            _iob.OtDay = 1;
                            _iob.OtDifferenceTime = TimeSpan.Parse("00:00:00");

                            _dob.Insert_HR_Atot(_iob);
                        }
                        catch (Exception)
                        {
                            //ignore
                        }
                    }
                }
                else
                {
                    _iob.Stats = "WORKING";
                    if (ds.Tables[0].Rows.Count > 0)
                    {
                        gdProcess.DataSource = ds;
                        gdProcess.DataBind();
                    }
                    foreach (GridViewRow grid in gdProcess.Rows)
                    {
                        try
                        {
                            _iob.TransDT =
                                (DateTime.Parse(txtAttDate.Text, _dateformat,
                                    System.Globalization.DateTimeStyles.AssumeLocal));
                            _iob.EMPID = Convert.ToInt32(grid.Cells[1].Text);
                            _iob.SHIFTID = Convert.ToInt32(grid.Cells[2].Text);
                            _iob.AttInTime = TimeSpan.Parse(grid.Cells[3].Text);
                            _iob.AttOutTime = TimeSpan.Parse(grid.Cells[4].Text);
                            _iob.ShiftFrTm = TimeSpan.Parse(grid.Cells[7].Text);
                            _iob.ShiftToTm = TimeSpan.Parse(grid.Cells[8].Text);

                            TimeSpan differenceTime = TimeSpan.Parse("00:00:00");
                            if (_iob.AttOutTime > _iob.ShiftToTm)
                            {
                                differenceTime = _iob.AttOutTime - _iob.ShiftToTm;
                            }
                            else differenceTime = TimeSpan.Parse("00:00:00");
                            _iob.OtDay = 0;
                            _iob.OtDifferenceTime = differenceTime > TimeSpan.Parse("00:29:00")
                                ? differenceTime
                                : TimeSpan.Parse("00:00:00");

                            _dob.Insert_HR_Atot(_iob);
                        }
                        catch (Exception)
                        {
                            //ignore
                        }
                    }
                }




                Response.Write("<script>alert('Process Completed.');</script>");
            }
        }

        protected void btnAttProcess_OnClick(object sender, EventArgs e)
        {
            if (Session["USERID"] == null)
            {
                Response.Redirect("~/Login/UI/Login.aspx");
            }
            else
            {
                var transDate =
                    (DateTime.Parse(txtAttDate.Text, _dateformat, System.Globalization.DateTimeStyles.AssumeLocal))
                        .ToString("yyyy-MM-dd");

                // var departmentid = ddlBranch.SelectedValue;
                dbFunctions.ExecuteQuery($@"DELETE FROM HR_ATREG WHERE TRANSDT='{transDate}' AND ENTRYTPI='AUTO' AND ENTRYTPO='AUTO'");


                GridView gdAttendanceProcess = new GridView();

                dbFunctions.gridViewAdd(gdAttendanceProcess,
                     $@"SELECT       HR_ATTMCHN.EMPMCHNID, CONVERT(NVARCHAR,HR_ATTMCHN.TRANSDT,103) TRANSDT, 
                    HR_ATTMCHN.USERID, HR_ATTMCHN.USERNAME, 
                    HR_ATTMCHN.INTIME, HR_ATTMCHN.OUTTIME, HR_EMP.EMPID, HR_EMP.SHIFTID,
                    HR_EMP.EMPNM FROM HR_ATTMCHN INNER JOIN
                    HR_EMP ON HR_ATTMCHN.USERID = HR_EMP.EMPIDM WHERE HR_ATTMCHN.TRANSDT='{transDate}'");
                if (gdAttendanceProcess.Rows.Count > 0)
                {
                    GetAttendance(gdAttendanceProcess);
                    Response.Write("<script>alert('Process Completed.');</script>");
                }
            }
        }

        public void GetAttendance(GridView gdAttendanceProcess)
        {
            _iob.UserPC = HttpContext.Current.Session["PCName"].ToString();
            _iob.UserID = Convert.ToInt32(HttpContext.Current.Session["USERID"].ToString());
            _iob.Ipaddress = HttpContext.Current.Session["IpAddress"].ToString();
            _iob.ITime = dbFunctions.Timezone(DateTime.Now);
            var txtLotiLongTude = new TextBox();
            if (Master != null)
                txtLotiLongTude = (TextBox)Master.FindControl("txtLotiLongTude");
            else
                txtLotiLongTude.Text = "";
            _iob.Ltude = txtLotiLongTude.Text;

            foreach (GridViewRow grid in gdAttendanceProcess.Rows)
            {


                //var machineName = grid.Cells[1].Text;
                var transdt = grid.Cells[1].Text;
                var empid = grid.Cells[6].Text;
                var gettimein = grid.Cells[4].Text;
                var gettimeout = grid.Cells[5].Text;
                var shiftid = grid.Cells[7].Text;

                _iob.TransDT = DateTime.Parse(transdt, _dateformat, System.Globalization.DateTimeStyles.AssumeLocal);

                if (empid != "" || gettimein != "" || transdt != "" || gettimeout != "" || shiftid != "")
                {
                    string attcount = dbFunctions.StringData($@"SELECT COUNT(*) FROM HR_ATREG WHERE TRANSDT='{_iob.TransDT}' AND EMPID='{empid}'");

                    if (Convert.ToInt32(attcount) <= 0)
                    {
                        var getintime = DateTime.Parse(gettimein, _dateformat, System.Globalization.DateTimeStyles.AssumeLocal);
                        var getouttime = DateTime.Parse(gettimeout, _dateformat, System.Globalization.DateTimeStyles.AssumeLocal);

                        var intime = getintime.ToString("HH:m:s");
                        var outtime = getouttime.ToString("HH:m:s");

                        _iob.SHIFTID = Convert.ToInt32(shiftid);
                        _iob.AttInTime = TimeSpan.Parse(intime);
                        _iob.AttOutTime = TimeSpan.Parse(outtime);
                        _iob.EntryTypeIn = "AUTO";
                        _iob.EntryTypeOut = "AUTO";
                        _iob.EMPID = Convert.ToInt32(empid);

                        _dob.Insert_HR_Attendance(_iob);

                    }
                }
            }
        }

        //public void EmpAttendanceRegData(string empmenualId, string datetime)
        //{
        //    _conn.Open();
        //    SqlCommand cmd = new SqlCommand(@"SELECT DISTINCT HR_ATTMCHN.TRANSDT, HR_ATREG.TIMEIN, HR_ATREG.TIMEOUT, HR_EMP.EMPID, HR_EMP.SHIFTID 
        //    FROM HR_EMP INNER JOIN 
        //    HR_ATTMCHN ON HR_EMP.EMPIDM = HR_ATTMCHN.EMPMCHNID LEFT JOIN 
        //    HR_ATREG ON HR_ATREG.EMPID = HR_EMP.EMPID  AND HR_ATREG.TRANSDT=HR_ATTMCHN.TRANSDT
        //    WHERE HR_ATTMCHN.TRANSDT='" + datetime + "' AND HR_ATTMCHN.EMPMCHNID='" + empmenualId + "'", _conn);
        //    SqlDataReader reader = cmd.ExecuteReader();
        //    if (reader.Read())
        //    {
        //        _attProcess.Date = reader[0].ToString();
        //        _attProcess.InTime = reader[1].ToString();
        //        _attProcess.OutTime = reader[2].ToString();
        //        _attProcess.EmployeeId = reader[3].ToString();
        //        _attProcess.ShiftId = reader[4].ToString();
        //    }
        //    _conn.Close();
        //    reader.Close();
        //}

    }
}