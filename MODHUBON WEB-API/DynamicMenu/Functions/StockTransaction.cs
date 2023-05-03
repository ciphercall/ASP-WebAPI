using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using AlchemyAccounting;

namespace DynamicMenu.Functions
{
    public class StockTransaction
    {
        public static string OrderTransactionNo(string year)
        {
            string transno = "";
            var maxid = dbFunctions.StringData(@"SELECT DISTINCT MAX(TRANSNO) AS TRANSNO FROM STK_TRANS WHERE COMPID='101' 
            AND TRANSYY='" + year + "' AND TRANSTP='IORD'");
            if (maxid == "")
            {
                transno = year.Substring(2, 2) + "000001";
            }
            else
            {
                string id = maxid.Substring(2, 6);
                Int32 idint = Convert.ToInt32(id);
                if (idint < 9)
                {
                    int max = idint + 1;
                    transno = year.Substring(2, 2) + "00000" + max;
                }
                else if (idint < 99)
                {
                    int max = idint + 1;
                    transno = year.Substring(2, 2) + "0000" + max;
                }
                else if (idint < 999)
                {
                    int max = idint + 1;
                    transno = year.Substring(2, 2) + "000" + max;
                }
                else if (idint < 9999)
                {
                    int max = idint + 1;
                    transno = year.Substring(2, 2) + "00" + max;
                }
                else if (idint < 99999)
                {
                    int max = idint + 1;
                    transno = year.Substring(2, 2) + "0" + max;
                }
                else if (idint < 999999)
                {
                    int max = idint + 1;
                    transno = year.Substring(2, 2) + max;
                }
                else
                {
                    transno = "";
                }
            }
            return transno;
        }

        public static string SaleTransactionNo(string year)
        {
            string transno = "";
            var maxid = dbFunctions.StringData(@"SELECT MAX(TRANSNO) AS TRANSNO FROM STK_TRANSMST WHERE COMPID='101' 
            AND TRANSYY='" + year + "' AND TRANSTP='SALE'");
            if (maxid == "")
            {
                transno = year.Substring(2, 2) + "000001";
            }
            else
            {
                string id = maxid.Substring(2, 6);
                Int32 idint = Convert.ToInt32(id);
                if (idint < 9)
                {
                    int max = idint + 1;
                    transno = year.Substring(2, 2) + "00000" + max;
                }
                else if (idint < 99)
                {
                    int max = idint + 1;
                    transno = year.Substring(2, 2) + "0000" + max;
                }
                else if (idint < 999)
                {
                    int max = idint + 1;
                    transno = year.Substring(2, 2) + "000" + max;
                }
                else if (idint < 9999)
                {
                    int max = idint + 1;
                    transno = year.Substring(2, 2) + "00" + max;
                }
                else if (idint < 99999)
                {
                    int max = idint + 1;
                    transno = year.Substring(2, 2) + "0" + max;
                }
                else if (idint < 999999)
                {
                    int max = idint + 1;
                    transno = year.Substring(2, 2) + max;
                }
                else
                {
                    transno = "";
                }
            }
            return transno;
        }
        public static string SaleReturnTransactionNo(string year)
        {
            string transno = "";
            var maxid = dbFunctions.StringData(@"SELECT MAX(TRANSNO) AS TRANSNO FROM STK_TRANSMST WHERE COMPID='101' 
            AND TRANSYY='" + year + "' AND TRANSTP='IRTS'");
            if (maxid == "")
            {
                transno = year.Substring(2, 2) + "000001";
            }
            else
            {
                string id = maxid.Substring(2, 6);
                Int32 idint = Convert.ToInt32(id);
                if (idint < 9)
                {
                    int max = idint + 1;
                    transno = year.Substring(2, 2) + "00000" + max;
                }
                else if (idint < 99)
                {
                    int max = idint + 1;
                    transno = year.Substring(2, 2) + "0000" + max;
                }
                else if (idint < 999)
                {
                    int max = idint + 1;
                    transno = year.Substring(2, 2) + "000" + max;
                }
                else if (idint < 9999)
                {
                    int max = idint + 1;
                    transno = year.Substring(2, 2) + "00" + max;
                }
                else if (idint < 99999)
                {
                    int max = idint + 1;
                    transno = year.Substring(2, 2) + "0" + max;
                }
                else if (idint < 999999)
                {
                    int max = idint + 1;
                    transno = year.Substring(2, 2) + max;
                }
                else
                {
                    transno = "";
                }
            }
            return transno;
        }

        public static string PartySerial(string year, string partyid)
        {
            string transno = "";
            var maxid = dbFunctions.StringData(@"SELECT MAX(PSSL) AS TRANSNO FROM STK_TRANSMST WHERE COMPID='101' 
            AND TRANSYY='" + year + "' AND TRANSTP='IORD'  AND PSID='" + partyid + "'");
            if (maxid == "")
            {
                transno = year.Substring(2, 2) + partyid + "00001";
            }
            else
            {
                string id = maxid.Substring(7, 5);
                Int32 idint = Convert.ToInt32(id);
                if (idint < 9)
                {
                    int max = idint + 1;
                    transno = year.Substring(2, 2) + partyid + "0000" + max;
                }
                else if (idint < 99)
                {
                    int max = idint + 1;
                    transno = year.Substring(2, 2) + partyid + "000" + max;
                }
                else if (idint < 999)
                {
                    int max = idint + 1;
                    transno = year.Substring(2, 2) + partyid + "00" + max;
                }
                else if (idint < 9999)
                {
                    int max = idint + 1;
                    transno = year.Substring(2, 2) + partyid + "0" + max;
                }
                else if (idint < 99999)
                {
                    int max = idint + 1;
                    transno = year.Substring(2, 2) + partyid + max;
                }
                else
                {
                    transno = "";
                }
            }
            return transno;
        }


        public static string SalePartySerial(string year, string partyid)
        {
            string transno = "";
            var maxid = dbFunctions.StringData(@"SELECT MAX(PSSL) AS TRANSNO FROM STK_TRANSMST WHERE COMPID='101' 
            AND TRANSYY='" + year + "' AND TRANSTP='SALE'  AND PSID='" + partyid + "'");
            if (maxid == "")
            {
                transno = year.Substring(2, 2) + partyid + "00001";
            }
            else
            {
                string id = maxid.Substring(7, 5);
                Int32 idint = Convert.ToInt32(id);
                if (idint < 9)
                {
                    int max = idint + 1;
                    transno = year.Substring(2, 2) + partyid + "0000" + max;
                }
                else if (idint < 99)
                {
                    int max = idint + 1;
                    transno = year.Substring(2, 2) + partyid + "000" + max;
                }
                else if (idint < 999)
                {
                    int max = idint + 1;
                    transno = year.Substring(2, 2) + partyid + "00" + max;
                }
                else if (idint < 9999)
                {
                    int max = idint + 1;
                    transno = year.Substring(2, 2) + partyid + "0" + max;
                }
                else if (idint < 99999)
                {
                    int max = idint + 1;
                    transno = year.Substring(2, 2) + partyid + max;
                }
                else
                {
                    transno = "";
                }
            }
            return transno;
        }
        public static string SaleReturnPartySerial(string year, string partyid)
        {
            string transno = "";
            var maxid = dbFunctions.StringData(@"SELECT MAX(PSSL) AS TRANSNO FROM STK_TRANSMST WHERE COMPID='101' 
            AND TRANSYY='" + year + "' AND TRANSTP='IRTS'  AND PSID='" + partyid + "'");
            if (maxid == "")
            {
                transno = year.Substring(2, 2) + partyid + "00001";
            }
            else
            {
                string id = maxid.Substring(7, 5);
                Int32 idint = Convert.ToInt32(id);
                if (idint < 9)
                {
                    int max = idint + 1;
                    transno = year.Substring(2, 2) + partyid + "0000" + max;
                }
                else if (idint < 99)
                {
                    int max = idint + 1;
                    transno = year.Substring(2, 2) + partyid + "000" + max;
                }
                else if (idint < 999)
                {
                    int max = idint + 1;
                    transno = year.Substring(2, 2) + partyid + "00" + max;
                }
                else if (idint < 9999)
                {
                    int max = idint + 1;
                    transno = year.Substring(2, 2) + partyid + "0" + max;
                }
                else if (idint < 99999)
                {
                    int max = idint + 1;
                    transno = year.Substring(2, 2) + partyid + max;
                }
                else
                {
                    transno = "";
                }
            }
            return transno;
        }
        public static string FeedBackTransactionNo(string year)
        {
            string transno = "";
            var maxid = dbFunctions.StringData(@"SELECT MAX(CSSL) FROM ASL_CS WHERE COMPID='101' AND CSYY='" + year + "'");
            if (maxid == "")
            {
                transno = year + "0001";
            }
            else
            {
                string id = maxid.Substring(4, 4);
                Int32 idint = Convert.ToInt32(id);
                if (idint < 9)
                {
                    int max = idint + 1;
                    transno = year+ "000" + max;
                }
                else if (idint < 99)
                {
                    int max = idint + 1;
                    transno = year + "00" + max;
                }
                else if (idint < 999)
                {
                    int max = idint + 1;
                    transno = year+ "0" + max;
                }
                else if (idint < 9999)
                {
                    int max = idint + 1;
                    transno = year+ max;
                }
                else
                {
                    transno = "";
                }
            }
            return transno;
        }
    }
}