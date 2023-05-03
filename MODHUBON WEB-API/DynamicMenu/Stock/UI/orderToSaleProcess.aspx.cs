using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using AlchemyAccounting;
using DynamicMenu.Functions;
using DynamicMenu.Stock.DataAccess;
using DynamicMenu.Stock.Interface;

namespace DynamicMenu.Stock.UI
{
    public partial class orderToSaleProcess : System.Web.UI.Page
    {
        IFormatProvider dateformat = new System.Globalization.CultureInfo("fr-FR", true);
        StockDataAcces dob = new StockDataAcces();
        StockInterface iob = new StockInterface();
        protected void Page_Load(object sender, EventArgs e)
        {
            if (Session["UserName"] == null)
            {
                Response.Redirect("~/Login/UI/Login.aspx");
            }
            else
            {
                string formLink = "/Stock/UI/orderToSaleProcess.aspx";
                bool permission = UserPermissionChecker.checkParmit(formLink, "STATUS");
                if (permission == true)
                {
                    if (!IsPostBack)
                    {

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

        protected void btnProcess_OnClick(object sender, EventArgs e)
        {
            DateTime date = DateTime.Parse(txtDate.Text, dateformat, System.Globalization.DateTimeStyles.AssumeLocal);
            string transDate = date.ToString("yyyy-MM-dd");
            DateTime year = dbFunctions.Timezone(DateTime.Now);
            string varYear = year.ToString("yyyy");


            dbFunctions.lblAdd(@"SELECT DISTINCT TRANSDT FROM STK_TRANS WHERE TRANSTP='SALE' AND TRANSDT='" + transDate + "'", lblPCheckDate);
            if (lblPCheckDate.Text == "")
            {


                GridView gvMaster = new GridView();

                iob.UserID = HttpContext.Current.Session["USERID"].ToString();
                iob.Ipaddress = HttpContext.Current.Session["IpAddress"].ToString();
                iob.UserPC = HttpContext.Current.Session["PCName"].ToString();
                iob.ITime = dbFunctions.Timezone(DateTime.Now);



                if (txtDate.Text == "")
                {
                    Response.Write("<script>alert('Select a Date want to process?');</script>");
                }
                else
                {
                    SqlConnection con = new SqlConnection(dbFunctions.connection);
                    con.Open();
                    SqlCommand cmd = new SqlCommand(@"SELECT DISTINCT COMPID, PSID, BILLDT
                    FROM STK_TRANSMST WHERE TRANSTP='IORD' AND BILLDT='" + transDate + "' ORDER BY PSID", con);
                    SqlDataReader dr = cmd.ExecuteReader();
                    foreach (var item in dr)
                    {
                        string COMPID = dr["COMPID"].ToString();
                        //string TRANSTP = dr["TRANSTP"].ToString();
                        string TRANSDT = "01-01-1990";
                        //  string TRANSYY = dr["TRANSYY"].ToString();
                        // string TRANSNO = dr["TRANSNO"].ToString();
                        string PSID = dr["PSID"].ToString();
                        // string PSSL = dr["PSSL"].ToString();
                        string BILLDT = dr["BILLDT"].ToString();
                        // string REMARKS = dr["REMARKS"].ToString();


                        //  DateTime date1 = DateTime.Parse(transDate, dateformat, System.Globalization.DateTimeStyles.AssumeLocal);
                        //   DateTime date1YEAR = DateTime.Parse(BILLDT, dateformat, System.Globalization.DateTimeStyles.AssumeLocal);
                        //   string transDate1 = date1.ToString("yyyy-MM-dd");
                        // DateTime year = dbFunctions.Timezone(DateTime.Now);
                        //  string varYear = date1YEAR.ToString("yyyy");



                        string Trans = TransNumber(varYear);

                        iob.CompanyId = COMPID;
                        iob.TransTP = "SALE";
                        iob.TrDt = Convert.ToDateTime(BILLDT);
                        iob.MonthYear = varYear;
                        iob.TransNo = Convert.ToInt64(Trans);
                        iob.PartyId = PSID;
                        iob.PartySerial = StockTransaction.SalePartySerial(iob.TrDt.Year.ToString(), iob.PartyId);
                        iob.BillDate = Convert.ToDateTime(BILLDT);
                        iob.Remarks = "";
                        iob.vehicalsno = "";
                        iob.driveerNm = "";
                        iob.asstNm = "";


                        string result = dob.InsertSaleTransactionMaster(iob);
                        if (result == "true")
                        {



                            SqlCommand cmd1 = new SqlCommand(@"SELECT  COMPID, PSID, BILLDT, ITEMID, SUM(ISNULL(QTY,0)) QTY  FROM STK_TRANS WHERE TRANSTP='IORD'  AND BILLDT='" + transDate + "' AND PSID='"+ iob.PartyId + "' GROUP BY COMPID, PSID, BILLDT, ITEMID ORDER BY PSID, BILLDT, ITEMID", con);
                            SqlDataReader dr1 = cmd1.ExecuteReader();
                            foreach (var item1 in dr1)
                            {

                                // String TRANSSL = dr1["TRANSSL"].ToString();
                                String ITEMID = dr1["ITEMID"].ToString();
                                String QTY = dr1["QTY"].ToString();
                              //  String BILLDT1 = dr1["BILLDT"].ToString();
                                // String RETRT = dr1["RETRT"].ToString();
                                // String NETRT = dr1["RETRT"].ToString();
                                // String NETAMT = dr1["NETAMT"].ToString();
                                // String AMOUNT = dr1["AMOUNT"].ToString();
                                //  String REMARKSd = dr1["REMARKS"].ToString();

                                iob.CompanyId = "101";
                                iob.TransTP = "SALE";
                                
                           
                                iob.MonthYear = varYear;
                                iob.TransNo = Convert.ToInt64(Trans);
                                iob.ItemID = ITEMID;

                                dbFunctions.lblAdd("SELECT MAX(TRANSSL)+1 FROM STK_TRANS WHERE TRANSYY ='" + varYear + "' AND TRANSNO ='" + iob.TransNo + "' and TRANSTP='Sale'", lblTransSLItem);
                                if (lblTransSLItem.Text == "")
                                {
                                    lblTransSLItem.Text = "1";
                                }

                                dbFunctions.lblAdd("SELECT SALRT FROM STK_ITEM WHERE ITEMID ='" + iob.ItemID + "'", lblItemRate);

                                iob.TransSL = lblTransSLItem.Text;

                                iob.Orderqty = Convert.ToDecimal(QTY);
                                iob.Rate = Convert.ToDecimal(lblItemRate.Text);


                                string itid = iob.ItemID.Substring(0, 5);


                                if (iob.PartyId == "20470")
                                {
                                    iob.RetRt = Convert.ToDecimal("0");

                                }//1,5 in 10108,10110 
                                else if (itid == "10108" || itid == "10110")
                                {
                                    iob.RetRt = Convert.ToDecimal("0");
                                }//1,5 in 10109
                                else if (itid == "10109")
                                {
                                    iob.RetRt = Convert.ToDecimal("8");
                                }
                                else
                                {
                                    iob.RetRt = Convert.ToDecimal("20");
                                }
                                {

                                }

                                //  decimal Rate = Convert.ToDecimal(txtRateFooter.Text);
                                // var txtperRTFooter = (TextBox)gv_details.FooterRow.FindControl("txtperRTFooter");
                                // decimal Peret = Convert.ToDecimal(txtperRTFooter.Text);

                                iob.netRt = Convert.ToDecimal((iob.Rate - ((iob.Rate * iob.RetRt) / 100)).ToString("f2"));

                                //   iob.Amount = Convert.ToDecimal(AMOUNT);
                                // iob.RetRt = Convert.ToDecimal("");
                                // iob.NetAmnt = Convert.ToDecimal("");
                                iob.RemarksDetails = "";
                                // iob.NetAmnt = Convert.ToDecimal(AMOUNT);
                               
                                iob.OrderDT = Convert.ToDateTime(TRANSDT);
                                iob.TrDt = Convert.ToDateTime(BILLDT);
                                dob.InsertSaleTransaction(iob);

                            }

                        }

                    }
                    dr.Close();
                    con.Close();
                    string userName = HttpContext.Current.Session["UserName"].ToString();

                    string serialNo = "";
                    int sl, serial;

                    // ShowGrid_Sale();
                    iob.BillDate = (DateTime.Parse(txtDate.Text, dateformat, System.Globalization.DateTimeStyles.AssumeLocal));
                    string trans_DT = iob.BillDate.ToString("yyyy/MM/dd");
                    iob.Username = userName;

                    #region
                    //==============================================================================
                    //STK_TRANS PROCESS//
                    //=============================================================================

                    //foreach (GridViewRow grid in GridView3.Rows)
                    //{
                    //    try
                    //    {

                    //            dbFunctions.lblAdd(@"Select max(SERIALNO) FROM  STK_TRANS where TRANSDT = '" + trans_DT + "' and TRANSTP = 'SALE' and TABLEID='GL_MTRANS'", lblSerial_Mrec);
                    //            if (lblSerial_Mrec.Text == "")
                    //            {
                    //                serialNo = "5000";
                    //                iob.TransSL = serialNo;
                    //            }
                    //            else
                    //            {
                    //                sl = int.Parse(lblSerial_Mrec.Text);
                    //                serial = sl + 1;

                    //                iob.TransSL = serial.ToString();
                    //            }

                    //            iob.TransTP = grid.Cells[0].Text;
                    //            iob.MonthYear = grid.Cells[2].Text;
                    //            iob.TransNo = Convert.ToInt64(grid.Cells[3].Text);
                    //             iob.Amount = Convert.ToDecimal(grid.Cells[12].Text);
                    //            string Remarks = grid.Cells[13].Text;
                    //            if (Remarks == "&nbsp;")
                    //            {
                    //                iob.Remarks = "";
                    //            }
                    //            else
                    //                iob.Remarks = Remarks;
                    //        //    dob.doProcess_MREC_Multiple(iob);

                    //    }
                    //    catch (Exception ex)
                    //    {
                    //        Response.Write(ex.Message);
                    //    }
                    //}
                    #endregion
                    // Sale_process();
                    Response.Write("<script>alert('Process Completed.');</script>");
                }

            }
            else
            {
                Response.Write("<script>alert('This Process Already Execute!!!');</script>");
            }

        }


        public void ShowGrid_Sale()
        {

            DateTime Pdate = DateTime.Parse(txtDate.Text, dateformat, System.Globalization.DateTimeStyles.AssumeLocal);
            string p_Date = Pdate.ToString("yyyy/MM/dd");

            string connectionString = ConfigurationManager.ConnectionStrings["AslDbContext"].ConnectionString;
            SqlConnection conn = new SqlConnection(connectionString);

            conn.Open();
            //SqlCommand cmd = new SqlCommand(" SELECT TRANSTP, TRANSDT, TRANSMY, TRANSNO, STOREFR, STORETO, PSID, LCTP, LCID, LCDATE, REMARKS_P, sum(AMOUNT)as AMOUNT from STK_TRANS where TRANSDT='" + p_Date + "' and TRANSTP='SALE'" +
            //                                " group by TRANSTP, TRANSDT, TRANSMY, TRANSNO, STOREFR, STORETO, PSID, LCTP, LCID, LCDATE, REMARKS_P", conn);
            SqlCommand cmd = new SqlCommand($@"SELECT STK_TRANS.COMPID, STK_TRANS.TRANSTP, STK_TRANS.TRANSDT, STK_TRANS.TRANSYY, STK_TRANS.TRANSNO, STK_TRANS.PSID, STK_TRANS.PSSL, STK_TRANS.BILLDT, STK_TRANS.STOREFR, 
            STK_TRANS.STORETO, STK_TRANS.TRANSSL, STK_TRANS.ITEMID, STK_TRANS.QTY, STK_TRANS.RATE,STK_TRANS.RETRT,STK_TRANS.NETRT,STK_TRANS.NETAMT, STK_TRANS.AMOUNT, STK_TRANS.REMARKS, STK_ITEM.ITEMNM,  STK_ITEM.MUNIT
            FROM STK_TRANS INNER JOIN
            STK_ITEM ON STK_TRANS.ITEMID = STK_ITEM.ITEMID
            STK_TRANS.TRANSDT = '" + p_Date + "' AND (STK_TRANS.TRANSTP = 'IORD')", conn);

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
                    #region
                    //if (grid.Cells[0].Text == "SALE")
                    //{
                    //    dbFunctions.lblAdd(@"Select max(SERIALNO) FROM  STK_TRANS where TRANSDT = '" + trans_DT + "' and TRANSTP = 'JOUR' and SERIALNO like '12%'", lblSerial_SALE);
                    //    if (lblSerial_SALE.Text == "")
                    //    {
                    //        serialNo = "12000";
                    //        iob.Serial_SALE = serialNo;
                    //    }
                    //    else
                    //    {
                    //        sl = int.Parse(lblSerial_SALE.Text);
                    //        serial = sl + 1;

                    //        iob.Serial_SALE = serial.ToString();
                    //    }
                    #endregion
                    iob.TrDt = Convert.ToDateTime(trans_DT);
                    //  iob.vehicalsno = txtVehicalsNo.Text;
                    //   iob.driveerNm = txtdriverNM.Text;
                    // iob.asstNm = txtasstNM.Text;
                    iob.CompanyId = "101";
                    iob.TransTP = "SALE";
                    iob.TrDt = Convert.ToDateTime(grid.Cells[2].Text);
                    iob.MonthYear = grid.Cells[3].Text;
                    iob.TransNo = Convert.ToInt64(grid.Cells[4].Text);
                    iob.PsId = grid.Cells[5].Text;
                    iob.PartySerial = grid.Cells[6].Text;
                    iob.TrDt = Convert.ToDateTime(grid.Cells[7].Text);
                    iob.TransSL = grid.Cells[10].Text;
                    iob.ItemID = grid.Cells[11].Text;
                    iob.Qty = Convert.ToDecimal(grid.Cells[12].Text);
                    iob.Rate = Convert.ToDecimal(grid.Cells[13].Text);
                    iob.Amount = Convert.ToDecimal(grid.Cells[14].Text);
                    iob.RetRt = Convert.ToDecimal(grid.Cells[16].Text);
                    iob.netRt = Convert.ToDecimal(grid.Cells[17].Text);
                    iob.NetAmnt = Convert.ToDecimal(grid.Cells[18].Text);


                    dob.InsertSaleTransaction(iob);
                    // }
                }
                catch (Exception ex)
                {
                    Response.Write(ex.Message);
                }
            }
        }

        public string TransNumber(string TransY)
        {
          
            string Trans = "";
            Label lblSMxNo = new Label();
            dbFunctions.lblAdd(@"Select max(TRANSNO) FROM STK_TRANSMST where TRANSYY='" + TransY + "' and TRANSTP = 'SALE'", lblSMxNo);
            if (lblSMxNo.Text == "")
            {
                Trans = StockTransaction.SaleTransactionNo(TransY);
            }
            else
            {
                int iNo = int.Parse(lblSMxNo.Text);
                int totIno = iNo + 1;
                Trans = totIno.ToString();
            }
            return Trans;
        }



    }
}