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
    public class LogInController : ApiController
    {
        [System.Web.Http.HttpPost]
        [System.Web.Http.Route("api/LogIn/PartyLogin")]
        public object PartyLogin(string loginuserid, string password)
        {
            AllUserInformation emptyuserInformation = new AllUserInformation();

            if (Login.PartyLoginCheck(loginuserid, password))
            {
                //collect user info start
                AllUserInformation userInformation = new AllUserInformation();
                userInformation = Login.CollectUserInfo(loginuserid);
                //collect user info end

                //Generate token start
                TokenUserInfo tokenUserInfo = new TokenUserInfo();
                tokenUserInfo.UserName = userInformation.UserName;
                tokenUserInfo.MobileNo = userInformation.Mobile1;
                tokenUserInfo.UserCreateDate = userInformation.UserCreateDate;

                string token = Token.GenerateToken(tokenUserInfo);
                string tokendate = Token.TokenExpireDateIncrement(DateTime.Now, 1);
                //Generate token end
                var s = Token.SaveToken(userInformation.UserCode, loginuserid, token, tokendate, "PARTY");
                if (s)
                {
                    return new
                    {
                        Token = token,
                        Data = userInformation,
                        Success = true,
                        Message = ""
                    };
                }
            }
            else
            {
                return new
                {
                    Data = emptyuserInformation,
                    Success = false,
                    Message = "Login id password missmatch"
                };
            }
            return new
            {
                Data = emptyuserInformation,
                Success = false,
                Message = "Login id password missmatch"
            };
        }
        [System.Web.Http.HttpGet]
        [System.Web.Http.Route("api/LogIn/GetOrderTimePeriod")]
        public object GetOrderTimePeriod(string usercode, string token)
        {
            List<OrderTimePeriod> nulllist = new List<OrderTimePeriod>();

            if (Token.TokenCheck(usercode, token))
            {
                List<OrderTimePeriod> list = new List<OrderTimePeriod>();
                SqlConnection con = new SqlConnection(dbFunctions.connection);
                SqlCommand cmd = new SqlCommand("SELECT ORDERFR, ORDERTO FROM ASL_COMPANY WHERE COMPID='101'", con);
                con.Open();
                SqlDataReader dr = cmd.ExecuteReader();
                while (dr.Read())
                {
                    list.Add(new OrderTimePeriod
                    {
                        TimeFrom = dr[0].ToString(),
                        TimeTo = dr[1].ToString(),
                        CurrentTime = dbFunctions.Timezone(DateTime.Now).ToString("HH:mm")
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
