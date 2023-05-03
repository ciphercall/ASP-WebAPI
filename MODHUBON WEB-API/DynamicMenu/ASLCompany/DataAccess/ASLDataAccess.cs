using System;
using System.Data;
using System.Data.SqlClient;
using System.Configuration;
using System.Linq;
using System.Web;
using System.Web.Security;
using System.Web.UI;
using System.Web.UI.HtmlControls;
using System.Web.UI.WebControls;
using System.Web.UI.WebControls.WebParts;
using System.Xml.Linq;
using AlchemyAccounting;
using DynamicMenu.ASLCompany.Interface;

namespace DynamicMenu.ASLCompany.DataAccess
{
    public class ASLDataAccess 
    {
         SqlConnection con;
        SqlCommand cmd;

        public ASLDataAccess()
        {
            con = new SqlConnection(dbFunctions.connection);
            cmd = new SqlCommand("", con);
        }


        public string INSERT_ASL_COMPANY(ASLInterface ob)
        {
            string s = "";
            SqlTransaction tran = null;
            try
            {
                if (con.State != ConnectionState.Open)
                    con.Open();
                tran = con.BeginTransaction();
                cmd.CommandType = CommandType.Text;
                cmd.CommandText = @"INSERT INTO ASL_COMPANY(ADDRESS,COMPID,COMPNM,CONTACTNO,EMAILID,INSIPNO,INSLTUDE,INSTIME,INSUSERID,STATUS,WEBID)
 				Values 
				(@ADDRESS,@COMPID,@COMPNM,@CONTACTNO,@EMAILID,@INSIPNO,@INSLTUDE,@INSTIME,@INSUSERID,@STATUS,@WEBID)";
                cmd.Parameters.Clear();
                cmd.Parameters.Add("@ADDRESS", SqlDbType.NVarChar).Value = ob.Address;
                cmd.Parameters.Add("@COMPID", SqlDbType.BigInt).Value = ob.CompanyId;
                cmd.Parameters.Add("@COMPNM", SqlDbType.NVarChar).Value = ob.ComapanyName;
                cmd.Parameters.Add("@CONTACTNO", SqlDbType.NVarChar).Value = ob.ContactNo;
                cmd.Parameters.Add("@EMAILID", SqlDbType.NVarChar).Value = ob.EmailId;
                cmd.Parameters.Add("@STATUS", SqlDbType.NVarChar).Value = ob.Status;
                cmd.Parameters.Add("@WEBID", SqlDbType.NVarChar).Value = ob.WebId;

                cmd.Parameters.Add("@INSIPNO", SqlDbType.NVarChar).Value = ob.IpAddressInsert;
                cmd.Parameters.Add("@INSLTUDE", SqlDbType.NVarChar).Value = ob.LotiLengTudeInsert;
                cmd.Parameters.Add("@INSTIME", SqlDbType.DateTime).Value = ob.InTimeInsert;
                cmd.Parameters.Add("@INSUSERID", SqlDbType.BigInt).Value = ob.UserIdInsert;

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


        public string INSERT_ASL_USERCO(ASLInterface ob)
        {
            string s = "";
            SqlTransaction tran = null;
            try
            {
                if (con.State != ConnectionState.Open)
                    con.Open();
                tran = con.BeginTransaction();
                cmd.CommandType = CommandType.Text;
                cmd.CommandText = @"INSERT INTO ASL_USERCO(COMPID,USERID,BRANCHCD,USERNM,DEPTNM,OPTP,ADDRESS,MOBNO,EMAILID,LOGINBY,LOGINID,LOGINPW,TIMEFR,TIMETO,STATUS,USERPC,INSUSERID,INSTIME,INSIPNO,INSLTUDE)
 				Values 
				(@COMPID,@USERID,@BRANCHCD,@USERNM,@DEPTNM,@OPTP,@ADDRESS,@MOBNO,@EMAILID,@LOGINBY,@LOGINID,@LOGINPW,@TIMEFR,@TIMETO,@STATUS,@USERPC,@INSUSERID,@INSTIME,@INSIPNO,@INSLTUDE)";
                cmd.Parameters.Clear();
                cmd.Parameters.Add("@COMPID", SqlDbType.BigInt).Value = ob.CompanyId;
                cmd.Parameters.Add("@USERID", SqlDbType.BigInt).Value = ob.CompanyUserId;
                cmd.Parameters.Add("@BRANCHCD", SqlDbType.BigInt).Value = ob.BranchCode;
                cmd.Parameters.Add("@USERNM", SqlDbType.NVarChar).Value = ob.UserName;
                cmd.Parameters.Add("@DEPTNM", SqlDbType.NVarChar).Value = ob.DepartmentName;
                cmd.Parameters.Add("@OPTP", SqlDbType.NVarChar).Value = ob.OpType;
                cmd.Parameters.Add("@ADDRESS", SqlDbType.NVarChar).Value = ob.Address;
                cmd.Parameters.Add("@MOBNO", SqlDbType.NVarChar).Value = ob.MobileNo;
                cmd.Parameters.Add("@EMAILID", SqlDbType.NVarChar).Value = ob.EmailId;
                cmd.Parameters.Add("@LOGINBY", SqlDbType.NVarChar).Value = ob.LogInBy;
                cmd.Parameters.Add("@LOGINID", SqlDbType.NVarChar).Value = ob.LogInId;
                cmd.Parameters.Add("@LOGINPW", SqlDbType.NVarChar).Value = ob.Password;
                cmd.Parameters.Add("@TIMEFR", SqlDbType.NVarChar).Value = ob.TimeFrom;
                cmd.Parameters.Add("@TIMETO", SqlDbType.NVarChar).Value = ob.TimeTo;
                cmd.Parameters.Add("@STATUS", SqlDbType.NVarChar).Value = ob.Status;


                cmd.Parameters.Add("@USERPC", SqlDbType.NVarChar).Value = ob.UserPcInsert;
                cmd.Parameters.Add("@INSIPNO", SqlDbType.NVarChar).Value = ob.IpAddressInsert;
                cmd.Parameters.Add("@INSLTUDE", SqlDbType.NVarChar).Value = ob.LotiLengTudeInsert;
                cmd.Parameters.Add("@INSTIME", SqlDbType.DateTime).Value = ob.InTimeInsert;
                cmd.Parameters.Add("@INSUSERID", SqlDbType.BigInt).Value = ob.UserIdInsert;

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
        public string UPDATE_ASL_USERCO(ASLInterface ob)
        {
            string s = "";
            SqlTransaction tran = null;
            try
            {
                if (con.State != ConnectionState.Open)
                    con.Open();
                tran = con.BeginTransaction();
                cmd.CommandType = CommandType.Text;
                cmd.CommandText = @"UPDATE ASL_USERCO SET BRANCHCD=@BRANCHCD,DEPTNM=@DEPTNM,OPTP=@OPTP, ADDRESS=@ADDRESS, MOBNO=@MOBNO,
                 TIMEFR=@TIMEFR, TIMETO=@TIMETO, STATUS=@STATUS, UPDUSERID=@UPDUSERID, UPDTIME=@UPDTIME, 
                UPDIPNO=@UPDIPNO, UPDLTUDE=@UPDLTUDE WHERE USERID=@USERID";
                cmd.Parameters.Clear();

                cmd.Parameters.Add("@USERID", SqlDbType.BigInt).Value = ob.CompanyUserId;
                cmd.Parameters.Add("@BRANCHCD", SqlDbType.BigInt).Value = ob.BranchCode;
                cmd.Parameters.Add("@DEPTNM", SqlDbType.NVarChar).Value = ob.DepartmentName;
                cmd.Parameters.Add("@OPTP", SqlDbType.NVarChar).Value = ob.OpType;
                cmd.Parameters.Add("@ADDRESS", SqlDbType.NVarChar).Value = ob.Address;
                cmd.Parameters.Add("@MOBNO", SqlDbType.NVarChar).Value = ob.MobileNo;
                cmd.Parameters.Add("@TIMEFR", SqlDbType.NVarChar).Value = ob.TimeFrom;
                cmd.Parameters.Add("@TIMETO", SqlDbType.NVarChar).Value = ob.TimeTo;
                cmd.Parameters.Add("@STATUS", SqlDbType.NVarChar).Value = ob.Status;

                cmd.Parameters.Add("@UPDIPNO", SqlDbType.NVarChar).Value = ob.IpAddressInsert;
                cmd.Parameters.Add("@UPDLTUDE", SqlDbType.NVarChar).Value = ob.LotiLengTudeInsert;
                cmd.Parameters.Add("@UPDTIME", SqlDbType.DateTime).Value = ob.InTimeInsert;
                cmd.Parameters.Add("@UPDUSERID", SqlDbType.BigInt).Value = ob.UserIdInsert;

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


        public string INSERT_ASL_MENUMST(ASLInterface ob)
        {
            string s = "";
            SqlTransaction tran = null;
            try
            {
                if (con.State != ConnectionState.Open)
                    con.Open();
                tran = con.BeginTransaction();
                cmd.CommandType = CommandType.Text;
                cmd.CommandText = @"INSERT INTO ASL_MENUMST(MODULEID,MODULENM,USERPC,INSUSERID,INSTIME,INSIPNO)
 				Values 
				(@MODULEID,@MODULENM,@USERPC,@INSUSERID,@INSTIME,@INSIPNO)";
                cmd.Parameters.Clear();
                cmd.Parameters.Add("@MODULEID", SqlDbType.NVarChar).Value = ob.ModuleId;
                cmd.Parameters.Add("@MODULENM", SqlDbType.NVarChar).Value = ob.ModuleName;
                cmd.Parameters.Add("@USERPC", SqlDbType.NVarChar).Value = ob.UserPcInsert;
                cmd.Parameters.Add("@INSUSERID", SqlDbType.BigInt).Value = ob.UserIdInsert;
                cmd.Parameters.Add("@INSTIME", SqlDbType.DateTime).Value = ob.InTimeInsert;
                cmd.Parameters.Add("@INSIPNO", SqlDbType.NVarChar).Value = ob.IpAddressInsert;

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

        public string INSERT_ASL_MENU(ASLInterface ob)
        {
            string s = "";
            SqlTransaction tran = null;
            try
            {
                if (con.State != ConnectionState.Open)
                    con.Open();
                tran = con.BeginTransaction();
                cmd.CommandType = CommandType.Text;
                cmd.CommandText = @"INSERT INTO ASL_MENU(MODULEID,MENUTP,MENUID,MENUNM,FLINK,MENUSL,USERPC,INSUSERID,INSTIME,INSIPNO)
 				Values 
				(@MODULEID,@MENUTP,@MENUID,@MENUNM,@FLINK,@MENUSL,@USERPC,@INSUSERID,@INSTIME,@INSIPNO)";
                cmd.Parameters.Clear();
                cmd.Parameters.Add("@MODULEID", SqlDbType.NVarChar).Value = ob.ModuleId;
                cmd.Parameters.Add("@MENUTP", SqlDbType.NVarChar).Value = ob.MenuType;
                cmd.Parameters.Add("@MENUID", SqlDbType.NVarChar).Value = ob.MenuId;
                cmd.Parameters.Add("@MENUNM", SqlDbType.NVarChar).Value = ob.MenuName;
                cmd.Parameters.Add("@FLINK", SqlDbType.NVarChar).Value = ob.MenuLink;
                cmd.Parameters.Add("@MENUSL", SqlDbType.Int).Value = ob.MenuSerial;
                cmd.Parameters.Add("@USERPC", SqlDbType.NVarChar).Value = ob.UserPcInsert;
                cmd.Parameters.Add("@INSUSERID", SqlDbType.BigInt).Value = ob.UserIdInsert;
                cmd.Parameters.Add("@INSTIME", SqlDbType.DateTime).Value = ob.InTimeInsert;
                cmd.Parameters.Add("@INSIPNO", SqlDbType.NVarChar).Value = ob.IpAddressInsert;

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

        public string DELETE_ASL_MENUMST(ASLInterface ob)
        {
            string s = "";
            SqlTransaction tran = null;
            try
            {
                if (con.State != ConnectionState.Open)
                    con.Open();
                tran = con.BeginTransaction();
                cmd.CommandType = CommandType.Text;

                cmd.CommandText = @"DELETE FROM ASL_MENUMST WHERE MODULEID=@MODULEID ";
                cmd.Parameters.Clear();
                cmd.Parameters.Add("@MODULEID", SqlDbType.NVarChar).Value = ob.ModuleId;

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
        public string DELETE_ASL_MENU(ASLInterface ob)
        {
            string s = "";
            SqlTransaction tran = null;
            try
            {
                if (con.State != ConnectionState.Open)
                    con.Open();
                tran = con.BeginTransaction();
                cmd.CommandType = CommandType.Text;

                cmd.CommandText = @"DELETE FROM ASL_MENU WHERE MENUID=@MENUID AND MODULEID=@MODULEID ";
                cmd.Parameters.Clear();
                cmd.Parameters.Add("@MENUID", SqlDbType.NVarChar).Value = ob.MenuId;
                cmd.Parameters.Add("@MODULEID", SqlDbType.NVarChar).Value = ob.ModuleId;

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


        public string UPDATE_ASL_MENU(ASLInterface ob)
        {
            string s = "";
            SqlTransaction tran = null;
            try
            {
                if (con.State != ConnectionState.Open)
                    con.Open();
                tran = con.BeginTransaction();
                cmd.CommandType = CommandType.Text;
                cmd.CommandText = @"UPDATE ASL_MENU SET MENUTP=@MENUTP,MENUNM=@MENUNM,FLINK=@FLINK,MENUSL=@MENUSL, UPDUSERID=@UPDUSERID,UPDTIME=@UPDTIME,UPDIPNO=@UPDIPNO 
                WHERE MENUID=@MENUID AND MODULEID=@MODULEID ";
                cmd.Parameters.Clear();

                cmd.Parameters.Add("@MENUID", SqlDbType.NVarChar).Value = ob.MenuId;
                cmd.Parameters.Add("@MODULEID", SqlDbType.NVarChar).Value = ob.ModuleId;

                cmd.Parameters.Add("@MENUTP", SqlDbType.NVarChar).Value = ob.MenuType;
                cmd.Parameters.Add("@MENUNM", SqlDbType.NVarChar).Value = ob.MenuName;
                cmd.Parameters.Add("@FLINK", SqlDbType.NVarChar).Value = ob.MenuLink;
                cmd.Parameters.Add("@MENUSL", SqlDbType.Int).Value = ob.MenuSerial;

                cmd.Parameters.Add("@UPDUSERID", SqlDbType.BigInt).Value = ob.UserIdUpdate;
                cmd.Parameters.Add("@UPDTIME", SqlDbType.DateTime).Value = ob.InTimeUpdate;
                cmd.Parameters.Add("@UPDIPNO", SqlDbType.NVarChar).Value = ob.IpAddressUpdate;

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


        public string INSERT_ASL_ROLE(ASLInterface ob)
        {
            string s = "";
            SqlTransaction tran = null;
            try
            {
                if (con.State != ConnectionState.Open)
                    con.Open();
                tran = con.BeginTransaction();
                cmd.CommandType = CommandType.Text;
                cmd.CommandText = @"INSERT INTO ASL_ROLE(COMPID,USERID,MODULEID,MENUTP,MENUID,STATUS,INSERTR,UPDATER,DELETER,USERPC,INSUSERID,INSTIME,INSIPNO,INSLTUDE)
 				Values 
				(@COMPID,@USERID,@MODULEID,@MENUTP,@MENUID,@STATUS,@INSERTR,@UPDATER,@DELETER,@USERPC,@INSUSERID,@INSTIME,@INSIPNO,@INSLTUDE)";
                cmd.Parameters.Clear();
                cmd.Parameters.Add("@COMPID", SqlDbType.BigInt).Value = ob.CompanyId;
                cmd.Parameters.Add("@USERID", SqlDbType.BigInt).Value = ob.CompanyUserId;
                cmd.Parameters.Add("@MODULEID", SqlDbType.NVarChar).Value = ob.ModuleId;
                cmd.Parameters.Add("@MENUTP", SqlDbType.NVarChar).Value = ob.MenuType;
                cmd.Parameters.Add("@MENUID", SqlDbType.NVarChar).Value = ob.MenuId;
                cmd.Parameters.Add("@STATUS", SqlDbType.NVarChar).Value = ob.Status;
                cmd.Parameters.Add("@INSERTR", SqlDbType.NVarChar).Value = ob.InsertRole;
                cmd.Parameters.Add("@UPDATER", SqlDbType.NVarChar).Value = ob.UpdateRole;
                cmd.Parameters.Add("@DELETER", SqlDbType.NVarChar).Value = ob.DeleteRole;

                cmd.Parameters.Add("@USERPC", SqlDbType.NVarChar).Value = ob.UserPcInsert;
                cmd.Parameters.Add("@INSIPNO", SqlDbType.NVarChar).Value = ob.IpAddressInsert;
                cmd.Parameters.Add("@INSLTUDE", SqlDbType.NVarChar).Value = ob.LotiLengTudeInsert;
                cmd.Parameters.Add("@INSTIME", SqlDbType.DateTime).Value = ob.InTimeInsert;
                cmd.Parameters.Add("@INSUSERID", SqlDbType.BigInt).Value = ob.UserIdInsert;

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

        public string UPDATE_ASL_ROLE(ASLInterface ob)
        {
            string s = "";
            SqlTransaction tran = null;
            try
            {
                if (con.State != ConnectionState.Open)
                    con.Open();
                tran = con.BeginTransaction();
                cmd.CommandType = CommandType.Text;
                cmd.CommandText = @"UPDATE ASL_ROLE SET STATUS=@STATUS,INSERTR=@INSERTR,UPDATER=@UPDATER, 
                DELETER=@DELETER, UPDUSERID=@UPDUSERID,UPDTIME=@UPDTIME,UPDIPNO=@UPDIPNO,UPDLTUDE=@UPDLTUDE 
                WHERE COMPID=@COMPID AND USERID=@USERID AND MODULEID=@MODULEID AND MENUID=@MENUID AND MENUTP=@MENUTP";
                cmd.Parameters.Clear();
                cmd.Parameters.Add("@STATUS", SqlDbType.NVarChar).Value = ob.Status;
                cmd.Parameters.Add("@INSERTR", SqlDbType.NVarChar).Value = ob.InsertRole;
                cmd.Parameters.Add("@UPDATER", SqlDbType.NVarChar).Value = ob.UpdateRole;
                cmd.Parameters.Add("@DELETER", SqlDbType.NVarChar).Value = ob.DeleteRole;

                cmd.Parameters.Add("@COMPID", SqlDbType.Int).Value = ob.CompanyId;
                cmd.Parameters.Add("@USERID", SqlDbType.Int).Value = ob.CompanyUserId;
                cmd.Parameters.Add("@MODULEID", SqlDbType.NVarChar).Value = ob.ModuleId;
                cmd.Parameters.Add("@MENUID", SqlDbType.NVarChar).Value = ob.MenuId;
                cmd.Parameters.Add("@MENUTP", SqlDbType.NVarChar).Value = ob.MenuType;
                
                cmd.Parameters.Add("@UPDUSERID", SqlDbType.BigInt).Value = ob.UserIdUpdate;
                cmd.Parameters.Add("@UPDTIME", SqlDbType.DateTime).Value = ob.InTimeUpdate;
                cmd.Parameters.Add("@UPDIPNO", SqlDbType.NVarChar).Value = ob.IpAddressUpdate;
                cmd.Parameters.Add("@UPDLTUDE", SqlDbType.NVarChar).Value = ob.LotiLengTudeUpdate;

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

        public string DELETE_ASL_ROLE(ASLInterface ob)
        {
            string s = "";
            SqlTransaction tran = null;
            try
            {
                if (con.State != ConnectionState.Open)
                    con.Open();
                tran = con.BeginTransaction();
                cmd.CommandType = CommandType.Text;

                cmd.CommandText = @"DELETE FROM ASL_ROLE WHERE COMPID=@COMPID AND MODULEID=@MODULEID  
                        AND MENUTP=@MENUTP AND MENUID=@MENUID AND USERID!=@USERID";
                cmd.Parameters.Clear();
                cmd.Parameters.Add("@COMPID", SqlDbType.Int).Value = ob.CompanyId;
                cmd.Parameters.Add("@USERID", SqlDbType.Int).Value = ob.CompanyUserId;
                cmd.Parameters.Add("@MODULEID", SqlDbType.NVarChar).Value = ob.ModuleId;
                cmd.Parameters.Add("@MENUID", SqlDbType.NVarChar).Value = ob.MenuId;
                cmd.Parameters.Add("@MENUTP", SqlDbType.NVarChar).Value = ob.MenuType;

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

        public string UPDATE_ASL_ROLE_HLOE_COMPANY(ASLInterface ob)
        {
            string s = "";
            SqlTransaction tran = null;
            try
            {
                if (con.State != ConnectionState.Open)
                    con.Open();
                tran = con.BeginTransaction();
                cmd.CommandType = CommandType.Text;
                cmd.CommandText = @"UPDATE ASL_ROLE SET STATUS=@STATUS,INSERTR=@INSERTR,UPDATER=@UPDATER, 
                DELETER=@DELETER, UPDUSERID=@UPDUSERID,UPDTIME=@UPDTIME,UPDIPNO=@UPDIPNO,UPDLTUDE=@UPDLTUDE 
                WHERE COMPID=@COMPID AND MODULEID=@MODULEID AND MENUID=@MENUID AND MENUTP=@MENUTP";
                cmd.Parameters.Clear();
                cmd.Parameters.Add("@STATUS", SqlDbType.NVarChar).Value = ob.Status;
                cmd.Parameters.Add("@INSERTR", SqlDbType.NVarChar).Value = ob.InsertRole;
                cmd.Parameters.Add("@UPDATER", SqlDbType.NVarChar).Value = ob.UpdateRole;
                cmd.Parameters.Add("@DELETER", SqlDbType.NVarChar).Value = ob.DeleteRole;

                cmd.Parameters.Add("@COMPID", SqlDbType.Int).Value = ob.CompanyId;
                cmd.Parameters.Add("@MODULEID", SqlDbType.NVarChar).Value = ob.ModuleId;
                cmd.Parameters.Add("@MENUID", SqlDbType.NVarChar).Value = ob.MenuId;
                cmd.Parameters.Add("@MENUTP", SqlDbType.NVarChar).Value = ob.MenuType;

                cmd.Parameters.Add("@UPDUSERID", SqlDbType.BigInt).Value = ob.UserIdUpdate;
                cmd.Parameters.Add("@UPDTIME", SqlDbType.DateTime).Value = ob.InTimeUpdate;
                cmd.Parameters.Add("@UPDIPNO", SqlDbType.NVarChar).Value = ob.IpAddressUpdate;
                cmd.Parameters.Add("@UPDLTUDE", SqlDbType.NVarChar).Value = ob.LotiLengTudeUpdate;

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

        public string INSERT_ASL_LOG(ASLInterface ob)
        {
            string s = "";
            SqlTransaction tran = null;
            try
            {
                if (con.State != ConnectionState.Open)
                    con.Open();
                tran = con.BeginTransaction();
                cmd.CommandType = CommandType.Text;
                cmd.CommandText = @"INSERT INTO ASL_LOG(COMPID,USERID,LOGTYPE,LOGSLNO,LOGDT,LOGTIME,LOGIPNO,LOGLTUDE,TABLEID,LOGDATA,USERPC)
 				Values 
				(@COMPID,@USERID,@LOGTYPE,@LOGSLNO,@LOGDT,@LOGTIME,@LOGIPNO,@LOGLTUDE,@TABLEID,@LOGDATA,@USERPC)";
                cmd.Parameters.Clear();
                cmd.Parameters.Add("@COMPID", SqlDbType.BigInt).Value = ob.CompanyId;
                cmd.Parameters.Add("@USERID", SqlDbType.BigInt).Value = ob.CompanyUserId;
                cmd.Parameters.Add("@LOGTYPE", SqlDbType.NVarChar).Value = ob.LogType;
                cmd.Parameters.Add("@LOGSLNO", SqlDbType.BigInt).Value = ob.LogSlNo;
                cmd.Parameters.Add("@TABLEID", SqlDbType.NVarChar).Value = ob.TableId;
                cmd.Parameters.Add("@LOGDATA", SqlDbType.NVarChar).Value = ob.LogData;

                cmd.Parameters.Add("@LOGDT", SqlDbType.DateTime).Value = ob.InTimeInsert;
                cmd.Parameters.Add("@LOGTIME", SqlDbType.DateTime).Value = ob.InTimeInsert;
                cmd.Parameters.Add("@LOGIPNO", SqlDbType.NVarChar).Value = ob.IpAddressInsert;
                cmd.Parameters.Add("@LOGLTUDE", SqlDbType.NVarChar).Value = ob.LotiLengTudeInsert;
                cmd.Parameters.Add("@USERPC", SqlDbType.NVarChar).Value = ob.UserPcInsert;

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