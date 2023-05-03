using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Web;
using AlchemyAccounting;

namespace DynamicMenu.Functions
{
    public class Token
    {
        static readonly IFormatProvider _dateformat = new CultureInfo("fr-FR", true);
        public static string GenerateToken(TokenUserInfo userInfo)
        {
            string datetime = dbFunctions.Timezone(DateTime.Now).ToString(CultureInfo.InvariantCulture);
            var chars = userInfo.UserName + userInfo.MobileNo + datetime + userInfo.UserName + datetime + userInfo.UserCreateDate + userInfo.MobileNo + userInfo.UserName;
            chars = SpecialCharRemove(chars);

            var stringChars = new char[50];
            var random = new Random();

            for (int i = 0; i < stringChars.Length; i++)
            {
                stringChars[i] = chars[random.Next(chars.Length)];
            }

            return new String(stringChars);
        }

        public static bool SaveToken(string userid, string loginid, string token, string expeiredate, string usertype)
        {
            var s = dbFunctions.ExecuteQuery("UPDATE STK_USERPS SET APITOKEN='" + token + "', APITOKENEXPTM ='" + expeiredate +
                 "' WHERE USERCD='" + userid + "' AND LOGINID='" + loginid + "' AND COMPID='101' AND PSTP='" + usertype + "'");
            if (s == "")
            {
                return true;
            }
            return false;
        }
        public static string TokenExpireDateIncrement(DateTime dt, int days)
        {
            string incremendate = dt.AddDays(days).ToString(CultureInfo.InvariantCulture);
            return incremendate;
        }

        public static string SpecialCharRemove(string character)
        {
            return character.Replace(" ", "").Replace("/", "").Replace(":", "");
        }
        public static bool TokenCheck(string usercode, string token)
        {
            var data = dbFunctions.DataReaderAdd(@"SELECT APITOKEN, APITOKENEXPTM FROM STK_USERPS WHERE USERCD='" + usercode + "'");
            try
            {
                string apitoken = data[0];
                string tokenExpireDate = data[1];

                DateTime transdate = DateTime.Parse(tokenExpireDate);
                DateTime dateTimeNow = dbFunctions.Timezone(DateTime.Now);

                if (transdate > dateTimeNow && apitoken == token)
                    return true;
            }
            catch (Exception)
            {
                return false;
            }
            return false;
        }
    }

    public class TokenUserInfo
    {
        public String UserName { get; set; }
        public String MobileNo { get; set; }
        public String UserCreateDate { get; set; }
    }
}