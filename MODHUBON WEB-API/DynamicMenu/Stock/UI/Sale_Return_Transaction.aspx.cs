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
    public partial class Sale_Return_Transaction : System.Web.UI.Page
    {
        IFormatProvider dateformat = new System.Globalization.CultureInfo("fr-FR", true);
        StockDataAcces dob = new StockDataAcces();
        StockInterface iob = new StockInterface();
        protected void Page_Load(object sender, EventArgs e)
        {
            if (Session["USERID"] == null)
                Response.Redirect("~/Login/UI/Login.aspx");
            else
            {
                const string formLink = "/Stock/UI/Sale_Return_Transaction.aspx";
                bool permission = UserPermissionChecker.checkParmit(formLink, "STATUS");
                if (permission == true)
                {
                    if (!IsPostBack)
                    {
                        DateTime date = dbFunctions.Timezone(DateTime.Now);
                        txtTransDate.Text = date.ToString("dd-MM-yyyy");
                        txtTransactionNo.Text = StockTransaction.SaleReturnTransactionNo(date.Year.ToString());


                        Start();

                        dbFunctions.DropDownAddSelectTextWithValue(txtPartyName, "SELECT PARTYNM,PARTYID FROM STK_PARTY");

                        //    dbFunctions.dropDownAddWithSelect(txtBehicalsNo, "SELECT VEHICLENO FROM STK_TRANSMST");
                        //  dbFunctions.dropDownAddWithSelect(txtdriverNM, "SELECT DRIVERNM FROM STK_TRANSMST");
                        //   dbFunctions.dropDownAddWithSelect(txtasstNM, "SELECT ASSTNM FROM STK_TRANSMST");



                        ShowGrid();

                    }

                }
                else
                {
                    Response.Redirect("../../DashBoard/UI/Default.aspx");
                }
            }

        }


        public void Start()
        {
            //if (TabContainer1.ActiveTabIndex == 0)
            //{
            DateTime today = DateTime.Today.Date;
            string td = dbFunctions.Dayformat(today);
            txtTransDate.Text = td;
            DateTime transdate = DateTime.Parse(txtTransDate.Text, dateformat, System.Globalization.DateTimeStyles.AssumeLocal);
            string TrDate = transdate.ToString("yyyy/MM/dd");

            string mon = DateTime.Today.Date.ToString("MMM").ToUpper();
            string varYear = today.ToString("yyyy");
            string year = today.ToString("yy");
            //lblSMY.Text = mon + "-" + year;
            lblSMY.Text = varYear;
            dbFunctions.lblAdd(@"Select max(TRANSNO) FROM STK_TRANSMST where TRANSYY='" + lblSMY.Text + "' and TRANSTP = 'IRTS'", lblSMxNo);
            if (lblSMxNo.Text == "")
            {
                txtTransactionNo.Text = txtTransactionNo.Text;
            }
            else
            {
                int iNo = int.Parse(lblSMxNo.Text);
                int totIno = iNo + 1;
                txtTransactionNo.Text = totIno.ToString();
            }

            // txtSLMNo.Focus();

            ShowGrid();
            //}
        }

        protected void ShowGrid()
        {
            DateTime date = DateTime.Parse(txtTransDate.Text, dateformat, System.Globalization.DateTimeStyles.AssumeLocal);
            string transDate = date.ToString("yyyy-MM-dd");
            SqlConnection conn = new SqlConnection(dbFunctions.connection);
            conn.Open();

            string TransNO;
            //  iob.TransNo2 =  ? txtTransactionNo.Text : ddlTransactionNo.Text;
            if (btnSaleEdit.Text == "EDIT")
            {
                TransNO = txtTransactionNo.Text;
            }
            else
            {
                TransNO = ddlTransactionNo.Text;
            }

            SqlCommand cmd = new SqlCommand(@"SELECT        STK_TRANS.COMPID, STK_TRANS.TRANSTP, STK_TRANS.TRANSDT, STK_TRANS.TRANSYY, STK_TRANS.TRANSNO, STK_TRANS.PSID, STK_TRANS.PSSL, STK_TRANS.BILLDT, STK_TRANS.STOREFR, 
            STK_TRANS.STORETO, STK_TRANS.TRANSSL, STK_TRANS.ITEMID, STK_TRANS.QTY, STK_TRANS.RATE,STK_TRANS.RETTP,STK_TRANS.RETRT,STK_TRANS.NETRT,STK_TRANS.NETAMT, STK_TRANS.AMOUNT, STK_TRANS.REMARKS, STK_ITEM.ITEMNM,  STK_ITEM.MUNIT
            FROM STK_TRANS INNER JOIN
            STK_ITEM ON STK_TRANS.ITEMID = STK_ITEM.ITEMID
            WHERE (STK_TRANS.TRANSNO = '" + TransNO + "') AND (STK_TRANS.TRANSDT = '" + transDate +
            "') AND (STK_TRANS.TRANSTP = 'IRTS')", conn);
            SqlDataAdapter da = new SqlDataAdapter(cmd);
            DataSet ds = new DataSet();
            da.Fill(ds);
            conn.Close();
            if (ds.Tables[0].Rows.Count > 0)
            {
                gv_details.DataSource = ds;
                gv_details.DataBind();




                DropDownList txtItemNameFooter = (DropDownList)gv_details.FooterRow.FindControl("txtItemNameFooter");
                txtItemNameFooter.Focus();
            }
            else
            {
                ds.Tables[0].Rows.Add(ds.Tables[0].NewRow());
                gv_details.DataSource = ds;
                gv_details.DataBind();
                int columncount = gv_details.Rows[0].Cells.Count;
                gv_details.Rows[0].Cells.Clear();
                gv_details.Rows[0].Cells.Add(new TableCell());
                gv_details.Rows[0].Cells[0].ColumnSpan = columncount;
                gv_details.Rows[0].Cells[0].Text = "No Records Found";


                DropDownList txtItemNameFooter = (DropDownList)gv_details.FooterRow.FindControl("txtItemNameFooter");
                txtItemNameFooter.Focus();
            }
            var txtItemNameFooters = (DropDownList)gv_details.FooterRow.FindControl("txtItemNameFooter");
            var txtItemNameEdit = (DropDownList)gv_details.FooterRow.FindControl("txtItemNameEdit");

            dbFunctions.DropDownAddSelectTextWithValue(txtItemNameFooters, "SELECT ITEMNM,ITEMID  FROM STK_ITEM ");
            dbFunctions.DropDownAddSelectTextWithValue(txtItemNameEdit, "SELECT ITEMNM,ITEMID  FROM STK_ITEM ");

            var data = dbFunctions.DataReaderAdd(@"SELECT ISNULL(SUM(AMOUNT),0) AS AMOUNT, ISNULL(SUM(NETAMT),0) AS NETAMT FROM STK_TRANS 
                WHERE (TRANSTP = 'IRTS') AND (TRANSDT = '" + transDate + "') AND (TRANSNO = '" + TransNO + "')");
            if (data.Count > 0)
            {
                lblamount.Text = data[0];
                lblTotalNetAmount.Text = data[1];
            }
        }

        protected void txtPerRTEdit_OnTextChanged(object sender, EventArgs e)
        {

            GridViewRow row = ((GridViewRow)((TextBox)sender).NamingContainer);


            var txtQtyEdit = (TextBox)row.FindControl("txtQtyEdit");



            var txtPerRTEdit = (TextBox)row.FindControl("txtPerRTEdit");
            decimal perrt = Convert.ToDecimal(txtPerRTEdit.Text);


            // var txtRateEdit = (TextBox)row.FindControl("lblUnitEdit");
            var txtnetretEdit = (TextBox)row.FindControl("txtnetretEdit");



            var txtnetAmntEdit = (TextBox)row.FindControl("txtnetAmntEdit");
            var txtRateEdit = (TextBox)row.FindControl("txtRateEdit");
            decimal Rate = Convert.ToDecimal(txtRateEdit.Text);




            txtnetretEdit.Text = (Rate - ((Rate * perrt) / 100)).ToString();

            decimal qty = Convert.ToDecimal(txtQtyEdit.Text);
            decimal Neret = Convert.ToDecimal(txtnetretEdit.Text);

            txtnetAmntEdit.Text = (Neret * qty).ToString();

        }

        protected void txtperRTFooter_OnTextChanged(object sender, EventArgs e)
        {


            var txtQtyFooter = (TextBox)gv_details.FooterRow.FindControl("txtQtyFooter");
            var txtRateFooter = (TextBox)gv_details.FooterRow.FindControl("txtRateFooter");
            decimal Rate = Convert.ToDecimal(txtRateFooter.Text);
            var txtperRTFooter = (TextBox)gv_details.FooterRow.FindControl("txtperRTFooter");
            decimal Peret = Convert.ToDecimal(txtperRTFooter.Text);
            var txtnetRetFoter = (TextBox)gv_details.FooterRow.FindControl("txtnetRetFoter");
            var txtnetamntFooter = (TextBox)gv_details.FooterRow.FindControl("txtnetamntFooter");

            //   txtnetRetFoter.Text = txtRateFooter.Text -((txtRateFooter.Text*txtperRTFooter.Text)/100);
            txtnetRetFoter.Text = (Rate - ((Rate * Peret) / 100)).ToString();

            decimal qty = Convert.ToDecimal(txtQtyFooter.Text);

            decimal Neret = Convert.ToDecimal(txtnetRetFoter.Text);
            txtnetamntFooter.Text = (Neret * qty).ToString();


        }

        protected void txtnetAmntEdit_OnTextChanged(object sender, EventArgs e)
        {
            GridViewRow row = ((GridViewRow)((TextBox)sender).NamingContainer);

            var txtQtyEdit = (TextBox)row.FindControl("txtQtyEdit");
            decimal qty = Convert.ToDecimal(txtQtyEdit.Text);

            var txtnetretEdit = (TextBox)row.FindControl("txtnetretEdit");
            decimal Neret = Convert.ToDecimal(txtnetretEdit.Text);
            var txtnetAmntEdit = (TextBox)row.FindControl("txtnetAmntEdit");

            txtnetAmntEdit.Text = (Neret * qty).ToString();

        }

        protected void txtnetamntFooter_OnTextChanged(object sender, EventArgs e)
        {

            var txtQtyFooter = (TextBox)gv_details.FooterRow.FindControl("txtQtyFooter");
            decimal qty = Convert.ToDecimal(txtQtyFooter.Text);
            var txtnetRetFoter = (TextBox)gv_details.FooterRow.FindControl("txtnetRetFoter");
            decimal Neret = Convert.ToDecimal(txtnetRetFoter.Text);
            var txtnetamntFooter = (TextBox)gv_details.FooterRow.FindControl("txtnetamntFooter");
            txtnetamntFooter.Text = (Neret * qty).ToString();
        }

        protected void txtQtyEdit_OnTextChanged(object sender, EventArgs e)
        {

            GridViewRow row = ((GridViewRow)((TextBox)sender).NamingContainer);


            var txtQtyEdit = (TextBox)row.FindControl("txtQtyEdit");
            decimal qty = Convert.ToDecimal(txtQtyEdit.Text);
            //      var txtItemNameEdit = (TextBox)row.FindControl("txtItemNameEdit");

            var txtPerRTEdit = (TextBox)row.FindControl("txtPerRTEdit");
            var lblItemCodeEdit = (TextBox)row.FindControl("lblItemCodeEdit");
            var lblAmountEdit = (TextBox)row.FindControl("lblAmountEdit");

            // var txtRateEdit = (TextBox)row.FindControl("lblUnitEdit");
            var txtnetretEdit = (TextBox)row.FindControl("txtnetretEdit");
            var txtnetAmntEdit = (TextBox)row.FindControl("txtnetAmntEdit");
            var txtRateEdit = (TextBox)row.FindControl("txtRateEdit");
            decimal rate = Convert.ToDecimal(txtRateEdit.Text);

            string itid = lblItemCodeEdit.Text.Substring(0, 5);
            //  lblAmountFooter.Text = txtQtyFooter.Text*txtRateFooter.Text;
            lblAmountEdit.Text = (qty * rate).ToString();

            if (txtPartyCode.Text == "20470")
            {
                txtPerRTEdit.Text = "0";

            }//1,5 in 10108,10110 
            else if (itid == "10108" || itid == "10110")
            {
                txtPerRTEdit.Text = "0";
            }//1,5 in 10109
            else if (itid == "10109")
            {
                txtPerRTEdit.Text = "8";
            }
            else
            {
                txtPerRTEdit.Text = "20";
            }
            {

            }
            decimal Rate = Convert.ToDecimal(txtRateEdit.Text);

            decimal Peret = Convert.ToDecimal(txtPerRTEdit.Text);

            txtnetretEdit.Text = (Rate - ((Rate * Peret) / 100)).ToString();
            decimal Neret = Convert.ToDecimal(txtnetretEdit.Text);

            txtnetAmntEdit.Text = (Neret * qty).ToString();

            txtPerRTEdit.Focus();
        }

        protected void txtQtyFooter_OnTextChanged(object sender, EventArgs e)
        {

            var lblItemCodeFooter = (DropDownList)gv_details.FooterRow.FindControl("txtItemNameFooter");
            var ddlRetTPFooter = (DropDownList)gv_details.FooterRow.FindControl("ddlRetTPFooter");
            var lblAmountFooter = (TextBox)gv_details.FooterRow.FindControl("lblAmountFooter");

            var txtQtyFooter = (TextBox)gv_details.FooterRow.FindControl("txtQtyFooter");
            decimal qty = Convert.ToDecimal(txtQtyFooter.Text);
            var txtRateFooter = (TextBox)gv_details.FooterRow.FindControl("txtRateFooter");
            decimal rate = Convert.ToDecimal(txtRateFooter.Text);
            // var txtperRTFooter = (TextBox)gv_details.FooterRow.FindControl("txtperRTFooter");
            var txtnetRetFoter = (TextBox)gv_details.FooterRow.FindControl("txtnetRetFoter");
            var txtnetamntFooter = (TextBox)gv_details.FooterRow.FindControl("txtnetamntFooter");

            var txtperRTFooter = (TextBox)gv_details.FooterRow.FindControl("txtperRTFooter");

            string itid = lblItemCodeFooter.Text.Substring(0, 5);
            //  lblAmountFooter.Text = txtQtyFooter.Text*txtRateFooter.Text;
            lblAmountFooter.Text = (qty * rate).ToString("f2");

            if (txtPartyCode.Text == "20470")
            {
                txtperRTFooter.Text = "0";

            }//1,5 in 10108,10110 
            else if (itid == "10108" || itid == "10110")
            {
                txtperRTFooter.Text = "0";
            }//1,5 in 10109
            else if (itid == "10109")
            {
                txtperRTFooter.Text = "8";
            }
            else
            {
                txtperRTFooter.Text = "20";
            }
            {

            }

            decimal Rate = Convert.ToDecimal(txtRateFooter.Text);
            decimal Peret = Convert.ToDecimal(txtperRTFooter.Text);

            txtnetRetFoter.Text = (Rate - ((Rate * Peret) / 100)).ToString("f2");
            decimal Neret = Convert.ToDecimal(txtnetRetFoter.Text);

            txtnetamntFooter.Text = (Neret * qty).ToString("f2");

            ddlRetTPFooter.Focus();
        }

        protected void txtItemNameFooter_OnSelectedIndexChanged(object sender, EventArgs e)
        {


            var txtItemNameFooter = (DropDownList)gv_details.FooterRow.FindControl("txtItemNameFooter");
            var txtQtyFooter = (TextBox)gv_details.FooterRow.FindControl("txtQtyFooter");
            var txtRateFooter = (TextBox)gv_details.FooterRow.FindControl("txtRateFooter");
            var lblAmountFooter = (TextBox)gv_details.FooterRow.FindControl("lblAmountFooter");
            var lblUnitFooter = (Label)gv_details.FooterRow.FindControl("lblUnitFooter");
            var txtperRTFooter = (TextBox)gv_details.FooterRow.FindControl("txtperRTFooter");
            var txtnetRetFoter = (TextBox)gv_details.FooterRow.FindControl("txtnetRetFoter");
            var txtnetamntFooter = (TextBox)gv_details.FooterRow.FindControl("txtnetamntFooter");

            var lblItemCodeFooter = (DropDownList)gv_details.FooterRow.FindControl("txtItemNameFooter");
            string itid = lblItemCodeFooter.Text.Substring(0, 5);
            //   dbFunctions.txtAdd(@"Select QTY from STK_ITEM where ITEMNM = '" + txtItemNameFooter.Text + "'", txtQtyFooter);

            dbFunctions.lblAdd(@"Select MUNIT from STK_ITEM where ITEMID = '" + txtItemNameFooter.Text + "'", lblUnitFooter);
            dbFunctions.txtAdd(@"Select SALRT from STK_ITEM where ITEMID = '" + txtItemNameFooter.Text + "'", txtRateFooter);

            decimal qty = Convert.ToDecimal(txtQtyFooter.Text);
            decimal rate = Convert.ToDecimal(txtRateFooter.Text);
            if (txtItemNameFooter.Text == "")
            {
                lblGridMSG.Visible = true;
                lblGridMSG.Text = "Please Item Particulars shouldn't be Empty";
                txtItemNameFooter.Focus();
            }





            lblAmountFooter.Text = (qty * rate).ToString("f2");

            if (txtPartyCode.Text == "20470")
            {
                txtperRTFooter.Text = "0";

            }//1,5 in 10108,10110 
            else if (itid == "10108" || itid == "10110")
            {
                txtperRTFooter.Text = "0";
            }//1,5 in 10109
            else if (itid == "10109")
            {
                txtperRTFooter.Text = "8";
            }
            else
            {
                txtperRTFooter.Text = "20";
            }
            {

            }

            decimal Rate = Convert.ToDecimal(txtRateFooter.Text);
            decimal Peret = Convert.ToDecimal(txtperRTFooter.Text);

            txtnetRetFoter.Text = (Rate - ((Rate * Peret) / 100)).ToString("f2");

            decimal Neret = Convert.ToDecimal(txtnetRetFoter.Text);

            txtnetamntFooter.Text = (Neret * qty).ToString("f2");

            txtQtyFooter.Focus();


        }

        protected void btnSaleEdit_OnClick(object sender, EventArgs e)
        {
            if (btnSaleEdit.Text == "EDIT")
            {
                ddlTransactionNo.Visible = true;
                DateTime transdate = DateTime.Parse(txtTransDate.Text, dateformat, System.Globalization.DateTimeStyles.AssumeLocal);
                string TrDate = transdate.ToString("yyyy/MM/dd");
                string TrMonth = transdate.ToString("yyyy");
                dbFunctions.dropDownAddWithSelect(ddlTransactionNo, "SELECT DISTINCT TRANSNO FROM STK_TRANS WHERE TRANSDT ='" + TrDate + "' AND TRANSYY='" + TrMonth + "' and TRANSTP='IRTS'");


                txtTransactionNo.Visible = false;
                btnSaleEdit.Text = "NEW";


                txtPartyName.Enabled = false;
                txtTransDate.Enabled = true;
                btncomplete.Visible = true;
                //btnPrint.Visible = true;
                //btnDoPrint.Visible = true;
                txtPartyName.SelectedIndex = -1;
                txtRemarks.Text = "";
                txtVehicalsNo.Text = "";
                txtdriverNM.Text = "";
                txtasstNM.Text = "";

                ShowGrid();
            }
            else
            {

                txtTransactionNo.Visible = true;
                btnSaleEdit.Text = "EDIT";
                txtPartyName.Enabled = true;
                //btnPrint.Visible = false;
                //btnDoPrint.Visible = false;
                ddlTransactionNo.Visible = false;

                txtPartyName.SelectedIndex = -1;
                txtRemarks.Text = "";
                txtVehicalsNo.Text = "";
                txtdriverNM.Text = "";
                txtasstNM.Text = "";


                Start();
            }
        }

        protected void gv_details_OnRowDataBound(object sender, GridViewRowEventArgs e)
        {

            if (e.Row.RowType == DataControlRowType.Footer)
            {
                if (btnSaleEdit.Text == "EDIT")
                {
                    if (txtTransactionNo.Text == "")
                        iob.TransNo = 0;
                    else
                        iob.TransNo = Convert.ToInt32(txtTransactionNo.Text);
                }
                else
                {
                    if (ddlTransactionNo.Text == "--SELECT--" || ddlTransactionNo.Text == "")
                    {
                        iob.TransNo = 0;
                    }
                    else
                        iob.TransNo = Convert.ToInt32(ddlTransactionNo.Text);
                }

                dbFunctions.lblAdd("SELECT MAX(TRANSSL) FROM STK_TRANS WHERE TRANSYY ='" + lblSMY.Text + "' AND TRANSNO ='" + iob.TransNo + "' and TRANSTP='IRTS'", lblTransSL);
                //dbFunctions.lblAdd(@"select MAX(TRANSSL) from STK_TRANS where TRANSTP = 'SALE' and TRANSMY = '" + lblSMY.Text + "'", lblTransSL);
                int sl, fSl = 0;

                if (lblTransSL.Text == "")
                {
                    e.Row.Cells[0].Text = "1";
                }
                else
                {
                    sl = Convert.ToInt16(lblTransSL.Text);
                    fSl = sl + 1;

                    e.Row.Cells[0].Text = fSl.ToString();
                }
                // 
            }

        }


        private string CkeckTransNO(string TransMY, Int64 TransNO)
        {
            string result = "false";
            SqlConnection conn = new SqlConnection(dbFunctions.connection);
            conn.Open();
            string script = "SELECT * FROM STK_TRANSMST WHERE TRANSYY ='" + TransMY + "' AND TRANSNO =" + TransNO + " and TRANSTP='IRTS'";
            SqlCommand cmd = new SqlCommand(script, conn);
            SqlDataAdapter da = new SqlDataAdapter(cmd);
            DataSet ds = new DataSet();
            da.Fill(ds);
            conn.Close();
            if (ds.Tables[0].Rows.Count < 1)
            {
                result = "true";
            }
            return result;
        }
        public void check_Invoice_No()
        {
            lblSMxNo.Text = "";
            dbFunctions.lblAdd("SELECT MAX(TRANSNO) AS TRANSNO FROM STK_TRANSMST WHERE TRANSYY ='" + iob.MonthYear + "' and TRANSTP = 'IRTS'", lblcheckMSTNO);
            if (lblcheckMSTNO.Text == "")
            {
                txtTransactionNo.Text = txtTransactionNo.Text;
            }
            else
            {
                Int64 trns, ftrns = 0;
                trns = Convert.ToInt64(lblcheckMSTNO.Text);
                ftrns = trns + 1;
                txtTransactionNo.Text = ftrns.ToString();
            }
        }

        protected void gv_details_OnRowCommand(object sender, GridViewCommandEventArgs e)
        {

            if (e.CommandName.Equals("SaveCon") || e.CommandName.Equals("Complete"))
            {
                if (Session["USERID"] == null)
                {
                    Response.Redirect("~/Login/UI/Login.aspx");
                }
                else
                {
                    DateTime TransDT = DateTime.Parse(txtTransDate.Text, dateformat, System.Globalization.DateTimeStyles.AssumeLocal);
                    string TrDt = TransDT.ToString("yyyy/MM/dd");
                    iob.UserID = HttpContext.Current.Session["USERID"].ToString();
                    iob.Ipaddress = HttpContext.Current.Session["IpAddress"].ToString();
                    iob.UserPC = HttpContext.Current.Session["PCName"].ToString();
                    iob.ITime = dbFunctions.Timezone(DateTime.Now);
                    string serial = gv_details.FooterRow.Cells[0].Text;
                    var txtItemNameFooter = (DropDownList)gv_details.FooterRow.FindControl("txtItemNameFooter");
                    var ddlRetTPFooter = (DropDownList)gv_details.FooterRow.FindControl("ddlRetTPFooter");
                    var txtQtyFooter = (TextBox)gv_details.FooterRow.FindControl("txtQtyFooter");
                    var txtRateFooter = (TextBox)gv_details.FooterRow.FindControl("txtRateFooter");
                    var txtperRTFooter = (TextBox)gv_details.FooterRow.FindControl("txtperRTFooter");
                    var txtnetRetFoter = (TextBox)gv_details.FooterRow.FindControl("txtnetRetFoter");
                    var txtnetamntFooter = (TextBox)gv_details.FooterRow.FindControl("txtnetamntFooter");




                    var txtRemarksFooter = (TextBox)gv_details.FooterRow.FindControl("txtRemarksFooter");

                    iob.ItemSerial = Convert.ToInt32(serial);

                    if (txtItemNameFooter.Text == "")
                    {
                        lblMSG.Visible = true;
                        lblMSG.Text = "Select Item Name";
                        txtItemNameFooter.Focus();
                    }
                    else if (txtPartyName.Text == "")
                    {
                        lblMSG.Visible = true;
                        lblMSG.Text = "Select Party Name";
                        txtPartyName.Text = "";
                        txtPartyName.Focus();
                    }
                    else
                    {
                        lblGridMSG.Visible = false;
                        lblMSG.Visible = false;

                        if (txtRateFooter.Text == "")
                        {
                            txtRateFooter.Text = "0";
                        }
                        iob.TrDt = DateTime.Parse(txtTransDate.Text, dateformat,
                        System.Globalization.DateTimeStyles.AssumeLocal);
                        iob.MonthYear = iob.TrDt.Year.ToString();

                        if (btnSaleEdit.Text == "EDIT")
                        {
                            dbFunctions.lblAdd(
                                "SELECT COUNT(*) FROM STK_TRANSMST WHERE TRANSYY ='" + iob.MonthYear + "' AND TRANSNO='" +
                                txtTransactionNo.Text + "' and TRANSTP='IRTS'", lbltransNomst);

                            if (Convert.ToInt16(lbltransNomst.Text) == 0)
                            {
                                iob.TransNo = Convert.ToInt32(txtTransactionNo.Text);
                            }
                            else
                            {
                                dbFunctions.lblAdd(
                                    "SELECT COUNT(*) FROM STK_TRANS WHERE TRANSYY ='" + lblSMY.Text + "' AND TRANSNO='" + txtTransactionNo.Text + "' and TRANSTP='IRTS'", lblTransSL);
                                if (Convert.ToInt16(lblTransSL.Text) == 0)
                                {
                                    check_Invoice_No();
                                    iob.TransNo = Convert.ToInt32(txtTransactionNo.Text);
                                }
                                else
                                {
                                    if (iob.ItemSerial == 1)
                                    {
                                        check_Invoice_No();
                                        iob.TransNo = Convert.ToInt32(txtTransactionNo.Text);
                                    }
                                    else
                                    {
                                        iob.TransNo = Convert.ToInt32(txtTransactionNo.Text);
                                    }


                                }

                            }
                        }
                        else
                        {
                            iob.TransNo = Convert.ToInt32(ddlTransactionNo.Text);
                        }




                        //    iob.BillDate = DateTime.Parse(txtBillDate.Text, dateformat,
                        //   System.Globalization.DateTimeStyles.AssumeLocal);
                        iob.TrDt = Convert.ToDateTime(TrDt);
                        iob.vehicalsno = txtVehicalsNo.Text;
                        iob.driveerNm = txtdriverNM.Text;
                        iob.asstNm = txtasstNM.Text;
                        iob.CompanyId = "101";
                        iob.ItemID = txtItemNameFooter.Text;
                        iob.Qty = Convert.ToDecimal(txtQtyFooter.Text);
                        iob.Rate = Convert.ToDecimal(txtRateFooter.Text);
                        iob.Amount = iob.Qty * iob.Rate;
                        iob.rettp = ddlRetTPFooter.Text;
                        iob.TransTP = "IRTS";
                        iob.PartyId = txtPartyName.SelectedValue;
                        iob.PartySerial = StockTransaction.SaleReturnPartySerial(iob.TrDt.Year.ToString(), iob.PartyId);
                        iob.RetRt = Convert.ToDecimal(txtperRTFooter.Text);
                        iob.netRt = Convert.ToDecimal(txtnetRetFoter.Text);
                        iob.NetAmnt = Convert.ToDecimal(txtnetamntFooter.Text);
                        iob.Remarks = txtRemarks.Text;
                        iob.RemarksDetails = txtRemarksFooter.Text;
                        iob.TransSL = serial;

                        if (e.CommandName.Equals("SaveCon"))
                        {
                            string Check = CkeckTransNO(iob.MonthYear, iob.TransNo);
                            if (Check == "true")
                            {
                                check_Invoice_No();
                                iob.TransNo = Convert.ToInt32(txtTransactionNo.Text);
                                dob.InsertSaleReturnTransactionMaster(iob);
                                dob.InsertSaleReturnTransaction(iob);
                                ShowGrid();
                            }
                            else
                            {
                                dob.InsertSaleReturnTransaction(iob);

                                ShowGrid();

                            }


                            dbFunctions.DropDownAddSelectTextWithValue(txtItemNameFooter, "SELECT ITEMNM,ITEMID  FROM STK_ITEM ");


                            // txtJobNo.Focus();
                        }
                        else if (e.CommandName.Equals("Complete"))
                        {

                            string Check = CkeckTransNO(iob.MonthYear, iob.TransNo);
                            if (Check == "true")
                            {
                                check_Invoice_No();
                                iob.TransNo = Convert.ToInt32(txtTransactionNo.Text);
                                dob.InsertSaleReturnTransactionMaster(iob);
                                dob.InsertSaleReturnTransaction(iob);
                            }
                            else
                            {
                                dob.InsertSaleReturnTransaction(iob);
                            }



                            ddlTransactionNo.SelectedIndex = -1;
                            txtVehicalsNo.Text = "";
                            txtdriverNM.Text = "";
                            txtasstNM.Text = "";
                            txtRemarks.Text = "";
                            txtPartyName.SelectedIndex = -1;
                            ShowGrid();


                            //  gv_details.ShowFooter = false;
                        } // else if (e.CommandName.Equals("Complete"))
                          // {




                    }
                }
            }
            // }

        }

        protected void gv_details_OnRowEditing(object sender, GridViewEditEventArgs e)
        {


            if (Session["USERID"] == null)
            {
                Response.Redirect("~/Login/UI/Login.aspx");
            }
            else
            {
                gv_details.EditIndex = e.NewEditIndex;
                ShowGrid();



                Label lblitemidedit = (Label)gv_details.Rows[e.NewEditIndex].FindControl("lblitemidedit");
                DropDownList txtItemNameEdit = (DropDownList)gv_details.Rows[e.NewEditIndex].FindControl("txtItemNameEdit");
                dbFunctions.DropDownAddSelectTextWithValue(txtItemNameEdit, "SELECT ITEMNM,ITEMID  FROM STK_ITEM ");

                txtItemNameEdit.Text = lblitemidedit.Text;
                TextBox txtQtyEdit = (TextBox)gv_details.Rows[e.NewEditIndex].FindControl("txtQtyEdit");
                txtQtyEdit.Focus();
            }
        }

        protected void gv_details_OnRowCancelingEdit(object sender, GridViewCancelEditEventArgs e)
        {


            if (Session["USERID"] == null)
            {
                Response.Redirect("~/Login/UI/Login.aspx");
            }
            else
            {
                gv_details.EditIndex = -1;
                ShowGrid();
            }

        }

        protected void gv_details_OnRowDeleting(object sender, GridViewDeleteEventArgs e)
        {

            if (Session["USERID"] == null)
            {
                Response.Redirect("~/Login/UI/Login.aspx");
            }
            else
            {
                iob.TrDt = DateTime.Parse(txtTransDate.Text, dateformat, System.Globalization.DateTimeStyles.AssumeLocal);

                var lblCOMPID = (Label)gv_details.Rows[e.RowIndex].FindControl("lblCOMPID");
                var lblItemid = (Label)gv_details.Rows[e.RowIndex].FindControl("lblItemid");
                var lblItemSl = (Label)gv_details.Rows[e.RowIndex].FindControl("lblItemSl");

                iob.MonthYear = iob.TrDt.Year.ToString();
                iob.ItemID = lblItemid.Text;
                iob.TransSL = lblItemSl.Text;
                iob.CompanyId = lblCOMPID.Text;
                iob.TransTP = "IRTS";

                iob.TransNo = btnSaleEdit.Text == "EDIT"
                    ? Convert.ToInt64(txtTransactionNo.Text)
                    : Convert.ToInt64(ddlTransactionNo.Text);
                var result = dob.DeleteSaleReturnTransaction(iob);
                var count = dbFunctions.StringData(@"SELECT COUNT(*) FROM STK_TRANS WHERE
                        TRANSTP='IRTS' AND TRANSYY='" + iob.MonthYear + "' AND TRANSNO='" + iob.TransNo + "'");

                if (count == "0")
                {

                    dob.DeleteSaleReturnTransactionMaster(iob);
                    if (btnSaleEdit.Text == "EDIT")
                    {
                        dbFunctions.dropDownAddWithSelect(ddlTransactionNo, @"SELECT TRANSNO FROM STK_TRANSMST WHERE TRANSDT='" + iob.TrDt.ToString("yyyy-MM-dd") + "'");
                        ddlTransactionNo.Focus();
                    }
                    Refresh();
                }
                if (result == "")
                {
                    ShowGrid();
                }
            }



        }
        protected void Refresh()
        {
            txtPartyName.SelectedIndex = -1;
            //  txtPartyCode.Text = "";
            txtRemarks.Text = "";
            lblTotalNetAmount.Text = "0.00";
            lblamount.Text = "0.00";
            txtVehicalsNo.Text = "";
            txtdriverNM.Text = "";
            txtasstNM.Text = "";
            //    lblTotalQty.Text = "0.00";
        }


        protected void gv_details_OnRowUpdating(object sender, GridViewUpdateEventArgs e)
        {



            var txtQtyEdit = (TextBox)gv_details.Rows[e.RowIndex].FindControl("txtQtyEdit");
            var ddllRetTPEdit = (DropDownList)gv_details.Rows[e.RowIndex].FindControl("ddllRetTPEdit");
            //  var txtItemNameEdit = (DropDownList)gv_details.Rows[e.RowIndex].FindControl("txtItemNameEdit");
            var txtPerRTEdit = (TextBox)gv_details.Rows[e.RowIndex].FindControl("txtPerRTEdit");
            var txtnetretEdit = (TextBox)gv_details.Rows[e.RowIndex].FindControl("txtnetretEdit");
            var txtnetAmntEdit = (TextBox)gv_details.Rows[e.RowIndex].FindControl("txtnetAmntEdit");
            var txtRateEdit = (TextBox)gv_details.Rows[e.RowIndex].FindControl("txtRateEdit");
            var lblItemSlEdit = (Label)gv_details.Rows[e.RowIndex].FindControl("lblItemSlEdit");
            //var lblitemid = (Label)gv_details.Rows[e.RowIndex].FindControl("lblitemid");
            var txtItemNameEdit = (DropDownList)gv_details.Rows[e.RowIndex].FindControl("txtItemNameEdit");

            //   var txtRemarksEdit = (TextBox)gv_details.Rows[e.RowIndex].FindControl("txtRemarksEdit");

            if (txtPartyCode.Text == "")
            {
                lblMSG.Visible = true;
                lblMSG.Text = "Select Party Name";
                txtPartyName.Text = "";
                txtPartyName.Focus();
            }
            if (btnSaleEdit.Text == "NEW" && ddlTransactionNo.Text == "--SELECT--")
            {
                lblMSG.Visible = true;
                lblMSG.Text = "Select Transaction No";
                ddlTransactionNo.Text = "";
                ddlTransactionNo.Focus();
            }
            else
            {
                iob.UpdUserID = HttpContext.Current.Session["USERID"].ToString();
                iob.UPDIpaddress = HttpContext.Current.Session["IpAddress"].ToString();
                iob.UPDUserPC = HttpContext.Current.Session["PCName"].ToString();
                iob.UpdTime = dbFunctions.Timezone(DateTime.Now);

                if (Session["USERID"] == null)
                {
                    Response.Redirect("~/Login/UI/Login.aspx");
                }
                else
                {
                    lblGridMSG.Visible = false;
                    lblMSG.Visible = false;


                    iob.TrDt = DateTime.Parse(txtTransDate.Text, dateformat, System.Globalization.DateTimeStyles.AssumeLocal);
                    iob.MonthYear = iob.TrDt.Year.ToString();
                    iob.CompanyId = "101";

                    iob.ItemID = txtItemNameEdit.SelectedValue;
                    iob.TransSL = lblItemSlEdit.Text;
                    iob.rettp = ddllRetTPEdit.Text;
                    iob.RetRt = Convert.ToDecimal(txtPerRTEdit.Text);
                    iob.netRt = Convert.ToDecimal(txtnetretEdit.Text);
                    iob.NetAmnt = Convert.ToDecimal(txtnetAmntEdit.Text);
                    iob.TransTP = "IRTS";
                    iob.TransNo = btnSaleEdit.Text == "EDIT" ? Convert.ToInt64(txtTransactionNo.Text) : Convert.ToInt64(ddlTransactionNo.Text);


                    iob.Qty = Convert.ToDecimal(txtQtyEdit.Text);
                    iob.Rate = Convert.ToDecimal(txtRateEdit.Text);
                    iob.Amount = iob.Qty * iob.Rate;
                    iob.RemarksDetails = txtRemarks.Text;

                    var s = dob.UpdateSaleReturnTransaction(iob);
                    if (s == "")
                    {
                        gv_details.EditIndex = -1;
                        ShowGrid();
                    }
                }
            }

        }

        protected void txtItemNameEdit_OnSelectedIndexChanged(object sender, EventArgs e)
        {


            GridViewRow row = ((GridViewRow)((DropDownList)sender).NamingContainer);

            var txtQtyEdit = (TextBox)row.FindControl("txtQtyEdit");
            decimal qty = Convert.ToDecimal(txtQtyEdit.Text);
            DropDownList txtItemNameEdit = (DropDownList)row.FindControl("txtItemNameEdit");
            var lblUnitEdit = (Label)row.FindControl("lblUnitEdit");
            var txtRateEdit = (TextBox)row.FindControl("txtRateEdit");

            var txtPerRTEdit = (TextBox)row.FindControl("txtPerRTEdit");

            var txtnetretEdit = (TextBox)row.FindControl("txtnetretEdit");
            var txtnetAmntEdit = (TextBox)row.FindControl("txtnetAmntEdit");

            dbFunctions.lblAdd(@"Select MUNIT from STK_ITEM where ITEMID = '" + txtItemNameEdit.Text + "'", lblUnitEdit);
            dbFunctions.txtAdd(@"Select SALRT from STK_ITEM where ITEMID = '" + txtItemNameEdit.Text + "'", txtRateEdit);


            decimal rate = Convert.ToDecimal(txtRateEdit.Text);
            var lblAmountEdit = (TextBox)row.FindControl("lblAmountEdit");
            lblAmountEdit.Text = (qty * rate).ToString();

            if (txtItemNameEdit.Text == "")
            {
                lblGridMSG.Visible = true;
                lblGridMSG.Text = "Please Item Particulars shouldn't be Empty";
                txtItemNameEdit.Focus();
            }

            var lblItemCodeEdit = (TextBox)row.FindControl("lblItemCodeEdit");
            string itid = lblItemCodeEdit.Text.Substring(0, 5);
            //  lblAmountFooter.Text = txtQtyFooter.Text*txtRateFooter.Text;
            lblAmountEdit.Text = (qty * rate).ToString("f2");

            if (txtPartyCode.Text == "20470")
            {
                txtPerRTEdit.Text = "0";

            }//1,5 in 10108,10110 
            else if (itid == "10108" || itid == "10110")
            {
                txtPerRTEdit.Text = "0";
            }//1,5 in 10109
            else if (itid == "10109")
            {
                txtPerRTEdit.Text = "8";
            }
            else
            {
                txtPerRTEdit.Text = "20";
            }
            {

            }
            decimal Rate = Convert.ToDecimal(txtRateEdit.Text);
            decimal Peret = Convert.ToDecimal(txtPerRTEdit.Text);
            txtnetretEdit.Text = (Rate - ((Rate * Peret) / 100)).ToString("f2");
            decimal Neret = Convert.ToDecimal(txtnetretEdit.Text);

            txtnetAmntEdit.Text = (Neret * qty).ToString("f2");


            txtQtyEdit.Focus();


        }

        protected void txtTransDate_OnTextChanged(object sender, EventArgs e)
        {

            try
            {
                DateTime date = DateTime.Parse(txtTransDate.Text, dateformat, System.Globalization.DateTimeStyles.AssumeLocal);
                string transDate = date.ToString("yyyy-MM-dd");

                if (btnSaleEdit.Text == "EDIT")
                {
                    txtTransactionNo.Text = StockTransaction.SaleReturnTransactionNo(date.Year.ToString());
                    txtPartyName.Focus();
                }
                else
                {
                    dbFunctions.dropDownAddWithSelect(ddlTransactionNo, @"SELECT TRANSNO FROM STK_TRANSMST WHERE TRANSTP='IRTS' AND TRANSDT='" + transDate + "'");
                    ddlTransactionNo.Focus();
                }
                ShowGrid();

                // txtPartyName.Focus();
            }
            catch (Exception)
            {
                //ignore
            }

        }

        protected void ddlTransactionNo_OnSelectedIndexChanged(object sender, EventArgs e)
        {

            string connectionString = ConfigurationManager.ConnectionStrings["AslDbContext"].ConnectionString;
            SqlConnection con = new SqlConnection(connectionString);

            DateTime transdate = DateTime.Parse(txtTransDate.Text, dateformat, System.Globalization.DateTimeStyles.AssumeLocal);
            string TrDt = transdate.ToString("yyyy/MM/dd");
            lblSMY.Text = transdate.ToString("yyyy");

            if (ddlTransactionNo.Text == "--SELECT--")
            {
                gv_details.Visible = false;
                lblMSG.Visible = true;
                lblGridMSG.Text = "Type Invoice No.";
                lblamount.Text = "";
                lblTotalNetAmount.Text = "";
                //lblTotalQty.Text = "";
            }
            else
            {
                gv_details.Visible = true;
                lblGridMSG.Visible = false;
                Int64 TransNo = Convert.ToInt64(ddlTransactionNo.Text);
                dbFunctions.txtAdd("select PSID from STK_TRANSMST where TRANSTP='IRTS' and TRANSDT = '" + TrDt + "' and TRANSYY = '" + lblSMY.Text + "' and TRANSNO =" + TransNo + "", txtPartyCode);

                // dbFunctions.lblAdd("SELECT PARTYNM FROM STK_PARTY WHERE PARTYID='" + txtPartyCode.Text + "'", lblpartyID);
                txtPartyName.Text = txtPartyCode.Text;


                dbFunctions.txtAdd(@"select REMARKS from STK_TRANSMST where TRANSTP='IRTS' and TRANSDT = '" + TrDt + "' and TRANSYY = '" + lblSMY.Text + "' and TRANSNO =" + TransNo + "", txtRemarks);

                dbFunctions.txtAdd("select  VEHICLENO from STK_TRANSMST where TRANSTP='IRTS' and TRANSDT = '" + TrDt + "' and TRANSYY = '" + lblSMY.Text + "' and TRANSNO =" + TransNo + "", txtVehicalsNo);

                dbFunctions.txtAdd("select  DRIVERNM from STK_TRANSMST where TRANSTP='IRTS' and TRANSDT = '" + TrDt + "' and TRANSYY = '" + lblSMY.Text + "' and TRANSNO =" + TransNo + "", txtdriverNM);
                dbFunctions.txtAdd("select  ASSTNM from STK_TRANSMST where TRANSTP='IRTS' and TRANSDT = '" + TrDt + "' and TRANSYY = '" + lblSMY.Text + "' and TRANSNO =" + TransNo + "", txtasstNM);
                txtTransDate.Enabled = false;
                txtPartyName.Enabled = false;

                ShowGrid();

                var txtItemNameFooter = (DropDownList)gv_details.FooterRow.FindControl("txtItemNameFooter");
                var txtItemNameEdit = (DropDownList)gv_details.FooterRow.FindControl("txtItemNameEdit");


                dbFunctions.DropDownAddSelectTextWithValue(txtItemNameFooter, "SELECT ITEMNM,ITEMID  FROM STK_ITEM ");
                dbFunctions.DropDownAddSelectTextWithValue(txtItemNameEdit, "SELECT ITEMNM,ITEMID  FROM STK_ITEM ");

            }


        }

        protected void txtPartyName_OnSelectedIndexChanged(object sender, EventArgs e)
        {
            txtVehicalsNo.Focus();

        }

        protected void btncomplete_OnClick(object sender, EventArgs e)
        {

            string connectionString = ConfigurationManager.ConnectionStrings["AslDbContext"].ConnectionString;
            SqlConnection conn = new SqlConnection(connectionString);
            string userName = HttpContext.Current.Session["UserName"].ToString();

            DateTime transdate = DateTime.Parse(txtTransDate.Text, dateformat, System.Globalization.DateTimeStyles.AssumeLocal);
            string TrDate = transdate.ToString("yyyy/MM/dd");
            Int64 TransNo = Convert.ToInt64(ddlTransactionNo.Text);

            if (ddlTransactionNo.Text == "--SELECT--")
            {
                lblGridMSG.Visible = true;
                lblGridMSG.Text = "Select Invoice No.";
            }
            else
            {
                lblGridMSG.Visible = false;


                conn.Open();
                SqlCommand cmd1 = new SqlCommand("update STK_TRANSMST set VEHICLENO=" + txtVehicalsNo.Text + ", DRIVERNM=" + txtdriverNM.Text + ", ASSTNM = " + txtasstNM.Text + "," +
                                                 "  REMARKS='" + txtRemarks.Text + "' where TRANSTP = 'IRTS'  and TRANSYY='" + lblSMY.Text + "' and TRANSDT='" + TrDate + "' and TRANSNO = " + TransNo + "", conn);
                cmd1.ExecuteNonQuery();
                conn.Close();



                ///////Refresh/////
                ddlTransactionNo.SelectedIndex = -1;

                DateTime date = dbFunctions.Timezone(DateTime.Now);
                txtTransDate.Text = date.ToString("dd-MM-yyyy");
                txtTransactionNo.Text = StockTransaction.SaleReturnTransactionNo(date.Year.ToString());
                ShowGrid();
                //Up_Sales.Update();
                ddlTransactionNo.Focus();
            }
        }

        protected void ddlRetTPFooter_OnTextChanged(object sender, EventArgs e)
        {

            var lblItemCodeFooter = (DropDownList)gv_details.FooterRow.FindControl("txtItemNameFooter");
            var ddlRetTPFooter = (DropDownList)gv_details.FooterRow.FindControl("ddlRetTPFooter");
            //   var lblAmountFooter = (TextBox)gv_details.FooterRow.FindControl("lblAmountFooter");

            var txtQtyFooter = (TextBox)gv_details.FooterRow.FindControl("txtQtyFooter");
            decimal qty = Convert.ToDecimal(txtQtyFooter.Text);
            var txtRateFooter = (TextBox)gv_details.FooterRow.FindControl("txtRateFooter");
            decimal rate = Convert.ToDecimal(txtRateFooter.Text);
            // var txtperRTFooter = (TextBox)gv_details.FooterRow.FindControl("txtperRTFooter");
            var txtnetRetFoter = (TextBox)gv_details.FooterRow.FindControl("txtnetRetFoter");
            var txtnetamntFooter = (TextBox)gv_details.FooterRow.FindControl("txtnetamntFooter");

            var txtperRTFooter = (TextBox)gv_details.FooterRow.FindControl("txtperRTFooter");

            string itid = lblItemCodeFooter.Text.Substring(0, 5);
            //  lblAmountFooter.Text = txtQtyFooter.Text*txtRateFooter.Text;
            //  lblAmountFooter.Text = (qty * rate).ToString("f2");

            if (txtPartyCode.Text == "20470" && ddlRetTPFooter.Text == "GOOD")
            {
                txtperRTFooter.Text = "0";

            }//1,5 in 10108,10110 
            else if (itid == "10108" || itid == "10110" && ddlRetTPFooter.Text == "GOOD")
            {
                txtperRTFooter.Text = "0";
            }//1,5 in 10109
            else if (itid == "10109" && ddlRetTPFooter.Text == "GOOD")
            {
                txtperRTFooter.Text = "8";
            }
            else
            {
                txtperRTFooter.Text = "40";
            }
            {

            }

            decimal Rate = Convert.ToDecimal(txtRateFooter.Text);
            // var txtperRTFooter = (TextBox)gv_details.FooterRow.FindControl("txtperRTFooter");
            decimal Peret = Convert.ToDecimal(txtperRTFooter.Text);
            txtnetRetFoter.Text = (Rate - ((Rate * Peret) / 100)).ToString("f2");
            decimal Neret = Convert.ToDecimal(txtnetRetFoter.Text);

            txtnetamntFooter.Text = (Neret * qty).ToString("f2");

            txtperRTFooter.Focus();
        }

        protected void ddllRetTPEdit_OnTextChanged(object sender, EventArgs e)
        {


            GridViewRow row = ((GridViewRow)((TextBox)sender).NamingContainer);


            var txtQtyEdit = (TextBox)row.FindControl("txtQtyEdit");
            decimal qty = Convert.ToDecimal(txtQtyEdit.Text);
            //      var txtItemNameEdit = (TextBox)row.FindControl("txtItemNameEdit");

            var txtPerRTEdit = (TextBox)row.FindControl("txtPerRTEdit");
            var lblItemCodeEdit = (TextBox)row.FindControl("lblItemCodeEdit");
            //  var lblAmountEdit = (TextBox)row.FindControl("lblAmountEdit");
            var ddllRetTPEdit = (DropDownList)row.FindControl("ddllRetTPEdit");

            var txtnetretEdit = (TextBox)row.FindControl("txtnetretEdit");
            var txtnetAmntEdit = (TextBox)row.FindControl("txtnetAmntEdit");
            var txtRateEdit = (TextBox)row.FindControl("txtRateEdit");

            string itid = lblItemCodeEdit.Text.Substring(0, 5);
            //  lblAmountFooter.Text = txtQtyFooter.Text*txtRateFooter.Text;
            //  lblAmountEdit.Text = (qty * rate).ToString();

            if (txtPartyCode.Text == "20470" && ddllRetTPEdit.Text == "GOOD")
            {
                txtPerRTEdit.Text = "0";

            }//1,5 in 10108,10110 
            else if (itid == "10108" || itid == "10110" && ddllRetTPEdit.Text == "GOOD")
            {
                txtPerRTEdit.Text = "0";
            }//1,5 in 10109
            else if (itid == "10109" && ddllRetTPEdit.Text == "GOOD")
            {
                txtPerRTEdit.Text = "8";
            }
            else
            {
                txtPerRTEdit.Text = "40";
            }
            {

            }
            decimal Rate = Convert.ToDecimal(txtRateEdit.Text);
            decimal Peret = Convert.ToDecimal(txtPerRTEdit.Text);
            txtnetretEdit.Text = (Rate - ((Rate * Peret) / 100)).ToString();
            decimal Neret = Convert.ToDecimal(txtnetretEdit.Text);

            txtnetAmntEdit.Text = (Neret * qty).ToString();
        }

        protected void txtVehicalsNo_OnTextChanged(object sender, EventArgs e)
        {
            txtdriverNM.Focus();
        }
        protected void txtdriverNM_OnTextChanged(object sender, EventArgs e)
        {
            txtasstNM.Focus();
        }
        protected void txtasstNM_OnTextChanged(object sender, EventArgs e)
        {
            txtRemarks.Focus();
        }

        protected void txtRemarks_OnTextChanged(object sender, EventArgs e)
        {
            var txtItemNameFooter = (DropDownList)gv_details.FooterRow.FindControl("txtItemNameFooter");
            txtItemNameFooter.Focus();

        }
    }
}