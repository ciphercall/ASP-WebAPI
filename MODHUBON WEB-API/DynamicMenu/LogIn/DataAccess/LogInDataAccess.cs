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
using DynamicMenu.LogIn.Interface;

namespace DynamicMenu.LogIn.DataAccess
{
    public class LogInDataAccess 
    {
         SqlConnection con;
        SqlCommand cmd;

        public LogInDataAccess()
        {
            con = new SqlConnection(dbFunctions.connection);
            cmd = new SqlCommand("", con);
        }

        public string UPDATE_ASL_PASSWORD(LogInInterface ob)
        {
            string s = "";
            SqlTransaction tran = null;
            try
            {
                if (con.State != ConnectionState.Open)
                    con.Open();
                tran = con.BeginTransaction();
                cmd.CommandType = CommandType.Text;
                cmd.CommandText = @"UPDATE ASL_USERCO SET LOGINPW=@LOGINPW, UPDUSERID=@UPDUSERID, UPDTIME=@UPDTIME, 
                UPDIPNO=@UPDIPNO, UPDLTUDE=@UPDLTUDE WHERE USERID=@USERID";
                cmd.Parameters.Clear();
                cmd.Parameters.Add("@USERID", SqlDbType.BigInt).Value = ob.UserID;
                cmd.Parameters.Add("@LOGINPW", SqlDbType.NVarChar).Value = ob.Password;

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
        public string UPDATE_ASL_PASSWORD_USING_TOKEN(LogInInterface ob)
        {
            string s = "";
            SqlTransaction tran = null;
            try
            {
                if (con.State != ConnectionState.Open)
                    con.Open();
                tran = con.BeginTransaction();
                cmd.CommandType = CommandType.Text;
                cmd.CommandText = @"UPDATE ASL_USERCO SET LOGINPW=@LOGINPW, TOKENNO='', UPDUSERID=@UPDUSERID, UPDTIME=@UPDTIME, 
                UPDIPNO=@UPDIPNO, UPDLTUDE=@UPDLTUDE WHERE USERID=@USERID AND TOKENNO=@TOKENNO";
                cmd.Parameters.Clear();
                cmd.Parameters.Add("@USERID", SqlDbType.BigInt).Value = ob.UserID;
                cmd.Parameters.Add("@TOKENNO", SqlDbType.NVarChar).Value = ob.Token;
                cmd.Parameters.Add("@LOGINPW", SqlDbType.NVarChar).Value = ob.Password;

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
    }
}