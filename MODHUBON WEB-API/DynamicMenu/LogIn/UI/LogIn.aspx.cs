using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using AlchemyAccounting;
using DynamicMenu.LogData;
using DynamicMenu.LogIn.DataAccess;
using DynamicMenu.LogIn.Interface;

namespace DynamicMenu.LogIn.UI
{
    public partial class LogIn : System.Web.UI.Page
    {
        LogInDataAccess dob = new LogInDataAccess();
        LogInInterface iob = new LogInInterface();
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                txtUser.Focus();
            }
        }

        public string FieldCheck()
        {
            string checkResult = "";
            if (txtUser.Text == "")
            {
                checkResult = "Please write email of user name.";
                txtUser.Focus();
            }
            else if (txtPassword.Text == "")
            {
                checkResult = "Please write password.";
                txtPassword.Focus();
            }
            //else if (txtIp.Text == "")
            //{
            //    checkResult = "Ip not set, please refresh browser.";
            //    btnSubmit.Focus();
            //}
            //else if (txtLotiLongTude.Text == "")
            //{
            //    checkResult = "Location not set, please refresh browser or allow location.";
            //    btnSubmit.Focus();
            //}
            else
            {
                checkResult = "true";
            }

            return checkResult;
        }
        protected void btnSubmit_Click(object sender, EventArgs e)
        {

            if (FieldCheck() == "true")
            {
                string passByEmial = dbFunctions.StringData("SELECT LOGINPW FROM ASL_USERCO WHERE LOGINID='" + txtUser.Text + "'");

                if (passByEmial != "")
                {
                    var decryptData = dbFunctions.Encrypt(txtPassword.Text.Trim());
                    if (passByEmial == decryptData)
                    {
                        string timeFrom = dbFunctions.StringData("SELECT TIMEFR FROM ASL_USERCO WHERE LOGINID='" + txtUser.Text + "'");
                        string timeTo = dbFunctions.StringData("SELECT TIMETO FROM ASL_USERCO WHERE LOGINID='" + txtUser.Text + "'");
                        string userStatus = dbFunctions.StringData("SELECT STATUS FROM ASL_USERCO WHERE LOGINID='" + txtUser.Text + "'");
                        DateTime todayDate = dbFunctions.Timezone(DateTime.Now);
                        TimeSpan logTimeSpan = TimeSpan.Parse(dbFunctions.TimeFormat(todayDate));
                        TimeSpan timeForSpan = TimeSpan.Parse(timeFrom);
                        TimeSpan timeToSpan = TimeSpan.Parse(timeTo);
                        if (timeForSpan <= logTimeSpan && logTimeSpan <= timeToSpan && userStatus == "A")
                        {
                            SessionDeclare(txtUser.Text);

                            // logdata add start //
                            string lotileng = txtLotiLongTude.Text;
                            string logdata = "Log in Id: " + txtUser.Text + ", User Type: " + Session["USERTYPE"];
                            string logid = "LOGIN";
                            string tableid = "ASL_USERCO";
                            LogDataFunction.InsertLogData(lotileng, logid, tableid, logdata, txtIp.Text);
                            // logdata add start //
                            string urllink = txtlink.Text;

                            if (urllink != "")
                                if (urllink.Substring(0, 1) != "/")
                                    urllink = "/" + txtlink.Text;

                            if (urllink == "" || urllink == "/javascript:__doPostBack('ctl00$lnkLogOut','')" ||
                             urllink == "javascript:__doPostBack('ctl00$ContentPlaceHolder1$linkBEdit','')" || 
                             urllink == "/javascript:__doPostBack('ctl00$ContentPlaceHolder1$returnGrid$ctl04$lnk','')")
                                Response.Redirect("~/DashBoard/UI/Default.aspx");
                            else
                                Response.Redirect(urllink);

                        }
                        else
                        {
                            lblMsg.Text = "Your log in time: " + DateTime.ParseExact(timeFrom, "HH:mm", null).ToString("hh:mm tt") + " to." + DateTime.ParseExact(timeTo, "HH:mm", null).ToString("hh:mm tt") + "";
                            lblMsg.Visible = true;
                        }

                    }
                    else
                    {
                        lblMsg.Text = "Wrong user name & password.";
                        lblMsg.Visible = true;
                    }
                }
                else
                {
                    lblMsg.Text = "Wrong user name & password.";
                    lblMsg.Visible = true;
                }
            }
            else
            {
                lblMsg.Text = FieldCheck();
                lblMsg.Visible = true;
            }
        }

        public void SessionDeclare(string user)
        {
            SqlConnection con = new SqlConnection(dbFunctions.connection);
            con.Open();
            string query = "SELECT USERNM, OPTP, COMPID, USERID, BRANCHCD FROM ASL_USERCO WHERE LOGINID='" + user + "'";
            SqlCommand cmd = new SqlCommand(query, con);
            SqlDataReader dr = cmd.ExecuteReader();
            while (dr.Read())
            {
                Session["USERNAME"] = dr[0].ToString();
                Session["USERTYPE"] = dr[1].ToString();
                Session["COMPANYID"] = dr[2].ToString();
                Session["USERID"] = dr[3].ToString();
                Session["BrCD"] = dr[4].ToString();
            }
            dr.Close();
            con.Close();
            Session["LOGINID"] = user;

            Session["Location"] = txtLotiLongTude.Text;
            if (txtIp.Text != "")
                Session["IpAddress"] = txtIp.Text;
            else
                Session["IpAddress"] = dbFunctions.IpAddress();
            Session["PCName"] = dbFunctions.UserPc();



        }

    }
}