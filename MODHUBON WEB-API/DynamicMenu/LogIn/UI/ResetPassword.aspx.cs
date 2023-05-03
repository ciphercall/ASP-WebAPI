using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using AlchemyAccounting;
using DynamicMenu.LogIn.DataAccess;
using DynamicMenu.LogIn.Interface;

namespace DynamicMenu.LogIn.UI
{
    public partial class ResetPassword : System.Web.UI.Page
    {
        LogInDataAccess dob = new LogInDataAccess();
        LogInInterface iob = new LogInInterface();
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                Session["InvalidLink"] = "False";
                Session["UserIdNull"] = "TRUE";
                Session["PasswordChanged"] = "False";
                Session["PasswordNotChanged"] = "False";
                string userid = Request.QueryString["u"];
                string token = Request.QueryString["r"];
                if (userid != "" && token != "")
                {

                    string databaseToken = "";
                    string databaseTokenTime = "";
                    var connection = new SqlConnection(dbFunctions.connection);
                    connection.Open();
                    var sqlCommand = new SqlCommand(@"SELECT TOKENNO,TOKENINTM FROM ASL_USERCO 
                    WHERE USERID='" + userid + "' AND TOKENNO='" + token + "'", connection);
                    SqlDataReader dr = sqlCommand.ExecuteReader();
                    while (dr.Read())
                    {
                        databaseToken = dr["TOKENNO"].ToString();
                        databaseTokenTime = dr["TOKENINTM"].ToString();
                    }
                    dr.Close();
                    connection.Close();
                    if (databaseToken != "" || databaseTokenTime != "")
                    {
                        string timeNow = dbFunctions.Timezone(DateTime.Now).ToString("MM-dd-yyyy hh:mm:ss tt");

                        DateTime dateTime1 = Convert.ToDateTime(timeNow);
                        DateTime dateTime2 = Convert.ToDateTime(databaseTokenTime);
                        TimeSpan timeSpan = dateTime1 - dateTime2;
                        if (timeSpan > new TimeSpan(24, 0, 0))
                        {
                            Session["UserIdNull"] = "TRUE";
                            Session["InvalidLink"] = "True";
                        }
                        else
                        {
                            txtNewPass.Focus();
                            lblToken.Text = databaseToken;
                            lblUserid.Text = userid;
                            lblMsg.Visible = false;
                            Session["UserIdNull"] = "FALSE";
                            Session["InvalidLink"] = "False";
                        }
                    }
                    else
                    {
                        Session["UserIdNull"] = "TRUE";
                        Session["InvalidLink"] = "True";
                    }


                }
                else
                {
                    Session["UserIdNull"] = "TRUE";
                    Session["InvalidLink"] = "True";
                }
            }
        }
        public CheckResultWithMsg FieldCheck()
        {
            bool checkResult = false;
            string msg = "";
            if (txtConPass.Text == "")
            {
                msg = "Fill confirm password field.";
            }
            else if (txtNewPass.Text == "")
            {
                msg = "Fill new password field.";
            }
            else if (txtNewPass.Text != txtConPass.Text)
            {
                msg = "Password mismatch.";
            }
            else
            {
                checkResult = true;
            }
            return new CheckResultWithMsg()
            {
                Msg = msg,
                CheckResult = checkResult
            };
        }
        public void Refresh()
        {

            txtNewPass.Text = "";
            txtConPass.Text = "";
            txtNewPass.Focus();
        }
        public class CheckResultWithMsg
        {
            public string Msg { get; set; }
            public bool CheckResult { get; set; }
        }

        protected void btnSubmit_Click(object sender, EventArgs e)
        {
            //string unm = Session["UserName"].ToString();

            if (FieldCheck().CheckResult == false)
            {
                Response.Write("<script>alert('" + FieldCheck().Msg + "')</script>");
                Refresh();
            }
            else
            {
                iob.LotiLengTudeUpdate = "";
                iob.IpAddressUpdate = dbFunctions.IpAddress();
                iob.UserIdUpdate = Convert.ToInt16(lblUserid.Text);
                iob.UserPcUpdate = dbFunctions.UserPc();
                iob.InTimeUpdate = dbFunctions.Timezone(DateTime.Now);
                iob.Token = lblToken.Text;
                iob.UserID = Convert.ToInt16(lblUserid.Text);
                iob.Password = dbFunctions.Encrypt(txtNewPass.Text.Trim());

                string s = dob.UPDATE_ASL_PASSWORD_USING_TOKEN(iob);
                if (s == "")
                    NullSession();
                else
                {
                    Session["UserIdNull"] = "TRUE";
                    Session["PasswordChanged"] = "False";
                    Session["PasswordNotChanged"] = "True";
                    Session["InvalidLink"] = "False";
                }


            }
        }

        public void NullSession()
        {
            Session["PasswordChanged"] = "True";
            Session["UserIdNull"] = "TRUE";
            Session["PasswordNotChanged"] = "False";
            Session["InvalidLink"] = "False";
        }
    }
}
