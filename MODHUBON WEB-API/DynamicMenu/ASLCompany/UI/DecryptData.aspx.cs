using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using AlchemyAccounting;

namespace DynamicMenu.ASLCompany.UI
{
    public partial class DecryptData : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                if (Session["USERID"] == null && Session["USERTYPE"].ToString() == "SUPERADMIN")
                {
                    Response.Redirect("~/LogIn/UI/LogIn.aspx");
                }
                else
                {
                    txtText.Focus();
                }
            }
        }

        protected void btnSubmit_Click(object sender, EventArgs e)
        {
            try
            {
                if (txtText.Text != "")
                {
                    var encrypttext = txtText.Text.Trim();
                    txtPlainText.Text = dbFunctions.Decrypt(encrypttext);
                }
                txtText.Focus();
            }
            catch (Exception)
            {
                txtText.Focus();
            }

        }

        protected void btnEncrypt_Click(object sender, EventArgs e)
        {
            try
            {
                if (txtText.Text != "")
                {
                    var encrypttext = txtText.Text.Trim();
                    txtPlainText.Text = dbFunctions.Encrypt(encrypttext);
                }
                txtText.Focus();
            }
            catch (Exception)
            {
                txtText.Focus();
            }
        }
    }
}