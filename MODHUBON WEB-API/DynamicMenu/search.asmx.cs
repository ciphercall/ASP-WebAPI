using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Services;
using System.Web.Script.Services;
using System.Data.SqlClient;
using DynamicMenu.Functions;
using static AlchemyAccounting.ThreeValuePass;

namespace AlchemyAccounting
{
    /// <summary>
    /// Summary description for search
    /// </summary>
    [WebService(Namespace = "http://tempuri.org/")]
    [WebServiceBinding(ConformsTo = WsiProfiles.BasicProfile1_1)]
    [System.ComponentModel.ToolboxItem(false)]
    // To allow this Web Service to be called from script, using ASP.NET AJAX, uncomment the following line. 
    [System.Web.Script.Services.ScriptService]

    public class search : System.Web.Services.WebService
    {
        IFormatProvider dateformat = new System.Globalization.CultureInfo("fr-FR", true);
        [WebMethod(EnableSession = true)]
        public string checkSession(string Session)
        {
            string sess = HttpContext.Current.Session["" + Session + ""].ToString();
            return sess;
        }
        [WebMethod]
        [ScriptMethod(ResponseFormat = ResponseFormat.Json)]
        public List<TowValue> GetCompletionListOpeningBalanceEntryAccountNM(string term)
        {
            List<string> s = new List<string>();
            List<TowValue> lst = new List<TowValue>();
            SqlConnection conn = new SqlConnection(dbFunctions.connection);
            using (SqlCommand obj_Sqlcommand = new SqlCommand(@"SELECT ACCOUNTNM AS txt, ACCOUNTCD AS txtid FROM GL_ACCHART WHERE STATUSCD='P' and ACCOUNTNM LIKE  @SearchText +'%'", conn))
            {
                conn.Open();
                obj_Sqlcommand.Parameters.AddWithValue("@SearchText", term);
                SqlDataReader obj_result = obj_Sqlcommand.ExecuteReader();
                while (obj_result.Read())
                {
                    string result = obj_result["txt"].ToString().TrimEnd();
                    string id = obj_result["txtid"].ToString().TrimEnd();
                    lst.Add(new TowValue { value = id, label = result });
                }
                var q = lst.ToList();
                return (List<TowValue>)q;
            }
        }


        [WebMethod]
        [ScriptMethod(ResponseFormat = ResponseFormat.Json)]
        public List<string> GetCompletionListCompany(string txt)
        {
            // your code to query the database goes here
            List<string> result = new List<string>();

            SqlConnection conn = new SqlConnection(dbFunctions.connection);
            using (
                SqlCommand obj_Sqlcommand =
                    new SqlCommand(
                        "SELECT COMPNM AS txt FROM ASL_COMPANY WHERE COMPNM LIKE @SearchText +'%' ORDER BY COMPNM", conn)
                )
            {
                conn.Open();
                obj_Sqlcommand.Parameters.AddWithValue("@SearchText", txt);
                SqlDataReader obj_result = obj_Sqlcommand.ExecuteReader();
                while (obj_result.Read())
                {
                    result.Add(obj_result["txt"].ToString().TrimEnd());
                }
            }
            return result;
        }


        [WebMethod]
        [ScriptMethod(ResponseFormat = ResponseFormat.Json)]
        public List<string> GetCompletionListCostPoolName(string txt)
        {
            // your code to query the database goes here
            List<string> result = new List<string>();

            SqlConnection conn = new SqlConnection(dbFunctions.connection);
            using (
                SqlCommand obj_Sqlcommand =
                    new SqlCommand("SELECT CATNM AS txt FROM GL_COSTPMST WHERE CATNM LIKE @SearchText +'%'", conn))
            {
                conn.Open();
                obj_Sqlcommand.Parameters.AddWithValue("@SearchText", txt);
                SqlDataReader obj_result = obj_Sqlcommand.ExecuteReader();
                while (obj_result.Read())
                {
                    result.Add(obj_result["txt"].ToString().TrimEnd());
                }
            }
            return result;
        }

        [WebMethod]

        [ScriptMethod(ResponseFormat = ResponseFormat.Json)]
        public List<string> GetCompletionListModuleName(string txt)
        {
            // your code to query the database goes here
            List<string> result = new List<string>();

            SqlConnection conn = new SqlConnection(dbFunctions.connection);
            using (
                SqlCommand obj_Sqlcommand =
                    new SqlCommand(
                        "SELECT MODULENM AS txt FROM ASL_MENUMST WHERE MODULENM LIKE @SearchText +'%' ORDER BY MODULENM",
                        conn))
            {
                conn.Open();
                obj_Sqlcommand.Parameters.AddWithValue("@SearchText", txt);
                SqlDataReader obj_result = obj_Sqlcommand.ExecuteReader();
                while (obj_result.Read())
                {
                    result.Add(obj_result["txt"].ToString().TrimEnd());
                }
            }
            return result;
        }

        [WebMethod(EnableSession = true)]
        [ScriptMethod(ResponseFormat = ResponseFormat.Json)]
        public List<string> GetCompletionListMenuName(string txt)
        {
            // your code to query the database goes here
            List<string> result = new List<string>();
            string moduleId = "";
            if (HttpContext.Current.Session["ModuleId"] != null)
                moduleId = HttpContext.Current.Session["ModuleId"].ToString();
            SqlConnection conn = new SqlConnection(dbFunctions.connection);
            using (
                SqlCommand obj_Sqlcommand =
                    new SqlCommand(
                        "SELECT MENUNM AS txt FROM ASL_MENU WHERE MODULEID='" + moduleId +
                        "' AND MENUNM LIKE  @SearchText +'%' ORDER BY MENUNM", conn))
            {
                conn.Open();
                obj_Sqlcommand.Parameters.AddWithValue("@SearchText", txt);
                SqlDataReader obj_result = obj_Sqlcommand.ExecuteReader();
                while (obj_result.Read())
                {
                    result.Add(obj_result["txt"].ToString().TrimEnd());
                }
            }
            return result;
        }



        [WebMethod(EnableSession = true)]
        [ScriptMethod(ResponseFormat = ResponseFormat.Json)]
        public List<string> GetCompletionListMenuNameByType(string txt)
        {
            // your code to query the database goes here
            List<string> result = new List<string>();
            string moduleId = "";
            string menuType = "";
            if (HttpContext.Current.Session["ModuleId"] != null && HttpContext.Current.Session["MenuType"] != null)
            {
                moduleId = HttpContext.Current.Session["ModuleId"].ToString();
                menuType = HttpContext.Current.Session["MenuType"].ToString();
            }
            SqlConnection conn = new SqlConnection(dbFunctions.connection);
            using (
                SqlCommand obj_Sqlcommand =
                    new SqlCommand(
                        "SELECT MENUNM AS txt FROM ASL_MENU WHERE MODULEID='" + moduleId + "' AND  MENUTP='" + menuType +
                        "' AND MENUNM LIKE  @SearchText +'%' ORDER BY MENUNM", conn))
            {
                conn.Open();
                obj_Sqlcommand.Parameters.AddWithValue("@SearchText", txt);
                SqlDataReader obj_result = obj_Sqlcommand.ExecuteReader();
                while (obj_result.Read())
                {
                    result.Add(obj_result["txt"].ToString().TrimEnd());
                }
            }
            return result;
        }


        [WebMethod(EnableSession = true)]
        [ScriptMethod(ResponseFormat = ResponseFormat.Json)]
        public List<string> GetCompletionListUserNameForMenuRole(string txt)
        {
            // your code to query the database goes here
            List<string> result = new List<string>();
            string usrid = "";
          //  string menuType = "";
            if (HttpContext.Current.Session["COMPANYID"] != null)
            {
                usrid = HttpContext.Current.Session["COMPANYID"].ToString();
                //menuType = HttpContext.Current.Session["MenuType"].ToString();
            }
            SqlConnection conn = new SqlConnection(dbFunctions.connection);
            using (
                SqlCommand obj_Sqlcommand =
                    new SqlCommand(@"SELECT DISTINCT ASL_USERCO.USERNM AS txt FROM ASL_USERCO INNER JOIN
ASL_ROLE ON ASL_USERCO.COMPID = ASL_ROLE.COMPID AND ASL_USERCO.USERID = ASL_ROLE.USERID 
WHERE ASL_USERCO.COMPID!='" + usrid + "' AND ASL_USERCO.USERID!='" + usrid +
                                   "01' AND ASL_USERCO.USERNM LIKE @SearchText +'%' ORDER BY txt", conn))
            {
                conn.Open();
                obj_Sqlcommand.Parameters.AddWithValue("@SearchText", txt);
                SqlDataReader obj_result = obj_Sqlcommand.ExecuteReader();
                while (obj_result.Read())
                {
                    result.Add(obj_result["txt"].ToString().TrimEnd());
                }
            }
            return result;
        }


        [WebMethod(EnableSession = true)]
        [ScriptMethod(ResponseFormat = ResponseFormat.Json)]
        public List<string> GetCompletionListCostPool(string txt)
        {
            // your code to query the database goes here
            List<string> result = new List<string>();
            string uTp = "";
            string brCD = "";
            string query = "";
            if (HttpContext.Current.Session["USERTYPE"] != null && HttpContext.Current.Session["BrCD"] != null)
            {
                uTp = HttpContext.Current.Session["USERTYPE"].ToString();
                brCD = HttpContext.Current.Session["BrCD"].ToString();
            }
            SqlConnection conn = new SqlConnection(dbFunctions.connection);
            SqlCommand cmd = new SqlCommand();
            //if (uTp == "COMPADMIN")
            query = "SELECT COSTPNM AS txt FROM GL_COSTP WHERE COSTPNM LIKE @SearchText +'%'";
            //else
            //    query = "SELECT COSTPNM AS txt FROM GL_COSTP WHERE COSTPNM LIKE @SearchText +'%' AND CATID ='" + brCD + "'";
            using (SqlCommand obj_Sqlcommand = new SqlCommand(query, conn))
            {
                conn.Open();
                obj_Sqlcommand.Parameters.AddWithValue("@SearchText", txt);
                SqlDataReader obj_result = obj_Sqlcommand.ExecuteReader();
                while (obj_result.Read())
                {
                    result.Add(obj_result["txt"].ToString().TrimEnd());
                }
            }
            return result;
        }

        [WebMethod(EnableSession = true)]
        [ScriptMethod(ResponseFormat = ResponseFormat.Json)]
        public List<string> GetCompletionListCostPoolSingleVEdit(string txt)
        {
            // your code to query the database goes here
            List<string> result = new List<string>();
            string uTp = "";
            string brCD = "";
            string query = "";
            if (HttpContext.Current.Session["USERTYPE"] != null && HttpContext.Current.Session["BrCD"] != null)
            {
                uTp = HttpContext.Current.Session["USERTYPE"].ToString();
                brCD = HttpContext.Current.Session["BrCD"].ToString();
            }
            SqlConnection conn = new SqlConnection(dbFunctions.connection);
            SqlCommand cmd = new SqlCommand();
            //if (uTp == "COMPADMIN")
            query = "SELECT (GL_COSTP.COSTPNM + '|' + GL_COSTPMST.CATNM) AS txt FROM GL_COSTP INNER JOIN GL_COSTPMST ON GL_COSTP.CATID = GL_COSTPMST.CATID WHERE (GL_COSTP.COSTPNM + ' - ' + GL_COSTPMST.CATNM) LIKE  @SearchText +'%'";
            //else
            //    query = "SELECT (GL_COSTP.COSTPNM + '|' + GL_COSTPMST.CATNM) AS txt FROM GL_COSTP INNER JOIN GL_COSTPMST ON GL_COSTP.CATID = GL_COSTPMST.CATID WHERE (GL_COSTP.COSTPNM + ' - ' + GL_COSTPMST.CATNM) LIKE  @SearchText +'%' AND GL_COSTP.CATID ='" + brCD + "'";
            using (SqlCommand obj_Sqlcommand = new SqlCommand(query, conn))
            {
                conn.Open();
                obj_Sqlcommand.Parameters.AddWithValue("@SearchText", txt);
                SqlDataReader obj_result = obj_Sqlcommand.ExecuteReader();
                while (obj_result.Read())
                {
                    result.Add(obj_result["txt"].ToString().TrimEnd());
                }
            }
            return result;
        }



        [WebMethod(EnableSession = true)]
        [ScriptMethod(ResponseFormat = ResponseFormat.Json)]
        public List<string> GetCompletionListMrecD(string txt)
        {
            // your code to query the database goes here
            List<string> result = new List<string>();
            string uTp = "";
            string brCD = "";
            string query = "";
            if (HttpContext.Current.Session["USERTYPE"] != null && HttpContext.Current.Session["BrCD"] != null)
            {
                uTp = HttpContext.Current.Session["USERTYPE"].ToString();
                brCD = HttpContext.Current.Session["BrCD"].ToString();
            }
            SqlConnection conn = new SqlConnection(dbFunctions.connection);
            SqlCommand cmd = new SqlCommand();
            // if (uTp == "COMPADMIN")
            query = "SELECT ACCOUNTNM AS txt FROM GL_ACCHART WHERE substring(ACCOUNTCD,1,7) in ('1020101','1020102','2020103') and STATUSCD = 'P' and ACCOUNTNM LIKE @SearchText +'%'";
            //else
            //    query = "SELECT ACCOUNTNM AS txt FROM GL_ACCHART WHERE substring(ACCOUNTCD,1,7) in ('1020101','1020102','2020103') and STATUSCD = 'P' and ACCOUNTNM LIKE @SearchText +'%' AND (BRANCHCD ='" + brCD + "' OR BRANCHCD IS NULL OR BRANCHCD ='')";
            using (SqlCommand obj_Sqlcommand = new SqlCommand(query, conn))
            {
                conn.Open();
                obj_Sqlcommand.Parameters.AddWithValue("@SearchText", txt);
                SqlDataReader obj_result = obj_Sqlcommand.ExecuteReader();
                while (obj_result.Read())
                {
                    result.Add(obj_result["txt"].ToString().TrimEnd());
                }
            }
            return result;
        }

        [WebMethod(EnableSession = true)]
        [ScriptMethod(ResponseFormat = ResponseFormat.Json)]
        public List<string> GetCompletionListMrecC(string txt)
        {
            // your code to query the database goes here
            List<string> result = new List<string>();
            string uTp = "";
            string brCD = "";
            string query = "";
            if (HttpContext.Current.Session["USERTYPE"] != null && HttpContext.Current.Session["BrCD"] != null)
            {
                uTp = HttpContext.Current.Session["USERTYPE"].ToString();
                brCD = HttpContext.Current.Session["BrCD"].ToString();
            }
            SqlConnection conn = new SqlConnection(dbFunctions.connection);
            SqlCommand cmd = new SqlCommand();
            // if (uTp == "COMPADMIN")
            query = "SELECT ACCOUNTNM AS txt FROM GL_ACCHART WHERE substring(ACCOUNTCD,1,7) not in ('1020101','1020102','2020103') and STATUSCD = 'P' and ACCOUNTNM LIKE  @SearchText +'%'";
            //else
            // query = "SELECT ACCOUNTNM AS txt FROM GL_ACCHART WHERE substring(ACCOUNTCD,1,7) not in ('1020101','1020102','2020103') and STATUSCD = 'P' and ACCOUNTNM LIKE @SearchText +'%' AND (BRANCHCD ='" + brCD + "' OR BRANCHCD IS NULL OR BRANCHCD ='')";
            using (SqlCommand obj_Sqlcommand = new SqlCommand(query, conn))
            {
                conn.Open();
                obj_Sqlcommand.Parameters.AddWithValue("@SearchText", txt);
                SqlDataReader obj_result = obj_Sqlcommand.ExecuteReader();
                while (obj_result.Read())
                {
                    result.Add(obj_result["txt"].ToString().TrimEnd());
                }
            }
            return result;
        }

        [WebMethod(EnableSession = true)]
        [ScriptMethod(ResponseFormat = ResponseFormat.Json)]
        public List<string> GetCompletionListMpayD(string txt)
        {
            // your code to query the database goes here
            List<string> result = new List<string>();
            string uTp = "";
            string brCD = "";
            string query = "";
            if (HttpContext.Current.Session["USERTYPE"] != null && HttpContext.Current.Session["BrCD"] != null)
            {
                uTp = HttpContext.Current.Session["USERTYPE"].ToString();
                brCD = HttpContext.Current.Session["BrCD"].ToString();
            }
            SqlConnection conn = new SqlConnection(dbFunctions.connection);
            SqlCommand cmd = new SqlCommand();
            //if (uTp == "COMPADMIN")
            query = "SELECT ACCOUNTNM AS txt FROM GL_ACCHART WHERE  substring(ACCOUNTCD,1,7) not in ('1020101','1020102','2020103') and STATUSCD = 'P' and ACCOUNTNM LIKE  @SearchText +'%'";
            // else
            //   query = "SELECT ACCOUNTNM AS txt FROM GL_ACCHART WHERE  substring(ACCOUNTCD,1,7) not in ('1020101','1020102','2020103') and STATUSCD = 'P' and ACCOUNTNM LIKE @SearchText +'%' AND (BRANCHCD ='" + brCD + "' OR BRANCHCD IS NULL OR BRANCHCD ='')";
            using (SqlCommand obj_Sqlcommand = new SqlCommand(query, conn))
            {
                conn.Open();
                obj_Sqlcommand.Parameters.AddWithValue("@SearchText", txt);
                SqlDataReader obj_result = obj_Sqlcommand.ExecuteReader();
                while (obj_result.Read())
                {
                    result.Add(obj_result["txt"].ToString().TrimEnd());
                }
            }
            return result;
        }

        [WebMethod(EnableSession = true)]
        [ScriptMethod(ResponseFormat = ResponseFormat.Json)]
        public List<string> GetCompletionListMpayC(string txt)
        {
            // your code to query the database goes here
            List<string> result = new List<string>();
            string uTp = "";
            string brCD = "";
            string query = "";
            if (HttpContext.Current.Session["USERTYPE"] != null && HttpContext.Current.Session["BrCD"] != null)
            {
                uTp = HttpContext.Current.Session["USERTYPE"].ToString();
                brCD = HttpContext.Current.Session["BrCD"].ToString();
            }
            SqlConnection conn = new SqlConnection(dbFunctions.connection);
            SqlCommand cmd = new SqlCommand();
            //if (uTp == "COMPADMIN")
            query = "SELECT ACCOUNTNM AS txt FROM GL_ACCHART WHERE  substring(ACCOUNTCD,1,7)  in ('1020101','1020102','2020103') and STATUSCD = 'P' and ACCOUNTNM LIKE  @SearchText +'%'";
            //  else
            //    query = "SELECT ACCOUNTNM AS txt FROM GL_ACCHART WHERE  substring(ACCOUNTCD,1,7)  in ('1020101','1020102','2020103') and STATUSCD = 'P' and ACCOUNTNM LIKE  @SearchText +'%'  AND (BRANCHCD ='" + brCD + "' OR BRANCHCD IS NULL OR BRANCHCD ='')";
            using (SqlCommand obj_Sqlcommand = new SqlCommand(query, conn))
            {
                conn.Open();
                obj_Sqlcommand.Parameters.AddWithValue("@SearchText", txt);
                SqlDataReader obj_result = obj_Sqlcommand.ExecuteReader();
                while (obj_result.Read())
                {
                    result.Add(obj_result["txt"].ToString().TrimEnd());
                }
            }
            return result;
        }

        [WebMethod(EnableSession = true)]
        [ScriptMethod(ResponseFormat = ResponseFormat.Json)]
        public List<string> GetCompletionListJourD(string txt)
        {
            // your code to query the database goes here
            List<string> result = new List<string>();
            string uTp = "";
            string brCD = "";
            string query = "";
            if (HttpContext.Current.Session["USERTYPE"] != null && HttpContext.Current.Session["BrCD"] != null)
            {
                uTp = HttpContext.Current.Session["USERTYPE"].ToString();
                brCD = HttpContext.Current.Session["BrCD"].ToString();
            }
            SqlConnection conn = new SqlConnection(dbFunctions.connection);
            SqlCommand cmd = new SqlCommand();
            // if (uTp == "COMPADMIN")
            query = "SELECT ACCOUNTNM AS txt FROM GL_ACCHART WHERE substring(ACCOUNTCD,1,7) not in ('1020101','1020102','2020103') and STATUSCD = 'P' and ACCOUNTNM LIKE  @SearchText +'%'";
            // else
            //   query = "SELECT ACCOUNTNM AS txt FROM GL_ACCHART WHERE substring(ACCOUNTCD,1,7) not in ('1020101','1020102','2020103') and STATUSCD = 'P' and ACCOUNTNM LIKE  @SearchText +'%' AND (BRANCHCD ='" + brCD + "' OR BRANCHCD IS NULL OR BRANCHCD ='')";
            using (SqlCommand obj_Sqlcommand = new SqlCommand(query, conn))
            {
                conn.Open();
                obj_Sqlcommand.Parameters.AddWithValue("@SearchText", txt);
                SqlDataReader obj_result = obj_Sqlcommand.ExecuteReader();
                while (obj_result.Read())
                {
                    result.Add(obj_result["txt"].ToString().TrimEnd());
                }
            }
            return result;
        }
        [WebMethod(EnableSession = true)]
        [ScriptMethod(ResponseFormat = ResponseFormat.Json)]
        public List<string> GetCompletionListJourC(string txt)
        {
            // your code to query the database goes here
            List<string> result = new List<string>();
            string uTp = "";
            string brCD = "";
            string query = "";
            if (HttpContext.Current.Session["USERTYPE"] != null && HttpContext.Current.Session["BrCD"] != null)
            {
                uTp = HttpContext.Current.Session["USERTYPE"].ToString();
                brCD = HttpContext.Current.Session["BrCD"].ToString();
            }
            SqlConnection conn = new SqlConnection(dbFunctions.connection);
            SqlCommand cmd = new SqlCommand();
            // if (uTp == "COMPADMIN")
            query = "SELECT ACCOUNTNM AS txt FROM GL_ACCHART WHERE substring(ACCOUNTCD,1,7) not in ('1020101','1020102','2020103') and STATUSCD = 'P' and ACCOUNTNM LIKE @SearchText +'%'";
            // else
            // query = "SELECT ACCOUNTNM AS txt FROM GL_ACCHART WHERE substring(ACCOUNTCD,1,7) not in ('1020101','1020102','2020103') and STATUSCD = 'P' and ACCOUNTNM LIKE  @SearchText +'%' AND (BRANCHCD ='" + brCD + "' OR BRANCHCD IS NULL OR BRANCHCD ='')";
            using (SqlCommand obj_Sqlcommand = new SqlCommand(query, conn))
            {
                conn.Open();
                obj_Sqlcommand.Parameters.AddWithValue("@SearchText", txt);
                SqlDataReader obj_result = obj_Sqlcommand.ExecuteReader();
                while (obj_result.Read())
                {
                    result.Add(obj_result["txt"].ToString().TrimEnd());
                }
            }
            return result;
        }
        [WebMethod(EnableSession = true)]
        [ScriptMethod(ResponseFormat = ResponseFormat.Json)]
        public List<string> GetCompletionListConD(string txt)
        {
            // your code to query the database goes here
            List<string> result = new List<string>();
            string uTp = "";
            string brCD = "";
            string query = "";
            if (HttpContext.Current.Session["USERTYPE"] != null && HttpContext.Current.Session["BrCD"] != null)
            {
                uTp = HttpContext.Current.Session["USERTYPE"].ToString();
                brCD = HttpContext.Current.Session["BrCD"].ToString();
            }
            SqlConnection conn = new SqlConnection(dbFunctions.connection);
            SqlCommand cmd = new SqlCommand();
            // if (uTp == "COMPADMIN")
            query = "SELECT ACCOUNTNM AS txt FROM GL_ACCHART WHERE substring(ACCOUNTCD,1,7) in ('1020101','1020102','2020103')  and STATUSCD = 'P' and ACCOUNTNM LIKE @SearchText +'%'";
            // else
            // query = "SELECT ACCOUNTNM AS txt FROM GL_ACCHART WHERE substring(ACCOUNTCD,1,7) in ('1020101','1020102','2020103')  and STATUSCD = 'P' and ACCOUNTNM LIKE @SearchText +'%' AND (BRANCHCD ='" + brCD + "' OR BRANCHCD IS NULL OR BRANCHCD ='')";
            using (SqlCommand obj_Sqlcommand = new SqlCommand(query, conn))
            {
                conn.Open();
                obj_Sqlcommand.Parameters.AddWithValue("@SearchText", txt);
                SqlDataReader obj_result = obj_Sqlcommand.ExecuteReader();
                while (obj_result.Read())
                {
                    result.Add(obj_result["txt"].ToString().TrimEnd());
                }
            }
            return result;
        }
        [WebMethod(EnableSession = true)]
        [ScriptMethod(ResponseFormat = ResponseFormat.Json)]
        public List<string> GetCompletionListConC(string txt)
        {
            // your code to query the database goes here
            List<string> result = new List<string>();
            string uTp = "";
            string brCD = "";
            string query = "";
            if (HttpContext.Current.Session["USERTYPE"] != null && HttpContext.Current.Session["BrCD"] != null)
            {
                uTp = HttpContext.Current.Session["USERTYPE"].ToString();
                brCD = HttpContext.Current.Session["BrCD"].ToString();
            }
            SqlConnection conn = new SqlConnection(dbFunctions.connection);
            SqlCommand cmd = new SqlCommand();
            //if (uTp == "COMPADMIN")
            query = "SELECT ACCOUNTNM AS txt FROM GL_ACCHART WHERE substring(ACCOUNTCD,1,7) in ('1020101','1020102','2020103') and STATUSCD = 'P' and ACCOUNTNM LIKE @SearchText +'%'";
            // else
            //   query = "SELECT ACCOUNTNM AS txt FROM GL_ACCHART WHERE substring(ACCOUNTCD,1,7) in ('1020101','1020102','2020103') and STATUSCD = 'P' and ACCOUNTNM LIKE @SearchText +'%' AND (BRANCHCD ='" + brCD + "' OR BRANCHCD IS NULL OR BRANCHCD ='')";
            using (SqlCommand obj_Sqlcommand = new SqlCommand(query, conn))
            {
                conn.Open();
                obj_Sqlcommand.Parameters.AddWithValue("@SearchText", txt);
                SqlDataReader obj_result = obj_Sqlcommand.ExecuteReader();
                while (obj_result.Read())
                {
                    result.Add(obj_result["txt"].ToString().TrimEnd());
                }
            }
            return result;
        }


        [WebMethod(EnableSession = true)]
        [ScriptMethod(ResponseFormat = ResponseFormat.Json)]
        public List<string> GetCompletionListDebit(string txt)
        {
            // your code to query the database goes here
            List<string> result = new List<string>();
            string uTp = "";
            string brCD = "";
            string query = "";
            string Transtype = HttpContext.Current.Session["Transtype"].ToString();
            if (HttpContext.Current.Session["USERTYPE"] != null && HttpContext.Current.Session["BrCD"] != null)
            {
                uTp = HttpContext.Current.Session["USERTYPE"].ToString();
                brCD = HttpContext.Current.Session["BrCD"].ToString();
            }
            SqlConnection conn = new SqlConnection(dbFunctions.connection);
            SqlCommand cmd = new SqlCommand();
            if (Transtype == "MREC")
            {
                // if (uTp == "COMPADMIN")
                //  {
                query = "SELECT ACCOUNTNM AS txt FROM GL_ACCHART WHERE substring(ACCOUNTCD,1,7) in ('1020101','1020102','2020103')  AND STATUSCD = 'P' and ACCOUNTNM LIKE  @SearchText +'%'";
                //  }
                // else
                //  {
                //    query = "SELECT ACCOUNTNM AS txt FROM GL_ACCHART WHERE substring(ACCOUNTCD,1,7) in ('1020101','1020102','2020103')  AND STATUSCD = 'P' and ACCOUNTNM LIKE  @SearchText +'%' AND (BRANCHCD ='" + brCD + "' OR BRANCHCD IS NULL OR BRANCHCD ='')";
                // }
            }

            else if (Transtype == "MPAY")
            {
                //if (uTp == "COMPADMIN")
                //{
                query = "SELECT ACCOUNTNM AS txt FROM GL_ACCHART WHERE substring(ACCOUNTCD,1,7) not in ('1020101','1020102','2020103') AND STATUSCD = 'P' and ACCOUNTNM LIKE  @SearchText +'%'";
                //}
                //else
                //{
                //    query = "SELECT ACCOUNTNM AS txt FROM GL_ACCHART WHERE substring(ACCOUNTCD,1,7) not in ('1020101','1020102','2020103') AND STATUSCD = 'P' and ACCOUNTNM LIKE  @SearchText +'%' AND (BRANCHCD ='" + brCD + "' OR BRANCHCD IS NULL OR BRANCHCD ='')";
                //}
            }
            else if (Transtype == "JOUR")
            {
                //if (uTp == "COMPADMIN")
                //{
                query = "SELECT ACCOUNTNM AS txt FROM GL_ACCHART WHERE substring(ACCOUNTCD,1,7) not in ('1020101','1020102','2020103') AND STATUSCD = 'P' and ACCOUNTNM LIKE  @SearchText +'%'";
                //}
                //else
                //{
                //    query = "SELECT ACCOUNTNM AS txt FROM GL_ACCHART WHERE substring(ACCOUNTCD,1,7) not in ('1020101','1020102','2020103') AND STATUSCD = 'P' and ACCOUNTNM LIKE  @SearchText +'%' AND (BRANCHCD ='" + brCD + "' OR BRANCHCD IS NULL OR BRANCHCD ='')";
                //}
            }
            else if (Transtype == "CONT")
            {
                //if (uTp == "COMPADMIN")
                //{
                query = "SELECT ACCOUNTNM AS txt FROM GL_ACCHART WHERE substring(ACCOUNTCD,1,7) in ('1020101','1020102','2020103') AND STATUSCD = 'P' and ACCOUNTNM LIKE  @SearchText +'%'";
                //}
                //else
                //{
                //    query = "SELECT ACCOUNTNM AS txt FROM GL_ACCHART WHERE substring(ACCOUNTCD,1,7) in ('1020101','1020102','2020103') AND STATUSCD = 'P' and ACCOUNTNM LIKE  @SearchText +'%' AND (BRANCHCD ='" + brCD + "' OR BRANCHCD IS NULL OR BRANCHCD ='')";
                //}
            }
            else
            {
                Transtype = "";
            }
            using (SqlCommand obj_Sqlcommand = new SqlCommand(query, conn))
            {
                conn.Open();
                obj_Sqlcommand.Parameters.AddWithValue("@SearchText", txt);
                SqlDataReader obj_result = obj_Sqlcommand.ExecuteReader();
                while (obj_result.Read())
                {
                    result.Add(obj_result["txt"].ToString().TrimEnd());
                }
            }
            return result;
        }




        [WebMethod(EnableSession = true)]
        [ScriptMethod(ResponseFormat = ResponseFormat.Json)]
        public List<string> GetCompletionListCredit(string txt)
        {
            // your code to query the database goes here
            List<string> result = new List<string>();
            string uTp = "";
            string brCD = "";
            string query = "";
            string Transtype = "";
            if (HttpContext.Current.Session["USERTYPE"] != null && HttpContext.Current.Session["BrCD"] != null)
        //    if (HttpContext.Current.Session["USERTYPE"] != null && HttpContext.Current.Session["BrCD"] != null && HttpContext.Current.Session["Transtype"] != "")
            {
                uTp = HttpContext.Current.Session["USERTYPE"].ToString();
                brCD = HttpContext.Current.Session["BrCD"].ToString();
                Transtype = HttpContext.Current.Session["Transtype"].ToString();
            }
            SqlConnection conn = new SqlConnection(dbFunctions.connection);
            SqlCommand cmd = new SqlCommand();
            if (Transtype == "MREC")
            {
                //if (uTp == "COMPADMIN")
                //{
                query = "SELECT ACCOUNTNM AS txt FROM GL_ACCHART WHERE substring(ACCOUNTCD,1,7) not in ('1020101','1020102','2020103') AND STATUSCD = 'P' and ACCOUNTNM LIKE  @SearchText +'%'";
                //}
                //else
                //{
                //    query = "SELECT ACCOUNTNM AS txt FROM GL_ACCHART WHERE substring(ACCOUNTCD,1,7) not in ('1020101','1020102','2020103') AND STATUSCD = 'P' and ACCOUNTNM LIKE  @SearchText +'%' AND (BRANCHCD ='" + brCD + "' OR BRANCHCD IS NULL OR BRANCHCD ='')";
                //}
            }

            else if (Transtype == "MPAY")
            {
                //if (uTp == "COMPADMIN")
                //{
                query = "SELECT ACCOUNTNM AS txt FROM GL_ACCHART WHERE  substring(ACCOUNTCD,1,7)  in ('1020101','1020102','2020103') AND STATUSCD = 'P' and ACCOUNTNM LIKE  @SearchText +'%'";
                //    }
                //    else
                //    {
                //        query = "SELECT ACCOUNTNM AS txt FROM GL_ACCHART WHERE  substring(ACCOUNTCD,1,7)  in ('1020101','1020102','2020103') AND STATUSCD = 'P' and ACCOUNTNM LIKE  @SearchText +'%' AND (BRANCHCD ='" + brCD + "' OR BRANCHCD IS NULL OR BRANCHCD ='')";
                //    }
            }
            else if (Transtype == "JOUR")
            {
                //if (uTp == "COMPADMIN")
                //{
                query = "SELECT ACCOUNTNM AS txt FROM GL_ACCHART WHERE substring(ACCOUNTCD,1,7) not in ('1020101','1020102','2020103') AND STATUSCD = 'P' and ACCOUNTNM LIKE  @SearchText +'%'";
                //}
                //else
                //{
                //    query = "SELECT ACCOUNTNM AS txt FROM GL_ACCHART WHERE substring(ACCOUNTCD,1,7) not in ('1020101','1020102','2020103') AND STATUSCD = 'P' and ACCOUNTNM LIKE  @SearchText +'%' AND (BRANCHCD ='" + brCD + "' OR BRANCHCD IS NULL OR BRANCHCD ='')";
                //}
            }
            else if (Transtype == "CONT")
            {
                //if (uTp == "COMPADMIN")
                //{
                query = "SELECT ACCOUNTNM AS txt FROM GL_ACCHART WHERE substring(ACCOUNTCD,1,7) in ('1020101','1020102','2020103') AND STATUSCD = 'P' and ACCOUNTNM LIKE  @SearchText +'%'";
                //}
                //else
                //{
                //    query = "SELECT ACCOUNTNM AS txt FROM GL_ACCHART WHERE substring(ACCOUNTCD,1,7) in ('1020101','1020102','2020103') AND STATUSCD = 'P' and ACCOUNTNM LIKE  @SearchText +'%' AND (BRANCHCD ='" + brCD + "' OR BRANCHCD IS NULL OR BRANCHCD ='')";
                //}
            }
            else
            {
                Transtype = "";
            }
            using (SqlCommand obj_Sqlcommand = new SqlCommand(query, conn))
            {
                conn.Open();
                obj_Sqlcommand.Parameters.AddWithValue("@SearchText", txt);
                SqlDataReader obj_result = obj_Sqlcommand.ExecuteReader();
                while (obj_result.Read())
                {
                    result.Add(obj_result["txt"].ToString().TrimEnd());
                }
            }
            return result;
        }




        [WebMethod(EnableSession = true)]
        [ScriptMethod(ResponseFormat = ResponseFormat.Json)]
        public List<string> GetCompletionListCreditSingleVoucherEdit(string txt)
        {
            // your code to query the database goes here
            List<string> result = new List<string>();
            string uTp = "";
            string brCD = "";
            string query = "";
            string Transtype = "";
            if (HttpContext.Current.Session["USERTYPE"] != null && HttpContext.Current.Session["BrCD"] != null)
         //   if (HttpContext.Current.Session["USERTYPE"] != null && HttpContext.Current.Session["BrCD"] != null && HttpContext.Current.Session["Transtype"] != "")
            {
                uTp = HttpContext.Current.Session["USERTYPE"].ToString();
                brCD = HttpContext.Current.Session["BrCD"].ToString();
                Transtype = HttpContext.Current.Session["Transtype"].ToString();
            }
            SqlConnection conn = new SqlConnection(dbFunctions.connection);
            SqlCommand cmd = new SqlCommand();
            if (Transtype == "MREC")
            {
                query = "SELECT ACCOUNTNM AS txt FROM GL_ACCHART WHERE substring(ACCOUNTCD,1,7) not in ('1020101','1020102','2020103') AND STATUSCD = 'P' and ACCOUNTNM LIKE  @SearchText +'%'";
            }

            else if (Transtype == "MPAY")
            {
                query = "SELECT ACCOUNTNM AS txt FROM GL_ACCHART WHERE  substring(ACCOUNTCD,1,7)  in ('1020101','1020102','2020103') AND STATUSCD = 'P' and ACCOUNTNM LIKE   @SearchText +'%'";
            }
            else if (Transtype == "JOUR")
            {
                query = "SELECT ACCOUNTNM AS txt FROM GL_ACCHART WHERE substring(ACCOUNTCD,1,7) not in ('1020101','1020102','2020103') AND STATUSCD = 'P' and ACCOUNTNM LIKE  @SearchText +'%'";
            }
            else if (Transtype == "CONT")
            {
                query = "SELECT ACCOUNTNM AS txt FROM GL_ACCHART WHERE substring(ACCOUNTCD,1,7) in ('1020101','1020102','2020103') AND STATUSCD = 'P' and ACCOUNTNM LIKE  @SearchText +'%'";
            }
            else
            {
                Transtype = "";
            }
            using (SqlCommand obj_Sqlcommand = new SqlCommand(query, conn))
            {
                conn.Open();
                obj_Sqlcommand.Parameters.AddWithValue("@SearchText", txt);
                SqlDataReader obj_result = obj_Sqlcommand.ExecuteReader();
                while (obj_result.Read())
                {
                    result.Add(obj_result["txt"].ToString().TrimEnd());
                }
            }
            return result;
        }



        [WebMethod(EnableSession = true)]
        [ScriptMethod(ResponseFormat = ResponseFormat.Json)]
        public List<string> GetCompletionListDebitSingleVoucherEdit(string txt)
        {
            // your code to query the database goes here
            List<string> result = new List<string>();
            string uTp = "";
            string brCD = "";
            string query = "";
            string Transtype = "";
            if (HttpContext.Current.Session["USERTYPE"] != null && HttpContext.Current.Session["BrCD"] != null)
         //   if (HttpContext.Current.Session["USERTYPE"] != null && HttpContext.Current.Session["BrCD"] != null && HttpContext.Current.Session["Transtype"] != "")
            {
                uTp = HttpContext.Current.Session["USERTYPE"].ToString();
                brCD = HttpContext.Current.Session["BrCD"].ToString();
                Transtype = HttpContext.Current.Session["Transtype"].ToString();
            }
            SqlConnection conn = new SqlConnection(dbFunctions.connection);
            SqlCommand cmd = new SqlCommand();
            if (Transtype == "MREC")
            {
                query = "SELECT ACCOUNTNM AS txt  FROM GL_ACCHART WHERE substring(ACCOUNTCD,1,7) in ('1020101','1020102','2020103')  AND STATUSCD = 'P' and ACCOUNTNM LIKE  @SearchText +'%'";
            }

            else if (Transtype == "MPAY")
            {
                query = "SELECT ACCOUNTNM AS txt  FROM GL_ACCHART WHERE substring(ACCOUNTCD,1,7) not in ('1020101','1020102','2020103') AND STATUSCD = 'P' and ACCOUNTNM LIKE   @SearchText +'%'";
            }
            else if (Transtype == "JOUR")
            {
                query = "SELECT ACCOUNTNM AS txt  FROM GL_ACCHART WHERE substring(ACCOUNTCD,1,7) not in ('1020101','1020102','2020103') AND STATUSCD = 'P' and ACCOUNTNM LIKE  @SearchText +'%'";
            }
            else if (Transtype == "CONT")
            {
                query = "SELECT ACCOUNTNM AS txt FROM GL_ACCHART WHERE substring(ACCOUNTCD,1,7) in ('1020101','1020102','2020103') AND STATUSCD = 'P' and ACCOUNTNM LIKE @SearchText +'%'";
            }
            else
            {
                Transtype = "";
            }
            using (SqlCommand obj_Sqlcommand = new SqlCommand(query, conn))
            {
                conn.Open();
                obj_Sqlcommand.Parameters.AddWithValue("@SearchText", txt);
                SqlDataReader obj_result = obj_Sqlcommand.ExecuteReader();
                while (obj_result.Read())
                {
                    result.Add(obj_result["txt"].ToString().TrimEnd());
                }
            }
            return result;
        }



        [WebMethod(EnableSession = true)]
        [ScriptMethod(ResponseFormat = ResponseFormat.Json)]
        public List<string> GetCompletionListLavelCode(string txt)
        {
            // your code to query the database goes here
            List<string> result = new List<string>();
            string lavelCd = "";
            if (HttpContext.Current.Session["LAVELCD"] != null)
                lavelCd = HttpContext.Current.Session["LAVELCD"].ToString();
            SqlConnection conn = new SqlConnection(dbFunctions.connection);
            using (SqlCommand obj_Sqlcommand = new SqlCommand("SELECT ACCOUNTNM+'| (L-'+convert(nvarchar,LEVELCD,103)+')'  AS txt FROM GL_ACCHART WHERE ACCOUNTCD like '" + lavelCd + "' and LEVELCD between 1 and 4 and ACCOUNTNM like  @SearchText +'%'", conn))
            {
                conn.Open();
                obj_Sqlcommand.Parameters.AddWithValue("@SearchText", txt);
                SqlDataReader obj_result = obj_Sqlcommand.ExecuteReader();
                while (obj_result.Read())
                {
                    result.Add(obj_result["txt"].ToString().TrimEnd());
                }
            }
            return result;
        }


        [WebMethod(EnableSession = true)]
        [ScriptMethod(ResponseFormat = ResponseFormat.Json)]
        public List<string> GetCompletionListBankBook(string txt)
        {
            // your code to query the database goes here
            List<string> result = new List<string>();
            string uTp = "";
            string brCD = "";
            string query = "";
            if (HttpContext.Current.Session["USERTYPE"] != null && HttpContext.Current.Session["BrCD"] != null)
            {
                uTp = HttpContext.Current.Session["USERTYPE"].ToString();
                brCD = HttpContext.Current.Session["BrCD"].ToString();
            }
            SqlConnection conn = new SqlConnection(dbFunctions.connection);
            SqlCommand cmd = new SqlCommand();
            //  if (uTp == "COMPADMIN")
            query = "SELECT ACCOUNTNM AS txt FROM GL_ACCHART WHERE substring(ACCOUNTCD,1,7) =('1020102') and STATUSCD='P' and ACCOUNTNM LIKE  @SearchText +'%'";
            // else
            //   query = "SELECT ACCOUNTNM AS txt FROM GL_ACCHART WHERE substring(ACCOUNTCD,1,7) =('1020102') and STATUSCD='P' and ACCOUNTNM LIKE  @SearchText +'%'  AND (BRANCHCD ='" + brCD + "' OR BRANCHCD IS NULL OR BRANCHCD ='')";
            using (SqlCommand obj_Sqlcommand = new SqlCommand(query, conn))
            {
                conn.Open();
                obj_Sqlcommand.Parameters.AddWithValue("@SearchText", txt);
                SqlDataReader obj_result = obj_Sqlcommand.ExecuteReader();
                while (obj_result.Read())
                {
                    result.Add(obj_result["txt"].ToString().TrimEnd());
                }
            }
            return result;
        }



        [WebMethod(EnableSession = true)]
        [ScriptMethod(ResponseFormat = ResponseFormat.Json)]
        public List<string> GetCompletionListCashBook(string txt)
        {
            // your code to query the database goes here
            List<string> result = new List<string>();
            string uTp = "";
            string brCD = "";
            string query = "";
            if (HttpContext.Current.Session["USERTYPE"] != null && HttpContext.Current.Session["BrCD"] != null)
            {
                uTp = HttpContext.Current.Session["USERTYPE"].ToString();
                brCD = HttpContext.Current.Session["BrCD"].ToString();
            }
            SqlConnection conn = new SqlConnection(dbFunctions.connection);
            SqlCommand cmd = new SqlCommand();
            // if (uTp == "COMPADMIN")
            query = "SELECT ACCOUNTNM As txt FROM GL_ACCHART WHERE substring(ACCOUNTCD,1,7) =('1020101') and STATUSCD='P' and ACCOUNTNM LIKE  @SearchText +'%'";
            //  else
            //  query = "SELECT ACCOUNTNM As txt FROM GL_ACCHART WHERE substring(ACCOUNTCD,1,7) =('1020101') and STATUSCD='P' and ACCOUNTNM LIKE   @SearchText +'%'  AND (BRANCHCD ='" + brCD + "' OR BRANCHCD IS NULL OR BRANCHCD ='')";
            using (SqlCommand obj_Sqlcommand = new SqlCommand(query, conn))
            {
                conn.Open();
                obj_Sqlcommand.Parameters.AddWithValue("@SearchText", txt);
                SqlDataReader obj_result = obj_Sqlcommand.ExecuteReader();
                while (obj_result.Read())
                {
                    result.Add(obj_result["txt"].ToString().TrimEnd());
                }
            }
            return result;
        }



        [WebMethod(EnableSession = true)]
        [ScriptMethod(ResponseFormat = ResponseFormat.Json)]
        public List<string> GetCompletionListLedgerBook(string txt)
        {
            // your code to query the database goes here
            List<string> result = new List<string>();
            string uTp = "";
            string brCD = "";
            string query = "";
            if (HttpContext.Current.Session["USERTYPE"] != null && HttpContext.Current.Session["BrCD"] != null)
            {
                uTp = HttpContext.Current.Session["USERTYPE"].ToString();
                brCD = HttpContext.Current.Session["BrCD"].ToString();
            }
            SqlConnection conn = new SqlConnection(dbFunctions.connection);
            SqlCommand cmd = new SqlCommand();
            //   if (uTp == "COMPADMIN")
            query = "SELECT ACCOUNTNM AS txt FROM GL_ACCHART WHERE substring(ACCOUNTCD,1,7) not in ('1020101','1020102') and STATUSCD='P' and ACCOUNTNM LIKE  @SearchText +'%'";
            //   else
            //   query = "SELECT ACCOUNTNM  AS txt FROM GL_ACCHART  AS txt WHERE substring(ACCOUNTCD,1,7) not in ('1020101','1020102') and STATUSCD='P' and ACCOUNTNM LIKE @SearchText +'%' AND (BRANCHCD ='" + brCD + "' OR BRANCHCD IS NULL OR BRANCHCD ='')";
            using (SqlCommand obj_Sqlcommand = new SqlCommand(query, conn))
            {
                conn.Open();
                obj_Sqlcommand.Parameters.AddWithValue("@SearchText", txt);
                SqlDataReader obj_result = obj_Sqlcommand.ExecuteReader();
                while (obj_result.Read())
                {
                    result.Add(obj_result["txt"].ToString().TrimEnd());
                }
            }
            return result;
        }


        [WebMethod(EnableSession = true)]
        [ScriptMethod(ResponseFormat = ResponseFormat.Json)]
        public List<string> GetCompletionListLedgerBookDepoDelear(string txt)
        {
            // your code to query the database goes here
            List<string> result = new List<string>();
            string uTp = "";
            string brCD = "";
            string query = "";
            if (HttpContext.Current.Session["USERTYPE"] != null && HttpContext.Current.Session["BrCD"] != null)
            {
                uTp = HttpContext.Current.Session["USERTYPE"].ToString();
                brCD = HttpContext.Current.Session["BrCD"].ToString();
            }
            SqlConnection conn = new SqlConnection(dbFunctions.connection);
            SqlCommand cmd = new SqlCommand();
            //   if (uTp == "COMPADMIN")
            query = "SELECT ACCOUNTNM AS txt FROM GL_ACCHART WHERE substring(ACCOUNTCD,1,7) in ('1020201','1020202') and STATUSCD='P' and ACCOUNTNM LIKE  @SearchText +'%'";
            //   else
            //   query = "SELECT ACCOUNTNM  AS txt FROM GL_ACCHART  AS txt WHERE substring(ACCOUNTCD,1,7) not in ('1020101','1020102') and STATUSCD='P' and ACCOUNTNM LIKE @SearchText +'%' AND (BRANCHCD ='" + brCD + "' OR BRANCHCD IS NULL OR BRANCHCD ='')";
            using (SqlCommand obj_Sqlcommand = new SqlCommand(query, conn))
            {
                conn.Open();
                obj_Sqlcommand.Parameters.AddWithValue("@SearchText", txt);
                SqlDataReader obj_result = obj_Sqlcommand.ExecuteReader();
                while (obj_result.Read())
                {
                    result.Add(obj_result["txt"].ToString().TrimEnd());
                }
            }
            return result;
        }
        //  [WebMethod(EnableSession = true)]
        //[ScriptMethod(ResponseFormat = ResponseFormat.Json)]
        //public List<string> GetCompletionListLedgerBookGeneral(string txt)
        //{
        //    // your code to query the database goes here
        //    List<string> result = new List<string>();
        //    string uTp = "";
        //    string brCD = "";
        //    string query = "";
        //    if (HttpContext.Current.Session["USERTYPE"] != null && HttpContext.Current.Session["BrCD"] != null)
        //    {
        //        uTp = HttpContext.Current.Session["USERTYPE"].ToString();
        //        brCD = HttpContext.Current.Session["BrCD"].ToString();
        //    }
        //    SqlConnection conn = new SqlConnection(dbFunctions.connection);
        //    SqlCommand cmd = new SqlCommand();
        //    //   if (uTp == "COMPADMIN")
        //    query = "SELECT ACCOUNTNM AS txt FROM GL_ACCHART WHERE substring(ACCOUNTCD,1,7) not in ('1020201','1020202') and STATUSCD='P' and ACCOUNTNM LIKE  @SearchText +'%'";
        //    //   else
        //    //   query = "SELECT ACCOUNTNM  AS txt FROM GL_ACCHART  AS txt WHERE substring(ACCOUNTCD,1,7) not in ('1020101','1020102') and STATUSCD='P' and ACCOUNTNM LIKE @SearchText +'%' AND (BRANCHCD ='" + brCD + "' OR BRANCHCD IS NULL OR BRANCHCD ='')";
        //    using (SqlCommand obj_Sqlcommand = new SqlCommand(query, conn))
        //    {
        //        conn.Open();
        //        obj_Sqlcommand.Parameters.AddWithValue("@SearchText", txt);
        //        SqlDataReader obj_result = obj_Sqlcommand.ExecuteReader();
        //        while (obj_result.Read())
        //        {
        //            result.Add(obj_result["txt"].ToString().TrimEnd());
        //        }
        //    }
        //    return result;
        //}
        [WebMethod]
        [ScriptMethod(ResponseFormat = ResponseFormat.Json)]
        public List<OneValuePass> GetCompletionListLedgerBookGeneral(string term)
        {
            List<string> s = new List<string>();
            List<OneValuePass> lst = new List<OneValuePass>();
            SqlConnection conn = new SqlConnection(dbFunctions.connection);
            using (SqlCommand obj_Sqlcommand = new SqlCommand(@"SELECT ACCOUNTNM AS txt, ACCOUNTCD AS txtid FROM GL_ACCHART WHERE STATUSCD='P' and ACCOUNTNM LIKE  @SearchText +'%'", conn))
            {
                conn.Open();
                obj_Sqlcommand.Parameters.AddWithValue("@SearchText", term);
                SqlDataReader obj_result = obj_Sqlcommand.ExecuteReader();
                while (obj_result.Read())
                {
                    string result = obj_result["txt"].ToString().TrimEnd();
                    string id = obj_result["txtid"].ToString().TrimEnd();
                    lst.Add(new OneValuePass { value = id, label = result });
                }
                var q = lst.ToList();
                return (List<OneValuePass>)q;
            }
        }


        [WebMethod(EnableSession = true)]
        [ScriptMethod(ResponseFormat = ResponseFormat.Json)]
        public List<string> GetCompletionListNotesAccount(string txt)
        {
            // your code to query the database goes here
            List<string> result = new List<string>();
           // string uTp = "";
           // string brCD = "";
            string query = "";
            string lableCode = "";
            if (HttpContext.Current.Session["ddlLevelID"] != null)
            {
                lableCode = HttpContext.Current.Session["ddlLevelID"].ToString();
            }
            SqlConnection conn = new SqlConnection(dbFunctions.connection);
            SqlCommand cmd = new SqlCommand();
            if (lableCode == "1")
            {
                query = "SELECT (ACCOUNTNM+' (L - '+convert(nvarchar(10),LEVELCD,103)+')') as txt FROM GL_ACCHART WHERE ACCOUNTCD like '1%' and LEVELCD between 1 and 4 and ACCOUNTNM like @SearchText +'%'";
            }

            else if (lableCode == "2")
            {
                query = "SELECT (ACCOUNTNM+' (L - '+convert(nvarchar(10),LEVELCD,103)+')') as txt FROM GL_ACCHART WHERE ACCOUNTCD like '2%' and LEVELCD between 1 and 4  and ACCOUNTNM like @SearchText +'%'";
            }
            else if (lableCode == "3")
            {
                query = "SELECT (ACCOUNTNM+' (L - '+convert(nvarchar(10),LEVELCD,103)+')') as txt FROM GL_ACCHART WHERE ACCOUNTCD like '3%'  and LEVELCD between 1 and 4  and ACCOUNTNM like @SearchText +'%'";
            }
            else if (lableCode == "4")
            {
                query = "SELECT (ACCOUNTNM+' (L - '+convert(nvarchar(10),LEVELCD,103)+')') as txt FROM GL_ACCHART WHERE ACCOUNTCD like '4%'  and LEVELCD between 1 and 4  and ACCOUNTNM like @SearchText +'%'";
            }
            else
            {
                lableCode = "";
            }
            using (SqlCommand obj_Sqlcommand = new SqlCommand(query, conn))
            {
                conn.Open();
                obj_Sqlcommand.Parameters.AddWithValue("@SearchText", txt);
                SqlDataReader obj_result = obj_Sqlcommand.ExecuteReader();
                while (obj_result.Read())
                {
                    result.Add(obj_result["txt"].ToString().TrimEnd());
                }
            }
            return result;
        }




        [WebMethod(EnableSession = true)]
        [ScriptMethod(ResponseFormat = ResponseFormat.Json)]
        public List<string> GetCompletionListReceiptStatementSelected(string txt)
        {
            // your code to query the database goes here
            List<string> result = new List<string>();
            string uTp = "";
            string brCD = "";
            string query = "";
            if (HttpContext.Current.Session["USERTYPE"] != null && HttpContext.Current.Session["BrCD"] != null)
            {
                uTp = HttpContext.Current.Session["USERTYPE"].ToString();
                brCD = HttpContext.Current.Session["BrCD"].ToString();
            }
            SqlConnection conn = new SqlConnection(dbFunctions.connection);
            SqlCommand cmd = new SqlCommand();
            //  if (uTp == "COMPADMIN")
            query = "SELECT ACCOUNTNM As txt FROM GL_ACCHART WHERE substring(ACCOUNTCD,1,7) in ('1020101','1020102') and STATUSCD='P' and ACCOUNTNM LIKE   @SearchText +'%'";
            // else
            //    query = "SELECT ACCOUNTNM As txt FROM GL_ACCHART WHERE substring(ACCOUNTCD,1,7) in ('1020101','1020102') and STATUSCD='P' and ACCOUNTNM LIKE  @SearchText +'%' AND (BRANCHCD ='" + brCD + "' OR BRANCHCD IS NULL OR BRANCHCD ='')";
            using (SqlCommand obj_Sqlcommand = new SqlCommand(query, conn))
            {
                conn.Open();
                obj_Sqlcommand.Parameters.AddWithValue("@SearchText", txt);
                SqlDataReader obj_result = obj_Sqlcommand.ExecuteReader();
                while (obj_result.Read())
                {
                    result.Add(obj_result["txt"].ToString().TrimEnd());
                }
            }
            return result;
        }










        //Accounts End//Accounts End//Accounts End//Accounts End//Accounts End//Accounts End//Accounts End//Accounts End
        //Accounts End//Accounts End//Accounts End//Accounts End//Accounts End//Accounts End//Accounts End//Accounts End
        //Accounts End//Accounts End//Accounts End//Accounts End//Accounts End//Accounts End//Accounts End//Accounts End
        //Accounts End//Accounts End//Accounts End//Accounts End//Accounts End//Accounts End//Accounts End//Accounts End


        /*--------------------------------------------------------------------------------------------------------------*/


        //Stock Start  //Stock Start //Stock Start //Stock Start //Stock Start //Stock Start //Stock Start //Stock Start 
        //Stock Start  //Stock Start //Stock Start //Stock Start //Stock Start //Stock Start //Stock Start //Stock Start 
        //Stock Start  //Stock Start //Stock Start //Stock Start //Stock Start //Stock Start //Stock Start //Stock Start 
        //Stock Start  //Stock Start //Stock Start //Stock Start //Stock Start //Stock Start //Stock Start //Stock Start 
        //Stock Start  //Stock Start //Stock Start //Stock Start //Stock Start //Stock Start //Stock Start //Stock Start 



        [WebMethod(EnableSession = true)]
        [ScriptMethod(ResponseFormat = ResponseFormat.Json)]
        public List<string> GetCompletionListItemCategoryName(string txt)
        {
            // your code to query the database goes here
            List<string> result = new List<string>();
            SqlConnection conn = new SqlConnection(dbFunctions.connection);
            using (
                SqlCommand obj_Sqlcommand =
                    new SqlCommand(
                        "SELECT distinct(SUBNM) AS txt FROM STK_ITEMMST WHERE SUBNM LIKE  @SearchText +'%' ORDER BY SUBNM",
                        conn))
            {
                conn.Open();
                obj_Sqlcommand.Parameters.AddWithValue("@SearchText", txt);
                SqlDataReader obj_result = obj_Sqlcommand.ExecuteReader();
                while (obj_result.Read())
                {
                    result.Add(obj_result["txt"].ToString().TrimEnd());
                }
            }
            return result;
        }


        [WebMethod(EnableSession = true)]
        [ScriptMethod(ResponseFormat = ResponseFormat.Json)]
        public List<string> GetCompletionListItemMasterCategoryName(string txt)
        {
            // your code to query the database goes here
            List<string> result = new List<string>();
            SqlConnection conn = new SqlConnection(dbFunctions.connection);
            using (
                SqlCommand obj_Sqlcommand =
                    new SqlCommand("SELECT DISTINCT(MSTNM) AS txt FROM STK_ITEMMST WHERE MSTNM LIKE  @SearchText +'%'",
                        conn))
            {
                conn.Open();
                obj_Sqlcommand.Parameters.AddWithValue("@SearchText", txt);
                SqlDataReader obj_result = obj_Sqlcommand.ExecuteReader();
                while (obj_result.Read())
                {
                    result.Add(obj_result["txt"].ToString().TrimEnd());
                }
            }
            return result;
        }

        [WebMethod(EnableSession = true)]
        [ScriptMethod(ResponseFormat = ResponseFormat.Json)]
        public List<string> GetCompletionListItemName(string txt)
        {
            // your code to query the database goes here
            List<string> result = new List<string>();
            SqlConnection conn = new SqlConnection(dbFunctions.connection);
            using (
                SqlCommand obj_Sqlcommand =
                    new SqlCommand(
                        "SELECT (ITEMNM + '|' + ITEMCD) AS txt FROM STK_ITEM WHERE ITEMNM LIKE  @SearchText +'%'", conn)
                )
            {
                conn.Open();
                obj_Sqlcommand.Parameters.AddWithValue("@SearchText", txt);
                SqlDataReader obj_result = obj_Sqlcommand.ExecuteReader();
                while (obj_result.Read())
                {
                    result.Add(obj_result["txt"].ToString().TrimEnd());
                }
            }
            return result;
        }


        [WebMethod(EnableSession = true)]
        [ScriptMethod(ResponseFormat = ResponseFormat.Json)]
        public List<string> GetAutoStoreNameFR(string txt)
        {
            // your code to query the database goes here
            List<string> result = new List<string>();
            //string storeTo = checkSession("StoreTo");
            string store = HttpContext.Current.Session["StoreTo"].ToString();
            SqlConnection conn = new SqlConnection(dbFunctions.connection);
            using (
                SqlCommand obj_Sqlcommand =
                    new SqlCommand(
                        "SELECT STORENM as txt FROM STK_STORE WHERE STORENM like @SearchText +'%'  and STORENM !='" +
                        store + "'", conn))
            {
                conn.Open();
                obj_Sqlcommand.Parameters.AddWithValue("@SearchText", txt);
                SqlDataReader obj_result = obj_Sqlcommand.ExecuteReader();
                while (obj_result.Read())
                {
                    result.Add(obj_result["txt"].ToString().TrimEnd());
                }
            }
            return result;
        }

        [WebMethod(EnableSession = true)]
        [ScriptMethod(ResponseFormat = ResponseFormat.Json)]
        public List<string> GetAutoStoreName(string txt)
        {
            // your code to query the database goes here
            List<string> result = new List<string>();
            string store = HttpContext.Current.Session["StoreFR"].ToString();
            SqlConnection conn = new SqlConnection(dbFunctions.connection);
            using (
                SqlCommand obj_Sqlcommand =
                    new SqlCommand(
                        "SELECT STORENM as txt FROM STK_STORE WHERE STORENM like @SearchText +'%'  and STORENM !='" +
                        store + "'", conn))
            {
                conn.Open();
                obj_Sqlcommand.Parameters.AddWithValue("@SearchText", txt);
                SqlDataReader obj_result = obj_Sqlcommand.ExecuteReader();
                while (obj_result.Read())
                {
                    result.Add(obj_result["txt"].ToString().TrimEnd());
                }
            }
            return result;
        }


        [WebMethod]
        [ScriptMethod(ResponseFormat = ResponseFormat.Json)]
        public List<string> GetCompletionListPrivilegeCard(string txt)
        {
            // your code to query the database goes here
            List<string> result = new List<string>();

            SqlConnection conn = new SqlConnection(dbFunctions.connection);
            using (
                SqlCommand obj_Sqlcommand =
                    new SqlCommand(
                        "SELECT PCARDHID AS txt FROM PC_CARD WHERE PCARDHID LIKE @SearchText +'%'  Order by PCARDHID",
                        conn))
            {
                conn.Open();
                obj_Sqlcommand.Parameters.AddWithValue("@SearchText", txt);
                SqlDataReader obj_result = obj_Sqlcommand.ExecuteReader();
                while (obj_result.Read())
                {
                    result.Add(obj_result["txt"].ToString().TrimEnd());
                }
            }
            return result;
        }

        [WebMethod]
        [ScriptMethod(ResponseFormat = ResponseFormat.Json)]
        public List<string> GetCompletionListCategoryMasterName(string txt)
        {
            // your code to query the database goes here
            List<string> result = new List<string>();

            SqlConnection conn = new SqlConnection(dbFunctions.connection);
            using (
                SqlCommand obj_Sqlcommand =
                    new SqlCommand(
                        "SELECT DISTINCT(MSTNM) AS txt FROM STK_ITEMMST WHERE MSTNM LIKE @SearchText +'%' AND SUBSTRING(MSTID,1,1) !='1'",
                        conn))
            {
                conn.Open();
                obj_Sqlcommand.Parameters.AddWithValue("@SearchText", txt);
                SqlDataReader obj_result = obj_Sqlcommand.ExecuteReader();
                while (obj_result.Read())
                {
                    result.Add(obj_result["txt"].ToString().TrimEnd());
                }
            }
            return result;
        }

        [WebMethod]
        [ScriptMethod(ResponseFormat = ResponseFormat.Json)]
        public List<string> GetCompletionListSubCategoryName(string txt)
        {
            // your code to query the database goes here
            List<string> result = new List<string>();

            SqlConnection conn = new SqlConnection(dbFunctions.connection);
            using (
                SqlCommand obj_Sqlcommand =
                    new SqlCommand(@"SELECT DISTINCT(CATNM) AS txt FROM STK_ITEMMST WHERE CATNM LIKE @SearchText +'%'", conn))
            {
                conn.Open();
                obj_Sqlcommand.Parameters.AddWithValue("@SearchText", txt);
                SqlDataReader obj_result = obj_Sqlcommand.ExecuteReader();
                while (obj_result.Read())
                {
                    result.Add(obj_result["txt"].ToString().TrimEnd());
                }
            }
            return result;
        }

        [WebMethod]
        [ScriptMethod(ResponseFormat = ResponseFormat.Json)]
        public List<string> GetCompletionListSubName(string txt)
        {
            // your code to query the database goes here
            List<string> result = new List<string>();

            SqlConnection conn = new SqlConnection(dbFunctions.connection);
            using (
                SqlCommand obj_Sqlcommand =
                    new SqlCommand("SELECT DISTINCT(SUBNM) AS txt FROM STK_ITEMMST WHERE SUBNM LIKE @SearchText +'%'",
                        conn))
            {
                conn.Open();
                obj_Sqlcommand.Parameters.AddWithValue("@SearchText", txt);
                SqlDataReader obj_result = obj_Sqlcommand.ExecuteReader();
                while (obj_result.Read())
                {
                    result.Add(obj_result["txt"].ToString().TrimEnd());
                }
            }
            return result;
        }

        [WebMethod]
        [ScriptMethod(ResponseFormat = ResponseFormat.Json)]
        public List<string> GetCompletionListBrandName(string txt)
        {
            // your code to query the database goes here
            List<string> result = new List<string>();

            SqlConnection conn = new SqlConnection(dbFunctions.connection);
            using (
                SqlCommand obj_Sqlcommand =
                    new SqlCommand("Select Distinct BRAND AS txt From STK_ITEM where BRAND LIKE @SearchText +'%'", conn)
                )
            {
                conn.Open();
                obj_Sqlcommand.Parameters.AddWithValue("@SearchText", txt);
                SqlDataReader obj_result = obj_Sqlcommand.ExecuteReader();
                while (obj_result.Read())
                {
                    result.Add(obj_result["txt"].ToString().TrimEnd());
                }
            }
            return result;
        }

        [WebMethod]
        [ScriptMethod(ResponseFormat = ResponseFormat.Json)]
        public List<string> GetCompletionListProductUnitMeasurement(string txt)
        {
            // your code to query the database goes here
            List<string> result = new List<string>();

            SqlConnection conn = new SqlConnection(dbFunctions.connection);
            using (
                SqlCommand obj_Sqlcommand =
                    new SqlCommand("Select Distinct MUNIT As txt From STK_ITEM where MUNIT LIKE @SearchText +'%'", conn))
            {
                conn.Open();
                obj_Sqlcommand.Parameters.AddWithValue("@SearchText", txt);
                SqlDataReader obj_result = obj_Sqlcommand.ExecuteReader();
                while (obj_result.Read())
                {
                    result.Add(obj_result["txt"].ToString().TrimEnd());
                }
            }
            return result;
        }

        [WebMethod]
        [ScriptMethod(ResponseFormat = ResponseFormat.Json)]
        public List<string> GetCompletionListStoreName(string txt)
        {
            // your code to query the database goes here
            List<string> result = new List<string>();

            SqlConnection conn = new SqlConnection(dbFunctions.connection);
            using (
                SqlCommand obj_Sqlcommand =
                    new SqlCommand("SELECT STORENM AS txt FROM STK_STORE WHERE STORENM LIKE @SearchText +'%'", conn))
            {
                conn.Open();
                obj_Sqlcommand.Parameters.AddWithValue("@SearchText", txt);
                SqlDataReader obj_result = obj_Sqlcommand.ExecuteReader();
                while (obj_result.Read())
                {
                    result.Add(obj_result["txt"].ToString().TrimEnd());
                }
            }
            return result;
        }



        [WebMethod(EnableSession = true)]
        [ScriptMethod(ResponseFormat = ResponseFormat.Json)]
        public List<string> GetCompletionListExchangeTransSO(string txt)
        {
            // your code to query the database goes here
            List<string> result = new List<string>();
            string unit = HttpContext.Current.Session["UNIT"].ToString();
            SqlConnection conn = new SqlConnection(dbFunctions.connection);
            using (
                SqlCommand obj_Sqlcommand =
                    new SqlCommand(
                        "SELECT TRANSNO AS txt FROM STK_TRANSMST WHERE TRANSTP='SALE'  And UNITFR='" + unit +
                        "' AND TRANSNO LIKE  @SearchText +'%' ORDER BY TRANSNO", conn))
            {
                conn.Open();
                obj_Sqlcommand.Parameters.AddWithValue("@SearchText", txt);
                SqlDataReader obj_result = obj_Sqlcommand.ExecuteReader();
                while (obj_result.Read())
                {
                    result.Add(obj_result["txt"].ToString().TrimEnd());
                }
            }
            return result;
        }


        [WebMethod(EnableSession = true)]
        [ScriptMethod(ResponseFormat = ResponseFormat.Json)]
        public List<string> GetCompletionListDueTransSO(string txt)
        {
            // your code to query the database goes here
            List<string> result = new List<string>();
            SqlConnection conn = new SqlConnection(dbFunctions.connection);
            //string unit = HttpContext.Current.Session["UnitIdForDue"].ToString();
            using (
                SqlCommand obj_Sqlcommand =
                    new SqlCommand(
                        "Select TRANSNO AS txt From STK_TRANSMST where SALETP='ADV' AND TRANSNO LIKE  @SearchText +'%' ORDER BY TRANSNO",
                        conn))
            {
                conn.Open();
                obj_Sqlcommand.Parameters.AddWithValue("@SearchText", txt);
                SqlDataReader obj_result = obj_Sqlcommand.ExecuteReader();
                while (obj_result.Read())
                {
                    result.Add(obj_result["txt"].ToString().TrimEnd());
                }
            }
            return result;
        }


        [WebMethod(EnableSession = true)]
        [ScriptMethod(ResponseFormat = ResponseFormat.Json)]
        public List<string> GetCompletionListVoucherNoReprint(string txt)
        {
            // your code to query the database goes here
            List<string> result = new List<string>();
            string type = HttpContext.Current.Session["ReprintType"].ToString();
            string unit = HttpContext.Current.Session["RePrintUnitId"].ToString();
            string query = "";
            if (type == "SALE")
                query = @"SELECT TRANSNO AS txt FROM STK_TRANSMST WHERE TRANSTP='" + type + "' AND UNITFR='" + unit +
                        "' AND TRANSNO LIKE @SearchText +'%'  AND TRRTSNO IS NULL";
            else
                query = @"SELECT TRANSNO AS txt FROM STK_TRANSMST WHERE TRANSTP='" + type + "' AND UNITFR='" + unit +
                        "' AND TRRTSNO LIKE @SearchText +'%'";
            SqlConnection conn = new SqlConnection(dbFunctions.connection);
            using (SqlCommand obj_Sqlcommand = new SqlCommand(query, conn))
            {
                conn.Open();
                obj_Sqlcommand.Parameters.AddWithValue("@SearchText", txt);
                SqlDataReader obj_result = obj_Sqlcommand.ExecuteReader();
                while (obj_result.Read())
                {
                    result.Add(obj_result["txt"].ToString().TrimEnd());
                }
            }
            return result;
        }

        [WebMethod(EnableSession = true)]
        [ScriptMethod(ResponseFormat = ResponseFormat.Json)]
        public List<string> GetCompletionListItemRefferNo(string txt)
        {
            // your code to query the database goes here
            List<string> result = new List<string>();
            SqlConnection conn = new SqlConnection(dbFunctions.connection);
            string referSubId = HttpContext.Current.Session["REFERSUBID"].ToString();
            using (SqlCommand obj_Sqlcommand = new SqlCommand(@"SELECT DISTINCT REFNO AS txt From STK_ITEM 
                WHERE SUBID='" + referSubId + "' AND REFNO LIKE @SearchText +'%' ORDER BY REFNO", conn))
            {
                conn.Open();
                obj_Sqlcommand.Parameters.AddWithValue("@SearchText", txt);
                SqlDataReader obj_result = obj_Sqlcommand.ExecuteReader();
                while (obj_result.Read())
                {
                    result.Add(obj_result["txt"].ToString().TrimEnd());
                }
            }
            return result;
        }

        [WebMethod(EnableSession = true)]
        [ScriptMethod(ResponseFormat = ResponseFormat.Json)]
        public List<string> GetCompletionListItemNameWithOutItemCode(string txt)
        {
            // your code to query the database goes here
            List<string> result = new List<string>();
            SqlConnection conn = new SqlConnection(dbFunctions.connection);
            using (
                SqlCommand obj_Sqlcommand =
                    new SqlCommand("SELECT DISTINCT ITEMNM AS txt FROM STK_ITEM WHERE ITEMNM LIKE  @SearchText +'%'",
                        conn))
            {
                conn.Open();
                obj_Sqlcommand.Parameters.AddWithValue("@SearchText", txt);
                SqlDataReader obj_result = obj_Sqlcommand.ExecuteReader();
                while (obj_result.Read())
                {
                    result.Add(obj_result["txt"].ToString().TrimEnd());
                }
            }
            return result;
        }

        [WebMethod(EnableSession = true)]
        [ScriptMethod(ResponseFormat = ResponseFormat.Json)]
        public List<string> GetCompletionListItemAuthorName(string txt)
        {
            // your code to query the database goes here
            List<string> result = new List<string>();
            SqlConnection conn = new SqlConnection(dbFunctions.connection);
            using (
                SqlCommand obj_Sqlcommand =
                    new SqlCommand("SELECT DISTINCT AUTHOR AS txt FROM STK_ITEM WHERE AUTHOR LIKE  @SearchText +'%'",
                        conn))
            {
                conn.Open();
                obj_Sqlcommand.Parameters.AddWithValue("@SearchText", txt);
                SqlDataReader obj_result = obj_Sqlcommand.ExecuteReader();
                while (obj_result.Read())
                {
                    result.Add(obj_result["txt"].ToString().TrimEnd());
                }
            }
            return result;
        }

        [WebMethod(EnableSession = true)]
        [ScriptMethod(ResponseFormat = ResponseFormat.Json)]
        public List<string> GetCompletionListItemPublicationName(string txt)
        {
            // your code to query the database goes here
            List<string> result = new List<string>();
            SqlConnection conn = new SqlConnection(dbFunctions.connection);
            using (
                SqlCommand obj_Sqlcommand =
                    new SqlCommand("SELECT DISTINCT PUBNM AS txt FROM STK_ITEM WHERE PUBNM LIKE  @SearchText +'%'", conn)
                )
            {
                conn.Open();
                obj_Sqlcommand.Parameters.AddWithValue("@SearchText", txt);
                SqlDataReader obj_result = obj_Sqlcommand.ExecuteReader();
                while (obj_result.Read())
                {
                    result.Add(obj_result["txt"].ToString().TrimEnd());
                }
            }
            return result;
        }










        // ********************************Omor Boi Ghor*****************************************//
        // ********************************Omor Boi Ghor*****************************************//
        // ********************************Omor Boi Ghor*****************************************//
        // ********************************Omor Boi Ghor*****************************************//
        // ********************************Omor Boi Ghor*****************************************//
        // ********************************Omor Boi Ghor*****************************************//
        // ********************************Omor Boi Ghor*****************************************//
        // ********************************Omor Boi Ghor*****************************************//
        // ********************************Omor Boi Ghor*****************************************//
        // ********************************Omor Boi Ghor*****************************************//
        // ********************************Omor Boi Ghor*****************************************//
        // ********************************Omor Boi Ghor*****************************************//



        [WebMethod(EnableSession = true)]
        [ScriptMethod(ResponseFormat = ResponseFormat.Json)]
        public List<string> GetCompletionListTransNo(string txt)
        {
            // your code to query the database goes here
            List<string> result = new List<string>();
            SqlConnection conn = new SqlConnection(dbFunctions.connection);
            using (
                SqlCommand obj_Sqlcommand =
                    new SqlCommand(
                        "SELECT TRANSNO AS txt FROM STK_TRANSMST WHERE TRANSTP='SALE'  AND TRANSNO  LIKE  @SearchText +'%'", conn))
            {
                conn.Open();
                obj_Sqlcommand.Parameters.AddWithValue("@SearchText", txt);
                SqlDataReader obj_result = obj_Sqlcommand.ExecuteReader();
                while (obj_result.Read())
                {
                    result.Add(obj_result["txt"].ToString().TrimEnd());
                }
            }
            return result;
        }
        [WebMethod(EnableSession = true)]
        [ScriptMethod(ResponseFormat = ResponseFormat.Json)]
        public List<string> GetItemNameMobileNumber(string txt)
        {
            // your code to query the database goes here
            List<string> result = new List<string>();
            SqlConnection conn = new SqlConnection(dbFunctions.connection);
            using (
                SqlCommand obj_Sqlcommand = new SqlCommand(@"SELECT DISTINCT CUSTCNO AS txt FROM STK_TRANSMST WHERE TRANSTP='SALE' AND ISNULL(CUSTCNO,'') <> '' 
                AND CUSTCNO LIKE  @SearchText + '%'", conn))
            {
                conn.Open();
                obj_Sqlcommand.Parameters.AddWithValue("@SearchText", txt);
                SqlDataReader obj_result = obj_Sqlcommand.ExecuteReader();
                while (obj_result.Read())
                {
                    result.Add(obj_result["txt"].ToString().TrimEnd());
                }
            }
            return result;
        }
        [WebMethod(EnableSession = true)]
        [ScriptMethod(ResponseFormat = ResponseFormat.Json)]
        public string GetOrderInvoiceNo(string year)
        {
            // your code to query the database goes here
            return StockTransaction.OrderTransactionNo(year);
        }
        [WebMethod]
        [ScriptMethod(ResponseFormat = ResponseFormat.Json)]
        public List<OneValuePass> GetCompletionListPartyNameWithId(string term)
        {
            List<string> s = new List<string>();
            List<OneValuePass> lst = new List<OneValuePass>();
            SqlConnection conn = new SqlConnection(dbFunctions.connection);
            using (SqlCommand obj_Sqlcommand = new SqlCommand(@"SELECT PARTYNM,PARTYID FROM STK_PARTY WHERE PARTYNM LIKE  @SearchText +'%'  ORDER BY PARTYNM", conn))
            {
                conn.Open();
                obj_Sqlcommand.Parameters.AddWithValue("@SearchText", term);
                SqlDataReader obj_result = obj_Sqlcommand.ExecuteReader();
                while (obj_result.Read())
                {
                    string result = obj_result["PARTYNM"].ToString().TrimEnd();
                    string id = obj_result["PARTYID"].ToString().TrimEnd();
                    lst.Add(new OneValuePass { value = id, label = result });
                }
                var q = lst.ToList();
                return (List<OneValuePass>)q;

            }
        }
        [WebMethod]
        [ScriptMethod(ResponseFormat = ResponseFormat.Json)]
        public List<ThreeValuePass> GetCompletionListItemNameWithId(string term)
        {
            List<string> s = new List<string>();
            List<ThreeValuePass> lst = new List<ThreeValuePass>();
            SqlConnection conn = new SqlConnection(dbFunctions.connection);
            using (SqlCommand obj_Sqlcommand = new SqlCommand(@"SELECT ITEMNM, ITEMID,MUNIT,SALRT  FROM STK_ITEM  WHERE ITEMNM LIKE  @SearchText +'%' AND ISNULL(STATUS,'A')='A' ORDER BY ITEMNM", conn))
            {
                conn.Open();
                obj_Sqlcommand.Parameters.AddWithValue("@SearchText", term);
                SqlDataReader obj_result = obj_Sqlcommand.ExecuteReader();
                while (obj_result.Read())
                {
                    string itemnm = obj_result["ITEMNM"].ToString().TrimEnd();
                    string itmeid = obj_result["ITEMID"].ToString().TrimEnd();
                    string unit = obj_result["MUNIT"].ToString().TrimEnd();
                    string rate = obj_result["SALRT"].ToString().TrimEnd();
                    lst.Add(new ThreeValuePass { value = itemnm, label1 = itmeid, label2 = unit, label3 = rate });
                }
                var q = lst.ToList();
                return (List<ThreeValuePass>)q;

            }
        }

        [WebMethod(EnableSession = true)]
        [ScriptMethod(ResponseFormat = ResponseFormat.Json)]
        public List<OneValuePass> GetTransactionNo(string date)
        {
            // your code to query the database goes here
            List<OneValuePass> lst = new List<OneValuePass>();
            SqlConnection conn = new SqlConnection(dbFunctions.connection);
            DateTime datef = DateTime.Parse(date, dateformat, System.Globalization.DateTimeStyles.AssumeLocal);
            string transDate = datef.ToString("yyyy-MM-dd");
            using (SqlCommand obj_Sqlcommand = new SqlCommand(@"SELECT TRANSNO FROM STK_TRANSMST WHERE TRANSDT='" + transDate + "'", conn))
            {
                conn.Open();
                SqlDataReader obj_result = obj_Sqlcommand.ExecuteReader();
                while (obj_result.Read())
                {
                    string result = obj_result["TRANSNO"].ToString().TrimEnd();
                    string id = obj_result["TRANSNO"].ToString().TrimEnd();
                    lst.Add(new OneValuePass { value = id, label = result });
                }
                var q = lst.ToList();
                //foreach (var item in lst)
                //{
                //    q = lst.ToList();
                //}
                return (List<OneValuePass>)q;

            }
        }
        [WebMethod]
        [System.Web.Script.Services.ScriptMethod(ResponseFormat = ResponseFormat.Json)]
        public List<OneValuePass> GetCompaniesJSON(string term)
        {
            List<string> s = new List<string>();
            List<OneValuePass> lst = new List<OneValuePass>();
            SqlConnection conn = new SqlConnection(dbFunctions.connection);
            using (SqlCommand obj_Sqlcommand = new SqlCommand(@"SELECT ACCOUNTNM AS txt, ACCOUNTCD AS txtid fROM GL_ACCHART WHERE SUBSTRING(ACCOUNTCD,1,7) IN('2020202','2020201') AND STATUSCD='P' AND ACCOUNTNM  LIKE  @SearchText +'%'", conn))
            {
                conn.Open();
                obj_Sqlcommand.Parameters.AddWithValue("@SearchText", term);
                SqlDataReader obj_result = obj_Sqlcommand.ExecuteReader();
                while (obj_result.Read())
                {
                    string result = obj_result["txt"].ToString().TrimEnd();
                    string id = obj_result["txtid"].ToString().TrimEnd();
                    lst.Add(new OneValuePass { value = id, label = result });
                }
                var q = lst.ToList();
                //foreach (var item in lst)
                //{
                //    q = lst.ToList();
                //}
                return (List<OneValuePass>)q;

            }
        }
        [WebMethod]
        [System.Web.Script.Services.ScriptMethod(ResponseFormat = ResponseFormat.Json)]
        public List<OneValuePass> GetItemNameItemIdJSON(string term)
        {
            List<string> s = new List<string>();
            List<OneValuePass> lst = new List<OneValuePass>();
            SqlConnection conn = new SqlConnection(dbFunctions.connection);
            using (SqlCommand obj_Sqlcommand = new SqlCommand(@"SELECT ITEMNM+'  |  '+AUTHOR +'  |  '+ CASE WHEN STK_ITEM.VERSION=0 THEN '' ELSE CAST(VERSION AS NVARCHAR) +' Edition' END AS ITEMNM, 
                    ITEMID FROM STK_ITEM WHERE ITEMNM LIKE  @SearchText +'%'  ORDER BY ITEMNM", conn))
            {
                conn.Open();
                obj_Sqlcommand.Parameters.AddWithValue("@SearchText", term);
                SqlDataReader obj_result = obj_Sqlcommand.ExecuteReader();
                while (obj_result.Read())
                {
                    string result = obj_result["ITEMNM"].ToString().TrimEnd();
                    string id = obj_result["ITEMID"].ToString().TrimEnd();
                    lst.Add(new OneValuePass { value = id, label = result });
                }
                var q = lst.ToList();
                return (List<OneValuePass>)q;

            }
        }



        [WebMethod(EnableSession = true)]
        [ScriptMethod(ResponseFormat = ResponseFormat.Json)]
        public List<string> GetVehiclesNumber(string txt)
        {
            // your code to query the database goes here
            List<string> result = new List<string>();
            SqlConnection conn = new SqlConnection(dbFunctions.connection);
            using (
                SqlCommand obj_Sqlcommand = new SqlCommand(@"SELECT DISTINCT VEHICLENO AS txt FROM STK_TRANSMST WHERE TRANSTP='SALE' AND ISNULL(VEHICLENO,'') <> '' 
                AND VEHICLENO LIKE  @SearchText + '%'", conn))
            {
                conn.Open();
                obj_Sqlcommand.Parameters.AddWithValue("@SearchText", txt);
                SqlDataReader obj_result = obj_Sqlcommand.ExecuteReader();
                while (obj_result.Read())
                {
                    result.Add(obj_result["txt"].ToString().TrimEnd());
                }
            }
            return result;
        }


        [WebMethod(EnableSession = true)]
        [ScriptMethod(ResponseFormat = ResponseFormat.Json)]
        public List<string> GetDriverName(string txt)
        {
            // your code to query the database goes here
            List<string> result = new List<string>();
            SqlConnection conn = new SqlConnection(dbFunctions.connection);
            using (
                SqlCommand obj_Sqlcommand = new SqlCommand(@"SELECT DISTINCT DRIVERNM AS txt FROM STK_TRANSMST WHERE TRANSTP='SALE' AND ISNULL(DRIVERNM,'') <> '' 
                AND DRIVERNM LIKE  @SearchText + '%'", conn))
            {
                conn.Open();
                obj_Sqlcommand.Parameters.AddWithValue("@SearchText", txt);
                SqlDataReader obj_result = obj_Sqlcommand.ExecuteReader();
                while (obj_result.Read())
                {
                    result.Add(obj_result["txt"].ToString().TrimEnd());
                }
            }
            return result;
        }




        [WebMethod(EnableSession = true)]
        [ScriptMethod(ResponseFormat = ResponseFormat.Json)]
        public List<string> GetAsstName(string txt)
        {
            // your code to query the database goes here
            List<string> result = new List<string>();
            SqlConnection conn = new SqlConnection(dbFunctions.connection);
            using (
                SqlCommand obj_Sqlcommand = new SqlCommand(@"SELECT DISTINCT ASSTNM AS txt FROM STK_TRANSMST WHERE TRANSTP='SALE' AND ISNULL(ASSTNM,'') <> '' 
                AND ASSTNM LIKE  @SearchText + '%'", conn))
            {
                conn.Open();
                obj_Sqlcommand.Parameters.AddWithValue("@SearchText", txt);
                SqlDataReader obj_result = obj_Sqlcommand.ExecuteReader();
                while (obj_result.Read())
                {
                    result.Add(obj_result["txt"].ToString().TrimEnd());
                }
            }
            return result;
        }
        [WebMethod]
        [ScriptMethod(ResponseFormat = ResponseFormat.Json)]
        public List<TowValue> GetCompletionListCreditSingleVoucherNew(string txt, string transtype)
        {
            var query = "";
            List<TowValue> lst = new List<TowValue>();
            SqlConnection conn = new SqlConnection(dbFunctions.connection);
            if (transtype == "MREC")
            {
                query = "SELECT ACCOUNTNM, ACCOUNTCD FROM GL_ACCHART WHERE substring(ACCOUNTCD,1,7) not in ('1020101','1020102','2020103') AND STATUSCD = 'P' and ACCOUNTNM LIKE  @SearchText +'%'";
            }
            else if (transtype == "MPAY")
            {
                query = "SELECT ACCOUNTNM, ACCOUNTCD FROM GL_ACCHART WHERE  substring(ACCOUNTCD,1,7)  in ('1020101','1020102','2020103') AND STATUSCD = 'P' and ACCOUNTNM LIKE   @SearchText +'%'";
            }
            else if (transtype == "JOUR")
            {
                query = "SELECT ACCOUNTNM, ACCOUNTCD FROM GL_ACCHART WHERE substring(ACCOUNTCD,1,7) not in ('1020101','1020102','2020103') AND STATUSCD = 'P' and ACCOUNTNM LIKE  @SearchText +'%'";
            }
            else if (transtype == "CONT")
            {
                query = "SELECT ACCOUNTNM, ACCOUNTCD FROM GL_ACCHART WHERE substring(ACCOUNTCD,1,7) in ('1020101','1020102','2020103') AND STATUSCD = 'P' and ACCOUNTNM LIKE  @SearchText +'%'";
            }
            using (SqlCommand objSqlcommand = new SqlCommand(query, conn))
            {
                conn.Open();
                objSqlcommand.Parameters.AddWithValue("@SearchText", txt);
                SqlDataReader objResult = objSqlcommand.ExecuteReader();
                while (objResult.Read())
                {
                    string result = objResult["ACCOUNTNM"].ToString().TrimEnd();
                    string id = objResult["ACCOUNTCD"].ToString().TrimEnd();
                    lst.Add(new TowValue { value = id, label = result });
                }
                var q = lst.ToList();
                return (List<TowValue>)q;
            }
        }
        [WebMethod]
        [ScriptMethod(ResponseFormat = ResponseFormat.Json)]
        public List<TowValue> GetCompletionListtDebitSingleVoucherNew(string txt, string transtype)
        {
            var query = "";
            List<TowValue> lst = new List<TowValue>();
            SqlConnection conn = new SqlConnection(dbFunctions.connection);
            if (transtype == "MREC")
            {
                query = "SELECT ACCOUNTNM, ACCOUNTCD  FROM GL_ACCHART WHERE substring(ACCOUNTCD,1,7) in ('1020101','1020102','2020103')  AND STATUSCD = 'P' and ACCOUNTNM LIKE  @SearchText +'%'";
            }
            else if (transtype == "MPAY")
            {
                query = "SELECT ACCOUNTNM, ACCOUNTCD  FROM GL_ACCHART WHERE substring(ACCOUNTCD,1,7) not in ('1020101','1020102','2020103') AND STATUSCD = 'P' and ACCOUNTNM LIKE   @SearchText +'%'";
            }
            else if (transtype == "JOUR")
            {
                query = "SELECT ACCOUNTNM, ACCOUNTCD  FROM GL_ACCHART WHERE substring(ACCOUNTCD,1,7) not in ('1020101','1020102','2020103') AND STATUSCD = 'P' and ACCOUNTNM LIKE  @SearchText +'%'";
            }
            else if (transtype == "CONT")
            {
                query = "SELECT ACCOUNTNM, ACCOUNTCD FROM GL_ACCHART WHERE substring(ACCOUNTCD,1,7) in ('1020101','1020102','2020103') AND STATUSCD = 'P' and ACCOUNTNM LIKE @SearchText +'%'";
            }
            using (SqlCommand objSqlcommand = new SqlCommand(query, conn))
            {
                conn.Open();
                objSqlcommand.Parameters.AddWithValue("@SearchText", txt);
                SqlDataReader objResult = objSqlcommand.ExecuteReader();
                while (objResult.Read())
                {
                    string result = objResult["ACCOUNTNM"].ToString().TrimEnd();
                    string id = objResult["ACCOUNTCD"].ToString().TrimEnd();
                    lst.Add(new TowValue { value = id, label = result });
                }
                var q = lst.ToList();
                return (List<TowValue>)q;
            }
        }



        [WebMethod(EnableSession = true)]
        [ScriptMethod(ResponseFormat = ResponseFormat.Json)]
        public List<string> GetCompletionListPostName(string txt)
        {
            // your code to query the database goes here
            List<string> result = new List<string>();
            SqlConnection conn = new SqlConnection(dbFunctions.connection);
            using (SqlCommand obj_Sqlcommand = new SqlCommand(@"SELECT POSTNM AS txt FROM HR_POST WHERE POSTNM LIKE  @SearchText +'%'", conn))
            {
                conn.Open();
                obj_Sqlcommand.Parameters.AddWithValue("@SearchText", txt);
                SqlDataReader obj_result = obj_Sqlcommand.ExecuteReader();
                while (obj_result.Read())
                {
                    result.Add(obj_result["txt"].ToString().TrimEnd());
                }
            }
            return result;
        }



        [WebMethod(EnableSession = true)]
        [ScriptMethod(ResponseFormat = ResponseFormat.Json)]
        public List<string> GetCompletionListeEmployeeName(string txt)
        {
            // your code to query the database goes here
            List<string> result = new List<string>();
            SqlConnection conn = new SqlConnection(dbFunctions.connection);
            using (SqlCommand obj_Sqlcommand = new SqlCommand(@"SELECT EMPNM AS txt FROM HR_EMP WHERE EMPNM LIKE  @SearchText +'%'", conn))
            {
                conn.Open();
                obj_Sqlcommand.Parameters.AddWithValue("@SearchText", txt);
                SqlDataReader obj_result = obj_Sqlcommand.ExecuteReader();
                while (obj_result.Read())
                {
                    result.Add(obj_result["txt"].ToString().TrimEnd());
                }
            }
            return result;
        }
        [WebMethod(EnableSession = true)]
        [ScriptMethod(ResponseFormat = ResponseFormat.Json)]
        public List<TowValue> GetEmployeeNameWithID(string txt )
        {
            // your code to query the database goes here
            List<TowValue> lst = new List<TowValue>();
            SqlConnection conn = new SqlConnection(dbFunctions.connection);
            using (SqlCommand obj_Sqlcommand = new SqlCommand(@"SELECT EMPNM AS txt,EMPID ID FROM HR_EMP WHERE EMPNM LIKE  @SearchText +'%'", conn))
            {
                conn.Open();
                obj_Sqlcommand.Parameters.AddWithValue("@SearchText", txt);
                SqlDataReader objResult = obj_Sqlcommand.ExecuteReader();
                while (objResult.Read())
                {
                    string result = objResult["txt"].ToString().TrimEnd();
                    string id = objResult["ID"].ToString().TrimEnd();
                    lst.Add(new TowValue { value = id, label = result });
                }
                var q = lst.ToList();
                return (List<TowValue>)q;
            } 
        } 

    }
    public class OneValuePass
    {
        string _value;
        string _label;

        public string value
        {
            get { return _value; }
            set { _value = value; }
        }
        public string label
        {
            get { return _label; }
            set { _label = value; }
        }

    }
    public class ThreeValuePass
    {
        string _value;
        string _label1;
        string _label2;
        string _label3;

        public string value
        {
            get { return _value; }
            set { _value = value; }
        }
        public string label1
        {
            get { return _label1; }
            set { _label1 = value; }
        }

        public string label2
        {
            get { return _label2; }
            set { _label2 = value; }
        }
        public string label3
        {
            get { return _label3; }
            set { _label3 = value; }
        }

        public class TowValue
        {
            public string value { get; set; }

            public string label { get; set; }
        }
        public class ItemNameIdRate
        {
            public string ItemName { get; set; }
            public string ItemId { get; set; }
            public string SateRate { get; set; }
        }
    }
}
