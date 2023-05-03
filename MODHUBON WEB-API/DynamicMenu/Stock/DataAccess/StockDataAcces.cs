using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Text;
using System.Data;
using System.Data.SqlClient;
using System.Configuration;
using System.Web.Security;
using System.Web.UI;
using System.Web.UI.HtmlControls;
using System.Web.UI.WebControls;
using System.Web.UI.WebControls.WebParts;
using System.Xml.Linq;
using AlchemyAccounting;
using DynamicMenu.Stock.Interface;

namespace DynamicMenu.Stock.DataAccess
{
    public class StockDataAcces
    {
        SqlConnection con;
        SqlCommand cmd;

        public StockDataAcces()
        {
            con = new SqlConnection(dbFunctions.connection);
            cmd = new SqlCommand("", con);
        }

        public string insertPS(StockInterface ob)
        {
            string s = "";
            SqlTransaction tran = null;
            try
            {
                if (con.State != ConnectionState.Open)
                    con.Open();
                tran = con.BeginTransaction();
                cmd.CommandType = CommandType.Text;
                cmd.CommandText = "INSERT INTO STK_PS( PSTP, PSID, CITY, ADDRESS, CONTACTNO, EMAIL, WEBID, CPNM, CPNO, REMARKS, STATUS, USERPC, USERID, IPADDRESS,PS_ID) " +
                                  "VALUES (@PSTP,@PSID,@CITY,@ADDRESS,@CONTACTNO,@EMAIL,@WEBID,@CPNM,@CPNO,@REMARKS,@STATUS,@USERPC,@USERID,@IPADDRESS,@PS_ID)";
                cmd.Parameters.Clear();
                cmd.Parameters.Add("@PSTP", SqlDbType.NVarChar).Value = ob.Pstp;
                cmd.Parameters.Add("@PSID", SqlDbType.NVarChar).Value = ob.Pscd;
                cmd.Parameters.Add("@CITY", SqlDbType.NVarChar).Value = ob.City;
                cmd.Parameters.Add("@ADDRESS", SqlDbType.NVarChar).Value = ob.Address;
                cmd.Parameters.Add("@CONTACTNO", SqlDbType.NVarChar).Value = ob.Contactno;
                cmd.Parameters.Add("@EMAIL", SqlDbType.NVarChar).Value = ob.Email;
                cmd.Parameters.Add("@WEBID", SqlDbType.NVarChar).Value = ob.Webid;
                cmd.Parameters.Add("@CPNM", SqlDbType.NVarChar).Value = ob.Cpnm;
                cmd.Parameters.Add("@CPNO", SqlDbType.NVarChar).Value = ob.Cpno;
                cmd.Parameters.Add("@REMARKS", SqlDbType.NVarChar).Value = ob.Remarks;
                cmd.Parameters.Add("@STATUS", SqlDbType.NVarChar).Value = ob.Status;
                cmd.Parameters.Add("@USERPC", SqlDbType.NVarChar).Value = "";
                cmd.Parameters.Add("@USERID", SqlDbType.NVarChar).Value = ob.Username;
                cmd.Parameters.Add("@IPADDRESS", SqlDbType.NVarChar).Value = "";
                cmd.Parameters.Add("@PS_ID", SqlDbType.NVarChar).Value = ob.PsId;

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
        public string InsertParchaseSTK_TRANS(StockInterface ob)
        {
            string s = "";
            SqlTransaction tran = null;
            try
            {
                if (con.State != ConnectionState.Open)
                    con.Open();
                tran = con.BeginTransaction();
                cmd.CommandType = CommandType.Text;
                cmd.CommandText = @"insert into STK_TRANS ( TRANSTP, TRANSDT, TRANSMY, TRANSNO, INVREFNO, STOREFR, STORETO, PSID, LCTP, LCID,LCDATE,
                                REMARKS, TRANSSL, CATID, ITEMID, UNITTP, CPQTY, CQTY, PQTY, QTY, RATE, AMOUNT, USERPC, INTIME, USERID, IPADD ) 
                  values(@TRANSTP, @TRANSDT, @TRANSMY, @TRANSNO, @INVREFNO, @STOREFR, @STORETO, @PSID, @LCTP, @LCID,@LCDATE,
                                @REMARKS, @TRANSSL, @CATID, @ITEMID, @UNITTP, @CPQTY, @CQTY, @PQTY, @QTY, @RATE, @AMOUNT, @USERPC, @INTIME, @USERID, @IPADDRESS)";
                //comm = new SqlCommand(query, conn);
                cmd.Parameters.Clear();
                cmd.Parameters.Add("@TRANSTP", SqlDbType.NVarChar).Value = ob.TransTP;
                cmd.Parameters.Add("@TRANSDT", SqlDbType.SmallDateTime).Value = ob.TrDt;
                cmd.Parameters.Add("@TRANSMY", SqlDbType.NVarChar).Value = ob.MonthYear;
                cmd.Parameters.Add("@TRANSNO", SqlDbType.BigInt).Value = ob.TransNo;
                cmd.Parameters.Add("@INVREFNO", SqlDbType.NVarChar).Value = ob.InvRefNo;
                cmd.Parameters.Add("@STOREFR", SqlDbType.NVarChar).Value = ob.StoreFR;
                cmd.Parameters.Add("@STORETO", SqlDbType.NVarChar).Value = ob.To;
                cmd.Parameters.Add("@PSID", SqlDbType.NVarChar).Value = ob.PsId;
                cmd.Parameters.Add("@LCTP", SqlDbType.NVarChar).Value = ob.LcTp;
                cmd.Parameters.Add("@LCID", SqlDbType.NVarChar).Value = ob.LcId;
                cmd.Parameters.Add("@LCDATE", SqlDbType.Date).Value = ob.LcDt;
                cmd.Parameters.Add("@REMARKS", SqlDbType.NVarChar).Value = ob.Remarks;
                cmd.Parameters.Add("@TRANSSL", SqlDbType.NVarChar).Value = ob.TransSL;
                cmd.Parameters.Add("@CATID", SqlDbType.NVarChar).Value = ob.CatID;
                cmd.Parameters.Add("@ITEMID", SqlDbType.NVarChar).Value = ob.ItemID;
                cmd.Parameters.Add("@UNITTP", SqlDbType.NVarChar).Value = ob.UnitTP;
                cmd.Parameters.Add("@CPQTY", SqlDbType.Decimal).Value = ob.CpQTY;
                cmd.Parameters.Add("@CQTY", SqlDbType.Decimal).Value = ob.CQTY;
                cmd.Parameters.Add("@PQTY", SqlDbType.Decimal).Value = ob.PQTY;
                cmd.Parameters.Add("@QTY", SqlDbType.Decimal).Value = ob.Qty;
                cmd.Parameters.Add("@RATE", SqlDbType.Decimal).Value = ob.Rate;
                cmd.Parameters.Add("@AMOUNT", SqlDbType.Decimal).Value = ob.Amount;
                cmd.Parameters.Add("@USERPC", SqlDbType.NVarChar).Value = ob.UserPC;
                cmd.Parameters.Add("@INTIME", SqlDbType.SmallDateTime).Value = ob.ITime;
                cmd.Parameters.Add("@USERID", SqlDbType.NVarChar).Value = ob.UserID;
                cmd.Parameters.Add("@IPADDRESS", SqlDbType.NVarChar).Value = ob.Ipaddress;
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
        public string InsertParchaseSTK_TRANSMST(StockInterface ob)
        {
            string s = "";
            SqlTransaction tran = null;
            try
            {
                if (con.State != ConnectionState.Open)
                    con.Open();
                tran = con.BeginTransaction();
                cmd.CommandType = CommandType.Text;
                cmd.CommandText = @"insert into STK_TRANSMST ( TRANSTP, TRANSDT, TRANSMY, TRANSNO, INVREFNO, STOREFR, STORETO, PSID, LCTP, LCID, LCDATE, REMARKS, USERPC, INTIME, USERID, IPADD) 
                  values(@TRANSTP, @TRANSDT, @TRANSMY, @TRANSNO, @INVREFNO, @STOREFR, @STORETO, @PSID, @LCTP, @LCID,@LCDATE, @REMARKS,@USERPC, @INTIME, @USERID, @IPADDRESS)";
                //comm = new SqlCommand(query, conn);
                cmd.Parameters.Clear();
                cmd.Parameters.Add("@TRANSTP", SqlDbType.NVarChar).Value = ob.TransTP;
                cmd.Parameters.Add("@TRANSDT", SqlDbType.SmallDateTime).Value = ob.TrDt;
                cmd.Parameters.Add("@TRANSMY", SqlDbType.NVarChar).Value = ob.MonthYear;
                cmd.Parameters.Add("@TRANSNO", SqlDbType.BigInt).Value = ob.TransNo;
                cmd.Parameters.Add("@INVREFNO", SqlDbType.NVarChar).Value = ob.InvRefNo;
                cmd.Parameters.Add("@STOREFR", SqlDbType.NVarChar).Value = ob.StoreFR;
                cmd.Parameters.Add("@STORETO", SqlDbType.NVarChar).Value = ob.To;
                cmd.Parameters.Add("@PSID", SqlDbType.NVarChar).Value = ob.PsId;
                cmd.Parameters.Add("@LCTP", SqlDbType.NVarChar).Value = ob.LcTp;
                cmd.Parameters.Add("@LCID", SqlDbType.NVarChar).Value = ob.LcId;
                cmd.Parameters.Add("@LCDATE", SqlDbType.Date).Value = ob.LcDt;
                cmd.Parameters.Add("@REMARKS", SqlDbType.NVarChar).Value = ob.Remarks;
                cmd.Parameters.Add("@USERPC", SqlDbType.NVarChar).Value = ob.UserPC;
                cmd.Parameters.Add("@INTIME", SqlDbType.SmallDateTime).Value = ob.ITime;
                cmd.Parameters.Add("@USERID", SqlDbType.NVarChar).Value = ob.UserID;
                cmd.Parameters.Add("@IPADDRESS", SqlDbType.NVarChar).Value = ob.Ipaddress;
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

        public string UpdatePurchaseSTK_TRANSMST(StockInterface ob)
        {
            string P = "";
            SqlTransaction tran = null;
            try
            {
                string userName = HttpContext.Current.Session["UserName"].ToString();
                if (con.State != ConnectionState.Open)
                    con.Open();
                tran = con.BeginTransaction();
                cmd.CommandType = CommandType.Text;

                cmd.CommandText = "update STK_TRANSMST set INVREFNO =@INVREFNO , STORETO=@STORETO , PSID=@PSID , LCTP =@LCTP , LCID=@LCID , LCDATE =@LCDATE , REMARKS =@REMARKS ," +
                              " UPDUSERPC=@UPDUSERPC , UPDATEUSERID =@UPDATEUSERID , UPDTIME=@UPDTIME , UPDIPADDRESS=@UPDIPADDRESS where TRANSTP = 'BUY' and TRANSMY=@TRANSMY and TRANSDT=@TRANSDT and TRANSNO =@TRANSNO ";
                cmd.Parameters.Clear();
                cmd.Parameters.Add("@INVREFNO", SqlDbType.NVarChar).Value = ob.InvRefNo;
                cmd.Parameters.Add("@STORETO", SqlDbType.NVarChar).Value = ob.To;
                cmd.Parameters.Add("@PSID", SqlDbType.NVarChar).Value = ob.PsId;
                cmd.Parameters.Add("@LCTP", SqlDbType.NVarChar).Value = ob.LcTp;
                cmd.Parameters.Add("@LCID", SqlDbType.NVarChar).Value = ob.LcId;
                cmd.Parameters.Add("@LCDATE", SqlDbType.Date).Value = ob.LcDt;
                cmd.Parameters.Add("@REMARKS", SqlDbType.NVarChar).Value = ob.Remarks;
                cmd.Parameters.Add("@UPDUSERPC", SqlDbType.NVarChar).Value = ob.UPDUserPC;
                cmd.Parameters.Add("@UPDATEUSERID", SqlDbType.NVarChar).Value = ob.UserID;
                cmd.Parameters.Add("@UPDTIME", SqlDbType.DateTime).Value = ob.UpdTime;
                cmd.Parameters.Add("@UPDIPADDRESS", SqlDbType.NVarChar).Value = ob.UPDIpaddress;
                cmd.Parameters.Add("@TRANSMY", SqlDbType.NVarChar).Value = ob.MonthYear;
                cmd.Parameters.Add("@TRANSDT", SqlDbType.DateTime).Value = ob.TrDt;
                cmd.Parameters.Add("@TRANSNO", SqlDbType.BigInt).Value = ob.TransNo;

                cmd.Transaction = tran;
                cmd.ExecuteNonQuery();
                tran.Commit();
                if (con.State != ConnectionState.Closed)
                    con.Close();
            }
            catch (Exception ex)
            {
                tran.Rollback();
                P = ex.Message;
            }
            return P;

        }




        public string insertPrivilegeCard(StockInterface ob)
        {
            string s = "";
            SqlTransaction tran = null;
            try
            {
                if (con.State != ConnectionState.Open)
                    con.Open();
                tran = con.BeginTransaction();
                cmd.CommandType = CommandType.Text;
                cmd.CommandText = @"INSERT INTO PC_CARD( PCARDHTP, PCARDHID, PCARDHNM, FATHERNM, MOTHERNM, SPOUSENM, ADDRESS_PRE, ADDRESS_PER, 
                                    TELNO, MOBNO, DOB, EMAILID, VOTERID, COMPNM, ADDRESSCO, DESIGNATION, TELNOCO, MOBNOCO, ANNUALINC, PORTALINC, REMARKS, REFERHID, 
                                    SUBMITDT, ISSUEDT, ISSUEFR, STATUS, USERPC, USERID, INTIME, IPADDRESS)
                             VALUES (@PCARDHTP,@PCARDHID,@PCARDHNM,@FATHERNM,@MOTHERNM,@SPOUSENM,@ADDRESS_PRE,@ADDRESS_PER,@TELNO,@MOBNO,@DOB,@EMAILID,
                             @VOTERID,@COMPNM,@ADDRESSCO,@DESIGNATION,@TELNOCO,@MOBNOCO,@ANNUALINC,@PORTALINC,@REMARKS,@REFERHID,@SUBMITDT,@ISSUEDT,
                             @ISSUEFR,@STATUS,@USERPC,@USERID,@INTIME,@IPADDRESS)";

                cmd.Parameters.Clear();
                cmd.Parameters.Add("@PCARDHTP", SqlDbType.NVarChar).Value = ob.CardHTP;
                cmd.Parameters.Add("@PCARDHID", SqlDbType.NVarChar).Value = ob.CardHID;
                cmd.Parameters.Add("@PCARDHNM", SqlDbType.NVarChar).Value = ob.CardHNM;
                cmd.Parameters.Add("@FATHERNM", SqlDbType.NVarChar).Value = ob.FatherNM;
                cmd.Parameters.Add("@MOTHERNM", SqlDbType.NVarChar).Value = ob.MotherNM;
                cmd.Parameters.Add("@SPOUSENM", SqlDbType.NVarChar).Value = ob.SpouseNM;
                cmd.Parameters.Add("@ADDRESS_PRE", SqlDbType.NVarChar).Value = ob.PrsntAdd;
                cmd.Parameters.Add("@ADDRESS_PER", SqlDbType.NVarChar).Value = ob.PermntAdd;
                cmd.Parameters.Add("@TELNO", SqlDbType.NVarChar).Value = ob.TelNo;
                cmd.Parameters.Add("@MOBNO", SqlDbType.NVarChar).Value = ob.MobNo;
                cmd.Parameters.Add("@DOB", SqlDbType.DateTime).Value = ob.DOfBirth;
                cmd.Parameters.Add("@EMAILID", SqlDbType.NVarChar).Value = ob.EmailID;
                cmd.Parameters.Add("@VOTERID", SqlDbType.NVarChar).Value = ob.VotrID;
                cmd.Parameters.Add("@COMPNM", SqlDbType.NVarChar).Value = ob.OffNM;
                cmd.Parameters.Add("@ADDRESSCO", SqlDbType.NVarChar).Value = ob.OffAdd;
                cmd.Parameters.Add("@DESIGNATION", SqlDbType.NVarChar).Value = ob.Designation;
                cmd.Parameters.Add("@TELNOCO", SqlDbType.NVarChar).Value = ob.OffTelNo;
                cmd.Parameters.Add("@MOBNOCO", SqlDbType.NVarChar).Value = ob.OffMblNo;
                cmd.Parameters.Add("@ANNUALINC", SqlDbType.Decimal).Value = ob.AnnualIncm;
                cmd.Parameters.Add("@PORTALINC", SqlDbType.NVarChar).Value = ob.IncmPortl;
                cmd.Parameters.Add("@REMARKS", SqlDbType.NVarChar).Value = ob.Comments;
                cmd.Parameters.Add("@REFERHID", SqlDbType.NVarChar).Value = ob.RfrCrdHID;
                cmd.Parameters.Add("@SUBMITDT", SqlDbType.DateTime).Value = ob.SubmtDT;
                cmd.Parameters.Add("@ISSUEDT", SqlDbType.DateTime).Value = ob.IssuDT;
                cmd.Parameters.Add("@ISSUEFR", SqlDbType.NVarChar).Value = ob.IssuFrm;
                cmd.Parameters.Add("@STATUS", SqlDbType.NVarChar).Value = ob.Status;
                cmd.Parameters.Add("@USERPC", SqlDbType.NVarChar).Value = ob.UserPC;
                //cmd.Parameters.Add("@UPDUSERPC", SqlDbType.NVarChar).Value = ob.UPDUserPC;
                cmd.Parameters.Add("@USERID", SqlDbType.NVarChar).Value = ob.UserID;
                //cmd.Parameters.Add("@UPDUSERID", SqlDbType.NVarChar).Value = ob.UPDUserID;
                cmd.Parameters.Add("@INTIME", SqlDbType.DateTime).Value = ob.ITime;
                //cmd.Parameters.Add("@UPDTIME", SqlDbType.DateTime).Value = ob.UpdTime;
                cmd.Parameters.Add("@IPADDRESS", SqlDbType.NVarChar).Value = ob.Ipaddress;
                //cmd.Parameters.Add("@UPDIPADD", SqlDbType.NVarChar).Value = ob.UPDIpaddress;

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




        public string UPDTransfrSTK_TRANSMST(StockInterface ob)
        {
            string P = "";
            SqlTransaction tran = null;
            try
            {
                string userName = HttpContext.Current.Session["UserName"].ToString();
                if (con.State != ConnectionState.Open)
                    con.Open();
                tran = con.BeginTransaction();
                cmd.CommandType = CommandType.Text;
                cmd.CommandText = "update STK_TRANSMST set TRRTSNO=@TRRTSNO , UNITTO=@UNITTO UNITFR=@UNITFR, REMARKS=@REMARKS," +
                    " TRMODE=@TRMODE, ITEMIDFG=@ITEMIDFG, PRODQTY=@PRODQTY," +
                    " UPDUSERPC=@UPDUSERPC , UPDATEUSERID =@UPDATEUSERID , UPDTIME=@UPDTIME , UPDIPADDRESS=@UPDIPADDRESS where TRANSTP = 'IISS' and TRANSDT=@TRANSDT and TRANSNO =@TRANSNO ";
                cmd.Parameters.Clear();
                cmd.Parameters.Add("@TRRTSNO", SqlDbType.NVarChar).Value = ob.InvRefNo;
                cmd.Parameters.Add("@UNITFR", SqlDbType.NVarChar).Value = ob.StoreFR;
                cmd.Parameters.Add("@UNITTO", SqlDbType.NVarChar).Value = ob.To;
                cmd.Parameters.Add("@REMARKS", SqlDbType.NVarChar).Value = ob.Remarks;

                cmd.Parameters.Add("@TRMODE", SqlDbType.NVarChar).Value = ob.TranMode;
                cmd.Parameters.Add("@ITEMIDFG", SqlDbType.NVarChar).Value = ob.ItemFG;
                cmd.Parameters.Add("@PRODQTY", SqlDbType.Decimal).Value = ob.ProductionQty;

                cmd.Parameters.Add("@UPDUSERPC", SqlDbType.NVarChar).Value = ob.UPDUserPC;
                cmd.Parameters.Add("@UPDATEUSERID", SqlDbType.NVarChar).Value = ob.UpdUserID;
                cmd.Parameters.Add("@UPDTIME", SqlDbType.SmallDateTime).Value = ob.UpdTime;
                cmd.Parameters.Add("@UPDIPADDRESS", SqlDbType.NVarChar).Value = ob.UPDIpaddress;
                //cmd.Parameters.Add("@TRANSMY", SqlDbType.NVarChar).Value = ob.MonthYear;
                cmd.Parameters.Add("@TRANSDT", SqlDbType.SmallDateTime).Value = ob.TrDt;
                cmd.Parameters.Add("@TRANSNO", SqlDbType.NVarChar).Value = ob.TransNo2;

                cmd.Transaction = tran;
                cmd.ExecuteNonQuery();
                tran.Commit();
                if (con.State != ConnectionState.Closed)
                    con.Close();
            }
            catch (Exception ex)
            {
                tran.Rollback();
                P = ex.Message;
            }
            return P;
        }


        public string UpdateReceiveSTK_TRANS(StockInterface ob)
        {
            string P = "";
            SqlTransaction tran = null;
            try
            {
                string userName = HttpContext.Current.Session["UserName"].ToString();
                if (con.State != ConnectionState.Open)
                    con.Open();
                tran = con.BeginTransaction();
                cmd.CommandType = CommandType.Text;
                cmd.CommandText = "update STK_TRANS set TRRTSNO =@TRRTSNO , UNITTO=@UNITTO , ITEMID =@ITEMID , ITEMCD=@ITEMCD, " +
                              " QTY=@QTY , RATE=@RATE , AMOUNT=@AMOUNT , UPDUSERPC=@UPDUSERPC , UPDATEUSERID=@UPDATEUSERID , UPDTIME=@UPDTIME , UPDIPADDRESS=@UPDIPADDRESS where TRANSTP = 'IREC' and TRANSDT=@TRANSDT and TRANSNO =@TRANSNO and ITEMSL =@ITEMSL ";
                cmd.Parameters.Clear();
                cmd.Parameters.Add("@TRRTSNO", SqlDbType.NVarChar).Value = ob.InvRefNo;
                cmd.Parameters.Add("@UNITTO", SqlDbType.NVarChar).Value = ob.To;
                cmd.Parameters.Add("@ITEMID", SqlDbType.NVarChar).Value = ob.ItemID;
                cmd.Parameters.Add("@ITEMCD", SqlDbType.NVarChar).Value = ob.ItemCD;
                cmd.Parameters.Add("@QTY", SqlDbType.Decimal).Value = ob.Qty;
                cmd.Parameters.Add("@RATE", SqlDbType.Decimal).Value = ob.Rate;
                cmd.Parameters.Add("@AMOUNT", SqlDbType.Decimal).Value = ob.Amount;
                cmd.Parameters.Add("@UPDUSERPC", SqlDbType.NVarChar).Value = ob.UPDUserPC;
                cmd.Parameters.Add("@UPDATEUSERID", SqlDbType.NVarChar).Value = ob.UpdUserID;
                cmd.Parameters.Add("@UPDTIME", SqlDbType.SmallDateTime).Value = ob.UpdTime;
                cmd.Parameters.Add("@UPDIPADDRESS", SqlDbType.NVarChar).Value = ob.UPDIpaddress;
                cmd.Parameters.Add("@TRANSDT", SqlDbType.SmallDateTime).Value = ob.TrDt;
                cmd.Parameters.Add("@TRANSNO", SqlDbType.NVarChar).Value = ob.TransNo2;
                cmd.Parameters.Add("@ITEMSL", SqlDbType.NVarChar).Value = ob.TransSL;

                cmd.Transaction = tran;
                cmd.ExecuteNonQuery();
                tran.Commit();
                if (con.State != ConnectionState.Closed)
                    con.Close();
            }
            catch (Exception ex)
            {
                tran.Rollback();
                P = ex.Message;
            }
            return P;

        }


        public string UpdateReceiveSTK_TRANSMST(StockInterface ob)
        {
            string P = "";
            SqlTransaction tran = null;
            try
            {
                string userName = HttpContext.Current.Session["UserName"].ToString();
                if (con.State != ConnectionState.Open)
                    con.Open();
                tran = con.BeginTransaction();
                cmd.CommandType = CommandType.Text;
                cmd.CommandText = "update STK_TRANSMST set TRRTSNO=@TRRTSNO , UNITTO=@UNITTO , REMARKS=@REMARKS," +
                              " UPDUSERPC=@UPDUSERPC , UPDATEUSERID =@UPDATEUSERID , UPDTIME=@UPDTIME , UPDIPADDRESS=@UPDIPADDRESS where TRANSTP = 'IREC' and TRANSDT=@TRANSDT and TRANSNO =@TRANSNO ";
                cmd.Parameters.Clear();
                cmd.Parameters.Add("@TRRTSNO", SqlDbType.NVarChar).Value = ob.InvRefNo;
                cmd.Parameters.Add("@UNITTO", SqlDbType.NVarChar).Value = ob.To;
                cmd.Parameters.Add("@REMARKS", SqlDbType.NVarChar).Value = ob.Remarks;
                cmd.Parameters.Add("@UPDUSERPC", SqlDbType.NVarChar).Value = ob.UPDUserPC;
                cmd.Parameters.Add("@UPDATEUSERID", SqlDbType.NVarChar).Value = ob.UpdUserID;
                cmd.Parameters.Add("@UPDTIME", SqlDbType.SmallDateTime).Value = ob.UpdTime;
                cmd.Parameters.Add("@UPDIPADDRESS", SqlDbType.NVarChar).Value = ob.UPDIpaddress;
                //cmd.Parameters.Add("@TRANSMY", SqlDbType.NVarChar).Value = ob.MonthYear;
                cmd.Parameters.Add("@TRANSDT", SqlDbType.SmallDateTime).Value = ob.TrDt;
                cmd.Parameters.Add("@TRANSNO", SqlDbType.NVarChar).Value = ob.TransNo2;

                cmd.Transaction = tran;
                cmd.ExecuteNonQuery();
                tran.Commit();
                if (con.State != ConnectionState.Closed)
                    con.Close();
            }
            catch (Exception ex)
            {
                tran.Rollback();
                P = ex.Message;
            }
            return P;
        }


        public string InsertReceiveSTK_TRANS(StockInterface ob)
        {
            string s = "";
            SqlTransaction tran = null;
            try
            {
                if (con.State != ConnectionState.Open)
                    con.Open();
                tran = con.BeginTransaction();
                cmd.CommandType = CommandType.Text;
                cmd.CommandText = @"insert into STK_TRANS ( TRANSTP, TRANSDT, TRANSNO, TRRTSNO, UNITFR, UNITTO, ITEMSL, ITEMID, ITEMCD, QTY, 
                                TRMODE, ITEMIDFG, PRODQTY, RATE, AMOUNT, USERPC, USERID, INTIME, IPADD) 
                  values(@TRANSTP, @TRANSDT, @TRANSNO, @TRRTSNO, @UNITFR, @UNITTO, @ITEMSL, @ITEMID, @ITEMCD, @QTY, 
                               @TRMODE, @ITEMIDFG, @PRODQTY, @RATE, @AMOUNT, @USERPC, @USERID, @INTIME, @IPADD)";

                cmd.Parameters.Clear();
                cmd.Parameters.Add("@TRANSTP", SqlDbType.NVarChar).Value = ob.TransTP;
                cmd.Parameters.Add("@TRANSDT", SqlDbType.SmallDateTime).Value = ob.TrDt;
                cmd.Parameters.Add("@TRANSNO", SqlDbType.NVarChar).Value = ob.TransNo2;
                cmd.Parameters.Add("@TRRTSNO", SqlDbType.NVarChar).Value = ob.InvRefNo;
                cmd.Parameters.Add("@UNITFR", SqlDbType.NVarChar).Value = ob.StoreFR;
                cmd.Parameters.Add("@UNITTO", SqlDbType.NVarChar).Value = ob.To;
                cmd.Parameters.Add("@ITEMSL", SqlDbType.NVarChar).Value = ob.TransSL;

                cmd.Parameters.Add("@TRMODE", SqlDbType.NVarChar).Value = ob.TranMode;
                cmd.Parameters.Add("@ITEMIDFG", SqlDbType.NVarChar).Value = ob.ItemFG;
                cmd.Parameters.Add("@PRODQTY", SqlDbType.Decimal).Value = ob.ProductionQty;

                cmd.Parameters.Add("@ITEMID", SqlDbType.NVarChar).Value = ob.ItemID;
                cmd.Parameters.Add("@ITEMCD", SqlDbType.NVarChar).Value = ob.ItemCD;
                //cmd.Parameters.Add("@UNITTP", SqlDbType.NVarChar).Value = ob.UnitTP;
                //cmd.Parameters.Add("@CPQTY", SqlDbType.Decimal).Value = ob.CpQTY;
                //cmd.Parameters.Add("@CQTY", SqlDbType.Decimal).Value = ob.CQTY;
                //cmd.Parameters.Add("@PQTY", SqlDbType.Decimal).Value = ob.PQTY;
                cmd.Parameters.Add("@QTY", SqlDbType.Decimal).Value = ob.Qty;
                cmd.Parameters.Add("@RATE", SqlDbType.Decimal).Value = ob.Rate;
                cmd.Parameters.Add("@AMOUNT", SqlDbType.Decimal).Value = ob.Amount;
                cmd.Parameters.Add("@USERPC", SqlDbType.NVarChar).Value = ob.UserPC;
                cmd.Parameters.Add("@INTIME", SqlDbType.SmallDateTime).Value = ob.ITime;
                cmd.Parameters.Add("@USERID", SqlDbType.NVarChar).Value = ob.UserID;
                cmd.Parameters.Add("@IPADD", SqlDbType.NVarChar).Value = ob.Ipaddress;
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



        public string InsertReceiveSTK_TRANSMST(StockInterface ob)
        {
            string s = "";
            SqlTransaction tran = null;
            try
            {
                if (con.State != ConnectionState.Open)
                    con.Open();
                tran = con.BeginTransaction();
                cmd.CommandType = CommandType.Text;
                cmd.CommandText = @"insert into STK_TRANSMST ( TRMODE,ITEMIDFG,PRODQTY,TRANSTP, TRANSDT, TRANSNO, TRRTSNO, UNITFR, UNITTO,REMARKS, USERPC, INTIME, USERID, IPADD) 
                  values(@TRMODE,@ITEMIDFG,@PRODQTY,@TRANSTP, @TRANSDT, @TRANSNO, @TRRTSNO, @UNITFR, @UNITTO, @REMARKS, @USERPC, @INTIME, @USERID, @IPADD)";
                //comm = new SqlCommand(query, conn);
                cmd.Parameters.Clear();
                cmd.Parameters.Add("@TRANSTP", SqlDbType.NVarChar).Value = ob.TransTP;
                cmd.Parameters.Add("@TRANSDT", SqlDbType.SmallDateTime).Value = ob.TrDt;
                //cmd.Parameters.Add("@TRANSMY", SqlDbType.NVarChar).Value = ob.MonthYear;
                cmd.Parameters.Add("@TRANSNO", SqlDbType.NVarChar).Value = ob.TransNo2;
                cmd.Parameters.Add("@TRRTSNO", SqlDbType.NVarChar).Value = ob.InvRefNo;
                cmd.Parameters.Add("@UNITFR", SqlDbType.NVarChar).Value = ob.StoreFR;
                cmd.Parameters.Add("@UNITTO", SqlDbType.NVarChar).Value = ob.To;

                cmd.Parameters.Add("@TRMODE", SqlDbType.NVarChar).Value = ob.TranMode;
                cmd.Parameters.Add("@ITEMIDFG", SqlDbType.NVarChar).Value = ob.ItemFG;
                cmd.Parameters.Add("@PRODQTY", SqlDbType.Decimal).Value = ob.ProductionQty;

                cmd.Parameters.Add("@REMARKS", SqlDbType.NVarChar).Value = ob.Remarks;
                cmd.Parameters.Add("@USERPC", SqlDbType.NVarChar).Value = ob.UserPC;
                cmd.Parameters.Add("@INTIME", SqlDbType.SmallDateTime).Value = ob.ITime;
                cmd.Parameters.Add("@USERID", SqlDbType.NVarChar).Value = ob.UserID;
                cmd.Parameters.Add("@IPADD", SqlDbType.NVarChar).Value = ob.Ipaddress;
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


        public string UpdatePrivilegeCard(StockInterface ob)
        {
            string s = "";
            SqlTransaction tran = null;
            try
            {
                if (con.State != ConnectionState.Open)
                    con.Open();
                tran = con.BeginTransaction();
                cmd.CommandType = CommandType.Text;
                cmd.CommandText = @"update PC_CARD set PCARDHTP=@PCARDHTP, PCARDHNM=@PCARDHNM, FATHERNM=@FATHERNM, MOTHERNM=@MOTHERNM," +
                                    "SPOUSENM=@SPOUSENM, ADDRESS_PRE=@ADDRESS_PRE, ADDRESS_PER=@ADDRESS_PER, " +
                                   "TELNO=@TELNO, MOBNO=@MOBNO, DOB=@DOB, EMAILID=@EMAILID, VOTERID=@VOTERID, COMPNM=@COMPNM, ADDRESSCO=@ADDRESSCO," +
                                   "DESIGNATION=@DESIGNATION, TELNOCO=@TELNOCO, MOBNOCO=@MOBNOCO, ANNUALINC=@ANNUALINC, PORTALINC=@PORTALINC, REMARKS=@REMARKS," +
                                   "REFERHID=@REFERHID, SUBMITDT=@SUBMITDT, ISSUEDT=@ISSUEDT, ISSUEFR=@ISSUEFR, STATUS=@STATUS, UPDUSERPC=@UPDUSERPC," +
                                   "UPDUSERID=@UPDUSERID, UPDTIME=@UPDTIME, UPDIPADD=@UPDIPADD where PCARDHID=@PCARDHID";

                cmd.Parameters.Clear();
                cmd.Parameters.Add("@PCARDHTP", SqlDbType.NVarChar).Value = ob.CardHTP;
                cmd.Parameters.Add("@PCARDHNM", SqlDbType.NVarChar).Value = ob.CardHNM;
                cmd.Parameters.Add("@FATHERNM", SqlDbType.NVarChar).Value = ob.FatherNM;
                cmd.Parameters.Add("@MOTHERNM", SqlDbType.NVarChar).Value = ob.MotherNM;
                cmd.Parameters.Add("@SPOUSENM", SqlDbType.NVarChar).Value = ob.SpouseNM;
                cmd.Parameters.Add("@ADDRESS_PRE", SqlDbType.NVarChar).Value = ob.PrsntAdd;
                cmd.Parameters.Add("@ADDRESS_PER", SqlDbType.NVarChar).Value = ob.PermntAdd;
                cmd.Parameters.Add("@TELNO", SqlDbType.NVarChar).Value = ob.TelNo;
                cmd.Parameters.Add("@MOBNO", SqlDbType.NVarChar).Value = ob.MobNo;
                cmd.Parameters.Add("@DOB", SqlDbType.DateTime).Value = ob.DOfBirth;
                cmd.Parameters.Add("@EMAILID", SqlDbType.NVarChar).Value = ob.EmailID;
                cmd.Parameters.Add("@VOTERID", SqlDbType.NVarChar).Value = ob.VotrID;
                cmd.Parameters.Add("@COMPNM", SqlDbType.NVarChar).Value = ob.OffNM;
                cmd.Parameters.Add("@ADDRESSCO", SqlDbType.NVarChar).Value = ob.OffAdd;
                cmd.Parameters.Add("@DESIGNATION", SqlDbType.NVarChar).Value = ob.Designation;
                cmd.Parameters.Add("@TELNOCO", SqlDbType.NVarChar).Value = ob.OffTelNo;
                cmd.Parameters.Add("@MOBNOCO", SqlDbType.NVarChar).Value = ob.OffMblNo;
                cmd.Parameters.Add("@ANNUALINC", SqlDbType.Decimal).Value = ob.AnnualIncm;
                cmd.Parameters.Add("@PORTALINC", SqlDbType.NVarChar).Value = ob.IncmPortl;
                cmd.Parameters.Add("@REMARKS", SqlDbType.NVarChar).Value = ob.Comments;
                cmd.Parameters.Add("@REFERHID", SqlDbType.NVarChar).Value = ob.RfrCrdHID;
                cmd.Parameters.Add("@SUBMITDT", SqlDbType.DateTime).Value = ob.SubmtDT;
                cmd.Parameters.Add("@ISSUEDT", SqlDbType.DateTime).Value = ob.IssuDT;
                cmd.Parameters.Add("@ISSUEFR", SqlDbType.NVarChar).Value = ob.IssuFrm;
                cmd.Parameters.Add("@STATUS", SqlDbType.NVarChar).Value = ob.Status;
                cmd.Parameters.Add("@UPDUSERPC", SqlDbType.NVarChar).Value = ob.UPDUserPC;
                cmd.Parameters.Add("@UPDUSERID", SqlDbType.NVarChar).Value = ob.UPDUserID;
                cmd.Parameters.Add("@UPDTIME", SqlDbType.DateTime).Value = ob.UpdTime;
                cmd.Parameters.Add("@UPDIPADD", SqlDbType.NVarChar).Value = ob.UPDIpaddress;
                cmd.Parameters.Add("@PCARDHID", SqlDbType.NVarChar).Value = ob.CardHID;

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


        public string UpdateReceiveSTK_TRANSCS(StockInterface ob)
        {
            string P = "";
            SqlTransaction tran = null;
            try
            {
                string userName = HttpContext.Current.Session["UserName"].ToString();
                if (con.State != ConnectionState.Open)
                    con.Open();
                tran = con.BeginTransaction();
                cmd.CommandType = CommandType.Text;

                cmd.CommandText = "update STK_TRANSCS set INVREFNO =@INVREFNO , STORETO=@STORETO , REMARKS =@REMARKS , CATID =@CATID , ITEMID =@ITEMID , UNITTP =@UNITTP , CPQTY =@CPQTY , CQTY =@CQTY , PQTY =@PQTY , " +
                              " QTY=@QTY , RATE=@RATE , AMOUNT=@AMOUNT , UPDUSERPC=@UPDUSERPC , UPDATEUSERID=@UPDATEUSERID , UPDTIME=@UPDTIME , UPDIPADDRESS=@UPDIPADDRESS where TRANSTP = 'IRFG' and TRANSMY=@TRANSMY and TRANSDT=@TRANSDT and TRANSNO =@TRANSNO and TRANSSL =@TRANSSL ";
                cmd.Parameters.Clear();
                cmd.Parameters.Add("@INVREFNO", SqlDbType.NVarChar).Value = ob.InvRefNo;
                cmd.Parameters.Add("@STORETO", SqlDbType.NVarChar).Value = ob.To;
                //cmd.Parameters.Add("@PSID", SqlDbType.NVarChar).Value = ob.PsId;
                //cmd.Parameters.Add("@LCTP", SqlDbType.NVarChar).Value = ob.LcTp;
                //cmd.Parameters.Add("@LCID", SqlDbType.NVarChar).Value = ob.LcId;
                //cmd.Parameters.Add("@LCDATE", SqlDbType.Date).Value = ob.LcDt;
                cmd.Parameters.Add("@REMARKS", SqlDbType.NVarChar).Value = ob.Remarks;
                cmd.Parameters.Add("@CATID", SqlDbType.NVarChar).Value = ob.CatID;
                cmd.Parameters.Add("@ITEMID", SqlDbType.NVarChar).Value = ob.ItemID;
                cmd.Parameters.Add("@UNITTP", SqlDbType.NVarChar).Value = ob.UnitTP;
                cmd.Parameters.Add("@CPQTY", SqlDbType.Decimal).Value = ob.CpQTY;
                cmd.Parameters.Add("@CQTY", SqlDbType.Decimal).Value = ob.CQTY;
                cmd.Parameters.Add("@PQTY", SqlDbType.Decimal).Value = ob.PQTY;
                cmd.Parameters.Add("@QTY", SqlDbType.Decimal).Value = ob.Qty;
                cmd.Parameters.Add("@RATE", SqlDbType.Decimal).Value = ob.Rate;
                cmd.Parameters.Add("@AMOUNT", SqlDbType.Decimal).Value = ob.Amount;
                cmd.Parameters.Add("@UPDUSERPC", SqlDbType.NVarChar).Value = ob.UPDUserPC;
                cmd.Parameters.Add("@UPDATEUSERID", SqlDbType.NVarChar).Value = ob.UserID;
                cmd.Parameters.Add("@UPDTIME", SqlDbType.DateTime).Value = ob.UpdTime;
                cmd.Parameters.Add("@UPDIPADDRESS", SqlDbType.NVarChar).Value = ob.UPDIpaddress;
                cmd.Parameters.Add("@TRANSMY", SqlDbType.NVarChar).Value = ob.MonthYear;
                cmd.Parameters.Add("@TRANSDT", SqlDbType.DateTime).Value = ob.TrDt;
                cmd.Parameters.Add("@TRANSNO", SqlDbType.BigInt).Value = ob.TransNo;
                cmd.Parameters.Add("@TRANSSL", SqlDbType.NVarChar).Value = ob.TransSL;

                cmd.Transaction = tran;
                cmd.ExecuteNonQuery();
                tran.Commit();
                if (con.State != ConnectionState.Closed)
                    con.Close();
            }
            catch (Exception ex)
            {
                tran.Rollback();
                P = ex.Message;
            }
            return P;

        }






        public string UpdatePurchaseSTK_TRANS(StockInterface ob)
        {
            string P = "";
            SqlTransaction tran = null;
            try
            {
                string userName = HttpContext.Current.Session["UserName"].ToString();
                if (con.State != ConnectionState.Open)
                    con.Open();
                tran = con.BeginTransaction();
                cmd.CommandType = CommandType.Text;

                cmd.CommandText = "update STK_TRANS set INVREFNO =@INVREFNO , STORETO=@STORETO , PSID=@PSID , LCTP =@LCTP , LCID=@LCID , LCDATE =@LCDATE , REMARKS =@REMARKS , CATID =@CATID , ITEMID =@ITEMID , UNITTP =@UNITTP , CPQTY =@CPQTY , CQTY =@CQTY , PQTY =@PQTY , " +
                              " QTY =@QTY , RATE =@RATE , AMOUNT =@AMOUNT , UPDUSERPC=@UPDUSERPC , UPDATEUSERID =@UPDATEUSERID , UPDTIME=@UPDTIME , UPDIPADDRESS=@UPDIPADDRESS where TRANSTP = 'BUY' and TRANSMY=@TRANSMY and TRANSDT=@TRANSDT and TRANSNO =@TRANSNO and TRANSSL =@TRANSSL ";
                cmd.Parameters.Clear();
                cmd.Parameters.Add("@INVREFNO", SqlDbType.NVarChar).Value = ob.InvRefNo;
                cmd.Parameters.Add("@STORETO", SqlDbType.NVarChar).Value = ob.To;
                cmd.Parameters.Add("@PSID", SqlDbType.NVarChar).Value = ob.PsId;
                cmd.Parameters.Add("@LCTP", SqlDbType.NVarChar).Value = ob.LcTp;
                cmd.Parameters.Add("@LCID", SqlDbType.NVarChar).Value = ob.LcId;
                cmd.Parameters.Add("@LCDATE", SqlDbType.Date).Value = ob.LcDt;
                cmd.Parameters.Add("@REMARKS", SqlDbType.NVarChar).Value = ob.Remarks;
                cmd.Parameters.Add("@CATID", SqlDbType.NVarChar).Value = ob.CatID;
                cmd.Parameters.Add("@ITEMID", SqlDbType.NVarChar).Value = ob.ItemID;
                cmd.Parameters.Add("@UNITTP", SqlDbType.NVarChar).Value = ob.UnitTP;
                cmd.Parameters.Add("@CPQTY", SqlDbType.Decimal).Value = ob.CpQTY;
                cmd.Parameters.Add("@CQTY", SqlDbType.Decimal).Value = ob.CQTY;
                cmd.Parameters.Add("@PQTY", SqlDbType.Decimal).Value = ob.PQTY;
                cmd.Parameters.Add("@QTY", SqlDbType.Decimal).Value = ob.Qty;
                cmd.Parameters.Add("@RATE", SqlDbType.Decimal).Value = ob.Rate;
                cmd.Parameters.Add("@AMOUNT", SqlDbType.Decimal).Value = ob.Amount;
                cmd.Parameters.Add("@UPDUSERPC", SqlDbType.NVarChar).Value = ob.UPDUserPC;
                cmd.Parameters.Add("@UPDATEUSERID", SqlDbType.NVarChar).Value = ob.UserID;
                cmd.Parameters.Add("@UPDTIME", SqlDbType.DateTime).Value = ob.UpdTime;
                cmd.Parameters.Add("@UPDIPADDRESS", SqlDbType.NVarChar).Value = ob.UPDIpaddress;
                cmd.Parameters.Add("@TRANSMY", SqlDbType.NVarChar).Value = ob.MonthYear;
                cmd.Parameters.Add("@TRANSDT", SqlDbType.DateTime).Value = ob.TrDt;
                cmd.Parameters.Add("@TRANSNO", SqlDbType.BigInt).Value = ob.TransNo;
                cmd.Parameters.Add("@TRANSSL", SqlDbType.NVarChar).Value = ob.TransSL;

                cmd.Transaction = tran;
                cmd.ExecuteNonQuery();
                tran.Commit();
                if (con.State != ConnectionState.Closed)
                    con.Close();
            }
            catch (Exception ex)
            {
                tran.Rollback();
                P = ex.Message;
            }
            return P;

        }
        public string UpdatePurchaseBtnTRANSMST(StockInterface ob)
        {
            string m = "";
            SqlTransaction tran = null;
            try
            {
                string userName = HttpContext.Current.Session["UserName"].ToString();
                if (con.State != ConnectionState.Open)
                    con.Open();
                tran = con.BeginTransaction();
                cmd.CommandType = CommandType.Text;
                cmd.CommandText = @"update STK_TRANSMSTCS set INVREFNO =@INVREFNO, STORETO=@STORETO, PSID=@PSID, REMARKS=@REMARKS,
        LCID=@LCID,
        UPDATEUSERID=@UPDATEUSERID, UPDUSERPC=@UPDUSERPC, UPDTIME=@UPDTIME, UPDIPADDRESS=@UPDIPADDRESS 
                        where TRANSTP = 'BUY' and TRANSMY=@TRANSMY and TRANSDT=@TRANSDT and TRANSNO =@TRANSNO";

                cmd.Parameters.Clear();
                cmd.Parameters.Add("@INVREFNO", SqlDbType.NVarChar).Value = ob.InvRefNo;
                cmd.Parameters.Add("@STORETO", SqlDbType.NVarChar).Value = ob.ToId;
                cmd.Parameters.Add("@PSID", SqlDbType.NVarChar).Value = ob.PsId;
                cmd.Parameters.Add("@REMARKS", SqlDbType.NVarChar).Value = ob.Remarks;
                cmd.Parameters.Add("@LCID", SqlDbType.NVarChar).Value = ob.LcId;

                cmd.Parameters.Add("@TRANSMY", SqlDbType.NVarChar).Value = ob.MonthYear;
                cmd.Parameters.Add("@TRANSDT", SqlDbType.NVarChar).Value = ob.TrDt;
                cmd.Parameters.Add("@TRANSNO", SqlDbType.NVarChar).Value = ob.TransNo;

                cmd.Parameters.Add("@UPDATEUSERID", SqlDbType.NVarChar).Value = ob.UpdUserID;
                cmd.Parameters.Add("@UPDUSERPC", SqlDbType.NVarChar).Value = ob.UPDUserPC;
                cmd.Parameters.Add("@UPDTIME", SqlDbType.NVarChar).Value = ob.UpdTime;
                cmd.Parameters.Add("@UPDIPADDRESS", SqlDbType.NVarChar).Value = ob.UPDIpaddress;

                cmd.Transaction = tran;
                cmd.ExecuteNonQuery();
                tran.Commit();
                if (con.State != ConnectionState.Closed)
                    con.Close();
            }
            catch (Exception ex)
            {
                tran.Rollback();
                m = ex.Message;
            }
            return m;
        }
        public string UpdatePurchaseBtnTRANS(StockInterface ob)
        {
            string P1 = "";
            SqlTransaction tran = null;
            try
            {
                string userName = HttpContext.Current.Session["UserName"].ToString();
                if (con.State != ConnectionState.Open)
                    con.Open();
                tran = con.BeginTransaction();
                cmd.CommandType = CommandType.Text;
                cmd.CommandText = @"update STK_TRANSCS set INVREFNO =@INVREFNO, STORETO=@STORETO, LCID=@LCID, PSID=@PSID, REMARKS=@REMARKS, 
                UPDATEUSERID=@UPDATEUSERID, UPDUSERPC=@UPDUSERPC, UPDTIME=@UPDTIME, UPDIPADDRESS=@UPDIPADDRESS 
                where TRANSTP = 'BUY' and TRANSMY=@TRANSMY and TRANSDT=@TRANSDT and TRANSNO =@TRANSNO";

                cmd.Parameters.Clear();
                cmd.Parameters.Add("@INVREFNO", SqlDbType.NVarChar).Value = ob.InvRefNo;
                cmd.Parameters.Add("@STORETO", SqlDbType.NVarChar).Value = ob.ToId;
                cmd.Parameters.Add("@PSID", SqlDbType.NVarChar).Value = ob.PsId;
                cmd.Parameters.Add("@REMARKS", SqlDbType.NVarChar).Value = ob.Remarks;
                cmd.Parameters.Add("@LCID", SqlDbType.NVarChar).Value = ob.LcId; ;

                cmd.Parameters.Add("@TRANSMY", SqlDbType.NVarChar).Value = ob.MonthYear;
                cmd.Parameters.Add("@TRANSDT", SqlDbType.NVarChar).Value = ob.TrDt;
                cmd.Parameters.Add("@TRANSNO", SqlDbType.NVarChar).Value = ob.TransNo;

                cmd.Parameters.Add("@UPDATEUSERID", SqlDbType.NVarChar).Value = ob.UpdUserID;
                cmd.Parameters.Add("@UPDUSERPC", SqlDbType.NVarChar).Value = ob.UPDUserPC;
                cmd.Parameters.Add("@UPDTIME", SqlDbType.NVarChar).Value = ob.UpdTime;
                cmd.Parameters.Add("@UPDIPADDRESS", SqlDbType.NVarChar).Value = ob.UPDIpaddress;

                cmd.Transaction = tran;
                cmd.ExecuteNonQuery();
                tran.Commit();
                if (con.State != ConnectionState.Closed)
                    con.Close();
            }
            catch (Exception ex)
            {
                tran.Rollback();
                P1 = ex.Message;
            }
            return P1;
        }

        public string UPDTransfrSTK_TransMST(StockInterface ob)
        {
            string s = "";
            SqlTransaction tran = null;
            try
            {
                if (con.State != ConnectionState.Open)
                    con.Open();
                tran = con.BeginTransaction();
                cmd.CommandType = CommandType.Text;
                //SqlCommand cmd1 = new SqlCommand("update STK_TRANSMST set INVREFNO = '" + txtMemo_Trans.Text + "', STOREFR = '" + txtTFrID_Trans.Text + "', STORETO='" + txtTToID_Trans.Text + "', REMARKS = '" + txtRemarks_Trans.Text + "', USERID = '" + userName + "'" +
                //          " where TRANSTP = 'ITRF' and TRANSMY='" + lblTMy.Text + "' and TRANSDT='" + TransDT + "' and TRANSNO = " + TransNo + "", conn);
                //cmd1.ExecuteNonQuery();
                cmd.CommandText = "update STK_TRANSMST set INVREFNO=@INVREFNO,STOREFR=@STOREFR,STORETO=@STORETO,REMARKS=@REMARKS,UPDATEUSERID=@UPDATEUSERID, " +
                    "UPDUSERPC=@UPDUSERPC,UPDTIME=@UPDTIME,UPDIPADDRESS=@UPDIPADDRESS  where TRANSTP='ITRF' and TRANSMY=@TRANSMY and TRANSDT=@TRANSDT " +
                    "and TRANSNO=@TRANSNO";
                cmd.Parameters.Clear();
                cmd.Parameters.Add("@INVREFNO", SqlDbType.NVarChar).Value = ob.InvRefNo;
                cmd.Parameters.Add("@STOREFR", SqlDbType.NVarChar).Value = ob.StoreFR;
                cmd.Parameters.Add("@STORETO", SqlDbType.NVarChar).Value = ob.To;
                cmd.Parameters.Add("@REMARKS", SqlDbType.NVarChar).Value = ob.Remarks;
                cmd.Parameters.Add("@UPDATEUSERID", SqlDbType.NVarChar).Value = ob.UserID;
                cmd.Parameters.Add("@UPDUSERPC", SqlDbType.NVarChar).Value = ob.UPDUserPC;
                cmd.Parameters.Add("@UPDTIME", SqlDbType.SmallDateTime).Value = ob.UpdTime;
                cmd.Parameters.Add("@UPDIPADDRESS", SqlDbType.NVarChar).Value = ob.UPDIpaddress;
                //For Where clouse
                cmd.Parameters.Add("@TRANSMY", SqlDbType.NVarChar).Value = ob.MonthYear;
                cmd.Parameters.Add("@TRANSDT", SqlDbType.SmallDateTime).Value = ob.TrDt;
                cmd.Parameters.Add("@TRANSNO", SqlDbType.BigInt).Value = ob.TransNo;
                //cmd.Parameters.Add("@TRANSSL", SqlDbType.NVarChar).Value = ob.TransSL;

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
        public string InsertTransfrSTK_TRANS(StockInterface ob)
        {
            string s = "";
            SqlTransaction tran = null;
            try
            {
                if (con.State != ConnectionState.Open)
                    con.Open();
                tran = con.BeginTransaction();
                cmd.CommandType = CommandType.Text;
                cmd.CommandText = @"insert into STK_TRANS ( TRANSTP, TRANSDT, TRANSNO, TRRTSNO, UNITFR, UNITTO, ITEMSL, ITEMID, ITEMCD, QTY, 
                                RATE, AMOUNT, USERPC, USERID, INTIME, IPADD) 
                  values(@TRANSTP, @TRANSDT, @TRANSNO, @TRRTSNO, @UNITFR, @UNITTO,
                                @ITEMSL, @ITEMID, @ITEMCD, @QTY, @RATE, @AMOUNT, @USERPC, @USERID, @INTIME, @IPADD)";

                cmd.Parameters.Clear();
                cmd.Parameters.Add("@TRANSTP", SqlDbType.NVarChar).Value = ob.TransTP;
                cmd.Parameters.Add("@TRANSDT", SqlDbType.SmallDateTime).Value = ob.TrDt;
                cmd.Parameters.Add("@TRANSNO", SqlDbType.NVarChar).Value = ob.TransNo2;
                //cmd.Parameters.Add("@TRANSMY", SqlDbType.NVarChar).Value = ob.MonthYear;
                cmd.Parameters.Add("@TRRTSNO", SqlDbType.NVarChar).Value = ob.InvRefNo;
                cmd.Parameters.Add("@UNITFR", SqlDbType.NVarChar).Value = ob.StoreFR;
                cmd.Parameters.Add("@UNITTO", SqlDbType.NVarChar).Value = ob.To;
                cmd.Parameters.Add("@ITEMSL", SqlDbType.NVarChar).Value = ob.TransSL;
                //cmd.Parameters.Add("@REMARKS", SqlDbType.NVarChar).Value = ob.Remarks;
                //cmd.Parameters.Add("@CATID", SqlDbType.NVarChar).Value = ob.CatID;
                cmd.Parameters.Add("@ITEMID", SqlDbType.NVarChar).Value = ob.ItemID;
                cmd.Parameters.Add("@ITEMCD", SqlDbType.NVarChar).Value = ob.ItemCD;
                //cmd.Parameters.Add("@UNITTP", SqlDbType.NVarChar).Value = ob.UnitTP;
                //cmd.Parameters.Add("@CPQTY", SqlDbType.Decimal).Value = ob.CpQTY;
                //cmd.Parameters.Add("@CQTY", SqlDbType.Decimal).Value = ob.CQTY;
                //cmd.Parameters.Add("@PQTY", SqlDbType.Decimal).Value = ob.PQTY;
                cmd.Parameters.Add("@QTY", SqlDbType.Decimal).Value = ob.Qty;
                cmd.Parameters.Add("@RATE", SqlDbType.Decimal).Value = ob.Rate;
                cmd.Parameters.Add("@AMOUNT", SqlDbType.Decimal).Value = ob.Amount;
                cmd.Parameters.Add("@USERPC", SqlDbType.NVarChar).Value = ob.UserPC;
                cmd.Parameters.Add("@INTIME", SqlDbType.SmallDateTime).Value = ob.ITime;
                cmd.Parameters.Add("@USERID", SqlDbType.NVarChar).Value = ob.UserID;
                cmd.Parameters.Add("@IPADD", SqlDbType.NVarChar).Value = ob.Ipaddress;
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
        public string InsertTransfrSTK_TRANSMST(StockInterface ob)
        {
            string s = "";
            SqlTransaction tran = null;
            try
            {
                if (con.State != ConnectionState.Open)
                    con.Open();
                tran = con.BeginTransaction();
                cmd.CommandType = CommandType.Text;
                cmd.CommandText = @"insert into STK_TRANSMST ( TRANSTP, TRANSDT, TRANSNO, TRRTSNO, UNITFR, UNITTO, REMARKS, USERPC, INTIME, USERID, IPADD) 
                  values(@TRANSTP, @TRANSDT, @TRANSNO, @TRRTSNO, @UNITFR, @UNITTO, @REMARKS, @USERPC, @INTIME, @USERID, @IPADDRESS)";
                //comm = new SqlCommand(query, conn);
                cmd.Parameters.Clear();
                cmd.Parameters.Add("@TRANSTP", SqlDbType.NVarChar).Value = ob.TransTP;
                cmd.Parameters.Add("@TRANSDT", SqlDbType.SmallDateTime).Value = ob.TrDt;
                //cmd.Parameters.Add("@TRANSMY", SqlDbType.NVarChar).Value = ob.MonthYear;
                cmd.Parameters.Add("@TRANSNO", SqlDbType.NVarChar).Value = ob.TransNo2;
                cmd.Parameters.Add("@TRRTSNO", SqlDbType.NVarChar).Value = ob.InvRefNo;
                cmd.Parameters.Add("@UNITFR", SqlDbType.NVarChar).Value = ob.StoreFR;
                cmd.Parameters.Add("@UNITTO", SqlDbType.NVarChar).Value = ob.To;
                cmd.Parameters.Add("@REMARKS", SqlDbType.NVarChar).Value = ob.Remarks;
                cmd.Parameters.Add("@USERPC", SqlDbType.NVarChar).Value = ob.UserPC;
                cmd.Parameters.Add("@INTIME", SqlDbType.SmallDateTime).Value = ob.ITime;
                cmd.Parameters.Add("@USERID", SqlDbType.NVarChar).Value = ob.UserID;
                cmd.Parameters.Add("@IPADDRESS", SqlDbType.NVarChar).Value = ob.Ipaddress;
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
        public string insertSaleParTrans(StockInterface ob)
        {
            string s = "";
            SqlTransaction tran = null;
            try
            {
                if (con.State != ConnectionState.Open)
                    con.Open();
                tran = con.BeginTransaction();
                cmd.CommandType = CommandType.Text;

                cmd.CommandText = "insert into STK_TRANSCS (TRANSTP,TRANSDT,TRANSMY,TRANSNO,INVREFNO,STOREFR,STORETO, " +
                                "PSID,REMARKS,TRANSSL,CATID,ITEMID,UNITTP,CPQTY,CQTY,PQTY,QTY,RATE,AMOUNT,  " +
                                "DISCRT,DISCAMT,NETAMT,INTIME,USERPC,USERID,IPADDRESS ) " +
                 " values(@TRANSTP,@TRANSDT,@TRANSMY,@TRANSNO,@INVREFNO,@STOREFR,@STORETO, " +
                                "@PSID,@REMARKS,@TRANSSL,@CATID,@ITEMID,@UNITTP,@CPQTY,@CQTY,@PQTY,@QTY,@RATE,@AMOUNT,  " +
                                "@DISCRT,@DISCAMT,@NETAMT,@INTIME,@USERPC,@USERID,@IPADDRESS )";

                cmd.Parameters.Clear();
                cmd.Parameters.Add("@TRANSTP", SqlDbType.NVarChar).Value = ob.TransTP;
                cmd.Parameters.Add("@TRANSDT", SqlDbType.SmallDateTime).Value = ob.TrDt;
                cmd.Parameters.Add("@TRANSMY", SqlDbType.NVarChar).Value = ob.MonthYear;
                cmd.Parameters.Add("@TRANSNO", SqlDbType.BigInt).Value = ob.TransNo;
                cmd.Parameters.Add("@INVREFNO", SqlDbType.NVarChar).Value = ob.InvRefNo;
                cmd.Parameters.Add("@STOREFR", SqlDbType.NVarChar).Value = ob.StoreFR;
                cmd.Parameters.Add("@STORETO", SqlDbType.NVarChar).Value = ob.To;
                cmd.Parameters.Add("@PSID", SqlDbType.NVarChar).Value = ob.PsId;
                cmd.Parameters.Add("@REMARKS", SqlDbType.NVarChar).Value = ob.Remarks;
                cmd.Parameters.Add("@TRANSSL", SqlDbType.NVarChar).Value = ob.TransSL;
                cmd.Parameters.Add("@CATID", SqlDbType.NVarChar).Value = ob.CatID;
                cmd.Parameters.Add("@ITEMID", SqlDbType.NVarChar).Value = ob.ItemID;
                cmd.Parameters.Add("@UNITTP", SqlDbType.NVarChar).Value = ob.UnitTP;
                cmd.Parameters.Add("@CPQTY", SqlDbType.Decimal).Value = ob.CpQTY;
                cmd.Parameters.Add("@CQTY", SqlDbType.Decimal).Value = ob.CQTY;
                cmd.Parameters.Add("@PQTY", SqlDbType.Decimal).Value = ob.PQTY;
                cmd.Parameters.Add("@QTY", SqlDbType.Decimal).Value = ob.Qty;
                cmd.Parameters.Add("@RATE", SqlDbType.Decimal).Value = ob.Rate;
                cmd.Parameters.Add("@AMOUNT", SqlDbType.Decimal).Value = ob.Amount;

                cmd.Parameters.Add("@DISCRT", SqlDbType.Decimal).Value = ob.disCountRT;
                cmd.Parameters.Add("@DISCAMT", SqlDbType.Decimal).Value = ob.disAmount;
                cmd.Parameters.Add("@NETAMT", SqlDbType.Decimal).Value = ob.NetAmt;
                cmd.Parameters.Add("@INTIME", SqlDbType.SmallDateTime).Value = ob.ITime;
                cmd.Parameters.Add("@USERPC", SqlDbType.NVarChar).Value = ob.UserPC;
                cmd.Parameters.Add("@USERID", SqlDbType.NVarChar).Value = ob.UserID;
                cmd.Parameters.Add("@IPADDRESS", SqlDbType.NVarChar).Value = ob.Ipaddress;
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


        public string insertSaleParTransMST(StockInterface ob)
        {
            string s = "";
            SqlTransaction tran = null;
            try
            {
                if (con.State != ConnectionState.Open)
                    con.Open();
                tran = con.BeginTransaction();
                cmd.CommandType = CommandType.Text;
                cmd.CommandText = "insert into STK_TRANSMSTCS (TRANSTP,TRANSDT,TRANSMY,TRANSNO,INVREFNO,STOREFR,STORETO, " +
                                "PSID,REMARKS, " +
                                "INTIME,USERPC,USERID,IPADDRESS ) " +
                 " values(@TRANSTP,@TRANSDT,@TRANSMY,@TRANSNO,@INVREFNO,@STOREFR,@STORETO, " +
                                "@PSID,@REMARKS, " +
                                "@INTIME,@USERPC,@USERID,@IPADDRESS )";

                cmd.Parameters.Clear();
                cmd.Parameters.Add("@TRANSTP", SqlDbType.NVarChar).Value = ob.TransTP;
                cmd.Parameters.Add("@TRANSDT", SqlDbType.SmallDateTime).Value = ob.TrDt;
                cmd.Parameters.Add("@TRANSMY", SqlDbType.NVarChar).Value = ob.MonthYear;
                cmd.Parameters.Add("@TRANSNO", SqlDbType.BigInt).Value = ob.TransNo;
                cmd.Parameters.Add("@INVREFNO", SqlDbType.NVarChar).Value = ob.InvRefNo;
                cmd.Parameters.Add("@STOREFR", SqlDbType.NVarChar).Value = ob.StoreFR;
                cmd.Parameters.Add("@STORETO", SqlDbType.NVarChar).Value = ob.To;
                cmd.Parameters.Add("@PSID", SqlDbType.NVarChar).Value = ob.PsId;
                cmd.Parameters.Add("@REMARKS", SqlDbType.NVarChar).Value = ob.Remarks;
                cmd.Parameters.Add("@INTIME", SqlDbType.SmallDateTime).Value = ob.ITime;
                cmd.Parameters.Add("@USERPC", SqlDbType.NVarChar).Value = ob.UserPC;
                cmd.Parameters.Add("@USERID", SqlDbType.NVarChar).Value = ob.UserID;
                cmd.Parameters.Add("@IPADDRESS", SqlDbType.NVarChar).Value = ob.Ipaddress;
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
        public string updateSaleParTransMST(StockInterface ob)
        {
            string s = "";
            SqlTransaction tran = null;
            try
            {
                if (con.State != ConnectionState.Open)
                    con.Open();
                tran = con.BeginTransaction();
                cmd.CommandType = CommandType.Text;
                //SqlCommand cmd1 = new SqlCommand("update STK_TRANSMST set TOTAMT=" + totamt + ", DISAMT=" + grsDis + ", TOTNET = " + p_NetAmt + ", " +
                //                       " USERID = '" + userName + "' where TRANSTP = 'IRTS'  and TRANSMY='" + lblMy_Ret.Text + "' and TRANSDT='" + TrDate + "' and TRANSNO = " + TransNo + "", conn);

                cmd.CommandText = "update STK_TRANSMST set TOTAMT=@TOTAMT,DISAMT=@DISAMT,TOTNET=@TOTNET,UPDATEUSERID=@UPDATEUSERID,UPDTIME=@UPDTIME,UPDIPADDRESS=@UPDIPADDRESS where  TRANSTP=@TRANSTP  " +
                "and TRANSDT=@TRANSDT and TRANSNO=@TRANSNO ";
                cmd.Parameters.Clear();
                cmd.Parameters.Add("@TOTAMT", SqlDbType.Decimal).Value = ob.TotAmnt;
                cmd.Parameters.Add("@DISAMT", SqlDbType.Decimal).Value = ob.disAmount;
                cmd.Parameters.Add("@TOTNET", SqlDbType.Decimal).Value = ob.NetAmt;
                cmd.Parameters.Add("@TRANSTP", SqlDbType.NVarChar).Value = ob.TransTP;
                cmd.Parameters.Add("@TRANSDT", SqlDbType.SmallDateTime).Value = ob.TrDt;
                cmd.Parameters.Add("@TRANSNO", SqlDbType.BigInt).Value = ob.TransNo;
                cmd.Parameters.Add("@UPDATEUSERID", SqlDbType.NVarChar).Value = ob.UPDUserPC;
                cmd.Parameters.Add("@UPDTIME", SqlDbType.SmallDateTime).Value = ob.UpdTime;
                cmd.Parameters.Add("@UPDIPADDRESS", SqlDbType.NVarChar).Value = ob.UPDIpaddress;
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
        public string updateSaleParTrans(StockInterface ob)
        {
            string s = "";
            SqlTransaction tran = null;
            try
            {
                if (con.State != ConnectionState.Open)
                    con.Open();
                tran = con.BeginTransaction();
                cmd.CommandType = CommandType.Text;
                cmd.CommandText = "update STK_TRANSMST set INVREFNO=@INVREFNO,STOREFR=@STOREFR,PSID=@PSID,REMARKS=@REMARKS," +
              "  UPDATEUSERID=@UPDATEUSERID,UPDTIME=@UPDTIME,UPDIPADDRESS=@UPDIPADDRESS where  TRANSTP=@TRANSTP and TRANSMY=@TRANSMY " +
                "and TRANSDT=@TRANSDT and TRANSNO=@TRANSNO";

                cmd.Parameters.Clear();
                cmd.Parameters.Add("@INVREFNO", SqlDbType.NVarChar).Value = ob.TotAmnt;
                cmd.Parameters.Add("@STOREFR", SqlDbType.NVarChar).Value = ob.disAmount;
                cmd.Parameters.Add("@PSID", SqlDbType.NVarChar).Value = ob.NetAmt;
                cmd.Parameters.Add("@REMARKS", SqlDbType.NVarChar).Value = ob.TransTP;
                cmd.Parameters.Add("@UPDATEUSERID", SqlDbType.NVarChar).Value = ob.UPDUserPC;
                cmd.Parameters.Add("@UPDTIME", SqlDbType.SmallDateTime).Value = ob.UpdTime;
                cmd.Parameters.Add("@UPDIPADDRESS", SqlDbType.NVarChar).Value = ob.UPDIpaddress;
                cmd.Parameters.Add("@TRANSTP", SqlDbType.NVarChar).Value = ob.TrDt;
                cmd.Parameters.Add("@TRANSMY", SqlDbType.NVarChar).Value = ob.TransNo;
                cmd.Parameters.Add("@TRANSDT", SqlDbType.SmallDateTime).Value = ob.TrDt;
                cmd.Parameters.Add("@TRANSNO", SqlDbType.BigInt).Value = ob.TransNo;
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
        public string GridUPD_RetSTK_Trans(StockInterface ob)
        {
            string s = "";
            SqlTransaction tran = null;
            try
            {
                if (con.State != ConnectionState.Open)
                    con.Open();
                tran = con.BeginTransaction();
                cmd.CommandText = "update STK_TRANS set INVREFNO=@INVREFNO,STORETO=@STORETO,PSID=@PSID,REMARKS=@REMARKS,CATID=@CATID," +
                    "ITEMID=@ITEMID,UNITTP=@UNITTP,CPQTY=@CPQTY,CQTY=@CQTY,PQTY=@PQTY,QTY=@QTY,RATE=@RATE,AMOUNT=@AMOUNT,DISCRT=@DISCRT,DISCAMT=@DISCAMT," +
                    "NETAMT=@NETAMT,UPDATEUSERID=@UPDATEUSERID,UPDUSERPC=@UPDUSERPC,UPDTIME=@UPDTIME,UPDIPADDRESS=@UPDIPADDRESS where TRANSTP=@TRANSTP and " +
                    "TRANSMY=@TRANSMY and TRANSNO=@TRANSNO and TRANSSL=@TRANSSL";

                cmd.Parameters.Clear();
                cmd.Parameters.Add("@INVREFNO", SqlDbType.NVarChar).Value = ob.InvRefNo;
                // cmd.Parameters.Add("@STOREFR", SqlDbType.NVarChar).Value = ob.StoreFR;
                cmd.Parameters.Add("@STORETO", SqlDbType.NVarChar).Value = ob.To;
                cmd.Parameters.Add("@PSID", SqlDbType.NVarChar).Value = ob.PsId;
                cmd.Parameters.Add("@REMARKS", SqlDbType.NVarChar).Value = ob.Remarks;
                cmd.Parameters.Add("@CATID", SqlDbType.NVarChar).Value = ob.CatID;
                cmd.Parameters.Add("@ITEMID", SqlDbType.NVarChar).Value = ob.ItemID;
                cmd.Parameters.Add("@UNITTP", SqlDbType.NVarChar).Value = ob.UnitTP;
                cmd.Parameters.Add("@CPQTY", SqlDbType.Decimal).Value = ob.CpQTY;
                cmd.Parameters.Add("@CQTY", SqlDbType.Decimal).Value = ob.CQTY;
                cmd.Parameters.Add("@PQTY", SqlDbType.Decimal).Value = ob.PQTY;
                cmd.Parameters.Add("@QTY", SqlDbType.Decimal).Value = ob.Qty;
                cmd.Parameters.Add("@RATE", SqlDbType.Decimal).Value = ob.Rate;
                cmd.Parameters.Add("@AMOUNT", SqlDbType.Decimal).Value = ob.Amount;
                cmd.Parameters.Add("@DISCRT", SqlDbType.Decimal).Value = ob.disCountRT;
                cmd.Parameters.Add("@DISCAMT", SqlDbType.Decimal).Value = ob.disAmount;
                cmd.Parameters.Add("@NETAMT", SqlDbType.Decimal).Value = ob.NetAmt;
                cmd.Parameters.Add("@UPDATEUSERID", SqlDbType.NVarChar).Value = ob.UserID;
                cmd.Parameters.Add("@UPDUSERPC", SqlDbType.NVarChar).Value = ob.UPDUserPC;
                cmd.Parameters.Add("@UPDTIME", SqlDbType.SmallDateTime).Value = ob.UpdTime;
                cmd.Parameters.Add("@UPDIPADDRESS", SqlDbType.NVarChar).Value = ob.UPDIpaddress;
                cmd.Parameters.Add("@TRANSTP", SqlDbType.NVarChar).Value = ob.TransTP;
                cmd.Parameters.Add("@TRANSMY", SqlDbType.NVarChar).Value = ob.MonthYear;
                //cmd.Parameters.Add("@TRANSDT", SqlDbType.SmallDateTime).Value = ob.TrDt;
                cmd.Parameters.Add("@TRANSNO", SqlDbType.BigInt).Value = ob.TransNo;
                cmd.Parameters.Add("@TRANSSL", SqlDbType.NVarChar).Value = ob.TransSL;

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
        public string GridUPD_RetSTK_TransMST(StockInterface ob)
        {
            string s = "";
            SqlTransaction tran = null;
            try
            {
                if (con.State != ConnectionState.Open)
                    con.Open();
                tran = con.BeginTransaction();
                //SqlCommand cmd1 = new SqlCommand("update STK_TRANSMST set INVREFNO = '" + txtSLMNo_Ret.Text + "', STORETO='" + txtSlFr_Ret.Text + "', PSID='" + txtPID_Ret.Text + "', REMARKS = '" + txtRemarks_Ret.Text + "', " +
                //      " USERID = '" + userName + "' where TRANSTP = 'IRTS'  and TRANSMY='" + lblMy_Ret.Text + "' and TRANSDT='" + TrDate + "' and TRANSNO = " + TransNo + "", conn);

                cmd.CommandText = "update STK_TRANS set INVREFNO=@INVREFNO,STORETO=@STORETO,PSID=@PSID,REMARKS=@REMARKS," +
                    "UPDATEUSERID=@UPDATEUSERID,UPDUSERPC=@UPDUSERPC,UPDTIME=@UPDTIME,UPDIPADDRESS=@UPDIPADDRESS where TRANSTP=@TRANSTP and " +
                    "TRANSMY=@TRANSMY and TRANSDT=@TRANSDT and TRANSNO=@TRANSNO";

                cmd.Parameters.Clear();
                cmd.Parameters.Add("@INVREFNO", SqlDbType.NVarChar).Value = ob.InvRefNo;
                // cmd.Parameters.Add("@STOREFR", SqlDbType.NVarChar).Value = ob.StoreFR;
                cmd.Parameters.Add("@STORETO", SqlDbType.NVarChar).Value = ob.To;
                cmd.Parameters.Add("@PSID", SqlDbType.NVarChar).Value = ob.PsId;
                cmd.Parameters.Add("@REMARKS", SqlDbType.NVarChar).Value = ob.Remarks;
                cmd.Parameters.Add("@UPDATEUSERID", SqlDbType.NVarChar).Value = ob.UserID;
                cmd.Parameters.Add("@UPDUSERPC", SqlDbType.NVarChar).Value = ob.UPDUserPC;
                cmd.Parameters.Add("@UPDTIME", SqlDbType.SmallDateTime).Value = ob.UpdTime;
                cmd.Parameters.Add("@UPDIPADDRESS", SqlDbType.NVarChar).Value = ob.UPDIpaddress;
                cmd.Parameters.Add("@TRANSTP", SqlDbType.NVarChar).Value = ob.TransTP;
                cmd.Parameters.Add("@TRANSMY", SqlDbType.NVarChar).Value = ob.MonthYear;
                cmd.Parameters.Add("@TRANSDT", SqlDbType.SmallDateTime).Value = ob.TrDt;
                cmd.Parameters.Add("@TRANSNO", SqlDbType.BigInt).Value = ob.TransNo;


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


        public string insert_STK_ITEMMST(StockInterface ob)
        {

            string s = "";
            SqlTransaction tran = null;
            try
            {
                if (con.State != ConnectionState.Open)
                    con.Open();
                tran = con.BeginTransaction();
                cmd.CommandText = @"INSERT INTO STK_ITEMMST ( COMPID, CATID, CATNM, CATNMB, USERPC, USERID, INTIME, IPADDRESS, INSLTUDE) 
                values( @COMPID, @CATID, @CATNM, @CATNMB, @USERPC, @USERID, @INTIME, @IPADDRESS, '')";
                cmd.Parameters.Clear();
                cmd.Parameters.Add("@COMPID", SqlDbType.NVarChar).Value = ob.CompanyId;
                cmd.Parameters.Add("@CATID", SqlDbType.NVarChar).Value = ob.SubID;
                cmd.Parameters.Add("@CATNM", SqlDbType.NVarChar).Value = ob.SubNM;
                cmd.Parameters.Add("@CATNMB", SqlDbType.NVarChar).Value = ob.SubNMBan;

                cmd.Parameters.Add("@INTIME", SqlDbType.SmallDateTime).Value = ob.ITime;
                cmd.Parameters.Add("@USERPC", SqlDbType.NVarChar).Value = ob.UserPC;
                cmd.Parameters.Add("@USERID", SqlDbType.NVarChar).Value = ob.UserID;
                cmd.Parameters.Add("@IPADDRESS", SqlDbType.NVarChar).Value = ob.Ipaddress;
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


        public string update_STK_ITEMMST(StockInterface ob)
        {
            //update STK_ITEMMST set SUBNM=@SUBNM where SUBID=@SUBID"
            string s = "";
            SqlTransaction tran = null;
            try
            {
                if (con.State != ConnectionState.Open)
                    con.Open();
                tran = con.BeginTransaction();
                cmd.CommandText = "update STK_ITEMMST set CATNM=@CATNM, CATNMB=@CATNMB where CATID=@CATID AND COMPID=@COMPID";
                cmd.Parameters.Clear();
                cmd.Parameters.Add("@CATNM", SqlDbType.NVarChar).Value = ob.SubNM;
                cmd.Parameters.Add("@CATNMB", SqlDbType.NVarChar).Value = ob.SubNMBan;

                cmd.Parameters.Add("@CATID", SqlDbType.NVarChar).Value = ob.SubID;
                cmd.Parameters.Add("@COMPID", SqlDbType.NVarChar).Value = ob.CompanyId;
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

        public string insert_STK_ITEM(StockInterface ob)
        {
            string s = "";
            SqlTransaction tran = null;
            try
            {
                if (con.State != ConnectionState.Open)
                    con.Open();
                tran = con.BeginTransaction();
                cmd.CommandText = @"insert into STK_ITEM (COMPID, CATID, ITEMID, ITEMNM, ITEMNMB, MUNIT, 
                BUYRT, SALRT, IMAGEPATH,STATUS, USERPC, USERID, INTIME, IPADDRESS, INSLTUDE) 
                values(@COMPID, @CATID, @ITEMID, @ITEMNM, @ITEMNMB, @MUNIT, 
                @BUYRT, @SALRT, @IMAGEPATH,@STATUS, @USERPC, @USERID, @INTIME, @IPADDRESS, '')";
                cmd.Parameters.Clear();
                cmd.Parameters.Add("@COMPID", SqlDbType.NVarChar).Value = ob.CompanyId;
                cmd.Parameters.Add("@CATID", SqlDbType.NVarChar).Value = ob.SubID;
                cmd.Parameters.Add("@ITEMID", SqlDbType.NVarChar).Value = ob.ItemID;

                cmd.Parameters.Add("@ITEMNM", SqlDbType.NVarChar).Value = ob.ItemNM;
                cmd.Parameters.Add("@ITEMNMB", SqlDbType.NVarChar).Value = ob.ItemNMBangla;
                cmd.Parameters.Add("@MUNIT", SqlDbType.NVarChar).Value = ob.UnitTP;
                cmd.Parameters.Add("@BUYRT", SqlDbType.Decimal).Value = ob.BuyRT;
                cmd.Parameters.Add("@SALRT", SqlDbType.Decimal).Value = ob.SaleRT;
                cmd.Parameters.Add("@IMAGEPATH", SqlDbType.NVarChar).Value = ob.ImagePath;
                cmd.Parameters.Add("@STATUS", SqlDbType.NVarChar).Value = ob.Status;

                cmd.Parameters.Add("@INTIME", SqlDbType.SmallDateTime).Value = ob.ITime;
                cmd.Parameters.Add("@USERPC", SqlDbType.NVarChar).Value = ob.UserPC;
                cmd.Parameters.Add("@USERID", SqlDbType.NVarChar).Value = ob.UserID;
                cmd.Parameters.Add("@IPADDRESS", SqlDbType.NVarChar).Value = ob.Ipaddress;
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

        public string update_STK_ITEM(StockInterface ob)
        {
            string s = "";
            SqlTransaction tran = null;
            try
            {
                if (con.State != ConnectionState.Open)
                    con.Open();
                tran = con.BeginTransaction();
                cmd.CommandText = @"UPDATE STK_ITEM SET ITEMNM=@ITEMNM, ITEMNMB=@ITEMNMB, MUNIT=@MUNIT, BUYRT=@BUYRT, 
                SALRT=@SALRT,STATUS=@STATUS, UPDUSERPC=@UPDUSERPC, UPDINTIME=@UPDINTIME, UPDUSERID=@UPDUSERID, UPDIPADDRESS=@UPDIPADDRESS, 
                UPDLTUDE='', IMAGEPATH=@IMAGEPATH WHERE CATID=@CATID and ITEMID=@ITEMID";
                cmd.Parameters.Clear();
                cmd.Parameters.Add("@CATID", SqlDbType.NVarChar).Value = ob.SubID;
                cmd.Parameters.Add("@ITEMID", SqlDbType.NVarChar).Value = ob.ItemID;

                cmd.Parameters.Add("@ITEMNM", SqlDbType.NVarChar).Value = ob.ItemNM;
                cmd.Parameters.Add("@ITEMNMB", SqlDbType.NVarChar).Value = ob.ItemNMBangla;
                cmd.Parameters.Add("@MUNIT", SqlDbType.NVarChar).Value = ob.UnitTP;

                cmd.Parameters.Add("@IMAGEPATH", SqlDbType.NVarChar).Value = ob.ImagePath;


                cmd.Parameters.Add("@BUYRT", SqlDbType.Decimal).Value = ob.BuyRT;
                cmd.Parameters.Add("@SALRT", SqlDbType.Decimal).Value = ob.SaleRT;

                cmd.Parameters.Add("@STATUS", SqlDbType.NVarChar).Value = ob.Status;

                cmd.Parameters.Add("@UPDINTIME", SqlDbType.SmallDateTime).Value = ob.UpdTime;
                cmd.Parameters.Add("@UPDUSERPC", SqlDbType.NVarChar).Value = ob.UPDUserPC;
                cmd.Parameters.Add("@UPDUSERID", SqlDbType.NVarChar).Value = ob.UpdUserID;
                cmd.Parameters.Add("@UPDIPADDRESS", SqlDbType.NVarChar).Value = ob.UPDIpaddress;
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
        public string update_STK_ITEMWithOutImage(StockInterface ob)
        {
            string s = "";
            SqlTransaction tran = null;
            try
            {
                if (con.State != ConnectionState.Open)
                    con.Open();
                tran = con.BeginTransaction();
                cmd.CommandText = @"UPDATE STK_ITEM SET ITEMNM=@ITEMNM, ITEMNMB=@ITEMNMB, MUNIT=@MUNIT,STATUS=@STATUS, BUYRT=@BUYRT, 
                SALRT=@SALRT, UPDUSERPC=@UPDUSERPC, UPDINTIME=@UPDINTIME, UPDUSERID=@UPDUSERID, UPDIPADDRESS=@UPDIPADDRESS, 
                UPDLTUDE='' WHERE CATID=@CATID and ITEMID=@ITEMID";
                cmd.Parameters.Clear();
                cmd.Parameters.Add("@CATID", SqlDbType.NVarChar).Value = ob.SubID;
                cmd.Parameters.Add("@ITEMID", SqlDbType.NVarChar).Value = ob.ItemID;

                cmd.Parameters.Add("@ITEMNM", SqlDbType.NVarChar).Value = ob.ItemNM;
                cmd.Parameters.Add("@ITEMNMB", SqlDbType.NVarChar).Value = ob.ItemNMBangla;
                cmd.Parameters.Add("@MUNIT", SqlDbType.NVarChar).Value = ob.UnitTP;
                cmd.Parameters.Add("@STATUS", SqlDbType.NVarChar).Value = ob.Status;

                cmd.Parameters.Add("@BUYRT", SqlDbType.Decimal).Value = ob.BuyRT;
                cmd.Parameters.Add("@SALRT", SqlDbType.Decimal).Value = ob.SaleRT;

                cmd.Parameters.Add("@UPDINTIME", SqlDbType.SmallDateTime).Value = ob.UpdTime;
                cmd.Parameters.Add("@UPDUSERPC", SqlDbType.NVarChar).Value = ob.UPDUserPC;
                cmd.Parameters.Add("@UPDUSERID", SqlDbType.NVarChar).Value = ob.UpdUserID;
                cmd.Parameters.Add("@UPDIPADDRESS", SqlDbType.NVarChar).Value = ob.UPDIpaddress;
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


        public string update_STK_ITEMNEW(StockInterface ob)
        {
            string s = "";
            SqlTransaction tran = null;
            try
            {
                if (con.State != ConnectionState.Open)
                    con.Open();
                tran = con.BeginTransaction();
                cmd.CommandText = "update STK_ITEM set ITEMCD =@ITEMCD, ITEMNM =@ITEMNM," +
                        "BRAND =@BRAND, UNIT=@UNIT,BUYRT=@BUYRT,SALERT=@SALERT,PQTY=@PQTY,MINSQTY=@MINSQTY," +
                        "UPDUSERID=@UPDUSERID, REFTP=@REFTP, REFNO=@REFNO where SUBID=@SUBID and ITEMID=@ITEMID";
                cmd.Parameters.Clear();
                cmd.Parameters.Add("@SUBID", SqlDbType.NVarChar).Value = ob.SubID;
                cmd.Parameters.Add("@ITEMID", SqlDbType.NVarChar).Value = ob.ItemID;
                cmd.Parameters.Add("@ITEMCD", SqlDbType.NVarChar).Value = ob.ItemID;
                cmd.Parameters.Add("@ITEMNM", SqlDbType.NVarChar).Value = ob.ItemNM;

                cmd.Parameters.Add("@BRAND", SqlDbType.NVarChar).Value = ob.Brand;
                cmd.Parameters.Add("@UNIT", SqlDbType.NVarChar).Value = ob.UnitTP;
                cmd.Parameters.Add("@BUYRT", SqlDbType.Decimal).Value = ob.BuyRT;
                cmd.Parameters.Add("@SALERT", SqlDbType.Decimal).Value = ob.SaleRT;

                cmd.Parameters.Add("@PQTY", SqlDbType.Decimal).Value = ob.PQTY;
                cmd.Parameters.Add("@MINSQTY", SqlDbType.Decimal).Value = ob.MinStkQty;
                cmd.Parameters.Add("@REFTP", SqlDbType.NVarChar).Value = ob.RefferenceType;
                cmd.Parameters.Add("@REFNO", SqlDbType.NVarChar).Value = ob.RefferenceNo;

                cmd.Parameters.Add("@UPDINTIME", SqlDbType.SmallDateTime).Value = ob.UpdTime;
                cmd.Parameters.Add("@UPDUSERPC", SqlDbType.NVarChar).Value = ob.UPDUserPC;
                cmd.Parameters.Add("@UPDUSERID", SqlDbType.NVarChar).Value = ob.UpdUserID;
                cmd.Parameters.Add("@UPDIPADDRESS", SqlDbType.NVarChar).Value = ob.UPDIpaddress;
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

        public string insert_STK_DISCDP(StockInterface ob)
        {

            // string insret = "insert into STK_DISCDP (ITEMID,DISCRT,EFDT,ETDT,USERPC,USERID,INTIME,IPADDRESS)" +
            //"VALUES(@ITEMID,@DISCRT,@EFDT,@ETDT,@USERPC,@USERID,@INTIME,@IPADDRESS)";
            string s = "";
            SqlTransaction tran = null;
            try
            {
                if (con.State != ConnectionState.Open)
                    con.Open();
                tran = con.BeginTransaction();
                cmd.CommandText = "insert into STK_DISCDP (ITEMID,DISCRT,EFDT,ETDT,USERPC,USERID,INTIME,IPADDRESS)" +
                    "VALUES(@ITEMID,@DISCRT,@EFDT,@ETDT,@USERPC,@USERID,@INTIME,@IPADDRESS)";
                cmd.Parameters.Clear();
                cmd.Parameters.Add("@ITEMID", SqlDbType.NVarChar).Value = ob.ItemID;
                cmd.Parameters.Add("@DISCRT", SqlDbType.Decimal).Value = ob.disCountRT;
                cmd.Parameters.Add("@EFDT", SqlDbType.SmallDateTime).Value = ob.Efdt;
                // cmd.Parameters.Add("@STOREFR", SqlDbType.NVarChar).Value = ob.StoreFR;
                cmd.Parameters.Add("@ETDT", SqlDbType.SmallDateTime).Value = ob.Etdt;

                cmd.Parameters.Add("@USERPC", SqlDbType.NVarChar).Value = ob.UserPC;
                cmd.Parameters.Add("@USERID", SqlDbType.NVarChar).Value = ob.UserID;
                cmd.Parameters.Add("@INTIME", SqlDbType.SmallDateTime).Value = ob.ITime;
                // cmd.Parameters.Add("@STOREFR", SqlDbType.NVarChar).Value = ob.StoreFR;
                cmd.Parameters.Add("@IPADDRESS", SqlDbType.NVarChar).Value = ob.Ipaddress;


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


        //Collection From Sayem vai.
        //Collection From Sayem vai.
        //Collection From Sayem vai.
        //Collection From Sayem vai.



        public string UpdatePurchaseSTK_TRANSCS(StockInterface ob)
        {
            string P = "";
            SqlTransaction tran = null;
            try
            {
                string userName = HttpContext.Current.Session["UserName"].ToString();
                if (con.State != ConnectionState.Open)
                    con.Open();
                tran = con.BeginTransaction();
                cmd.CommandType = CommandType.Text;

                cmd.CommandText = "update STK_TRANSCS set INVREFNO =@INVREFNO , STORETO=@STORETO , PSID=@PSID , LCTP =@LCTP , LCID=@LCID , REMARKS =@REMARKS , CATID =@CATID , ITEMID =@ITEMID , UNITTP =@UNITTP , CPQTY =@CPQTY , CQTY =@CQTY , PQTY =@PQTY , " +
                              " QTY =@QTY , RATE =@RATE , AMOUNT =@AMOUNT , UPDUSERPC=@UPDUSERPC , UPDATEUSERID =@UPDATEUSERID , UPDTIME=@UPDTIME , UPDIPADDRESS=@UPDIPADDRESS where TRANSTP = 'BUY' and TRANSMY=@TRANSMY and TRANSDT=@TRANSDT and TRANSNO =@TRANSNO and TRANSSL =@TRANSSL ";
                cmd.Parameters.Clear();
                cmd.Parameters.Add("@INVREFNO", SqlDbType.NVarChar).Value = ob.InvRefNo;
                cmd.Parameters.Add("@STORETO", SqlDbType.NVarChar).Value = ob.To;
                cmd.Parameters.Add("@PSID", SqlDbType.NVarChar).Value = ob.PsId;
                cmd.Parameters.Add("@LCTP", SqlDbType.NVarChar).Value = ob.LcTp;
                cmd.Parameters.Add("@LCID", SqlDbType.NVarChar).Value = ob.LcId;
                // cmd.Parameters.Add("@LCDATE", SqlDbType.Date).Value = ob.LcDt;
                cmd.Parameters.Add("@REMARKS", SqlDbType.NVarChar).Value = ob.Remarks;
                cmd.Parameters.Add("@CATID", SqlDbType.NVarChar).Value = ob.CatID;
                cmd.Parameters.Add("@ITEMID", SqlDbType.NVarChar).Value = ob.ItemID;
                cmd.Parameters.Add("@UNITTP", SqlDbType.NVarChar).Value = ob.UnitTP;
                cmd.Parameters.Add("@CPQTY", SqlDbType.Decimal).Value = ob.CpQTY;
                cmd.Parameters.Add("@CQTY", SqlDbType.Decimal).Value = ob.CQTY;
                cmd.Parameters.Add("@PQTY", SqlDbType.Decimal).Value = ob.PQTY;
                cmd.Parameters.Add("@QTY", SqlDbType.Decimal).Value = ob.Qty;
                cmd.Parameters.Add("@RATE", SqlDbType.Decimal).Value = ob.Rate;
                cmd.Parameters.Add("@AMOUNT", SqlDbType.Decimal).Value = ob.Amount;
                cmd.Parameters.Add("@UPDUSERPC", SqlDbType.NVarChar).Value = ob.UPDUserPC;
                cmd.Parameters.Add("@UPDATEUSERID", SqlDbType.NVarChar).Value = ob.UserID;
                cmd.Parameters.Add("@UPDTIME", SqlDbType.DateTime).Value = ob.UpdTime;
                cmd.Parameters.Add("@UPDIPADDRESS", SqlDbType.NVarChar).Value = ob.UPDIpaddress;
                cmd.Parameters.Add("@TRANSMY", SqlDbType.NVarChar).Value = ob.MonthYear;
                cmd.Parameters.Add("@TRANSDT", SqlDbType.DateTime).Value = ob.TrDt;
                cmd.Parameters.Add("@TRANSNO", SqlDbType.BigInt).Value = ob.TransNo;
                cmd.Parameters.Add("@TRANSSL", SqlDbType.NVarChar).Value = ob.TransSL;

                cmd.Transaction = tran;
                cmd.ExecuteNonQuery();
                tran.Commit();
                if (con.State != ConnectionState.Closed)
                    con.Close();
            }
            catch (Exception ex)
            {
                tran.Rollback();
                P = ex.Message;
            }
            return P;

        }


        public string UpdatePurchaseSTK_TRANSMSTCS(StockInterface ob)
        {
            string P = "";
            SqlTransaction tran = null;
            try
            {
                string userName = HttpContext.Current.Session["UserName"].ToString();
                if (con.State != ConnectionState.Open)
                    con.Open();
                tran = con.BeginTransaction();
                cmd.CommandType = CommandType.Text;

                cmd.CommandText = "update STK_TRANSMSTCS set INVREFNO =@INVREFNO , STORETO=@STORETO , PSID=@PSID , LCTP =@LCTP , LCID=@LCID , REMARKS =@REMARKS ," +
                              " UPDUSERPC=@UPDUSERPC , UPDATEUSERID =@UPDATEUSERID , UPDTIME=@UPDTIME , UPDIPADDRESS=@UPDIPADDRESS where TRANSTP = 'BUY' and TRANSMY=@TRANSMY and TRANSDT=@TRANSDT and TRANSNO =@TRANSNO ";
                cmd.Parameters.Clear();
                cmd.Parameters.Add("@INVREFNO", SqlDbType.NVarChar).Value = ob.InvRefNo;
                cmd.Parameters.Add("@STORETO", SqlDbType.NVarChar).Value = ob.To;
                cmd.Parameters.Add("@PSID", SqlDbType.NVarChar).Value = ob.PsId;
                cmd.Parameters.Add("@LCTP", SqlDbType.NVarChar).Value = ob.LcTp;
                cmd.Parameters.Add("@LCID", SqlDbType.NVarChar).Value = ob.LcId;

                cmd.Parameters.Add("@TRANSMY", SqlDbType.NVarChar).Value = ob.MonthYear;
                cmd.Parameters.Add("@TRANSDT", SqlDbType.DateTime).Value = ob.TrDt;
                cmd.Parameters.Add("@TRANSNO", SqlDbType.BigInt).Value = ob.TransNo;
                //cmd.Parameters.Add("@LCDATE", SqlDbType.Date).Value = ob.LcDt;
                cmd.Parameters.Add("@REMARKS", SqlDbType.NVarChar).Value = ob.Remarks;



                cmd.Parameters.Add("@UPDUSERPC", SqlDbType.NVarChar).Value = ob.UPDUserPC;
                cmd.Parameters.Add("@UPDATEUSERID", SqlDbType.NVarChar).Value = ob.UserID;
                cmd.Parameters.Add("@UPDTIME", SqlDbType.DateTime).Value = ob.UpdTime;
                cmd.Parameters.Add("@UPDIPADDRESS", SqlDbType.NVarChar).Value = ob.UPDIpaddress;


                cmd.Transaction = tran;
                cmd.ExecuteNonQuery();
                tran.Commit();
                if (con.State != ConnectionState.Closed)
                    con.Close();
            }
            catch (Exception ex)
            {
                tran.Rollback();
                P = ex.Message;
            }
            return P;

        }



        public string InsertParchaseSTK_TRANSCS(StockInterface ob)
        {
            string s = "";
            SqlTransaction tran = null;
            try
            {
                if (con.State != ConnectionState.Open)
                    con.Open();
                tran = con.BeginTransaction();
                cmd.CommandType = CommandType.Text;
                cmd.CommandText = @"insert into STK_TRANSCS ( TRANSTP, TRANSDT, TRANSMY, TRANSNO, INVREFNO, STOREFR, STORETO, PSID, LCTP, LCID,
                                REMARKS, TRANSSL, CATID, ITEMID, UNITTP, CPQTY, CQTY, PQTY, QTY, RATE, AMOUNT, USERPC, INTIME, USERID, IPADDRESS ) 
                  values(@TRANSTP, @TRANSDT, @TRANSMY, @TRANSNO, @INVREFNO, @STOREFR, @STORETO, @PSID, @LCTP, @LCID,
                                @REMARKS, @TRANSSL, @CATID, @ITEMID, @UNITTP, @CPQTY, @CQTY, @PQTY, @QTY, @RATE, @AMOUNT, @USERPC, @INTIME, @USERID, @IPADDRESS)";
                //comm = new SqlCommand(query, conn);
                cmd.Parameters.Clear();
                cmd.Parameters.Add("@TRANSTP", SqlDbType.NVarChar).Value = ob.TransTP;
                cmd.Parameters.Add("@TRANSDT", SqlDbType.SmallDateTime).Value = ob.TrDt;
                cmd.Parameters.Add("@TRANSMY", SqlDbType.NVarChar).Value = ob.MonthYear;
                cmd.Parameters.Add("@TRANSNO", SqlDbType.BigInt).Value = ob.TransNo;
                cmd.Parameters.Add("@INVREFNO", SqlDbType.NVarChar).Value = ob.InvRefNo;
                cmd.Parameters.Add("@STOREFR", SqlDbType.NVarChar).Value = ob.StoreFR;
                cmd.Parameters.Add("@STORETO", SqlDbType.NVarChar).Value = ob.To;
                cmd.Parameters.Add("@PSID", SqlDbType.NVarChar).Value = ob.PsId;
                cmd.Parameters.Add("@LCTP", SqlDbType.NVarChar).Value = ob.LcTp;
                cmd.Parameters.Add("@LCID", SqlDbType.NVarChar).Value = ob.LcId;
                //cmd.Parameters.Add("@LCDATE", SqlDbType.Date).Value = ob.LcDt;
                cmd.Parameters.Add("@REMARKS", SqlDbType.NVarChar).Value = ob.Remarks;
                cmd.Parameters.Add("@TRANSSL", SqlDbType.NVarChar).Value = ob.TransSL;
                cmd.Parameters.Add("@CATID", SqlDbType.NVarChar).Value = ob.CatID;
                cmd.Parameters.Add("@ITEMID", SqlDbType.NVarChar).Value = ob.ItemID;
                cmd.Parameters.Add("@UNITTP", SqlDbType.NVarChar).Value = ob.UnitTP;
                cmd.Parameters.Add("@CPQTY", SqlDbType.Decimal).Value = ob.CpQTY;
                cmd.Parameters.Add("@CQTY", SqlDbType.Decimal).Value = ob.CQTY;
                cmd.Parameters.Add("@PQTY", SqlDbType.Decimal).Value = ob.PQTY;
                cmd.Parameters.Add("@QTY", SqlDbType.Decimal).Value = ob.Qty;
                cmd.Parameters.Add("@RATE", SqlDbType.Decimal).Value = ob.Rate;
                cmd.Parameters.Add("@AMOUNT", SqlDbType.Decimal).Value = ob.Amount;
                cmd.Parameters.Add("@USERPC", SqlDbType.NVarChar).Value = ob.UserPC;
                cmd.Parameters.Add("@INTIME", SqlDbType.SmallDateTime).Value = ob.ITime;
                cmd.Parameters.Add("@USERID", SqlDbType.NVarChar).Value = ob.UserID;
                cmd.Parameters.Add("@IPADDRESS", SqlDbType.NVarChar).Value = ob.Ipaddress;
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

        public string InsertParchaseSTK_TRANSMSTCS(StockInterface ob)
        {
            string s = "";
            SqlTransaction tran = null;
            try
            {
                if (con.State != ConnectionState.Open)
                    con.Open();
                tran = con.BeginTransaction();
                cmd.CommandType = CommandType.Text;
                cmd.CommandText = @"insert into STK_TRANSMSTCS ( TRANSTP, TRANSDT, TRANSMY, TRANSNO, INVREFNO, STOREFR, STORETO, PSID, LCTP, LCID,  REMARKS, USERPC, INTIME, USERID, IPADDRESS) 
                  values(@TRANSTP, @TRANSDT, @TRANSMY, @TRANSNO, @INVREFNO, @STOREFR, @STORETO, @PSID, @LCTP, @LCID, @REMARKS,@USERPC, @INTIME, @USERID, @IPADDRESS)";
                //comm = new SqlCommand(query, conn);
                cmd.Parameters.Clear();
                cmd.Parameters.Add("@TRANSTP", SqlDbType.NVarChar).Value = ob.TransTP;
                cmd.Parameters.Add("@TRANSDT", SqlDbType.SmallDateTime).Value = ob.TrDt;
                cmd.Parameters.Add("@TRANSMY", SqlDbType.NVarChar).Value = ob.MonthYear;
                cmd.Parameters.Add("@TRANSNO", SqlDbType.BigInt).Value = ob.TransNo;
                cmd.Parameters.Add("@INVREFNO", SqlDbType.NVarChar).Value = ob.InvRefNo;
                cmd.Parameters.Add("@STOREFR", SqlDbType.NVarChar).Value = ob.StoreFR;
                cmd.Parameters.Add("@STORETO", SqlDbType.NVarChar).Value = ob.To;
                cmd.Parameters.Add("@PSID", SqlDbType.NVarChar).Value = ob.PsId;
                cmd.Parameters.Add("@LCTP", SqlDbType.NVarChar).Value = ob.LcTp;
                cmd.Parameters.Add("@LCID", SqlDbType.NVarChar).Value = ob.LcId;
                //cmd.Parameters.Add("@LCDATE", SqlDbType.Date).Value = ob.LcDt;
                cmd.Parameters.Add("@REMARKS", SqlDbType.NVarChar).Value = ob.Remarks;
                cmd.Parameters.Add("@USERPC", SqlDbType.NVarChar).Value = ob.UserPC;
                cmd.Parameters.Add("@INTIME", SqlDbType.SmallDateTime).Value = ob.ITime;
                cmd.Parameters.Add("@USERID", SqlDbType.NVarChar).Value = ob.UserID;
                cmd.Parameters.Add("@IPADDRESS", SqlDbType.NVarChar).Value = ob.Ipaddress;
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



        public string InsertTransfrSTK_TRANSCS(StockInterface ob)
        {
            string s = "";
            SqlTransaction tran = null;
            try
            {
                if (con.State != ConnectionState.Open)
                    con.Open();
                tran = con.BeginTransaction();
                cmd.CommandType = CommandType.Text;
                cmd.CommandText = "insert into STK_TRANSCS (TRANSTP,TRANSDT,TRANSMY,TRANSNO,INVREFNO,STOREFR,STORETO,PSID,LCTP,LCID," +
                               " REMARKS,TRANSSL,CATID,ITEMID,UNITTP,CPQTY,CQTY,PQTY,QTY,RATE,AMOUNT,USERPC,INTIME,USERID,IPADDRESS)values" +
                                     "(@TRANSTP,@TRANSDT,@TRANSMY,@TRANSNO,@INVREFNO,@STOREFR,@STORETO,@PSID,@LCTP,@LCID," +
                               " @REMARKS,@TRANSSL,@CATID,@ITEMID,@UNITTP,@CPQTY,@CQTY,@PQTY,@QTY,@RATE,@AMOUNT,@USERPC,@INTIME,@USERID,@IPADDRESS)";
                //comm = new SqlCommand(query, conn);
                cmd.Parameters.Clear();
                cmd.Parameters.Add("@TRANSTP", SqlDbType.NVarChar).Value = ob.TransTP;
                cmd.Parameters.Add("@TRANSDT", SqlDbType.SmallDateTime).Value = ob.TrDt;
                cmd.Parameters.Add("@TRANSMY", SqlDbType.NVarChar).Value = ob.MonthYear;
                cmd.Parameters.Add("@TRANSNO", SqlDbType.BigInt).Value = ob.TransNo;
                cmd.Parameters.Add("@INVREFNO", SqlDbType.NVarChar).Value = ob.InvRefNo;
                cmd.Parameters.Add("@STOREFR", SqlDbType.NVarChar).Value = ob.StoreFR;
                cmd.Parameters.Add("@STORETO", SqlDbType.NVarChar).Value = ob.To;
                cmd.Parameters.Add("@PSID", SqlDbType.NVarChar).Value = ob.PsId;
                cmd.Parameters.Add("@LCTP", SqlDbType.NVarChar).Value = ob.LcTp;
                cmd.Parameters.Add("@LCID", SqlDbType.NVarChar).Value = ob.LcId;
                //cmd.Parameters.Add("@LCDATE", SqlDbType.Date).Value = ob.LcDt;
                cmd.Parameters.Add("@REMARKS", SqlDbType.NVarChar).Value = ob.Remarks;
                cmd.Parameters.Add("@TRANSSL", SqlDbType.NVarChar).Value = ob.TransSL;
                cmd.Parameters.Add("@CATID", SqlDbType.NVarChar).Value = ob.CatID;
                cmd.Parameters.Add("@ITEMID", SqlDbType.NVarChar).Value = ob.ItemID;
                cmd.Parameters.Add("@UNITTP", SqlDbType.NVarChar).Value = ob.UnitTP;
                cmd.Parameters.Add("@CPQTY", SqlDbType.Decimal).Value = ob.CpQTY;
                cmd.Parameters.Add("@CQTY", SqlDbType.Decimal).Value = ob.CQTY;
                cmd.Parameters.Add("@PQTY", SqlDbType.Decimal).Value = ob.PQTY;
                cmd.Parameters.Add("@QTY", SqlDbType.Decimal).Value = ob.Qty;
                cmd.Parameters.Add("@RATE", SqlDbType.Decimal).Value = ob.Rate;
                cmd.Parameters.Add("@AMOUNT", SqlDbType.Decimal).Value = ob.Amount;
                cmd.Parameters.Add("@USERPC", SqlDbType.NVarChar).Value = ob.UserPC;
                cmd.Parameters.Add("@INTIME", SqlDbType.SmallDateTime).Value = ob.ITime;
                cmd.Parameters.Add("@USERID", SqlDbType.NVarChar).Value = ob.UserID;
                cmd.Parameters.Add("@IPADDRESS", SqlDbType.NVarChar).Value = ob.Ipaddress;
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



        public string InsertTransfrSTK_TRANSMSTCS(StockInterface ob)
        {
            string s = "";
            SqlTransaction tran = null;
            try
            {
                if (con.State != ConnectionState.Open)
                    con.Open();
                tran = con.BeginTransaction();
                cmd.CommandType = CommandType.Text;

                cmd.CommandText = @"insert into STK_TRANSMSTCS (TRANSTP,TRANSDT,TRANSMY,TRANSNO,INVREFNO,STOREFR,STORETO,PSID,LCTP,LCID,
                                REMARKS,USERPC,INTIME,USERID,IPADDRESS ) 
                  values(@TRANSTP,@TRANSDT,@TRANSMY,@TRANSNO,@INVREFNO,@STOREFR,@STORETO,@PSID,@LCTP,@LCID,
                                @REMARKS,@USERPC,@INTIME,@USERID,@IPADDRESS)";
                //comm = new SqlCommand(query, conn);
                cmd.Parameters.Add("@TRANSTP", SqlDbType.NVarChar).Value = ob.TransTP;
                cmd.Parameters.Add("@TRANSDT", SqlDbType.SmallDateTime).Value = ob.TrDt;
                cmd.Parameters.Add("@TRANSMY", SqlDbType.NVarChar).Value = ob.MonthYear;
                cmd.Parameters.Add("@TRANSNO", SqlDbType.BigInt).Value = ob.TransNo;
                cmd.Parameters.Add("@INVREFNO", SqlDbType.NVarChar).Value = ob.InvRefNo;
                cmd.Parameters.Add("@STOREFR", SqlDbType.NVarChar).Value = ob.StoreFR;
                cmd.Parameters.Add("@STORETO", SqlDbType.NVarChar).Value = ob.To;
                cmd.Parameters.Add("@PSID", SqlDbType.NVarChar).Value = ob.PsId;
                cmd.Parameters.Add("@LCTP", SqlDbType.NVarChar).Value = ob.LcTp;
                cmd.Parameters.Add("@LCID", SqlDbType.NVarChar).Value = ob.LcId;
                //cmd.Parameters.Add("@TRANSSL", SqlDbType.NVarChar).Value = ob.TransSL;
                cmd.Parameters.Add("@REMARKS", SqlDbType.NVarChar).Value = ob.Remarks;
                cmd.Parameters.Add("@USERPC", SqlDbType.NVarChar).Value = ob.UserPC;
                cmd.Parameters.Add("@INTIME", SqlDbType.SmallDateTime).Value = ob.ITime;
                cmd.Parameters.Add("@USERID", SqlDbType.NVarChar).Value = ob.UserPC;
                cmd.Parameters.Add("@IPADDRESS", SqlDbType.NVarChar).Value = ob.Ipaddress;
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


        public string UPDTransfrSTK_TRANS(StockInterface ob)
        {
            string P = "";
            SqlTransaction tran = null;
            try
            {
                string userName = HttpContext.Current.Session["UserName"].ToString();
                if (con.State != ConnectionState.Open)
                    con.Open();
                tran = con.BeginTransaction();
                cmd.CommandType = CommandType.Text;
                cmd.CommandText = "update STK_TRANS set TRRTSNO =@TRRTSNO , UNITTO=@UNITTO, UNITFR=@UNITFR, ITEMID =@ITEMID , ITEMCD=@ITEMCD, " +
                              " QTY=@QTY , TRMODE=@TRMODE, ITEMIDFG=@ITEMIDFG, PRODQTY=@PRODQTY, RATE=@RATE , AMOUNT=@AMOUNT , UPDUSERPC=@UPDUSERPC , UPDATEUSERID=@UPDATEUSERID , UPDTIME=@UPDTIME , UPDIPADDRESS=@UPDIPADDRESS where TRANSTP = 'IISS' and TRANSDT=@TRANSDT and TRANSNO =@TRANSNO and ITEMSL =@ITEMSL ";
                cmd.Parameters.Clear();
                cmd.Parameters.Add("@TRRTSNO", SqlDbType.NVarChar).Value = ob.InvRefNo;
                cmd.Parameters.Add("@UNITTO", SqlDbType.NVarChar).Value = ob.To;
                cmd.Parameters.Add("@UNITFR", SqlDbType.NVarChar).Value = ob.StoreFR;
                //cmd.Parameters.Add("@CATID", SqlDbType.NVarChar).Value = ob.CatID;
                cmd.Parameters.Add("@ITEMID", SqlDbType.NVarChar).Value = ob.ItemID;
                cmd.Parameters.Add("@ITEMCD", SqlDbType.NVarChar).Value = ob.ItemCD;

                cmd.Parameters.Add("@TRMODE", SqlDbType.NVarChar).Value = ob.TranMode;
                cmd.Parameters.Add("@ITEMIDFG", SqlDbType.NVarChar).Value = ob.ItemFG;
                cmd.Parameters.Add("@PRODQTY", SqlDbType.Decimal).Value = ob.ProductionQty;

                //cmd.Parameters.Add("@UNITTP", SqlDbType.NVarChar).Value = ob.UnitTP;
                //cmd.Parameters.Add("@CPQTY", SqlDbType.Decimal).Value = ob.CpQTY;
                //cmd.Parameters.Add("@CQTY", SqlDbType.Decimal).Value = ob.CQTY;
                //cmd.Parameters.Add("@PQTY", SqlDbType.Decimal).Value = ob.PQTY;
                cmd.Parameters.Add("@QTY", SqlDbType.Decimal).Value = ob.Qty;
                cmd.Parameters.Add("@RATE", SqlDbType.Decimal).Value = ob.Rate;
                cmd.Parameters.Add("@AMOUNT", SqlDbType.Decimal).Value = ob.Amount;
                cmd.Parameters.Add("@UPDUSERPC", SqlDbType.NVarChar).Value = ob.UPDUserPC;
                cmd.Parameters.Add("@UPDATEUSERID", SqlDbType.NVarChar).Value = ob.UpdUserID;
                cmd.Parameters.Add("@UPDTIME", SqlDbType.SmallDateTime).Value = ob.UpdTime;
                cmd.Parameters.Add("@UPDIPADDRESS", SqlDbType.NVarChar).Value = ob.UPDIpaddress;
                //cmd.Parameters.Add("@TRANSMY", SqlDbType.NVarChar).Value = ob.MonthYear;
                cmd.Parameters.Add("@TRANSDT", SqlDbType.SmallDateTime).Value = ob.TrDt;
                cmd.Parameters.Add("@TRANSNO", SqlDbType.NVarChar).Value = ob.TransNo2;
                cmd.Parameters.Add("@ITEMSL", SqlDbType.NVarChar).Value = ob.TransSL;

                cmd.Transaction = tran;
                cmd.ExecuteNonQuery();
                tran.Commit();
                if (con.State != ConnectionState.Closed)
                    con.Close();
            }
            catch (Exception ex)
            {
                tran.Rollback();
                P = ex.Message;
            }
            return P;
        }
        public string UPDTransfrSTK_TRANSMSTCS(StockInterface ob)
        {
            string s = "";
            SqlTransaction tran = null;
            try
            {
                if (con.State != ConnectionState.Open)
                    con.Open();
                tran = con.BeginTransaction();
                cmd.CommandType = CommandType.Text;
                //SqlCommand cmd1 = new SqlCommand("update STK_TRANSMSTCS set INVREFNO = '" + txtMemo_Trans.Text + "', STOREFR = '" + txtTFrID_Trans.Text + "', STORETO='" + txtTToID_Trans.Text + "', REMARKS = '" + txtRemarks_Trans.Text + "', USERID = '" + userName + "'" +
                //          " where TRANSTP = 'ITRF' and TRANSMY='" + lblTMy.Text + "' and TRANSDT='" + TransDT + "' and TRANSNO = " + TransNo + "", conn);
                //cmd1.ExecuteNonQuery();
                cmd.CommandText = "update STK_TRANSMSTCS set INVREFNO=@INVREFNO,STOREFR=@STOREFR,STORETO=@STORETO,REMARKS=@REMARKS,UPDATEUSERID=@UPDATEUSERID, " +
                    "UPDUSERPC=@UPDUSERPC,UPDTIME=@UPDTIME,UPDIPADDRESS=@UPDIPADDRESS  where TRANSTP='ITRF' and TRANSMY=@TRANSMY and TRANSDT=@TRANSDT " +
                    "and TRANSNO=@TRANSNO";
                cmd.Parameters.Clear();
                cmd.Parameters.Add("@INVREFNO", SqlDbType.NVarChar).Value = ob.InvRefNo;
                cmd.Parameters.Add("@STOREFR", SqlDbType.NVarChar).Value = ob.StoreFR;
                cmd.Parameters.Add("@STORETO", SqlDbType.NVarChar).Value = ob.To;
                cmd.Parameters.Add("@REMARKS", SqlDbType.NVarChar).Value = ob.Remarks;
                cmd.Parameters.Add("@UPDATEUSERID", SqlDbType.NVarChar).Value = ob.UserID;
                cmd.Parameters.Add("@UPDUSERPC", SqlDbType.NVarChar).Value = ob.UPDUserPC;
                cmd.Parameters.Add("@UPDTIME", SqlDbType.SmallDateTime).Value = ob.UpdTime;
                cmd.Parameters.Add("@UPDIPADDRESS", SqlDbType.NVarChar).Value = ob.UPDIpaddress;
                //For Where clouse
                cmd.Parameters.Add("@TRANSMY", SqlDbType.NVarChar).Value = ob.MonthYear;
                cmd.Parameters.Add("@TRANSDT", SqlDbType.SmallDateTime).Value = ob.TrDt;
                cmd.Parameters.Add("@TRANSNO", SqlDbType.BigInt).Value = ob.TransNo;
                //cmd.Parameters.Add("@TRANSSL", SqlDbType.NVarChar).Value = ob.TransSL;

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

        public string GridUPD_RetSTK_TRANSCS(StockInterface ob)
        {
            string s = "";
            SqlTransaction tran = null;
            try
            {
                if (con.State != ConnectionState.Open)
                    con.Open();
                tran = con.BeginTransaction();
                cmd.CommandText = "update STK_TRANSCS set INVREFNO=@INVREFNO,STOREFR=@STOREFR,STORETO=@STORETO,PSID=@PSID,REMARKS=@REMARKS,CATID=@CATID," +
                    "ITEMID=@ITEMID,UNITTP=@UNITTP,CPQTY=@CPQTY,CQTY=@CQTY,PQTY=@PQTY,QTY=@QTY,RATE=@RATE,AMOUNT=@AMOUNT,DISCRT=@DISCRT,DISCAMT=@DISCAMT," +
                    "NETAMT=@NETAMT,UPDATEUSERID=@UPDATEUSERID,UPDUSERPC=@UPDUSERPC,UPDTIME=@UPDTIME,UPDIPADDRESS=@UPDIPADDRESS where TRANSTP=@TRANSTP and " +
                    "TRANSMY=@TRANSMY and TRANSNO=@TRANSNO and TRANSSL=@TRANSSL";

                cmd.Parameters.Clear();
                cmd.Parameters.Add("@INVREFNO", SqlDbType.NVarChar).Value = ob.InvRefNo;
                cmd.Parameters.Add("@STOREFR", SqlDbType.NVarChar).Value = ob.StoreFR;
                cmd.Parameters.Add("@STORETO", SqlDbType.NVarChar).Value = ob.To;
                cmd.Parameters.Add("@PSID", SqlDbType.NVarChar).Value = ob.PsId;
                cmd.Parameters.Add("@REMARKS", SqlDbType.NVarChar).Value = ob.Remarks;
                cmd.Parameters.Add("@CATID", SqlDbType.NVarChar).Value = ob.CatID;
                cmd.Parameters.Add("@ITEMID", SqlDbType.NVarChar).Value = ob.ItemID;
                cmd.Parameters.Add("@UNITTP", SqlDbType.NVarChar).Value = ob.UnitTP;
                cmd.Parameters.Add("@CPQTY", SqlDbType.Decimal).Value = ob.CpQTY;
                cmd.Parameters.Add("@CQTY", SqlDbType.Decimal).Value = ob.CQTY;
                cmd.Parameters.Add("@PQTY", SqlDbType.Decimal).Value = ob.PQTY;
                cmd.Parameters.Add("@QTY", SqlDbType.Decimal).Value = ob.Qty;
                cmd.Parameters.Add("@RATE", SqlDbType.Decimal).Value = ob.Rate;
                cmd.Parameters.Add("@AMOUNT", SqlDbType.Decimal).Value = ob.Amount;
                cmd.Parameters.Add("@DISCRT", SqlDbType.Decimal).Value = ob.disCountRT;
                cmd.Parameters.Add("@DISCAMT", SqlDbType.Decimal).Value = ob.disAmount;
                cmd.Parameters.Add("@NETAMT", SqlDbType.Decimal).Value = ob.NetAmt;
                cmd.Parameters.Add("@UPDATEUSERID", SqlDbType.NVarChar).Value = ob.UserID;
                cmd.Parameters.Add("@UPDUSERPC", SqlDbType.NVarChar).Value = ob.UPDUserPC;
                cmd.Parameters.Add("@UPDTIME", SqlDbType.SmallDateTime).Value = ob.UpdTime;
                cmd.Parameters.Add("@UPDIPADDRESS", SqlDbType.NVarChar).Value = ob.UPDIpaddress;
                cmd.Parameters.Add("@TRANSTP", SqlDbType.NVarChar).Value = ob.TransTP;
                cmd.Parameters.Add("@TRANSMY", SqlDbType.NVarChar).Value = ob.MonthYear;
                //cmd.Parameters.Add("@TRANSDT", SqlDbType.SmallDateTime).Value = ob.TrDt;
                cmd.Parameters.Add("@TRANSNO", SqlDbType.BigInt).Value = ob.TransNo;
                cmd.Parameters.Add("@TRANSSL", SqlDbType.NVarChar).Value = ob.TransSL;

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
        public string GridUPD_RetSTK_TRANSMSTCS(StockInterface ob)
        {
            string s = "";
            SqlTransaction tran = null;
            try
            {
                if (con.State != ConnectionState.Open)
                    con.Open();
                tran = con.BeginTransaction();
                //SqlCommand cmd1 = new SqlCommand("update STK_TRANSMSTCS set INVREFNO = '" + txtSLMNo_Ret.Text + "', STORETO='" + txtSlFr_Ret.Text + "', PSID='" + txtPID_Ret.Text + "', REMARKS = '" + txtRemarks_Ret.Text + "', " +
                //      " USERID = '" + userName + "' where TRANSTP = 'IRTS'  and TRANSMY='" + lblMy_Ret.Text + "' and TRANSDT='" + TrDate + "' and TRANSNO = " + TransNo + "", conn);

                cmd.CommandText = "update STK_TRANSCS set INVREFNO=@INVREFNO,STOREFR=@STOREFR,STORETO=@STORETO,PSID=@PSID,REMARKS=@REMARKS," +
                    "UPDATEUSERID=@UPDATEUSERID,UPDUSERPC=@UPDUSERPC,UPDTIME=@UPDTIME,UPDIPADDRESS=@UPDIPADDRESS where TRANSTP=@TRANSTP and " +
                    "TRANSMY=@TRANSMY and TRANSDT=@TRANSDT and TRANSNO=@TRANSNO";

                cmd.Parameters.Clear();
                cmd.Parameters.Add("@INVREFNO", SqlDbType.NVarChar).Value = ob.InvRefNo;
                cmd.Parameters.Add("@STOREFR", SqlDbType.NVarChar).Value = ob.StoreFR;
                cmd.Parameters.Add("@STORETO", SqlDbType.NVarChar).Value = ob.To;
                cmd.Parameters.Add("@PSID", SqlDbType.NVarChar).Value = ob.PsId;
                cmd.Parameters.Add("@REMARKS", SqlDbType.NVarChar).Value = ob.Remarks;
                cmd.Parameters.Add("@UPDATEUSERID", SqlDbType.NVarChar).Value = ob.UserID;
                cmd.Parameters.Add("@UPDUSERPC", SqlDbType.NVarChar).Value = ob.UPDUserPC;
                cmd.Parameters.Add("@UPDTIME", SqlDbType.SmallDateTime).Value = ob.UpdTime;
                cmd.Parameters.Add("@UPDIPADDRESS", SqlDbType.NVarChar).Value = ob.UPDIpaddress;
                cmd.Parameters.Add("@TRANSTP", SqlDbType.NVarChar).Value = ob.TransTP;
                cmd.Parameters.Add("@TRANSMY", SqlDbType.NVarChar).Value = ob.MonthYear;
                cmd.Parameters.Add("@TRANSDT", SqlDbType.SmallDateTime).Value = ob.TrDt;
                cmd.Parameters.Add("@TRANSNO", SqlDbType.BigInt).Value = ob.TransNo;


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


        public string UpdateReceiveBtnTRANS(StockInterface ob)
        {
            string P1 = "";
            SqlTransaction tran = null;
            try
            {
                string userName = HttpContext.Current.Session["UserName"].ToString();
                if (con.State != ConnectionState.Open)
                    con.Open();
                tran = con.BeginTransaction();
                cmd.CommandType = CommandType.Text;
                cmd.CommandText = "update STK_TRANSCS set INVREFNO =@INVREFNO, STORETO=@STORETO, REMARKS=@REMARKS, UPDATEUSERID=@USERID where TRANSTP = 'IRFG' and TRANSMY=@TRANSMY and TRANSDT=@TRANSDT and TRANSNO =@TRANSNO";

                cmd.Parameters.Clear();
                cmd.Parameters.Add("@INVREFNO", SqlDbType.NVarChar).Value = ob.InvRefNo;
                cmd.Parameters.Add("@STORETO", SqlDbType.NVarChar).Value = ob.ToId;
                //cmd.Parameters.Add("@PSID", SqlDbType.NVarChar).Value = ob.PsId;
                cmd.Parameters.Add("@REMARKS", SqlDbType.NVarChar).Value = ob.Remarks;
                cmd.Parameters.Add("@USERID", SqlDbType.NVarChar).Value = ob.UserID;
                cmd.Parameters.Add("@TRANSMY", SqlDbType.NVarChar).Value = ob.MonthYear;
                cmd.Parameters.Add("@TRANSDT", SqlDbType.DateTime).Value = ob.TrDt;
                cmd.Parameters.Add("@TRANSNO", SqlDbType.BigInt).Value = ob.TransNo;

                cmd.Transaction = tran;
                cmd.ExecuteNonQuery();
                tran.Commit();
                if (con.State != ConnectionState.Closed)
                    con.Close();
            }
            catch (Exception ex)
            {
                tran.Rollback();
                P1 = ex.Message;
            }
            return P1;
        }

        public string InsertReceiveSTK_TRANSCS(StockInterface ob)
        {
            string s = "";
            SqlTransaction tran = null;
            try
            {
                if (con.State != ConnectionState.Open)
                    con.Open();
                tran = con.BeginTransaction();
                cmd.CommandType = CommandType.Text;
                cmd.CommandText = @"insert into STK_TRANSCS ( TRANSTP, TRANSDT, TRANSMY, TRANSNO, INVREFNO, STOREFR, STORETO,
                                REMARKS, TRANSSL, CATID, ITEMID, UNITTP, CPQTY, CQTY, PQTY, QTY, RATE, AMOUNT, USERPC, INTIME, USERID, IPADDRESS ) 
                  values(@TRANSTP, @TRANSDT, @TRANSMY, @TRANSNO, @INVREFNO, @STOREFR, @STORETO,
                                @REMARKS, @TRANSSL, @CATID, @ITEMID, @UNITTP, @CPQTY, @CQTY, @PQTY, @QTY, @RATE, @AMOUNT, @USERPC, @INTIME, @USERID, @IPADDRESS)";
                //comm = new SqlCommand(query, conn);
                cmd.Parameters.Clear();
                cmd.Parameters.Add("@TRANSTP", SqlDbType.NVarChar).Value = ob.TransTP;
                cmd.Parameters.Add("@TRANSDT", SqlDbType.SmallDateTime).Value = ob.TrDt;
                cmd.Parameters.Add("@TRANSMY", SqlDbType.NVarChar).Value = ob.MonthYear;
                cmd.Parameters.Add("@TRANSNO", SqlDbType.BigInt).Value = ob.TransNo;
                cmd.Parameters.Add("@INVREFNO", SqlDbType.NVarChar).Value = ob.InvRefNo;
                cmd.Parameters.Add("@STOREFR", SqlDbType.NVarChar).Value = ob.StoreFR;
                cmd.Parameters.Add("@STORETO", SqlDbType.NVarChar).Value = ob.To;
                cmd.Parameters.Add("@REMARKS", SqlDbType.NVarChar).Value = ob.Remarks;
                cmd.Parameters.Add("@TRANSSL", SqlDbType.NVarChar).Value = ob.TransSL;
                cmd.Parameters.Add("@CATID", SqlDbType.NVarChar).Value = ob.CatID;
                cmd.Parameters.Add("@ITEMID", SqlDbType.NVarChar).Value = ob.ItemID;
                cmd.Parameters.Add("@UNITTP", SqlDbType.NVarChar).Value = ob.UnitTP;
                cmd.Parameters.Add("@CPQTY", SqlDbType.Decimal).Value = ob.CpQTY;
                cmd.Parameters.Add("@CQTY", SqlDbType.Decimal).Value = ob.CQTY;
                cmd.Parameters.Add("@PQTY", SqlDbType.Decimal).Value = ob.PQTY;
                cmd.Parameters.Add("@QTY", SqlDbType.Decimal).Value = ob.Qty;
                cmd.Parameters.Add("@RATE", SqlDbType.Decimal).Value = ob.Rate;
                cmd.Parameters.Add("@AMOUNT", SqlDbType.Decimal).Value = ob.Amount;
                cmd.Parameters.Add("@USERPC", SqlDbType.NVarChar).Value = ob.UserPC;
                cmd.Parameters.Add("@INTIME", SqlDbType.SmallDateTime).Value = ob.ITime;
                cmd.Parameters.Add("@USERID", SqlDbType.NVarChar).Value = ob.UserID;
                cmd.Parameters.Add("@IPADDRESS", SqlDbType.NVarChar).Value = ob.Ipaddress;
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
        public string InsertReceiveSTK_TRANSMSTCS(StockInterface ob)
        {
            string s = "";
            SqlTransaction tran = null;
            try
            {
                if (con.State != ConnectionState.Open)
                    con.Open();
                tran = con.BeginTransaction();
                cmd.CommandType = CommandType.Text;
                cmd.CommandText = @"insert into STK_TRANSMSTCS ( TRANSTP, TRANSDT, TRANSMY, TRANSNO, INVREFNO, STOREFR, STORETO, REMARKS, USERPC, INTIME, USERID, IPADDRESS) 
                  values(@TRANSTP, @TRANSDT, @TRANSMY, @TRANSNO, @INVREFNO, @STOREFR, @STORETO, @REMARKS,@USERPC, @INTIME, @USERID, @IPADDRESS)";
                //comm = new SqlCommand(query, conn);
                cmd.Parameters.Clear();
                cmd.Parameters.Add("@TRANSTP", SqlDbType.NVarChar).Value = ob.TransTP;
                cmd.Parameters.Add("@TRANSDT", SqlDbType.SmallDateTime).Value = ob.TrDt;
                cmd.Parameters.Add("@TRANSMY", SqlDbType.NVarChar).Value = ob.MonthYear;
                cmd.Parameters.Add("@TRANSNO", SqlDbType.BigInt).Value = ob.TransNo;
                cmd.Parameters.Add("@INVREFNO", SqlDbType.NVarChar).Value = ob.InvRefNo;
                cmd.Parameters.Add("@STOREFR", SqlDbType.NVarChar).Value = ob.StoreFR;
                cmd.Parameters.Add("@STORETO", SqlDbType.NVarChar).Value = ob.To;
                cmd.Parameters.Add("@REMARKS", SqlDbType.NVarChar).Value = ob.Remarks;
                cmd.Parameters.Add("@USERPC", SqlDbType.NVarChar).Value = ob.UserPC;
                cmd.Parameters.Add("@INTIME", SqlDbType.SmallDateTime).Value = ob.ITime;
                cmd.Parameters.Add("@USERID", SqlDbType.NVarChar).Value = ob.UserID;
                cmd.Parameters.Add("@IPADDRESS", SqlDbType.NVarChar).Value = ob.Ipaddress;
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

        public string UpdateReceiveSTK_TRANSMSTCS(StockInterface ob)
        {
            string P = "";
            SqlTransaction tran = null;
            try
            {
                string userName = HttpContext.Current.Session["UserName"].ToString();
                if (con.State != ConnectionState.Open)
                    con.Open();
                tran = con.BeginTransaction();
                cmd.CommandType = CommandType.Text;

                cmd.CommandText = "update STK_TRANSMSTCS set INVREFNO=@INVREFNO , STOREFR=@STOREFR, STORETO=@STORETO , REMARKS=@REMARKS ," +
                              " UPDUSERPC=@UPDUSERPC , UPDATEUSERID =@UPDATEUSERID , UPDTIME=@UPDTIME , UPDIPADDRESS=@UPDIPADDRESS where TRANSTP = 'IRFG' and TRANSMY=@TRANSMY and TRANSDT=@TRANSDT and TRANSNO =@TRANSNO ";
                cmd.Parameters.Clear();
                cmd.Parameters.Add("@INVREFNO", SqlDbType.NVarChar).Value = ob.InvRefNo;
                cmd.Parameters.Add("@STOREFR", SqlDbType.NVarChar).Value = ob.StoreFR;
                cmd.Parameters.Add("@STORETO", SqlDbType.NVarChar).Value = ob.To;
                cmd.Parameters.Add("@REMARKS", SqlDbType.NVarChar).Value = ob.Remarks;
                cmd.Parameters.Add("@UPDUSERPC", SqlDbType.NVarChar).Value = ob.UPDUserPC;
                cmd.Parameters.Add("@UPDATEUSERID", SqlDbType.NVarChar).Value = ob.UserID;
                cmd.Parameters.Add("@UPDTIME", SqlDbType.DateTime).Value = ob.UpdTime;
                cmd.Parameters.Add("@UPDIPADDRESS", SqlDbType.NVarChar).Value = ob.UPDIpaddress;
                cmd.Parameters.Add("@TRANSMY", SqlDbType.NVarChar).Value = ob.MonthYear;
                cmd.Parameters.Add("@TRANSDT", SqlDbType.DateTime).Value = ob.TrDt;
                cmd.Parameters.Add("@TRANSNO", SqlDbType.BigInt).Value = ob.TransNo;

                cmd.Transaction = tran;
                cmd.ExecuteNonQuery();
                tran.Commit();
                if (con.State != ConnectionState.Closed)
                    con.Close();
            }
            catch (Exception ex)
            {
                tran.Rollback();
                P = ex.Message;
            }
            return P;

        }




        public string UpdateReceiveBtnTRANSMST(StockInterface ob)
        {
            string m = "";
            SqlTransaction tran = null;
            try
            {
                string userName = HttpContext.Current.Session["UserName"].ToString();
                if (con.State != ConnectionState.Open)
                    con.Open();
                tran = con.BeginTransaction();
                cmd.CommandType = CommandType.Text;
                cmd.CommandText = "update STK_TRANSMSTCS set INVREFNO =@INVREFNO, STORETO=@STORETO, REMARKS=@REMARKS, UPDATEUSERID=@UPDATEUSERID where TRANSTP = 'IRFG' and TRANSMY=@TRANSMY and TRANSDT=@TRANSDT and TRANSNO =@TRANSNO";

                cmd.Parameters.Clear();
                cmd.Parameters.Add("@INVREFNO", SqlDbType.NVarChar).Value = ob.InvRefNo;
                cmd.Parameters.Add("@STORETO", SqlDbType.NVarChar).Value = ob.ToId;
                //cmd.Parameters.Add("@PSID", SqlDbType.NVarChar).Value = ob.PsId;
                cmd.Parameters.Add("@REMARKS", SqlDbType.NVarChar).Value = ob.Remarks;
                cmd.Parameters.Add("@UPDATEUSERID", SqlDbType.NVarChar).Value = ob.UserID;

                cmd.Parameters.Add("@TRANSMY", SqlDbType.NVarChar).Value = ob.MonthYear;
                cmd.Parameters.Add("@TRANSDT", SqlDbType.DateTime).Value = ob.TrDt;
                cmd.Parameters.Add("@TRANSNO", SqlDbType.BigInt).Value = ob.TransNo;

                cmd.Transaction = tran;
                cmd.ExecuteNonQuery();
                tran.Commit();
                if (con.State != ConnectionState.Closed)
                    con.Close();
            }
            catch (Exception ex)
            {
                tran.Rollback();
                m = ex.Message;
            }
            return m;
        }


        public string UPDTransfrSTK_TRANSCS(StockInterface ob)
        {
            string s = "";
            SqlTransaction tran = null;
            try
            {
                if (con.State != ConnectionState.Open)
                    con.Open();
                tran = con.BeginTransaction();
                cmd.CommandType = CommandType.Text;


                cmd.CommandText = "update STK_TRANSCS set INVREFNO=@INVREFNO,STOREFR=@STOREFR,STORETO=@STORETO,REMARKS=@REMARKS,CATID=@CATID," +
                    "ITEMID=@ITEMID,UNITTP=@UNITTP,CPQTY=@CPQTY,CQTY=@CQTY,PQTY=@PQTY,QTY=@QTY,RATE=@RATE,AMOUNT=@AMOUNT,UPDATEUSERID=@UPDATEUSERID, " +
                    "UPDUSERPC=@UPDUSERPC,UPDTIME=@UPDTIME,UPDIPADDRESS=@UPDIPADDRESS  where TRANSTP='IIFG' and TRANSMY=@TRANSMY and TRANSDT=@TRANSDT " +
                    "and TRANSNO=@TRANSNO and TRANSSL=@TRANSSL";

                cmd.Parameters.Clear();
                cmd.Parameters.Add("@INVREFNO", SqlDbType.NVarChar).Value = ob.InvRefNo;
                cmd.Parameters.Add("@STOREFR", SqlDbType.NVarChar).Value = ob.StoreFR;
                cmd.Parameters.Add("@STORETO", SqlDbType.NVarChar).Value = ob.To;
                cmd.Parameters.Add("@REMARKS", SqlDbType.NVarChar).Value = ob.Remarks;
                cmd.Parameters.Add("@CATID", SqlDbType.NVarChar).Value = ob.CatID;
                cmd.Parameters.Add("@ITEMID", SqlDbType.NVarChar).Value = ob.ItemID;
                cmd.Parameters.Add("@UNITTP", SqlDbType.NVarChar).Value = ob.UnitTP;
                cmd.Parameters.Add("@CPQTY", SqlDbType.Decimal).Value = ob.CpQTY;
                cmd.Parameters.Add("@CQTY", SqlDbType.Decimal).Value = ob.CQTY;
                cmd.Parameters.Add("@PQTY", SqlDbType.Decimal).Value = ob.PQTY;
                cmd.Parameters.Add("@QTY", SqlDbType.Decimal).Value = ob.Qty;
                cmd.Parameters.Add("@RATE", SqlDbType.Decimal).Value = ob.Rate;
                cmd.Parameters.Add("@AMOUNT", SqlDbType.Decimal).Value = ob.Amount;
                cmd.Parameters.Add("@UPDATEUSERID", SqlDbType.NVarChar).Value = ob.UserID;
                cmd.Parameters.Add("@TRANSTP", SqlDbType.NVarChar).Value = ob.TransTP;
                cmd.Parameters.Add("@UPDUSERPC", SqlDbType.NVarChar).Value = ob.UPDUserPC;
                cmd.Parameters.Add("@UPDTIME", SqlDbType.SmallDateTime).Value = ob.UpdTime;
                cmd.Parameters.Add("@UPDIPADDRESS", SqlDbType.NVarChar).Value = ob.UPDIpaddress;
                //For Where clouse
                cmd.Parameters.Add("@TRANSMY", SqlDbType.NVarChar).Value = ob.MonthYear;
                cmd.Parameters.Add("@TRANSDT", SqlDbType.SmallDateTime).Value = ob.TrDt;
                cmd.Parameters.Add("@TRANSNO", SqlDbType.BigInt).Value = ob.TransNo;
                cmd.Parameters.Add("@TRANSSL", SqlDbType.NVarChar).Value = ob.TransSL;

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
        public string InsertPurchaseSTK_TRANSMST(StockInterface ob)
        {
            string s = "";
            SqlTransaction tran = null;
            try
            {
                if (con.State != ConnectionState.Open)
                    con.Open();
                tran = con.BeginTransaction();
                cmd.CommandType = CommandType.Text;
                cmd.CommandText = @"insert into STK_TRANSMST 
                      ( TRANSTP, TRANSDT, TRANSNO, TRRTSNO, UNITTO, PSID, REMARKS, USERPC, INTIME, USERID, IPADDRESS) 
                values(@TRANSTP, @TRANSDT, @TRANSNO,@TRRTSNO,@UNITTO,@PSID,@REMARKS, @USERPC, @INTIME, @USERID,@IPADDRESS)";
                //comm = new SqlCommand(query, conn);
                cmd.Parameters.Clear();
                cmd.Parameters.Add("@TRANSTP", SqlDbType.NVarChar).Value = ob.TransTP;
                cmd.Parameters.Add("@TRANSDT", SqlDbType.SmallDateTime).Value = ob.TrDt;
                cmd.Parameters.Add("@TRANSNO", SqlDbType.NVarChar).Value = ob.TransNo2;
                cmd.Parameters.Add("@TRRTSNO", SqlDbType.NVarChar).Value = ob.InvRefNo;
                cmd.Parameters.Add("@UNITTO", SqlDbType.NVarChar).Value = ob.To;
                cmd.Parameters.Add("@PSID", SqlDbType.NVarChar).Value = ob.PsId;
                cmd.Parameters.Add("@REMARKS", SqlDbType.NVarChar).Value = ob.Remarks;

                cmd.Parameters.Add("@USERPC", SqlDbType.NVarChar).Value = ob.UserPC;
                cmd.Parameters.Add("@INTIME", SqlDbType.SmallDateTime).Value = ob.ITime;
                cmd.Parameters.Add("@USERID", SqlDbType.NVarChar).Value = ob.UserID;
                cmd.Parameters.Add("@IPADDRESS", SqlDbType.NVarChar).Value = ob.Ipaddress;
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
        public string InsertPurchaseSTK_TRANS(StockInterface ob)
        {
            string s = "";
            SqlTransaction tran = null;
            try
            {
                if (con.State != ConnectionState.Open)
                    con.Open();
                tran = con.BeginTransaction();
                cmd.CommandType = CommandType.Text;
                cmd.CommandText = @"insert into STK_TRANS ( TRANSTP, TRANSDT, TRANSNO, TRRTSNO, UNITTO, ITEMSL, ITEMID, ITEMCD, QTY, 
                                 RATE, AMOUNT, USERPC, USERID, INTIME, IPADDRESS) 
                  values(@TRANSTP, @TRANSDT, @TRANSNO, @TRRTSNO, @UNITTO, @ITEMSL, @ITEMID, @ITEMCD, @QTY, 
                                 @RATE, @AMOUNT, @USERPC, @USERID, @INTIME, @IPADDRESS)";
                cmd.Parameters.Clear();
                cmd.Parameters.Add("@TRANSTP", SqlDbType.NVarChar).Value = ob.TransTP;
                cmd.Parameters.Add("@TRANSDT", SqlDbType.SmallDateTime).Value = ob.TrDt;
                cmd.Parameters.Add("@TRANSNO", SqlDbType.NVarChar).Value = ob.TransNo2;
                cmd.Parameters.Add("@TRRTSNO", SqlDbType.NVarChar).Value = ob.InvRefNo;
                cmd.Parameters.Add("@UNITTO", SqlDbType.NVarChar).Value = ob.To;
                cmd.Parameters.Add("@ITEMSL", SqlDbType.NVarChar).Value = ob.TransSL;

                cmd.Parameters.Add("@ITEMID", SqlDbType.NVarChar).Value = ob.ItemID;
                cmd.Parameters.Add("@ITEMCD", SqlDbType.NVarChar).Value = ob.ItemCD;
                cmd.Parameters.Add("@QTY", SqlDbType.Decimal).Value = ob.Qty;
                cmd.Parameters.Add("@RATE", SqlDbType.Decimal).Value = ob.Rate;
                cmd.Parameters.Add("@AMOUNT", SqlDbType.Decimal).Value = ob.Amount;
                cmd.Parameters.Add("@USERPC", SqlDbType.NVarChar).Value = ob.UserPC;
                cmd.Parameters.Add("@INTIME", SqlDbType.SmallDateTime).Value = ob.ITime;
                cmd.Parameters.Add("@USERID", SqlDbType.NVarChar).Value = ob.UserID;
                cmd.Parameters.Add("@IPADDRESS", SqlDbType.NVarChar).Value = ob.Ipaddress;
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
        public string UpdateStorePurchaseSTK_TRANS(StockInterface ob)
        {
            string P = "";
            SqlTransaction tran = null;
            try
            {
                string userName = HttpContext.Current.Session["UserName"].ToString();
                if (con.State != ConnectionState.Open)
                    con.Open();
                tran = con.BeginTransaction();
                cmd.CommandType = CommandType.Text;
                cmd.CommandText = "update STK_TRANS set TRRTSNO =@TRRTSNO , UNITTO=@UNITTO , ITEMID =@ITEMID , ITEMCD=@ITEMCD, " +
                " QTY=@QTY , RATE=@RATE , AMOUNT=@AMOUNT , UPDUSERPC=@UPDUSERPC , UPDUSERID=@UPDUSERID , UPDTIME=@UPDTIME , " +
                "UPDIPADDRESS=@UPDIPADDRESS where TRANSTP = 'BUY' and TRANSDT=@TRANSDT and TRANSNO =@TRANSNO and ITEMSL =@ITEMSL ";
                cmd.Parameters.Clear();
                cmd.Parameters.Add("@TRRTSNO", SqlDbType.NVarChar).Value = ob.InvRefNo;
                cmd.Parameters.Add("@UNITTO", SqlDbType.NVarChar).Value = ob.To;
                cmd.Parameters.Add("@ITEMID", SqlDbType.NVarChar).Value = ob.ItemID;
                cmd.Parameters.Add("@ITEMCD", SqlDbType.NVarChar).Value = ob.ItemCD;
                cmd.Parameters.Add("@QTY", SqlDbType.Decimal).Value = ob.Qty;
                cmd.Parameters.Add("@RATE", SqlDbType.Decimal).Value = ob.Rate;
                cmd.Parameters.Add("@AMOUNT", SqlDbType.Decimal).Value = ob.Amount;
                cmd.Parameters.Add("@UPDUSERPC", SqlDbType.NVarChar).Value = ob.UPDUserPC;
                cmd.Parameters.Add("@UPDUSERID", SqlDbType.NVarChar).Value = ob.UpdUserID;
                cmd.Parameters.Add("@UPDTIME", SqlDbType.SmallDateTime).Value = ob.UpdTime;
                cmd.Parameters.Add("@UPDIPADDRESS", SqlDbType.NVarChar).Value = ob.UPDIpaddress;
                cmd.Parameters.Add("@TRANSDT", SqlDbType.SmallDateTime).Value = ob.TrDt;
                cmd.Parameters.Add("@TRANSNO", SqlDbType.NVarChar).Value = ob.TransNo2;
                cmd.Parameters.Add("@ITEMSL", SqlDbType.NVarChar).Value = ob.TransSL;

                cmd.Transaction = tran;
                cmd.ExecuteNonQuery();
                tran.Commit();
                if (con.State != ConnectionState.Closed)
                    con.Close();
            }
            catch (Exception ex)
            {
                tran.Rollback();
                P = ex.Message;
            }
            return P;

        }
        public string UpdateStorePurchaseSTK_TRANSMST(StockInterface ob)
        {
            string P = "";
            SqlTransaction tran = null;
            try
            {
                string userName = HttpContext.Current.Session["UserName"].ToString();
                if (con.State != ConnectionState.Open)
                    con.Open();
                tran = con.BeginTransaction();
                cmd.CommandType = CommandType.Text;
                cmd.CommandText = @"UPDATE STK_TRANSMST SET  PSID=@PSID, TOTAMT=@TOTAMT, TPCOST=@TPCOST, DISCOUNT=@DISCOUNT, NETAMT=@NETAMT, 
                REMARKS=@REMARKS WHERE TRANSTP=@TRANSTP AND TRANSDT=@TRANSDT AND TRANSNO=@TRANSNO";
                cmd.Parameters.Clear();
                cmd.Parameters.Add("@PSID", SqlDbType.NVarChar).Value = ob.PsId;
                cmd.Parameters.Add("@REMARKS", SqlDbType.NVarChar).Value = ob.Remarks;

                cmd.Parameters.Add("@TOTAMT", SqlDbType.Decimal).Value = ob.TotAmnt;
                cmd.Parameters.Add("@TPCOST", SqlDbType.Decimal).Value = ob.TpCostAmt;
                cmd.Parameters.Add("@DISCOUNT", SqlDbType.Decimal).Value = ob.disCountRT;
                cmd.Parameters.Add("@NETAMT", SqlDbType.Decimal).Value = ob.NetAmt;

                cmd.Parameters.Add("@TRANSDT", SqlDbType.SmallDateTime).Value = ob.TrDt;
                cmd.Parameters.Add("@TRANSNO", SqlDbType.NVarChar).Value = ob.TransNo2;
                cmd.Parameters.Add("@TRANSTP", SqlDbType.NVarChar).Value = ob.TransTP;

                cmd.Transaction = tran;
                cmd.ExecuteNonQuery();
                tran.Commit();
                if (con.State != ConnectionState.Closed)
                    con.Close();
            }
            catch (Exception ex)
            {
                if (tran != null) tran.Rollback();
                P = ex.Message;
            }
            return P;

        }



        /*********************** ISSUE *****************************/
        /*********************** ISSUE *****************************/
        /*********************** ISSUE *****************************/
        /*********************** ISSUE *****************************/
        /*********************** ISSUE *****************************/
        /*********************** ISSUE *****************************/
        /*********************** ISSUE *****************************/
        /*********************** ISSUE *****************************/
        /*********************** ISSUE *****************************/



        public string InsertIssueSTK_TRANS(StockInterface ob)
        {
            string s = "";
            SqlTransaction tran = null;
            try
            {
                if (con.State != ConnectionState.Open)
                    con.Open();
                tran = con.BeginTransaction();
                cmd.CommandType = CommandType.Text;
                cmd.CommandText = @"insert into STK_TRANS ( TRANSTP, TRANSDT, TRANSNO, TRRTSNO, UNITTO, UNITFR, ITEMSL, ITEMID, ITEMCD, QTY, 
                                 RATE, AMOUNT, USERPC, USERID, INTIME, IPADDRESS) 
                  values(@TRANSTP, @TRANSDT, @TRANSNO, @TRRTSNO, @UNITTO, @UNITFR, @ITEMSL, @ITEMID, @ITEMCD, @QTY, 
                                 @RATE, @AMOUNT, @USERPC, @USERID, @INTIME, @IPADDRESS)";
                cmd.Parameters.Clear();
                cmd.Parameters.Add("@TRANSTP", SqlDbType.NVarChar).Value = ob.TransTP;
                cmd.Parameters.Add("@TRANSDT", SqlDbType.SmallDateTime).Value = ob.TrDt;
                cmd.Parameters.Add("@TRANSNO", SqlDbType.NVarChar).Value = ob.TransNo2;
                cmd.Parameters.Add("@TRRTSNO", SqlDbType.NVarChar).Value = ob.InvRefNo;
                cmd.Parameters.Add("@UNITTO", SqlDbType.NVarChar).Value = ob.To;
                cmd.Parameters.Add("@UNITFR", SqlDbType.NVarChar).Value = ob.StoreFR;
                cmd.Parameters.Add("@ITEMSL", SqlDbType.NVarChar).Value = ob.TransSL;

                cmd.Parameters.Add("@ITEMID", SqlDbType.NVarChar).Value = ob.ItemID;
                cmd.Parameters.Add("@ITEMCD", SqlDbType.NVarChar).Value = ob.ItemCD;
                cmd.Parameters.Add("@QTY", SqlDbType.Decimal).Value = ob.Qty;
                cmd.Parameters.Add("@RATE", SqlDbType.Decimal).Value = ob.Rate;
                cmd.Parameters.Add("@AMOUNT", SqlDbType.Decimal).Value = ob.Amount;
                cmd.Parameters.Add("@USERPC", SqlDbType.NVarChar).Value = ob.UserPC;
                cmd.Parameters.Add("@INTIME", SqlDbType.SmallDateTime).Value = ob.ITime;
                cmd.Parameters.Add("@USERID", SqlDbType.NVarChar).Value = ob.UserID;
                cmd.Parameters.Add("@IPADDRESS", SqlDbType.NVarChar).Value = ob.Ipaddress;
                cmd.Transaction = tran;
                cmd.ExecuteNonQuery();
                tran.Commit();
                if (con.State != ConnectionState.Closed)
                    con.Close();
            }
            catch (Exception ex)
            {
                if (tran != null) tran.Rollback();
                s = ex.Message;
            }
            return s;
        }
        public string InsertIssueSTK_TRANSMST(StockInterface ob)
        {
            string s = "";
            SqlTransaction tran = null;
            try
            {
                if (con.State != ConnectionState.Open)
                    con.Open();
                tran = con.BeginTransaction();
                cmd.CommandType = CommandType.Text;
                cmd.CommandText = @"insert into STK_TRANSMST 
                      ( TRANSTP, TRANSDT, TRANSNO, TRRTSNO, UNITTO, UNITFR, REMARKS, USERPC, INTIME, USERID, IPADDRESS) 
                values(@TRANSTP, @TRANSDT, @TRANSNO,@TRRTSNO,@UNITTO,@UNITFR,@REMARKS, @USERPC, @INTIME, @USERID,@IPADDRESS)";
                //comm = new SqlCommand(query, conn);
                cmd.Parameters.Clear();
                cmd.Parameters.Add("@TRANSTP", SqlDbType.NVarChar).Value = ob.TransTP;
                cmd.Parameters.Add("@TRANSDT", SqlDbType.SmallDateTime).Value = ob.TrDt;
                cmd.Parameters.Add("@TRANSNO", SqlDbType.NVarChar).Value = ob.TransNo2;
                cmd.Parameters.Add("@TRRTSNO", SqlDbType.NVarChar).Value = ob.InvRefNo;
                cmd.Parameters.Add("@UNITTO", SqlDbType.NVarChar).Value = ob.To;
                cmd.Parameters.Add("@UNITFR", SqlDbType.NVarChar).Value = ob.StoreFR;
                cmd.Parameters.Add("@REMARKS", SqlDbType.NVarChar).Value = ob.Remarks;

                cmd.Parameters.Add("@USERPC", SqlDbType.NVarChar).Value = ob.UserPC;
                cmd.Parameters.Add("@INTIME", SqlDbType.SmallDateTime).Value = ob.ITime;
                cmd.Parameters.Add("@USERID", SqlDbType.NVarChar).Value = ob.UserID;
                cmd.Parameters.Add("@IPADDRESS", SqlDbType.NVarChar).Value = ob.Ipaddress;
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
        public string UpdateIssueSTK_TRANS(StockInterface ob)
        {
            string P = "";
            SqlTransaction tran = null;
            try
            {
                string userName = HttpContext.Current.Session["UserName"].ToString();
                if (con.State != ConnectionState.Open)
                    con.Open();
                tran = con.BeginTransaction();
                cmd.CommandType = CommandType.Text;
                cmd.CommandText = "update STK_TRANS set TRRTSNO =@TRRTSNO , UNITTO=@UNITTO , UNITFR=@UNITFR, ITEMID =@ITEMID , ITEMCD=@ITEMCD, " +
                " QTY=@QTY , RATE=@RATE , AMOUNT=@AMOUNT , UPDUSERPC=@UPDUSERPC , UPDUSERID=@UPDUSERID , UPDTIME=@UPDTIME , " +
                "UPDIPADDRESS=@UPDIPADDRESS where TRANSTP = @TRANSTP and TRANSDT=@TRANSDT and TRANSNO =@TRANSNO and ITEMSL =@ITEMSL ";
                cmd.Parameters.Clear();
                cmd.Parameters.Add("@TRANSTP", SqlDbType.NVarChar).Value = ob.TransTP;
                cmd.Parameters.Add("@TRRTSNO", SqlDbType.NVarChar).Value = ob.InvRefNo;
                cmd.Parameters.Add("@UNITTO", SqlDbType.NVarChar).Value = ob.To;
                cmd.Parameters.Add("@UNITFR", SqlDbType.NVarChar).Value = ob.StoreFR;
                cmd.Parameters.Add("@ITEMID", SqlDbType.NVarChar).Value = ob.ItemID;
                cmd.Parameters.Add("@ITEMCD", SqlDbType.NVarChar).Value = ob.ItemCD;
                cmd.Parameters.Add("@QTY", SqlDbType.Decimal).Value = ob.Qty;
                cmd.Parameters.Add("@RATE", SqlDbType.Decimal).Value = ob.Rate;
                cmd.Parameters.Add("@AMOUNT", SqlDbType.Decimal).Value = ob.Amount;
                cmd.Parameters.Add("@UPDUSERPC", SqlDbType.NVarChar).Value = ob.UPDUserPC;
                cmd.Parameters.Add("@UPDUSERID", SqlDbType.NVarChar).Value = ob.UpdUserID;
                cmd.Parameters.Add("@UPDTIME", SqlDbType.SmallDateTime).Value = ob.UpdTime;
                cmd.Parameters.Add("@UPDIPADDRESS", SqlDbType.NVarChar).Value = ob.UPDIpaddress;
                cmd.Parameters.Add("@TRANSDT", SqlDbType.SmallDateTime).Value = ob.TrDt;
                cmd.Parameters.Add("@TRANSNO", SqlDbType.NVarChar).Value = ob.TransNo2;
                cmd.Parameters.Add("@ITEMSL", SqlDbType.NVarChar).Value = ob.TransSL;

                cmd.Transaction = tran;
                cmd.ExecuteNonQuery();
                tran.Commit();
                if (con.State != ConnectionState.Closed)
                    con.Close();
            }
            catch (Exception ex)
            {
                tran.Rollback();
                P = ex.Message;
            }
            return P;

        }
        public string UpdateIssueSTK_TRANSMST(StockInterface ob)
        {
            string P = "";
            SqlTransaction tran = null;
            try
            {
                string userName = HttpContext.Current.Session["UserName"].ToString();
                if (con.State != ConnectionState.Open)
                    con.Open();
                tran = con.BeginTransaction();
                cmd.CommandType = CommandType.Text;
                cmd.CommandText = @"UPDATE STK_TRANSMST SET  TOTAMT=@TOTAMT, TPCOST=@TPCOST, DISCOUNT=@DISCOUNT, NETAMT=@NETAMT, 
                REMARKS=@REMARKS WHERE TRANSTP=@TRANSTP AND TRANSDT=@TRANSDT AND TRANSNO=@TRANSNO";
                cmd.Parameters.Clear();
                cmd.Parameters.Add("@REMARKS", SqlDbType.NVarChar).Value = ob.Remarks;

                cmd.Parameters.Add("@TOTAMT", SqlDbType.Decimal).Value = ob.TotAmnt;
                cmd.Parameters.Add("@TPCOST", SqlDbType.Decimal).Value = ob.TpCostAmt;
                cmd.Parameters.Add("@DISCOUNT", SqlDbType.Decimal).Value = ob.disCountRT;
                cmd.Parameters.Add("@NETAMT", SqlDbType.Decimal).Value = ob.NetAmt;

                cmd.Parameters.Add("@TRANSDT", SqlDbType.SmallDateTime).Value = ob.TrDt;
                cmd.Parameters.Add("@TRANSNO", SqlDbType.NVarChar).Value = ob.TransNo2;
                cmd.Parameters.Add("@TRANSTP", SqlDbType.NVarChar).Value = ob.TransTP;

                cmd.Transaction = tran;
                cmd.ExecuteNonQuery();
                tran.Commit();
                if (con.State != ConnectionState.Closed)
                    con.Close();
            }
            catch (Exception ex)
            {
                if (tran != null) tran.Rollback();
                P = ex.Message;
            }
            return P;

        }


        public string InsertPartyInformation(StockInterface ob)
        {
            string s = "";
            SqlTransaction tran = null;
            try
            {
                if (con.State != ConnectionState.Open)
                    con.Open();
                tran = con.BeginTransaction();
                cmd.CommandType = CommandType.Text;
                cmd.CommandText = @"INSERT INTO STK_PARTY(COMPID, PARTYID, PARTYNM, PARTYNMB, ADDRESS, ADDRESSB, 
                MOBNO1, MOBNO2, EMAILID, APNM, APCNO, REMARKS, 
                STATUS, USERPC, USERID, INTIME, IPADDRESS, INSLTUDE) 
                VALUES(@COMPID, @PARTYID, @PARTYNM, @PARTYNMB, @ADDRESS, @ADDRESSB, 
                @MOBNO1, @MOBNO2, @EMAILID, @APNM, @APCNO, @REMARKS, 
                @STATUS, @USERPC, @USERID, @INTIME, @IPADDRESS, ''); 

                INSERT INTO STK_USERPS(COMPID, PSTP, PSID, USERCD, USERNM, MOBNO1, MOBNO2, LOGINID, LOGINPW, 
                STATUS, USERPC, USERID, INTIME, IPADDRESS, INSLTUDE) 
                VALUES(@COMPID, @PSTP, @PARTYID, @USERCD, @PARTYNM, @MOBNO1, @MOBNO2, @MOBNO1, @MOBNO1, 
                @STATUS, @USERPC, @USERID, @INTIME, @IPADDRESS, '')";

                cmd.Parameters.Clear();
                cmd.Parameters.Add("@COMPID", SqlDbType.NVarChar).Value = ob.CompanyId;
                cmd.Parameters.Add("@PARTYID", SqlDbType.NVarChar).Value = ob.PartyId;
                cmd.Parameters.Add("@PARTYNM", SqlDbType.NVarChar).Value = ob.PartyNameEnglish;
                cmd.Parameters.Add("@PARTYNMB", SqlDbType.NVarChar).Value = ob.PartyNameBangla;
                cmd.Parameters.Add("@ADDRESS", SqlDbType.NVarChar).Value = ob.AddressEnglish;
                cmd.Parameters.Add("@ADDRESSB", SqlDbType.NVarChar).Value = ob.AddressBangla;
                cmd.Parameters.Add("@MOBNO1", SqlDbType.NVarChar).Value = ob.MobNo;
                cmd.Parameters.Add("@MOBNO2", SqlDbType.NVarChar).Value = ob.MobNo2;
                cmd.Parameters.Add("@EMAILID", SqlDbType.NVarChar).Value = ob.Email;
                cmd.Parameters.Add("@APNM", SqlDbType.NVarChar).Value = ob.Author;
                cmd.Parameters.Add("@APCNO", SqlDbType.NVarChar).Value = ob.ApContactNo;
                cmd.Parameters.Add("@REMARKS", SqlDbType.NVarChar).Value = ob.Remarks;
                cmd.Parameters.Add("@STATUS", SqlDbType.NVarChar).Value = ob.Status;

                cmd.Parameters.Add("@PSTP", SqlDbType.NVarChar).Value = ob.Pstp;
                cmd.Parameters.Add("@USERCD", SqlDbType.NVarChar).Value = ob.UserCode;

                cmd.Parameters.Add("@USERPC", SqlDbType.NVarChar).Value = ob.UserPC;
                cmd.Parameters.Add("@INTIME", SqlDbType.SmallDateTime).Value = ob.ITime;
                cmd.Parameters.Add("@USERID", SqlDbType.NVarChar).Value = ob.UserID;
                cmd.Parameters.Add("@IPADDRESS", SqlDbType.NVarChar).Value = ob.Ipaddress;
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
        public string UpdatePartyInformation(StockInterface ob)
        {
            string s = "";
            SqlTransaction tran = null;
            try
            {
                if (con.State != ConnectionState.Open)
                    con.Open();
                tran = con.BeginTransaction();
                cmd.CommandType = CommandType.Text;
                cmd.CommandText = @"UPDATE STK_PARTY SET PARTYNM=@PARTYNM, PARTYNMB=@PARTYNMB, ADDRESS=@ADDRESS, ADDRESSB=@ADDRESSB, 
                MOBNO1=@MOBNO1, MOBNO2=@MOBNO2, EMAILID=@EMAILID, APNM=@APNM, APCNO=@APCNO, REMARKS=@REMARKS, 
                STATUS=@STATUS, UPDUSERPC=@UPDUSERPC, UPDINTIME=@UPDINTIME, UPDUSERID=@UPDUSERID, UPDIPADDRESS=@UPDIPADDRESS, UPDLTUDE=''
                WHERE  COMPID=@COMPID AND PARTYID=@PARTYID";

                cmd.Parameters.Clear();
                cmd.Parameters.Add("@COMPID", SqlDbType.NVarChar).Value = ob.CompanyId;
                cmd.Parameters.Add("@PARTYID", SqlDbType.NVarChar).Value = ob.PartyId;

                cmd.Parameters.Add("@PARTYNM", SqlDbType.NVarChar).Value = ob.PartyNameEnglish;
                cmd.Parameters.Add("@PARTYNMB", SqlDbType.NVarChar).Value = ob.PartyNameBangla;
                cmd.Parameters.Add("@ADDRESS", SqlDbType.NVarChar).Value = ob.AddressEnglish;
                cmd.Parameters.Add("@ADDRESSB", SqlDbType.NVarChar).Value = ob.AddressBangla;
                cmd.Parameters.Add("@MOBNO1", SqlDbType.NVarChar).Value = ob.MobNo;
                cmd.Parameters.Add("@MOBNO2", SqlDbType.NVarChar).Value = ob.MobNo2;
                cmd.Parameters.Add("@EMAILID", SqlDbType.NVarChar).Value = ob.Email;
                cmd.Parameters.Add("@APNM", SqlDbType.NVarChar).Value = ob.Author;
                cmd.Parameters.Add("@APCNO", SqlDbType.NVarChar).Value = ob.ApContactNo;
                cmd.Parameters.Add("@REMARKS", SqlDbType.NVarChar).Value = ob.Remarks;
                cmd.Parameters.Add("@STATUS", SqlDbType.NVarChar).Value = ob.Status;

                cmd.Parameters.Add("@UPDUSERPC", SqlDbType.NVarChar).Value = ob.UserPC;
                cmd.Parameters.Add("@UPDINTIME", SqlDbType.SmallDateTime).Value = ob.ITime;
                cmd.Parameters.Add("@UPDUSERID", SqlDbType.NVarChar).Value = ob.UserID;
                cmd.Parameters.Add("@UPDIPADDRESS", SqlDbType.NVarChar).Value = ob.Ipaddress;
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
        public string InsertPartyUserLogin(StockInterface ob)
        {
            string s = "";
            SqlTransaction tran = null;
            try
            {
                if (con.State != ConnectionState.Open)
                    con.Open();
                tran = con.BeginTransaction();
                cmd.CommandType = CommandType.Text;
                cmd.CommandText = @"INSERT INTO STK_USERPS(COMPID, PSTP, PSID, USERCD, USERNM, MOBNO1, MOBNO2, LOGINID, LOGINPW, 
                STATUS, USERPC, USERID, INTIME, IPADDRESS, INSLTUDE) 
                VALUES(@COMPID, @PSTP, @PSID, @USERCD, @USERNM, @MOBNO1, @MOBNO2, @LOGINID, @LOGINPW, 
                @STATUS, @USERPC, @USERID, @INTIME, @IPADDRESS, '')";

                cmd.Parameters.Clear();
                cmd.Parameters.Add("@COMPID", SqlDbType.NVarChar).Value = ob.CompanyId;
                cmd.Parameters.Add("@PSID", SqlDbType.NVarChar).Value = ob.PartyId;
                cmd.Parameters.Add("@PSTP", SqlDbType.NVarChar).Value = ob.Pstp;
                cmd.Parameters.Add("@USERCD", SqlDbType.NVarChar).Value = ob.UserCode;

                cmd.Parameters.Add("@USERNM", SqlDbType.NVarChar).Value = ob.PartyNameEnglish;
                cmd.Parameters.Add("@MOBNO1", SqlDbType.NVarChar).Value = ob.MobNo;
                cmd.Parameters.Add("@MOBNO2", SqlDbType.NVarChar).Value = ob.MobNo2;
                cmd.Parameters.Add("@LOGINID", SqlDbType.NVarChar).Value = ob.LoginId;
                cmd.Parameters.Add("@LOGINPW", SqlDbType.NVarChar).Value = ob.Password;
                cmd.Parameters.Add("@STATUS", SqlDbType.NVarChar).Value = ob.Status;

                cmd.Parameters.Add("@USERPC", SqlDbType.NVarChar).Value = ob.UserPC;
                cmd.Parameters.Add("@INTIME", SqlDbType.SmallDateTime).Value = ob.ITime;
                cmd.Parameters.Add("@USERID", SqlDbType.NVarChar).Value = ob.UserID;
                cmd.Parameters.Add("@IPADDRESS", SqlDbType.NVarChar).Value = ob.Ipaddress;
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
        public string UpdatePartyUserLogin(StockInterface ob)
        {
            string s = "";
            SqlTransaction tran = null;
            try
            {
                if (con.State != ConnectionState.Open)
                    con.Open();
                tran = con.BeginTransaction();
                cmd.CommandType = CommandType.Text;
                cmd.CommandText = @"UPDATE STK_USERPS SET USERNM=@USERNM, MOBNO1=@MOBNO1, MOBNO2=@MOBNO2, LOGINID=@LOGINID, LOGINPW=@LOGINPW, 
                STATUS=@STATUS, UPDUSERPC=@UPDUSERPC, UPDINTIME=@UPDINTIME, UPDUSERID=@UPDUSERID, UPDIPADDRESS=@UPDIPADDRESS, UPDLTUDE=''
                WHERE COMPID=@COMPID AND PSTP=@PSTP AND PSID=@PSID AND USERCD=@USERCD";

                cmd.Parameters.Clear();
                cmd.Parameters.Add("@COMPID", SqlDbType.NVarChar).Value = ob.CompanyId;
                cmd.Parameters.Add("@PSID", SqlDbType.NVarChar).Value = ob.PartyId;
                cmd.Parameters.Add("@PSTP", SqlDbType.NVarChar).Value = ob.Pstp;
                cmd.Parameters.Add("@USERCD", SqlDbType.NVarChar).Value = ob.UserCode;

                cmd.Parameters.Add("@USERNM", SqlDbType.NVarChar).Value = ob.PartyNameEnglish;
                cmd.Parameters.Add("@MOBNO1", SqlDbType.NVarChar).Value = ob.MobNo;
                cmd.Parameters.Add("@MOBNO2", SqlDbType.NVarChar).Value = ob.MobNo2;
                cmd.Parameters.Add("@LOGINID", SqlDbType.NVarChar).Value = ob.LoginId;
                cmd.Parameters.Add("@LOGINPW", SqlDbType.NVarChar).Value = ob.Password;
                cmd.Parameters.Add("@STATUS", SqlDbType.NVarChar).Value = ob.Status;

                cmd.Parameters.Add("@UPDUSERPC", SqlDbType.NVarChar).Value = ob.UserPC;
                cmd.Parameters.Add("@UPDINTIME", SqlDbType.SmallDateTime).Value = ob.ITime;
                cmd.Parameters.Add("@UPDUSERID", SqlDbType.NVarChar).Value = ob.UserID;
                cmd.Parameters.Add("@UPDIPADDRESS", SqlDbType.NVarChar).Value = ob.Ipaddress;
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

        public string InsertOrderMaster(StockInterface ob)
        {
            string s = "";
            SqlTransaction tran = null;
            try
            {
                if (con.State != ConnectionState.Open)
                    con.Open();
                tran = con.BeginTransaction();
                cmd.CommandType = CommandType.Text;
                cmd.CommandText = @"INSERT INTO STK_TRANSMST (COMPID, TRANSTP, TRANSDT, TRANSYY, TRANSNO, PSID, PSSL, BILLDT, 
                REMARKS, USERPC, USERID, INTIME, IPADDRESS, INSLTUDE) 
                VALUES(@COMPID, @TRANSTP, @TRANSDT, @TRANSYY, @TRANSNO, @PSID, @PSSL, @BILLDT, 
                @REMARKS, @USERPC, @USERID, @INTIME, @IPADDRESS, '')";

                cmd.Parameters.Clear();
                cmd.Parameters.Add("@COMPID", SqlDbType.Int).Value = ob.CompanyId;
                cmd.Parameters.Add("@TRANSTP", SqlDbType.NVarChar).Value = ob.TransTP;
                cmd.Parameters.Add("@TRANSDT", SqlDbType.SmallDateTime).Value = ob.TrDt;
                cmd.Parameters.Add("@TRANSYY", SqlDbType.Int).Value = ob.MonthYear;
                cmd.Parameters.Add("@TRANSNO", SqlDbType.NVarChar).Value = ob.TransNo;
                cmd.Parameters.Add("@PSID", SqlDbType.NVarChar).Value = ob.PartyId;
                cmd.Parameters.Add("@BILLDT", SqlDbType.SmallDateTime).Value = ob.BillDate;
                cmd.Parameters.Add("@PSSL", SqlDbType.NVarChar).Value = ob.PartySerial;
                cmd.Parameters.Add("@REMARKS", SqlDbType.NVarChar).Value = ob.Remarks;

                cmd.Parameters.Add("@USERPC", SqlDbType.NVarChar).Value = ob.UserPC;
                cmd.Parameters.Add("@INTIME", SqlDbType.SmallDateTime).Value = ob.ITime;
                cmd.Parameters.Add("@USERID", SqlDbType.NVarChar).Value = ob.UserID;
                cmd.Parameters.Add("@IPADDRESS", SqlDbType.NVarChar).Value = ob.Ipaddress;
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
        public string InsertOrder(StockInterface ob)
        {
            string s = "";
            SqlTransaction tran = null;
            try
            {
                if (con.State != ConnectionState.Open)
                    con.Open();
                tran = con.BeginTransaction();
                cmd.CommandType = CommandType.Text;
                cmd.CommandText = @"INSERT INTO STK_TRANS (COMPID, TRANSTP, TRANSDT, TRANSYY, TRANSNO, PSID, PSSL, BILLDT, TRANSSL, 
                    ITEMID, QTY, RATE, AMOUNT, REMARKS, USERPC, USERID, INTIME, IPADDRESS, INSLTUDE)  
                VALUES(@COMPID, @TRANSTP, @TRANSDT, @TRANSYY, @TRANSNO, @PSID, @PSSL, @BILLDT, @TRANSSL, 
                    @ITEMID, @QTY, @RATE, @AMOUNT, @REMARKS, @USERPC, @USERID, @INTIME, @IPADDRESS, '')";

                cmd.Parameters.Clear();
                cmd.Parameters.Add("@COMPID", SqlDbType.NVarChar).Value = ob.CompanyId;
                cmd.Parameters.Add("@TRANSTP", SqlDbType.NVarChar).Value = ob.TransTP;
                cmd.Parameters.Add("@TRANSDT", SqlDbType.SmallDateTime).Value = ob.TrDt;
                cmd.Parameters.Add("@TRANSYY", SqlDbType.Int).Value = ob.MonthYear;
                cmd.Parameters.Add("@TRANSNO", SqlDbType.NVarChar).Value = ob.TransNo;
                cmd.Parameters.Add("@PSID", SqlDbType.NVarChar).Value = ob.PartyId;
                cmd.Parameters.Add("@BILLDT", SqlDbType.SmallDateTime).Value = ob.BillDate;
                cmd.Parameters.Add("@PSSL", SqlDbType.NVarChar).Value = ob.PartySerial;
                cmd.Parameters.Add("@REMARKS", SqlDbType.NVarChar).Value = ob.RemarksDetails;

                cmd.Parameters.Add("@ITEMID", SqlDbType.NVarChar).Value = ob.ItemID;
                cmd.Parameters.Add("@TRANSSL", SqlDbType.NVarChar).Value = ob.TransSL;
                cmd.Parameters.Add("@QTY", SqlDbType.Decimal).Value = ob.Qty;
                cmd.Parameters.Add("@RATE", SqlDbType.Decimal).Value = ob.Rate;
                cmd.Parameters.Add("@AMOUNT", SqlDbType.Decimal).Value = ob.Amount;

                cmd.Parameters.Add("@USERPC", SqlDbType.NVarChar).Value = ob.UserPC;
                cmd.Parameters.Add("@INTIME", SqlDbType.SmallDateTime).Value = ob.ITime;
                cmd.Parameters.Add("@USERID", SqlDbType.NVarChar).Value = ob.UserID;
                cmd.Parameters.Add("@IPADDRESS", SqlDbType.NVarChar).Value = ob.Ipaddress;
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




        public string InsertSaleTransactionMaster(StockInterface ob)
        {
            string s = "";
            SqlTransaction tran = null;
            try
            {
                if (con.State != ConnectionState.Open)
                    con.Open();
                tran = con.BeginTransaction();
                cmd.CommandType = CommandType.Text;
                cmd.CommandText = @"INSERT INTO STK_TRANSMST (COMPID, TRANSTP, TRANSDT, TRANSYY, TRANSNO, PSID,BILLDT,PSSL, VEHICLENO,DRIVERNM,ASSTNM,
                REMARKS, USERPC, USERID, INTIME, IPADDRESS, INSLTUDE) 
                VALUES(@COMPID, @TRANSTP, @TRANSDT, @TRANSYY, @TRANSNO, @PSID,@BILLDT, @PSSL,@VEHICLENO,@DRIVERNM,@ASSTNM, 
                @REMARKS, @USERPC, @USERID, @INTIME, @IPADDRESS, '')";

                cmd.Parameters.Clear();
                cmd.Parameters.Add("@COMPID", SqlDbType.Int).Value = ob.CompanyId;
                cmd.Parameters.Add("@TRANSTP", SqlDbType.NVarChar).Value = ob.TransTP;
                cmd.Parameters.Add("@TRANSDT", SqlDbType.SmallDateTime).Value = ob.TrDt;
                cmd.Parameters.Add("@TRANSYY", SqlDbType.Int).Value = ob.MonthYear;
                cmd.Parameters.Add("@TRANSNO", SqlDbType.NVarChar).Value = ob.TransNo;
                cmd.Parameters.Add("@PSID", SqlDbType.NVarChar).Value = ob.PartyId;
                cmd.Parameters.Add("@BILLDT", SqlDbType.SmallDateTime).Value = ob.BillDate;
                cmd.Parameters.Add("@PSSL", SqlDbType.NVarChar).Value = ob.PartySerial;
                cmd.Parameters.Add("@VEHICLENO", SqlDbType.NVarChar).Value = ob.vehicalsno;
                cmd.Parameters.Add("@DRIVERNM", SqlDbType.NVarChar).Value = ob.driveerNm;
                cmd.Parameters.Add("@ASSTNM", SqlDbType.NVarChar).Value = ob.asstNm;
                cmd.Parameters.Add("@REMARKS", SqlDbType.NVarChar).Value = ob.Remarks;

                cmd.Parameters.Add("@USERPC", SqlDbType.NVarChar).Value = ob.UserPC;
                cmd.Parameters.Add("@INTIME", SqlDbType.SmallDateTime).Value = ob.ITime;
                cmd.Parameters.Add("@USERID", SqlDbType.NVarChar).Value = ob.UserID;
                cmd.Parameters.Add("@IPADDRESS", SqlDbType.NVarChar).Value = ob.Ipaddress;
                cmd.Transaction = tran;
                cmd.ExecuteNonQuery();
                tran.Commit();
                s = "true";
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


        public string InsertSaleTransaction(StockInterface ob)
        {
            string s = "";
            SqlTransaction tran = null;
            try
            {
                if (con.State != ConnectionState.Open)
                    con.Open();
                tran = con.BeginTransaction();
                cmd.CommandType = CommandType.Text;
                cmd.CommandText = @"INSERT INTO STK_TRANS (COMPID, TRANSTP, TRANSDT,BILLDT,ORDERDT, TRANSYY, TRANSNO, PSID, PSSL, TRANSSL, 
                    ITEMID, QTY,ORDERQTY, RATE, AMOUNT,RETTP,RETRT,NETRT,NETAMT, REMARKS, USERPC, USERID, INTIME, IPADDRESS, INSLTUDE)  
                VALUES(@COMPID, @TRANSTP, @TRANSDT,@BILLDT,@ORDERDT, @TRANSYY, @TRANSNO, @PSID, @PSSL, @TRANSSL, 
                    @ITEMID, @QTY,@ORDERQTY, @RATE, @AMOUNT,'',@RETRT,@NETRT,@NETAMT, @REMARKS, @USERPC, @USERID, @INTIME, @IPADDRESS, '')";

                cmd.Parameters.Clear();
                cmd.Parameters.Add("@COMPID", SqlDbType.Int).Value = ob.CompanyId;
                cmd.Parameters.Add("@TRANSTP", SqlDbType.NVarChar).Value = ob.TransTP;
                cmd.Parameters.Add("@TRANSDT", SqlDbType.SmallDateTime).Value = ob.TrDt;
                cmd.Parameters.Add("@ORDERDT", SqlDbType.SmallDateTime).Value = ob.OrderDT;
                cmd.Parameters.Add("@TRANSYY", SqlDbType.Int).Value = ob.MonthYear;
                cmd.Parameters.Add("@TRANSNO", SqlDbType.NVarChar).Value = ob.TransNo;
                cmd.Parameters.Add("@PSID", SqlDbType.NVarChar).Value = ob.PartyId;
                cmd.Parameters.Add("@BILLDT", SqlDbType.SmallDateTime).Value = ob.BillDate;
                cmd.Parameters.Add("@PSSL", SqlDbType.NVarChar).Value = ob.PartySerial;
                //cmd.Parameters.Add("@RETTP", SqlDbType.NVarChar).Value = ob.PartySerial;

                cmd.Parameters.Add("@RETRT", SqlDbType.Decimal).Value = ob.RetRt;
                cmd.Parameters.Add("@NETRT", SqlDbType.Decimal).Value = ob.netRt;
                cmd.Parameters.Add("@NETAMT", SqlDbType.Decimal).Value = ob.NetAmnt;

                cmd.Parameters.Add("@REMARKS", SqlDbType.NVarChar).Value = ob.RemarksDetails;

                cmd.Parameters.Add("@ITEMID", SqlDbType.NVarChar).Value = ob.ItemID;
                cmd.Parameters.Add("@TRANSSL", SqlDbType.NVarChar).Value = ob.TransSL;
                cmd.Parameters.Add("@QTY", SqlDbType.Decimal).Value = ob.Qty;
                cmd.Parameters.Add("@ORDERQTY", SqlDbType.Decimal).Value = ob.Orderqty;
                cmd.Parameters.Add("@RATE", SqlDbType.Decimal).Value = ob.Rate;
                cmd.Parameters.Add("@AMOUNT", SqlDbType.Decimal).Value = ob.Amount;

                cmd.Parameters.Add("@USERPC", SqlDbType.NVarChar).Value = ob.UserPC;
                cmd.Parameters.Add("@INTIME", SqlDbType.SmallDateTime).Value = ob.ITime;
                cmd.Parameters.Add("@USERID", SqlDbType.NVarChar).Value = ob.UserID;
                cmd.Parameters.Add("@IPADDRESS", SqlDbType.NVarChar).Value = ob.Ipaddress;
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



        public string InsertSaleReturnTransactionMaster(StockInterface ob)
        {
            string s = "";
            SqlTransaction tran = null;
            try
            {
                if (con.State != ConnectionState.Open)
                    con.Open();
                tran = con.BeginTransaction();
                cmd.CommandType = CommandType.Text;
                cmd.CommandText = @"INSERT INTO STK_TRANSMST (COMPID, TRANSTP, TRANSDT, TRANSYY, TRANSNO, PSID, PSSL, VEHICLENO,DRIVERNM,ASSTNM,
                REMARKS, USERPC, USERID, INTIME, IPADDRESS, INSLTUDE) 
                VALUES(@COMPID, @TRANSTP, @TRANSDT, @TRANSYY, @TRANSNO, @PSID, @PSSL,@VEHICLENO,@DRIVERNM,@ASSTNM, 
                @REMARKS, @USERPC, @USERID, @INTIME, @IPADDRESS, '')";

                cmd.Parameters.Clear();
                cmd.Parameters.Add("@COMPID", SqlDbType.Int).Value = ob.CompanyId;
                cmd.Parameters.Add("@TRANSTP", SqlDbType.NVarChar).Value = ob.TransTP;
                cmd.Parameters.Add("@TRANSDT", SqlDbType.SmallDateTime).Value = ob.TrDt;
                cmd.Parameters.Add("@TRANSYY", SqlDbType.Int).Value = ob.MonthYear;
                cmd.Parameters.Add("@TRANSNO", SqlDbType.NVarChar).Value = ob.TransNo;
                cmd.Parameters.Add("@PSID", SqlDbType.NVarChar).Value = ob.PartyId;
                //    cmd.Parameters.Add("@BILLDT", SqlDbType.SmallDateTime).Value = ob.BillDate;
                cmd.Parameters.Add("@PSSL", SqlDbType.NVarChar).Value = ob.PartySerial;
                cmd.Parameters.Add("@VEHICLENO", SqlDbType.NVarChar).Value = ob.vehicalsno;
                cmd.Parameters.Add("@DRIVERNM", SqlDbType.NVarChar).Value = ob.driveerNm;
                cmd.Parameters.Add("@ASSTNM", SqlDbType.NVarChar).Value = ob.asstNm;
                cmd.Parameters.Add("@REMARKS", SqlDbType.NVarChar).Value = ob.Remarks;

                cmd.Parameters.Add("@USERPC", SqlDbType.NVarChar).Value = ob.UserPC;
                cmd.Parameters.Add("@INTIME", SqlDbType.SmallDateTime).Value = ob.ITime;
                cmd.Parameters.Add("@USERID", SqlDbType.NVarChar).Value = ob.UserID;
                cmd.Parameters.Add("@IPADDRESS", SqlDbType.NVarChar).Value = ob.Ipaddress;
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


        public string InsertSaleReturnTransaction(StockInterface ob)
        {
            string s = "";
            SqlTransaction tran = null;
            try
            {
                if (con.State != ConnectionState.Open)
                    con.Open();
                tran = con.BeginTransaction();
                cmd.CommandType = CommandType.Text;
                cmd.CommandText = @"INSERT INTO STK_TRANS (COMPID, TRANSTP, TRANSDT, TRANSYY, TRANSNO, PSID, PSSL, TRANSSL, 
                    ITEMID, QTY, RATE, AMOUNT,RETTP,RETRT,NETRT,NETAMT, REMARKS, USERPC, USERID, INTIME, IPADDRESS, INSLTUDE)  
                VALUES(@COMPID, @TRANSTP, @TRANSDT, @TRANSYY, @TRANSNO, @PSID, @PSSL, @TRANSSL, 
                    @ITEMID, @QTY, @RATE, @AMOUNT,@RETTP,@RETRT,@NETRT,@NETAMT, @REMARKS, @USERPC, @USERID, @INTIME, @IPADDRESS, '')";

                cmd.Parameters.Clear();
                cmd.Parameters.Add("@COMPID", SqlDbType.Int).Value = ob.CompanyId;
                cmd.Parameters.Add("@TRANSTP", SqlDbType.NVarChar).Value = ob.TransTP;
                cmd.Parameters.Add("@TRANSDT", SqlDbType.SmallDateTime).Value = ob.TrDt;
                cmd.Parameters.Add("@TRANSYY", SqlDbType.Int).Value = ob.MonthYear;
                cmd.Parameters.Add("@TRANSNO", SqlDbType.NVarChar).Value = ob.TransNo;
                cmd.Parameters.Add("@PSID", SqlDbType.NVarChar).Value = ob.PartyId;
                //  cmd.Parameters.Add("@BILLDT", SqlDbType.SmallDateTime).Value = ob.BillDate;
                cmd.Parameters.Add("@PSSL", SqlDbType.NVarChar).Value = ob.PartySerial;
                cmd.Parameters.Add("@RETTP", SqlDbType.NVarChar).Value = ob.rettp;

                cmd.Parameters.Add("@RETRT", SqlDbType.Decimal).Value = ob.RetRt;
                cmd.Parameters.Add("@NETRT", SqlDbType.Decimal).Value = ob.netRt;
                cmd.Parameters.Add("@NETAMT", SqlDbType.Decimal).Value = ob.NetAmnt;

                cmd.Parameters.Add("@REMARKS", SqlDbType.NVarChar).Value = ob.RemarksDetails;

                cmd.Parameters.Add("@ITEMID", SqlDbType.NVarChar).Value = ob.ItemID;
                cmd.Parameters.Add("@TRANSSL", SqlDbType.NVarChar).Value = ob.TransSL;
                cmd.Parameters.Add("@QTY", SqlDbType.Decimal).Value = ob.Qty;
                cmd.Parameters.Add("@RATE", SqlDbType.Decimal).Value = ob.Rate;
                cmd.Parameters.Add("@AMOUNT", SqlDbType.Decimal).Value = ob.Amount;

                cmd.Parameters.Add("@USERPC", SqlDbType.NVarChar).Value = ob.UserPC;
                cmd.Parameters.Add("@INTIME", SqlDbType.SmallDateTime).Value = ob.ITime;
                cmd.Parameters.Add("@USERID", SqlDbType.NVarChar).Value = ob.UserID;
                cmd.Parameters.Add("@IPADDRESS", SqlDbType.NVarChar).Value = ob.Ipaddress;
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
        public string UpdateOrder(StockInterface ob)
        {
            string s = "";
            SqlTransaction tran = null;
            try
            {
                if (con.State != ConnectionState.Open)
                    con.Open();
                tran = con.BeginTransaction();
                cmd.CommandType = CommandType.Text;
                cmd.CommandText = @"UPDATE STK_TRANS  SET  QTY=@QTY, RATE=@RATE, AMOUNT=@AMOUNT, REMARKS=@REMARKS, UPDUSERPC=@USERPC, UPDUSERID=@USERID, 
                UPDINTIME=@INTIME, UPDIPADDRESS=@IPADDRESS, UPDLTUDE=''    
                WHERE COMPID=@COMPID AND TRANSTP=@TRANSTP AND TRANSYY=@TRANSYY AND 
                TRANSNO=@TRANSNO AND TRANSSL=@TRANSSL AND ITEMID=@ITEMID";

                cmd.Parameters.Clear();
                cmd.Parameters.Add("@COMPID", SqlDbType.NVarChar).Value = ob.CompanyId;
                cmd.Parameters.Add("@TRANSTP", SqlDbType.NVarChar).Value = ob.TransTP;
                cmd.Parameters.Add("@TRANSYY", SqlDbType.Int).Value = ob.MonthYear;
                cmd.Parameters.Add("@TRANSNO", SqlDbType.NVarChar).Value = ob.TransNo;
                cmd.Parameters.Add("@ITEMID", SqlDbType.NVarChar).Value = ob.ItemID;
                cmd.Parameters.Add("@TRANSSL", SqlDbType.NVarChar).Value = ob.TransSL;

                cmd.Parameters.Add("@QTY", SqlDbType.Decimal).Value = ob.Qty;
                cmd.Parameters.Add("@RATE", SqlDbType.Decimal).Value = ob.Rate;
                cmd.Parameters.Add("@AMOUNT", SqlDbType.Decimal).Value = ob.Amount;
                cmd.Parameters.Add("@REMARKS", SqlDbType.NVarChar).Value = ob.RemarksDetails;

                cmd.Parameters.Add("@USERPC", SqlDbType.NVarChar).Value = ob.UserPC;
                cmd.Parameters.Add("@INTIME", SqlDbType.SmallDateTime).Value = ob.ITime;
                cmd.Parameters.Add("@USERID", SqlDbType.NVarChar).Value = ob.UserID;
                cmd.Parameters.Add("@IPADDRESS", SqlDbType.NVarChar).Value = ob.Ipaddress;
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


        public string UpdateSaleTransaction(StockInterface ob)
        {
            string s = "";
            SqlTransaction tran = null;
            try
            {
                if (con.State != ConnectionState.Open)
                    con.Open();
                tran = con.BeginTransaction();
                cmd.CommandType = CommandType.Text;
                cmd.CommandText = @"UPDATE STK_TRANS  SET QTY=@QTY, RATE=@RATE, AMOUNT=@AMOUNT,RETRT=@RETRT,NETRT=@NETRT,NETAMT=@NETAMT, REMARKS=@REMARKS, UPDUSERPC=@USERPC, UPDUSERID=@USERID, 
                UPDINTIME=@INTIME, UPDIPADDRESS=@IPADDRESS, UPDLTUDE=''    
                WHERE COMPID=@COMPID AND TRANSTP=@TRANSTP AND TRANSYY=@TRANSYY AND 
                TRANSNO=@TRANSNO AND TRANSSL=@TRANSSL";
                
                cmd.Parameters.Clear();
                cmd.Parameters.Add("@COMPID", SqlDbType.NVarChar).Value = ob.CompanyId;
                cmd.Parameters.Add("@TRANSTP", SqlDbType.NVarChar).Value = ob.TransTP;
                cmd.Parameters.Add("@TRANSYY", SqlDbType.Int).Value = ob.MonthYear;
                cmd.Parameters.Add("@TRANSNO", SqlDbType.NVarChar).Value = ob.TransNo;
                cmd.Parameters.Add("@ITEMID", SqlDbType.NVarChar).Value = ob.ItemID;
                cmd.Parameters.Add("@TRANSSL", SqlDbType.NVarChar).Value = ob.TransSL;

                cmd.Parameters.Add("@QTY", SqlDbType.Decimal).Value = ob.Qty;
                cmd.Parameters.Add("@RATE", SqlDbType.Decimal).Value = ob.Rate;
                cmd.Parameters.Add("@AMOUNT", SqlDbType.Decimal).Value = ob.Amount;
                cmd.Parameters.Add("@RETRT", SqlDbType.Decimal).Value = ob.RetRt;
                cmd.Parameters.Add("@NETRT", SqlDbType.Decimal).Value = ob.netRt;
                cmd.Parameters.Add("@NETAMT", SqlDbType.Decimal).Value = ob.NetAmnt;


                cmd.Parameters.Add("@REMARKS", SqlDbType.NVarChar).Value = ob.RemarksDetails;

                cmd.Parameters.Add("@USERPC", SqlDbType.NVarChar).Value = ob.UPDUserPC;
                cmd.Parameters.Add("@USERID", SqlDbType.NVarChar).Value = ob.UpdUserID;
                cmd.Parameters.Add("@INTIME", SqlDbType.SmallDateTime).Value = ob.UpdTime;
                cmd.Parameters.Add("@IPADDRESS", SqlDbType.NVarChar).Value = ob.UPDIpaddress;
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



        public string UpdateSaleReturnTransaction(StockInterface ob)
        {
            string s = "";
            SqlTransaction tran = null;
            try
            {
                if (con.State != ConnectionState.Open)
                    con.Open();
                tran = con.BeginTransaction();
                cmd.CommandType = CommandType.Text;
                cmd.CommandText = @"UPDATE STK_TRANS  SET  ITEMID=@ITEMID, QTY=@QTY, RATE=@RATE, AMOUNT=@AMOUNT,RETTP=@RETTP,RETRT=@RETRT,NETRT=@NETRT,NETAMT=@NETAMT, REMARKS=@REMARKS, UPDUSERPC=@USERPC, UPDUSERID=@USERID, 
                UPDINTIME=@INTIME, UPDIPADDRESS=@IPADDRESS, UPDLTUDE=''    
                WHERE COMPID=@COMPID AND TRANSTP=@TRANSTP AND TRANSYY=@TRANSYY AND 
                TRANSNO=@TRANSNO AND TRANSSL=@TRANSSL";

                cmd.Parameters.Clear();
                cmd.Parameters.Add("@COMPID", SqlDbType.NVarChar).Value = ob.CompanyId;
                cmd.Parameters.Add("@TRANSTP", SqlDbType.NVarChar).Value = ob.TransTP;
                cmd.Parameters.Add("@TRANSYY", SqlDbType.Int).Value = ob.MonthYear;
                cmd.Parameters.Add("@TRANSNO", SqlDbType.NVarChar).Value = ob.TransNo;
                cmd.Parameters.Add("@ITEMID", SqlDbType.NVarChar).Value = ob.ItemID;
                cmd.Parameters.Add("@TRANSSL", SqlDbType.NVarChar).Value = ob.TransSL;

                cmd.Parameters.Add("@QTY", SqlDbType.Decimal).Value = ob.Qty;
                cmd.Parameters.Add("@RATE", SqlDbType.Decimal).Value = ob.Rate;
                cmd.Parameters.Add("@AMOUNT", SqlDbType.Decimal).Value = ob.Amount;
                cmd.Parameters.Add("@RETTP", SqlDbType.NVarChar).Value = ob.rettp;
                cmd.Parameters.Add("@RETRT", SqlDbType.Decimal).Value = ob.RetRt;
                cmd.Parameters.Add("@NETRT", SqlDbType.Decimal).Value = ob.netRt;
                cmd.Parameters.Add("@NETAMT", SqlDbType.Decimal).Value = ob.NetAmnt;


                cmd.Parameters.Add("@REMARKS", SqlDbType.NVarChar).Value = ob.RemarksDetails;

                cmd.Parameters.Add("@USERPC", SqlDbType.NVarChar).Value = ob.UPDUserPC;
                cmd.Parameters.Add("@INTIME", SqlDbType.SmallDateTime).Value = ob.UpdTime;
                cmd.Parameters.Add("@USERID", SqlDbType.NVarChar).Value = ob.UpdUserID;
                cmd.Parameters.Add("@IPADDRESS", SqlDbType.NVarChar).Value = ob.UPDIpaddress;
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

        public string DeleteOrder(StockInterface ob)
        {
            string s = "";
            SqlTransaction tran = null;
            try
            {
                if (con.State != ConnectionState.Open)
                    con.Open();
                tran = con.BeginTransaction();
                cmd.CommandType = CommandType.Text;
                cmd.CommandText = @"DELETE FROM STK_TRANS     
                WHERE COMPID=@COMPID AND TRANSTP=@TRANSTP AND TRANSYY=@TRANSYY AND 
                TRANSNO=@TRANSNO AND TRANSSL=@TRANSSL AND ITEMID=@ITEMID";

                cmd.Parameters.Clear();
                cmd.Parameters.Add("@COMPID", SqlDbType.NVarChar).Value = ob.CompanyId;
                cmd.Parameters.Add("@TRANSTP", SqlDbType.NVarChar).Value = ob.TransTP;
                cmd.Parameters.Add("@TRANSYY", SqlDbType.Int).Value = ob.MonthYear;
                cmd.Parameters.Add("@TRANSNO", SqlDbType.NVarChar).Value = ob.TransNo;
                cmd.Parameters.Add("@ITEMID", SqlDbType.NVarChar).Value = ob.ItemID;
                cmd.Parameters.Add("@TRANSSL", SqlDbType.NVarChar).Value = ob.TransSL;

                cmd.Parameters.Add("@USERPC", SqlDbType.NVarChar).Value = ob.UserPC;
                cmd.Parameters.Add("@INTIME", SqlDbType.SmallDateTime).Value = ob.ITime;
                cmd.Parameters.Add("@USERID", SqlDbType.NVarChar).Value = ob.UserID;
                cmd.Parameters.Add("@IPADDRESS", SqlDbType.NVarChar).Value = ob.Ipaddress;
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

        public string DeleteSaleTransaction(StockInterface ob)
        {
            string s = "";
            SqlTransaction tran = null;
            try
            {
                if (con.State != ConnectionState.Open)
                    con.Open();
                tran = con.BeginTransaction();
                cmd.CommandType = CommandType.Text;
                cmd.CommandText = @"DELETE FROM STK_TRANS     
                WHERE COMPID=@COMPID AND TRANSTP=@TRANSTP AND TRANSYY=@TRANSYY AND 
                TRANSNO=@TRANSNO AND TRANSSL=@TRANSSL AND ITEMID=@ITEMID";

                cmd.Parameters.Clear();
                cmd.Parameters.Add("@COMPID", SqlDbType.NVarChar).Value = ob.CompanyId;
                cmd.Parameters.Add("@TRANSTP", SqlDbType.NVarChar).Value = ob.TransTP;
                cmd.Parameters.Add("@TRANSYY", SqlDbType.Int).Value = ob.MonthYear;
                cmd.Parameters.Add("@TRANSNO", SqlDbType.NVarChar).Value = ob.TransNo;
                cmd.Parameters.Add("@ITEMID", SqlDbType.NVarChar).Value = ob.ItemID;
                cmd.Parameters.Add("@TRANSSL", SqlDbType.NVarChar).Value = ob.TransSL;
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

        public string DeleteSaleReturnTransaction(StockInterface ob)
        {
            string s = "";
            SqlTransaction tran = null;
            try
            {
                if (con.State != ConnectionState.Open)
                    con.Open();
                tran = con.BeginTransaction();
                cmd.CommandType = CommandType.Text;
                cmd.CommandText = @"DELETE FROM STK_TRANS     
                WHERE COMPID=@COMPID AND TRANSTP=@TRANSTP AND TRANSYY=@TRANSYY AND 
                TRANSNO=@TRANSNO AND TRANSSL=@TRANSSL AND ITEMID=@ITEMID";

                cmd.Parameters.Clear();
                cmd.Parameters.Add("@COMPID", SqlDbType.NVarChar).Value = ob.CompanyId;
                cmd.Parameters.Add("@TRANSTP", SqlDbType.NVarChar).Value = ob.TransTP;
                cmd.Parameters.Add("@TRANSYY", SqlDbType.Int).Value = ob.MonthYear;
                cmd.Parameters.Add("@TRANSNO", SqlDbType.NVarChar).Value = ob.TransNo;
                cmd.Parameters.Add("@ITEMID", SqlDbType.NVarChar).Value = ob.ItemID;
                cmd.Parameters.Add("@TRANSSL", SqlDbType.NVarChar).Value = ob.TransSL;
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
        public string DeleteOrderMaster(StockInterface ob)
        {
            string s = "";
            SqlTransaction tran = null;
            try
            {
                if (con.State != ConnectionState.Open)
                    con.Open();
                tran = con.BeginTransaction();
                cmd.CommandType = CommandType.Text;
                cmd.CommandText = @"DELETE FROM STK_TRANSMST     
                WHERE COMPID=@COMPID AND TRANSTP=@TRANSTP AND TRANSYY=@TRANSYY AND TRANSNO=@TRANSNO ";

                cmd.Parameters.Clear();
                cmd.Parameters.Add("@COMPID", SqlDbType.NVarChar).Value = ob.CompanyId;
                cmd.Parameters.Add("@TRANSTP", SqlDbType.NVarChar).Value = ob.TransTP;
                cmd.Parameters.Add("@TRANSYY", SqlDbType.Int).Value = ob.MonthYear;
                cmd.Parameters.Add("@TRANSNO", SqlDbType.NVarChar).Value = ob.TransNo;

                cmd.Parameters.Add("@USERPC", SqlDbType.NVarChar).Value = ob.UserPC;
                cmd.Parameters.Add("@INTIME", SqlDbType.SmallDateTime).Value = ob.ITime;
                cmd.Parameters.Add("@USERID", SqlDbType.NVarChar).Value = ob.UserID;
                cmd.Parameters.Add("@IPADDRESS", SqlDbType.NVarChar).Value = ob.Ipaddress;
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


        public string DeleteSaleTransactionMaster(StockInterface ob)
        {
            string s = "";
            SqlTransaction tran = null;
            try
            {
                if (con.State != ConnectionState.Open)
                    con.Open();
                tran = con.BeginTransaction();
                cmd.CommandType = CommandType.Text;
                cmd.CommandText = @"DELETE FROM STK_TRANSMST     
                WHERE COMPID=@COMPID AND TRANSTP=@TRANSTP AND TRANSYY=@TRANSYY AND TRANSNO=@TRANSNO ";

                cmd.Parameters.Clear();
                cmd.Parameters.Add("@COMPID", SqlDbType.NVarChar).Value = ob.CompanyId;
                cmd.Parameters.Add("@TRANSTP", SqlDbType.NVarChar).Value = ob.TransTP;
                cmd.Parameters.Add("@TRANSYY", SqlDbType.Int).Value = ob.MonthYear;
                cmd.Parameters.Add("@TRANSNO", SqlDbType.NVarChar).Value = ob.TransNo;

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

        public string DeleteSaleReturnTransactionMaster(StockInterface ob)
        {
            string s = "";
            SqlTransaction tran = null;
            try
            {
                if (con.State != ConnectionState.Open)
                    con.Open();
                tran = con.BeginTransaction();
                cmd.CommandType = CommandType.Text;
                cmd.CommandText = @"DELETE FROM STK_TRANSMST     
                WHERE COMPID=@COMPID AND TRANSTP=@TRANSTP AND TRANSYY=@TRANSYY AND TRANSNO=@TRANSNO ";

                cmd.Parameters.Clear();
                cmd.Parameters.Add("@COMPID", SqlDbType.NVarChar).Value = ob.CompanyId;
                cmd.Parameters.Add("@TRANSTP", SqlDbType.NVarChar).Value = ob.TransTP;
                cmd.Parameters.Add("@TRANSYY", SqlDbType.Int).Value = ob.MonthYear;
                cmd.Parameters.Add("@TRANSNO", SqlDbType.NVarChar).Value = ob.TransNo;
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
        public string InsertNotice(StockInterface ob)
        {
            string s = "";
            SqlTransaction tran = null;
            try
            {
                if (con.State != ConnectionState.Open)
                    con.Open();
                tran = con.BeginTransaction();
                cmd.CommandType = CommandType.Text;
                cmd.CommandText = @"INSERT INTO ASL_NOTICE(COMPID, NOTICEDT, NOTICEYY, NOTICESL, NOTICETP, NOTICE, 
                EFDT, ETDT, STATUS, USERPC, USERID, INTIME, IPADDRESS, INSLTUDE) 
                VALUES(@COMPID, @NOTICEDT, @NOTICEYY, @NOTICESL, @NOTICETP, @NOTICE, 
                @EFDT, @ETDT, @STATUS, @USERPC, @USERID, @INTIME, @IPADDRESS, '')";

                cmd.Parameters.Clear();
                cmd.Parameters.Add("@COMPID", SqlDbType.Int).Value = ob.CompanyId;
                cmd.Parameters.Add("@NOTICEDT", SqlDbType.SmallDateTime).Value = ob.TrDt;
                cmd.Parameters.Add("@NOTICEYY", SqlDbType.Int).Value = ob.NoticeYear;
                cmd.Parameters.Add("@NOTICESL", SqlDbType.NVarChar).Value = ob.NoticeId;

                cmd.Parameters.Add("@NOTICETP", SqlDbType.NVarChar).Value = ob.NoticeType;
                cmd.Parameters.Add("@NOTICE", SqlDbType.NVarChar).Value = ob.Notice;
                cmd.Parameters.Add("@EFDT", SqlDbType.SmallDateTime).Value = ob.EffectFromDate;
                cmd.Parameters.Add("@ETDT", SqlDbType.SmallDateTime).Value = ob.EffectToDate;
                cmd.Parameters.Add("@STATUS", SqlDbType.NVarChar).Value = ob.Status;

                cmd.Parameters.Add("@USERPC", SqlDbType.NVarChar).Value = ob.UserPC;
                cmd.Parameters.Add("@INTIME", SqlDbType.SmallDateTime).Value = ob.ITime;
                cmd.Parameters.Add("@USERID", SqlDbType.NVarChar).Value = ob.UserID;
                cmd.Parameters.Add("@IPADDRESS", SqlDbType.NVarChar).Value = ob.Ipaddress;
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
        public string UpdateNotice(StockInterface ob)
        {
            string s = "";
            SqlTransaction tran = null;
            try
            {
                if (con.State != ConnectionState.Open)
                    con.Open();
                tran = con.BeginTransaction();
                cmd.CommandType = CommandType.Text;
                cmd.CommandText = @"UPDATE ASL_NOTICE  SET  NOTICEYY=@NOTICEYY, NOTICESL=@NOTICESL, NOTICETP=@NOTICETP, NOTICE=@NOTICE, 
                EFDT=@EFDT, ETDT=@ETDT, STATUS=@STATUS, UPDUSERPC=@USERPC, UPDUSERID=@USERID, 
                UPDINTIME=@INTIME, UPDIPADDRESS=@IPADDRESS, UPDLTUDE=''    
                WHERE COMPID=@COMPID AND NOTICEYY=@NOTICEYY AND NOTICESL=@NOTICESL";

                cmd.Parameters.Clear();
                cmd.Parameters.Add("@COMPID", SqlDbType.Int).Value = ob.CompanyId;
                cmd.Parameters.Add("@NOTICEYY", SqlDbType.Int).Value = ob.NoticeYear;
                cmd.Parameters.Add("@NOTICESL", SqlDbType.NVarChar).Value = ob.NoticeId;

                cmd.Parameters.Add("@NOTICETP", SqlDbType.NVarChar).Value = ob.NoticeType;
                cmd.Parameters.Add("@NOTICE", SqlDbType.NVarChar).Value = ob.Notice;
                cmd.Parameters.Add("@EFDT", SqlDbType.SmallDateTime).Value = ob.EffectFromDate;
                cmd.Parameters.Add("@ETDT", SqlDbType.SmallDateTime).Value = ob.EffectToDate;
                cmd.Parameters.Add("@STATUS", SqlDbType.NVarChar).Value = ob.Status;

                cmd.Parameters.Add("@USERPC", SqlDbType.NVarChar).Value = ob.UserPC;
                cmd.Parameters.Add("@INTIME", SqlDbType.SmallDateTime).Value = ob.ITime;
                cmd.Parameters.Add("@USERID", SqlDbType.NVarChar).Value = ob.UserID;
                cmd.Parameters.Add("@IPADDRESS", SqlDbType.NVarChar).Value = ob.Ipaddress;
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
        public string DeleteNotice(StockInterface ob)
        {
            string s = "";
            SqlTransaction tran = null;
            try
            {
                if (con.State != ConnectionState.Open)
                    con.Open();
                tran = con.BeginTransaction();
                cmd.CommandType = CommandType.Text;
                cmd.CommandText = @"DELETE FROM ASL_NOTICE     
                WHERE COMPID=@COMPID AND NOTICEYY=@NOTICEYY AND NOTICESL=@NOTICESL";

                cmd.Parameters.Clear();
                cmd.Parameters.Add("@COMPID", SqlDbType.Int).Value = ob.CompanyId;
                cmd.Parameters.Add("@NOTICEYY", SqlDbType.Int).Value = ob.NoticeYear;
                cmd.Parameters.Add("@NOTICESL", SqlDbType.NVarChar).Value = ob.NoticeId;
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