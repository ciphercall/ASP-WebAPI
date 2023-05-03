using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using AlchemyAccounting;
using DynamicMenu.Functions;
using DynamicMenu.Models.Table;

namespace DynamicMenu.Controllers
{
    public class OrderController : ApiController
    {
        [System.Web.Http.HttpPost]
        [System.Web.Http.Route("api/Order/CreateOrder")]
        public object CreateOrder([FromBody] OrderModel order)
        {
            var token = order.Token;
            var usercode = order.UserCode;
            var partyid = order.PartyId;
            var itemandqty = order.ItemQtys;
            var imeino = order.ImeiNo;

            if (Token.TokenCheck(usercode, token))
            {
                try
                {
                    DateTime todayDateTime = dbFunctions.Timezone(DateTime.Now);
                    string companyid = "101";
                    int year = todayDateTime.Year;
                    string transdt = todayDateTime.ToString("yyyy-MM-dd");
                    string billDate = order.BillDate;
                    string transtp = "IORD";
                    string transactionno = StockTransaction.OrderTransactionNo(year.ToString());
                    string partySerial = StockTransaction.PartySerial(year.ToString(), partyid);

                    string query = @"INSERT INTO STK_TRANSMST (COMPID, TRANSTP, TRANSDT, TRANSYY, TRANSNO, PSID, PSSL, BILLDT, 
                REMARKS, USERPC, USERID, INTIME, IPADDRESS, INSLTUDE) 
                VALUES('" + companyid + "', '" + transtp + "', '" + transdt + "', '" + year + "', '" + transactionno +
                                   "', '" + partyid +
                                   "', '" + partySerial + "', '" + billDate +
                                   "', '', 'ANDROID APP', '" + usercode + "', '" + todayDateTime + "', '" + imeino +
                                   "', ''); ";

                    int transslsl = 1;
                    foreach (var item in itemandqty)
                    {
                        string itemid = item.ItemId;
                        string remarks = item.Remarks;
                        decimal qty = item.Qty;
                        var rate =
                            dbFunctions.StringData(@"SELECT ISNULL(SALRT,0) SALRT FROM STK_ITEM WHERE ITEMID='" + itemid +
                                                   "'");
                        if (rate == "") rate = "0";

                        decimal itemrate = Convert.ToDecimal(rate);
                        decimal amount = qty * itemrate;

                        query += @" INSERT INTO STK_TRANS (COMPID, TRANSTP, TRANSDT, TRANSYY, TRANSNO, PSID, PSSL, BILLDT, TRANSSL, 
                    ITEMID, QTY, RATE, AMOUNT, REMARKS, USERPC, USERID, INTIME, IPADDRESS, INSLTUDE) 
                    VALUES('" + companyid + "', '" + transtp + "', '" + transdt + "', '" + year + "', '" + transactionno +
                                 "', '" + partyid +
                                 "', '" + partySerial + "', '" + billDate + "', '" + transslsl + "', '" + itemid + "', '" +
                                 qty + "', '" + rate +
                                 "', '" + amount + "', N'" + remarks + "', 'ANDROID APP', '" + usercode + "', '" + todayDateTime +
                                 "', '" + imeino + "', '')";

                        transslsl++;
                    }
                    string s = dbFunctions.ExecuteQuery(query);
                    if (s == "")
                        return new
                        {
                            Data = transactionno,
                            Success = true,
                            Message = "Save data Successfully."
                        };
                    else
                        return new
                        {
                            Success = false,
                            Message = "Internal Error Ocured."
                        };
                }
                catch (Exception ex)
                {
                    return new
                    {
                        Data = ex,
                        Success = false,
                        Message = "Internal Error Ocured."
                    };
                }
            }
            return new
            {
                Success = false,
                Message = "Authorized not permitted."
            };
        }
        [System.Web.Http.HttpGet]
        [System.Web.Http.Route("api/Order/GetAllTransactionListDateWise")]
        public object GetAllTransactionListDateWise(string usercode, string token, string partyid, string fromdate, string todate)
        {
            List<TransactionList> nulllist = new List<TransactionList>();

            if (Token.TokenCheck(usercode, token))
            {
                List<TransactionList> list = new List<TransactionList>();
                SqlConnection con = new SqlConnection(dbFunctions.connection);
                SqlCommand cmd = new SqlCommand(@"SELECT REPLACE(CONVERT(nvarchar, STK_TRANSMST.TRANSDT, 106), ' ', '-') AS DATEVIEW, 
                REPLACE(CONVERT(nvarchar, STK_TRANSMST.TRANSDT, 111), '/', '-') AS DBDATE, STK_TRANSMST.TRANSNO, STK_USERPS.USERNM,
                REPLACE(CONVERT(nvarchar, STK_TRANSMST.BILLDT, 106), ' ', '-')  AS BILLDT
                FROM STK_TRANSMST INNER JOIN
                STK_USERPS ON STK_TRANSMST.COMPID = STK_USERPS.COMPID AND STK_TRANSMST.USERID = STK_USERPS.USERCD
                WHERE STK_TRANSMST.PSID='" + partyid + "' AND STK_TRANSMST.TRANSDT BETWEEN '" + fromdate + "' AND '" + todate + "'", con);
                con.Open();
                SqlDataReader dr = cmd.ExecuteReader();
                while (dr.Read())
                {
                    list.Add(new TransactionList
                    {
                        TransactionDate = dr[0].ToString(),
                        DateDb = dr[1].ToString(),
                        TransactionNo = dr[2].ToString(),
                        UserName = dr[3].ToString(),
                        BillDate = dr[4].ToString()
                    });
                }
                dr.Close();
                con.Close();
                if (list.Count > 0)
                    return new
                    {
                        Data = list,
                        Success = true,
                        Message = ""
                    };
                else return new
                {
                    Data = nulllist,
                    Success = false,
                    Message = "No Data Found."
                };
            }
            return new
            {
                Data = nulllist,
                Success = false,
                Message = "Authorized not permitted."
            };
        }
        [System.Web.Http.HttpGet]
        [System.Web.Http.Route("api/Order/GetTransactionTransNoWise")]
        public object GetTransactionTransNoWise(string usercode, string token, string transno, string transdate)
        {
            List<TransactionDetails> nulllist = new List<TransactionDetails>();

            if (Token.TokenCheck(usercode, token))
            {
                List<TransactionDetails> list = new List<TransactionDetails>();
                SqlConnection con = new SqlConnection(dbFunctions.connection);
                SqlCommand cmd = new SqlCommand(@"SELECT STK_TRANS.ITEMID, STK_ITEM.ITEMNM, STK_ITEM.ITEMNMB, STK_ITEM.MUNIT, STK_ITEM.IMAGEPATH, 
                STK_TRANS.QTY, STK_TRANS.RATE, STK_TRANS.AMOUNT, STK_TRANS.REMARKS
                FROM STK_TRANS INNER JOIN STK_ITEM ON STK_TRANS.ITEMID = STK_ITEM.ITEMID
                WHERE (STK_TRANS.TRANSNO = '" + transno + "') AND TRANSDT='" + transdate + "'", con);
                con.Open();
                SqlDataReader dr = cmd.ExecuteReader();
                while (dr.Read())
                {
                    list.Add(new TransactionDetails
                    {
                        ItemId = dr[0].ToString(),
                        ItemNameEng = dr[1].ToString(),
                        ItemNameBan = dr[2].ToString(),
                        Unit = dr[3].ToString(),
                        ImagePath = dr[4].ToString(),
                        Qty = dr[5].ToString(),
                        Rate = dr[6].ToString(),
                        Amount = dr[7].ToString(),
                        Remarks = dr[8].ToString()
                    });
                }
                dr.Close();
                con.Close();
                if (list.Count > 0)
                    return new
                    {
                        Data = list,
                        Success = true,
                        Message = ""
                    };
                else return new
                {
                    Data = nulllist,
                    Success = false,
                    Message = "No Data Found."
                };
            }
            return new
            {
                Data = nulllist,
                Success = false,
                Message = "Authorized not permitted."
            };
        }
    }
}
