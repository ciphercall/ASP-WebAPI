using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Providers.Entities;
using System.Web.UI.WebControls;
using AlchemyAccounting;
using DynamicMenu.logData.DataAccess;
using DynamicMenu.LogData.Interface;

namespace DynamicMenu.LogData
{
    
    public class LogDataFunction
    {
        static LogDataAccess dob = new LogDataAccess();
        static LogDataInterface iob = new LogDataInterface();
        public static void InsertLogData(string lotiLengtude, string logType, string tableId, string logData, string ipAddress)
        {
            try
            {
                iob.LotiLengTudeInsert = lotiLengtude;
                iob.IpAddressInsert = ipAddress;
                iob.UserIdInsert = Convert.ToInt64(HttpContext.Current.Session["USERID"].ToString());
                iob.UserPcInsert = dbFunctions.UserPc();
                iob.InTimeInsert = dbFunctions.Timezone(DateTime.Now);

                string logSl = dbFunctions.StringData("SELECT ISNULL(MAX(LOGSLNO+1),1) AS MAXLOGSLNO FROM ASL_LOG WHERE USERID= "+iob.UserIdInsert+"");
                iob.LogSlNo = Convert.ToInt64(logSl);
                iob.LogType = logType;
                iob.CompanyId = Convert.ToInt64(HttpContext.Current.Session["COMPANYID"].ToString());
                iob.CompanyUserId = Convert.ToInt64(HttpContext.Current.Session["USERID"].ToString());
                iob.TableId = tableId;
                iob.LogDatA = logData;
                dob.INSERT_ASL_LOG(iob);
            }
            catch (Exception)
            {
                
            }

        }
    }
}