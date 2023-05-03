using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using AlchemyAccounting;
using DynamicMenu.Stock.DataAccess;
using DynamicMenu.Stock.Interface;

namespace DynamicMenu.Stock.UI
{
    public partial class OrderTimeChange : System.Web.UI.Page
    {
        StockDataAcces dob = new StockDataAcces();
        StockInterface iob = new StockInterface();
        protected void Page_Load(object sender, EventArgs e)
        {
            if (Session["USERID"] == null)
                Response.Redirect("~/Login/UI/Login.aspx");
            else
            {
                const string formLink = "/Stock/UI/OrderTimeChange.aspx";
                bool permission = UserPermissionChecker.checkParmit(formLink, "STATUS");
                if (permission)
                {
                    if (!IsPostBack)
                    {
                        var data = dbFunctions.DataReaderAdd(@"SELECT ORDERFR, ORDERTO FROM ASL_COMPANY WHERE COMPID='101'");
                        txtStartTime.Text = data[0];
                        txtEndTime.Text = data[1];
                    }
                }
                else
                {
                    Response.Redirect("../../DashBoard/UI/Default.aspx");
                }
            }
        }

        protected void btnUpdate_OnClick(object sender, EventArgs e)
        {
            if (txtStartTime.Text == "")
            {
                lblErr.Text = "Select Start Time.";
                lblErr.Visible = true;
            }
            else if (txtEndTime.Text == "")
            {

                lblErr.Text = "Select End Time.";
                lblErr.Visible = true;
            }
            else
            {
                lblErr.Text = "";
                lblErr.Visible = false;
                try
                {
                    TimeSpan timeForSpan = TimeSpan.Parse(txtStartTime.Text);
                    TimeSpan timeToSpan = TimeSpan.Parse(txtEndTime.Text);
                    iob.UserID = HttpContext.Current.Session["USERID"].ToString();
                    iob.Ipaddress = HttpContext.Current.Session["IpAddress"].ToString();
                    iob.UserPC = HttpContext.Current.Session["PCName"].ToString();
                    iob.ITime = dbFunctions.Timezone(DateTime.Now);
                    iob.StartTime = txtStartTime.Text;
                    iob.EndTime = txtEndTime.Text;

                    var s=dbFunctions.ExecuteQuery($@"UPDATE ASL_COMPANY SET ORDERFR='{iob.StartTime}', ORDERTO='{iob.EndTime}', UPDUSERID='{iob.UserID}', 
                    UPDTIME='{iob.ITime}', UPDIPNO='{iob.Ipaddress}', UPDLTUDE='' WHERE COMPID='{iob.UserID.Substring(0,3)}'");
                    if (s == "")
                    {
                        lblErr.ForeColor=Color.Green;
                        lblErr.Text = "Time Change Successfully.";
                        lblErr.Visible = true;
                    }
                }
                catch (Exception)
                {
                    lblErr.Text = "Time is not currect format.";
                    lblErr.Visible = false;
                }
               
            }

        }
    }
}