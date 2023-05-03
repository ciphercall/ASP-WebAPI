using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Net;
using System.Net.Mail;

namespace AlchemySMS.mail
{
    public class class_mail
    {
        public static bool SendMail(string from, string to, string body, string sub, string host, string userName, string pass)
        {
            bool ret = false;
            MailMessage oMail = new MailMessage(new MailAddress(from), new MailAddress(to));
            
            oMail.Body = body;
            oMail.Subject = sub;
            SmtpClient oSmtp = new SmtpClient();
            oSmtp.Host = host;
            oSmtp.Credentials = new NetworkCredential(userName, pass);
            oSmtp.EnableSsl = false;
            oSmtp.Send(oMail);
            ret = true;
            return ret;
        }
    }
}