using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using AlchemyAccounting;
using DynamicMenu.Accounts.DataAccess;
using DynamicMenu.Accounts.Interface;
using DynamicMenu.LogData;
using Image = System.Drawing.Image;

namespace DynamicMenu.Accounts.UI
{
    public partial class ChequeIssueInfo : System.Web.UI.Page
    {
        IFormatProvider dateformat = new System.Globalization.CultureInfo("fr-FR", true);
        SqlConnection conn;
        SqlCommand cmdd;

        AccountInterface eim = new AccountInterface();
        AccountDataAccess eida = new AccountDataAccess();
        protected void Page_Load(object sender, EventArgs e)
        {
            if (Session["UserName"] == null)
            {
                Response.Redirect("~/Login/UI/Login.aspx");
            }
            else
            {
                string formLink = "/ACCOUNTS/UI/ChequeReceiveInfo.aspx";
                bool permission = UserPermissionChecker.checkParmit(formLink, "STATUS");
                if (permission == true)
                {
                    if (!IsPostBack)
                    {
                     
                        DateTime today = dbFunctions.Timezone(DateTime.Now);
                        txtTransDT.Text = today.ToString("dd/MM/yyyy");
                    }
                    else
                    {

                    }
                }
                else
                {
                    Response.Redirect("../DeshBoard/UI/Default.aspx");
                }
            }
        }

        protected void btnSubmit_OnClick(object sender, EventArgs e)
        {
            GridShow();
        }

      

        protected void gvDetails_RowDataBound(object sender, GridViewRowEventArgs e)
        {
            // if (e.Row.RowType == DataControlRowType.Header)
            //  {

            //Label lblDebit = new Label();
            //Label lblCredit = new Label();

            //lblDebit = (Label)e.Row.FindControl("lblDebit");
            //lblCredit = (Label)e.Row.FindControl("lblCredit");

            //if (ddlTransType.Text == "MREC")
            //{
            //    lblDebit.Text = "Received To";
            //    lblCredit.Text = "Received From";
            //}
            //else if (ddlTransType.Text == "MPAY")
            //{
            //    lblDebit.Text = "Payment To";
            //    lblCredit.Text = "Payment From";
            //}
            //else if (ddlTransType.Text == "JOUR")
            //{
            //    lblDebit.Text = "Debited To";
            //    lblCredit.Text = "Credited To";
            //}
            //else if (ddlTransType.Text == "CONT")
            //{
            //    lblDebit.Text = "Deposited To";
            //    lblCredit.Text = "Withdrawn From";
            //}

            //  }
            if (e.Row.RowType == DataControlRowType.Footer)
            {
                //DateTime today = dbFunctions.Timezone(Convert.ToDateTime(txtTransDT.Text));
                //txtTransDT.Text = today.ToString("dd/MM/yyyy");

                DateTime today = DateTime.Parse(txtTransDT.Text, dateformat, System.Globalization.DateTimeStyles.AssumeLocal);
                txtTransDT.Text = today.ToString("dd/MM/yyyy");

                string mon = dbFunctions.Timezone(DateTime.Today.Date).ToString("MMM").ToUpper();
                string year = today.ToString("yy");
                string monYR = mon + "-" + year;

                dbFunctions.lblAdd(
                             @"Select max(cast(TRANSNO as int)) as TRANSNO FROM GL_CHEQUE where TRANSTP='MPAY'  and TRANSMY = '" + monYR + "'", lblVCount);
                int trans = 0;
                int transNo = 0;
                if (lblVCount.Text == "")
                {
                    e.Row.Cells[0].Text = "1";
                }
                else
                {
                    trans = int.Parse(lblVCount.Text);
                    transNo = trans + 1;
                    e.Row.Cells[0].Text = transNo.ToString();
                }
                //  TextBox txtChequeNo = (TextBox)gvDetails.FooterRow.FindControl("txtChequeNo");
                // txtChequeNo.Focus();


                //else  /////////Edit Mode
                //{
                //    DateTime eddate = DateTime.Parse(txtTransDate.Text, dateformat, System.Globalization.DateTimeStyles.AssumeLocal);
                //    string edate = eddate.ToString("yyyy-MM-dd");

                //    Int64 EditTransNo = 0;
                //    if (ddlVouch.Text == "--SELECT--")
                //    {
                //        EditTransNo = 0;
                //    }
                //    else
                //        EditTransNo = Convert.ToInt64(ddlVouch.Text);

                //    dbFunctions.lblAdd(@"SELECT MAX(SERIALNO) FROM GL_MTRANS WHERE TRANSTP = '" + ddlTransType.Text + "' AND TRANSDT = '" + edate + "' AND TRANSMY = '" + txtTransYear.Text + "' AND TRANSNO = " + EditTransNo + "", lblSLCount);

                //    int sl = 0;
                //    int slno = 0;
                //    if (lblSLCount.Text == "")
                //    {
                //        e.Row.Cells[0].Text = "1";
                //    }
                //    else
                //    {
                //        sl = int.Parse(lblSLCount.Text);
                //        slno = sl + 1;
                //        e.Row.Cells[0].Text = slno.ToString();
                //    }
                //}


            }
        }

        private void GridShow()
        {
            DateTime eddate = DateTime.Parse(txtTransDT.Text, dateformat, System.Globalization.DateTimeStyles.AssumeLocal);
            string edate = eddate.ToString("yyyy-MM-dd");
            conn = new SqlConnection(dbFunctions.connection);
            conn.Open();

            cmdd = new SqlCommand("SELECT ISNULL(TRANSNO,0) TRANSNO,TRANSMODE,DEBITCD,CREDITCD,CHEQUENO, CONVERT(NVARCHAR(11),CHEQUEDT,103) CHEQUEDT,CHQBANKBR, AMOUNT, REMARKS,GL_ACCHART.ACCOUNTNM AS ACNM, STATUS FROM" +
                                  " GL_CHEQUE INNER JOIN GL_ACCHART ON GL_CHEQUE.DEBITCD = GL_ACCHART.ACCOUNTCD WHERE TRANSDT='" + edate + "' AND CREDITCD='" + txtBankDEBITCD.Text + "' AND TRANSTP='MPAY'", conn);


            SqlDataAdapter da = new SqlDataAdapter(cmdd);
            DataSet ds = new DataSet();
            da.Fill(ds);
            conn.Close();


            if (ds.Tables[0].Rows.Count > 0)
            {
                gvDetails.DataSource = ds;
                gvDetails.DataBind();
                gvDetails.Visible = true;

                TextBox txtChequeNo = (TextBox)gvDetails.FooterRow.FindControl("txtChequeNo");
                txtChequeNo.Focus();

            }
            else
            {
                ds.Tables[0].Rows.Add(ds.Tables[0].NewRow());
                gvDetails.DataSource = ds;
                gvDetails.DataBind();
                int columncount = gvDetails.Rows[0].Cells.Count;
                gvDetails.Rows[0].Cells.Clear();
                gvDetails.Rows[0].Cells.Add(new TableCell());
                gvDetails.Rows[0].Cells[0].ColumnSpan = columncount;
                gvDetails.Rows[0].Cells[0].Text = "No Records Found";
                gvDetails.Rows[0].Visible = false;
            }
        }

        protected void gvDetails_RowCommand(object sender, GridViewCommandEventArgs e)
        {
          // ImageButton img = (ImageButton)sender;
            Session["VALUE"] = "2";
            if (e.CommandName.Equals("SaveCon"))
            {
                Label txtTransNo = (Label)gvDetails.FooterRow.FindControl("txtTransNo");
                TextBox txtRemark = (TextBox)gvDetails.FooterRow.FindControl("txtRemarks");
                TextBox txtAmount = (TextBox)gvDetails.FooterRow.FindControl("txtAmount");
                TextBox txtChqBankBr = (TextBox)gvDetails.FooterRow.FindControl("txtChqBankBr");
                TextBox txtCreditCD = (TextBox)gvDetails.FooterRow.FindControl("txtCreditCD");
                TextBox txtChequeNo = (TextBox)gvDetails.FooterRow.FindControl("txtChequeNo");
                TextBox txtChequeDt = (TextBox)gvDetails.FooterRow.FindControl("txtChequeDt");
                DropDownList ddlTransMode = (DropDownList)gvDetails.FooterRow.FindControl("ddlTransMode");
                DropDownList ddlStatus = (DropDownList)gvDetails.FooterRow.FindControl("ddlStatus");


                //if (Previousdata(gvDetails.FooterRow.Cells[0].Text) == false)
                //{
                if (txtChequeNo.Text == "")
                {
                    lblErrMsg.Visible = true;
                    lblErrMsg.Text = "particular input missing";
                    txtChequeNo.Focus();
                }
                else if (txtChequeDt.Text == "")
                {
                    lblErrMsg.Visible = true;
                    lblErrMsg.Text = "particular input missing";
                    txtChequeDt.Focus();
                }
                else if (txtChqBankBr.Text == "")
                {
                    lblErrMsg.Visible = true;
                    lblErrMsg.Text = "particular input missing";
                    txtChqBankBr.Focus();
                }
                else if (txtCreditCD.Text == "")
                {
                    lblErrMsg.Visible = true;
                    lblErrMsg.Text = "particular input missing";
                    txtCreditCD.Focus();
                }
                if (txtAmount.Text == "")
                {
                    lblErrMsg.Visible = true;
                    lblErrMsg.Text = "particular input missing";
                    txtAmount.Focus();
                }
                else
                {

                    eim.Transdt = DateTime.Parse(txtTransDT.Text, dateformat, System.Globalization.DateTimeStyles.AssumeLocal);

                    DateTime today = DateTime.Parse(txtTransDT.Text, dateformat, System.Globalization.DateTimeStyles.AssumeLocal);
                    txtTransDT.Text = today.ToString("dd/MM/yyyy");

                    string mon = dbFunctions.Timezone(DateTime.Today.Date).ToString("MMM").ToUpper();
                    string year = today.ToString("yy");
                    string monYR = mon + "-" + year;

                    eim.Transtp = "MPAY";
                    eim.Monyear = monYR;
                    eim.TransNo = gvDetails.FooterRow.Cells[0].Text;                  
                    eim.Transmode = ddlTransMode.SelectedValue;
                    eim.Chequeno = txtChequeNo.Text;
                    eim.Chequedt = DateTime.Parse(txtChequeDt.Text, dateformat, System.Globalization.DateTimeStyles.AssumeLocal);
                    // eim.EXPID = gvDetails.FooterRow.Cells[0].Text;
                    eim.Debitcd = txtCreditCD.Text;
                    eim.Creditcd = txtBankDEBITCD.Text;
                    eim.ChequeBankbr = txtChqBankBr.Text;
                    eim.Amount = Convert.ToDecimal(txtAmount.Text);
                    eim.Remarks = txtRemark.Text;
                    eim.Status = ddlStatus.SelectedValue;

                    eim.InTime = dbFunctions.Timezone(DateTime.Now);
                    eim.UpdateTime = dbFunctions.Timezone(DateTime.Now);
                    eim.Userpc = HttpContext.Current.Session["PCName"].ToString();
                    eim.IPAddress = HttpContext.Current.Session["IpAddress"].ToString();
                    eim.Userid = HttpContext.Current.Session["USERID"].ToString();

                    eida.IssChequeInsert(eim);
                }
                GridShow();
            }
            if (e.CommandName.Equals("Print"))
            {
              //  gvDetails_RowDeleting(sender,  e);
                //Session["VALUE"] ="1";
                int index = Convert.ToInt32(e.CommandArgument);
              //  GridView row = gvDetails.Rows[index];
               // string index1 = Int32.Parse(e.CommandArgument.ToString()).ToString();
                //   // lblCreditNM
                //    Label txtTransNo = (Label)gvDetails.FooterRow.FindControl("txtTransNo");
                //    TextBox txtRemark = (TextBox)gvDetails.FooterRow.FindControl("txtRemarks");
                //   TextBox txtAmount = (TextBox)gvDetails.FooterRow.FindControl("txtAmount");
                //    TextBox txtChqBankBr = (TextBox)gvDetails.FooterRow.FindControl("txtChqBankBr");
                //    TextBox lblCreditNM = (TextBox)gvDetails.FooterRow.FindControl("lblCreditNM");
                //    TextBox txtChequeNo = (TextBox)gvDetails.FooterRow.FindControl("txtChequeNo");
                //    TextBox txtChequeDt = (TextBox)gvDetails.FooterRow.FindControl("txtChequeDt");
                //    DropDownList ddlTransMode = (DropDownList)gvDetails.FooterRow.FindControl("ddlTransMode");
                //    DropDownList ddlStatus = (DropDownList)gvDetails.FooterRow.FindControl("ddlStatus");
                // Label lblReqNo = gvDetails.Rows[index].FindControl("lblREQNO") as Label;
                //   Label lblTransNo = (Label)gvDetails.Rows[]("lblTransNo");

                //    Session["transno"] = txtTransNo.Text;
                //    Session["date"] = txtTransDT.Text;
                //    Session["BankName"] = txtBankDEBITCD.Text;
                //    Session["txtRemark"] = txtRemark;
                //    Session["txtAmount"] = txtAmount;
                //    Session["txtChqBankBr"] = txtChqBankBr;
                //    Session["txtCreditCD"] = lblCreditNM;
                //    Session["txtChequeNo"] = txtChequeNo;
                //    Session["txtChequeDt"] = txtChequeDt;
                //    Session["ddlTransMode"] = ddlTransMode;
                //    Session["ddlStatus"] = ddlStatus;

                //    Page.ClientScript.RegisterStartupScript(
                //           GetType(), "OpenWindow", "window.open('../Report/Report/rptChequeIssueInformationVoucher.aspx','_newtab');", true);

                Label lblTransNo = (Label)gvDetails.Rows[index].FindControl("lblTransNo");
                Label lblChequeNo = (Label)gvDetails.Rows[index].FindControl("lblChequeNo");
                Label lblChequeDt = (Label)gvDetails.Rows[Convert.ToInt32(index)].FindControl("lblChequeDt");
                Label lblTransMode = (Label)gvDetails.Rows[Convert.ToInt32(index)].FindControl("lblTransMode");

                Label lblChqBankBr = (Label)gvDetails.Rows[Convert.ToInt32(index)].FindControl("lblChqBankBr");
                Label lblCreditNM = (Label)gvDetails.Rows[Convert.ToInt32(index)].FindControl("lblCreditNM");
                Label lblAmount = (Label)gvDetails.Rows[index].FindControl("lblAmount");
                Label lblRemarks = (Label)gvDetails.Rows[index].FindControl("lblRemarks");
                Label lblStatus = (Label)gvDetails.Rows[index].FindControl("lblStatus");


                Session["date"] = txtTransDT.Text;
                Session["banknm"] = txtBankNm.Text;

                Session["lblChequeNo"] = lblChequeNo.Text;
                Session["lblChequeDt"] = lblChequeDt.Text;
                Session["lblTransMode"] = lblTransMode.Text;
                Session["lblChqBankBr"] = lblChqBankBr.Text;
                Session["lblCreditNM"] = lblCreditNM.Text;
                Session["lblAmount"] = lblAmount.Text;
                Session["lblRemarks"] = lblRemarks.Text;
                Session["lblStatus"] = lblStatus.Text;
                Session["txtTransNo"] = lblTransNo.Text;


                ScriptManager.RegisterStartupScript(this,
                    this.GetType(), "OpenWindow",
                    "window.open('../Report/Report/rptChequeIssueInformationVoucher.aspx','_newtab');", true);



            }

        }


        protected void gvDetails_RowDeleting(object sender, GridViewDeleteEventArgs e)
        {


            //ImageButton imgbtnDelete = (ImageButton)gvDetails.Rows[e.RowIndex].FindControl("imgbtnDelete");
            //// string k = imgbtnDelete.AlternateText;
            //if (imgbtnDelete.AlternateText == "1")

            //{
                Label lblTransNo = (Label)gvDetails.Rows[e.RowIndex].FindControl("lblTransno");

                eim.Transdt = DateTime.Parse(txtTransDT.Text, dateformat,
                    System.Globalization.DateTimeStyles.AssumeLocal);

                DateTime today = DateTime.Parse(txtTransDT.Text, dateformat,
                    System.Globalization.DateTimeStyles.AssumeLocal);
                txtTransDT.Text = today.ToString("dd/MM/yyyy");

                string mon = dbFunctions.Timezone(DateTime.Today.Date).ToString("MMM").ToUpper();
                string year = today.ToString("yy");
                string monYR = mon + "-" + year;

                eim.Transtp = "MPAY";
                eim.Monyear = monYR;
                eim.TransNo = lblTransNo.Text;
                eim.Creditcd = txtBankDEBITCD.Text;


            //try
            //{
            //    // logdata add start //
            //    string lotileng = HttpContext.Current.Session["Location"].ToString();
            //    string ipAddress = HttpContext.Current.Session["IpAddress"].ToString();
            //    string logdata = dbFunctions.StringData(@"SELECT 'PARTYID : ' + PARTYID + ' ,REGID : ' + REGID + ' ,JOBTP : ' + JOBTP + ' ,
            //                    EXPID : ' + EXPID + ' ,EXPRT : ' + ISNULL(CONVERT(NVARCHAR(50), EXPRT, 103), '(NULL)') + ' ,REMARKS : ' + ISNULL(REMARKS, '(NULL)') + ' ,
            //                    USERPC : ' + ISNULL(USERPC, '(NULL)') + ' ,USERID : ' + ISNULL(USERID, '(NULL)') + ' ,UPDATEUSERID : ' + ISNULL(UPDATEUSERID, '(NULL)') + ' ,
            //                    INTIME : ' + ISNULL(CONVERT(NVARCHAR(50), INTIME, 103), '(NULL)') + ' ,UPDATETIME : ' + ISNULL(CONVERT(NVARCHAR(50), UPDATETIME, 103), '(NULL)') + ' ,
            //                    IPADDRESS : ' + ISNULL(IPADDRESS, '(NULL)') FROM CNF_EXPPARTY
            //                    where EXPID = '" + eim.EXPID + "' and PARTYID = '" + eim.PARTYID + "'and REGID = '" + eim.RegID + "'and JOBTP = '" + eim.JobTP + "'");
            //    string logid = "DELETE";
            //    string tableid = "CNF_EXPPARTY";
            //    LogDataFunction.InsertLogData(lotileng, logid, tableid, logdata, ipAddress);
            //    // logdata add end //
            //}
            //catch (Exception ex)
            //{
            //    //ignore
            //}


            try
            {
                // logdata add start //
                string lotileng = HttpContext.Current.Session["Location"].ToString();
                string ipAddress = HttpContext.Current.Session["IpAddress"].ToString();
                string logdata = dbFunctions.StringData(@"SELECT 'TRANSTP : ' + CONVERT(NVARCHAR(50),TRANSTP,103)+' | '+'TRANSDT :
' + ISNULL(CONVERT(NVARCHAR(50),TRANSDT,103),'(NULL)')+' | '+'TRANSMY : ' + TRANSMY+' | '+'TRANSNO : ' + TRANSNO+' | '+'TRANSMODE :
' + ISNULL(TRANSMODE,'(NULL)')+' | '+'DEBITCD : ' + ISNULL(DEBITCD,'(NULL)')+' | '+'CREDITCD : ' + ISNULL(CREDITCD,'(NULL)')+' | '+'CHEQUENO :
' + ISNULL(CHEQUENO,'(NULL)')+' | '+'CHEQUEDT : ' + ISNULL(CONVERT(NVARCHAR(50),CHEQUEDT,103),'(NULL)')+' | '+'CHQBANKBR :
' + ISNULL(CHQBANKBR,'(NULL)')+' | '+'AMOUNT : ' + ISNULL(CONVERT(NVARCHAR(50),AMOUNT,103),'(NULL)')+' | '+'REMARKS : ' + ISNULL(REMARKS,'(NULL)')+' | '+'STATUS : 
' + ISNULL(STATUS,'(NULL)')+' | '+'USERPC : ' + ISNULL(USERPC,'(NULL)')+' | '+'USERID : ' + ISNULL(USERID,'(NULL)')+' | '+'UPDATEUSERID :
' + ISNULL(UPDATEUSERID,'(NULL)')+' | '+'INTIME : ' + ISNULL(CONVERT(NVARCHAR(50),INTIME,103),'(NULL)')+' | '+'UPDATETIME :
' + ISNULL(CONVERT(NVARCHAR(50),UPDATETIME,103),'(NULL)')+' | '+'IPADDRESS : ' + ISNULL(IPADDRESS,'(NULL)')+' | ' FROM GL_CHEQUE
where TRANSNO = '" + eim.TransNo + "' and TRANSMY = '" + eim.Monyear + "'and TRANSTP = '" + eim.Transtp + "'and CREDITCD = '" + eim.Creditcd + "'");
                string logid = "DELETE";
                string tableid = "GL_CHEQUE";
                LogDataFunction.InsertLogData(lotileng, logid, tableid, logdata, ipAddress);
                // logdata add end //
                // WHERE TRANSNO =@TRANSNO AND TRANSMY=@TRANSMY AND TRANSTP=@TRANSTP AND CREDITCD=@CREDITCD 
            }
            catch (Exception ex)
            {
                //ignore
            }


            eida.RemoveChequeIss(eim);
                gvDetails.EditIndex = -1;
                GridShow();

                TextBox txtChequeNo = (TextBox)gvDetails.FooterRow.FindControl("txtChequeNo");
                txtChequeNo.Focus();


            //}
            //else
            //{
              //  ImageButton ImagebtnPPrint = (ImageButton)gvDetails.Rows[e.RowIndex].FindControl("ImagebtnPPrint");
                // string j = ImagebtnPPrint.AlternateText;
                //if (ImagebtnPPrint.AlternateText == "2")
                //{

                    //Label lblTransNo = (Label)gvDetails.Rows[e.RowIndex].FindControl("lblTransNo");
                    //Label lblChequeNo = (Label)gvDetails.Rows[e.RowIndex].FindControl("lblChequeNo");
                    //Label lblChequeDt = (Label)gvDetails.Rows[e.RowIndex].FindControl("lblChequeDt");
                    //Label lblTransMode = (Label)gvDetails.Rows[e.RowIndex].FindControl("lblTransMode");

                    //Label lblChqBankBr = (Label)gvDetails.Rows[e.RowIndex].FindControl("lblChqBankBr");
                    //Label lblCreditNM = (Label)gvDetails.Rows[e.RowIndex].FindControl("lblCreditNM");
                    //Label lblAmount = (Label)gvDetails.Rows[e.RowIndex].FindControl("lblAmount");
                    //Label lblRemarks = (Label)gvDetails.Rows[e.RowIndex].FindControl("lblRemarks");
                    //Label lblStatus = (Label)gvDetails.Rows[e.RowIndex].FindControl("lblStatus");


                    //Session["date"] = txtTransDT.Text;
                    //Session["banknm"] = txtBankNm.Text;

                    //Session["lblChequeNo"] = lblChequeNo.Text;
                    //Session["lblChequeDt"] = lblChequeDt.Text;
                    //Session["lblTransMode"] = lblTransMode.Text;
                    //Session["lblChqBankBr"] = lblChqBankBr.Text;
                    //Session["lblCreditNM"] = lblCreditNM.Text;
                    //Session["lblAmount"] = lblAmount.Text;
                    //Session["lblRemarks"] = lblRemarks.Text;
                    //Session["lblStatus"] = lblStatus.Text;
                    //Session["txtTransNo"] = lblTransNo.Text;


                    //ScriptManager.RegisterStartupScript(this,
                    //    this.GetType(), "OpenWindow",
                    //    "window.open('../Report/Report/rptChequeIssueInformationVoucher.aspx','_newtab');", true);



                //}
                //else
                //{
                //}

            }
            //    string t = Session["VALUE"].ToString();

            // ImageButton imgbtnDelete = (ImageButton)gvDetails.Rows[e.RowIndex].FindControl("imgbtnDelete");
            //// string k = imgbtnDelete.AlternateText;
            // if (imgbtnDelete.AlternateText == "1")

            // {
            //     Label lblTransNo = (Label) gvDetails.Rows[e.RowIndex].FindControl("lblTransno");

            //     eim.Transdt = DateTime.Parse(txtTransDT.Text, dateformat,
            //         System.Globalization.DateTimeStyles.AssumeLocal);

            //     DateTime today = DateTime.Parse(txtTransDT.Text, dateformat,
            //         System.Globalization.DateTimeStyles.AssumeLocal);
            //     txtTransDT.Text = today.ToString("dd/MM/yyyy");

            //     string mon = dbFunctions.Timezone(DateTime.Today.Date).ToString("MMM").ToUpper();
            //     string year = today.ToString("yy");
            //     string monYR = mon + "-" + year;

            //     eim.Transtp = "MPAY";
            //     eim.Monyear = monYR;
            //     eim.TransNo = lblTransNo.Text;
            //     eim.Creditcd = txtBankDEBITCD.Text;


            //     //try
            //     //{
            //     //    // logdata add start //
            //     //    string lotileng = HttpContext.Current.Session["Location"].ToString();
            //     //    string ipAddress = HttpContext.Current.Session["IpAddress"].ToString();
            //     //    string logdata = dbFunctions.StringData(@"SELECT 'PARTYID : ' + PARTYID + ' ,REGID : ' + REGID + ' ,JOBTP : ' + JOBTP + ' ,
            //     //                    EXPID : ' + EXPID + ' ,EXPRT : ' + ISNULL(CONVERT(NVARCHAR(50), EXPRT, 103), '(NULL)') + ' ,REMARKS : ' + ISNULL(REMARKS, '(NULL)') + ' ,
            //     //                    USERPC : ' + ISNULL(USERPC, '(NULL)') + ' ,USERID : ' + ISNULL(USERID, '(NULL)') + ' ,UPDATEUSERID : ' + ISNULL(UPDATEUSERID, '(NULL)') + ' ,
            //     //                    INTIME : ' + ISNULL(CONVERT(NVARCHAR(50), INTIME, 103), '(NULL)') + ' ,UPDATETIME : ' + ISNULL(CONVERT(NVARCHAR(50), UPDATETIME, 103), '(NULL)') + ' ,
            //     //                    IPADDRESS : ' + ISNULL(IPADDRESS, '(NULL)') FROM CNF_EXPPARTY
            //     //                    where EXPID = '" + eim.EXPID + "' and PARTYID = '" + eim.PARTYID + "'and REGID = '" + eim.RegID + "'and JOBTP = '" + eim.JobTP + "'");
            //     //    string logid = "DELETE";
            //     //    string tableid = "CNF_EXPPARTY";
            //     //    LogDataFunction.InsertLogData(lotileng, logid, tableid, logdata, ipAddress);
            //     //    // logdata add end //
            //     //}
            //     //catch (Exception ex)
            //     //{
            //     //    //ignore
            //     //}
            //     eida.RemoveChequeIss(eim);
            //     gvDetails.EditIndex = -1;
            //     GridShow();

            //     TextBox txtChequeNo = (TextBox) gvDetails.FooterRow.FindControl("txtChequeNo");
            //     txtChequeNo.Focus();


        
        protected void gvDetails_RowCancelingEdit(object sender, GridViewCancelEditEventArgs e)
        {
            gvDetails.EditIndex = -1;
            GridShow();

          string a=  gvDetails.EditIndex.ToString();

        }

        protected void gvDetails_RowEditing(object sender, GridViewEditEventArgs e)
        {
            gvDetails.EditIndex = e.NewEditIndex;
            GridShow();
            TextBox txtChequeNoEdit = (TextBox)gvDetails.Rows[e.NewEditIndex].FindControl("txtChequeNoEdit");
            txtChequeNoEdit.Focus();


            Label lblTransNoEdit = (Label)gvDetails.Rows[e.NewEditIndex].FindControl("lblTransNoEdit");
            TextBox txtstatusedit = (TextBox)gvDetails.Rows[e.NewEditIndex].FindControl("txtstatusedit");
            DropDownList ddlStatus = (DropDownList)gvDetails.Rows[e.NewEditIndex].FindControl("ddlStatusEdit");
            dbFunctions.txtAdd(@"select STATUS from GL_CHEQUE where CREDITCD='" + txtBankDEBITCD.Text + "' and TRANSNO='" + lblTransNoEdit.Text + "' and CHEQUENO = '" + txtChequeNoEdit.Text + "'  and TRANSTP = 'MPAY' ", txtstatusedit);
            ddlStatus.Text = txtstatusedit.Text;
        }

        protected void gvDetails_RowUpdating(object sender, GridViewUpdateEventArgs e)
        {
            Label lblTransNoEdit = (Label)gvDetails.Rows[e.RowIndex].FindControl("lblTransNoEdit");
            TextBox txtRemarkEdit = (TextBox)gvDetails.Rows[e.RowIndex].FindControl("txtRemarksEdit");
            TextBox txtAmountEdit = (TextBox)gvDetails.Rows[e.RowIndex].FindControl("txtAmountEdit");
            TextBox txtChqBankBrEdit = (TextBox)gvDetails.Rows[e.RowIndex].FindControl("txtChqBankBrEdit");
            TextBox txtChequeNoEdit = (TextBox)gvDetails.Rows[e.RowIndex].FindControl("txtChequeNoEdit");
            TextBox txtCreditCDEdit = (TextBox)gvDetails.Rows[e.RowIndex].FindControl("txtCreditCDEdit");
            TextBox txtChequeDtEdit = (TextBox)gvDetails.Rows[e.RowIndex].FindControl("txtChequeDtEdit");
            DropDownList ddlTransModeEdit = (DropDownList)gvDetails.Rows[e.RowIndex].FindControl("ddlTransModeEdit");
            DropDownList ddlStatusEdit = (DropDownList)gvDetails.Rows[e.RowIndex].FindControl("ddlStatusEdit");

            if (txtChequeNoEdit.Text == "")
            {
                lblErrMsg.Visible = true;
                lblErrMsg.Text = "particular input missing";
                txtChequeNoEdit.Focus();
            }
            else if (txtChequeDtEdit.Text == "")
            {
                lblErrMsg.Visible = true;
                lblErrMsg.Text = "particular input missing";
                txtChequeDtEdit.Focus();
            }
            else if (txtChqBankBrEdit.Text == "")
            {
                lblErrMsg.Visible = true;
                lblErrMsg.Text = "particular input missing";
                txtChqBankBrEdit.Focus();
            }
            else if (txtAmountEdit.Text == "")
            {
                lblErrMsg.Visible = true;
                lblErrMsg.Text = "particular input missing";
                txtAmountEdit.Focus();
            }

            else
            {

                eim.Transdt = DateTime.Parse(txtTransDT.Text, dateformat, System.Globalization.DateTimeStyles.AssumeLocal);
                DateTime today = DateTime.Parse(txtTransDT.Text, dateformat, System.Globalization.DateTimeStyles.AssumeLocal);
                txtTransDT.Text = today.ToString("dd/MM/yyyy");

                string mon = dbFunctions.Timezone(DateTime.Today.Date).ToString("MMM").ToUpper();
                string year = today.ToString("yy");
                string monYR = mon + "-" + year;

                eim.Transtp = "MPAY";
                eim.Monyear = monYR;
                eim.TransNo = lblTransNoEdit.Text.ToString();
                eim.Transmode = ddlTransModeEdit.Text;
                eim.Chequeno = txtChequeNoEdit.Text;
                eim.Chequedt = DateTime.Parse(txtChequeDtEdit.Text, dateformat, System.Globalization.DateTimeStyles.AssumeLocal);
                eim.Debitcd = txtCreditCDEdit.Text;
                eim.Creditcd = txtBankDEBITCD.Text;
                eim.ChequeBankbr = txtChqBankBrEdit.Text;
                eim.Amount = Convert.ToDecimal(txtAmountEdit.Text);
                eim.Remarks = txtRemarkEdit.Text;
                eim.Status = ddlStatusEdit.Text;

                eim.InTime = dbFunctions.Timezone(DateTime.Now);
                eim.UpdateTime = dbFunctions.Timezone(DateTime.Now);
                eim.Userpc = HttpContext.Current.Session["PCName"].ToString();
                eim.IPAddress = HttpContext.Current.Session["IpAddress"].ToString();
                eim.Userid = HttpContext.Current.Session["USERID"].ToString();

                try
                {
                    // logdata add start //
                    string lotileng = HttpContext.Current.Session["Location"].ToString();
                    string ipAddress = HttpContext.Current.Session["IpAddress"].ToString();
                    string logdata = dbFunctions.StringData(@"SELECT 'TRANSTP : ' + CONVERT(NVARCHAR(50),TRANSTP,103)+' | '+'TRANSDT :
' + ISNULL(CONVERT(NVARCHAR(50),TRANSDT,103),'(NULL)')+' | '+'TRANSMY : ' + TRANSMY+' | '+'TRANSNO : ' + TRANSNO+' | '+'TRANSMODE :
' + ISNULL(TRANSMODE,'(NULL)')+' | '+'DEBITCD : ' + ISNULL(DEBITCD,'(NULL)')+' | '+'CREDITCD : ' + ISNULL(CREDITCD,'(NULL)')+' | '+'CHEQUENO :
' + ISNULL(CHEQUENO,'(NULL)')+' | '+'CHEQUEDT : ' + ISNULL(CONVERT(NVARCHAR(50),CHEQUEDT,103),'(NULL)')+' | '+'CHQBANKBR :
' + ISNULL(CHQBANKBR,'(NULL)')+' | '+'AMOUNT : ' + ISNULL(CONVERT(NVARCHAR(50),AMOUNT,103),'(NULL)')+' | '+'REMARKS : ' + ISNULL(REMARKS,'(NULL)')+' | '+'STATUS : 
' + ISNULL(STATUS,'(NULL)')+' | '+'USERPC : ' + ISNULL(USERPC,'(NULL)')+' | '+'USERID : ' + ISNULL(USERID,'(NULL)')+' | '+'UPDATEUSERID :
' + ISNULL(UPDATEUSERID,'(NULL)')+' | '+'INTIME : ' + ISNULL(CONVERT(NVARCHAR(50),INTIME,103),'(NULL)')+' | '+'UPDATETIME :
' + ISNULL(CONVERT(NVARCHAR(50),UPDATETIME,103),'(NULL)')+' | '+'IPADDRESS : ' + ISNULL(IPADDRESS,'(NULL)')+' | ' FROM GL_CHEQUE
where TRANSNO = '" + eim.TransNo + "' and TRANSMY = '" + eim.Monyear + "'and TRANSTP = '" + eim.Transtp + "'and CREDITCD = '" + eim.Creditcd + "'");
                    string logid = "UPDATE";
                    string tableid = "GL_CHEQUE";
                    LogDataFunction.InsertLogData(lotileng, logid, tableid, logdata, ipAddress);
                    // logdata add end //
                    // WHERE TRANSNO =@TRANSNO AND TRANSMY=@TRANSMY AND TRANSTP=@TRANSTP AND CREDITCD=@CREDITCD 
                }
                catch (Exception ex)
                {
                    //ignore
                }

                eida.IssChequeUpdate(eim);
                gvDetails.EditIndex = -1;
                GridShow();
                TextBox txtChequeNo = (TextBox)gvDetails.FooterRow.FindControl("txtChequeNo");
                txtChequeNo.Focus();
            }
        }

        protected void gvDetails_RowDeleted(object sender, GridViewDeletedEventArgs e)
        {

        }

    
        protected void txtChequeNo_OnTextChanged(object sender, EventArgs e)
        {
            TextBox txtChequeNo = (TextBox)gvDetails.FooterRow.FindControl("txtChequeNo");
            dbFunctions.lblAdd("select count(*) from GL_CHEQUE  where CREDITCD='" + txtBankDEBITCD.Text + "' and CHEQUENO = '" + txtChequeNo.Text + "' and TRANSTP = 'MPAY'", lblcheck);
            if (Convert.ToInt32(lblcheck.Text)>=1)
            {
                TextBox txtRemark = (TextBox)gvDetails.FooterRow.FindControl("txtRemarks");
                TextBox txtAmount = (TextBox)gvDetails.FooterRow.FindControl("txtAmount");
                TextBox txtChqBankBr = (TextBox)gvDetails.FooterRow.FindControl("txtChqBankBr");
                TextBox txtCreditCD = (TextBox)gvDetails.FooterRow.FindControl("txtCreditCD");
                TextBox txtCreditNM = (TextBox)gvDetails.FooterRow.FindControl("txtCreditNM");
                // TextBox txtChequeNo = (TextBox)gvDetails.FooterRow.FindControl("txtChequeNo");
                TextBox txtmode = (TextBox)gvDetails.FooterRow.FindControl("txtmode");
                TextBox txtChequeDt = (TextBox)gvDetails.FooterRow.FindControl("txtChequeDt");
                DropDownList ddlTransMode = (DropDownList)gvDetails.FooterRow.FindControl("ddlTransMode");
                DropDownList ddlStatus = (DropDownList)gvDetails.FooterRow.FindControl("ddlStatus");
               
                dbFunctions.txtAdd(@"select  CONVERT(NVARCHAR(20), CHEQUEDT, 103) CHEQUEDT  from GL_CHEQUE where CREDITCD='" + txtBankDEBITCD.Text + "' and CHEQUENO = '" + txtChequeNo.Text + "' and TRANSTP = 'MPAY' ", txtChequeDt);
                //     dbFunctions.dropDownAdd(@"select TRANSMODE from GL_CHEQUE where DEBITCD='"+ txtBankDEBITCD + "' and CHEQUENO = '" + txtChequeNo + "' ", ddlTransMode);
                dbFunctions.txtAdd(@"select CHQBANKBR from GL_CHEQUE where CREDITCD='" + txtBankDEBITCD.Text + "' and TRANSTP = 'MPAY' and CHEQUENO = '" + txtChequeNo.Text + "' ", txtChqBankBr);
                dbFunctions.txtAdd(@"select ACCOUNTNM from GL_CHEQUE INNER JOIN GL_ACCHART ON GL_ACCHART.ACCOUNTCD=GL_CHEQUE.DEBITCD where CREDITCD='" + txtBankDEBITCD.Text + "' and CHEQUENO = '" + txtChequeNo.Text + "' and TRANSTP = 'MPAY' ", txtCreditNM);
                dbFunctions.txtAdd(@"select ACCOUNTCD from GL_CHEQUE INNER JOIN GL_ACCHART ON GL_ACCHART.ACCOUNTCD=GL_CHEQUE.DEBITCD where CREDITCD='" + txtBankDEBITCD.Text + "' and CHEQUENO = '" + txtChequeNo.Text + "' and TRANSTP = 'MPAY' ", txtCreditCD);
                dbFunctions.txtAdd(@"select AMOUNT from GL_CHEQUE where CREDITCD='" + txtBankDEBITCD.Text + "' and CHEQUENO = '" + txtChequeNo.Text + "' and TRANSTP = 'MPAY' ", txtAmount);
                dbFunctions.txtAdd(@"select TRANSMODE from GL_CHEQUE where CREDITCD='" + txtBankDEBITCD.Text + "' and CHEQUENO = '" + txtChequeNo.Text + "' and TRANSTP = 'MPAY' ", txtmode);
                ddlTransMode.Text = txtmode.Text;
         //       dbFunctions.txtAdd(@"select REMARKS from GL_CHEQUE where CREDITCD='" + txtBankDEBITCD.Text + "' and CHEQUENO = '" + txtChequeNo.Text + "' and TRANSTP = 'MPAY' ", txtRemark);



            }
            // Label txtTransNo = (Label)gvDetails.FooterRow.FindControl("txtTransNo");

           // DropDownList ddlStatus = (DropDownList)gvDetails.FooterRow.FindControl("ddlStatus");
           // ddlStatus.Focus();

        }

        protected void txtChequeNoEdit_OnTextChanged(object sender, EventArgs e)
        {
            GridViewRow row = ((GridViewRow)((TextBox)sender).NamingContainer);
       //     TextBox txtPURateEdit = (TextBox)row.FindControl("txtPURateEdit");
            TextBox txtChequeNo = (TextBox)gvDetails.FooterRow.FindControl("txtChequeNo");
            Label lblTransNoEdit = (Label) row.FindControl("lblTransNoEdit");
                TextBox txtRemark = (TextBox)row.FindControl("txtRemarksEdit");
                TextBox txtAmount = (TextBox)row.FindControl("txtAmountEdit");
                TextBox txtChqBankBr = (TextBox)row.FindControl("txtChqBankBrEdit");
                TextBox txtCreditCD = (TextBox)row.FindControl("txtCreditCDEdit");
                TextBox txtCreditNM = (TextBox)row.FindControl("txtCreditNMEdit");
                TextBox txtChequeNoEdit = (TextBox)row.FindControl("txtChequeNoEdit");
                TextBox txtmode = (TextBox)row.FindControl("txtmodeedit");
                TextBox txtChequeDt = (TextBox)row.FindControl("txtChequeDtEdit");
                DropDownList ddlTransMode = (DropDownList)row.FindControl("ddlTransModeEdit");
                DropDownList ddlStatus = (DropDownList)row.FindControl("ddlStatusEdit");

                dbFunctions.txtAdd(@"select CONVERT(NVARCHAR(20), CHEQUEDT, 103) CHEQUEDT from GL_CHEQUE where CREDITCD='" + txtBankDEBITCD.Text + "' and CHEQUENO = '" + txtChequeNoEdit.Text + "' and TRANSTP = 'MPAY' ", txtChequeDt);
                //     dbFunctions.dropDownAdd(@"select TRANSMODE from GL_CHEQUE where DEBITCD='"+ txtBankDEBITCD + "' and CHEQUENO = '" + txtChequeNo + "' ", ddlTransMode);
                dbFunctions.txtAdd(@"select CHQBANKBR from GL_CHEQUE where CREDITCD='" + txtBankDEBITCD.Text + "' and TRANSTP = 'MPAY'  and CHEQUENO = '" + txtChequeNoEdit.Text + "' ", txtChqBankBr);
                dbFunctions.txtAdd(@"select ACCOUNTNM from GL_CHEQUE INNER JOIN GL_ACCHART ON GL_ACCHART.ACCOUNTCD=GL_CHEQUE.DEBITCD where CREDITCD='" + txtBankDEBITCD.Text + "' and  CHEQUENO = '" + txtChequeNoEdit.Text + "' and TRANSTP = 'MPAY' ", txtCreditNM);
                dbFunctions.txtAdd(@"select ACCOUNTCD from GL_CHEQUE INNER JOIN GL_ACCHART ON GL_ACCHART.ACCOUNTCD=GL_CHEQUE.DEBITCD where CREDITCD='" + txtBankDEBITCD.Text + "' and  CHEQUENO = '" + txtChequeNoEdit.Text + "' and TRANSTP = 'MPAY' ", txtCreditCD);
                dbFunctions.txtAdd(@"select AMOUNT from GL_CHEQUE where CREDITCD='" + txtBankDEBITCD.Text + "' and CHEQUENO = '" + txtChequeNoEdit.Text + "'  and TRANSTP = 'MPAY' ", txtAmount);
                dbFunctions.txtAdd(@"select TRANSMODE from GL_CHEQUE where CREDITCD='" + txtBankDEBITCD.Text + "' and CHEQUENO = '" + txtChequeNoEdit.Text + "'  and TRANSTP = 'MPAY' ", txtmode);
                ddlTransMode.Text = txtmode.Text;
          //      dbFunctions.txtAdd(@"select REMARKS from GL_CHEQUE where CREDITCD='" + txtBankDEBITCD.Text + "' and CHEQUENO = '" + txtChequeNoEdit.Text + "' and TRANSTP = 'MPAY' ", txtRemark);


            txtChequeNoEdit.Focus();

            //DropDownList ddlStatus = (DropDownList)gvDetails.FooterRow.FindControl("ddlStatus");
            //ddlStatus.Focus();
        }
    }
}