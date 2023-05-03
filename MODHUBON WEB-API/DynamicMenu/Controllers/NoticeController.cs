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
    public class NoticeController : ApiController
    {
        [System.Web.Http.HttpGet]
        [System.Web.Http.Route("api/Notice/GetAllNotice")]
        public object GetAllNotice(string usercode, string token, string noticeType)
        {
            List<Notice> nulllist = new List<Notice>();

            if (Token.TokenCheck(usercode, token))
            {
                DateTime todayDateTime = dbFunctions.Timezone(DateTime.Now);
                string transdt = todayDateTime.ToString("yyyy-MM-dd");
                List<Notice> list = new List<Notice>();
                SqlConnection con = new SqlConnection(dbFunctions.connection);
                SqlCommand cmd = new SqlCommand(@"SELECT COMPID, CONVERT(NVARCHAR,NOTICEDT, 103) AS NOTICEDTV, NOTICEYY, NOTICESL, NOTICETP, NOTICE, 
                CONVERT(NVARCHAR,EFDT, 103) AS EFDT, CONVERT(NVARCHAR,ETDT, 103) AS ETDT, 
                CASE WHEN STATUS ='A' THEN 'ACTIVE' ELSE 'INACTIVE' END AS STATUS, NOTICEDT
                FROM ASL_NOTICE WHERE NOTICETP='" + noticeType + "' AND '" + transdt + "' BETWEEN EFDT AND ETDT ORDER BY NOTICEDT DESC, NOTICESL", con);
                con.Open();
                SqlDataReader dr = cmd.ExecuteReader();
                while (dr.Read())
                {
                    list.Add(new Notice
                    {
                        CompanyId = dr[0].ToString(),
                        NoticeDate = dr[1].ToString(),
                        NoticeYear = dr[2].ToString(),
                        NoticeSerial = dr[3].ToString(),
                        NoticeType = dr[4].ToString(),
                        NoticeData = dr[5].ToString(),
                        EffectFrom = dr[6].ToString(),
                        EffectTo = dr[7].ToString(),
                        Status = dr[8].ToString()
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
        [System.Web.Http.HttpPost]
        [System.Web.Http.Route("api/FeedBack/CreateFeedBack")]
        public object CreateOrder([FromBody] FeedBack feedBack)
        {
            var token = feedBack.Token;
            var usercode = feedBack.UserCode;

            if (Token.TokenCheck(usercode, token))
            {
                try
                {
                    var partyid = feedBack.PartyId;
                    var imeino = feedBack.ImeiNo;
                    var noticetype = feedBack.FeedBackType;
                    var reason = feedBack.Reasons;
                    var comment = feedBack.Comments;
                    var status = "A";

                    DateTime todayDateTime = dbFunctions.Timezone(DateTime.Now);
                    string companyid = "101";
                    int year = todayDateTime.Year;
                    string transdt = todayDateTime.ToString("yyyy-MM-dd");
                    string transactionno = StockTransaction.FeedBackTransactionNo(year.ToString());

                    string query = @"INSERT INTO ASL_CS(COMPID, PSID, CSDT, CSYY, CSSL, CSTP, REASONS, 
                    COMMENTS, STATUS, USERPC, USERID, INTIME, IPADDRESS, INSLTUDE) 
                    VALUES('" + companyid + "', '" + partyid + "', '" + transdt + "', '" + year + "', '" + transactionno + "', '" + noticetype + "', N'" + reason +
                    "', N'" + comment + "', '" + status + "', 'ANDROID APP', '" + usercode + "', '" + todayDateTime + "', '" + imeino + "', '')";

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
        [System.Web.Http.Route("api/Notice/GetAllFeedBack")]
        public object GetAllFeedBack(string usercode, string token)
        {
            List<FeedBackReturn> nulllist = new List<FeedBackReturn>();

            if (Token.TokenCheck(usercode, token))
            {
                List<FeedBackReturn> list = new List<FeedBackReturn>();
                SqlConnection con = new SqlConnection(dbFunctions.connection);
                SqlCommand cmd = new SqlCommand(@"SELECT COMPID, PSID, REPLACE(CONVERT(NVARCHAR,CSDT,106),' ','-') CSDT, CSYY, 
                CSSL, CSTP, REASONS, COMMENTS, STATUS
                FROM ASL_CS WHERE USERID='" + usercode + "' AND STATUS='A'  ORDER BY INTIME DESC", con);
                con.Open();
                SqlDataReader dr = cmd.ExecuteReader();
                while (dr.Read())
                {
                    list.Add(new FeedBackReturn
                    {
                        CompanyId = dr[0].ToString(),
                        PartyId = dr[1].ToString(),
                        Date = dr[2].ToString(),
                        Year = dr[3].ToString(),
                        SerialNo = dr[4].ToString(),
                        FeedBackType = dr[5].ToString(),
                        Reasons = dr[6].ToString(),
                        Comments = dr[7].ToString(),
                        Status = dr[8].ToString()
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
