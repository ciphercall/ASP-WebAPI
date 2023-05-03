using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data;
using System.Configuration;
using AlchemyAccounting;
using System.Data.SqlClient;
using System.Web.Services;
using System.Text;
using System.Collections.Specialized;
using DynamicMenu.Accounts.DataAccess;
using DynamicMenu.Accounts.Interface;
using System.Drawing;
using DynamicMenu.LogData;


namespace DynamicMenu.Accounts.UI
{
    public partial class ChartofAccounts : System.Web.UI.Page
    {
        public string prefixText { get; set; }

        public int count { get; set; }

        public string contextKey { get; set; }
        public int index { get; set; }
        protected void Page_Load(object sender, EventArgs e)
        {
            if (Session["USERID"] == null)
            {
                Response.Redirect("~/Login/UI/Login.aspx");
            }
            else
            {
                string formLink = "/Accounts/UI/ChartofAccounts.aspx";
                bool permission = UserPermissionChecker.checkParmit(formLink, "STATUS");
                if (permission == true)
                {

                    if (!IsPostBack)
                    {
                        ddlLevelID.AutoPostBack = true;
                        //txtExpen.Visible = false;
                        //txtIncome.Visible = false;
                        //txtLiabilty.Visible = false;
                        lblAccTP.Visible = false;
                        lblIncrLevel.Visible = false;
                        lblLvlID.Visible = false;
                        lblMxAccCode.Visible = false;
                        lblNewLvlCD.Visible = false;
                        lblresult.Visible = false;
                        lblSelLvlCD.Visible = false;
                        lblStatus.Visible = false;
                        ddlLevelID.Focus();

                    }
                }
                else
                {
                    Response.Redirect("../../DeshBoard/UI/Default.aspx");
                }
            }
        }

        protected void ddlLevelID_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (Session["USERID"] == null)
            {
                Response.Redirect("~/Login/UI/Login.aspx");
            }
            else if (ddlLevelID.SelectedItem.Text == "SELECT")
            {
                ddlLevelID.Focus();
            }
            else
            {
                Session["LAVELCD"] = null;
                if (ddlLevelID.Text == "1")
                {
                    txtHdName.Text = "";
                    txtCode.Text = "";
                    lblAccTP.Text = "D";
                    gvDetails.Visible = false;
                    Session["LAVELCD"] = "1%";
                }
                else if (ddlLevelID.Text == "2")
                {
                    txtCode.Text = "";
                    lblAccTP.Text = "C";
                    gvDetails.Visible = false;
                    Session["LAVELCD"] = "2%";
                }
                else if (ddlLevelID.Text == "3")
                {
                    txtCode.Text = "";
                    lblAccTP.Text = "C";
                    gvDetails.Visible = false;
                    Session["LAVELCD"] = "3%";
                }
                else if (ddlLevelID.Text == "4")
                {
                    txtCode.Text = "";
                    lblAccTP.Text = "D";
                    gvDetails.Visible = false;
                    Session["LAVELCD"] = "4%";
                }
                txtHdName.Text = "";
                lblBotCode.Text = "";
                lblLvlID.Text = "";
                txtHdName.Focus();
            }
        }
        protected void txtHdName_TextChanged(object sender, EventArgs e)
        {
            if (Session["USERID"] == null)
            {
                Response.Redirect("~/Login/UI/Login.aspx");
            }
            else
            {
                if (ddlLevelID.Text == "SELECT")
                {
                    Response.Write("<script>alert('Select Transaction Type?');</script>");
                }
                else
                {
                    string headnm = "";

                    string searchPar = txtHdName.Text;
                    int splitter = searchPar.IndexOf("|");
                    if (splitter != -1)
                    {
                        string[] lineSplit = searchPar.Split('|');

                        headnm = lineSplit[0];
                        if (txtHdName.Text != "")
                        {
                            if (ddlLevelID.SelectedValue == "1")
                                dbFunctions.txtAdd(
                                    @"Select ACCOUNTCD from GL_ACCHART where ACCOUNTCD like '1%' AND ACCOUNTNM = '" +
                                    headnm + "'", txtCode);
                            else if (ddlLevelID.SelectedValue == "2")
                                dbFunctions.txtAdd(
                               @"Select ACCOUNTCD from GL_ACCHART where ACCOUNTCD like '2%'  AND ACCOUNTNM = '" +
                               headnm + "'", txtCode);
                            else if (ddlLevelID.SelectedValue == "3")
                                dbFunctions.txtAdd(
                               @"Select ACCOUNTCD from GL_ACCHART where ACCOUNTCD like '3%'  AND ACCOUNTNM = '" +
                               headnm + "'", txtCode);
                            else if (ddlLevelID.SelectedValue == "4")
                                dbFunctions.txtAdd(
                                    @"Select ACCOUNTCD from GL_ACCHART where ACCOUNTCD like '4%'  AND ACCOUNTNM = '" +
                                    headnm + "'", txtCode);
                            else
                            {
                                ddlLevelID.SelectedIndex = -1;
                                txtHdName.Text = "";
                                ddlLevelID.Focus();
                            }
                        }
                        else
                        {
                            txtCode.Text = "";

                        }
                        lblLvlID.Visible = true;
                        dbFunctions.lblAdd(@"Select LEVELCD from GL_ACCHART where ACCOUNTNM='" + headnm + "'",
                            lblLvlID);
                        lblBotCode.Text = "";
                        lblBotCode.Text = (Convert.ToDecimal(lblLvlID.Text) + 1).ToString();
                        gvDetails.Visible = false;
                        btnSubmit.Focus();

                    }
                    else
                    {

                    }
                }
            }
        }
        protected void BindEmployeeDetails()
        {
            string connectionString = ConfigurationManager.ConnectionStrings["AslDbContext"].ConnectionString;
            SqlConnection conn = new SqlConnection(connectionString);
            int level = Convert.ToInt16(lblLvlID.Text) + 1;

            conn.Open();
            SqlCommand cmd = new SqlCommand(@"SELECT GL_ACCHART.ACCOUNTCD,gc.COSTPNM, GL_ACCHART.ACCOUNTNM, GL_ACCHART.OPENINGDT, 
            GL_ACCHART.LEVELCD, GL_ACCHART.CONTROLCD, GL_ACCHART.ACCOUNTTP,  GL_ACCHART.BRANCHCD, GL_ACCHART.STATUSCD, 
            GL_ACCHART.ACTIVE, GL_ACCHART.USERPC, GL_ACCHART.USERID, 
            GL_ACCHART.ACTDTI, GL_ACCHART.INTIME, GL_ACCHART.IPADDRESS, ASL_BRANCH.BRANCHNM  
            FROM GL_ACCHART LEFT OUTER JOIN ASL_BRANCH ON GL_ACCHART.BRANCHCD = ASL_BRANCH.BRANCHCD 
            left outer join GL_COSTP as gc on gc.COSTPID=GL_ACCHART.COSTPID 
            WHERE GL_ACCHART.CONTROLCD='" + txtCode.Text + "' AND GL_ACCHART.LEVELCD='" + level + "'", conn);
            SqlDataAdapter da = new SqlDataAdapter(cmd);
            DataSet ds = new DataSet();
            da.Fill(ds);
            conn.Close();
            if (ds.Tables[0].Rows.Count > 0)
            {
                gvDetails.DataSource = ds;
                gvDetails.DataBind();
                TextBox txtAccHead = (TextBox)gvDetails.FooterRow.FindControl("txtAccHead");
                txtAccHead.Focus();
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
                TextBox txtAccHead = (TextBox)gvDetails.FooterRow.FindControl("txtAccHead");
                txtAccHead.Focus();
            }
        }



        protected void gvDetails_RowDataBound(object sender, GridViewRowEventArgs e)
        {
            //if (e.Row.RowType == DataControlRowType.DataRow)
            //{
            ////getting username from particular row
            //string username = Convert.ToString(DataBinder.Eval(e.Row.DataItem, "UserName"));
            ////identifying the control in gridview
            //ImageButton lnkbtnresult = (ImageButton)e.Row.FindControl("imgbtnDelete");
            ////raising javascript confirmationbox whenver user clicks on link button
            //if (lnkbtnresult != null)
            //{
            //    lnkbtnresult.Attributes.Add("onclick", "javascript:return ConfirmationBox('" + username + "')");
            //}
            if (e.Row.RowType == DataControlRowType.DataRow)
            {
                if (lblBotCode.Text == "5")
                {
                    e.Row.Cells[3].Enabled = true;
                }
                else
                {
                    e.Row.Cells[3].Enabled = false;
                }
            }
            else if (e.Row.RowType == DataControlRowType.Footer)
            {
                int level = Convert.ToInt16(lblLvlID.Text) + 1;
                dbFunctions.lblAdd(@"select MAX(ACCOUNTCD) from GL_ACCHART where LEVELCD='" + level + "' and CONTROLCD ='" + txtCode.Text + "'", lblMxAccCode);

                string conTrlCd = txtCode.Text;
                string mxCode;
                if (lblMxAccCode.Text == "")
                {
                    mxCode = txtCode.Text;
                }
                else
                {
                    mxCode = lblMxAccCode.Text;
                }

                string lvl2, lvl3, lvl4, lvl5, L2, L3, L4, L5, mid, accCode;
                int lv2, lv3, lv4, lv5, nLvlCode;
                int l2, l3, l4, l5;
                if (lblLvlID.Text == "1")
                {

                    lvl2 = mxCode.Substring(1, 2);
                    lv2 = int.Parse(lvl2);
                    l2 = lv2 + 1;
                    if (l2 < 10)
                    {
                        mid = l2.ToString();
                        L2 = "0" + mid;
                    }
                    else
                        L2 = l2.ToString();
                    lvl3 = mxCode.Substring(3, 2);
                    lv3 = int.Parse(lvl3);
                    l3 = lv3;
                    lvl4 = mxCode.Substring(5, 2);
                    lv4 = int.Parse(lvl4);
                    l4 = lv4;
                    lvl5 = mxCode.Substring(7, 5);
                    lv5 = int.Parse(lvl5);
                    l5 = lv5;

                    accCode = ddlLevelID.Text + L2 + lvl3 + lvl4 + lvl5;
                    e.Row.Cells[1].Text = accCode;
                    e.Row.Cells[2].Text = conTrlCd;
                    nLvlCode = 2;
                    lblNewLvlCD.Text = nLvlCode.ToString();
                    lblStatus.Text = "N"; ///status = Level 1 to 4 N or P
                }
                else if (lblLvlID.Text == "2")
                {
                    lvl2 = mxCode.Substring(1, 2);
                    lv2 = int.Parse(lvl2);
                    l2 = lv2;
                    lvl3 = mxCode.Substring(3, 2);
                    lv3 = int.Parse(lvl3);
                    l3 = lv3 + 1;
                    if (l3 < 10)
                    {
                        mid = l3.ToString();
                        L3 = "0" + mid;
                    }
                    else
                        L3 = l3.ToString();
                    lvl4 = mxCode.Substring(5, 2);
                    lv4 = int.Parse(lvl4);
                    l4 = lv4;
                    lvl5 = mxCode.Substring(7, 5);
                    lv5 = int.Parse(lvl5);
                    l5 = lv5;

                    accCode = ddlLevelID.Text + lvl2 + L3 + lvl4 + lvl5;
                    e.Row.Cells[1].Text = accCode;
                    e.Row.Cells[2].Text = conTrlCd;
                    nLvlCode = 3;
                    lblNewLvlCD.Text = nLvlCode.ToString();
                    lblStatus.Text = "N"; ///status = Level if 1 to 4 N or else P
                }
                else if (lblLvlID.Text == "3")
                {
                    lvl2 = mxCode.Substring(1, 2);
                    lv2 = int.Parse(lvl2);
                    l2 = lv2;
                    lvl3 = mxCode.Substring(3, 2);
                    lvl4 = mxCode.Substring(5, 2);
                    lv4 = int.Parse(lvl4);
                    l4 = lv4 + 1;
                    if (l4 < 10)
                    {
                        mid = l4.ToString();
                        L4 = "0" + mid;
                    }
                    else
                        L4 = l4.ToString();
                    lvl5 = mxCode.Substring(7, 5);
                    lv5 = int.Parse(lvl5);
                    l5 = lv5;

                    accCode = ddlLevelID.Text + lvl2 + lvl3 + L4 + lvl5;
                    e.Row.Cells[1].Text = accCode;
                    e.Row.Cells[2].Text = conTrlCd;
                    nLvlCode = 4;
                    lblNewLvlCD.Text = nLvlCode.ToString();
                    lblStatus.Text = "N"; ///status = Level if 1 to 4 N or else P
                }
                else if (lblLvlID.Text == "4")
                {
                    lvl2 = mxCode.Substring(1, 2);
                    lv2 = int.Parse(lvl2);
                    l2 = lv2;
                    lvl3 = mxCode.Substring(3, 2);
                    lvl4 = mxCode.Substring(5, 2);

                    lvl5 = mxCode.Substring(7, 5);
                    lv5 = int.Parse(lvl5);
                    l5 = lv5 + 1;
                    if (l5 < 10)
                    {
                        mid = l5.ToString();
                        L5 = "0000" + mid;
                    }
                    else if (l5 < 100)
                    {
                        mid = l5.ToString();
                        L5 = "000" + mid;
                    }
                    else if (l5 < 1000)
                    {
                        mid = l5.ToString();
                        L5 = "00" + mid;
                    }
                    else if (l5 < 10000)
                    {
                        mid = l5.ToString();
                        L5 = "0" + mid;
                    }
                    //else if (l5 < 11110)
                    //{
                    //    mid = l5.ToString();
                    //    L5 = "0000" + mid;
                    //}
                    //else if (l5 < 11100)
                    //{
                    //    mid = l5.ToString();
                    //    L5 = "000" + mid;
                    //}
                    //else if (l5 < 11000)
                    //{
                    //    mid = l5.ToString();
                    //    L5 = "00" + mid;
                    //}
                    //else if (l5 < 10000)
                    //{
                    //    mid = l5.ToString();
                    //    L5 = "0" + mid;
                    //}
                    else
                        L5 = l5.ToString();
                    accCode = ddlLevelID.Text + lvl2 + lvl3 + lvl4 + L5;
                    e.Row.Cells[1].Text = accCode;
                    e.Row.Cells[2].Text = conTrlCd;
                    nLvlCode = 5;
                    lblNewLvlCD.Text = nLvlCode.ToString();
                    lblStatus.Text = "P"; ///status = Level if 1 to 4 N or else P
                }

                if (lblBotCode.Text == "5")
                {
                    e.Row.Cells[3].Enabled = true;
                }
                else
                {
                    e.Row.Cells[3].Enabled = false;
                }

                DropDownList ddlAccess = (DropDownList)e.Row.FindControl("ddlAccess");
                dbFunctions.DropDownAddSelectTextWithValue(ddlAccess, "SELECT COSTPNM, COSTPID FROM GL_COSTP");
            }
           // SELECT CATNM FROM GL_COSTPMST ORDER BY CATID
            //SELECT COSTPNM, COSTPID FROM GL_COSTP
        }

        protected void ddlAccess_SelectedIndexChanged(object sender, EventArgs e)
        {
            GridViewRow row = ((GridViewRow)((DropDownList)sender).NamingContainer);
            DropDownList ddlAccess = (DropDownList)row.FindControl("ddlAccess");
            string brNM = ddlAccess.Text;
            Label lblAccessCDFot = (Label)row.FindControl("lblAccessCDFot");
           // dbFunctions.lblAdd("SELECT CATID FROM GL_COSTP WHERE COSTPNM ='" + brNM + "'", lblAccessCDFot);
            dbFunctions.lblAdd("SELECT COSTPID FROM GL_COSTP WHERE COSTPNM ='" + brNM + "'", lblAccessCDFot);
            ImageButton imgbtnAdd = (ImageButton)row.FindControl("imgbtnAdd");
            imgbtnAdd.Focus();
        }

        protected void gvDetails_RowCommand(object sender, GridViewCommandEventArgs e)
        {
            string userName = HttpContext.Current.Session["USERID"].ToString();
            string connectionString = ConfigurationManager.ConnectionStrings["AslDbContext"].ConnectionString;
            SqlConnection conn = new SqlConnection(connectionString);

            string query = "";
            SqlCommand comm = new SqlCommand(query, conn);


            DateTime openDT = DateTime.Now;
            int levelCD = Convert.ToInt16(lblNewLvlCD.Text);
            string AccCode, ControlCode;

            if (e.CommandName.Equals("AddNew"))
            {
                if (Session["USERID"] == null)
                {
                    Response.Redirect("~/Login/UI/Login.aspx");
                }
                else
                {
                    TextBox txtAccHead = (TextBox)gvDetails.FooterRow.FindControl("txtAccHead");
                    txtAccHead.Focus();
                    AccCode = gvDetails.FooterRow.Cells[1].Text;
                    ControlCode = gvDetails.FooterRow.Cells[2].Text;
                    Label lblAccessCDFot = (Label)gvDetails.FooterRow.FindControl("lblAccessCDFot");
                    DropDownList ddlAccess = (DropDownList)gvDetails.FooterRow.FindControl("ddlAccess");

                    conn.Open();
                    SqlCommand cmd = new SqlCommand("Select CONTROLCD from GL_ACCHARTMST where CONTROLCD='" + txtCode.Text + "'", conn);
                    SqlDataAdapter da = new SqlDataAdapter(cmd);
                    DataSet ds = new DataSet();
                    da.Fill(ds);
                    conn.Close();
                    if (ds.Tables[0].Rows.Count > 0)
                    {

                        query = ("insert into GL_ACCHART (ACCOUNTCD, ACCOUNTNM, OPENINGDT, LEVELCD, CONTROLCD, ACCOUNTTP,  STATUSCD, ACTIVE, USERPC, USERID, ACTDTI, IPADDRESS) " +
                                "values(@AccCode,'" + txtAccHead.Text + "',@OPENINGDT,@LEVELCD,@ControlCode,'" + lblAccTP.Text + "','" + lblStatus.Text + "','A','',@USERID,'','')");

                        comm = new SqlCommand(query, conn);
                        comm.Parameters.AddWithValue("@AccCode", AccCode);
                        comm.Parameters.AddWithValue("@ControlCode", ControlCode);
                        comm.Parameters.AddWithValue("@OPENINGDT", openDT);
                        comm.Parameters.AddWithValue("@LEVELCD", levelCD);
                        comm.Parameters.AddWithValue("@USERID", userName);

                        conn.Open();
                        int result = comm.ExecuteNonQuery();
                        conn.Close();
                        BindEmployeeDetails();
                        //if (result == 1)
                        //{
                        //    Response.Write("<script>alert('Successfully Saved');</script>");
                        //    
                        //}
                        //else
                        //{
                        //    Response.Write("<script>alert('Data not Saved');</script>");
                        //}
                    }
                    else
                    {

                        query = "insert into GL_ACCHARTMST  (CONTROLCD, USERID, USERPC, ACTDTI, IPADDRESS) " +
                                "values(@CONTROLCD,@USERID,'','','')";
                        comm = new SqlCommand(query, conn);
                        comm.Parameters.AddWithValue("@CONTROLCD", ControlCode);
                        comm.Parameters.AddWithValue("@USERID", userName);
                        conn.Open();
                        comm.ExecuteNonQuery();
                        conn.Close();

                        query = ("insert into GL_ACCHART (ACCOUNTCD, ACCOUNTNM, OPENINGDT, LEVELCD, CONTROLCD, ACCOUNTTP, STATUSCD, ACTIVE, USERPC, USERID, ACTDTI, IPADDRESS) " +
                               "values(@AccCode,'" + txtAccHead.Text + "',@OPENINGDT,@LEVELCD,@ControlCode,'" + lblAccTP.Text + "','" + lblStatus.Text + "','A','',@USERID,'','')");

                        comm = new SqlCommand(query, conn);
                        comm.Parameters.AddWithValue("@AccCode", AccCode);
                        comm.Parameters.AddWithValue("@ControlCode", ControlCode);
                        comm.Parameters.AddWithValue("@OPENINGDT", openDT);
                        comm.Parameters.AddWithValue("@LEVELCD", levelCD);
                        comm.Parameters.AddWithValue("@USERID", userName);

                        conn.Open();
                        int result = comm.ExecuteNonQuery();
                        conn.Close();
                        BindEmployeeDetails();
                        //Response.Write("<script>alert('Successfully Saved');</script>");
                        //if (result == 1)
                        //{
                        //    Response.Write("<script>alert('Successfully Saved');</script>");

                        //}
                        //else
                        //{
                        //    Response.Write("<script>alert('Data not Saved');</script>");
                        //}
                    }
                }
            }
        }

        protected void btnSubmit_Click(object sender, EventArgs e)
        {
            txtHdName_TextChanged(sender, e);
            if (Session["USERID"] == null)
            {
                Response.Redirect("~/Login/UI/Login.aspx");
            }
            else
            {
                if (ddlLevelID.Text == "Select")
                {
                    Response.Write("<script>alert('Select Transaction Type?');</script>");
                }
                else if (txtHdName.Text == "")
                {
                    Response.Write("<script>alert('Type Account Head?');</script>");
                }
                else
                {
                    BindEmployeeDetails();
                    gvDetails.Visible = true;
                }
            }
        }

        protected void gvDetails_RowEditing(object sender, GridViewEditEventArgs e)
        {
            gvDetails.EditIndex = e.NewEditIndex;
            BindEmployeeDetails();

            DropDownList ddlAccessEdit = (DropDownList)gvDetails.Rows[e.NewEditIndex].FindControl("ddlAccessEdit");
            dbFunctions.DropDownAddSelectTextWithValue(ddlAccessEdit, "SELECT COSTPNM, COSTPID FROM GL_COSTP");
            TextBox txtAccCode = (TextBox)gvDetails.Rows[e.NewEditIndex].FindControl("txtAccCode");
            lblBRCD.Text = "";
            dbFunctions.lblAdd("SELECT GL_COSTP.COSTPNM FROM GL_ACCHART INNER JOIN GL_COSTP ON GL_ACCHART.COSTPID = GL_COSTP.CATID WHERE GL_ACCHART.ACCOUNTCD='" + txtAccCode.Text + "'", lblBRCD);
            ddlAccessEdit.Text = lblBRCD.Text;

            TextBox txtAccHead = (TextBox)gvDetails.Rows[e.NewEditIndex].FindControl("txtAccHead");
            txtAccHead.Focus();
        }

        protected void ddlAccessEdit_SelectedIndexChanged(object sender, EventArgs e)
        {
            GridViewRow row = ((GridViewRow)((DropDownList)sender).NamingContainer);
            DropDownList ddlAccessEdit = (DropDownList)row.FindControl("ddlAccessEdit");
            string brNM = ddlAccessEdit.Text;
            Label lblAccessCDEdit = (Label)row.FindControl("lblAccessCDEdit");
            dbFunctions.lblAdd(" SELECT COSTPID FROM GL_COSTP WHERE COSTPNM ='" + brNM + "'", lblAccessCDEdit);
           
            ImageButton imgbtnUpdate = (ImageButton)row.FindControl("imgbtnUpdate");
            imgbtnUpdate.Focus();
        }

        protected void gvDetails_RowUpdating(object sender, GridViewUpdateEventArgs e)
        {
            if (Session["USERID"] == null)
            {
                Response.Redirect("~/Login/UI/Login.aspx");
            }
            else
            {
                string connectionString = ConfigurationManager.ConnectionStrings["AslDbContext"].ConnectionString;
                SqlConnection conn = new SqlConnection(connectionString);
                string userName = HttpContext.Current.Session["USERID"].ToString();
                string ipAddress = HttpContext.Current.Session["IpAddress"].ToString();

                TextBox txtAccHead = (TextBox)gvDetails.Rows[e.RowIndex].FindControl("txtAccHead");
                TextBox AccCode = (TextBox)gvDetails.Rows[e.RowIndex].FindControl("txtAccCode");
                TextBox ControlCode = (TextBox)gvDetails.Rows[e.RowIndex].FindControl("txtContolCode");
                Label lblAccessCDEdit = (Label)gvDetails.Rows[e.RowIndex].FindControl("lblAccessCDEdit");
                DropDownList ddlAccessEdit = (DropDownList)gvDetails.Rows[e.RowIndex].FindControl("ddlAccessEdit");



                try
                {
                    // logdata add start //
                    string lotileng = HttpContext.Current.Session["Location"].ToString();
                    ipAddress = HttpContext.Current.Session["IpAddress"].ToString();
                    string logdata = dbFunctions.StringData(@"SELECT 'ACCOUNTCD : ' + ACCOUNTCD+' | '+'ACCOUNTNM : ' + ACCOUNTNM+' | '+'OPENINGDT :
' + CONVERT(NVARCHAR(50),OPENINGDT,103)+' | '+'LEVELCD : ' + CONVERT(NVARCHAR(50),LEVELCD,103)+' | '+'CONTROLCD : ' + CONTROLCD+' | '+'ACCOUNTTP :
' + ISNULL(CONVERT(NVARCHAR(50),ACCOUNTTP,103),'(NULL)')+' | '+'BRANCHCD : ' + ISNULL(CONVERT(NVARCHAR(50),BRANCHCD,103),'(NULL)')+' | '+'STATUSCD :
' + ISNULL(CONVERT(NVARCHAR(50),STATUSCD,103),'(NULL)')+' | '+'ACTIVE : ' + ISNULL(CONVERT(NVARCHAR(50),ACTIVE,103),'(NULL)')+' | '+'USERPC :
' + ISNULL(USERPC,'(NULL)')+' | '+'UPDUSERPC : ' + ISNULL(UPDUSERPC,'(NULL)')+' | '+'UPDUSERID : ' + ISNULL(UPDUSERID,'(NULL)')+' | '+'USERID : 
' + ISNULL(USERID,'(NULL)')+' | '+'ACTDTI : ' + ISNULL(CONVERT(NVARCHAR(50),ACTDTI,103),'(NULL)')+' | '+'INTIME : 
' + CONVERT(NVARCHAR(50),INTIME,103)+' | '+'UPDTIME : ' + ISNULL(CONVERT(NVARCHAR(50),UPDTIME,103),'(NULL)')+' | '+'IPADDRESS : 
' + ISNULL(IPADDRESS,'(NULL)')+' | '+'UPDIPADD : ' + ISNULL(UPDIPADD,'(NULL)')+' | '+'COSTPID : ' + ISNULL(COSTPID,'(NULL)')+' | ' FROM GL_ACCHART
   where ACCOUNTCD = '" + AccCode.Text + "' and CONTROLCD = '" + ControlCode.Text + "'");
                    string logid = "UPDATE";
                    string tableid = "GL_ACCHART";
                    LogDataFunction.InsertLogData(lotileng, logid, tableid, logdata, ipAddress);
                    // logdata add end //
                }
                catch (Exception ex)
                {

                }

                conn.Open();
                SqlCommand cmd = new SqlCommand("update GL_ACCHART set ACCOUNTNM='" + txtAccHead.Text + "', USERID = '" + userName + "' where ACCOUNTCD = '" + AccCode.Text + "' and CONTROLCD = '" + ControlCode.Text + "'", conn);
                cmd.ExecuteNonQuery();
                conn.Close();
                //Response.Write("<script>alert('Successfully Updated');</script>");
                //lblresult.ForeColor = Color.Green;
                //lblresult.Text = "Details Updated successfully";
                gvDetails.EditIndex = -1;
                BindEmployeeDetails();
            }
        }

        protected void gvDetails_RowCancelingEdit(object sender, GridViewCancelEditEventArgs e)
        {
            gvDetails.EditIndex = -1;
            BindEmployeeDetails();
        }

        protected void gvDetails_RowDeleting(object sender, GridViewDeleteEventArgs e)
        {
            if (Session["USERID"] == null)
            {
                Response.Redirect("~/Login/UI/Login.aspx");
            }
            else
            {
                string connectionString = ConfigurationManager.ConnectionStrings["AslDbContext"].ConnectionString;
                SqlConnection conn = new SqlConnection(connectionString);
                string ipAddress = HttpContext.Current.Session["IpAddress"].ToString();
                //int userid = Convert.ToInt32(gvDetails.DataKeys[e.RowIndex].Values["UserId"].ToString());
                //string username = gvDetails.DataKeys[e.RowIndex].Values["UserName"].ToString();

                Label AccCode = (Label)gvDetails.Rows[e.RowIndex].FindControl("lblAcountCode");
                Label ControlCode = (Label)gvDetails.Rows[e.RowIndex].FindControl("lblControlCode");

                string accCd = AccCode.Text;
                string accSubCD2nd = accCd.Substring(0, 3);
                string accSubCD3rd = accCd.Substring(0, 5);
                string accSubCD4th = accCd.Substring(0, 7);

                //dbFunctions.lblAdd(@"select CONTROLCD from GL_ACCHARTMST where CONTROLCD='" + AccCode.Text + "'", lblDelCtrlCD);
                dbFunctions.lblAdd(@"select LEVELCD from  GL_ACCHART where ACCOUNTCD='" + AccCode.Text + "'", lblSelLvlCD);
                dbFunctions.lblAdd(@"select (LEVELCD+1)as LEVELCD from GL_ACCHART where LEVELCD='" + lblSelLvlCD.Text + "' and LEVELCD<>5", lblIncrLevel);

                conn.Open();
                SqlCommand cmd1 = new SqlCommand();

                if (lblLvlID.Text == "1")
                {
                    cmd1 = new SqlCommand("select DEBITCD from GL_MASTER where DEBITCD like '" + accSubCD2nd + "%'", conn);
                }

                else if (lblLvlID.Text == "2")
                {
                    cmd1 = new SqlCommand("select DEBITCD from GL_MASTER where DEBITCD like '" + accSubCD3rd + "%'", conn);
                }
                else if (lblLvlID.Text == "3")
                {
                    cmd1 = new SqlCommand("select DEBITCD from GL_MASTER where DEBITCD like '" + accSubCD4th + "%'", conn);
                }
                else if (lblLvlID.Text == "4")
                {
                    cmd1 = new SqlCommand("select DEBITCD from GL_MASTER where DEBITCD = '" + AccCode.Text + "'", conn);
                }

                SqlDataAdapter chk = new SqlDataAdapter(cmd1);
                DataSet ch = new DataSet();
                chk.Fill(ch);
                conn.Close();

                if (ch.Tables[0].Rows.Count > 0)
                {
                    Response.Write("<script>alert('This Account Head have Child Data.');</script>");
                }
                else
                {
                    //if (lblIncrLevel.Text == "")
                    //{
                    conn.Open();

                    SqlCommand cmd = new SqlCommand("SELECT * FROM GL_ACCHARTMST INNER JOIN GL_ACCHART ON GL_ACCHARTMST.CONTROLCD = GL_ACCHART.CONTROLCD WHERE (GL_ACCHARTMST.CONTROLCD = '" + accCd + "')", conn);
                    SqlDataAdapter chcekCntrCD = new SqlDataAdapter(cmd);
                    DataSet check = new DataSet();
                    chcekCntrCD.Fill(check);

                    if (check.Tables[0].Rows.Count > 0)
                    {
                        Response.Write("<script>alert('This Account Head have Child Data.');</script>");
                    }
                    else
                    {
                        try
                        {
                            string lotileng = HttpContext.Current.Session["Location"].ToString();
                        ipAddress = HttpContext.Current.Session["IpAddress"].ToString();
                        string logdata = dbFunctions.StringData(@"SELECT 'ACCOUNTCD : ' + ACCOUNTCD+' | '+'ACCOUNTNM : ' + ACCOUNTNM+' | '+'OPENINGDT :
' + CONVERT(NVARCHAR(50),OPENINGDT,103)+' | '+'LEVELCD : ' + CONVERT(NVARCHAR(50),LEVELCD,103)+' | '+'CONTROLCD : ' + CONTROLCD+' | '+'ACCOUNTTP :
' + ISNULL(CONVERT(NVARCHAR(50),ACCOUNTTP,103),'(NULL)')+' | '+'BRANCHCD : ' + ISNULL(CONVERT(NVARCHAR(50),BRANCHCD,103),'(NULL)')+' | '+'STATUSCD :
' + ISNULL(CONVERT(NVARCHAR(50),STATUSCD,103),'(NULL)')+' | '+'ACTIVE : ' + ISNULL(CONVERT(NVARCHAR(50),ACTIVE,103),'(NULL)')+' | '+'USERPC :
' + ISNULL(USERPC,'(NULL)')+' | '+'UPDUSERPC : ' + ISNULL(UPDUSERPC,'(NULL)')+' | '+'UPDUSERID : ' + ISNULL(UPDUSERID,'(NULL)')+' | '+'USERID : 
' + ISNULL(USERID,'(NULL)')+' | '+'ACTDTI : ' + ISNULL(CONVERT(NVARCHAR(50),ACTDTI,103),'(NULL)')+' | '+'INTIME : 
' + CONVERT(NVARCHAR(50),INTIME,103)+' | '+'UPDTIME : ' + ISNULL(CONVERT(NVARCHAR(50),UPDTIME,103),'(NULL)')+' | '+'IPADDRESS : 
' + ISNULL(IPADDRESS,'(NULL)')+' | '+'UPDIPADD : ' + ISNULL(UPDIPADD,'(NULL)')+' | '+'COSTPID : ' + ISNULL(COSTPID,'(NULL)')+' | ' FROM GL_ACCHART
   where ACCOUNTCD = '" + AccCode.Text + "' and CONTROLCD = '" + ControlCode.Text + "'");
                        string logid = "DELETE";
                        string tableid = "GL_ACCHART";
                        LogDataFunction.InsertLogData(lotileng, logid, tableid, logdata, ipAddress);
                        // logdata add end //
                    }
                catch (Exception ex)
                    {

                    }


                    SqlCommand cmd2 = new SqlCommand("delete FROM GL_ACCHART where ACCOUNTCD = '" + AccCode.Text + "' and LEVELCD = '" + lblSelLvlCD.Text + "'", conn);
                        int result = cmd2.ExecuteNonQuery();





                        try
                        {
                            string lotileng = HttpContext.Current.Session["Location"].ToString();
                            ipAddress = HttpContext.Current.Session["IpAddress"].ToString();
                            string logdata = dbFunctions.StringData(@"SELECT 'CONTROLCD : ' + CONTROLCD+' | '+'USERID : ' + ISNULL(USERID,'(NULL)')+' | '+'USERPC :
' + ISNULL(USERPC,'(NULL)')+' | '+'ACTDTI : ' + ISNULL(CONVERT(NVARCHAR(50),ACTDTI,103),'(NULL)')+' | '+'INTIME :
' + CONVERT(NVARCHAR(50),INTIME,103)+' | '+'IPADDRESS : ' + ISNULL(IPADDRESS,'(NULL)')+' | ' FROM GL_ACCHARTMST
  where CONTROLCD = '" + AccCode.Text + "' ");
                            string logid = "UPDATE";
                            string tableid = "GL_ACCHARTMST";
                            LogDataFunction.InsertLogData(lotileng, logid, tableid, logdata, ipAddress);
                            // logdata add end //
                        }
                        catch (Exception )
                        {

                        }



                        SqlCommand cmd3 = new SqlCommand("delete FROM GL_ACCHARTMST where CONTROLCD = '" + AccCode.Text + "' ", conn);
                        int result1 = cmd3.ExecuteNonQuery();
                        conn.Close();

                        if (result == 1)
                        {
                            //Response.Write("<script>alert('Successfully Deleted.');</script>");
                            BindEmployeeDetails();
                        }
                    }
                    //}
                    //else
                    //{
                    //    Response.Write("<script>alert('This Account Head have Child Data.');</script>");
                    //}
                }
            }


        }
    }
}