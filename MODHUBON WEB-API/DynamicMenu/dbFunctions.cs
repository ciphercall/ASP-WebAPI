using System;
using System.Data;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Configuration;
using System.IO;
using System.Linq;
using System.Net;
using System.Security.Cryptography;
using System.Text;
using System.Web;
using System.Web.Providers.Entities;
using System.Web.Security;
using System.Web.UI;
using System.Web.UI.HtmlControls;
using System.Web.UI.WebControls;
using System.Web.UI.WebControls.WebParts;
using System.Xml.Linq;


namespace AlchemyAccounting
{
    public class dbFunctions
    {
        public static String connection = ConfigurationManager.ConnectionStrings["AslDbContext"].ToString();

        public static void dropDownAdd(DropDownList ob, String sql)
        {
            List<String> List = new List<string>();
            try
            {
                SqlConnection con = new SqlConnection(connection);
                con.Open();
                SqlCommand cmd = new SqlCommand(sql, con);
                SqlDataReader rd = cmd.ExecuteReader(); List.Clear();
                //List.Add("Select");
                while (rd.Read())
                {
                    List.Add(rd[0].ToString());
                }
                rd.Close();
                ob.Items.Clear();
                ob.Text = "";
                for (int i = 0; i < List.Count; i++)
                {
                    ob.Items.Add(List[i].ToString());
                }
            }
            catch { }
        }

        public static void dropDownAddWithSelect(DropDownList ob, String sql)
        {
            List<String> List = new List<string>();
            try
            {
                SqlConnection con = new SqlConnection(connection);
                con.Open();
                SqlCommand cmd = new SqlCommand(sql, con);
                SqlDataReader rd = cmd.ExecuteReader(); List.Clear();
                List.Add("--SELECT--");
                while (rd.Read())
                {
                    List.Add(rd[0].ToString());
                }
                rd.Close();
                ob.Items.Clear();
                ob.Text = "";
                for (int i = 0; i < List.Count; i++)
                {
                    ob.Items.Add(List[i].ToString());
                }
            }
            catch { }
        }
        public static void DropDownAddTextWithValue(DropDownList ob, String sql)
        {
            List<String> ListName = new List<string>();
            List<String> ListValue = new List<string>();
            try
            {
                SqlConnection con = new SqlConnection(connection);
                con.Open();
                SqlCommand cmd = new SqlCommand(sql, con);
                SqlDataReader rd = cmd.ExecuteReader();
                ListName.Clear();
                ListValue.Clear();
                //List.Add("Select");
                while (rd.Read())
                {
                    ListName.Add(rd[0].ToString());
                    ListValue.Add(rd[1].ToString());
                }
                rd.Close();
                ob.Items.Clear();

                ob.Text = "";
                for (int i = 0; i < ListName.Count; i++)
                {
                    ob.Items.Add(new ListItem(ListName[i].ToUpper(), ListValue[i]));
                }
            }
            catch { }
        }

        public static void DropDownAddSelectTextWithValue(DropDownList ob, String sql)
        {
            List<String> ListName = new List<string>();
            List<String> ListValue = new List<string>();
            try
            {
                SqlConnection con = new SqlConnection(connection);
                con.Open();
                SqlCommand cmd = new SqlCommand(sql, con);
                SqlDataReader rd = cmd.ExecuteReader();
                ListName.Clear();
                ListValue.Clear();
                ListName.Add("--SELECT--");
                ListValue.Add("--SELECT--");
                while (rd.Read())
                {
                    ListName.Add(rd[0].ToString());
                    ListValue.Add(rd[1].ToString());
                }
                rd.Close();
                ob.Items.Clear();

                ob.Text = "";
                for (int i = 0; i < ListName.Count; i++)
                {
                    ob.Items.Add(new ListItem(ListName[i].ToUpper(), ListValue[i]));
                }
            }
            catch { }
        }
        public static void DropDownAddAllTextWithValue(DropDownList ob, String sql)
        {
            List<String> ListName = new List<string>();
            List<String> ListValue = new List<string>();
            try
            {
                SqlConnection con = new SqlConnection(connection);
                con.Open();
                SqlCommand cmd = new SqlCommand(sql, con);
                SqlDataReader rd = cmd.ExecuteReader();
                ListName.Clear();
                ListValue.Clear();
                ListName.Add("ALL");
                ListValue.Add("ALL");
                while (rd.Read())
                {
                    ListName.Add(rd[0].ToString());
                    ListValue.Add(rd[1].ToString());
                }
                rd.Close();
                ob.Items.Clear();

                ob.Text = "";
                for (int i = 0; i < ListName.Count; i++)
                {
                    ob.Items.Add(new ListItem(ListName[i].ToUpper(), ListValue[i]));
                }
            }
            catch { }
        }

        public static void editableDropDownAdd(DropDownList ob, String sql)
        {
            List<String> List = new List<string>();
            try
            {
                SqlConnection con = new SqlConnection(connection);
                con.Open();
                SqlCommand cmd = new SqlCommand(sql, con);
                SqlDataReader rd = cmd.ExecuteReader(); List.Clear();
                List.Add("Select");
                while (rd.Read())
                {
                    List.Add(rd[0].ToString());
                }
                rd.Close();
                ob.Items.Clear();
                ob.Text = "";
                for (int i = 0; i < List.Count; i++)
                {
                    ob.Items.Add(List[i].ToString());
                }
            }
            catch { }
        }

        public static void listAdd(ListBox ob, String sql)
        {
            var list = new List<string>();
            try
            {
                var con = new SqlConnection(connection);
                con.Open();
                var cmd = new SqlCommand(sql, con);
                SqlDataReader rd = cmd.ExecuteReader();
                list.Clear();
                while (rd.Read())
                {
                    list.Add(rd[0].ToString());
                }
                rd.Close();
                ob.Items.Clear();
                ob.Text = "";
                foreach (var item in list)
                {
                    ob.Items.Add(item);
                }
            }
            catch { }
        }
        public static List<string> DataReaderAdd(String sql)
        {
            var list = new List<string>();
            try
            {
                var con = new SqlConnection(connection);
                con.Open();
                var cmd = new SqlCommand(sql, con);
                SqlDataReader rd = cmd.ExecuteReader();
                while (rd.Read())
                {
                    int count = rd.FieldCount;
                    for (int i = 0; i < count; i++)
                    {
                        list.Add(rd[i].ToString());
                    }
                }
                con.Close();
                rd.Close();
                return list;
            }
            catch
            {
                return list;
            }
        }
        public static void txtAdd(String sql, TextBox txtadd)
        {
            //String mystring = "";
            try
            {
                var con = new SqlConnection(connection);
                con.Open();
                var cmd = new SqlCommand(sql, con);
                SqlDataReader reader = cmd.ExecuteReader();
                if (reader.Read())
                {
                    txtadd.Text = reader[0].ToString();
                }
                con.Close();
                reader.Close();
            }
            catch { }
            //return List;
        }

        public static void lblAdd(String sql, Label lblAdd)
        {
            try
            {
                var con = new SqlConnection(connection);
                con.Open();
                var cmd = new SqlCommand(sql, con);
                SqlDataReader reader = cmd.ExecuteReader();
                if (reader.Read())
                {
                    lblAdd.Text = reader[0].ToString();
                }
                con.Close();
                reader.Close();
            }
            catch { }
        }

        public static string StringData(String sql)
        {
            string data = "";
            try
            {
                var con = new SqlConnection(connection);
                con.Open();
                var cmd = new SqlCommand(sql, con);
                var reader = cmd.ExecuteReader();
                if (reader.Read())
                {
                    data = reader[0].ToString();
                }
                con.Close();
                reader.Close();
            }
            catch(Exception x)
            {

            }
            return data;
        }
        public static string ExecuteQuery(String sql)
        {
            string data = "";
            SqlConnection con = new SqlConnection(connection);
            con.Open();
            SqlTransaction tran = null;
            try
            {
                if (con.State != ConnectionState.Open)
                    con.Open();
                tran = con.BeginTransaction();

                SqlCommand cmd = new SqlCommand(sql, con) { Transaction = tran };

                cmd.ExecuteNonQuery();
                tran.Commit();
                if (con.State != ConnectionState.Closed)
                    con.Close();
            }
            catch (Exception exception)
            {
                tran.Rollback();
                data = exception.ToString();
            }
            return data;
        }

        public static void gridViewAdd(GridView ob, String sql)
        {
            var table = new DataTable();
            try
            {
                var con = new SqlConnection(connection);
                con.Open();
                var cmd = new SqlCommand(sql, con);
                var ad = new SqlDataAdapter(cmd);
                ad.Fill(table);
                ob.DataSource = table;
                ob.DataBind();
            }
            catch { }
            //return List;
        }

        public static string Dayformat(DateTime dt)
        {
            string mydate = dt.ToString("dd/MM/yyyy");
            return mydate;
        }
        public static string DayformatHifen(DateTime dt)
        {
            string mydate = dt.ToString("dd-MMM-yyyy");
            return mydate;
        }
        public static string TimeFormat(DateTime tt)
        {
            string myTime = tt.ToString("HH:mm:ss");
            return myTime;
        }
        public static string monformat(DateTime mm)
        {
            string mymonth = mm.ToString("MMM");
            return mymonth;
        }
        public static DateTime Timezone(DateTime datetime)
        {
            TimeZoneInfo timeZoneInfo = TimeZoneInfo.FindSystemTimeZoneById("Central Asia Standard Time");
            var PrintDate = TimeZoneInfo.ConvertTime(datetime, timeZoneInfo);
            return PrintDate;
        }

        public static string IpAddress()
        {
#pragma warning disable 618
            IPHostEntry ipHostInfo = Dns.Resolve(Dns.GetHostName());
#pragma warning restore 618
            IPAddress ipAddress = ipHostInfo.AddressList[0];
            return ipAddress.ToString();
        }

        public static string UserPc()
        {
            return Dns.GetHostName();
        }

        public static bool FieldCheck(string[] field)
        {
            bool checkResult = false;
            foreach (var data in field)
            {
                if (data == "")
                {
                    checkResult = false;
                    break;
                }
                else
                    checkResult = true;
            }
            return checkResult;
        }
        public static string SessionId(string url)
        {
            string log = "";
            string size = "";
            string searchPar = url;
            int splitter = searchPar.IndexOf("=", System.StringComparison.Ordinal);
            if (splitter != -1)
            {
                string[] lineSplit = searchPar.Split('=');

                log = lineSplit[0];
                size = lineSplit[1];
            }
            return size;
        }

        public static string SliptText(string text, char sumbol, int indexNo)
        {
            string returnText = "";
            string searchPar = text;
            int splitter = searchPar.IndexOf(sumbol);
            if (splitter != -1)
            {
                string[] lineSplit = searchPar.Split(sumbol);

                returnText = lineSplit[indexNo];
            }
            return returnText;
        }
        public static string FbProfilePicture(string userId)
        {
            string fbImageLink = "";
            string userid = userId;
            string fbusername = StringData("SELECT FBPIMG FROM ASL_USERCO WHERE USERID='" + userid + "'");
            fbImageLink = "http:/" + "/graph.facebook.com/" + fbusername + "/picture?type=large";

            return fbImageLink;
        }
        public static string Encrypt(string clearText)
        {
            const string encryptionKey = "MAKV2SPBNI99212";
            byte[] clearBytes = Encoding.Unicode.GetBytes(clearText);
            using (var encryptor = Aes.Create())
            {
                var pdb = new Rfc2898DeriveBytes(encryptionKey, new byte[] { 0x49, 0x76, 0x61, 0x6e, 0x20, 0x4d, 0x65, 0x64, 0x76, 0x65, 0x64, 0x65, 0x76 });
                if (encryptor != null)
                {
                    encryptor.Key = pdb.GetBytes(32);
                    encryptor.IV = pdb.GetBytes(16);
                    using (var ms = new MemoryStream())
                    {
                        using (var cs = new CryptoStream(ms, encryptor.CreateEncryptor(), CryptoStreamMode.Write))
                        {
                            cs.Write(clearBytes, 0, clearBytes.Length);
                            cs.Close();
                        }
                        clearText = Convert.ToBase64String(ms.ToArray());
                    }
                }
            }
            return clearText;
        }

        public static string ExecuteCommand(String sql)
        {
            string data = "";
            try
            {
                SqlConnection con = new SqlConnection(connection);
                con.Open();
                SqlCommand cmd = new SqlCommand(sql, con);
                cmd.ExecuteNonQuery();
                con.Close();
            }
            catch
            {
                //ignore
            }
            return data;
        }
        public static string Decrypt(string cipherText)
        {
            const string encryptionKey = "MAKV2SPBNI99212";
            byte[] cipherBytes = Convert.FromBase64String(cipherText);
            using (var encryptor = Aes.Create())
            {
                var pdb = new Rfc2898DeriveBytes(encryptionKey, new byte[] { 0x49, 0x76, 0x61, 0x6e, 0x20, 0x4d, 0x65, 0x64, 0x76, 0x65, 0x64, 0x65, 0x76 });
                if (encryptor != null)
                {
                    encryptor.Key = pdb.GetBytes(32);
                    encryptor.IV = pdb.GetBytes(16);
                    using (var ms = new MemoryStream())
                    {
                        using (var cs = new CryptoStream(ms, encryptor.CreateDecryptor(), CryptoStreamMode.Write))
                        {
                            cs.Write(cipherBytes, 0, cipherBytes.Length);
                            cs.Close();
                        }
                        cipherText = Encoding.Unicode.GetString(ms.ToArray());
                    }
                }
            }
            return cipherText;
        }
        public static void BindDropDownWithValue(DropDownList ob, String sql)
        {
            SqlConnection con = new SqlConnection(dbFunctions.connection);
            con.Open();
            SqlCommand cmd = new SqlCommand(sql, con);
            SqlDataAdapter da = new SqlDataAdapter(cmd);
            DataSet ds = new DataSet();
            da.Fill(ds);
            ob.DataSource = ds;
            ob.DataTextField = "NM";
            ob.DataValueField = "ID";
            ob.DataBind();
            ob.Items.Insert(0, new ListItem("--SELECT--"));
            con.Close();
        }
        public static void BindDropDownWithoutValue(DropDownList ob, String sql)
        {
            SqlConnection con = new SqlConnection(dbFunctions.connection);
            con.Open();
            SqlCommand cmd = new SqlCommand(sql, con);
            SqlDataAdapter da = new SqlDataAdapter(cmd);
            DataSet ds = new DataSet();
            da.Fill(ds);
            ob.DataSource = ds;
            ob.DataTextField = "NM";
            //ob.DataValueField = "ID";
            ob.DataBind();
            ob.Items.Insert(0, new ListItem("--SELECT--"));
            con.Close();
        }
    }
}
