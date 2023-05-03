using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Providers.Entities;
using AlchemyAccounting;

namespace DynamicMenu
{
    public class UserPermissionChecker
    {
        public static bool checkParmit(string formLink, string permissionType)
        {
            string moduleId = "";
            string menuId = "";
            moduleId = dbFunctions.StringData("SELECT MODULEID FROM ASL_MENU WHERE FLINK='" + formLink + "'");
            menuId = dbFunctions.StringData("SELECT MENUID FROM ASL_MENU WHERE FLINK='" + formLink + "'");

            string userId = HttpContext.Current.Session["USERID"].ToString();

            string permission = dbFunctions.StringData(@"SELECT CASE(" + permissionType + ") WHEN 'A' THEN 'true' else 'false' end As Status " +
            "FROM ASL_ROLE WHERE USERID='" + userId + "' AND MODULEID='" + moduleId + "' AND MENUID='" + menuId + "';");
            if (permission == "")
                permission = "false";

            return Convert.ToBoolean(permission);
        }
    }
}