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
    public partial class SalaryProcess : Page
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
                const string formLink = "/Payroll/UI/SalaryProcess.aspx";
                var permission = UserPermissionChecker.checkParmit(formLink, "STATUS");
                if (permission)
                {
                    if (!Page.IsPostBack)
                    {
                        var monyear = dbFunctions.Timezone(DateTime.Now);
                        txtMnYear.Text = monyear.ToString("MMM-yy").ToUpper();
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
                var monthyear = txtMnYear.Text;

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

                dbFunctions.StringData("DELETE FROM HR_SALPAY WHERE TRANSMY='" + monthyear + "'");

                gdProcess.DataSource = null;
                gdProcess.DataBind();

                _conn.Open();
                SqlCommand cmd = new SqlCommand(@"SELECT SUM(OPENDAY) OPENDAY, SUM(CLOSEDAY) CLOSEDAY, SUM(TOTALDAY) TOTALDAY, (SUM(TOTALDAY)-SUM(OPENDAY)-SUM(CLOSEDAY)) AS WORKDAY FROM (
                SELECT  COUNT(*) OPENDAY, 0 AS CLOSEDAY, 0 AS TOTALDAY FROM HR_HOLIDAYS WHERE STATUS='OPEN' AND SUBSTRING(CONVERT(NVARCHAR, TRANSDT, 6),4,3)+'-'+SUBSTRING(CONVERT(NVARCHAR, TRANSDT, 6),8,2)='" + monthyear + "' " +
                "UNION ALL " +
                "SELECT  0 AS OPENDAY, COUNT(*) CLOSEDAY,  0 AS TOTALDAY FROM HR_HOLIDAYS WHERE STATUS='CLOSE' AND SUBSTRING(CONVERT(NVARCHAR, TRANSDT, 6),4,3)+'-'+SUBSTRING(CONVERT(NVARCHAR, TRANSDT, 6),8,2)='" + monthyear + "' " +
                "UNION ALL " +
                "SELECT 0 AS OPENDAY, 0 CLOSEDAY,  TRANSDD  AS TOTALDAY FROM HR_DDMMYY WHERE TRANSMY='" + monthyear + "') A", _conn);
                SqlDataReader dr = cmd.ExecuteReader();
                while (dr.Read())
                {
                    _iob.HoliDayOpen = Convert.ToDecimal(dr["OPENDAY"].ToString());
                    _iob.HoliDayClose = Convert.ToDecimal(dr["CLOSEDAY"].ToString());
                    _iob.TotalMonthDay = Convert.ToDecimal(dr["TOTALDAY"].ToString());
                    _iob.TotalWorkDay = Convert.ToDecimal(dr["WORKDAY"].ToString());
                }
                dr.Close();
                _conn.Close();


                _conn.Open();
                SqlCommand cmdCommand = new SqlCommand(@"SELECT HR_EMPSALARY.COMPID, HR_EMPSALARY.EMPID, HR_SALDRCR.TRANSMY, ISNULL(HR_EMP.COSTPID, '10101') COSTPID, 
                ISNULL(HR_SALDRCR.BONUSF, 0.00) BONUSF, ISNULL(HR_SALDRCR.INCENTIVE, 0.00) INCENTIVE, 
                ISNULL(HR_EMPSALARY.CONVEY, 0.00) CONVEY, ISNULL(HR_SALDRCR.MOBILE, 0.00) MOBILE, ISNULL(HR_SALDRCR.DUEADJ, 0.00) DUEADJ, 
                ISNULL(HR_SALDRCR.ADVANCE, 0.00) ADVANCE, ISNULL(HR_SALDRCR.FOODING, 0.00) FOODING, ISNULL(HR_SALDRCR.FINEADJ, 0.00) FINEADJ, 
                ISNULL(HR_SALDRCR.DAYLEAVE, 0.00) DAYLEAVE, HR_EMPSALARY.POSTID, 
                ISNULL(HR_EMPSALARY.BASICSAL, 0.00) BASICSAL, ISNULL(HR_EMPSALARY.HOUSERENT, 0.00) HOUSERENT, ISNULL(HR_EMPSALARY.MEDICAL, 0.00) MEDICAL, 
                ISNULL(HR_EMPSALARY.TRANSPORT, 0.00) TRANSPORT, ISNULL(HR_EMPSALARY.RSTAMP, 0.00) RSTAMP, ISNULL(HR_EMPSALARY.PFRATE, 0.00) PFRATE, 
                HR_EMP.EMPTP, HR_EMP.GENDER, ISNULL(HR_EMP.COSTPID,'10101') COSTPID, ISNULL(HR_EMPSALARY.OTRTDAY, 0.00) OTRTDAY,
				ISNULL(HR_EMPSALARY.OTRTHOUR, 0.00) OTRTHOUR,ISNULL(HR_SALDRCR.OTHOUR, 0.00) OTHOUR,
 
                ISNULL(HR_SALDRCR.BONUSP, 0.00) BONUSP,ISNULL(HR_SALDRCR.FOODINGADD, 0.00) FOODINGADD, ISNULL(HR_SALDRCR.ITAX, 0.00) ITAX
 
                FROM HR_EMPSALARY INNER JOIN
                HR_EMP ON HR_EMPSALARY.COMPID = HR_EMP.COMPID AND HR_EMPSALARY.EMPID = HR_EMP.EMPID LEFT OUTER JOIN
                HR_SALDRCR ON HR_EMPSALARY.COMPID = HR_SALDRCR.COMPID AND HR_EMPSALARY.EMPID = HR_SALDRCR.EMPID 
                AND HR_SALDRCR.TRANSMY = '" + monthyear + "' WHERE ('01-" + monthyear + "' BETWEEN HR_EMPSALARY.JOBEFDT AND HR_EMPSALARY.JOBETDT)", _conn);

                SqlDataAdapter da = new SqlDataAdapter(cmdCommand);
                DataSet ds = new DataSet();
                da.Fill(ds);
                _conn.Close();
                if (ds.Tables[0].Rows.Count > 0)
                {
                    gdProcess.DataSource = ds;
                    gdProcess.DataBind();
                }
                foreach (GridViewRow grid in gdProcess.Rows)
                {

                    try
                    {
                        _iob.CmpID = int.Parse(grid.Cells[0].Text);
                        _iob.EMPID = Convert.ToInt32(grid.Cells[1].Text);
                        _iob.TransMonthYear = monthyear.ToUpper();
                        _iob.BranchCode = Convert.ToInt64(grid.Cells[3].Text);

                        _iob.Bonusf = Convert.ToDecimal(grid.Cells[4].Text);
                        _iob.Incentive = Convert.ToDecimal(grid.Cells[5].Text);


                        _iob.Convey = 0;
                        _iob.Mobile = Convert.ToDecimal(grid.Cells[7].Text);
                        _iob.Dueadj = Convert.ToDecimal(grid.Cells[8].Text);
                        _iob.Advance = Convert.ToDecimal(grid.Cells[9].Text);


                        _iob.Fooding = Convert.ToDecimal(grid.Cells[10].Text);
                        _iob.Fine = Convert.ToDecimal(grid.Cells[11].Text);
                        _iob.DayLeave = Convert.ToDecimal(grid.Cells[12].Text);
                        _iob.PostID = Convert.ToInt32(grid.Cells[13].Text);

                        _iob.BasicSal = Convert.ToDecimal(grid.Cells[14].Text);
                        _iob.HouseRent = Convert.ToDecimal(grid.Cells[15].Text);
                        _iob.Medical = Convert.ToDecimal(grid.Cells[16].Text);
                        _iob.TransPort = Convert.ToDecimal(grid.Cells[6].Text);
                        _iob.Stamp = Convert.ToDecimal(grid.Cells[18].Text);
                        _iob.EMPTP = grid.Cells[20].Text;
                        _iob.EmpGNM = grid.Cells[21].Text;
                        _iob.Branch = grid.Cells[22].Text;
                        _iob.OTRTday = Convert.ToDecimal(grid.Cells[23].Text);
                        _iob.OTRThour = Convert.ToDecimal(grid.Cells[24].Text);
                        _iob.Othour = Convert.ToDecimal(grid.Cells[25].Text);

                        _iob.BonusP = Convert.ToDecimal(grid.Cells[26].Text);
                        _iob.Foodinadd = Convert.ToDecimal(grid.Cells[27].Text);
                        _iob.ITax = Convert.ToDecimal(grid.Cells[28].Text);

                        _conn.Open();
                        SqlCommand cmddayemp = new SqlCommand(@"SELECT SUM(TOTALPRDAY) TOTALPRDAY, SUM(OTDAY) OTDAY, SUM(WRDAY) WRDAY, SUM(OTHOUR) OTHOUR FROM (
                        SELECT COUNT(*) TOTALPRDAY, 0 AS OTDAY, 0 AS WRDAY, 0 AS OTHOUR FROM HR_ATREG WHERE EMPID=@EMPID AND
                        SUBSTRING(CONVERT(NVARCHAR, TRANSDT, 6),4,3)+'-'+SUBSTRING(CONVERT(NVARCHAR, TRANSDT, 6),8,2)=@MONYY
                        UNION ALL
                        SELECT 0 AS TOTALPRDAY, COUNT(*) OTDAY, 0 AS WRDAY, 0 AS OTHOUR FROM HR_ATOT WHERE EMPID=@EMPID AND DAYSTATS='HOLIDAY' AND
                        SUBSTRING(CONVERT(NVARCHAR, TRANSDT, 6),4,3)+'-'+SUBSTRING(CONVERT(NVARCHAR, TRANSDT, 6),8,2)=@MONYY
                        UNION ALL
                        SELECT 0 AS TOTALPRDAY,  0 AS OTDAY, COUNT(*) WRDAY, 0 AS OTHOUR FROM HR_ATOT WHERE EMPID=@EMPID AND DAYSTATS='WORKING' AND
                        SUBSTRING(CONVERT(NVARCHAR, TRANSDT, 6),4,3)+'-'+SUBSTRING(CONVERT(NVARCHAR, TRANSDT, 6),8,2)=@MONYY
                        UNION ALL
                        SELECT 0 AS TOTALPRDAY,  0 AS OTDAY,  0 AS WRDAY, SUM(DATEDIFF(MINUTE, '0:00:00', OTHOUR)) OTHOUR FROM HR_ATOT WHERE EMPID=@EMPID AND DAYSTATS='WORKING' AND
                        SUBSTRING(CONVERT(NVARCHAR, TRANSDT, 6),4,3)+'-'+SUBSTRING(CONVERT(NVARCHAR, TRANSDT, 6),8,2)=@MONYY) A", _conn);
                        cmddayemp.Parameters.AddWithValue("@EMPID", _iob.EMPID);
                        cmddayemp.Parameters.AddWithValue("@MONYY", monthyear);

                        SqlDataReader drdayemp = cmddayemp.ExecuteReader();
                        while (drdayemp.Read())
                        {
                            _iob.TotalPresentDay = Convert.ToDecimal(drdayemp["TOTALPRDAY"].ToString());
                            _iob.TotalOverTimeDay = Convert.ToDecimal(drdayemp["OTDAY"].ToString());
                            _iob.TotalWokeringkDayCal = Convert.ToDecimal(drdayemp["WRDAY"].ToString());
                            _iob.TotalOverTimeMinute = Convert.ToDecimal(drdayemp["OTHOUR"].ToString());
                        }
                        drdayemp.Close();
                        _conn.Close();

                        _iob.DayTot = _iob.TotalMonthDay;
                        _iob.DayWork = _iob.TotalWorkDay;
                        _iob.DayHoli = _iob.HoliDayOpen + _iob.HoliDayClose;
                        _iob.DayPre = _iob.TotalPresentDay;
                        _iob.DayAbs = _iob.TotalMonthDay - _iob.TotalPresentDay - _iob.HoliDayClose - _iob.DayLeave;
                        _iob.OtDay = _iob.TotalOverTimeDay;
                        _iob.Othour = _iob.Othour;
                        _iob.Alwdaily = ((((_iob.BasicSal * 50) / 100) * _iob.DayPre) / _iob.TotalWorkDay);

                        _iob.AlwdailyOt = ((_iob.BasicSal * 50) / (_iob.TotalWorkDay * 100)) * _iob.TotalOverTimeDay;

                        _iob.AmountTotalHours = _iob.Othour * _iob.OTRThour;

                        _iob.GrossSal = _iob.BasicSal + _iob.HouseRent + _iob.Medical + _iob.TransPort + _iob.Stamp;
                        _iob.AbsDed = ((_iob.GrossSal / _iob.TotalMonthDay) * _iob.DayAbs);
                        _iob.TotAdd = _iob.GrossSal + _iob.BonusP + _iob.Bonusf + _iob.Incentive + _iob.AmountTotalDays + _iob.AmountTotalHours + _iob.PfAdd + _iob.Convey + _iob.Mobile + _iob.Dueadj + _iob.Foodinadd; // + _iob.Alwdaily + _iob.AlwdailyOt
                        _iob.TotDed = _iob.AbsDed + _iob.Advance + _iob.PfDed + _iob.Fooding + _iob.Fine + _iob.ITax;
                        _iob.NetPay = (_iob.TotAdd - _iob.TotDed);

                        _dob.Insert_Salary_Precess(_iob);
                    }
                    catch (Exception ex)
                    {
                        //ignore
                    }
                }

                Response.Write("<script>alert('Process Completed.');</script>");
            }
        }
    }
}