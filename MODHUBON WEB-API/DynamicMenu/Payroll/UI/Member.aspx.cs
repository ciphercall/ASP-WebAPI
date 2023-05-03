using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.IO;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using AlchemyAccounting;
using DynamicMenu.Payroll.DataAccess;
using DynamicMenu.Payroll.Interface;

namespace DynamicMenu.Payroll.UI
{
    public partial class Member : System.Web.UI.Page
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
                const string formLink = "/Payroll/UI/Member.aspx";
                bool permission = UserPermissionChecker.checkParmit(formLink, "STATUS");
                if (permission == true)
                {
                    if (!Page.IsPostBack)
                    {
                       dbFunctions.DropDownAddTextWithValue(ddlDPT, "SELECT DISTINCT DEPTNM, DEPTID FROM HR_DEPT");
                        txtEmpNM.Focus();

                        var uTp = HttpContext.Current.Session["USERTYPE"].ToString();
                        //var brCD = HttpContext.Current.Session["BrCD"].ToString();
                        //if (uTp == "COMPADMIN")
                       dbFunctions.DropDownAddTextWithValue(ddlCostPId, "SELECT BRANCHNM, BRANCHCD FROM ASL_BRANCH");
                        //else
                        //   dbFunctions.DropDownAddTextWithValue(ddlCostPId, "SELECT BRANCHNM, BRANCHCD FROM ASL_BRANCH WHERE BRANCHCD='" + brCD + "'");

                       dbFunctions.DropDownAddSelectTextWithValue(ddlShift, "SELECT SHIFTNM, SHIFTID FROM HR_SHIFT");
                    }
                }
                else
                {
                    Response.Redirect("../../DeshBoard/UI/Default.aspx");
                }
            }
        }
        //[System.Web.Services.WebMethodAttribute(), System.Web.Script.Services.ScriptMethodAttribute()]
        //public static string[] GetCompletionMemberNM(string prefixText, int count, string contextKey)
        //{

        //    SqlConnection conn = new SqlConnection(dbFunctions.connection);
        //    SqlCommand cmd = new SqlCommand("SELECT EMPNM FROM HR_EMP WHERE EMPNM like '" + prefixText + "%'", conn);
        //    SqlDataReader oReader;
        //    conn.Open();
        //    List<String> CompletionSet = new List<string>();
        //    oReader = cmd.ExecuteReader(CommandBehavior.CloseConnection);
        //    while (oReader.Read())
        //        CompletionSet.Add(oReader["EMPNM"].ToString());
        //    return CompletionSet.ToArray();
        //}
        private void Clear()
        {
            txtCRDISUDT.Text = "";
            txtCRDNO.Text = "";
            txtEmpBld.Text = "";
            txtEmpCNO.Text = "";
            txtEmpDOB.Text = "";
            txtEMPEmail.Text = "";
            txtEmpGNM.Text = "";
            txtEMPIDM.Text = "";
            txtEmpMNM.Text = "";
            txtEmpNM.Text = "";
            txtEmpPerAddress.Text = "";
            txtEmpPreAddress.Text = "";
            txtEmpVoterID.Text = "";
            txtJoinDT.Text = "";
            txtRef1Address.Text = "";
            txtRef1CNO.Text = "";
            txtRef1Desig.Text = "";
            txtRef1NM.Text = "";
            txtRef2Address.Text = "";
            txtRef2CNO.Text = "";
            txtRef2Desig.Text = "";
            txtRef2NM.Text = "";
            ddlDPT.SelectedIndex = -1;
            ddlEmpGen.SelectedIndex = -1;
            ddlEMPTP.SelectedIndex = -1;
            ddlSTATS.SelectedIndex = -1;
            txtID.Text = "";
            ddlCostPId.SelectedIndex = -1;
            ddlShift.SelectedIndex = -1;

        }
        private void NullChack()
        {
            DateTime DefaultDtae = Convert.ToDateTime("01-01-1900");
            if (txtCRDISUDT.Text == "")
                iob.CRDISUDT = DefaultDtae;
            if (txtCRDNO.Text == "")
                iob.CRDNO = "";
            if (txtEmpBld.Text == "")
                iob.BldGRP = "";
            if (txtEmpDOB.Text == "01/01/1900")
                iob.DOB = DefaultDtae;
            if (txtEMPEmail.Text == "")
                iob.Email = "";
            if (txtEmpGNM.Text == "")
                iob.EmpGNM = "";
            if (txtEmpMNM.Text == "")
                iob.EmpMNM = "";
            if (txtEmpPerAddress.Text == "")
                iob.PerAdrs = "";
            if (txtEmpPreAddress.Text == "")
                iob.PreAdrs = "";
            if (txtEmpVoterID.Text == "")
                iob.VotrID = "";
            if (txtJoinDT.Text == "")
                iob.JoinDT = DefaultDtae;
            if (txtRef1Address.Text == "")
                iob.Ref1Adrs = "";
            if (txtRef1CNO.Text == "")
                iob.Ref1CNO = "";
            if (txtRef1Desig.Text == "")
                iob.Ref1Desig = "";
            if (txtRef1NM.Text == "")
                iob.Ref1NM = "";
            if (txtRef2Address.Text == "")
                iob.Ref2Adrs = "";
            if (txtRef2CNO.Text == "")
                iob.Ref2CNO = "";
            if (txtRef2Desig.Text == "")
                iob.Ref2Desig = "";
            if (txtRef2NM.Text == "")
                iob.Ref2NM = "";
        }
        private void EMPID()
        {
            string CMPID = int.Parse("101").ToString();
            string EMPID = "";
            dbFunctions.lblAdd("SELECT MAX(EMPID) FROM HR_EMP  WHERE COMPID='" + CMPID + "'", lblEmpID);
            if (lblEmpID.Text == "")
            {
                EMPID = CMPID + "00001";
            }
            else
            {
                string Substr = lblEmpID.Text.Substring(3, 5);
                int subint = int.Parse(Substr) + 1;
                if (subint < 10)
                {
                    EMPID = CMPID + "0000" + subint;
                }
                else if (subint < 100)
                {
                    EMPID = CMPID + "000" + subint;
                }
                else if (subint < 1000)
                {
                    EMPID = CMPID + "00" + subint;
                }
                else if (subint < 10000)
                {
                    EMPID = CMPID + "0" + subint;
                }
                else if (subint < 100000)
                {
                    EMPID = CMPID + subint;
                }

            }
            iob.EMPID = int.Parse(EMPID);
        }

        public string ImageUpload(FileUpload fd, string fileNmunique)
        {
            lblMSG.Text = "";
            string img = "";
            try
            {
                if (fd.HasFile)
                {
                    string fileName = fd.FileName;
                    string exten = Path.GetExtension(fileName);

                    if (fd.FileBytes.Length > 200000)
                    {
                        lblMSG.Visible = true;
                        lblMSG.Text = "Upload Image to large! file must be less then 200kb";
                    }
                    else
                    {
                        lblMSG.Visible = false;

                        //here we have to restrict file type            
                        if (exten != null)
                        {
                            exten = exten.ToLower();
                            var acceptedFileTypes = new string[4];
                            acceptedFileTypes[0] = ".jpg";
                            acceptedFileTypes[1] = ".jpeg";
                            acceptedFileTypes[2] = ".gif";
                            acceptedFileTypes[3] = ".png";
                            bool acceptFile = false;
                            for (int i = 0; i <= 3; i++)
                            {
                                if (exten == acceptedFileTypes[i])
                                {
                                    acceptFile = true;
                                }
                            }
                            if (!acceptFile)
                            {
                                lblMSG.Visible = true;
                                lblMSG.Text = "Upload Image is not a permitted file type!";
                                fd.Focus();
                            }
                            else
                            {


                                //upload the file onto the server                   
                                fd.SaveAs(Server.MapPath("~/Payroll/EmpImg/" + fileNmunique + fileName));
                                img = "~/Payroll/EmpImg/" + fileNmunique + fileName;
                                System.Drawing.Image objImage = System.Drawing.Image.FromFile(Server.MapPath(img));
                                int width = objImage.Width;
                                int height = objImage.Height;


                                if (width > 100 || height > 100)
                                {
                                    img = "";
                                    lblMSG.Visible = true;
                                    lblMSG.Text = "Image is not in actual resulation! resulation size must be height and width 100X100 px.";
                                    var file = new FileInfo(img);
                                    file.Delete();
                                }
                            }
                        }
                    }
                }
                else
                    img = "";
            }
            catch (Exception)
            {
                img = "";
            }


            return img;
        }
        protected void btnSubmit_Click(object sender, EventArgs e)
        {
            if (Session["USERID"] == null)
                Response.Redirect("~/Login/UI/Login.aspx");
            else
            {
                try
                {
                    lblMSG.Text = "";
                    iob.UserPC = HttpContext.Current.Session["PCName"].ToString();
                    iob.Username = HttpContext.Current.Session["USERID"].ToString();
                    iob.Ipaddress = HttpContext.Current.Session["IpAddress"].ToString();
                    iob.ITime =dbFunctions.Timezone(DateTime.Now);
                    if (txtEmpNM.Text == "")
                        txtEmpNM.Focus();
                    else if (txtEmpCNO.Text == "")
                        txtEmpCNO.Focus();
                    else if (ddlEmpGen.Text == "--SELECT--")
                        ddlEmpGen.Focus();
                    else if (txtJoinDT.Text == "")
                        txtJoinDT.Focus();
                    else if (ddlDPT.Text == "--SELECT--")
                        ddlDPT.Focus();
                    else if (ddlEMPTP.Text == "--SELECT--")
                        ddlEMPTP.Focus();
                    else if (txtEMPIDM.Text == "")
                        txtEMPIDM.Focus();
                    else if (ddlSTATS.Text == "")
                        ddlSTATS.Focus();
                    else if (ddlShift.Text == "--SELECT--")
                        ddlShift.Focus();
                    else
                    {
                        EMPID();

                        if (txtEmpDOB.Text == "")
                            txtEmpDOB.Text = "01/01/1900";
                        DateTime DOB = DateTime.Parse(txtEmpDOB.Text, dateformat, System.Globalization.DateTimeStyles.AssumeLocal);
                        DateTime JoinDT = DateTime.Parse(txtJoinDT.Text, dateformat, System.Globalization.DateTimeStyles.AssumeLocal);
                        if (txtCRDISUDT.Text == "")
                            txtCRDISUDT.Text = "01/01/1900";
                        DateTime CRDISUDT = DateTime.Parse(txtCRDISUDT.Text, dateformat, System.Globalization.DateTimeStyles.AssumeLocal);

                        iob.CmpID = int.Parse(Session["COMPANYID"].ToString());
                        iob.EmpNM = txtEmpNM.Text;
                        iob.EmpGNM = txtEmpGNM.Text;
                        iob.EmpMNM = txtEmpMNM.Text;
                        iob.PreAdrs = txtEmpPreAddress.Text;
                        iob.PerAdrs = txtEmpPerAddress.Text;
                        iob.CNO = txtEmpCNO.Text;
                        iob.Email = txtEMPEmail.Text;
                        iob.DOB = DOB;
                        iob.Gndr = ddlEmpGen.Text;
                        iob.VotrID = txtEmpVoterID.Text;
                        iob.BldGRP = txtEmpBld.Text;
                        iob.Ref1NM = txtRef1NM.Text;
                        iob.Ref1Desig = txtRef1Desig.Text;
                        iob.Ref1Adrs = txtRef1Address.Text;
                        iob.Ref1CNO = txtRef1CNO.Text;
                        iob.Ref2NM = txtRef2NM.Text;
                        iob.Ref2Desig = txtRef2Desig.Text;
                        iob.Ref2Adrs = txtRef2Address.Text;
                        iob.Ref2CNO = txtRef2CNO.Text;
                        iob.JoinDT = JoinDT;
                        iob.DPTID = int.Parse(ddlDPT.SelectedValue);
                        iob.EMPTP = ddlEMPTP.Text;
                        iob.EMPIDM = txtEMPIDM.Text;
                        iob.CRDNO = txtCRDNO.Text;
                        iob.CRDISUDT = CRDISUDT;
                        iob.Stats = ddlSTATS.Text;
                        iob.CostPoolId = ddlCostPId.SelectedValue;
                        iob.SHIFTID = Convert.ToInt32(ddlShift.SelectedValue);
                        NullChack();
                        iob.Img = ImageUpload(FileUpload1, Convert.ToString(iob.EMPID));
                        if (lblMSG.Text == "")
                        {
                            dob.Insert_HR_EMP(iob);
                            Clear();
                            lblMSG.Visible = true;
                            lblMSG.Text = "Inserted !";
                            txtEmpNM.Focus();
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
                    iob.UPDTime =dbFunctions.Timezone(DateTime.Now);
                    if (txtEmpNM.Text == "")
                        txtEmpNM.Focus();
                    else if (txtEmpCNO.Text == "")
                        txtEmpCNO.Focus();
                    else if (ddlEmpGen.Text == "--SELECT--")
                        ddlEmpGen.Focus();
                    else if (txtJoinDT.Text == "")
                        txtJoinDT.Focus();
                    else if (ddlDPT.Text == "--SELECT--")
                        ddlDPT.Focus();
                    else if (ddlEMPTP.Text == "--SELECT--")
                        ddlEMPTP.Focus();
                    else if (txtEMPIDM.Text == "")
                        txtEMPIDM.Focus();
                    else if (ddlSTATS.Text == "--SELECT--")
                        ddlSTATS.Focus();
                    else if (ddlShift.Text == "--SELECT--")
                        ddlShift.Focus();
                    else
                    {
                        if (txtEmpDOB.Text == "")
                            txtEmpDOB.Text = "01/01/1900";
                        DateTime DOB = DateTime.Parse(txtEmpDOB.Text, dateformat, System.Globalization.DateTimeStyles.AssumeLocal);
                        DateTime JoinDT = DateTime.Parse(txtJoinDT.Text, dateformat, System.Globalization.DateTimeStyles.AssumeLocal);
                        if (txtCRDISUDT.Text == "")
                            txtCRDISUDT.Text = "01/01/1900";
                        DateTime CRDISUDT = DateTime.Parse(txtCRDISUDT.Text, dateformat, System.Globalization.DateTimeStyles.AssumeLocal);
                        iob.EMPID = int.Parse(txtID.Text);
                        iob.CmpID = int.Parse(Session["COMPANYID"].ToString());
                        iob.EmpNM = txtEmpNM.Text;
                        iob.EmpGNM = txtEmpGNM.Text;
                        iob.EmpMNM = txtEmpMNM.Text;
                        iob.PreAdrs = txtEmpPreAddress.Text;
                        iob.PerAdrs = txtEmpPerAddress.Text;
                        iob.CNO = txtEmpCNO.Text;
                        iob.Email = txtEMPEmail.Text;
                        iob.DOB = DOB;
                        iob.Gndr = ddlEmpGen.Text;
                        iob.Gndr = ddlEmpGen.Text;
                        iob.VotrID = txtEmpVoterID.Text;
                        iob.BldGRP = txtEmpBld.Text;
                        iob.Ref1NM = txtRef1NM.Text;
                        iob.Ref1Desig = txtRef1Desig.Text;
                        iob.Ref1Adrs = txtRef1Address.Text;
                        iob.Ref1CNO = txtRef1CNO.Text;
                        iob.Ref2NM = txtRef2NM.Text;
                        iob.Ref2Desig = txtRef2Desig.Text;
                        iob.Ref2Adrs = txtRef2Address.Text;
                        iob.Ref2CNO = txtRef2CNO.Text;
                        iob.JoinDT = JoinDT;
                        iob.DPTID = int.Parse(ddlDPT.SelectedValue);
                        iob.EMPTP = ddlEMPTP.Text;
                        iob.EMPIDM = txtEMPIDM.Text;
                        iob.CRDNO = txtCRDNO.Text;
                        iob.CRDISUDT = CRDISUDT;
                        iob.Stats = ddlSTATS.Text;
                        iob.CostPoolId = ddlCostPId.SelectedValue;
                        iob.SHIFTID = Convert.ToInt32(ddlShift.SelectedValue);
                        NullChack();
                        if (FileUpload1.FileName != "")
                            iob.Img = ImageUpload(FileUpload1, Convert.ToString(iob.EMPID));
                        else iob.Img = txtPath.Text;
                        if (lblMSG.Text == "")
                        {
                            dob.Update_HR_EMP(iob);
                            Clear();
                            txtEmpNM.Focus();
                            lblMSG.Visible = true;
                            lblMSG.Text = "Updated !";
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


        protected void txtEmpNM_TextChanged(object sender, EventArgs e)
        {
            lblMSG.Text = "";
            if (txtEmpNM.Text == "")
            {
                txtEmpNM.Focus();
            }
            else
            {
                if (btnEdit.Text == "New")
                {
                    try
                    {
                        var uTp = HttpContext.Current.Session["USERTYPE"].ToString();
                        //var brCD = HttpContext.Current.Session["BrCD"].ToString();
                        //if (uTp == "COMPADMIN")
                           dbFunctions.DropDownAddTextWithValue(ddlCostPId, "SELECT BRANCHNM, BRANCHCD FROM ASL_BRANCH");
                        //else
                          // dbFunctions.DropDownAddTextWithValue(ddlCostPId, "SELECT BRANCHNM, BRANCHCD FROM ASL_BRANCH WHERE BRANCHCD='" + brCD + "'");

                       dbFunctions.DropDownAddSelectTextWithValue(ddlShift, "SELECT SHIFTNM, SHIFTID FROM HR_SHIFT");
                       dbFunctions.DropDownAddTextWithValue(ddlDPT, "SELECT DISTINCT DEPTNM, DEPTID FROM HR_DEPT");

                       dbFunctions.txtAdd("SELECT EMPID FROM HR_EMP WHERE EMPNM='" + txtEmpNM.Text + "'", txtID);
                       dbFunctions.txtAdd("SELECT GUARDIANNM FROM HR_EMP WHERE EMPID='" + txtID.Text + "'", txtEmpGNM);
                        dbFunctions.lblAdd("SELECT EMPID FROM HR_EMP WHERE EMPID='" + txtID.Text + "'", lblEmpID);
                       dbFunctions.txtAdd("SELECT GUARDIANNM FROM HR_EMP WHERE EMPID='" + txtID.Text + "'", txtEmpGNM);
                       dbFunctions.txtAdd("SELECT MOTHERNM FROM HR_EMP WHERE EMPID='" + txtID.Text + "'", txtEmpMNM);
                       dbFunctions.txtAdd("SELECT ADDRESS_PRE FROM HR_EMP WHERE EMPID='" + txtID.Text + "'", txtEmpPreAddress);
                       dbFunctions.txtAdd("SELECT ADDRESS_PER FROM HR_EMP WHERE EMPID='" + txtID.Text + "'", txtEmpPerAddress);
                       dbFunctions.txtAdd("SELECT CONTACTNO FROM HR_EMP WHERE EMPID='" + txtID.Text + "'", txtEmpCNO);
                       dbFunctions.txtAdd("SELECT EMAILID FROM HR_EMP WHERE EMPID='" + txtID.Text + "'", txtEMPEmail);
                       dbFunctions.txtAdd("SELECT CONVERT(nvarchar(10),DOB,103) as DOB FROM HR_EMP WHERE EMPID='" + txtID.Text + "'", txtEmpDOB);
                        if (txtEmpDOB.Text == "1900-01-01")
                            txtEmpDOB.Text = "";
                        Label lblGender = new Label();
                        dbFunctions.lblAdd("SELECT GENDER FROM HR_EMP WHERE EMPID='" + txtID.Text + "'", lblGender);
                        ddlEmpGen.Text = lblGender.Text;
                       dbFunctions.txtAdd("SELECT VOTERIDNO FROM HR_EMP WHERE EMPID='" + txtID.Text + "'", txtEmpVoterID);
                       dbFunctions.txtAdd("SELECT BLOODGR FROM HR_EMP WHERE EMPID='" + txtID.Text + "'", txtEmpBld);
                       dbFunctions.txtAdd("SELECT IMAGE FROM HR_EMP WHERE EMPID='" + txtID.Text + "'", txtPath);
                        Image1.ImageUrl = txtPath.Text;
                       dbFunctions.txtAdd("SELECT REFNM1 FROM HR_EMP WHERE EMPID='" + txtID.Text + "'", txtRef1NM);
                       dbFunctions.txtAdd("SELECT REFDESIG1 FROM HR_EMP WHERE EMPID='" + txtID.Text + "'", txtRef1Desig);
                       dbFunctions.txtAdd("SELECT REFADD1 FROM HR_EMP WHERE EMPID='" + txtID.Text + "'", txtRef1Address);
                       dbFunctions.txtAdd("SELECT REFCNO1 FROM HR_EMP WHERE EMPID='" + txtID.Text + "'", txtRef1CNO);
                       dbFunctions.txtAdd("SELECT REFNM2 FROM HR_EMP WHERE EMPID='" + txtID.Text + "'", txtRef2NM);

                       dbFunctions.txtAdd("SELECT REFDESIG2 FROM HR_EMP WHERE EMPID='" + txtID.Text + "'", txtRef2Desig);
                       dbFunctions.txtAdd("SELECT REFADD2 FROM HR_EMP WHERE EMPID='" + txtID.Text + "'", txtRef2Address);
                       dbFunctions.txtAdd("SELECT REFCNO2 FROM HR_EMP WHERE EMPID='" + txtID.Text + "'", txtRef2CNO);
                       dbFunctions.txtAdd("SELECT CONVERT(nvarchar(10),JOININGDT,103) AS JOININGDT FROM HR_EMP WHERE EMPID='" + txtID.Text + "'", txtJoinDT);



                        Label lblEmpTP = new Label();
                        dbFunctions.lblAdd("SELECT EMPTP FROM HR_EMP WHERE EMPID='" + txtID.Text + "'", lblEmpTP);
                        ddlEMPTP.Text = lblEmpTP.Text;
                       dbFunctions.txtAdd("SELECT EMPIDM FROM HR_EMP WHERE EMPID='" + txtID.Text + "'", txtEMPIDM);
                       dbFunctions.txtAdd("SELECT CARDNO FROM HR_EMP WHERE EMPID='" + txtID.Text + "'", txtCRDNO);
                       dbFunctions.txtAdd("SELECT CONVERT(nvarchar(10),CARDIDT,103) AS CARDIDT FROM HR_EMP WHERE EMPID='" + txtID.Text + "'", txtCRDISUDT);
                        if (txtCRDISUDT.Text == "1900-01-01")
                            txtCRDISUDT.Text = "";
                        Label lblStats = new Label();
                        dbFunctions.lblAdd("SELECT STATUS FROM HR_EMP WHERE EMPID='" + txtID.Text + "'", lblStats);
                        ddlSTATS.SelectedIndex = ddlSTATS.Items.IndexOf(ddlSTATS.Items.FindByText(lblStats.Text));
                        //string shiftnm =dbFunctions.StringData(@"SELECT dbo.HR_SHIFT.SHIFTNM
                        //    FROM dbo.HR_EMP INNER JOIN
                        // dbo.HR_SHIFT ON dbo.HR_EMP.SHIFTID = dbo.HR_SHIFT.SHIFTID WHERE EMPID='" + txtID.Text + "'");
                        //if (shiftnm != "")
                        //    ddlShift.SelectedIndex = ddlShift.Items.IndexOf(ddlShift.Items.FindByText(shiftnm));

                        string ShiftNM =
                            dbFunctions.StringData(
                                "SELECT HR_EMP.SHIFTID FROM HR_EMP INNER JOIN HR_SHIFT ON HR_EMP.SHIFTID=HR_SHIFT.SHIFTID WHERE EMPID = '" + txtID.Text + "'");
                        ddlShift.Text = ShiftNM;

                        string costpnm =dbFunctions.StringData(@"SELECT ASL_BRANCH.BRANCHNM  FROM HR_EMP INNER JOIN ASL_BRANCH ON HR_EMP.COSTPID = ASL_BRANCH.BRANCHCD
                        WHERE HR_EMP.EMPID='" + txtID.Text + "'");

                        //string costpnm =dbFunctions.StringData(@"SELECT GL_COSTP.COSTPNM  FROM HR_EMP INNER JOIN GL_COSTP ON HR_EMP.COSTPID = GL_COSTP.COSTPID
                        //WHERE HR_EMP.EMPID='" + txtID.Text + "'");
                        if (costpnm != "")
                            ddlCostPId.SelectedIndex = ddlCostPId.Items.IndexOf(ddlCostPId.Items.FindByText(costpnm));


                        string dptnm =dbFunctions.StringData(@"SELECT HR_EMP.DEPTID FROM HR_DEPT INNER JOIN
                        HR_EMP ON HR_DEPT.DEPTID = HR_EMP.DEPTID WHERE HR_EMP.EMPID='" + txtID.Text + "'");
                        if (dptnm != "")
                            ddlDPT.SelectedIndex = ddlDPT.Items.IndexOf(ddlDPT.Items.FindByValue(dptnm));

                    }
                    catch (Exception)
                    {
                        //ignore
                    }
                }
                else
                {
                    txtEmpGNM.Focus();
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
                txtEmpNM.Focus();
                txtID.Visible = true;
                Image1.Visible = true;
            }
            else
            {
                Clear();
                btnEdit.Text = "Edit";
                txtID.Visible = false;
                btnSubmit.Visible = true;
                txtEmpNM.Focus();
                btnDelete.Visible = false;
                btnUpdate.Visible = false;
                Image1.Visible = false;
            }
        }

        protected void btnDelete_Click(object sender, EventArgs e)
        {
            try
            {
                conn.Open();
                SqlCommand cmd = new SqlCommand("DELETE FROM HR_EMP WHERE EMPID = '" + txtID.Text + "'", conn);
                cmd.ExecuteNonQuery();
                lblMSG.Visible = true;
                lblMSG.Text = "Deleted !";
                Clear();
                conn.Close();
                txtEmpNM.Focus();
            }
            catch (Exception ex)
            {
                Response.Write(ex);
            }
        }

        protected void txtPath_TextChanged(object sender, EventArgs e)
        {

        }
    }
}