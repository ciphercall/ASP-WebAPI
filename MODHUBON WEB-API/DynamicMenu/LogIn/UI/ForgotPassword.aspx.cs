using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Drawing;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using AlchemyAccounting;

namespace DynamicMenu.LogIn.UI
{
    public partial class ForgotPassword : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                emailInput.Focus();
                Session["Corectness"] = "OFF";
            }

        }

        protected void btnSubmit_Click(object sender, EventArgs e)
        {
            if (emailInput.Text != "")
            {
                string compamyName = dbFunctions.StringData("select compnm From [dbo].[ASL_COMPANY] where compid=101");
                string email = emailInput.Text;
                string sub = "Reset " + compamyName + " Password";
                string userid = dbFunctions.StringData("select userid From asl_userco where emailid='" + email + "'");
                var sb = new StringBuilder();
                string randomNumber = RandomNumber();
                DateTime time = dbFunctions.Timezone(DateTime.Now);
                if (userid != "")
                {

                    //update token number start
                    var connection = new SqlConnection(dbFunctions.connection);
                    connection.Open();
                    var sqlcommand = new SqlCommand("UPDATE ASL_USERCO SET TOKENNO='" + randomNumber + "', TOKENINTM='" + time + "' WHERE EMAILID='" + email + "' AND USERID='" + userid + "'", connection);
                    sqlcommand.ExecuteNonQuery();
                    connection.Close();
                    //update token number end




                    //Mail section start

                    sb.Append("Dear Sir,");
                    sb.Append(Environment.NewLine);
                    sb.Append("Somebody recently asked to reset your " + compamyName + " password.");
                    sb.Append(Environment.NewLine);
                    sb.Append("Click the following link.");
                    sb.Append(Environment.NewLine);
                    sb.Append(Environment.NewLine);
                    sb.Append("http://madhubanbd.com/LogIn/UI/ResetPassword.aspx?u=" + userid + "&r=" + randomNumber);
                    sb.Append(Environment.NewLine);
                    sb.Append(Environment.NewLine);

                    sb.Append("Best Regards");
                    sb.Append(Environment.NewLine);
                    sb.Append("Omar Boi Ghar");
                    sb.Append(Environment.NewLine);
                    sb.Append("http://madhubanbd.com");
                    sb.Append(Environment.NewLine);

                    AlchemySMS.mail.class_mail.SendMail("admin@alchemy-bd.com", email, sb.ToString(), sub, "mail.alchemy-bd.com", " admin@alchemy-bd.com", "Asl.admin@&123%");

                    //Mail section end


                    lblEmailId.Text = emailInput.Text;
                    Session["Corectness"] = "ON";
                    emailInput.Text = "";

                }
                else
                {
                    Session["Corectness"] = "OFF";
                    lblMsg.Text = "Email address not found. Please enter correct email address.";
                    lblMsg.Visible = true;
                    lblMsg.ForeColor = Color.Red;
                    emailInput.Text = "";
                    emailInput.Focus();
                }
            }
            else
            {
                Session["Corectness"] = "OFF";
                emailInput.Focus();
            }
        }

        public string RandomNumber()
        {
            var r = new Random();
            int rInt = r.Next(0, 10000); //for ints
            const int range = 100;
            var rDouble = (int)(r.NextDouble() * range * rInt);
            return rDouble.ToString(CultureInfo.InvariantCulture);
        }
    }
}