using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Data.SqlClient;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using AlchemyAccounting;
using DynamicMenu.Payroll.DataAccess;
using DynamicMenu.Payroll.Interface;

namespace DynamicMenu.Payroll.UI
{
    public partial class EmployeeLoan : System.Web.UI.Page
    {
        IFormatProvider dateformat = new System.Globalization.CultureInfo("fr-FR", true);
        PayrollDataAcces dob = new PayrollDataAcces();
        PayrollInterface iob = new PayrollInterface();
        SqlConnection conn = new SqlConnection(dbFunctions.connection);
        protected void Page_Load(object sender, EventArgs e)
        {
            if (Session["USERID"] == null)
            {
                Response.Redirect("~/Login/UI/Login.aspx");
            }
            else
            {
                const string formLink = "/Payroll/UI/EmployeeLoan.aspx";
                bool permission = UserPermissionChecker.checkParmit(formLink, "STATUS");
                if (permission == true)
                {
                    if (!Page.IsPostBack)
                    {
                        var Date = dbFunctions.Timezone(DateTime.Now);
                       // var uTp = HttpContext.Current.Session["USERTYPE"].ToString();
                        //var brCD = HttpContext.Current.Session["BrCD"].ToString();
                        dbFunctions.DropDownAddSelectTextWithValue(ddlEmployeeNM, "SELECT EMPNM,EMPID FROM HR_EMP");
                        txtDate.Text = Date.ToString("dd/MM/yyyy").ToUpper();
                        txtFdt.Text = Date.ToString("dd/MM/yyyy").ToUpper();
                        txtTdt.Text = Date.ToString("dd/MM/yyyy").ToUpper();

                        txtDate_OnTextChanged(sender, e);
                        Transid();
                    }
                }
                else
                {
                    Response.Redirect("../../DeshBoard/UI/Default.aspx");
                }
            }
        }

        protected void txtDate_OnTextChanged(object sender, EventArgs e)
        {
            if (btnEdit.Text == "Edit")
            {
                DateTime transdate = DateTime.Parse(txtDate.Text, dateformat, System.Globalization.DateTimeStyles.AssumeLocal);
                
                string varYear = transdate.ToString("yyyy");
                string mon = transdate.ToString("MMM").ToUpper();
                string year = transdate.ToString("yy");

                var mY = mon + "-" + year;
                txtMonthYear.Text = mY;
                Transid();
                ddlEmployeeNM.Focus();
            }
            else
            {
                DateTime transdate = DateTime.Parse(txtDate.Text, dateformat, System.Globalization.DateTimeStyles.AssumeLocal);
                string date = transdate.ToString("yyyy-MM-dd");
                string varYear = transdate.ToString("yyyy");
                string mon = transdate.ToString("MMM").ToUpper();
                string year = transdate.ToString("yy");

                var mY = mon + "-" + year;
                txtMonthYear.Text = mY;
                txtTransNO.Visible = false;
                ddlTransNo.Visible = true;
                dbFunctions.dropDownAddWithSelect(ddlTransNo, "SELECT TRANSNO FROM HR_EMPLOAN WHERE TRANSDT='" + date + "'");
            }

        }

        public void Transid()
        {
            DateTime transdate = DateTime.Parse(txtDate.Text, dateformat, System.Globalization.DateTimeStyles.AssumeLocal);
            string date = transdate.ToString("yyyy-MM-dd");

            dbFunctions.lblAdd($@"SELECT MAX(TRANSNO) FROM HR_EMPLOAN WHERE TRANSMY='{txtMonthYear.Text}'", lblSTID);
            long sid;

            if (lblSTID.Text == "")
            {
                txtTransNO.Text = "1";
            }
            else
            {
                sid = Convert.ToInt64(lblSTID.Text) + 1;
                txtTransNO.Text = sid.ToString();
            }
        }
        private void Clear()
        {
            txtFdt.Text = "";
            txtTdt.Text = "";
            ddlEmployeeNM.SelectedIndex = -1;
            txtDedAM.Text = "0";
            txtLoanAM.Text = "0";
            txtRemarks.Text = "";
            if (btnEdit.Text=="New")
            {
                ddlTransNo.SelectedIndex = -1;
            }

        }
        private void NullChack()
        {
            DateTime DefaultDtae = Convert.ToDateTime("01/01/1900");
            if (txtDate.Text == "")
                iob.Date = DefaultDtae;
            if (txtTdt.Text == "")
                iob.tdt = DefaultDtae;
            if (txtFdt.Text == "01/01/1900")
                iob.fdt = DefaultDtae;
        }

        protected void btnSubmit_Click(object sender, EventArgs e)
        {
            if (Session["USERID"] == null)
                Response.Redirect("~/Login/UI/Login.aspx");
            else
            {
                try
                {
                    Transid();
                    lblMSG.Text = "";
                    iob.UserPC = HttpContext.Current.Session["PCName"].ToString();
                    iob.Username = HttpContext.Current.Session["USERID"].ToString();
                    iob.Ipaddress = HttpContext.Current.Session["IpAddress"].ToString();
                    iob.ITime = dbFunctions.Timezone(DateTime.Now);
                    if (ddlEmployeeNM.Text == "--SELECT--")
                    {
                        ddlEmployeeNM.Focus();
                        lblMSG.Text = "Select Employee Name";
                        lblMSG.Visible = true;
                    }
                    else if (txtLoanAM.Text == "0")
                    {
                        txtLoanAM.Focus();
                        lblMSG.Text = "Please Give Loan Amount";
                        lblMSG.Visible = true;
                    }
                    else if (txtDedAM.Text == "0")
                    {
                        txtDedAM.Focus();
                        lblMSG.Text = "Please Give Amount";
                        lblMSG.Visible = true;
                    }
                    else if (txtFdt.Text == "")
                    {
                        txtFdt.Focus();
                    }
                    else if (txtTdt.Text == "")
                    {
                        txtTdt.Focus();
                    }

                    else
                    {
                        // EMPID();

                        if (txtDate.Text == "")
                            txtDate.Text = "01/01/1900";
                        DateTime DOB = DateTime.Parse(txtDate.Text, dateformat,
                            System.Globalization.DateTimeStyles.AssumeLocal);
                        if (txtFdt.Text == "")
                            txtFdt.Text = "01/01/1900";
                        DateTime fdt = DateTime.Parse(txtFdt.Text, dateformat,
                            System.Globalization.DateTimeStyles.AssumeLocal);
                        DateTime tdt = DateTime.Parse(txtTdt.Text, dateformat,
                            System.Globalization.DateTimeStyles.AssumeLocal);

                        iob.CmpID = int.Parse(Session["COMPANYID"].ToString());
                        iob.Ltude = Session["Location"].ToString();
                        iob.Date = DOB;
                        iob.MonthYear = txtMonthYear.Text;
                        iob.TransNo = Convert.ToInt64(txtTransNO.Text);
                        iob.EmployeeID = Convert.ToInt64(ddlEmployeeNM.SelectedValue);
                        iob.LoanAM = Convert.ToDecimal(txtLoanAM.Text);
                        iob.DeductionAM = Convert.ToDecimal(txtDedAM.Text);
                        iob.fdt = fdt;
                        iob.tdt = tdt;
                        iob.Remarks = txtRemarks.Text;
                        NullChack();
                        if (btnEdit.Text == "Edit")
                        {
                            dob.Insert_HR_EMPLOYEELOAN(iob);
                            Clear();
                            lblMSG.Visible = true;
                            lblMSG.Text = "Data Successfully Saved. !";
                            Transid();
                            ddlEmployeeNM.Focus();
                        }
                        else
                        {
                            lblMSG.Visible = true;
                        }

                    }
                }
                catch (Exception ex)
                {
                    Response.Write(ex);
                }
            }
        }

        protected void btnUpdate_Click(object sender, EventArgs e)
        {
            if (Session["USERID"] == null)
                Response.Redirect("~/Login/UI/Login.aspx");
            else
            {
                try
                {
                    lblMSG.Text = "";
                    iob.UPDUserPC = HttpContext.Current.Session["PCName"].ToString();
                    iob.UPDUsername = HttpContext.Current.Session["USERID"].ToString();
                    iob.UPDIpaddress = HttpContext.Current.Session["IpAddress"].ToString();
                    iob.Ltude = HttpContext.Current.Session["Location"].ToString();
                    iob.UPDTime = dbFunctions.Timezone(DateTime.Now);
                    if (ddlEmployeeNM.Text == "")
                        ddlEmployeeNM.Focus();
                    else
                    {
                        if (txtDate.Text == "")
                            txtDate.Text = "01/01/1900";
                        DateTime DOB = DateTime.Parse(txtDate.Text, dateformat, System.Globalization.DateTimeStyles.AssumeLocal);
                        if (txtFdt.Text == "")
                            txtFdt.Text = "01/01/1900";
                        DateTime fdt = DateTime.Parse(txtFdt.Text, dateformat, System.Globalization.DateTimeStyles.AssumeLocal);
                        DateTime tdt = DateTime.Parse(txtTdt.Text, dateformat, System.Globalization.DateTimeStyles.AssumeLocal);

                        iob.CmpID = int.Parse(Session["COMPANYID"].ToString());
                        iob.Date = DOB;
                        iob.MonthYear = txtMonthYear.Text;
                        iob.TransNo = Convert.ToInt64(ddlTransNo.Text);
                        iob.EmployeeID = Convert.ToInt64(ddlEmployeeNM.SelectedValue);
                        iob.LoanAM = Convert.ToDecimal(txtLoanAM.Text);
                        iob.DeductionAM = Convert.ToDecimal(txtDedAM.Text);
                        iob.fdt = fdt;
                        iob.tdt = tdt;
                        iob.Remarks = txtRemarks.Text;
                        NullChack();
                        if (lblMSG.Text == "")
                        {
                            dob.Update_HR_EMPLOYEELOAN(iob);
                            Clear();
                            ddlEmployeeNM.Focus();
                            lblMSG.Visible = true;
                            lblMSG.Text = "Data Successfully Updated. !";
                        }
                        else
                        {
                            lblMSG.Visible = false;
                        }
                    }
                }
                catch (Exception ex)
                {
                    Response.Write(ex);
                }
            }
        }
        
        protected void btnEdit_Click(object sender, EventArgs e)
        {
            if (btnEdit.Text == "Edit")
            {
                Clear();
                btnEdit.Text = "New";
                btnDelete.Visible = true;
                btnUpdate.Visible = true;
                btnSubmit.Visible = false;
                txtDate.Focus();
                txtTransNO.Visible = false;
                ddlTransNo.Visible = true;
                ddlEmployeeNM.Visible = true;
            }
            else
            {
                Clear();
                btnEdit.Text = "Edit";
                btnSubmit.Visible = true;
                ddlEmployeeNM.Focus();
                btnDelete.Visible = false;
                btnUpdate.Visible = false;
                txtTransNO.Visible = true;
                ddlTransNo.Visible = false;
                ddlEmployeeNM.Visible = true;
            }
        }

        protected void btnDelete_Click(object sender, EventArgs e)
        {
            try
            {
                conn.Open();
                SqlCommand cmd = new SqlCommand("DELETE FROM HR_EMPLOAN WHERE TRANSMY='" + txtMonthYear.Text + "' AND TRANSNO='" + ddlTransNo.Text + "'", conn);
                cmd.ExecuteNonQuery();
                lblMSG.Visible = true;
                lblMSG.Text = "Successfully Deleted !";
                Clear();
                conn.Close();
                ddlTransNo.Focus();
            }
            catch (Exception ex)
            {
                Response.Write(ex);
            }
        }


        protected void ddlTransNo_OnTextChanged(object sender, EventArgs e)
        {
            txtTransNO.Visible = false;
            ddlTransNo.Visible = true;
            ddlEmployeeNM.Visible = true;
            lblMSG.Text = "";
            if (btnEdit.Text == "New")
            {
                try
                {
                    var uTp = HttpContext.Current.Session["USERTYPE"].ToString();
                    var brCD = HttpContext.Current.Session["BrCD"].ToString();
                    
                    Label lblFromDT = new Label();
                    dbFunctions.lblAdd("SELECT CONVERT(nvarchar(10),DEDEFDT,103) DEDEFDT FROM HR_EMPLOAN WHERE TRANSNO='" + ddlTransNo.Text+"'", lblFromDT);
                    txtFdt.Text = lblFromDT.Text;
                    Label lblToDT = new Label();
                    dbFunctions.lblAdd("SELECT CONVERT(nvarchar(10),DEDETDT,103) DEDETDT FROM HR_EMPLOAN WHERE TRANSNO='" + ddlTransNo.Text + "'", lblToDT);
                    txtTdt.Text = lblToDT.Text;
                    dbFunctions.txtAdd("SELECT LOANAMT FROM HR_EMPLOAN WHERE TRANSNO='" + ddlTransNo.Text + "'", txtLoanAM);
                    dbFunctions.txtAdd("SELECT LOANAMT FROM HR_EMPLOAN WHERE TRANSNO='"+ddlTransNo.Text+"'", txtLoanAM);
                    dbFunctions.txtAdd("SELECT DEDAMT FROM HR_EMPLOAN WHERE TRANSNO='" + ddlTransNo.Text+"'", txtDedAM);
                    dbFunctions.txtAdd("SELECT TRANSMY FROM HR_EMPLOAN WHERE TRANSNO='" + ddlTransNo.Text+"'", txtMonthYear);
                    dbFunctions.txtAdd("SELECT REMARKS FROM HR_EMPLOAN WHERE TRANSNO='" + ddlTransNo.Text+"'", txtRemarks);
                    
                    string lblEMNM = dbFunctions.StringData("SELECT HR_EMP.EMPNM FROM HR_EMPLOAN INNER JOIN HR_EMP ON HR_EMPLOAN.EMPID=HR_EMP.EMPID  WHERE TRANSNO='" + ddlTransNo.Text + "'");
                    
                    ddlEmployeeNM.SelectedIndex = ddlEmployeeNM.Items.IndexOf(ddlEmployeeNM.Items.FindByText(lblEMNM));
                }
                catch (Exception)
                {
                    //ignore
                }
            }
            else
            {
                 //ddlTransNo.Focus();
            }
        }
    }
}
