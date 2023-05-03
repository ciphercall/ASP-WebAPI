using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Data;
using System.Data.Entity;
using System.Web.UI;
using System.Data.SqlClient;
using System.Web.UI.WebControls;
using AlchemyAccounting;
using DynamicMenu.Payroll.Interface;

namespace DynamicMenu.Payroll.DataAccess
{
    public class PayrollDataAcces
    {
        SqlConnection con;
        SqlCommand cmd;
        public PayrollDataAcces()
        {
            con = new SqlConnection(dbFunctions.connection);
            cmd = new SqlCommand("", con);
        }
        public string Insert_HR_POST(PayrollInterface ob)
        {
            string s = "";
            SqlTransaction tran = null;
            try
            {
                if (con.State != ConnectionState.Open)
                    con.Open();
                tran = con.BeginTransaction();
                cmd.CommandType = CommandType.Text;
                cmd.CommandText = @"INSERT INTO HR_POST(COMPID,POSTID,POSTNM,REMARKS,USERPC,INSUSERID,INSTIME,INSIPNO,
                               INSLTUDE) Values

                    (@COMPID,@POSTID,@POSTNM,@REMARKS,@USERPC,@INSUSERID,@INSTIME,@INSIPNO,
                               @INSLTUDE)";
                cmd.Parameters.Clear();
                cmd.Parameters.Add("@COMPID", SqlDbType.Int).Value = ob.CmpID;
                cmd.Parameters.Add("@POSTID", SqlDbType.Int).Value = ob.PostID;
                cmd.Parameters.Add("@POSTNM", SqlDbType.NVarChar).Value = ob.PostNM;
                cmd.Parameters.Add("@REMARKS", SqlDbType.NVarChar).Value = ob.Remarks;
                cmd.Parameters.Add("@USERPC", SqlDbType.NVarChar).Value = ob.UserPC;
                cmd.Parameters.Add("@INSUSERID", SqlDbType.Int).Value = ob.UserID;
                cmd.Parameters.Add("@INSTIME", SqlDbType.NVarChar).Value = ob.ITime;
                cmd.Parameters.Add("@INSIPNO", SqlDbType.NVarChar).Value = ob.Ipaddress;
                cmd.Parameters.Add("@INSLTUDE", SqlDbType.NVarChar).Value = ob.Ltude;
                cmd.Transaction = tran;
                cmd.ExecuteNonQuery();
                tran.Commit();
                if (con.State != ConnectionState.Closed)
                    con.Close();

            }
            catch (Exception ex)
            {
                tran.Rollback();
                s = ex.Message;
            }
            return s;
        }

        public string Update_HR_POST(PayrollInterface ob)
        {
            string s = "";
            SqlTransaction tran = null;
            try
            {
                if (con.State != ConnectionState.Open)
                    con.Open();
                tran = con.BeginTransaction();
                cmd.CommandType = CommandType.Text;
                cmd.CommandText = @"UPDATE  HR_POST SET COMPID=@COMPID,POSTNM=@POSTNM,REMARKS=@REMARKS,
                            UPDUSERID=@UPDUSERID,UPDTIME=@UPDTIME,UPDIPNO=@UPDIPNO,UPDLTUDE=@UPDLTUDE WHERE POSTID=@POSTID";

                cmd.Parameters.Clear();
                cmd.Parameters.Add("@COMPID", SqlDbType.Int).Value = ob.CmpID;
                cmd.Parameters.Add("@POSTID", SqlDbType.Int).Value = ob.PostID;
                cmd.Parameters.Add("@POSTNM", SqlDbType.NVarChar).Value = ob.PostNM;
                cmd.Parameters.Add("@REMARKS", SqlDbType.NVarChar).Value = ob.Remarks;
                cmd.Parameters.Add("@UPDUSERID", SqlDbType.Int).Value = ob.UPDUserID;
                cmd.Parameters.Add("@UPDTIME", SqlDbType.NVarChar).Value = ob.UPDTime;
                cmd.Parameters.Add("@UPDIPNO", SqlDbType.NVarChar).Value = ob.UPDIpaddress;
                cmd.Parameters.Add("@UPDLTUDE", SqlDbType.NVarChar).Value = ob.UPDLtude;
                cmd.Transaction = tran;
                cmd.ExecuteNonQuery();
                tran.Commit();
                if (con.State != ConnectionState.Closed)
                    con.Close();

            }
            catch (Exception ex)
            {
                tran.Rollback();
                s = ex.Message;
            }
            return s;
        }

        public string Insert_HR_DEPT(PayrollInterface ob)
        {
            string s = "";
            SqlTransaction tran = null;
            try
            {
                if (con.State != ConnectionState.Open)
                    con.Open();
                tran = con.BeginTransaction();
                cmd.CommandType = CommandType.Text;
                cmd.CommandText = @"INSERT INTO HR_DEPT(COMPID,DEPTID,DEPTNM,REMARKS,USERPC,INSUSERID,INSTIME,INSIPNO,
                               INSLTUDE) Values

                    (@COMPID,@DEPTID,@DEPTNM,@REMARKS,@USERPC,@INSUSERID,@INSTIME,@INSIPNO,
                               @INSLTUDE)";
                cmd.Parameters.Clear();
                cmd.Parameters.Add("@COMPID", SqlDbType.Int).Value = ob.CmpID;
                cmd.Parameters.Add("@DEPTID", SqlDbType.Int).Value = ob.DEPTID;
                cmd.Parameters.Add("@DEPTNM", SqlDbType.NVarChar).Value = ob.DEPTNM;
                cmd.Parameters.Add("@REMARKS", SqlDbType.NVarChar).Value = ob.Remarks;
                cmd.Parameters.Add("@USERPC", SqlDbType.NVarChar).Value = ob.UserPC;
                cmd.Parameters.Add("@INSUSERID", SqlDbType.Int).Value = ob.UserID;
                cmd.Parameters.Add("@INSTIME", SqlDbType.NVarChar).Value = ob.ITime;
                cmd.Parameters.Add("@INSIPNO", SqlDbType.NVarChar).Value = ob.Ipaddress;
                cmd.Parameters.Add("@INSLTUDE", SqlDbType.NVarChar).Value = ob.Ltude;
                cmd.Transaction = tran;
                cmd.ExecuteNonQuery();
                tran.Commit();
                if (con.State != ConnectionState.Closed)
                    con.Close();

            }
            catch (Exception ex)
            {
                tran.Rollback();
                s = ex.Message;
            }
            return s;
        }

        public string Update_HR_DEPT(PayrollInterface ob)
        {
            string s = "";
            SqlTransaction tran = null;
            try
            {
                if (con.State != ConnectionState.Open)
                    con.Open();
                tran = con.BeginTransaction();
                cmd.CommandType = CommandType.Text;
                cmd.CommandText = @"UPDATE  HR_DEPT SET COMPID=@COMPID,DEPTNM=@DEPTNM,REMARKS=@REMARKS,
                            UPDUSERID=@UPDUSERID,UPDTIME=@UPDTIME,UPDIPNO=@UPDIPNO,UPDLTUDE=@UPDLTUDE WHERE DEPTID=@DEPTID";

                cmd.Parameters.Clear();
                cmd.Parameters.Add("@COMPID", SqlDbType.Int).Value = ob.CmpID;
                cmd.Parameters.Add("@DEPTID", SqlDbType.Int).Value = ob.DEPTID;
                cmd.Parameters.Add("@DEPTNM", SqlDbType.NVarChar).Value = ob.DEPTNM;
                cmd.Parameters.Add("@REMARKS", SqlDbType.NVarChar).Value = ob.Remarks;
                cmd.Parameters.Add("@UPDUSERID", SqlDbType.Int).Value = ob.UPDUserID;
                cmd.Parameters.Add("@UPDTIME", SqlDbType.NVarChar).Value = ob.UPDTime;
                cmd.Parameters.Add("@UPDIPNO", SqlDbType.NVarChar).Value = ob.UPDIpaddress;
                cmd.Parameters.Add("@UPDLTUDE", SqlDbType.NVarChar).Value = ob.UPDLtude;
                cmd.Transaction = tran;
                cmd.ExecuteNonQuery();
                tran.Commit();
                if (con.State != ConnectionState.Closed)
                    con.Close();

            }
            catch (Exception ex)
            {
                tran.Rollback();
                s = ex.Message;
            }
            return s;
        }

        public string Insert_HR_HDAYTP(PayrollInterface ob)
        {
            string s = "";
            SqlTransaction tran = null;
            try
            {
                if (con.State != ConnectionState.Open)
                    con.Open();
                tran = con.BeginTransaction();
                cmd.CommandType = CommandType.Text;
                cmd.CommandText = @"INSERT INTO HR_HDAYTP(COMPID,HDAYID,HDAYNM,REMARKS,USERPC,INSUSERID,INSTIME,INSIPNO,
                               INSLTUDE) Values

                    (@COMPID,@HDAYID,@HDAYNM,@REMARKS,@USERPC,@INSUSERID,@INSTIME,@INSIPNO,
                               @INSLTUDE)";
                cmd.Parameters.Clear();
                cmd.Parameters.Add("@COMPID", SqlDbType.Int).Value = ob.CmpID;
                cmd.Parameters.Add("@HDAYID", SqlDbType.Int).Value = ob.HDAYID;
                cmd.Parameters.Add("@HDAYNM", SqlDbType.NVarChar).Value = ob.HDAYNM;
                cmd.Parameters.Add("@REMARKS", SqlDbType.NVarChar).Value = ob.Remarks;
                cmd.Parameters.Add("@USERPC", SqlDbType.NVarChar).Value = ob.UserPC;
                cmd.Parameters.Add("@INSUSERID", SqlDbType.Int).Value = ob.UserID;
                cmd.Parameters.Add("@INSTIME", SqlDbType.NVarChar).Value = ob.ITime;
                cmd.Parameters.Add("@INSIPNO", SqlDbType.NVarChar).Value = ob.Ipaddress;
                cmd.Parameters.Add("@INSLTUDE", SqlDbType.NVarChar).Value = ob.Ltude;
                cmd.Transaction = tran;
                cmd.ExecuteNonQuery();
                tran.Commit();
                if (con.State != ConnectionState.Closed)
                    con.Close();

            }
            catch (Exception ex)
            {
                tran.Rollback();
                s = ex.Message;
            }
            return s;
        }

        public string Update_HR_HDAYTP(PayrollInterface ob)
        {
            string s = "";
            SqlTransaction tran = null;
            try
            {
                if (con.State != ConnectionState.Open)
                    con.Open();
                tran = con.BeginTransaction();
                cmd.CommandType = CommandType.Text;
                cmd.CommandText = @"UPDATE  HR_HDAYTP SET COMPID=@COMPID,HDAYNM=@HDAYNM,REMARKS=@REMARKS,
                            UPDUSERID=@UPDUSERID,UPDTIME=@UPDTIME,UPDIPNO=@UPDIPNO,UPDLTUDE=@UPDLTUDE WHERE HDAYID=@HDAYID";

                cmd.Parameters.Clear();
                cmd.Parameters.Add("@COMPID", SqlDbType.Int).Value = ob.CmpID;
                cmd.Parameters.Add("@HDAYID", SqlDbType.Int).Value = ob.HDAYID;
                cmd.Parameters.Add("@HDAYNM", SqlDbType.NVarChar).Value = ob.HDAYNM;
                cmd.Parameters.Add("@REMARKS", SqlDbType.NVarChar).Value = ob.Remarks;
                cmd.Parameters.Add("@UPDUSERID", SqlDbType.Int).Value = ob.UPDUserID;
                cmd.Parameters.Add("@UPDTIME", SqlDbType.NVarChar).Value = ob.UPDTime;
                cmd.Parameters.Add("@UPDIPNO", SqlDbType.NVarChar).Value = ob.UPDIpaddress;
                cmd.Parameters.Add("@UPDLTUDE", SqlDbType.NVarChar).Value = ob.UPDLtude;
                cmd.Transaction = tran;
                cmd.ExecuteNonQuery();
                tran.Commit();
                if (con.State != ConnectionState.Closed)
                    con.Close();

            }
            catch (Exception ex)
            {
                tran.Rollback();
                s = ex.Message;
            }
            return s;
        }

        public string Insert_HR_HDAY(PayrollInterface ob)
        {
            string s = "";
            SqlTransaction tran = null;
            try
            {
                if (con.State != ConnectionState.Open)
                    con.Open();
                tran = con.BeginTransaction();
                cmd.CommandType = CommandType.Text;
                cmd.CommandText = @"INSERT INTO HR_HOLIDAYS (COMPID,TRANSYY,TRANSDT,HDAYID,STATUS,REMARKS,USERPC,INSUSERID,INSTIME,INSIPNO,
                               INSLTUDE) Values (@COMPID,@TRANSYY,@TRANSDT,@HDAYID,@STATUS,@REMARKS,@USERPC,@INSUSERID,@INSTIME,@INSIPNO,
                               @INSLTUDE)";
                cmd.Parameters.Clear();
                cmd.Parameters.Add("@COMPID", SqlDbType.Int).Value = ob.CmpID;
                cmd.Parameters.Add("@TRANSYY", SqlDbType.Int).Value = ob.TransYr;
                cmd.Parameters.Add("@TRANSDT", SqlDbType.Date).Value = ob.TransDT;
                cmd.Parameters.Add("@HDAYID", SqlDbType.Int).Value = ob.HDAYID;
                cmd.Parameters.Add("@STATUS", SqlDbType.NVarChar).Value = ob.Status;
                cmd.Parameters.Add("@REMARKS", SqlDbType.NVarChar).Value = ob.Remarks;
                cmd.Parameters.Add("@USERPC", SqlDbType.NVarChar).Value = ob.UserPC;
                cmd.Parameters.Add("@INSUSERID", SqlDbType.Int).Value = ob.UserID;
                cmd.Parameters.Add("@INSTIME", SqlDbType.NVarChar).Value = ob.ITime;
                cmd.Parameters.Add("@INSIPNO", SqlDbType.NVarChar).Value = ob.Ipaddress;
                cmd.Parameters.Add("@INSLTUDE", SqlDbType.NVarChar).Value = ob.Ltude;
                cmd.Transaction = tran;
                cmd.ExecuteNonQuery();
                tran.Commit();
                if (con.State != ConnectionState.Closed)
                    con.Close();

            }
            catch (Exception ex)
            {
                tran.Rollback();
                s = ex.Message;
            }
            return s;
        }

        public string Update_HR_HDAY(PayrollInterface ob)
        {
            string s = "";
            SqlTransaction tran = null;
            try
            {
                if (con.State != ConnectionState.Open)
                    con.Open();
                tran = con.BeginTransaction();
                cmd.CommandType = CommandType.Text;
                cmd.CommandText = @"UPDATE  HR_HOLIDAYS SET COMPID=@COMPID,TRANSYY=@TRANSYY,TRANSDT=@TRANSDT,STATUS=@STATUS,REMARKS=@REMARKS,
                            UPDUSERID=@UPDUSERID,UPDTIME=@UPDTIME,UPDIPNO=@UPDIPNO,UPDLTUDE=@UPDLTUDE WHERE HDAYID=@HDAYID";

                cmd.Parameters.Clear();
                cmd.Parameters.Add("@COMPID", SqlDbType.Int).Value = ob.CmpID;
                cmd.Parameters.Add("@TRANSYY", SqlDbType.Int).Value = ob.TransYr;
                cmd.Parameters.Add("@TRANSDT", SqlDbType.Date).Value = ob.TransDT;
                cmd.Parameters.Add("@HDAYID", SqlDbType.Int).Value = ob.HDAYID;
                cmd.Parameters.Add("@STATUS", SqlDbType.NVarChar).Value = ob.Status;
                cmd.Parameters.Add("@REMARKS", SqlDbType.NVarChar).Value = ob.Remarks;
                cmd.Parameters.Add("@UPDUSERID", SqlDbType.Int).Value = ob.UPDUserID;
                cmd.Parameters.Add("@UPDTIME", SqlDbType.NVarChar).Value = ob.UPDTime;
                cmd.Parameters.Add("@UPDIPNO", SqlDbType.NVarChar).Value = ob.UPDIpaddress;
                cmd.Parameters.Add("@UPDLTUDE", SqlDbType.NVarChar).Value = ob.UPDLtude;
                cmd.Transaction = tran;
                cmd.ExecuteNonQuery();
                tran.Commit();
                if (con.State != ConnectionState.Closed)
                    con.Close();

            }
            catch (Exception ex)
            {
                tran.Rollback();
                s = ex.Message;
            }
            return s;
        }

        public string Insert_HR_SHIFT(PayrollInterface ob)
        {
            string s = "";
            SqlTransaction tran = null;
            try
            {
                if (con.State != ConnectionState.Open)
                    con.Open();
                tran = con.BeginTransaction();
                cmd.CommandType = CommandType.Text;
                cmd.CommandText = @"INSERT INTO HR_SHIFT(COMPID,SHIFTID,SHIFTNM,TIMEFR,TIMETO,LATEFR,OTFROM,REMARKS,USERPC,INSUSERID,INSTIME,INSIPNO,
                               INSLTUDE) Values
                    (@COMPID,@SHIFTID,@SHIFTNM,@TIMEFR,@TIMETO,@LATEFR,@OTFROM,@REMARKS,@USERPC,@INSUSERID,@INSTIME,@INSIPNO,
                               @INSLTUDE)";
                cmd.Parameters.Clear();
                cmd.Parameters.Add("@COMPID", SqlDbType.Int).Value = ob.CmpID;
                cmd.Parameters.Add("@SHIFTID", SqlDbType.Int).Value = ob.SHIFTID;
                cmd.Parameters.Add("@SHIFTNM", SqlDbType.NVarChar).Value = ob.SHIFTNM;
                cmd.Parameters.Add("@TIMEFR", SqlDbType.Time).Value = ob.TimeFR;
                cmd.Parameters.Add("@TIMETO", SqlDbType.Time).Value = ob.TimeTO;

                cmd.Parameters.Add("@LATEFR", SqlDbType.Time).Value = ob.LateFR;
                cmd.Parameters.Add("@OTFROM", SqlDbType.Time).Value = ob.OTFrom;

                cmd.Parameters.Add("@REMARKS", SqlDbType.NVarChar).Value = ob.Remarks;
                cmd.Parameters.Add("@USERPC", SqlDbType.NVarChar).Value = ob.UserPC;
                cmd.Parameters.Add("@INSUSERID", SqlDbType.Int).Value = ob.UserID;
                cmd.Parameters.Add("@INSTIME", SqlDbType.NVarChar).Value = ob.ITime;
                cmd.Parameters.Add("@INSIPNO", SqlDbType.NVarChar).Value = ob.Ipaddress;
                cmd.Parameters.Add("@INSLTUDE", SqlDbType.NVarChar).Value = ob.Ltude;
                cmd.Transaction = tran;
                cmd.ExecuteNonQuery();
                tran.Commit();
                if (con.State != ConnectionState.Closed)
                    con.Close();

            }
            catch (Exception ex)
            {
                tran.Rollback();
                s = ex.Message;
            }
            return s;
        }

        public string Update_HR_SHIFT(PayrollInterface ob)
        {
            string s = "";
            SqlTransaction tran = null;
            try
            {
                if (con.State != ConnectionState.Open)
                    con.Open();
                tran = con.BeginTransaction();
                cmd.CommandType = CommandType.Text;
                cmd.CommandText = @"UPDATE  HR_SHIFT SET COMPID=@COMPID,SHIFTNM=@SHIFTNM,TIMEFR=@TIMEFR,TIMETO=@TIMETO,LATEFR=@LATEFR,OTFROM=@OTFROM,REMARKS=@REMARKS,
                            UPDUSERID=@UPDUSERID,UPDTIME=@UPDTIME,UPDIPNO=@UPDIPNO,UPDLTUDE=@UPDLTUDE WHERE SHIFTID=@SHIFTID";

                cmd.Parameters.Clear();
                cmd.Parameters.Add("@COMPID", SqlDbType.Int).Value = ob.CmpID;
                cmd.Parameters.Add("@SHIFTID", SqlDbType.Int).Value = ob.SHIFTID;
                cmd.Parameters.Add("@SHIFTNM", SqlDbType.NVarChar).Value = ob.SHIFTNM;
                cmd.Parameters.Add("@TIMEFR", SqlDbType.Time).Value = ob.TimeFR;
                cmd.Parameters.Add("@TIMETO", SqlDbType.Time).Value = ob.TimeTO;

                cmd.Parameters.Add("@LATEFR", SqlDbType.Time).Value = ob.LateFR;
                cmd.Parameters.Add("@OTFROM", SqlDbType.Time).Value = ob.OTFrom;

                cmd.Parameters.Add("@REMARKS", SqlDbType.NVarChar).Value = ob.Remarks;
                cmd.Parameters.Add("@UPDUSERID", SqlDbType.Int).Value = ob.UPDUserID;
                cmd.Parameters.Add("@UPDTIME", SqlDbType.NVarChar).Value = ob.UPDTime;
                cmd.Parameters.Add("@UPDIPNO", SqlDbType.NVarChar).Value = ob.UPDIpaddress;
                cmd.Parameters.Add("@UPDLTUDE", SqlDbType.NVarChar).Value = ob.UPDLtude;
                cmd.Transaction = tran;
                cmd.ExecuteNonQuery();
                tran.Commit();
                if (con.State != ConnectionState.Closed)
                    con.Close();

            }
            catch (Exception ex)
            {
                tran.Rollback();
                s = ex.Message;
            }
            return s;
        }

        public string Insert_HR_LEAVE(PayrollInterface ob)
        {
            string s = "";
            SqlTransaction tran = null;
            try
            {
                if (con.State != ConnectionState.Open)
                    con.Open();
                tran = con.BeginTransaction();
                cmd.CommandType = CommandType.Text;
                cmd.CommandText = @"INSERT INTO HR_LEAVE(COMPID,LEAVEID,LEAVENM,REMARKS,USERPC,INSUSERID,INSTIME,INSIPNO,
                               INSLTUDE) Values

                    (@COMPID,@LEAVEID,@LEAVENM,@REMARKS,@USERPC,@INSUSERID,@INSTIME,@INSIPNO,
                               @INSLTUDE)";
                cmd.Parameters.Clear();
                cmd.Parameters.Add("@COMPID", SqlDbType.Int).Value = ob.CmpID;
                cmd.Parameters.Add("@LEAVEID", SqlDbType.Int).Value = ob.LEAVEID;
                cmd.Parameters.Add("@LEAVENM", SqlDbType.NVarChar).Value = ob.LEAVENM;
                cmd.Parameters.Add("@REMARKS", SqlDbType.NVarChar).Value = ob.Remarks;
                cmd.Parameters.Add("@USERPC", SqlDbType.NVarChar).Value = ob.UserPC;
                cmd.Parameters.Add("@INSUSERID", SqlDbType.Int).Value = ob.UserID;
                cmd.Parameters.Add("@INSTIME", SqlDbType.NVarChar).Value = ob.ITime;
                cmd.Parameters.Add("@INSIPNO", SqlDbType.NVarChar).Value = ob.Ipaddress;
                cmd.Parameters.Add("@INSLTUDE", SqlDbType.NVarChar).Value = ob.Ltude;
                cmd.Transaction = tran;
                cmd.ExecuteNonQuery();
                tran.Commit();
                if (con.State != ConnectionState.Closed)
                    con.Close();

            }
            catch (Exception ex)
            {
                tran.Rollback();
                s = ex.Message;
            }
            return s;
        }

        public string Update_HR_LEAVE(PayrollInterface ob)
        {
            string s = "";
            SqlTransaction tran = null;
            try
            {
                if (con.State != ConnectionState.Open)
                    con.Open();
                tran = con.BeginTransaction();
                cmd.CommandType = CommandType.Text;
                cmd.CommandText = @"UPDATE  HR_LEAVE SET COMPID=@COMPID,LEAVENM=@LEAVENM,REMARKS=@REMARKS,
                            UPDUSERID=@UPDUSERID,UPDTIME=@UPDTIME,UPDIPNO=@UPDIPNO,UPDLTUDE=@UPDLTUDE WHERE LEAVEID=@LEAVEID";

                cmd.Parameters.Clear();
                cmd.Parameters.Add("@COMPID", SqlDbType.Int).Value = ob.CmpID;
                cmd.Parameters.Add("@LEAVEID", SqlDbType.Int).Value = ob.LEAVEID;
                cmd.Parameters.Add("@LEAVENM", SqlDbType.NVarChar).Value = ob.LEAVENM;
                cmd.Parameters.Add("@REMARKS", SqlDbType.NVarChar).Value = ob.Remarks;
                cmd.Parameters.Add("@UPDUSERID", SqlDbType.Int).Value = ob.UPDUserID;
                cmd.Parameters.Add("@UPDTIME", SqlDbType.NVarChar).Value = ob.UPDTime;
                cmd.Parameters.Add("@UPDIPNO", SqlDbType.NVarChar).Value = ob.UPDIpaddress;
                cmd.Parameters.Add("@UPDLTUDE", SqlDbType.NVarChar).Value = ob.UPDLtude;
                cmd.Transaction = tran;
                cmd.ExecuteNonQuery();
                tran.Commit();
                if (con.State != ConnectionState.Closed)
                    con.Close();

            }
            catch (Exception ex)
            {
                tran.Rollback();
                s = ex.Message;
            }
            return s;
        }
        public string Insert_HR_Salary_Add(PayrollInterface ob)
        {
            string s = "";
            SqlTransaction tran = null;
            try
            {
                if (con.State != ConnectionState.Open)
                    con.Open();
                tran = con.BeginTransaction();
                cmd.CommandType = CommandType.Text;
                cmd.CommandText = @"INSERT INTO HR_SALDRCR(COMPID, EMPID, TRANSMY, BRANCHCD, BONUSF, INCENTIVE, CONVEY, MOBILE, DUEADJ, 
                ADVANCE,OTHOUR, FOODING, FINEADJ, DAYLEAVE, REMARKS,FOODINGADD,BONUSP,ITAX, USERPC, INSUSERID, INSTIME, INSIPNO, INSLTUDE) 
                VALUES(@COMPID, @EMPID, @TRANSMY, @BRANCHCD, @BONUSF, @INCENTIVE, @CONVEY, @MOBILE, @DUEADJ, 
                @ADVANCE,@OTHOUR, @FOODING, @FINEADJ, @DAYLEAVE, @REMARKS,@FOODINGADD,@BONUSP,@ITAX, @USERPC, @INSUSERID, @INSTIME, @INSIPNO, @INSLTUDE)";
                cmd.Parameters.Clear();
                cmd.Parameters.Add("@COMPID", SqlDbType.Int).Value = ob.CmpID;
                cmd.Parameters.Add("@EMPID", SqlDbType.BigInt).Value = ob.EMPID;
                cmd.Parameters.Add("@TRANSMY", SqlDbType.NVarChar).Value = ob.TransMonthYear;
                cmd.Parameters.Add("@BRANCHCD", SqlDbType.NVarChar).Value = ob.BranchCode;
                
                cmd.Parameters.Add("@BONUSF", SqlDbType.Decimal).Value = ob.Bonusf;
                cmd.Parameters.Add("@INCENTIVE", SqlDbType.Decimal).Value = ob.Incentive;
                cmd.Parameters.Add("@CONVEY", SqlDbType.Decimal).Value = ob.Convey;
                cmd.Parameters.Add("@MOBILE", SqlDbType.Decimal).Value = ob.Mobile;
                cmd.Parameters.Add("@DUEADJ", SqlDbType.Decimal).Value = ob.Dueadj;
                cmd.Parameters.Add("@ADVANCE", SqlDbType.Decimal).Value = ob.Advance;
                cmd.Parameters.Add("@FOODING", SqlDbType.Decimal).Value = ob.Fooding;
                cmd.Parameters.Add("@OTHOUR", SqlDbType.Decimal).Value = ob.Othour;
                cmd.Parameters.Add("@FINEADJ", SqlDbType.Decimal).Value = ob.Fine;
                cmd.Parameters.Add("@DAYLEAVE", SqlDbType.Decimal).Value = ob.DayLeave;
                cmd.Parameters.Add("@REMARKS", SqlDbType.NVarChar).Value = ob.Remarks;

                cmd.Parameters.Add("@FOODINGADD", SqlDbType.Decimal).Value = ob.Foodinadd;
                cmd.Parameters.Add("@BONUSP", SqlDbType.Decimal).Value = ob.BonusP;
                cmd.Parameters.Add("@ITAX", SqlDbType.Decimal).Value = ob.ITax;

                cmd.Parameters.Add("@USERPC", SqlDbType.NVarChar).Value = ob.UserPC;
                cmd.Parameters.Add("@INSUSERID", SqlDbType.Int).Value = ob.UserID;
                cmd.Parameters.Add("@INSTIME", SqlDbType.NVarChar).Value = ob.ITime;
                cmd.Parameters.Add("@INSIPNO", SqlDbType.NVarChar).Value = ob.Ipaddress;
                cmd.Parameters.Add("@INSLTUDE", SqlDbType.NVarChar).Value = ob.Ltude;
                cmd.Transaction = tran;
                cmd.ExecuteNonQuery();
                tran.Commit();
                if (con.State != ConnectionState.Closed)
                    con.Close();

            }
            catch (Exception ex)
            {
                tran.Rollback();
                s = ex.Message;
            }
            return s;
        }
       // FOODINGADD,BONUSP,ITAX,

        public string Update_HR_Salary_Add(PayrollInterface ob)
        {
            string s = "";
            SqlTransaction tran = null;
            try
            {
                if (con.State != ConnectionState.Open)
                    con.Open();
                tran = con.BeginTransaction();
                cmd.CommandType = CommandType.Text;
                cmd.CommandText = @"UPDATE HR_SALDRCR SET COMPID=@COMPID, EMPID=@EMPID, TRANSMY=@TRANSMY, 
                BONUSF=@BONUSF, INCENTIVE=@INCENTIVE, CONVEY=@CONVEY, MOBILE=@MOBILE, DUEADJ=@DUEADJ, 
                ADVANCE=@ADVANCE,OTHOUR=@OTHOUR, FOODING=@FOODING, FINEADJ=@FINEADJ, DAYLEAVE=@DAYLEAVE, REMARKS=@REMARKS,
                FOODINGADD=@FOODINGADD, BONUSP=@BONUSP, ITAX=@ITAX, UPDUSERID=@UPDUSERID,
                UPDTIME=@UPDTIME,UPDIPNO=@UPDIPNO,UPDLTUDE=@UPDLTUDE 
                WHERE COMPID=@COMPID AND EMPID=@EMPID AND TRANSMY=@TRANSMY AND BRANCHCD=@BRANCHCD";

                cmd.Parameters.Clear();
                cmd.Parameters.Add("@COMPID", SqlDbType.Int).Value = ob.CmpID;
                cmd.Parameters.Add("@EMPID", SqlDbType.BigInt).Value = ob.EMPID;
                cmd.Parameters.Add("@TRANSMY", SqlDbType.NVarChar).Value = ob.TransMonthYear;
                cmd.Parameters.Add("@BRANCHCD", SqlDbType.NVarChar).Value = ob.BranchCode;

                cmd.Parameters.Add("@BONUSF", SqlDbType.Decimal).Value = ob.Bonusf;
                cmd.Parameters.Add("@INCENTIVE", SqlDbType.Decimal).Value = ob.Incentive;
                cmd.Parameters.Add("@CONVEY", SqlDbType.Decimal).Value = ob.Convey;
                cmd.Parameters.Add("@MOBILE", SqlDbType.Decimal).Value = ob.Mobile;
                cmd.Parameters.Add("@DUEADJ", SqlDbType.Decimal).Value = ob.Dueadj;
                cmd.Parameters.Add("@ADVANCE", SqlDbType.Decimal).Value = ob.Advance;
                cmd.Parameters.Add("@OTHOUR", SqlDbType.Decimal).Value = ob.Othour;
                cmd.Parameters.Add("@FOODING", SqlDbType.Decimal).Value = ob.Fooding;
                cmd.Parameters.Add("@FINEADJ", SqlDbType.Decimal).Value = ob.Fine;
                cmd.Parameters.Add("@DAYLEAVE", SqlDbType.Decimal).Value = ob.DayLeave;
                cmd.Parameters.Add("@REMARKS", SqlDbType.NVarChar).Value = ob.Remarks;

                cmd.Parameters.Add("@FOODINGADD", SqlDbType.NVarChar).Value = ob.Foodinadd;
                cmd.Parameters.Add("@BONUSP", SqlDbType.NVarChar).Value = ob.BonusP;
                cmd.Parameters.Add("@ITAX", SqlDbType.NVarChar).Value = ob.ITax;

                cmd.Parameters.Add("@UPDUSERID", SqlDbType.Int).Value = ob.UPDUserID;
                cmd.Parameters.Add("@UPDTIME", SqlDbType.NVarChar).Value = ob.UPDTime;
                cmd.Parameters.Add("@UPDIPNO", SqlDbType.NVarChar).Value = ob.UPDIpaddress;
                cmd.Parameters.Add("@UPDLTUDE", SqlDbType.NVarChar).Value = ob.UPDLtude;
                cmd.Transaction = tran;
                cmd.ExecuteNonQuery();
                tran.Commit();
                if (con.State != ConnectionState.Closed)
                    con.Close();

            }
            catch (Exception ex)
            {
                tran.Rollback();
                s = ex.Message;
            }
            return s;
        }
        public string Insert_HR_LEAVEYY(PayrollInterface ob)
        {
            string s = "";
            SqlTransaction tran = null;
            try
            {
                if (con.State != ConnectionState.Open)
                    con.Open();
                tran = con.BeginTransaction();
                cmd.CommandType = CommandType.Text;
                cmd.CommandText = @"INSERT INTO HR_LEAVEYY(COMPID,LEAVEYY,LEAVEID,LEAVEDD,REMARKS,USERPC,INSUSERID,INSTIME,INSIPNO,
                               INSLTUDE) Values

                    (@COMPID,@LEAVEYY,@LEAVEID,@LEAVEDD,@REMARKS,@USERPC,@INSUSERID,@INSTIME,@INSIPNO,
                               @INSLTUDE)";
                cmd.Parameters.Clear();
                cmd.Parameters.Add("@COMPID", SqlDbType.Int).Value = ob.CmpID;
                cmd.Parameters.Add("@LEAVEYY", SqlDbType.Int).Value = ob.LEAVEYR;
                cmd.Parameters.Add("@LEAVEID", SqlDbType.Int).Value = ob.LEAVEID;
                cmd.Parameters.Add("@LEAVEDD", SqlDbType.Int).Value = ob.LEAVEDD;
                cmd.Parameters.Add("@REMARKS", SqlDbType.NVarChar).Value = ob.Remarks;
                cmd.Parameters.Add("@USERPC", SqlDbType.NVarChar).Value = ob.UserPC;
                cmd.Parameters.Add("@INSUSERID", SqlDbType.BigInt).Value = ob.UserID;
                cmd.Parameters.Add("@INSTIME", SqlDbType.DateTime).Value = ob.ITime;
                cmd.Parameters.Add("@INSIPNO", SqlDbType.NVarChar).Value = ob.Ipaddress;
                cmd.Parameters.Add("@INSLTUDE", SqlDbType.NVarChar).Value = ob.Ltude;
                cmd.Transaction = tran;
                cmd.ExecuteNonQuery();
                tran.Commit();
                if (con.State != ConnectionState.Closed)
                    con.Close();

            }
            catch (Exception ex)
            {
                tran.Rollback();
                s = ex.Message;
            }
            return s;
        }

        public string Update_HR_LEAVEYY(PayrollInterface ob)
        {
            string s = "";
            SqlTransaction tran = null;
            try
            {
                if (con.State != ConnectionState.Open)
                    con.Open();
                tran = con.BeginTransaction();
                cmd.CommandType = CommandType.Text;
                cmd.CommandText = @"UPDATE  HR_LEAVEYY SET LEAVEDD=@LEAVEDD,REMARKS=@REMARKS,
                            UPDUSERID=@UPDUSERID,UPDTIME=@UPDTIME,UPDIPNO=@UPDIPNO,UPDLTUDE=@UPDLTUDE WHERE LEAVEID=@LEAVEID AND LEAVEYY=@LEAVEYY AND COMPID=@COMPID";

                cmd.Parameters.Clear();
                cmd.Parameters.Add("@COMPID", SqlDbType.Int).Value = ob.CmpID;
                cmd.Parameters.Add("@LEAVEYY", SqlDbType.Int).Value = ob.LEAVEYR;
                cmd.Parameters.Add("@LEAVEID", SqlDbType.Int).Value = ob.LEAVEID;
                cmd.Parameters.Add("@LEAVEDD", SqlDbType.Int).Value = ob.LEAVEDD;
                cmd.Parameters.Add("@REMARKS", SqlDbType.NVarChar).Value = ob.Remarks;
                cmd.Parameters.Add("@UPDUSERID", SqlDbType.Int).Value = ob.UPDUserID;
                cmd.Parameters.Add("@UPDTIME", SqlDbType.NVarChar).Value = ob.UPDTime;
                cmd.Parameters.Add("@UPDIPNO", SqlDbType.NVarChar).Value = ob.UPDIpaddress;
                cmd.Parameters.Add("@UPDLTUDE", SqlDbType.NVarChar).Value = ob.UPDLtude;
                cmd.Transaction = tran;
                cmd.ExecuteNonQuery();
                tran.Commit();
                if (con.State != ConnectionState.Closed)
                    con.Close();

            }
            catch (Exception ex)
            {
                tran.Rollback();
                s = ex.Message;
            }
            return s;
        }

        public string Insert_HR_EMP(PayrollInterface ob)
        {
            string s = "";
            SqlTransaction tran = null;
            try
            {
                if (con.State != ConnectionState.Open)
                    con.Open();
                tran = con.BeginTransaction();
                cmd.CommandType = CommandType.Text;
                cmd.CommandText = @"INSERT INTO HR_EMP (COMPID,EMPID,EMPNM,GUARDIANNM,MOTHERNM,ADDRESS_PRE,ADDRESS_PER,CONTACTNO,EMAILID,DOB,GENDER,VOTERIDNO,BLOODGR,IMAGE,
REFNM1,REFDESIG1,REFADD1,REFCNO1,REFNM2,REFDESIG2,REFADD2,REFCNO2,JOININGDT,DEPTID,EMPTP,COSTPID,SHIFTID,
EMPIDM,CARDNO,CARDIDT,STATUS,USERPC,INSUSERID,INSTIME,INSIPNO) Values
                    (@COMPID,@EMPID,@EMPNM,@GUARDIANNM,@MOTHERNM,@ADDRESS_PRE,@ADDRESS_PER,@CONTACTNO,@EMAILID,@DOB,@GENDER,@VOTERIDNO,@BLOODGR,@IMAGE,
@REFNM1,@REFDESIG1,@REFADD1,@REFCNO1,@REFNM2,@REFDESIG2,@REFADD2,@REFCNO2,@JOININGDT,@DEPTID,@EMPTP,@COSTPID,@SHIFTID,
@EMPIDM,@CARDNO,@CARDIDT,@STATUS,@USERPC,@INSUSERID,@INSTIME,@INSIPNO)";
                cmd.Parameters.Clear();
                cmd.Parameters.Add("@COMPID", SqlDbType.Int).Value = ob.CmpID;
                cmd.Parameters.Add("@EMPID", SqlDbType.Int).Value = ob.EMPID;
                cmd.Parameters.Add("@EMPNM", SqlDbType.NVarChar).Value = ob.EmpNM;
                cmd.Parameters.Add("@GUARDIANNM", SqlDbType.NVarChar).Value = ob.EmpGNM;
                cmd.Parameters.Add("@MOTHERNM", SqlDbType.NVarChar).Value = ob.EmpMNM;
                cmd.Parameters.Add("@ADDRESS_PRE", SqlDbType.NVarChar).Value = ob.PreAdrs;
                cmd.Parameters.Add("@ADDRESS_PER", SqlDbType.NVarChar).Value = ob.PerAdrs;
                cmd.Parameters.Add("@CONTACTNO", SqlDbType.NVarChar).Value = ob.CNO;
                cmd.Parameters.Add("@EMAILID", SqlDbType.NVarChar).Value = ob.Email;
                cmd.Parameters.Add("@DOB", SqlDbType.Date).Value = ob.DOB;
                cmd.Parameters.Add("@GENDER", SqlDbType.NVarChar).Value = ob.Gndr;
                cmd.Parameters.Add("@COSTPID", SqlDbType.NVarChar).Value = ob.CostPoolId;
                cmd.Parameters.Add("@VOTERIDNO", SqlDbType.NVarChar).Value = ob.VotrID;
                cmd.Parameters.Add("@BLOODGR", SqlDbType.NVarChar).Value = ob.BldGRP;
                cmd.Parameters.Add("@IMAGE", SqlDbType.NVarChar).Value = ob.Img;
                cmd.Parameters.Add("@REFNM1", SqlDbType.NVarChar).Value = ob.Ref1NM;
                cmd.Parameters.Add("@REFDESIG1", SqlDbType.NVarChar).Value = ob.Ref1Desig;
                cmd.Parameters.Add("@REFADD1", SqlDbType.NVarChar).Value = ob.Ref1Adrs;
                cmd.Parameters.Add("@REFCNO1", SqlDbType.NVarChar).Value = ob.Ref1CNO;
                cmd.Parameters.Add("@REFNM2", SqlDbType.NVarChar).Value = ob.Ref2NM;
                cmd.Parameters.Add("@REFDESIG2", SqlDbType.NVarChar).Value = ob.Ref2Desig;
                cmd.Parameters.Add("@REFADD2", SqlDbType.NVarChar).Value = ob.Ref2Adrs;
                cmd.Parameters.Add("@REFCNO2", SqlDbType.NVarChar).Value = ob.Ref2CNO;
                cmd.Parameters.Add("@JOININGDT", SqlDbType.Date).Value = ob.JoinDT;
                cmd.Parameters.Add("@DEPTID", SqlDbType.Int).Value = ob.DPTID;
                cmd.Parameters.Add("@SHIFTID", SqlDbType.Int).Value = ob.SHIFTID;
                cmd.Parameters.Add("@EMPTP", SqlDbType.NVarChar).Value = ob.EMPTP;
                cmd.Parameters.Add("@EMPIDM", SqlDbType.NVarChar).Value = ob.EMPIDM;
                cmd.Parameters.Add("@CARDNO", SqlDbType.NVarChar).Value = ob.CRDNO;
                cmd.Parameters.Add("@CARDIDT", SqlDbType.NVarChar).Value = ob.CRDISUDT;
                cmd.Parameters.Add("@STATUS", SqlDbType.NVarChar).Value = ob.Stats;
                cmd.Parameters.Add("@USERPC", SqlDbType.NVarChar).Value = ob.UserPC;
                cmd.Parameters.Add("@INSUSERID", SqlDbType.NVarChar).Value = ob.Username;
                cmd.Parameters.Add("@INSTIME", SqlDbType.SmallDateTime).Value = ob.ITime;
                cmd.Parameters.Add("@INSIPNO", SqlDbType.NVarChar).Value = ob.Ipaddress;
                // cmd.Parameters.Add("@INSLTUDE", SqlDbType.NVarChar).Value = ob.Ltude;
                cmd.Transaction = tran;
                cmd.ExecuteNonQuery();
                tran.Commit();
                if (con.State != ConnectionState.Closed)
                    con.Close();

            }
            catch (Exception ex)
            {
                tran.Rollback();
                s = ex.Message;
            }
            return s;
        }

        public string Update_HR_EMP(PayrollInterface ob)
        {
            string s = "";
            SqlTransaction tran = null;
            try
            {
                if (con.State != ConnectionState.Open)
                    con.Open();
                tran = con.BeginTransaction();
                cmd.CommandType = CommandType.Text;
                cmd.CommandText = @"UPDATE HR_EMP SET EMPNM=@EMPNM,GUARDIANNM=@GUARDIANNM,MOTHERNM=@MOTHERNM,ADDRESS_PRE=@ADDRESS_PRE,ADDRESS_PER=@ADDRESS_PER,
CONTACTNO=@CONTACTNO,EMAILID=@EMAILID,DOB=@DOB,GENDER=@GENDER,VOTERIDNO=@VOTERIDNO,BLOODGR=@BLOODGR,IMAGE=@IMAGE,REFNM1=@REFNM1,REFDESIG1=@REFDESIG1,REFADD1=@REFADD1,REFCNO1=@REFCNO1,REFNM2=@REFNM2,REFDESIG2=@REFDESIG2,
REFADD2=@REFADD2,REFCNO2=@REFCNO2,JOININGDT=@JOININGDT,COSTPID=@COSTPID,SHIFTID=@SHIFTID,
DEPTID=@DEPTID,EMPTP=@EMPTP,EMPIDM=@EMPIDM,CARDNO=@CARDNO,CARDIDT=@CARDIDT,STATUS=@STATUS,UPDUSERID=@UPDUSERID,UPDTIME=@UPDTIME,UPDIPNO=@UPDIPNO WHERE EMPID=@EMPID";
                cmd.Parameters.Clear();
                //cmd.Parameters.Add("@COMPID", SqlDbType.Int).Value = ob.CmpID;
                cmd.Parameters.Add("@EMPID", SqlDbType.Int).Value = ob.EMPID;
                cmd.Parameters.Add("@EMPNM", SqlDbType.NVarChar).Value = ob.EmpNM;
                cmd.Parameters.Add("@GUARDIANNM", SqlDbType.NVarChar).Value = ob.EmpGNM;
                cmd.Parameters.Add("@MOTHERNM", SqlDbType.NVarChar).Value = ob.EmpMNM;
                cmd.Parameters.Add("@ADDRESS_PRE", SqlDbType.NVarChar).Value = ob.PreAdrs;
                cmd.Parameters.Add("@ADDRESS_PER", SqlDbType.NVarChar).Value = ob.PerAdrs;
                cmd.Parameters.Add("@CONTACTNO", SqlDbType.NVarChar).Value = ob.CNO;
                cmd.Parameters.Add("@EMAILID", SqlDbType.NVarChar).Value = ob.Email;
                cmd.Parameters.Add("@DOB", SqlDbType.Date).Value = ob.DOB;
                cmd.Parameters.Add("@GENDER", SqlDbType.NVarChar).Value = ob.Gndr;
                cmd.Parameters.Add("@COSTPID", SqlDbType.NVarChar).Value = ob.CostPoolId;
                cmd.Parameters.Add("@VOTERIDNO", SqlDbType.NVarChar).Value = ob.VotrID;
                cmd.Parameters.Add("@BLOODGR", SqlDbType.NVarChar).Value = ob.BldGRP;
                cmd.Parameters.Add("@IMAGE", SqlDbType.NVarChar).Value = ob.Img;
                cmd.Parameters.Add("@REFNM1", SqlDbType.NVarChar).Value = ob.Ref1NM;
                cmd.Parameters.Add("@REFDESIG1", SqlDbType.NVarChar).Value = ob.Ref1Desig;
                cmd.Parameters.Add("@REFADD1", SqlDbType.NVarChar).Value = ob.Ref1Adrs;
                cmd.Parameters.Add("@REFCNO1", SqlDbType.NVarChar).Value = ob.Ref1CNO;
                cmd.Parameters.Add("@REFNM2", SqlDbType.NVarChar).Value = ob.Ref2NM;
                cmd.Parameters.Add("@REFDESIG2", SqlDbType.NVarChar).Value = ob.Ref2Desig;
                cmd.Parameters.Add("@REFADD2", SqlDbType.NVarChar).Value = ob.Ref2Adrs;
                cmd.Parameters.Add("@REFCNO2", SqlDbType.NVarChar).Value = ob.Ref2CNO;
                cmd.Parameters.Add("@JOININGDT", SqlDbType.Date).Value = ob.JoinDT;
                cmd.Parameters.Add("@DEPTID", SqlDbType.Int).Value = ob.DPTID;
                cmd.Parameters.Add("@SHIFTID", SqlDbType.Int).Value = ob.SHIFTID;
                cmd.Parameters.Add("@EMPTP", SqlDbType.NVarChar).Value = ob.EMPTP;
                cmd.Parameters.Add("@EMPIDM", SqlDbType.NVarChar).Value = ob.EMPIDM;
                cmd.Parameters.Add("@CARDNO", SqlDbType.NVarChar).Value = ob.CRDNO;
                cmd.Parameters.Add("@CARDIDT", SqlDbType.NVarChar).Value = ob.CRDISUDT;
                cmd.Parameters.Add("@STATUS", SqlDbType.NVarChar).Value = ob.Stats;
                cmd.Parameters.Add("@UPDUSERID", SqlDbType.NVarChar).Value = ob.UPDUsername;
                cmd.Parameters.Add("@UPDTIME", SqlDbType.SmallDateTime).Value = ob.UPDTime;
                cmd.Parameters.Add("@UPDIPNO", SqlDbType.NVarChar).Value = ob.UPDIpaddress;
                // cmd.Parameters.Add("@UPDLTUDE", SqlDbType.NVarChar).Value = ob.UPDIpaddress;
                //cmd.Parameters.Add("@INSLTUDE", SqlDbType.NVarChar).Value = ob.Ltude;
                cmd.Transaction = tran;
                cmd.ExecuteNonQuery();
                tran.Commit();
                if (con.State != ConnectionState.Closed)
                    con.Close();

            }
            catch (Exception ex)
            {
                tran.Rollback();
                s = ex.Message;
            }
            return s;
        }


        public string INSERT_HR_EMPSALARY(PayrollInterface ob)
        {
            string s = "";
            SqlTransaction tran = null;
            try
            {
                if (con.State != ConnectionState.Open)
                    con.Open();
                tran = con.BeginTransaction();
                cmd.CommandType = CommandType.Text;
                cmd.CommandText = @"INSERT INTO HR_EMPSALARY(COMPID,EMPID,POSTID,SALSTATUS,BASICSAL,HOUSERENT,MEDICAL,TRANSPORT,RSTAMP,CONVEY,PFRATE,PFEFDT,PFETDT,JOBEFDT,JOBETDT,OTRTHOUR,PBONUSRT,OTRTDAY,USERPC,INSUSERID,INSTIME,INSIPNO)
 				Values 
				(@COMPID,@EMPID,@POSTID,@SALSTATUS,@BASICSAL,@HOUSERENT,@MEDICAL,@TRANSPORT,@RSTAMP,@CONVEY,@PFRATE,@PFEFDT,@PFETDT,@JOBEFDT,@JOBETDT,@OTRTHOUR,@PBONUSRT,@OTRTDAY,@USERPC,@INSUSERID,@INSTIME,@INSIPNO)";
                cmd.Parameters.Clear();
                cmd.Parameters.Add("@COMPID", SqlDbType.Int).Value = ob.CmpID;
                cmd.Parameters.Add("@EMPID", SqlDbType.Int).Value = ob.EMPID;
                cmd.Parameters.Add("@POSTID", SqlDbType.Int).Value = ob.PostID;
                cmd.Parameters.Add("@SALSTATUS", SqlDbType.NVarChar).Value = ob.Status;
                cmd.Parameters.Add("@BASICSAL", SqlDbType.Decimal).Value = ob.BasicSal;
                cmd.Parameters.Add("@HOUSERENT", SqlDbType.Decimal).Value = ob.HouseRent;
                cmd.Parameters.Add("@MEDICAL", SqlDbType.Decimal).Value = ob.Medical;
                cmd.Parameters.Add("@TRANSPORT", SqlDbType.Decimal).Value = ob.TrnsPort;
                cmd.Parameters.Add("@RSTAMP", SqlDbType.Decimal).Value = ob.Revenue;
                cmd.Parameters.Add("@PFRATE", SqlDbType.Decimal).Value = ob.PFRate;
                cmd.Parameters.Add("@CONVEY", SqlDbType.Decimal).Value = ob.Convey;
                cmd.Parameters.Add("@PFEFDT", SqlDbType.Date).Value = ob.PFEffectFR;
                cmd.Parameters.Add("@PFETDT", SqlDbType.Date).Value = ob.PFEffectTO;
                cmd.Parameters.Add("@JOBEFDT", SqlDbType.Date).Value = ob.JOBEffectFR;
                cmd.Parameters.Add("@JOBETDT", SqlDbType.Date).Value = ob.JOBEffectTO;
                cmd.Parameters.Add("@OTRTHOUR", SqlDbType.Decimal).Value = ob.OTRThour;
                cmd.Parameters.Add("@PBONUSRT", SqlDbType.Decimal).Value = ob.PresentRT;
                cmd.Parameters.Add("@OTRTDAY", SqlDbType.Decimal).Value = ob.OTRTday;
                cmd.Parameters.Add("@USERPC", SqlDbType.NVarChar).Value = ob.UserPC;
                cmd.Parameters.Add("@INSUSERID", SqlDbType.Int).Value = ob.UserID;
                cmd.Parameters.Add("@INSTIME", SqlDbType.DateTime).Value = ob.ITime;
                cmd.Parameters.Add("@INSIPNO", SqlDbType.NVarChar).Value = ob.Ipaddress;
                cmd.Transaction = tran;
                cmd.ExecuteNonQuery();
                tran.Commit();
                if (con.State != ConnectionState.Closed)
                    con.Close();

            }
            catch (Exception ex)
            {
                tran.Rollback();
                s = ex.Message;
            }
            return s;
        }
        public string UPDATE_HR_EMPSALARY(PayrollInterface ob)
        {
            string s = "";
            SqlTransaction tran = null;
            try
            {
                if (con.State != ConnectionState.Open)
                    con.Open();
                tran = con.BeginTransaction();
                cmd.CommandType = CommandType.Text;
                cmd.CommandText = @"UPDATE HR_EMPSALARY SET SALSTATUS=@SALSTATUS,BASICSAL=@BASICSAL,HOUSERENT=@HOUSERENT,MEDICAL=@MEDICAL,TRANSPORT=@TRANSPORT,RSTAMP=@RSTAMP,CONVEY=@CONVEY,PFRATE=@PFRATE,PFEFDT=@PFEFDT,PFETDT=@PFETDT,JOBEFDT=@JOBEFDT,JOBETDT=@JOBETDT,OTRTHOUR=@OTRTHOUR,PBONUSRT=@PBONUSRT,OTRTDAY=@OTRTDAY,USERPC=@USERPC,UPDUSERID=@UPDUSERID,UPDTIME=@UPDTIME,UPDIPNO=@UPDIPNO WHERE POSTID=@POSTID AND COMPID=@COMPID AND EMPID=@EMPID AND SALSTATUS=@SALSTATUS ";
                cmd.Parameters.Clear();
                cmd.Parameters.Add("@COMPID", SqlDbType.Int).Value = ob.CmpID;
                cmd.Parameters.Add("@EMPID", SqlDbType.Int).Value = ob.EMPID;
                cmd.Parameters.Add("@POSTID", SqlDbType.Int).Value = ob.PostID;
                cmd.Parameters.Add("@SALSTATUS", SqlDbType.NVarChar).Value = ob.Status;
                cmd.Parameters.Add("@BASICSAL", SqlDbType.Decimal).Value = ob.BasicSal;
                cmd.Parameters.Add("@HOUSERENT", SqlDbType.Decimal).Value = ob.HouseRent;
                cmd.Parameters.Add("@MEDICAL", SqlDbType.Decimal).Value = ob.Medical;
                cmd.Parameters.Add("@TRANSPORT", SqlDbType.Decimal).Value = ob.TrnsPort;
                cmd.Parameters.Add("@RSTAMP", SqlDbType.Decimal).Value = ob.Revenue;
                cmd.Parameters.Add("@CONVEY", SqlDbType.Decimal).Value = ob.Convey;
                cmd.Parameters.Add("@PFRATE", SqlDbType.Decimal).Value = ob.PFRate;
                cmd.Parameters.Add("@PFEFDT", SqlDbType.Date).Value = ob.PFEffectFR;
                cmd.Parameters.Add("@PFETDT", SqlDbType.Date).Value = ob.PFEffectTO;
                cmd.Parameters.Add("@JOBEFDT", SqlDbType.Date).Value = ob.JOBEffectFR;
                cmd.Parameters.Add("@JOBETDT", SqlDbType.Date).Value = ob.JOBEffectTO;
                cmd.Parameters.Add("@OTRTHOUR", SqlDbType.Decimal).Value = ob.OTRThour;
                cmd.Parameters.Add("@PBONUSRT", SqlDbType.Decimal).Value = ob.PresentRT;
                cmd.Parameters.Add("@OTRTDAY", SqlDbType.Decimal).Value = ob.OTRTday;
                cmd.Parameters.Add("@USERPC", SqlDbType.NVarChar).Value = ob.UserPC;
                cmd.Parameters.Add("@UPDUSERID", SqlDbType.Int).Value = ob.UPDUserID;
                cmd.Parameters.Add("@UPDTIME", SqlDbType.DateTime).Value = ob.UPDTime;
                cmd.Parameters.Add("@UPDIPNO", SqlDbType.NVarChar).Value = ob.UPDIpaddress;
                cmd.Transaction = tran;
                cmd.ExecuteNonQuery();
                tran.Commit();
                if (con.State != ConnectionState.Closed)
                    con.Close();

            }
            catch (Exception ex)
            {
                tran.Rollback();
                s = ex.Message;
            }
            return s;
        }
        public string DELETE_HR_EMPSALARY(PayrollInterface ob)
        {
            string s = "";
            SqlTransaction tran = null;
            try
            {
                if (con.State != ConnectionState.Open)
                    con.Open();
                tran = con.BeginTransaction();
                cmd.CommandType = CommandType.Text;

                cmd.CommandText = "DELETE FROM HR_EMPSALARY WHERE POSTID=@POSTID AND COMPID=@COMPID AND EMPID=@EMPID AND SALSTATUS=@SALSTATUS ";
                cmd.Parameters.Clear();
                cmd.Parameters.Add("@POSTID", SqlDbType.Int).Value = ob.PostID;
                cmd.Parameters.Add("@COMPID", SqlDbType.Int).Value = ob.CmpID;
                cmd.Parameters.Add("@EMPID", SqlDbType.Int).Value = ob.EMPID;
                cmd.Parameters.Add("@SALSTATUS", SqlDbType.NVarChar).Value = ob.Status;

                cmd.Transaction = tran;
                cmd.ExecuteNonQuery();
                tran.Commit();
                if (con.State != ConnectionState.Closed)
                    con.Close();

            }
            catch (Exception ex)
            {
                tran.Rollback();
                s = ex.Message;
            }
            return s;
        }


        public string Insert_Salary_Precess(PayrollInterface ob)
        {
            string s = "";
            SqlTransaction tran = null;
            try
            {
                if (con.State != ConnectionState.Open)
                    con.Open();
                tran = con.BeginTransaction();
                cmd.CommandType = CommandType.Text;
                cmd.CommandText = @"INSERT INTO HR_SALPAY(COMPID, EMPID, POSTID, TRANSMY, BRANCHCD, BASICSAL, HOUSERENT, MEDICAL, TRANSPORT, GROSSSAL, 
                                DAYTOT, DAYWORK, DAYHOLI, DAYPRE, DAYLEAVE, DAYABS, OTDAY, OTHOUR, ALWDAILYNW, ALWDAILYOT, BONUSP, 
                                BONUSF, INCENTIVE, PFADD, CONVEY, MOBILE, DUEADJ, TOTADD, ADVANCE, PFDED, ABSDED, AMTOTDAYS, AMTOTHOUR, 
                                STAMP, FOODING, FINEADJ, TOTDED, NETPAY, USERPC, INSUSERID, INSTIME, INSIPNO, INSLTUDE, FOODINGADD, ITAX) 

                                VALUES(@COMPID, @EMPID, @POSTID, @TRANSMY, @BRANCHCD, @BASICSAL, @HOUSERENT, @MEDICAL, @TRANSPORT, @GROSSSAL, 
                                @DAYTOT, @DAYWORK, @DAYHOLI, @DAYPRE, @DAYLEAVE, @DAYABS, @OTDAY, @OTHOUR, @ALWDAILYNW, @ALWDAILYOT, @BONUSP, 
                                @BONUSF, @INCENTIVE, @PFADD, @CONVEY, @MOBILE, @DUEADJ, @TOTADD, @ADVANCE, @PFDED, @ABSDED,  @AMTOTDAYS, @AMTOTHOUR, 
                                @STAMP, @FOODING, @FINEADJ, @TOTDED, @NETPAY, @USERPC, @INSUSERID, @INSTIME, @INSIPNO, @INSLTUDE , @FOODINGADD, @ITAX)";
                cmd.Parameters.Clear();

                cmd.Parameters.Add("@COMPID", SqlDbType.Int).Value = ob.CmpID;
                cmd.Parameters.Add("@EMPID", SqlDbType.BigInt).Value = ob.EMPID;
                cmd.Parameters.Add("@TRANSMY", SqlDbType.NVarChar).Value = ob.TransMonthYear;
                cmd.Parameters.Add("@BRANCHCD", SqlDbType.BigInt).Value = ob.BranchCode;
                cmd.Parameters.Add("@POSTID", SqlDbType.Int).Value = ob.PostID;

                cmd.Parameters.Add("@ALWDAILYNW", SqlDbType.Decimal).Value = ob.Alwdaily;
                cmd.Parameters.Add("@ALWDAILYOT", SqlDbType.Decimal).Value = ob.AlwdailyOt;
                cmd.Parameters.Add("@BONUSP", SqlDbType.Decimal).Value = ob.BonusP;
                cmd.Parameters.Add("@BONUSF", SqlDbType.Decimal).Value = ob.Bonusf;
                cmd.Parameters.Add("@INCENTIVE", SqlDbType.Decimal).Value = ob.Incentive;
                cmd.Parameters.Add("@PFADD", SqlDbType.Decimal).Value = ob.PfAdd;

                cmd.Parameters.Add("@CONVEY", SqlDbType.Decimal).Value = ob.Convey;
                cmd.Parameters.Add("@MOBILE", SqlDbType.Decimal).Value = ob.Mobile;
                cmd.Parameters.Add("@DUEADJ", SqlDbType.Decimal).Value = ob.Dueadj;
                cmd.Parameters.Add("@ADVANCE", SqlDbType.Decimal).Value = ob.Advance;

                cmd.Parameters.Add("@AMTOTDAYS", SqlDbType.Decimal).Value = ob.AmountTotalDays;
                cmd.Parameters.Add("@AMTOTHOUR", SqlDbType.Decimal).Value = ob.AmountTotalHours;

                cmd.Parameters.Add("@PFDED", SqlDbType.Decimal).Value = ob.PfDed;
                cmd.Parameters.Add("@STAMP", SqlDbType.Decimal).Value = ob.Stamp;
                cmd.Parameters.Add("@FOODING", SqlDbType.Decimal).Value = ob.Fooding;
                cmd.Parameters.Add("@FINEADJ", SqlDbType.Decimal).Value = ob.Fine;

                cmd.Parameters.Add("@BASICSAL", SqlDbType.Decimal).Value = ob.BasicSal;
                cmd.Parameters.Add("@HOUSERENT", SqlDbType.Decimal).Value = ob.HouseRent;
                cmd.Parameters.Add("@MEDICAL", SqlDbType.Decimal).Value = ob.Medical;
                cmd.Parameters.Add("@TRANSPORT", SqlDbType.Decimal).Value = ob.TransPort;
                cmd.Parameters.Add("@GROSSSAL", SqlDbType.Decimal).Value = ob.GrossSal;

                cmd.Parameters.Add("@DAYTOT", SqlDbType.Decimal).Value = ob.DayTot;
                cmd.Parameters.Add("@DAYWORK", SqlDbType.Decimal).Value = ob.DayWork;
                cmd.Parameters.Add("@DAYHOLI", SqlDbType.Decimal).Value = ob.DayHoli;
                cmd.Parameters.Add("@DAYPRE", SqlDbType.Decimal).Value = ob.DayPre;
                cmd.Parameters.Add("@DAYLEAVE", SqlDbType.Decimal).Value = ob.DayLeave;
                cmd.Parameters.Add("@DAYABS", SqlDbType.Decimal).Value = ob.DayAbs;

                cmd.Parameters.Add("@ABSDED", SqlDbType.Decimal).Value = ob.AbsDed;
                cmd.Parameters.Add("@OTDAY", SqlDbType.Decimal).Value = ob.OtDay;
                cmd.Parameters.Add("@OTHOUR", SqlDbType.Decimal).Value = ob.Othour;
                cmd.Parameters.Add("@TOTADD", SqlDbType.Decimal).Value = ob.TotAdd;
                cmd.Parameters.Add("@TOTDED", SqlDbType.Decimal).Value = ob.TotDed;
                
                cmd.Parameters.Add("@NETPAY", SqlDbType.Decimal).Value = ob.NetPay;

                cmd.Parameters.Add("@FOODINGADD", SqlDbType.Decimal).Value = ob.Foodinadd;
                cmd.Parameters.Add("@ITAX", SqlDbType.Decimal).Value = ob.ITax;

                cmd.Parameters.Add("@USERPC", SqlDbType.NVarChar).Value = ob.UserPC;
                cmd.Parameters.Add("@INSUSERID", SqlDbType.Int).Value = ob.UserID;
                cmd.Parameters.Add("@INSTIME", SqlDbType.NVarChar).Value = ob.ITime;
                cmd.Parameters.Add("@INSIPNO", SqlDbType.NVarChar).Value = ob.Ipaddress;
                cmd.Parameters.Add("@INSLTUDE", SqlDbType.NVarChar).Value = ob.Ltude;
                cmd.Transaction = tran;
                cmd.ExecuteNonQuery();
                tran.Commit();
                if (con.State != ConnectionState.Closed)
                    con.Close();

            }
            catch (Exception ex)
            {
                tran.Rollback();
                s = ex.Message;
            }
            return s;
        }

        public string Insert_HR_Attendance(PayrollInterface ob)
        {
            string s = "";
            SqlTransaction tran = null;
            try
            {
                if (con.State != ConnectionState.Open)
                    con.Open();
                tran = con.BeginTransaction();
                cmd.CommandType = CommandType.Text;
                cmd.CommandText = @"INSERT INTO HR_ATREG(TRANSDT, EMPID, SHIFTID, TIMEIN, TIMEOUT, ENTRYTPI, ENTRYTPO, 
                USERPC, INSUSERID, INSTIME, INSIPNO, INSLTUDE, LATEHR,OTHOUR) 
                VALUES(@TRANSDT, @EMPID, @SHIFTID, @TIMEIN, @TIMEOUT, @ENTRYTPI, @ENTRYTPO, 
                @USERPC, @INSUSERID, @INSTIME, @INSIPNO, @INSLTUDE,@LATEHR,@OTHOUR)";
                cmd.Parameters.Clear();
                cmd.Parameters.Add("@TRANSDT", SqlDbType.DateTime).Value = ob.TransDT;
                cmd.Parameters.Add("@EMPID", SqlDbType.BigInt).Value = ob.EMPID;
                cmd.Parameters.Add("@SHIFTID", SqlDbType.BigInt).Value = ob.SHIFTID;

                cmd.Parameters.Add("@TIMEIN", SqlDbType.Time).Value = ob.AttInTime;
                cmd.Parameters.Add("@TIMEOUT", SqlDbType.Time).Value = ob.AttOutTime;

                cmd.Parameters.Add("@LATEHR", SqlDbType.Time).Value = ob.attLate;
                cmd.Parameters.Add("@OTHOUR", SqlDbType.Time).Value = ob.attOTHour;

                cmd.Parameters.Add("@ENTRYTPI", SqlDbType.NVarChar).Value = ob.EntryTypeIn;
                cmd.Parameters.Add("@ENTRYTPO", SqlDbType.NVarChar).Value = ob.EntryTypeOut;

                cmd.Parameters.Add("@USERPC", SqlDbType.NVarChar).Value = ob.UserPC;
                cmd.Parameters.Add("@INSUSERID", SqlDbType.Int).Value = ob.UserID;
                cmd.Parameters.Add("@INSTIME", SqlDbType.NVarChar).Value = ob.ITime;
                cmd.Parameters.Add("@INSIPNO", SqlDbType.NVarChar).Value = ob.Ipaddress;
                cmd.Parameters.Add("@INSLTUDE", SqlDbType.NVarChar).Value = ob.Ltude;
                cmd.Transaction = tran;
                cmd.ExecuteNonQuery();
                tran.Commit();
                if (con.State != ConnectionState.Closed)
                    con.Close();

            }
            catch (Exception ex)
            {
                tran.Rollback();
                s = ex.Message;
            }
            return s;
        }

        public string Update_HR_Attendance(PayrollInterface ob)
        {
            string s = "";
            SqlTransaction tran = null;
            try
            {
                if (con.State != ConnectionState.Open)
                    con.Open();
                tran = con.BeginTransaction();
                cmd.CommandType = CommandType.Text;
                cmd.CommandText = @"UPDATE HR_ATREG SET TIMEIN=@TIMEIN, TIMEOUT=@TIMEOUT, ENTRYTPI=@ENTRYTPI, ENTRYTPO=@ENTRYTPO,
                UPDUSERID=@UPDUSERID,UPDTIME=@UPDTIME,UPDIPNO=@UPDIPNO,UPDLTUDE=@UPDLTUDE,LATEHR=@LATEHR,OTHOUR=@OTHOUR 
                WHERE TRANSDT=@TRANSDT AND EMPID=@EMPID";

                cmd.Parameters.Clear();
                cmd.Parameters.Add("@TRANSDT", SqlDbType.DateTime).Value = ob.TransDT;
                cmd.Parameters.Add("@EMPID", SqlDbType.BigInt).Value = ob.EMPID;

                cmd.Parameters.Add("@TIMEIN", SqlDbType.Time).Value = ob.AttInTime;
                cmd.Parameters.Add("@TIMEOUT", SqlDbType.Time).Value = ob.AttOutTime;

                cmd.Parameters.Add("@LATEHR", SqlDbType.Time).Value = ob.attLate;
                cmd.Parameters.Add("@OTHOUR", SqlDbType.Time).Value = ob.attOTHour;

                cmd.Parameters.Add("@ENTRYTPI", SqlDbType.VarChar).Value = ob.EntryTypeIn;
                cmd.Parameters.Add("@ENTRYTPO", SqlDbType.VarChar).Value = ob.EntryTypeOut;

                cmd.Parameters.Add("@UPDUSERPC", SqlDbType.NVarChar).Value = ob.UPDUserPC;
                cmd.Parameters.Add("@UPDUSERID", SqlDbType.Int).Value = ob.UPDUserID;
                cmd.Parameters.Add("@UPDTIME", SqlDbType.NVarChar).Value = ob.UPDTime;
                cmd.Parameters.Add("@UPDIPNO", SqlDbType.NVarChar).Value = ob.UPDIpaddress;
                cmd.Parameters.Add("@UPDLTUDE", SqlDbType.NVarChar).Value = ob.UPDLtude;
                cmd.Transaction = tran;
                cmd.ExecuteNonQuery();
                tran.Commit();
                if (con.State != ConnectionState.Closed)
                    con.Close();

            }
            catch (Exception ex)
            {
                tran.Rollback();
                s = ex.Message;
            }
            return s;
        }
        public string Update_HR_AttendanceAutoMachine(PayrollInterface ob)
        {
            string s = "";
            SqlTransaction tran = null;
            try
            {
                if (con.State != ConnectionState.Open)
                    con.Open();
                tran = con.BeginTransaction();
                cmd.CommandType = CommandType.Text;
                cmd.CommandText = @"UPDATE HR_ATREG SET  TIMEOUT=@TIMEOUT, ENTRYTPO=@ENTRYTPO,
                UPDUSERID=@UPDUSERID,UPDTIME=@UPDTIME,UPDIPNO=@UPDIPNO,UPDLTUDE=@UPDLTUDE 
                WHERE TRANSDT=@TRANSDT AND EMPID=@EMPID";

                cmd.Parameters.Clear();
                cmd.Parameters.Add("@TRANSDT", SqlDbType.DateTime).Value = ob.TransDT;
                cmd.Parameters.Add("@EMPID", SqlDbType.BigInt).Value = ob.EMPID;
                
                cmd.Parameters.Add("@TIMEOUT", SqlDbType.Time).Value = ob.AttOutTime;
                cmd.Parameters.Add("@ENTRYTPO", SqlDbType.VarChar).Value = ob.EntryTypeOut;

                cmd.Parameters.Add("@UPDUSERPC", SqlDbType.NVarChar).Value = ob.UserPC;
                cmd.Parameters.Add("@UPDUSERID", SqlDbType.Int).Value = ob.UserID;
                cmd.Parameters.Add("@UPDTIME", SqlDbType.NVarChar).Value = ob.ITime;
                cmd.Parameters.Add("@UPDIPNO", SqlDbType.NVarChar).Value = ob.Ipaddress;
                cmd.Parameters.Add("@UPDLTUDE", SqlDbType.NVarChar).Value = ob.Ltude;
                cmd.Transaction = tran;
                cmd.ExecuteNonQuery();
                tran.Commit();
                if (con.State != ConnectionState.Closed)
                    con.Close();

            }
            catch (Exception ex)
            {
                tran.Rollback();
                s = ex.Message;
            }
            return s;
        }
        public string Delete_HR_Attendance(PayrollInterface ob)
        {
            string s = "";
            SqlTransaction tran = null;
            try
            {
                if (con.State != ConnectionState.Open)
                    con.Open();
                tran = con.BeginTransaction();
                cmd.CommandType = CommandType.Text;
                cmd.CommandText = @"DELETE FROM HR_ATREG WHERE TRANSDT=@TRANSDT AND EMPID=@EMPID";

                cmd.Parameters.Clear();
                cmd.Parameters.Add("@TRANSDT", SqlDbType.Int).Value = ob.TransDT;
                cmd.Parameters.Add("@EMPID", SqlDbType.BigInt).Value = ob.EMPID;
                cmd.Transaction = tran;
                cmd.ExecuteNonQuery();
                tran.Commit();
                if (con.State != ConnectionState.Closed)
                    con.Close();

            }
            catch (Exception ex)
            {
                tran.Rollback();
                s = ex.Message;
            }
            return s;
        }

        public string Insert_HR_Atot(PayrollInterface ob)
        {
            string s = "";
            SqlTransaction tran = null;
            try
            {
                if (con.State != ConnectionState.Open)
                    con.Open();
                tran = con.BeginTransaction();
                cmd.CommandType = CommandType.Text;
                cmd.CommandText = @"INSERT INTO HR_ATOT(TRANSDT, EMPID, SHIFTID, DAYSTATS, TIMEIN, TIMEOUT, OTDAY, 
                OTHOUR, USERPC, INSUSERID, INSTIME, INSIPNO, INSLTUDE) 
                VALUES(@TRANSDT, @EMPID, @SHIFTID, @DAYSTATS, @TIMEIN, @TIMEOUT, @OTDAY, 
                @OTHOUR, @USERPC, @INSUSERID, @INSTIME, @INSIPNO, @INSLTUDE)";
                cmd.Parameters.Clear();
                cmd.Parameters.Add("@TRANSDT", SqlDbType.DateTime).Value = ob.TransDT;
                cmd.Parameters.Add("@EMPID", SqlDbType.BigInt).Value = ob.EMPID;
                cmd.Parameters.Add("@SHIFTID", SqlDbType.BigInt).Value = ob.SHIFTID;
                cmd.Parameters.Add("@DAYSTATS", SqlDbType.NVarChar).Value = ob.Stats;

                cmd.Parameters.Add("@TIMEIN", SqlDbType.Time).Value = ob.AttInTime;
                cmd.Parameters.Add("@TIMEOUT", SqlDbType.Time).Value = ob.AttOutTime;
                cmd.Parameters.Add("@OTDAY", SqlDbType.Decimal).Value = ob.OtDay;
                cmd.Parameters.Add("@OTHOUR", SqlDbType.Time).Value = ob.OtDifferenceTime;

                cmd.Parameters.Add("@USERPC", SqlDbType.NVarChar).Value = ob.UserPC;
                cmd.Parameters.Add("@INSUSERID", SqlDbType.Int).Value = ob.UserID;
                cmd.Parameters.Add("@INSTIME", SqlDbType.NVarChar).Value = ob.ITime;
                cmd.Parameters.Add("@INSIPNO", SqlDbType.NVarChar).Value = ob.Ipaddress;
                cmd.Parameters.Add("@INSLTUDE", SqlDbType.NVarChar).Value = ob.Ltude;
                cmd.Transaction = tran;
                cmd.ExecuteNonQuery();
                tran.Commit();
                if (con.State != ConnectionState.Closed)
                    con.Close();

            }
            catch (Exception ex)
            {
                tran.Rollback();
                s = ex.Message;
            }
            return s;
        }

        /******************************************************/
        ////////////////////////////////////////////////////////
        ////////////////////////////////////////////////////////
        //*********************EMPLOYEE LOAN*******//
        ////////////////////////////////////////////////////////
        ////////////////////////////////////////////////////////
        ////////////////////////////////////////////////////////

        public string Insert_HR_EMPLOYEELOAN(PayrollInterface ob)
        {
            string s = "";
            SqlTransaction tran = null;
            try
            {
                if (con.State != ConnectionState.Open)
                    con.Open();
                tran = con.BeginTransaction();
                cmd.CommandType = CommandType.Text;
                cmd.CommandText = @"INSERT INTO HR_EMPLOAN(TRANSDT,TRANSMY,TRANSNO,EMPID,LOANAMT,DEDAMT,DEDEFDT,DEDETDT,REMARKS,USERPC, INSUSERID, INSTIME, INSIPNO, INSLTUDE) 
                VALUES(@TRANSDT,@TRANSMY,@TRANSNO,@EMPID,@LOANAMT,@DEDAMT,@DEDEFDT,@DEDETDT,@REMARKS,@USERPC,@INSUSERID,@INSTIME,@INSIPNO, @INSLTUDE)";
                cmd.Parameters.Clear();
                cmd.Parameters.Add("@TRANSDT", SqlDbType.DateTime).Value = ob.Date;
                cmd.Parameters.Add("@TRANSMY", SqlDbType.NVarChar).Value = ob.MonthYear;
                cmd.Parameters.Add("@TRANSNO", SqlDbType.BigInt).Value = ob.TransNo;
                cmd.Parameters.Add("@EMPID", SqlDbType.BigInt).Value = ob.EmployeeID;

                cmd.Parameters.Add("@LOANAMT", SqlDbType.Decimal).Value = ob.LoanAM;
                cmd.Parameters.Add("@DEDAMT", SqlDbType.Decimal).Value = ob.DeductionAM;
                cmd.Parameters.Add("@DEDEFDT", SqlDbType.DateTime).Value = ob.fdt;
                cmd.Parameters.Add("@DEDETDT", SqlDbType.DateTime).Value = ob.tdt;
                cmd.Parameters.Add("@REMARKS", SqlDbType.NVarChar).Value = ob.Remarks;

                cmd.Parameters.Add("@USERPC", SqlDbType.NVarChar).Value = ob.UserPC;
                cmd.Parameters.Add("@INSUSERID", SqlDbType.Int).Value = ob.Username;
                cmd.Parameters.Add("@INSTIME", SqlDbType.NVarChar).Value = ob.ITime;
                cmd.Parameters.Add("@INSIPNO", SqlDbType.NVarChar).Value = ob.Ipaddress;
                cmd.Parameters.Add("@INSLTUDE", SqlDbType.NVarChar).Value = ob.Ltude;
                cmd.Transaction = tran;
                cmd.ExecuteNonQuery();
                tran.Commit();
                if (con.State != ConnectionState.Closed)
                    con.Close();

            }
            catch (Exception ex)
            {
                tran.Rollback();
                s = ex.Message;
            }
            return s;
        }

        public string Update_HR_EMPLOYEELOAN(PayrollInterface ob)
        {
            string s = "";
            SqlTransaction tran = null;
            try
            {
                if (con.State != ConnectionState.Open)
                    con.Open();
                tran = con.BeginTransaction();
                cmd.CommandType = CommandType.Text;
                cmd.CommandText = @"UPDATE HR_EMPLOAN SET EMPID=@EMPID,LOANAMT=@LOANAMT,DEDAMT=@DEDAMT,DEDEFDT=@DEDEFDT,DEDETDT=@DEDETDT,REMARKS=@REMARKS,
                UPDUSERID=@UPDUSERID,UPDTIME=@UPDTIME,UPDIPNO=@UPDIPNO,UPDLTUDE=@UPDLTUDE 
                WHERE TRANSDT=@TRANSDT AND TRANSNO=@TRANSNO AND TRANSMY=@TRANSMY";

                cmd.Parameters.Clear();
                cmd.Parameters.Add("@TRANSDT", SqlDbType.DateTime).Value = ob.Date;
                cmd.Parameters.Add("@TRANSMY", SqlDbType.NVarChar).Value = ob.MonthYear;
                cmd.Parameters.Add("@TRANSNO", SqlDbType.BigInt).Value = ob.TransNo;
                cmd.Parameters.Add("@EMPID", SqlDbType.BigInt).Value = ob.EmployeeID;

                cmd.Parameters.Add("@LOANAMT", SqlDbType.Decimal).Value = ob.LoanAM;
                cmd.Parameters.Add("@DEDAMT", SqlDbType.Decimal).Value = ob.DeductionAM;

                cmd.Parameters.Add("@DEDEFDT", SqlDbType.DateTime).Value = ob.fdt;
                cmd.Parameters.Add("@DEDETDT", SqlDbType.DateTime).Value = ob.tdt;
                cmd.Parameters.Add("@REMARKS", SqlDbType.NVarChar).Value = ob.Remarks;

                cmd.Parameters.Add("@UPDUSERPC", SqlDbType.NVarChar).Value = ob.UPDUserPC;
                cmd.Parameters.Add("@UPDUSERID", SqlDbType.Int).Value = ob.UPDUsername;
                cmd.Parameters.Add("@UPDTIME", SqlDbType.NVarChar).Value = ob.UPDTime;
                cmd.Parameters.Add("@UPDIPNO", SqlDbType.NVarChar).Value = ob.UPDIpaddress;
                cmd.Parameters.Add("@UPDLTUDE", SqlDbType.NVarChar).Value = ob.Ltude;
                cmd.Transaction = tran;
                cmd.ExecuteNonQuery();
                tran.Commit();
                if (con.State != ConnectionState.Closed)
                    con.Close();

            }
            catch (Exception ex)
            {
                tran.Rollback();
                s = ex.Message;
            }
            return s;
        }

        /******************************************************/
        ////////////////////////////////////////////////////////
        ////////////////////////////////////////////////////////
        //*******************LEAVE APPLICATION****************//
        ////////////////////////////////////////////////////////
        ////////////////////////////////////////////////////////
        ////////////////////////////////////////////////////////

        public string Insert_HR_LEAVEAPPS_MST(PayrollInterface ob)
        {
            string s = "";
            SqlTransaction tran = null;
            try
            {
                if (con.State != ConnectionState.Open)
                    con.Open();
                tran = con.BeginTransaction();
                cmd.CommandType = CommandType.Text;
                cmd.CommandText = @"INSERT INTO HR_LAPPSMST(TRANSDT,TRANSMY,TRANSNO,EMPID,REMARKS,USERPC, INSUSERID, INSTIME, INSIPNO, INSLTUDE) 
                VALUES(@TRANSDT,@TRANSMY,@TRANSNO,@EMPID,@REMARKS,@USERPC,@INSUSERID,@INSTIME,@INSIPNO, @INSLTUDE)";
                cmd.Parameters.Clear();
                cmd.Parameters.Add("@TRANSDT", SqlDbType.DateTime).Value = ob.Date;
                cmd.Parameters.Add("@TRANSMY", SqlDbType.NVarChar).Value = ob.MonthYear;
                cmd.Parameters.Add("@TRANSNO", SqlDbType.BigInt).Value = ob.TRANSNO;
                cmd.Parameters.Add("@EMPID", SqlDbType.BigInt).Value = ob.EmployeeID;
                cmd.Parameters.Add("@REMARKS", SqlDbType.NVarChar).Value = ob.Remarks;

                cmd.Parameters.Add("@USERPC", SqlDbType.NVarChar).Value = ob.UserPC;
                cmd.Parameters.Add("@INSUSERID", SqlDbType.Int).Value = ob.Username;
                cmd.Parameters.Add("@INSTIME", SqlDbType.NVarChar).Value = ob.ITime;
                cmd.Parameters.Add("@INSIPNO", SqlDbType.NVarChar).Value = ob.Ipaddress;
                cmd.Parameters.Add("@INSLTUDE", SqlDbType.NVarChar).Value = ob.Ltude;
                cmd.Transaction = tran;
                cmd.ExecuteNonQuery();
                tran.Commit();
                if (con.State != ConnectionState.Closed)
                    con.Close();

            }
            catch (Exception ex)
            {
                tran.Rollback();
                s = ex.Message;
            }
            return s;
        }

        public string Insert_HR_LEAVEAPPS(PayrollInterface ob)
        {
            string s = "";
            SqlTransaction tran = null;
            try
            {
                if (con.State != ConnectionState.Open)
                    con.Open();
                tran = con.BeginTransaction();
                cmd.CommandType = CommandType.Text;
                cmd.CommandText = @"INSERT INTO HR_LAPPS(TRANSDT,TRANSMY,TRANSNO,EMPID,LEAVEID, LEAVEFR,LEAVETO,LEAVEDAYS,REASON,USERPC, INSUSERID, INSTIME, INSIPNO, INSLTUDE) 
                VALUES(@TRANSDT,@TRANSMY,@TRANSNO,@EMPID,@LEAVEID,@LEAVEFR,@LEAVETO,@LEAVEDAYS,@REASON, @USERPC,@INSUSERID,@INSTIME,@INSIPNO, @INSLTUDE)";
                cmd.Parameters.Clear();
                cmd.Parameters.Add("@TRANSDT", SqlDbType.DateTime).Value = ob.Date;
                cmd.Parameters.Add("@TRANSMY", SqlDbType.NVarChar).Value = ob.MonthYear;
                cmd.Parameters.Add("@TRANSNO", SqlDbType.BigInt).Value = ob.TRANSNO;
                cmd.Parameters.Add("@EMPID", SqlDbType.BigInt).Value = ob.EmployeeID;

                cmd.Parameters.Add("@LEAVEID", SqlDbType.Int).Value = ob.LEAVEID;
                cmd.Parameters.Add("@LEAVEFR", SqlDbType.DateTime).Value = ob.LEAVEFR;
                cmd.Parameters.Add("@LEAVETO", SqlDbType.DateTime).Value = ob.LEAVETO;
                cmd.Parameters.Add("@LEAVEDAYS", SqlDbType.BigInt).Value = ob.LEAVEDAYS;
                cmd.Parameters.Add("@REASON", SqlDbType.NVarChar).Value = ob.REASON;

                cmd.Parameters.Add("@USERPC", SqlDbType.NVarChar).Value = ob.UserPC;
                cmd.Parameters.Add("@INSUSERID", SqlDbType.Int).Value = ob.Username;
                cmd.Parameters.Add("@INSTIME", SqlDbType.NVarChar).Value = ob.ITime;
                cmd.Parameters.Add("@INSIPNO", SqlDbType.NVarChar).Value = ob.Ipaddress;
                cmd.Parameters.Add("@INSLTUDE", SqlDbType.NVarChar).Value = ob.Ltude;
                cmd.Transaction = tran;
                cmd.ExecuteNonQuery();
                tran.Commit();
                if (con.State != ConnectionState.Closed)
                    con.Close();

            }
            catch (Exception ex)
            {
                tran.Rollback();
                s = ex.Message;
            }
            return s;
        }


        public string Update_HR_LEAVE_APP_MST(PayrollInterface ob)
        {
            string s = "";
            SqlTransaction tran = null;
            try
            {
                if (con.State != ConnectionState.Open)
                    con.Open();
                tran = con.BeginTransaction();
                cmd.CommandType = CommandType.Text;
                cmd.CommandText = @"UPDATE HR_LAPPSMST SET EMPID=@EMPID,REMARKS=@REMARKS,
                UPDUSERID=@UPDUSERID,UPDTIME=@UPDTIME,UPDIPNO=@UPDIPNO,UPDLTUDE=@UPDLTUDE 
                WHERE TRANSNO=@TRANSNO AND TRANSMY=@TRANSMY";

                cmd.Parameters.Clear();
                cmd.Parameters.Add("@TRANSMY", SqlDbType.NVarChar).Value = ob.MonthYear;
                cmd.Parameters.Add("@TRANSNO", SqlDbType.BigInt).Value = ob.TRANSNO;
                cmd.Parameters.Add("@EMPID", SqlDbType.BigInt).Value = ob.EmployeeID;

                cmd.Parameters.Add("@REMARKS", SqlDbType.NVarChar).Value = ob.Remarks;

                cmd.Parameters.Add("@UPDUSERPC", SqlDbType.NVarChar).Value = ob.UserPC;
                cmd.Parameters.Add("@UPDUSERID", SqlDbType.Int).Value = ob.UserID;
                cmd.Parameters.Add("@UPDTIME", SqlDbType.NVarChar).Value = ob.InTime;
                cmd.Parameters.Add("@UPDIPNO", SqlDbType.NVarChar).Value = ob.Ipaddress;
                cmd.Parameters.Add("@UPDLTUDE", SqlDbType.NVarChar).Value = ob.Ltude;
                cmd.Transaction = tran;
                cmd.ExecuteNonQuery();
                tran.Commit();
                if (con.State != ConnectionState.Closed)
                    con.Close();

            }
            catch (Exception ex)
            {
                tran.Rollback();
                s = ex.Message;
            }
            return s;
        }
    }
}