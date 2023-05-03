using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data;
using System.Configuration;
using DynamicMenu;
using System.Data.SqlClient;
using System.Web.Services;
using System.Text;
using System.Collections.Specialized;
using AlchemyAccounting;
using DynamicMenu.Accounts.DataAccess;
using DynamicMenu.Accounts.Interface;

namespace DynamicMenu.Accounts.UI
{
    public partial class Process : System.Web.UI.Page
    {
        AccountDataAccess dob = new AccountDataAccess();
        AccountInterface iob = new AccountInterface();
        IFormatProvider dateformat = new System.Globalization.CultureInfo("fr-FR", true);

        protected void Page_Load(object sender, EventArgs e)
        {
            if (Session["UserName"] == null)
            {
                Response.Redirect("~/Login/UI/Login.aspx");
            }
            else
            {
                string formLink = "/Accounts/UI/Process.aspx";
                bool permission = UserPermissionChecker.checkParmit(formLink, "STATUS");
                if (permission == true)
                {
                    if (!IsPostBack)
                    {
                        lblSerial_Mrec.Visible = false;
                        lblSerial_Mpay.Visible = false;
                        lblSerial_Jour.Visible = false;
                        lblSerial_Cont.Visible = false;
                        DateTime today = DateTime.Today.Date;
                        string td = dbFunctions.Dayformat(today);
                        txtDate.Text = td;
                        btnProcess.Focus();
                    }
                }
                else
                {
                    Response.Redirect("../../DashBoard/UI/Default.aspx");
                }
            }
        }

        public void ShowGrid()
        {

            DateTime Pdate = DateTime.Parse(txtDate.Text, dateformat, System.Globalization.DateTimeStyles.AssumeLocal);
            string p_Date = Pdate.ToString("yyyy/MM/dd");

            string connectionString = ConfigurationManager.ConnectionStrings["AslDbContext"].ConnectionString;
            SqlConnection conn = new SqlConnection(connectionString);

            conn.Open();
            SqlCommand cmd = new SqlCommand(" SELECT TRANSTP, TRANSDT, TRANSMY, TRANSNO, SERIALNO, TRANSFOR, COSTPID, TRANSMODE, DEBITCD, CREDITCD, CHEQUENO, convert(nvarchar(20),CHEQUEDT,103) as CHEQUEDT, AMOUNT, REMARKS, USERPC, USERID, ACTDTI, INTIME, IPADDRESS " +
                                            " FROM dbo.GL_STRANS where TRANSDT = '" + p_Date + "'", conn);
            SqlDataAdapter da = new SqlDataAdapter(cmd);
            DataSet ds = new DataSet();
            da.Fill(ds);
            conn.Close();
            if (ds.Tables[0].Rows.Count > 0)
            {
                GridView1.DataSource = ds;
                GridView1.DataBind();
                GridView1.Visible = false;
            }
            else
            {
                GridView1.DataSource = ds;
                GridView1.DataBind();
                GridView1.Visible = false;
                //Response.Write("<script>alert('No Data Found');</script>");
                //GridView1.Visible = false;
            }
        }
        public void ShowGrid_Multiple()
        {

            DateTime Pdate = DateTime.Parse(txtDate.Text, dateformat, System.Globalization.DateTimeStyles.AssumeLocal);
            string p_Date = Pdate.ToString("yyyy/MM/dd");

            string connectionString = ConfigurationManager.ConnectionStrings["AslDbContext"].ConnectionString;
            SqlConnection conn = new SqlConnection(connectionString);

            conn.Open();
            SqlCommand cmd = new SqlCommand(" SELECT TRANSTP, TRANSDT, TRANSMY, TRANSNO, SERIALNO, TRANSFOR, COSTPID, TRANSMODE, DEBITCD, CREDITCD, CHEQUENO, convert(nvarchar(20),CHEQUEDT,103) as CHEQUEDT, AMOUNT, REMARKS, USERPC, USERID, INTIME, IPADDRESS " +
                                            " FROM GL_MTRANS where TRANSDT = '" + p_Date + "'", conn);
            SqlDataAdapter da = new SqlDataAdapter(cmd);
            DataSet ds = new DataSet();
            da.Fill(ds);
            conn.Close();
            if (ds.Tables[0].Rows.Count > 0)
            {
                gridMultiple.DataSource = ds;
                gridMultiple.DataBind();
                gridMultiple.Visible = false;
            }
            else
            {
                gridMultiple.DataSource = ds;
                gridMultiple.DataBind();
                gridMultiple.Visible = false;
                //Response.Write("<script>alert('No Data Found');</script>");
                //GridView1.Visible = false;
            }
        }

//        public void ShowGrid_Purchase()
//        {

//            DateTime Pdate = DateTime.Parse(txtDate.Text, dateformat, System.Globalization.DateTimeStyles.AssumeLocal);
//            string p_Date = Pdate.ToString("yyyy/MM/dd");

//            string connectionString = ConfigurationManager.ConnectionStrings["AslDbContext"].ConnectionString;
//            SqlConnection conn = new SqlConnection(connectionString);

//            conn.Open();
//            SqlCommand cmd = new SqlCommand(@"SELECT        STK_TRANS.TRANSTP, STK_TRANS.TRANSDT, STK_TRANS.TRANSMY, STK_TRANS.TRANSNO, STK_TRANS.STOREFR, STK_TRANS.STORETO, STK_TRANS.PSID, STK_TRANS.LCTP, STK_TRANS.LCID, 
//                         STK_TRANS.LCDATE, STK_TRANSMST.REMARKS AS REMARKS, SUM(STK_TRANS.AMOUNT) AS AMOUNT
//FROM            STK_TRANS INNER JOIN
//                         STK_TRANSMST ON STK_TRANS.TRANSTP = STK_TRANSMST.TRANSTP AND STK_TRANS.TRANSMY = STK_TRANSMST.TRANSMY AND STK_TRANS.TRANSNO = STK_TRANSMST.TRANSNO
//WHERE        (STK_TRANS.TRANSDT = @date) AND (STK_TRANS.LCTP = 'LOCAL') AND (STK_TRANS.TRANSTP = 'BUY') AND (STK_TRANS.TRANSDT <> '2015-06-04 00:00:00')
//GROUP BY STK_TRANS.TRANSTP, STK_TRANS.TRANSDT, STK_TRANS.TRANSMY, STK_TRANS.TRANSNO, STK_TRANS.STOREFR, STK_TRANS.STORETO, STK_TRANS.PSID, STK_TRANS.LCTP, STK_TRANS.LCID, 
//                         STK_TRANS.LCDATE, STK_TRANSMST.REMARKS", conn);
//            cmd.Parameters.Clear();
//            cmd.Parameters.AddWithValue("@date", p_Date);
//            SqlDataAdapter da = new SqlDataAdapter(cmd);
//            DataSet ds = new DataSet();
//            da.Fill(ds);
//            conn.Close();
//            if (ds.Tables[0].Rows.Count > 0)
//            {
//                GridView2.DataSource = ds;
//                GridView2.DataBind();
//                GridView2.Visible = false;
//            }
//            else
//            {
//                GridView2.DataSource = ds;
//                GridView2.DataBind();
//                GridView2.Visible = false;
//            }
//        }

//        public void ShowGrid_Purchase_Import()
//        {
//            DateTime Pdate = DateTime.Parse(txtDate.Text, dateformat, System.Globalization.DateTimeStyles.AssumeLocal);
//            string p_Date = Pdate.ToString("yyyy/MM/dd");

//            string connectionString = ConfigurationManager.ConnectionStrings["AslDbContext"].ConnectionString;
//            SqlConnection conn = new SqlConnection(connectionString);

//            conn.Open();
//            SqlCommand cmd = new SqlCommand(@"SELECT        STK_TRANS.TRANSTP, STK_TRANS.TRANSDT, STK_TRANS.TRANSMY, STK_TRANS.TRANSNO, STK_TRANS.STOREFR, STK_TRANS.STORETO, STK_TRANS.PSID, STK_TRANS.LCTP, STK_TRANS.LCID, 
//STK_TRANS.LCDATE, STK_TRANSMST.REMARKS, SUM(STK_TRANS.AMOUNT) AS AMOUNT
//FROM            STK_TRANS INNER JOIN
//STK_TRANSMST ON STK_TRANS.TRANSTP = STK_TRANSMST.TRANSTP AND STK_TRANS.TRANSMY = STK_TRANSMST.TRANSMY AND STK_TRANS.TRANSNO = STK_TRANSMST.TRANSNO
//WHERE        (STK_TRANS.TRANSDT = @date) AND (STK_TRANS.LCTP = 'IMPORT') AND (STK_TRANS.TRANSTP = 'BUY') AND (STK_TRANS.TRANSDT <> '2015-06-04 00:00:00')
//GROUP BY STK_TRANS.TRANSTP, STK_TRANS.TRANSDT, STK_TRANS.TRANSMY, STK_TRANS.TRANSNO, STK_TRANS.STOREFR, STK_TRANS.STORETO, STK_TRANS.PSID, STK_TRANS.LCTP, STK_TRANS.LCID, 
//STK_TRANS.LCDATE, STK_TRANS.REMARKS, STK_TRANSMST.REMARKS", conn);
//            cmd.Parameters.Clear();
//            cmd.Parameters.AddWithValue("@date", p_Date);
//            SqlDataAdapter da = new SqlDataAdapter(cmd);
//            DataSet ds = new DataSet();
//            da.Fill(ds);
//            conn.Close();
//            if (ds.Tables[0].Rows.Count > 0)
//            {
//                gvBuyImport.DataSource = ds;
//                gvBuyImport.DataBind();
//                gvBuyImport.Visible = false;
//            }
//            else
//            {
//                gvBuyImport.DataSource = ds;
//                gvBuyImport.DataBind();
//                gvBuyImport.Visible = false;
//            }
//        }

//        public void ShowGrid_Purchase_Ret()
//        {

//            DateTime Pdate = DateTime.Parse(txtDate.Text, dateformat, System.Globalization.DateTimeStyles.AssumeLocal);
//            string p_Date = Pdate.ToString("yyyy/MM/dd");

//            string connectionString = ConfigurationManager.ConnectionStrings["AslDbContext"].ConnectionString;
//            SqlConnection conn = new SqlConnection(connectionString);

//            conn.Open();
//            SqlCommand cmd = new SqlCommand(" SELECT TRANSTP, TRANSDT, TRANSMY, TRANSNO, STOREFR, STORETO, PSID, LCTP, LCID, LCDATE, REMARKS, TOTNET AMOUNT " +
//                                            " from STK_TRANSMST where TRANSDT='" + p_Date + "' and TRANSTP='IRTB' and TRANSDT <> '2013-04-30 00:00:00' ", conn);
//            SqlDataAdapter da = new SqlDataAdapter(cmd);
//            DataSet ds = new DataSet();
//            da.Fill(ds);
//            conn.Close();
//            if (ds.Tables[0].Rows.Count > 0)
//            {
//                gridPurchase_Ret.DataSource = ds;
//                gridPurchase_Ret.DataBind();
//                gridPurchase_Ret.Visible = false;
//            }
//            else
//            {
//                gridPurchase_Ret.DataSource = ds;
//                gridPurchase_Ret.DataBind();
//                gridPurchase_Ret.Visible = false;
//            }
//        }

        public void ShowGrid_Sale()
        {

            DateTime Pdate = DateTime.Parse(txtDate.Text, dateformat, System.Globalization.DateTimeStyles.AssumeLocal);
            string p_Date = Pdate.ToString("yyyy/MM/dd");

            string connectionString = ConfigurationManager.ConnectionStrings["AslDbContext"].ConnectionString;
            SqlConnection conn = new SqlConnection(connectionString);

            conn.Open();
            //SqlCommand cmd = new SqlCommand(" SELECT TRANSTP, TRANSDT, TRANSMY, TRANSNO, STOREFR, STORETO, PSID, LCTP, LCID, LCDATE, REMARKS_P, sum(AMOUNT)as AMOUNT from STK_TRANS where TRANSDT='" + p_Date + "' and TRANSTP='SALE'" +
            //                                " group by TRANSTP, TRANSDT, TRANSMY, TRANSNO, STOREFR, STORETO, PSID, LCTP, LCID, LCDATE, REMARKS_P", conn);
            SqlCommand cmd = new SqlCommand($@" 
 SELECT TRANSTP, TRANSDT, TRANSYY, TRANSNO, PSID, SUM(NETAMT) as AMOUNT from STK_TRANS where TRANSDT = '{p_Date}' and TRANSTP = 'SALE' AND SUBSTRING(ITEMID,1,5) NOT IN ('10110')
 GROUP BY TRANSTP, TRANSDT, TRANSYY, TRANSNO, PSID", conn);

            SqlDataAdapter da = new SqlDataAdapter(cmd);
            DataSet ds = new DataSet();
            da.Fill(ds);
            conn.Close();
            if (ds.Tables[0].Rows.Count > 0)
            {
                GridView3.DataSource = ds;
                GridView3.DataBind();
                GridView3.Visible = false;
            }
            else
            {
                GridView3.DataSource = ds;
                GridView3.DataBind();
                GridView3.Visible = false;
            }
        }

        //public void ShowGrid_Sale_Discount()
        //{

        //    DateTime Pdate = DateTime.Parse(txtDate.Text, dateformat, System.Globalization.DateTimeStyles.AssumeLocal);
        //    string p_Date = Pdate.ToString("yyyy/MM/dd");

        //    string connectionString = ConfigurationManager.ConnectionStrings["AslDbContext"].ConnectionString;
        //    SqlConnection conn = new SqlConnection(connectionString);

        //    conn.Open();
        //    SqlCommand cmd = new SqlCommand(" SELECT * FROM " +
        //                                        " (SELECT A.TRANSTP, A.TRANSDT, A.TRANSMY, A.TRANSNO, A.STOREFR, A.PSID, A.REMARKS, (A.DISCAMT+STK_TRANSMST.DISAMT) AS DISAMT " +
        //                                        " FROM  (SELECT TRANSTP, TRANSDT, TRANSMY, TRANSNO, INVREFNO, STOREFR, STORETO, PSID, SUM(DISCAMT) AS DISCAMT, REMARKS " +
        //                                                " FROM          STK_TRANS " +
        //                                                " WHERE (TRANSTP = 'SALE') AND (TRANSDT = '" + p_Date + "') AND (TRANSDT <='2014-06-30')" +
        //                                                " GROUP BY TRANSTP, TRANSDT, TRANSMY, TRANSNO, INVREFNO, STOREFR, STORETO, PSID,REMARKS) AS A INNER JOIN " +
        //                                    " STK_TRANSMST ON A.TRANSTP = STK_TRANSMST.TRANSTP AND A.TRANSDT = STK_TRANSMST.TRANSDT AND A.TRANSMY = STK_TRANSMST.TRANSMY AND " +
        //                                    " A.TRANSNO = STK_TRANSMST.TRANSNO AND A.INVREFNO = STK_TRANSMST.INVREFNO AND A.STOREFR = STK_TRANSMST.STOREFR AND " +
        //                                    " A.STORETO = STK_TRANSMST.STORETO AND A.PSID = STK_TRANSMST.PSID) AS B WHERE B.DISAMT<>0", conn);
        //    SqlDataAdapter da = new SqlDataAdapter(cmd);
        //    DataSet ds = new DataSet();
        //    da.Fill(ds);
        //    conn.Close();
        //    if (ds.Tables[0].Rows.Count > 0)
        //    {
        //        GridView4.DataSource = ds;
        //        GridView4.DataBind();
        //        GridView4.Visible = false;
        //    }
        //    else
        //    {
        //        GridView4.DataSource = ds;
        //        GridView4.DataBind();
        //        GridView4.Visible = false;
        //    }
        //}

        //public void ShowGrid_Sale_New()
        //{

        //    DateTime Pdate = DateTime.Parse(txtDate.Text, dateformat, System.Globalization.DateTimeStyles.AssumeLocal);
        //    string p_Date = Pdate.ToString("yyyy/MM/dd");

        //    string connectionString = ConfigurationManager.ConnectionStrings["AslDbContext"].ConnectionString;
        //    SqlConnection conn = new SqlConnection(connectionString);

        //    conn.Open();
        //    SqlCommand cmd = new SqlCommand(" SELECT TRANSTP, TRANSDT, TRANSMY, TRANSNO, STOREFR, STORETO, PSID, LCTP, LCID, LCDATE, REMARKS, (TOTAMT-DISAMT) AS AMOUNT " +
        //               " from STK_TRANSMST where TRANSDT='" + p_Date + "' and TRANSTP='SALE' AND (TRANSDT >'2014-06-30')", conn);
        //    SqlDataAdapter da = new SqlDataAdapter(cmd);
        //    DataSet ds = new DataSet();
        //    da.Fill(ds);
        //    conn.Close();
        //    if (ds.Tables[0].Rows.Count > 0)
        //    {
        //        gvSaleNew.DataSource = ds;
        //        gvSaleNew.DataBind();
        //        gvSaleNew.Visible = false;
        //    }
        //    else
        //    {
        //        gvSaleNew.DataSource = ds;
        //        gvSaleNew.DataBind();
        //        gvSaleNew.Visible = false;
        //    }
        //}

        //public void ShowGrid_Sale_New_LtCost()
        //{

        //    DateTime Pdate = DateTime.Parse(txtDate.Text, dateformat, System.Globalization.DateTimeStyles.AssumeLocal);
        //    string p_Date = Pdate.ToString("yyyy/MM/dd");

        //    string connectionString = ConfigurationManager.ConnectionStrings["AslDbContext"].ConnectionString;
        //    SqlConnection conn = new SqlConnection(connectionString);

        //    conn.Open();
        //    SqlCommand cmd = new SqlCommand(" SELECT TRANSTP, TRANSDT, TRANSMY, TRANSNO, STOREFR, STORETO, PSID, LCTP, LCID, LCDATE, REMARKS, LTCOST AS AMOUNT " +
        //               " from STK_TRANSMST where TRANSDT='" + p_Date + "' and TRANSTP='SALE'", conn);
        //    SqlDataAdapter da = new SqlDataAdapter(cmd);
        //    DataSet ds = new DataSet();
        //    da.Fill(ds);
        //    conn.Close();
        //    if (ds.Tables[0].Rows.Count > 0)
        //    {
        //        gvSaleLtCost.DataSource = ds;
        //        gvSaleLtCost.DataBind();
        //        gvSaleLtCost.Visible = false;
        //    }
        //    else
        //    {
        //        gvSaleLtCost.DataSource = ds;
        //        gvSaleLtCost.DataBind();
        //        gvSaleLtCost.Visible = false;
        //    }
        //}

        public void ShowGrid_Sale_Ret()
        {

            DateTime Pdate = DateTime.Parse(txtDate.Text, dateformat, System.Globalization.DateTimeStyles.AssumeLocal);
            string p_Date = Pdate.ToString("yyyy/MM/dd");

            string connectionString = ConfigurationManager.ConnectionStrings["AslDbContext"].ConnectionString;
            SqlConnection conn = new SqlConnection(connectionString);

            conn.Open();
            SqlCommand cmd = new SqlCommand($@" 
 SELECT TRANSTP, TRANSDT, TRANSYY, TRANSNO, PSID, SUM(NETAMT) as AMOUNT from STK_TRANS where TRANSDT = '{p_Date}' and TRANSTP = 'IRTS' AND SUBSTRING(ITEMID,1,5) NOT IN ('10110')
 GROUP BY TRANSTP, TRANSDT, TRANSYY, TRANSNO, PSID", conn);
            SqlDataAdapter da = new SqlDataAdapter(cmd);
            DataSet ds = new DataSet();
            da.Fill(ds);
            conn.Close();
            if (ds.Tables[0].Rows.Count > 0)
            {
                gridSale_Ret.DataSource = ds;
                gridSale_Ret.DataBind();
                gridSale_Ret.Visible = false;
            }
            else
            {
                gridSale_Ret.DataSource = ds;
                gridSale_Ret.DataBind();
                gridSale_Ret.Visible = false;
            }
        }
        //public void ShowGridLC()
        //{
        //    DateTime Pdate = DateTime.Parse(txtDate.Text, dateformat, System.Globalization.DateTimeStyles.AssumeLocal);
        //    string p_Date = Pdate.ToString("yyyy/MM/dd");

        //    string connectionString = ConfigurationManager.ConnectionStrings["AslDbContext"].ConnectionString;
        //    SqlConnection conn = new SqlConnection(connectionString);

        //    conn.Open();
        //    SqlCommand cmd = new SqlCommand(" SELECT TRANSTP, TRANSDT, TRANSMY, TRANSNO, LCCD, CNBCD, AMOUNT, (CASE WHEN LCINVNO = '' THEN REMARKS WHEN REMARKS = '' THEN LCINVNO ELSE (REMARKS + ' - ' + LCINVNO)END)AS REMARKS FROM LC_EXPENSE " +
        //                                    " WHERE (TRANSDT = '" + p_Date + "')", conn);
        //    SqlDataAdapter da = new SqlDataAdapter(cmd);
        //    DataSet ds = new DataSet();
        //    da.Fill(ds);
        //    conn.Close();
        //    if (ds.Tables[0].Rows.Count > 0)
        //    {
        //        gridLC.DataSource = ds;
        //        gridLC.DataBind();
        //        gridLC.Visible = false;
        //    }
        //    else
        //    {
        //        gridLC.DataSource = ds;
        //        gridLC.DataBind();
        //        gridLC.Visible = false;
        //    }
        //}

        protected void btnProcess_Click(object sender, EventArgs e)
        {
            if (txtDate.Text == "")
            {
                Response.Write("<script>alert('Select a Date want to process?');</script>");
            }
            else
            {
                string userName = HttpContext.Current.Session["UserName"].ToString();

                string serialNo = "";
                int sl, serial;

                //ShowGrid();
                //ShowGrid_glCheque();
                //ShowGrid_Multiple();
                //ShowGrid_Purchase();
                //ShowGrid_Purchase_Import();
                //ShowGrid_Purchase_Ret();
                ShowGrid_Sale();
                ////ShowGrid_Sale_Discount();
                ShowGrid_Sale_Ret();
                //ShowGridLC();
                //ShowGrid_Sale_New();
                //ShowGrid_Sale_New_LtCost();

                iob.Transdt = (DateTime.Parse(txtDate.Text, dateformat, System.Globalization.DateTimeStyles.AssumeLocal));
                string trans_DT = iob.Transdt.ToString("yyyy/MM/dd");
                iob.Username = userName;

                string connectionString = ConfigurationManager.ConnectionStrings["AslDbContext"].ConnectionString;
                SqlConnection conn = new SqlConnection(connectionString);
                conn.Open();
                SqlCommand cmd = new SqlCommand("Delete from GL_MASTER where TRANSDT='" + trans_DT + "' and TABLEID = 'GL_STRANS' and TRANSTP <> 'OPEN'", conn);
                cmd.ExecuteNonQuery();

                SqlCommand cmd3 = new SqlCommand("Delete from GL_MASTER where TRANSDT='" + trans_DT + "' and TABLEID = 'GL_MTRANS' and TRANSTP <> 'OPEN'", conn);
                cmd3.ExecuteNonQuery();

                SqlCommand cmd1 = new SqlCommand("Delete from GL_MASTER where TRANSDT='" + trans_DT + "' and TABLEID = 'STK_TRANS' and TRANSTP IN ('JOUR','MREC')", conn);
                cmd1.ExecuteNonQuery();

                SqlCommand cmd2 = new SqlCommand("Delete from GL_MASTER where TRANSDT='" + trans_DT + "' and TABLEID = 'LC_EXPENSE'", conn);
                cmd2.ExecuteNonQuery();

                SqlCommand cmd4 = new SqlCommand("Delete from GL_MASTER where TRANSDT='" + trans_DT + "' and TABLEID = 'GL_CHEQUE'  and TRANSTP <> 'OPEN'", conn);
                cmd4.ExecuteNonQuery();



                conn.Close();

                foreach (GridViewRow grid in GridView1.Rows)
                {
                    try
                    {
                        if (grid.Cells[0].Text == "MREC")
                        {
                            dbFunctions.lblAdd(@"Select max(SERIALNO) FROM  GL_MASTER where TRANSDT = '" + trans_DT + "' and TRANSTP = 'MREC' and TABLEID='GL_STRANS'", lblSerial_Mrec);
                            if (lblSerial_Mrec.Text == "")
                            {
                                serialNo = "1000";
                                iob.SerialNo_MREC = serialNo;
                            }
                            else
                            {
                                sl = int.Parse(lblSerial_Mrec.Text);
                                serial = sl + 1;

                                iob.SerialNo_MREC = serial.ToString();
                            }

                            iob.Transtp = grid.Cells[0].Text;
                            iob.Monyear = grid.Cells[2].Text;
                            iob.TransNo = grid.Cells[3].Text;
                            iob.Transfor = grid.Cells[5].Text;
                            iob.Costpid = grid.Cells[6].Text;
                            iob.Transmode = grid.Cells[7].Text;
                            iob.Debitcd = grid.Cells[8].Text;
                            iob.Creditcd = grid.Cells[9].Text;
                            //if (grid.Cells[10].Text == "&nbsp;")
                            //{
                            //    iob.Chequeno = null;
                            //}
                            //else
                            iob.Chequeno = grid.Cells[10].Text;
                            iob.Chequedt = DateTime.Parse(grid.Cells[11].Text, dateformat, System.Globalization.DateTimeStyles.AssumeLocal);
                            iob.Amount = Convert.ToDecimal(grid.Cells[12].Text);
                            string Remarks = grid.Cells[13].Text;
                            if (Remarks == "&nbsp;")
                            {
                                iob.Remarks = "";
                            }
                            else
                                iob.Remarks = Remarks;



                            dob.doProcess_MREC(iob);
                        }
                        else if (grid.Cells[0].Text == "MPAY")
                        {
                            dbFunctions.lblAdd(@"Select max(SERIALNO) FROM  GL_MASTER where TRANSDT = '" + trans_DT + "' and TRANSTP = 'MPAY' and TABLEID='GL_STRANS' ", lblSerial_Mpay);
                            if (lblSerial_Mpay.Text == "")
                            {
                                serialNo = "2000";
                                iob.SerialNo_MREC = serialNo;
                            }
                            else
                            {
                                sl = int.Parse(lblSerial_Mpay.Text);
                                serial = sl + 1;
                                iob.SerialNo_MREC = serial.ToString();
                            }
                            iob.Transtp = grid.Cells[0].Text;
                            iob.Monyear = grid.Cells[2].Text;
                            iob.TransNo = grid.Cells[3].Text;
                            iob.Transfor = grid.Cells[5].Text;
                            iob.Costpid = grid.Cells[6].Text;
                            iob.Transmode = grid.Cells[7].Text;
                            iob.Debitcd = grid.Cells[8].Text;
                            iob.Creditcd = grid.Cells[9].Text;
                            iob.Chequeno = grid.Cells[10].Text;
                            iob.Chequedt = DateTime.Parse(grid.Cells[11].Text, dateformat, System.Globalization.DateTimeStyles.AssumeLocal);
                            iob.Amount = Convert.ToDecimal(grid.Cells[12].Text);
                            string Remarks = grid.Cells[13].Text;
                            if (Remarks == "&nbsp;")
                            {
                                iob.Remarks = "";
                            }
                            else
                                iob.Remarks = Remarks;

                            dob.doProcess_MPAY(iob);
                        }
                        else if (grid.Cells[0].Text == "JOUR")
                        {
                            dbFunctions.lblAdd(@"Select max(SERIALNO) FROM  GL_MASTER where TRANSDT = '" + trans_DT + "' and TRANSTP = 'JOUR' and TABLEID='GL_STRANS'", lblSerial_Jour);
                            if (lblSerial_Jour.Text == "")
                            {
                                serialNo = "3000";
                                iob.SerialNo_MREC = serialNo;
                            }
                            else
                            {
                                sl = int.Parse(lblSerial_Jour.Text);
                                serial = sl + 1;
                                iob.SerialNo_MREC = serial.ToString();
                            }
                            iob.Transtp = grid.Cells[0].Text;
                            iob.Monyear = grid.Cells[2].Text;
                            iob.TransNo = grid.Cells[3].Text;
                            iob.Transfor = grid.Cells[5].Text;
                            iob.Costpid = grid.Cells[6].Text;
                            iob.Transmode = grid.Cells[7].Text;
                            iob.Debitcd = grid.Cells[8].Text;
                            iob.Creditcd = grid.Cells[9].Text;
                            iob.Chequeno = grid.Cells[10].Text;
                            iob.Chequedt = DateTime.Parse(grid.Cells[11].Text, dateformat, System.Globalization.DateTimeStyles.AssumeLocal);
                            iob.Amount = Convert.ToDecimal(grid.Cells[12].Text);
                            string Remarks = grid.Cells[13].Text;
                            if (Remarks == "&nbsp;")
                            {
                                iob.Remarks = "";
                            }
                            else
                                iob.Remarks = Remarks;

                            dob.doProcess_JOUR(iob);
                        }
                        else if (grid.Cells[0].Text == "CONT")
                        {
                            dbFunctions.lblAdd(@"Select max(SERIALNO) FROM  GL_MASTER where TRANSDT = '" + trans_DT + "' and TRANSTP = 'CONT' and TABLEID='GL_STRANS'", lblSerial_Cont);
                            if (lblSerial_Cont.Text == "")
                            {
                                serialNo = "4000";
                                iob.SerialNo_MREC = serialNo;
                            }
                            else
                            {
                                sl = int.Parse(lblSerial_Cont.Text);
                                serial = sl + 1;
                                iob.SerialNo_MREC = serial.ToString();
                            }
                            iob.Transtp = grid.Cells[0].Text;
                            iob.Monyear = grid.Cells[2].Text;
                            iob.TransNo = grid.Cells[3].Text;
                            iob.Transfor = grid.Cells[5].Text;
                            iob.Costpid = grid.Cells[6].Text;
                            iob.Transmode = grid.Cells[7].Text;
                            iob.Debitcd = grid.Cells[8].Text;
                            iob.Creditcd = grid.Cells[9].Text;
                            iob.Chequeno = grid.Cells[10].Text;
                            iob.Chequedt = DateTime.Parse(grid.Cells[11].Text, dateformat, System.Globalization.DateTimeStyles.AssumeLocal);
                            iob.Amount = Convert.ToDecimal(grid.Cells[12].Text);
                            string Remarks = grid.Cells[13].Text;
                            if (Remarks == "&nbsp;")
                            {
                                iob.Remarks = "";
                            }
                            else
                                iob.Remarks = Remarks;


                            dob.doProcess_CONT(iob);
                        }


                    }
                    catch (Exception ex)
                    {
                        Response.Write(ex.Message);
                    }
                }

                //==============================================================================
                //GL_MTRANS PROCESS//
                //=============================================================================

                foreach (GridViewRow grid in gridMultiple.Rows)
                {
                    try
                    {
                        if (grid.Cells[0].Text == "MREC")
                        {
                            dbFunctions.lblAdd(@"Select max(SERIALNO) FROM  GL_MASTER where TRANSDT = '" + trans_DT + "' and TRANSTP = 'MREC' and TABLEID='GL_MTRANS'", lblSerial_Mrec);
                            if (lblSerial_Mrec.Text == "")
                            {
                                serialNo = "5000";
                                iob.SerialNo_MREC = serialNo;
                            }
                            else
                            {
                                sl = int.Parse(lblSerial_Mrec.Text);
                                serial = sl + 1;

                                iob.SerialNo_MREC = serial.ToString();
                            }

                            iob.Transtp = grid.Cells[0].Text;
                            iob.Monyear = grid.Cells[2].Text;
                            iob.TransNo = grid.Cells[3].Text;
                            iob.Transfor = grid.Cells[5].Text;
                            iob.Costpid = grid.Cells[6].Text;
                            iob.Transmode = grid.Cells[7].Text;
                            iob.Debitcd = grid.Cells[8].Text;
                            iob.Creditcd = grid.Cells[9].Text;
                            //if (grid.Cells[10].Text == "&nbsp;")
                            //{
                            //    iob.Chequeno = null;
                            //}
                            //else
                            iob.Chequeno = grid.Cells[10].Text;
                            iob.Chequedt = DateTime.Parse(grid.Cells[11].Text, dateformat, System.Globalization.DateTimeStyles.AssumeLocal);
                            iob.Amount = Convert.ToDecimal(grid.Cells[12].Text);
                            string Remarks = grid.Cells[13].Text;
                            if (Remarks == "&nbsp;")
                            {
                                iob.Remarks = "";
                            }
                            else
                                iob.Remarks = Remarks;



                            dob.doProcess_MREC_Multiple(iob);
                        }
                        else if (grid.Cells[0].Text == "MPAY")
                        {
                            dbFunctions.lblAdd(@"Select max(SERIALNO) FROM  GL_MASTER where TRANSDT = '" + trans_DT + "' and TRANSTP = 'MPAY' and TABLEID='GL_MTRANS' ", lblSerial_Mpay);
                            if (lblSerial_Mpay.Text == "")
                            {
                                serialNo = "6000";
                                iob.SerialNo_MREC = serialNo;
                            }
                            else
                            {
                                sl = int.Parse(lblSerial_Mpay.Text);
                                serial = sl + 1;
                                iob.SerialNo_MREC = serial.ToString();
                            }
                            iob.Transtp = grid.Cells[0].Text;
                            iob.Monyear = grid.Cells[2].Text;
                            iob.TransNo = grid.Cells[3].Text;
                            iob.Transfor = grid.Cells[5].Text;
                            iob.Costpid = grid.Cells[6].Text;
                            iob.Transmode = grid.Cells[7].Text;
                            iob.Debitcd = grid.Cells[8].Text;
                            iob.Creditcd = grid.Cells[9].Text;
                            iob.Chequeno = grid.Cells[10].Text;
                            iob.Chequedt = DateTime.Parse(grid.Cells[11].Text, dateformat, System.Globalization.DateTimeStyles.AssumeLocal);
                            iob.Amount = Convert.ToDecimal(grid.Cells[12].Text);
                            string Remarks = grid.Cells[13].Text;
                            if (Remarks == "&nbsp;")
                            {
                                iob.Remarks = "";
                            }
                            else
                                iob.Remarks = Remarks;

                            dob.doProcess_MPAY_Multiple(iob);
                        }
                        else if (grid.Cells[0].Text == "JOUR")
                        {
                            dbFunctions.lblAdd(@"Select max(SERIALNO) FROM  GL_MASTER where TRANSDT = '" + trans_DT + "' and TRANSTP = 'JOUR' and TABLEID='GL_MTRANS'", lblSerial_Jour);
                            if (lblSerial_Jour.Text == "")
                            {
                                serialNo = "7000";
                                iob.SerialNo_MREC = serialNo;
                            }
                            else
                            {
                                sl = int.Parse(lblSerial_Jour.Text);
                                serial = sl + 1;
                                iob.SerialNo_MREC = serial.ToString();
                            }
                            iob.Transtp = grid.Cells[0].Text;
                            iob.Monyear = grid.Cells[2].Text;
                            iob.TransNo = grid.Cells[3].Text;
                            iob.Transfor = grid.Cells[5].Text;
                            iob.Costpid = grid.Cells[6].Text;
                            iob.Transmode = grid.Cells[7].Text;
                            iob.Debitcd = grid.Cells[8].Text;
                            iob.Creditcd = grid.Cells[9].Text;
                            iob.Chequeno = grid.Cells[10].Text;
                            iob.Chequedt = DateTime.Parse(grid.Cells[11].Text, dateformat, System.Globalization.DateTimeStyles.AssumeLocal);
                            iob.Amount = Convert.ToDecimal(grid.Cells[12].Text);
                            string Remarks = grid.Cells[13].Text;
                            if (Remarks == "&nbsp;")
                            {
                                iob.Remarks = "";
                            }
                            else
                                iob.Remarks = Remarks;

                            dob.doProcess_JOUR_Multiple(iob);
                        }
                        else if (grid.Cells[0].Text == "CONT")
                        {
                            dbFunctions.lblAdd(@"Select max(SERIALNO) FROM  GL_MASTER where TRANSDT = '" + trans_DT + "' and TRANSTP = 'CONT' and TABLEID='GL_MTRANS'", lblSerial_Cont);
                            if (lblSerial_Cont.Text == "")
                            {
                                serialNo = "8000";
                                iob.SerialNo_MREC = serialNo;
                            }
                            else
                            {
                                sl = int.Parse(lblSerial_Cont.Text);
                                serial = sl + 1;
                                iob.SerialNo_MREC = serial.ToString();
                            }
                            iob.Transtp = grid.Cells[0].Text;
                            iob.Monyear = grid.Cells[2].Text;
                            iob.TransNo = grid.Cells[3].Text;
                            iob.Transfor = grid.Cells[5].Text;
                            iob.Costpid = grid.Cells[6].Text;
                            iob.Transmode = grid.Cells[7].Text;
                            iob.Debitcd = grid.Cells[8].Text;
                            iob.Creditcd = grid.Cells[9].Text;
                            iob.Chequeno = grid.Cells[10].Text;
                            iob.Chequedt = DateTime.Parse(grid.Cells[11].Text, dateformat, System.Globalization.DateTimeStyles.AssumeLocal);
                            iob.Amount = Convert.ToDecimal(grid.Cells[12].Text);
                            string Remarks = grid.Cells[13].Text;
                            if (Remarks == "&nbsp;")
                            {
                                iob.Remarks = "";
                            }
                            else
                                iob.Remarks = Remarks;

                            dob.doProcess_CONT_Multiple(iob);
                        }


                    }
                    catch (Exception ex)
                    {
                        Response.Write(ex.Message);
                    }
                }

                //Response.Write("<script>alert('Process Completed.');</script>");

               // gLCHEQUE_process();
                //Buy_process();
                //Buy_process_Import();
                //Buy_process_Ret();
                Sale_process();
                ////Sale_Discount_process();
                Sale_process_Ret();
                //LC_Process();
                //Sale_process_New();
                //Sale_process_New_LtCost();

                Response.Write("<script>alert('Process Completed.');</script>");
            }
        }


        public void ShowGrid_glCheque()
        {

            DateTime Pdate = DateTime.Parse(txtDate.Text, dateformat, System.Globalization.DateTimeStyles.AssumeLocal);
            string p_Date = Pdate.ToString("yyyy/MM/dd");

            string connectionString = ConfigurationManager.ConnectionStrings["AslDbContext"].ConnectionString;
            SqlConnection conn = new SqlConnection(connectionString);

            conn.Open();
            SqlCommand cmd = new SqlCommand(" SELECT TRANSTP, TRANSDT, TRANSMY, TRANSNO, TRANSMODE, DEBITCD, CREDITCD,CHEQUENO, convert(nvarchar(20),CHEQUEDT,103) as CHEQUEDT,CHQBANKBR, STATUS, AMOUNT, REMARKS " +
                                            "FROM GL_CHEQUE  where TRANSDT = '" + p_Date + "'", conn);
            SqlDataAdapter da = new SqlDataAdapter(cmd);
            DataSet ds = new DataSet();
            da.Fill(ds);
            conn.Close();
            if (ds.Tables[0].Rows.Count > 0)
            {
                GridGLCheque.DataSource = ds;
                GridGLCheque.DataBind();
                GridGLCheque.Visible = false;
            }
            else
            {
                GridGLCheque.DataSource = ds;
                GridGLCheque.DataBind();
                GridGLCheque.Visible = false;
                //Response.Write("<script>alert('No Data Found');</script>");
                //GridGLCheque.Visible = false;
            }
        }


        //GL_CHEQUE PROCESS START //GL_CHEQUE PROCESS START //GL_CHEQUE PROCESS START //GL_CHEQUE PROCESS START
        //GL_CHEQUE PROCESS START //GL_CHEQUE PROCESS START //GL_CHEQUE PROCESS START //GL_CHEQUE PROCESS START
        //GL_CHEQUE PROCESS START //GL_CHEQUE PROCESS START //GL_CHEQUE PROCESS START //GL_CHEQUE PROCESS START
        //GL_CHEQUE PROCESS START //GL_CHEQUE PROCESS START //GL_CHEQUE PROCESS START //GL_CHEQUE PROCESS START

        public void gLCHEQUE_process()
        {
            string userName = HttpContext.Current.Session["UserName"].ToString();

            string serialNo = "";
            int sl, serial;

            iob.Username = userName;

            DateTime Transdt = (DateTime.Parse(txtDate.Text, dateformat, System.Globalization.DateTimeStyles.AssumeLocal));
            string trans_DT = Transdt.ToString("yyyy/MM/dd");

            foreach (GridViewRow grid in GridGLCheque.Rows)
            {
                try
                {
                    if (grid.Cells[0].Text == "MREC")
                    {
                        dbFunctions.lblAdd(@"Select max(SERIALNO) FROM  GL_MASTER where TRANSDT = '" + trans_DT + "' and TRANSTP = 'MREC' ", lblSerial_Mrec);
                        if (lblSerial_Mrec.Text == "")
                        {
                            serialNo = "5100";
                            iob.SerialNo_MREC = serialNo;
                        }
                        else
                        {
                            sl = int.Parse(lblSerial_Mrec.Text);
                            serial = sl + 1;

                            iob.SerialNo_MREC = serial.ToString();
                        }

                        iob.Transtp = grid.Cells[0].Text;
                        iob.Transdt = DateTime.Parse(grid.Cells[1].Text);
                        iob.Monyear = grid.Cells[2].Text;
                        iob.TransNo = grid.Cells[3].Text;
                        // iob.Transfor = grid.Cells[5].Text;
                        //  iob.Costpid = grid.Cells[6].Text;
                        iob.Transmode = grid.Cells[4].Text;
                        if (grid.Cells[10].Text == "DISHONORED" || grid.Cells[10].Text == "RETURNED")
                        {
                            iob.Debitcd = grid.Cells[6].Text;
                            iob.Creditcd = grid.Cells[5].Text;
                        }
                        else
                        {
                            iob.Debitcd = grid.Cells[5].Text;
                            iob.Creditcd = grid.Cells[6].Text;
                        }

                        //if (grid.Cells[10].Text == "&nbsp;")
                        //{
                        //    iob.Chequeno = null;
                        //}
                        //else
                        iob.Chequeno = grid.Cells[7].Text;
                        iob.Chequedt = DateTime.Parse(grid.Cells[8].Text, dateformat, System.Globalization.DateTimeStyles.AssumeLocal);
                        string Remarks = grid.Cells[12].Text;
                        string chquebr = grid.Cells[9].Text;
                        string Status = grid.Cells[10].Text;

                        if (Remarks == "&nbsp;")
                        {
                            Remarks = "";
                        }
                        else
                            Remarks = grid.Cells[12].Text;

                        iob.Remarks = Status + '-' + chquebr + '-' + Remarks;
                        if (grid.Cells[10].Text != "HONORED")
                        {
                            iob.Amount = 0;

                        }
                        else
                        {
                            iob.Amount = Convert.ToDecimal(grid.Cells[11].Text);
                        }

                        dob.gl_Cheque_Process_MREC(iob);
                    }
                    else if (grid.Cells[0].Text == "MPAY")
                    {
                        dbFunctions.lblAdd(@"Select max(SERIALNO) FROM  GL_MASTER where TRANSDT = '" + trans_DT + "' and TRANSTP = 'MPAY' ", lblSerial_Mpay);
                        if (lblSerial_Mpay.Text == "")
                        {
                            serialNo = "5600";
                            iob.SerialNo_MREC = serialNo;
                        }
                        else
                        {
                            sl = int.Parse(lblSerial_Mpay.Text);
                            serial = sl + 1;
                            iob.SerialNo_MREC = serial.ToString();
                        }
                        iob.Transtp = grid.Cells[0].Text;
                        iob.Transdt = DateTime.Parse(grid.Cells[1].Text);
                        iob.Monyear = grid.Cells[2].Text;
                        iob.TransNo = grid.Cells[3].Text;
                        // iob.Transfor = grid.Cells[5].Text;
                        //  iob.Costpid = grid.Cells[6].Text;
                        iob.Transmode = grid.Cells[4].Text;
                        if (grid.Cells[10].Text == "DISHONORED" || grid.Cells[10].Text == "RETURNED")
                        {
                            iob.Debitcd = grid.Cells[6].Text;
                            iob.Creditcd = grid.Cells[5].Text;
                        }
                        else
                        {
                            iob.Debitcd = grid.Cells[5].Text;
                            iob.Creditcd = grid.Cells[6].Text;
                        }
                        //if (grid.Cells[10].Text == "&nbsp;")
                        //{
                        //    iob.Chequeno = null;
                        //}
                        //else
                        iob.Chequeno = grid.Cells[7].Text;
                        iob.Chequedt = DateTime.Parse(grid.Cells[8].Text, dateformat, System.Globalization.DateTimeStyles.AssumeLocal);
                        string Remarks = grid.Cells[12].Text;
                        string chquebr = grid.Cells[9].Text;
                        string Status = grid.Cells[10].Text;
                        if (Remarks == "&nbsp;")
                        {
                            Remarks = "";
                        }
                        else
                            Remarks = grid.Cells[12].Text;

                        iob.Remarks = Status + '-' + chquebr + '-' + Remarks;
                        if (grid.Cells[10].Text != "HONORED")
                        {
                            iob.Amount = 0;

                        }
                        else
                        {
                            iob.Amount = Convert.ToDecimal(grid.Cells[11].Text);
                        }

                        dob.gl_Cheque_Process_MREC(iob);
                    }

                }
                catch (Exception ex)
                {
                    Response.Write(ex.Message);
                }
            }

        }





        public void Buy_process()
        {
            string userName = HttpContext.Current.Session["UserName"].ToString();

            string serialNo = "";
            int sl, serial;

            iob.Username = userName;

            DateTime Transdt = (DateTime.Parse(txtDate.Text, dateformat, System.Globalization.DateTimeStyles.AssumeLocal));
            string trans_DT = Transdt.ToString("yyyy/MM/dd");

            foreach (GridViewRow grid in GridView2.Rows)
            {
                try
                {
                    if (grid.Cells[0].Text == "BUY")
                    {
                        dbFunctions.lblAdd(@"Select max(SERIALNO) FROM  GL_MASTER where TRANSDT = '" + trans_DT + "' and TRANSTP = 'JOUR' and SERIALNO like '11%' ", lblSerial_BUY);
                        if (lblSerial_BUY.Text == "")
                        {
                            serialNo = "11000";
                            iob.Serial_BUY = serialNo;
                        }
                        else
                        {
                            sl = int.Parse(lblSerial_BUY.Text);
                            serial = sl + 1;

                            iob.Serial_BUY = serial.ToString();
                        }

                        iob.Transtp = "JOUR";
                        iob.Transdt = DateTime.Parse(grid.Cells[1].Text);
                        iob.Monyear = grid.Cells[2].Text;
                        iob.TransNo = grid.Cells[3].Text;
                        iob.StoreTo = grid.Cells[5].Text;

                        iob.Debitcd = "102060100001";
                        iob.Creditcd = grid.Cells[6].Text;
                        iob.Amount = Convert.ToDecimal(grid.Cells[11].Text);
                        string Remarks = grid.Cells[10].Text;
                        if (Remarks == "&nbsp;")
                        {
                            iob.Remarks = "";
                        }
                        else
                            iob.Remarks = Remarks;



                        dob.doProcess_BUY(iob);
                    }
                }
                catch (Exception ex)
                {
                    Response.Write(ex.Message);
                }
            }

        }

        public void Buy_process_Ret()
        {
            string userName = HttpContext.Current.Session["UserName"].ToString();

            string serialNo = "";
            int sl, serial;

            iob.Username = userName;

            DateTime Transdt = (DateTime.Parse(txtDate.Text, dateformat, System.Globalization.DateTimeStyles.AssumeLocal));
            string trans_DT = Transdt.ToString("yyyy/MM/dd");

            foreach (GridViewRow grid in gridPurchase_Ret.Rows)
            {
                try
                {
                    if (grid.Cells[0].Text == "IRTB")
                    {
                        dbFunctions.lblAdd(@"Select max(SERIALNO) FROM  GL_MASTER where TRANSDT = '" + trans_DT + "' and TRANSTP = 'JOUR' and SERIALNO like '14%' ", lblSerial_BUY);
                        if (lblSerial_BUY.Text == "")
                        {
                            serialNo = "14000";
                            iob.Serial_BUY = serialNo;
                        }
                        else
                        {
                            sl = int.Parse(lblSerial_BUY.Text);
                            serial = sl + 1;

                            iob.Serial_BUY = serial.ToString();
                        }

                        iob.Transtp = "JOUR";
                        iob.Transdt = DateTime.Parse(grid.Cells[1].Text);
                        iob.Monyear = grid.Cells[2].Text;
                        iob.TransNo = grid.Cells[3].Text;
                        iob.StoreFrom = grid.Cells[5].Text;

                        iob.Debitcd = grid.Cells[6].Text;
                        iob.Creditcd = "401020100002";
                        iob.Amount = Convert.ToDecimal(grid.Cells[11].Text);
                        string Remarks = grid.Cells[10].Text;
                        if (Remarks == "&nbsp;")
                        {
                            iob.Remarks = "";
                        }
                        else
                            iob.Remarks = Remarks;



                        dob.doProcess_BUY_Ret(iob);
                    }
                }
                catch (Exception ex)
                {
                    Response.Write(ex.Message);
                }
            }

        }

        public void Buy_process_Import()
        {
            string userName = HttpContext.Current.Session["UserName"].ToString();
            string serialNo = "";
            int sl, serial;

            iob.Username = userName;

            DateTime Transdt = (DateTime.Parse(txtDate.Text, dateformat, System.Globalization.DateTimeStyles.AssumeLocal));
            string trans_DT = Transdt.ToString("yyyy/MM/dd");

            foreach (GridViewRow grid in gvBuyImport.Rows)
            {
                try
                {
                    if (grid.Cells[0].Text == "BUY")
                    {
                        dbFunctions.lblAdd(@"Select max(SERIALNO) FROM  GL_MASTER where TRANSDT = '" + trans_DT + "' and TRANSTP = 'JOUR' and SERIALNO like '21%' ", lblSerial_BUY);
                        if (lblSerial_BUY.Text == "")
                        {
                            serialNo = "21000";
                            iob.Serial_BUY = serialNo;
                        }
                        else
                        {
                            sl = int.Parse(lblSerial_BUY.Text);
                            serial = sl + 1;

                            iob.Serial_BUY = serial.ToString();
                        }

                        iob.Transtp = "JOUR";
                        iob.Transdt = DateTime.Parse(grid.Cells[1].Text);
                        iob.Monyear = grid.Cells[2].Text;
                        iob.TransNo = grid.Cells[3].Text;
                        iob.StoreTo = grid.Cells[5].Text;

                        iob.Debitcd = "102060100001";
                        iob.Creditcd = grid.Cells[8].Text;
                        iob.Amount = Convert.ToDecimal(grid.Cells[11].Text);
                        string Remarks = grid.Cells[10].Text;
                        if (Remarks == "&nbsp;")
                        {
                            iob.Remarks = "IMPORT";
                        }
                        else
                            iob.Remarks = "IMPORT - " + Remarks;



                        dob.doProcess_BUY(iob);
                    }
                }
                catch (Exception ex)
                {
                    Response.Write(ex.Message);
                }
            }

        }

        public void Sale_process()
        {
            string userName = HttpContext.Current.Session["UserName"].ToString();
            string serialNo = "";
            int sl, serial;

            iob.Username = userName;

            DateTime Transdt = (DateTime.Parse(txtDate.Text, dateformat, System.Globalization.DateTimeStyles.AssumeLocal));
            string trans_DT = Transdt.ToString("yyyy/MM/dd");

            foreach (GridViewRow grid in GridView3.Rows)
            {
                try
                {
                    if (grid.Cells[0].Text == "SALE")
                    {
                        dbFunctions.lblAdd(@"Select max(SERIALNO) FROM  GL_MASTER where TRANSDT = '" + trans_DT + "' and TRANSTP = 'JOUR' and SERIALNO like '12%'", lblSerial_SALE);
                        if (lblSerial_SALE.Text == "")
                        {
                            serialNo = "12000";
                            iob.Serial_SALE = serialNo;
                        }
                        else
                        {
                            sl = int.Parse(lblSerial_SALE.Text);
                            serial = sl + 1;

                            iob.Serial_SALE = serial.ToString();
                        }

                        iob.Transtp = "JOUR";
                        iob.Transdt = DateTime.Parse(grid.Cells[1].Text);
                        iob.Monyear = grid.Cells[2].Text;
                        iob.TransNo = grid.Cells[3].Text;
                       // iob.StoreFrom = grid.Cells[4].Text;

                        iob.Debitcd = "1020201"+grid.Cells[4].Text;
                        iob.Creditcd = "301010100001";
                        iob.Amount = Convert.ToDecimal(grid.Cells[5].Text);
                        iob.Remarks = "SALE";
                        //string Remarks = grid.Cells[10].Text;
                        //if (Remarks == "&nbsp;")
                        //{
                        //    iob.Remarks = "";
                        //}
                        //else
                        //    iob.Remarks = Remarks;



                        dob.doProcess_SALE(iob);
                    }
                }
                catch (Exception ex)
                {
                    Response.Write(ex.Message);
                }
            }
        }

        public void Sale_process_New()
        {
            string userName = HttpContext.Current.Session["UserName"].ToString();
            string serialNo = "";
            int sl, serial;

            iob.Username = userName;

            DateTime Transdt = (DateTime.Parse(txtDate.Text, dateformat, System.Globalization.DateTimeStyles.AssumeLocal));
            string trans_DT = Transdt.ToString("yyyy/MM/dd");

            foreach (GridViewRow grid in gvSaleNew.Rows)
            {
                try
                {
                    if (grid.Cells[0].Text == "SALE")
                    {
                        dbFunctions.lblAdd(@"Select max(SERIALNO) FROM  GL_MASTER where TRANSDT = '" + trans_DT + "' and TRANSTP = 'JOUR' and SERIALNO like '12%'", lblSerial_SALE);
                        if (lblSerial_SALE.Text == "")
                        {
                            serialNo = "12000";
                            iob.Serial_SALE = serialNo;
                        }
                        else
                        {
                            sl = int.Parse(lblSerial_SALE.Text);
                            serial = sl + 1;

                            iob.Serial_SALE = serial.ToString();
                        }

                        iob.Transtp = "JOUR";
                        iob.Transdt = DateTime.Parse(grid.Cells[1].Text);
                        iob.Monyear = grid.Cells[2].Text;
                        iob.TransNo = grid.Cells[3].Text;
                        iob.StoreFrom = grid.Cells[4].Text;

                        iob.Debitcd = grid.Cells[6].Text;
                        iob.Creditcd = "301010100001";
                        iob.Amount = Convert.ToDecimal(grid.Cells[11].Text);
                        string Remarks = grid.Cells[10].Text;
                        if (Remarks == "&nbsp;")
                        {
                            iob.Remarks = "";
                        }
                        else
                            iob.Remarks = Remarks;

                        dob.doProcess_SALE(iob);
                    }
                }
                catch (Exception ex)
                {
                    Response.Write(ex.Message);
                }
            }
        }

        public void Sale_process_New_LtCost()
        {
            string userName = HttpContext.Current.Session["UserName"].ToString();
            string serialNo = "";
            int sl, serial;

            iob.Username = userName;

            DateTime Transdt = (DateTime.Parse(txtDate.Text, dateformat, System.Globalization.DateTimeStyles.AssumeLocal));
            string trans_DT = Transdt.ToString("yyyy/MM/dd");

            foreach (GridViewRow grid in gvSaleLtCost.Rows)
            {
                try
                {
                    if (grid.Cells[0].Text == "SALE")
                    {
                        dbFunctions.lblAdd(@"Select max(SERIALNO) FROM  GL_MASTER where TRANSDT = '" + trans_DT + "' and TRANSTP = 'JOUR' and SERIALNO like '16%'", lblSerial_SALE);
                        if (lblSerial_SALE.Text == "")
                        {
                            serialNo = "16000";
                            iob.Serial_SALE = serialNo;
                        }
                        else
                        {
                            sl = int.Parse(lblSerial_SALE.Text);
                            serial = sl + 1;

                            iob.Serial_SALE = serial.ToString();
                        }

                        iob.Transtp = "JOUR";
                        iob.Transdt = DateTime.Parse(grid.Cells[1].Text);
                        iob.Monyear = grid.Cells[2].Text;
                        iob.TransNo = grid.Cells[3].Text;
                        iob.StoreFrom = grid.Cells[4].Text;

                        iob.Debitcd = grid.Cells[6].Text;
                        iob.Creditcd = "202030100001";
                        iob.Amount = Convert.ToDecimal(grid.Cells[11].Text);
                        string Remarks = grid.Cells[10].Text;
                        if (Remarks == "&nbsp;")
                        {
                            iob.Remarks = "";
                        }
                        else
                            iob.Remarks = Remarks;

                        dob.doProcess_SALE_LtCost(iob);
                    }
                }
                catch (Exception ex)
                {
                    Response.Write(ex.Message);
                }
            }
        }

        public void Sale_Discount_process()
        {
            string userName = HttpContext.Current.Session["UserName"].ToString();
            string serialNo = "";
            int sl, serial;

            iob.Username = userName;

            DateTime Transdt = (DateTime.Parse(txtDate.Text, dateformat, System.Globalization.DateTimeStyles.AssumeLocal));
            string trans_DT = Transdt.ToString("yyyy/MM/dd");

            foreach (GridViewRow grid in GridView4.Rows)
            {
                try
                {
                    if (grid.Cells[0].Text == "SALE")
                    {
                        dbFunctions.lblAdd(@"Select max(SERIALNO) FROM  GL_MASTER where TRANSDT = '" + trans_DT + "' and TRANSTP = 'JOUR' and SERIALNO like '13%'", lblSlSale_Dis);
                        if (lblSlSale_Dis.Text == "")
                        {
                            serialNo = "13000";
                            iob.Sl_Sale_dis = serialNo;
                        }
                        else
                        {
                            sl = int.Parse(lblSlSale_Dis.Text);
                            serial = sl + 1;

                            iob.Sl_Sale_dis = serial.ToString();
                        }

                        iob.Transtp = "JOUR";
                        iob.Transdt = DateTime.Parse(grid.Cells[1].Text);
                        iob.Monyear = grid.Cells[2].Text;
                        iob.TransNo = grid.Cells[3].Text;
                        iob.StoreFrom = grid.Cells[4].Text;

                        iob.Debitcd = "401030100001";
                        iob.Creditcd = grid.Cells[5].Text;
                        iob.Amount = Convert.ToDecimal(grid.Cells[7].Text);
                        string Remarks = grid.Cells[6].Text;
                        if (Remarks == "&nbsp;")
                        {
                            iob.Remarks = "";
                        }
                        else
                            iob.Remarks = Remarks;



                        dob.doProcess_SALE_DisCount(iob);
                    }
                }
                catch (Exception ex)
                {
                    Response.Write(ex.Message);
                }
            }
        }

        public void Sale_process_Ret()
        {
            string userName = HttpContext.Current.Session["UserName"].ToString();
            string serialNo = "";
            int sl, serial;

            iob.Username = userName;

            DateTime Transdt = (DateTime.Parse(txtDate.Text, dateformat, System.Globalization.DateTimeStyles.AssumeLocal));
            string trans_DT = Transdt.ToString("yyyy/MM/dd");

            foreach (GridViewRow grid in gridSale_Ret.Rows)
            {
                try
                {
                    if (grid.Cells[0].Text == "IRTS")
                    {
                        dbFunctions.lblAdd(@"Select max(SERIALNO) FROM  GL_MASTER where TRANSDT = '" + trans_DT + "' and TRANSTP = 'JOUR' and SERIALNO like '15%'", lblSerial_SALE);
                        if (lblSerial_SALE.Text == "")
                        {
                            serialNo = "15000";
                            iob.Serial_SALE = serialNo;
                        }
                        else
                        {
                            sl = int.Parse(lblSerial_SALE.Text);
                            serial = sl + 1;

                            iob.Serial_SALE = serial.ToString();
                        }

                        //iob.Transtp = "JOUR";
                        //iob.Transdt = DateTime.Parse(grid.Cells[1].Text);
                        //iob.Monyear = grid.Cells[2].Text;
                        //iob.TransNo = grid.Cells[3].Text;
                        //// iob.StoreFrom = grid.Cells[4].Text;

                        //iob.Debitcd = "1020201" + grid.Cells[4].Text;
                        //iob.Creditcd = "301010100001";
                        //iob.Amount = Convert.ToDecimal(grid.Cells[5].Text);
                        //iob.Remarks = "SALE";
                       // TRANSTP, TRANSDT, TRANSYY, TRANSNO, PSID, SUM(NETAMT) as AMOUNT


                        iob.Transtp = "JOUR";
                        iob.Transdt = DateTime.Parse(grid.Cells[1].Text);
                        iob.Monyear = grid.Cells[2].Text;
                        iob.TransNo = grid.Cells[3].Text;
                       // iob.StoreFrom = grid.Cells[4].Text;

                        iob.Debitcd = "301010100002"; 
                        iob.Creditcd = "1020201" + grid.Cells[4].Text;
                        iob.Amount = Convert.ToDecimal(grid.Cells[5].Text);
                        string Remarks = "SALE RETURN";
                        //if (Remarks == "&nbsp;")
                        //{
                        //    iob.Remarks = "";
                        //}
                        //else
                        //    iob.Remarks = Remarks;



                        dob.doProcess_SALE_Ret(iob);
                    }
                }
                catch (Exception ex)
                {
                    Response.Write(ex.Message);
                }
            }
        }

        public void LC_Process()
        {
            string userName = HttpContext.Current.Session["UserName"].ToString();
            string ipAddress = HttpContext.Current.Session["IpAddress"].ToString();
            string PCName = HttpContext.Current.Session["PCName"].ToString();
            string connectionString = ConfigurationManager.ConnectionStrings["AslDbContext"].ConnectionString;
            SqlConnection conn = new SqlConnection(connectionString);

            string serialNo = "";
            int sl, serial;

            iob.Username = userName;
            iob.Userpc = PCName;
            iob.Ipaddress = ipAddress;

            DateTime Transdt = (DateTime.Parse(txtDate.Text, dateformat, System.Globalization.DateTimeStyles.AssumeLocal));
            string trans_DT = Transdt.ToString("yyyy/MM/dd");

            foreach (GridViewRow grid in gridLC.Rows)
            {
                try
                {
                    dbFunctions.lblAdd(@"Select max(SERIALNO) FROM  GL_MASTER where TRANSDT = '" + trans_DT + "' and TABLEID = 'LC_EXPENSE' ", lblSerial_LC);

                    if (lblSerial_LC.Text == "")
                    {
                        serialNo = "20000";
                        iob.SerialNo_MREC = serialNo;
                    }
                    else
                    {
                        sl = int.Parse(lblSerial_LC.Text);
                        serial = sl + 1;
                        iob.SerialNo_MREC = serial.ToString();
                    }

                    string CD = grid.Cells[5].Text;
                    string subCD = CD.Substring(0, 7);

                    if ((subCD == "1020101") || (subCD == "1020102") || (subCD == "2020103"))
                    {
                        iob.Transtp = "MPAY";
                    }
                    else
                    {
                        iob.Transtp = "JOUR";
                    }
                    iob.Transdt = DateTime.Parse(grid.Cells[1].Text);
                    iob.Monyear = grid.Cells[2].Text;
                    iob.TransNo = grid.Cells[3].Text;
                    iob.Debitcd = grid.Cells[4].Text;
                    iob.Creditcd = grid.Cells[5].Text;
                    iob.Amount = Convert.ToDecimal(grid.Cells[6].Text);
                    string Remarks = grid.Cells[7].Text;
                    if (Remarks == "&nbsp;")
                    {
                        iob.Remarks = "";
                    }
                    else
                        iob.Remarks = Remarks;

                    dob.doProcess_LC(iob);

                }
                catch (Exception ex)
                {
                    Response.Write(ex.Message);
                }
            }

        }


        protected void txtDate_TextChanged(object sender, EventArgs e)
        {
            ShowGrid();
            ShowGrid_Multiple();
            ShowGrid_glCheque();
         //   ShowGrid_Purchase();
           // ShowGrid_Purchase_Import();
          //  ShowGrid_Purchase_Ret();
            ShowGrid_Sale();
            //ShowGrid_Sale_Discount();
            ShowGrid_Sale_Ret();
          //  ShowGridLC();
            //ShowGrid_Sale_New();
            //ShowGrid_Sale_New_LtCost();
            btnProcess.Focus();
        }
    }
}