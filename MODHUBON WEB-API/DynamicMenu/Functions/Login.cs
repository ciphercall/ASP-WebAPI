using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Web;
using AlchemyAccounting;

namespace DynamicMenu.Functions
{
    public class Login
    {
        static readonly IFormatProvider _dateformat = new CultureInfo("fr-FR", true);
        public static bool PartyLoginCheck(string loginid, string userpass)
        {
            string dbpass = dbFunctions.StringData(@"SELECT STK_USERPS.LOGINPW FROM STK_USERPS INNER JOIN 
            STK_PARTY ON STK_USERPS.COMPID = STK_PARTY.COMPID AND STK_USERPS.PSID = STK_PARTY.PARTYID 
            WHERE (STK_USERPS.LOGINID = '" + loginid + "') AND (STK_USERPS.STATUS = 'A') AND (STK_PARTY.STATUS = 'A')");
            if (dbpass == userpass)
                return true;
            return false;
        }

        public static AllUserInformation CollectUserInfo(string loginid)
        {
            var dbpass = dbFunctions.DataReaderAdd($@"SELECT        STK_USERPS.COMPID, STK_USERPS.PSTP, STK_USERPS.PSID, STK_USERPS.USERCD, STK_USERPS.USERNM, STK_USERPS.MOBNO1, STK_USERPS.MOBNO2, STK_USERPS.STATUS, STK_USERPS.INTIME,
            STK_PARTY.PARTYNM FROM STK_USERPS INNER JOIN
            STK_PARTY ON STK_USERPS.COMPID = STK_PARTY.COMPID AND STK_USERPS.PSID = STK_PARTY.PARTYID
            WHERE (STK_USERPS.LOGINID = '{loginid}') AND (STK_USERPS.STATUS = 'A')");
            AllUserInformation userInformation = new AllUserInformation
            {
                ComapnyId = dbpass[0],
                UserType = dbpass[1],
                PsId = dbpass[2],
                UserCode = dbpass[3],
                UserName = dbpass[4],
                Mobile1 = dbpass[5],
                Mobile2 = dbpass[6],
                Status = dbpass[7],
                UserCreateDate = dbpass[8],
                PartyName = dbpass[9]
            };
            return userInformation;
        }

    }

    public class AllUserInformation
    {
        public string ComapnyId { get; set; }
        public string UserType { get; set; }
        public string PsId { get; set; }
        public string PartyName { get; set; }
        public string UserCode { get; set; }
        public string UserName { get; set; }
        public string Mobile1 { get; set; }
        public string Mobile2 { get; set; }
        public string Status { get; set; }
        public string UserCreateDate { get; set; }
    }
    public class UserTokenInformation
    {
        public string TokenId { get; set; }
        public string ToeknExpDate { get; set; }
    }
    public class OrderTimePeriod
    {
        public string TimeFrom { get; set; }
        public string TimeTo { get; set; }
        public string CurrentTime { get; set; }
    }
}