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
using System.Threading;


namespace DynamicMenu.Stock.Report.Report
{
    public partial class rpt_sales_statement_sc : System.Web.UI.Page
    {
        string strPreviousRowID = string.Empty;
        // To keep track the Index of Group Total    
        int intSubTotalIndex = 1;
        decimal totAmount = 0;
        string totAmountComma = "0";
        decimal dblGrandTotalAmount = 0;
        string grandtotalamt = "0";
        string dblGrandTotalAmountComma = "0";
        decimal dblSubGrandTotalAmount = 0;
        string SubdblGrandTotalAmountComma = "0";

        decimal qty = 0;
        string totqtyComma = "0";
        decimal dblGrandTotalqty = 0;
        string grandtotalqty = "0";
        decimal dblSubGrandTotalqty = 0;
        string Subgrandtotalqty = "0";

        decimal totAmt = 0;
        string totAmtComma = "0";
        decimal dblGrandTotaltotAmt = 0;
        string grandtotaltotAmt = "0";
        decimal dblSubGrandTotaltotAmt = 0;
        string SubgrandtotaltotAmt = "0";


        decimal discount = 0;
        string totdiscountComma = "0";
        decimal dblGrandTotaldiscount = 0;
        string grandtotaldiscount = "0";
        decimal dblSubGrandTotaldiscount = 0;
        string Subgrandtotaldiscount = "0";

        decimal net = 0;
        string totnetComma = "0";
        decimal dblGrandTotalnet = 0;
        string grandtotalnet = "0";
        decimal dblSubGrandTotalnet = 0;
        string Subgrandtotalnet = "0";

        decimal rec = 0;
        string totrecComma = "0";
        decimal dblGrandTotalrec = 0;
        string grandtotalrec = "0";
        decimal dblSubGrandTotalrec = 0;
        string Subgrandtotalrec = "0";

        decimal due = 0;
        string totdueComma = "0";
        decimal dblGrandTotaldue = 0;
        string grandtotaldue = "0";
        decimal dblSubGrandTotaldue = 0;
        string Subgrandtotaldue = "0";

        SqlConnection conn = new SqlConnection(dbFunctions.connection);
        IFormatProvider dateformat = new System.Globalization.CultureInfo("fr-FR", true);
        protected void Page_Load(object sender, EventArgs e)
        {
            if (Session["UserName"] == null)
            {
                Response.Redirect("~/Login/UI/Login.aspx");
            }
            else
            {
                try
                {
                    string uTp = HttpContext.Current.Session["USERTYPE"].ToString();
                    string brCD = HttpContext.Current.Session["BrCD"].ToString();
                    string companyId = HttpContext.Current.Session["COMPANYID"].ToString();
                    SqlCommand cmd = new SqlCommand();
                    if (uTp == "COMPADMIN")
                    {
                        dbFunctions.lblAdd(@"SELECT COMPNM FROM ASL_COMPANY WHERE COMPID ='101'", lblCompNM);
                       // dbFunctions.lblAdd(@"SELECT ADDRESS FROM ASL_COMPANY WHERE COMPID ='101'", lblAddress);
                    }
                    else
                    {
                        dbFunctions.lblAdd(@"SELECT COMPNM FROM ASL_COMPANY WHERE COMPID ='" + companyId + "'", lblCompNM);
                       // dbFunctions.lblAdd(@"SELECT ADDRESS FROM ASL_COMPANY WHERE COMPID ='" + brCD + "'", lblAddress);
                    }
                    DateTime PrintDate = dbFunctions.Timezone(DateTime.Now);
                    string td = PrintDate.ToString("dd-MMM-yyyy hh:mm tt");
                    lblTime.Text = td;

                    string fdate = Session["FromDate"].ToString();
                    string todate = Session["ToDate"].ToString();
                    lblFromDate.Text = fdate;
                    lblToDate.Text = todate;

                    string storeId = Session["StoreId"].ToString();
                    dbFunctions.lblAdd("SELECT STORENM FROM STK_STORE WHERE STOREID='" + storeId + "'", lblStoreName);

                    showrepeat1();
                    showrepeat2();


                    lblTotQty.Text = grandtotalqty;
                    lblTotAmount.Text = grandtotalamt;
                    lblDiscount.Text = grandtotaldiscount;
                    lblNet.Text = grandtotalnet;
                    lblRec.Text = grandtotalrec;
                    lblDue.Text = grandtotaldue;

                    lblTotQtyEx.Text = grandtotalqtyExchagne;
                    lblTotAmountEx.Text = grandtotalamtExchagne;
                    lblDiscountEx.Text = grandtotaldiscountExchagne;
                    lblNetEx.Text = grandtotalnetExchagne;
                    lblRecEx.Text = grandtotalrecExchagne;
                    lblDueEx.Text = grandtotaldueExchagne;

                    decimal TotalNetAmount = dblGrandTotalrec - dblGrandTotalrecExchagne;
                    lblTotalNetAmount.Text = SpellAmount.comma(TotalNetAmount);

                    string store = Session["StoreId"].ToString();
                    DateTime FRDT = DateTime.Parse(fdate, dateformat, System.Globalization.DateTimeStyles.AssumeLocal);
                    string fDt = FRDT.ToString("yyyy/MM/dd");
                    DateTime ToDT = DateTime.Parse(todate, dateformat, System.Globalization.DateTimeStyles.AssumeLocal);
                    string tDt = ToDT.ToString("yyyy/MM/dd");
                   
                    conn.Open();
                    SqlCommand cmdCommand = new SqlCommand(@"SELECT SUM(SALEAMT) AS SALEAMT,SUM(RTAMT) AS RTAMT,SUM(DISCOUNT) AS DISCOUNT,SUM(TOTDED) AS TOTDED ,
                    SUM(SALEAMT)+SUM(TOTDED)-SUM(RTAMT)-SUM(DISCOUNT) AS TOTNET
                    FROM (
                    select SUM(TOTAMT) AS SALEAMT, 0 RTAMT, 0 DISCOUNT, 0 TOTDED from STK_TRANSMST WHERE TRANSTP = 'SALE'
                    AND TRANSDT BETWEEN @fdt AND @tDt AND UNITFR=@Store
                    UNION ALL
                    select  0 SALEAMT, SUM(TOTAMT) AS RTAMT, 0 DISCOUNT, 0 TOTDED from STK_TRANSMST WHERE TRANSTP = 'IRTS'
                    AND TRANSDT BETWEEN @fdt AND @tDt AND UNITTO=@Store
                    UNION ALL
                    select  0 AS SALEAMT, 0 RTAMT, SUM(DISCOUNT) DISCOUNT, 0 TOTDED from STK_TRANSMST WHERE TRANSTP = 'SALE'
                    AND TRANSDT BETWEEN @fdt AND @tDt AND UNITFR=@Store
                    UNION ALL
                    select  0 AS SALEAMT, 0 RTAMT, SUM(DISCOUNT) DISCOUNT, 0 TOTDED from STK_TRANSMST WHERE TRANSTP = 'IRTS'
                    AND TRANSDT BETWEEN @fdt AND @tDt AND UNITTO=@Store
                    UNION ALL
                    select  0 SALEAMT, 0 RTAMT, 0 DISCOUNT, SUM(TOTDED) TOTDED from STK_TRANSMST WHERE TRANSTP = 'IRTS'
                    AND TRANSDT BETWEEN @fdt AND @tDt AND UNITTO=@Store
                    ) AS A ", conn);
                    cmdCommand.Parameters.AddWithValue("@fdt", fDt);
                    cmdCommand.Parameters.AddWithValue("@tDt", tDt);
                    cmdCommand.Parameters.AddWithValue("@Store", store);
                    SqlDataReader dr = cmdCommand.ExecuteReader();
                    while (dr.Read())
                    {
                        lblTotalSaleAmount.Text = dr["SALEAMT"].ToString();
                        lblTotalReturnAmount.Text = dr["RTAMT"].ToString();
                        lblTotalDiscountAmount.Text = dr["DISCOUNT"].ToString();
                        lblTotalDeductionAmount.Text = dr["TOTDED"].ToString();
                        lblTotalNetValue.Text = dr["TOTNET"].ToString();
                    }
                    dr.Close();
                    conn.Close();


                }
                catch
                {

                }
            }
        }


        protected void gv_Trans_RowDataBound(object sender, GridViewRowEventArgs e)
        {

            if (e.Row.RowType == DataControlRowType.DataRow)
            {
                string ITEMNM = DataBinder.Eval(e.Row.DataItem, "ITEMNM").ToString();
                e.Row.Cells[0].Text = ITEMNM;


                decimal QTY = Convert.ToDecimal(DataBinder.Eval(e.Row.DataItem, "QTY").ToString());
                string QTYAMT = SpellAmount.comma(QTY);
                e.Row.Cells[1].Text = QTYAMT;


                qty += QTY;
                totqtyComma = SpellAmount.comma(qty);

                dblSubGrandTotalqty += QTY;
                Subgrandtotalqty = dblSubGrandTotalqty.ToString();

                dblGrandTotalqty += QTY;
                grandtotalqty = dblGrandTotalqty.ToString();
                //lblTtlAmnt.Text = SpellAmount.comma(dblGrandTotalqty);



                decimal RATE = Convert.ToDecimal(DataBinder.Eval(e.Row.DataItem, "RATE").ToString());
                string RATEAmt = SpellAmount.comma(RATE);
                e.Row.Cells[2].Text = RATEAmt;



                decimal AMOUNT = Convert.ToDecimal(DataBinder.Eval(e.Row.DataItem, "AMOUNT").ToString());
                string AMNT = SpellAmount.comma(AMOUNT);
                e.Row.Cells[3].Text = AMNT;

                totAmount = AMOUNT;
                totAmountComma = SpellAmount.comma(totAmount);

                


                decimal TOTAMT = Convert.ToDecimal(DataBinder.Eval(e.Row.DataItem, "TOTAMT").ToString());
                string TOTAMTAMNT = SpellAmount.comma(TOTAMT);
                e.Row.Cells[4].Text = TOTAMTAMNT;

                totAmt = TOTAMT;
                totAmtComma = SpellAmount.comma(totAmt);
                




                decimal DISCOUNT = Convert.ToDecimal(DataBinder.Eval(e.Row.DataItem, "DISCOUNT").ToString());
                string DISCOUNTAMNT = SpellAmount.comma(DISCOUNT);
                e.Row.Cells[5].Text = DISCOUNTAMNT;

                discount = DISCOUNT;
                totdiscountComma = SpellAmount.comma(discount);

           



                decimal NETAMT = Convert.ToDecimal(DataBinder.Eval(e.Row.DataItem, "NETAMT").ToString());
                string NETAMTAMNT = SpellAmount.comma(NETAMT);
                e.Row.Cells[6].Text = NETAMTAMNT;

                net = NETAMT;
                totnetComma = SpellAmount.comma(net);

            


                decimal DUEAMT = Convert.ToDecimal(DataBinder.Eval(e.Row.DataItem, "DUEAMT").ToString());
                string DUEAMTAMNT = SpellAmount.comma(DUEAMT);
                e.Row.Cells[7].Text = DUEAMTAMNT;

                due = DUEAMT;
                totdueComma = SpellAmount.comma(due);



                decimal RECAMT = Convert.ToDecimal(DataBinder.Eval(e.Row.DataItem, "CASHAMT").ToString());
                string RECAMTAMNT = SpellAmount.comma(RECAMT);
                e.Row.Cells[8].Text = RECAMTAMNT;

                rec = RECAMT;
                totrecComma = SpellAmount.comma(rec);



              
            }
            else if (e.Row.RowType == DataControlRowType.Footer)
            {
                e.Row.Cells[0].Text = "Sub Total : ";
                e.Row.Cells[0].HorizontalAlign = HorizontalAlign.Center;
                e.Row.Cells[1].Text = totqtyComma;
                e.Row.Cells[1].HorizontalAlign = HorizontalAlign.Center;
                e.Row.Cells[3].Text = totAmtComma;
                e.Row.Cells[3].HorizontalAlign = HorizontalAlign.Right;
                e.Row.Cells[4].Text = totAmtComma;
                e.Row.Cells[4].HorizontalAlign = HorizontalAlign.Right;
                e.Row.Cells[5].Text = totdiscountComma;
                e.Row.Cells[5].HorizontalAlign = HorizontalAlign.Right;
                e.Row.Cells[6].Text = totnetComma;
                e.Row.Cells[6].HorizontalAlign = HorizontalAlign.Right;
                e.Row.Cells[7].Text = totdueComma;
                e.Row.Cells[7].HorizontalAlign = HorizontalAlign.Right;
                e.Row.Cells[8].Text = totrecComma;
                e.Row.Cells[8].HorizontalAlign = HorizontalAlign.Right;


                dblSubGrandTotaltotAmt += totAmt;
                SubgrandtotaltotAmt = dblSubGrandTotaltotAmt.ToString();

                dblSubGrandTotaldiscount += discount;
                Subgrandtotaldiscount = dblSubGrandTotaldiscount.ToString();
                dblSubGrandTotalnet += net;
                Subgrandtotalnet = dblSubGrandTotalnet.ToString();

                dblSubGrandTotalrec += rec;
                Subgrandtotalrec = dblSubGrandTotalrec.ToString();
                dblSubGrandTotaldue += due;
                Subgrandtotaldue = dblSubGrandTotaldue.ToString();

                dblGrandTotalAmount += totAmt;
                grandtotalamt = dblGrandTotalAmount.ToString();
                dblGrandTotaldiscount += discount;
                grandtotaldiscount = dblGrandTotaldiscount.ToString();
                dblGrandTotalnet += net;
                grandtotalnet = dblGrandTotalnet.ToString();
                dblGrandTotalrec += rec;
                grandtotalrec = dblGrandTotalrec.ToString();
                dblGrandTotaldue += due;
                grandtotaldue = dblGrandTotaldue.ToString();


                e.Row.Font.Bold = true;
                qty = 0;
                
                totAmt = 0;
                discount = 0;
                net = 0;
                totAmount = 0;
                
                rec = 0;
                due = 0;

            }



        }

        protected void gv_Trans_RowCreated(object sender, GridViewRowEventArgs e)
        {
            bool IsSubTotalRowNeedToAdd = false;
            bool IsGrandTotalRowNeedtoAdd = false;
            if ((strPreviousRowID != string.Empty) && (DataBinder.Eval(e.Row.DataItem, "TRANSNO") != null))
                if (strPreviousRowID != DataBinder.Eval(e.Row.DataItem, "TRANSNO").ToString())
                    IsSubTotalRowNeedToAdd = true;
            if ((strPreviousRowID != string.Empty) && (DataBinder.Eval(e.Row.DataItem, "TRANSNO") == null))
            {
                IsSubTotalRowNeedToAdd = true;
                IsGrandTotalRowNeedtoAdd = true;
                intSubTotalIndex = 0;
            }

            #region Inserting first Row and populating fist Group Header details


            if ((strPreviousRowID == string.Empty) && (DataBinder.Eval(e.Row.DataItem, "TRANSNO") != null))
            {
                GridView GridView1 = (GridView)sender;
                GridViewRow row = new GridViewRow(0, 0, DataControlRowType.DataRow, DataControlRowState.Insert);
                TableCell cell = new TableCell();
                cell.Text = " " + DataBinder.Eval(e.Row.DataItem, "TRANSNO").ToString();
                cell.ColumnSpan = 12;
                cell.Font.Bold = true;
                cell.CssClass = "GroupHeaderStyle";
                row.Cells.Add(cell);
                GridView1.Controls[0].Controls.AddAt(e.Row.RowIndex + intSubTotalIndex, row);
                intSubTotalIndex++;
            }

            #endregion

            if (IsSubTotalRowNeedToAdd)
            {
                #region Adding Sub Total Row

                GridView GridView1 = (GridView)sender;
                // Creating a Row          
                GridViewRow row = new GridViewRow(0, 0, DataControlRowType.DataRow, DataControlRowState.Insert);

                //Adding Total Cell          
                TableCell cell = new TableCell();

                //Adding the Row at the RowIndex position in the Grid      
                GridView1.Controls[0].Controls.AddAt(e.Row.RowIndex + intSubTotalIndex, row);
                intSubTotalIndex++;

                #endregion

                #region Adding Next Group Header Details

                if (DataBinder.Eval(e.Row.DataItem, "TRANSNO") != null)
                {
                    row = new GridViewRow(0, 0, DataControlRowType.DataRow, DataControlRowState.Insert);
                    cell = new TableCell();

                    cell.Text = DataBinder.Eval(e.Row.DataItem, "TRANSNO").ToString();
                    cell.ColumnSpan = 12;
                    cell.Font.Bold = true;
                    cell.CssClass = "GroupHeaderStyle";
                    row.Cells.Add(cell);
                    GridView1.Controls[0].Controls.AddAt(e.Row.RowIndex + intSubTotalIndex, row);
                    intSubTotalIndex++;
                }
                #endregion

                #region Reseting the Sub Total Variables

                

                #endregion

            }


            if (IsGrandTotalRowNeedtoAdd)
            {

            }
        }

        public void showrepeat1()
        {
            SqlConnection conn = new SqlConnection(dbFunctions.connection);
            string FROMDT = Session["FromDate"].ToString();
            string TODT = Session["ToDate"].ToString();
            string store = Session["StoreId"].ToString();
            DateTime FRDT = DateTime.Parse(FROMDT, dateformat, System.Globalization.DateTimeStyles.AssumeLocal);
            string Date1 = FRDT.ToString("yyyy/MM/dd");
            DateTime ToDT = DateTime.Parse(TODT, dateformat, System.Globalization.DateTimeStyles.AssumeLocal);
            string Date2 = ToDT.ToString("yyyy/MM/dd");
            conn.Open();
            SqlCommand cmd = new SqlCommand(@"SELECT DISTINCT CONVERT(NVARCHAR,TRANSDT,103) AS TRANSDT , TRANSDT AS DT  FROM STK_TRANSMST WHERE TRANSTP='SALE' AND UNITFR=@UNITFR AND  TRANSDT BETWEEN @d1 AND @d2 ORDER BY DT", conn);
            cmd.Parameters.Clear();
            cmd.Parameters.AddWithValue("@UNITFR", store);
            cmd.Parameters.AddWithValue("@d1", Date1);
            cmd.Parameters.AddWithValue("@d2", Date2);
            SqlDataAdapter da = new SqlDataAdapter(cmd);
            DataSet ds = new DataSet();
            da.Fill(ds);
            conn.Close();
            if (ds.Tables[0].Rows.Count > 0)
            {
                Repeater1.DataSource = ds;
                Repeater1.DataBind();
                Repeater1.Visible = true;


            }
            else
            {
                Repeater1.DataSource = ds;
                Repeater1.DataBind();
                Repeater1.Visible = true;
            }
        }
        protected void Repeater1_ItemDataBound(object sender, RepeaterItemEventArgs e)
        {
            //Label lblText = (Label)e.Item.FindControl("lblText");
            Label lbldate = (Label)e.Item.FindControl("lbldate");
            Label lblAmount = (Label)e.Item.FindControl("lblAmount");
            DateTime DT = DateTime.Parse(lbldate.Text, dateformat, System.Globalization.DateTimeStyles.AssumeLocal);
            string Date = DT.ToString("yyyy/MM/dd");
            Repeater Repeater2 = (Repeater)(e.Item.FindControl("Repeater2"));

            Label lblSubTotQty = (Label)(e.Item.FindControl("lblSubTotQty"));
            Label lblSubTotAmount = (Label)(e.Item.FindControl("lblSubTotAmount"));
            Label lblSubDiscount = (Label)(e.Item.FindControl("lblSubDiscount"));
            Label lblSubNet = (Label)(e.Item.FindControl("lblSubNet"));
            Label lblSubRec = (Label)(e.Item.FindControl("lblSubRec"));
            Label lblSubDue = (Label)(e.Item.FindControl("lblSubDue"));

            DataTable DT1 = LoadData1(Date);
            if (DT1.Rows.Count > 0)
            {
                Repeater2.DataSource = DT1;
                Repeater2.DataBind();

                lblSubTotQty.Text = Subgrandtotalqty;
                lblSubTotAmount.Text = SubgrandtotaltotAmt;
                lblSubDiscount.Text = Subgrandtotaldiscount;
                lblSubNet.Text = Subgrandtotalnet;
                lblSubRec.Text = Subgrandtotalrec;
                lblSubDue.Text = Subgrandtotaldue;

                
                dblSubGrandTotalAmount = 0;
                dblSubGrandTotalqty = 0;
                dblSubGrandTotaltotAmt = 0;
                dblSubGrandTotaldiscount = 0;
                dblSubGrandTotalnet = 0;
                dblSubGrandTotalrec = 0;
                dblSubGrandTotaldue = 0;

            }
            else
            {
                //lblText.Visible = false;
                lbldate.Visible = false;
                Repeater2.Visible = false;
            }

        }
        private DataTable LoadData1(string DT1)
        {
            string store = Session["StoreId"].ToString();
            SqlConnection conn = new SqlConnection(dbFunctions.connection);
            DataTable dtGrid = new DataTable();
            conn.Open();
            SqlCommand cmd = new SqlCommand(@"SELECT TRANSNO, CONVERT(NVARCHAR(10),TRANSDT,103) as TRANSDT,  
            CONVERT(VARCHAR(5), INTIME, 108) + ' ' + RIGHT(CONVERT(VARCHAR(30), INTIME, 9), 2) as INTIME FROM STK_TRANSMST WHERE TRANSTP='SALE' AND TRANSDT=@DATE AND UNITFR=@UNITFR ORDER BY TRANSNO", conn);
            cmd.Parameters.AddWithValue("@DATE", DT1);
            cmd.Parameters.AddWithValue("@UNITFR", store);
            SqlDataAdapter adapter = new SqlDataAdapter(cmd);
            adapter.Fill(dtGrid);
            conn.Close();
            return dtGrid;
        }
       
        protected void Repeater2_ItemDataBound(object sender, RepeaterItemEventArgs e)
        {
            // Repeater Repeater1= (Repeater)(e.Item.FindControl("Repeater1"));
            Label lbldate1 = (Label)e.Item.FindControl("lbldate1");
            Label lblTransDT = (Label)e.Item.FindControl("lblTransDT");
            //Label lblHead = (Label)e.Item.FindControl("lblHead");

            //string Head = lblHead.Text;
            DateTime DT = DateTime.Parse(lblTransDT.Text, dateformat, System.Globalization.DateTimeStyles.AssumeLocal);
            string Date = DT.ToString("yyyy/MM/dd");
            string TransNO = lbldate1.Text;
            GridView gv_Trans = (GridView)e.Item.FindControl("gv_Trans");
            DataTable Head2 = LoadData2(TransNO, Date);
            if (Head2.Rows.Count > 0)
            {
                gv_Trans.DataSource = Head2;
                gv_Trans.DataBind();
                MergeRows(gv_Trans);
            }
            else
            {
                //lblText.Visible = false;
                //lblHead.Visible = false;
                gv_Trans.Visible = false;
            }
        }
        private DataTable LoadData2(string TransNO, string TransDT)
        {
            string store = Session["StoreId"].ToString();
            SqlConnection conn = new SqlConnection(dbFunctions.connection);
            DataTable dtGrid = new DataTable();
            conn.Open();
            SqlCommand cmd = new SqlCommand(@"SELECT STK_TRANSMST.TRANSNO, dbo.STK_TRANS.ITEMID+' - '+ dbo.STK_ITEM.ITEMNM +' - '+ dbo.STK_ITEM.AUTHOR AS ITEMNM, SUM(STK_TRANS.QTY) AS QTY, STK_TRANS.RATE, SUM(STK_TRANS.AMOUNT) AS AMOUNT, STK_TRANSMST.TOTAMT, 
 dbo.STK_TRANSMST.DISCOUNT, dbo.STK_TRANSMST.NETAMT, dbo.STK_TRANSMST.CASHAMT,
(CASE WHEN STK_TRANSMST.TRRTSNO IS NULL THEN dbo.STK_TRANSMST.NETREC ELSE dbo.STK_TRANSMST.CASHAMT END) AS NETREC, 
dbo.STK_TRANSMST.CREDITAMT AS DUEAMT FROM dbo.STK_TRANSMST INNER JOIN
dbo.STK_TRANS ON dbo.STK_TRANSMST.TRANSTP = dbo.STK_TRANS.TRANSTP AND dbo.STK_TRANSMST.TRANSNO = dbo.STK_TRANS.TRANSNO AND 
dbo.STK_TRANSMST.TRANSDT = dbo.STK_TRANS.TRANSDT INNER JOIN
dbo.STK_ITEM ON dbo.STK_TRANS.ITEMID = dbo.STK_ITEM.ITEMID
WHERE dbo.STK_TRANSMST.TRANSTP = 'SALE'
AND STK_TRANSMST.TRANSDT = @DATE AND STK_TRANSMST.TRANSNO=@TRANSNO  AND STK_TRANSMST.UNITFR=@UNITFR
GROUP BY STK_TRANSMST.TRANSDT, STK_TRANSMST.TRANSNO,STK_TRANS.ITEMID,STK_ITEM.ITEMNM,STK_TRANSMST.TRRTSNO,
STK_TRANS.RATE,STK_TRANSMST.TOTAMT,STK_TRANSMST.DISCOUNT,STK_TRANSMST.NETAMT,STK_TRANSMST.NETREC,STK_TRANSMST.CREDITAMT,STK_TRANSMST.CASHAMT,STK_ITEM.AUTHOR
ORDER BY STK_TRANSMST.TRANSDT, STK_TRANSMST.TRANSNO,STK_ITEM.ITEMNM", conn);
            cmd.Parameters.AddWithValue("@DATE", TransDT);
            cmd.Parameters.AddWithValue("@UNITFR", store);
            cmd.Parameters.AddWithValue("@TRANSNO", TransNO);
            SqlDataAdapter adapter = new SqlDataAdapter(cmd);
            adapter.Fill(dtGrid);
            conn.Close();
            return dtGrid;
        }




        public static void MergeRows(GridView gridView)
        {
            for (int rowIndex = gridView.Rows.Count - 2; rowIndex >= 0; rowIndex--)
            {
                GridViewRow row = gridView.Rows[rowIndex];
                GridViewRow previousRow = gridView.Rows[rowIndex + 1];

                for (int i = 4; i < row.Cells.Count; i++)
                {
                    if (row.Cells[i].Text == previousRow.Cells[i].Text)
                    {
                        row.Cells[i].RowSpan = previousRow.Cells[i].RowSpan < 2 ? 2 :
                                               previousRow.Cells[i].RowSpan + 1;
                        previousRow.Cells[i].Visible = false;
                    }
                }
            }
        }


        protected void gridView_PreRender(object sender, EventArgs e)
        {
            
        }



        /*************************************** Exchange *******************************************/
        /*************************************** Exchange *******************************************/
        /*************************************** Exchange *******************************************/
        /*************************************** Exchange *******************************************/
        /*************************************** Exchange *******************************************/
        /*************************************** Exchange *******************************************/
        /*************************************** Exchange *******************************************/
        /*************************************** Exchange *******************************************/






        string strPreviousRowIDExchagne = string.Empty;
        // To keep track the Index of Group Total    
        int intSubTotalIndexExchagne = 1;
        decimal totAmountExchagne = 0;
        string totAmountCommaExchagne = "0";
        decimal dblGrandTotalAmountExchagne = 0;
        string grandtotalamtExchagne = "0";
        string dblGrandTotalAmountCommaExchagne = "0";
        decimal dblSubGrandTotalAmountExchagne = 0;
        string SubdblGrandTotalAmountCommaExchagne = "0";

        decimal qtyExchagne = 0;
        string totqtyCommaExchagne = "0";
        decimal dblGrandTotalqtyExchagne = 0;
        string grandtotalqtyExchagne = "0";
        decimal dblSubGrandTotalqtyExchagne = 0;
        string SubgrandtotalqtyExchagne = "0";

        decimal totAmtExchagne = 0;
        string totAmtCommaExchagne = "0";
        decimal dblGrandTotaltotAmtExchagne = 0;
        string grandtotaltotAmtExchagne = "0";
        decimal dblSubGrandTotaltotAmtExchagne = 0;
        string SubgrandtotaltotAmtExchagne = "0";


        decimal discountExchagne = 0;
        string totdiscountCommaExchagne = "0";
        decimal dblGrandTotaldiscountExchagne = 0;
        string grandtotaldiscountExchagne = "0";
        decimal dblSubGrandTotaldiscountExchagne = 0;
        string SubgrandtotaldiscountExchagne = "0";

        decimal netExchagne = 0;
        string totnetCommaExchagne = "0";
        decimal dblGrandTotalnetExchagne = 0;
        string grandtotalnetExchagne = "0";
        decimal dblSubGrandTotalnetExchagne = 0;
        string SubgrandtotalnetExchagne = "0";


        decimal recExchagne = 0;
        string totrecCommaExchagne = "0";
        decimal dblGrandTotalrecExchagne = 0;
        string grandtotalrecExchagne = "0";
        decimal dblSubGrandTotalrecExchagne = 0;
        string SubgrandtotalrecExchagne = "0";

        decimal dueExchagne = 0;
        string totdueCommaExchagne = "0";
        decimal dblGrandTotaldueExchagne = 0;
        string grandtotaldueExchagne = "0";
        decimal dblSubGrandTotaldueExchagne = 0;
        string SubgrandtotaldueExchagne = "0";




        protected void gv_Trans_RowDataBoundEx(object sender, GridViewRowEventArgs e)
        {

            if (e.Row.RowType == DataControlRowType.DataRow)
            {
                string ITEMNM = DataBinder.Eval(e.Row.DataItem, "ITEMNM").ToString();
                e.Row.Cells[0].Text = ITEMNM;


                decimal QTY = Convert.ToDecimal(DataBinder.Eval(e.Row.DataItem, "QTY").ToString());
                string QTYAMT = SpellAmount.comma(QTY);
                e.Row.Cells[1].Text = QTYAMT;


                qtyExchagne += QTY;
                totqtyCommaExchagne = SpellAmount.comma(qtyExchagne);

              


                decimal RATE = Convert.ToDecimal(DataBinder.Eval(e.Row.DataItem, "RATE").ToString());
                string RATEAmt = SpellAmount.comma(RATE);
                e.Row.Cells[2].Text = RATEAmt;



                decimal AMOUNT = Convert.ToDecimal(DataBinder.Eval(e.Row.DataItem, "AMOUNT").ToString());
                string AMNT = SpellAmount.comma(AMOUNT);
                e.Row.Cells[3].Text = AMNT;

                totAmountExchagne += AMOUNT;
                totAmountCommaExchagne = SpellAmount.comma(totAmountExchagne);

                dblGrandTotalAmountExchagne += AMOUNT;
                grandtotalamtExchagne = dblGrandTotalAmountExchagne.ToString();
                //lblTtlAmnt.Text = SpellAmount.comma(dblGrandTotalAmount);



                decimal TOTAMT = Convert.ToDecimal(DataBinder.Eval(e.Row.DataItem, "TOTAMT").ToString());
                string TOTAMTAMNT = SpellAmount.comma(TOTAMT);
                e.Row.Cells[4].Text = TOTAMTAMNT;

                totAmtExchagne = TOTAMT;
                totAmtCommaExchagne = SpellAmount.comma(totAmtExchagne);

               
              

                decimal DISCOUNT = Convert.ToDecimal(DataBinder.Eval(e.Row.DataItem, "DISCOUNT").ToString());
                string DISCOUNTAMNT = SpellAmount.comma(DISCOUNT);
                e.Row.Cells[5].Text = DISCOUNTAMNT;

                discountExchagne = DISCOUNT;
                totdiscountCommaExchagne = SpellAmount.comma(discountExchagne);



                decimal NETAMT = Convert.ToDecimal(DataBinder.Eval(e.Row.DataItem, "TOTDED").ToString());
                string NETAMTAMNT = SpellAmount.comma(NETAMT);
                e.Row.Cells[6].Text = NETAMTAMNT;

                netExchagne = NETAMT;
                totnetCommaExchagne = SpellAmount.comma(netExchagne);


               
                decimal RECAMT = Convert.ToDecimal(DataBinder.Eval(e.Row.DataItem, "NETAMT").ToString());
                string RECAMTAMNT = SpellAmount.comma(RECAMT);
                e.Row.Cells[8].Text = RECAMTAMNT;

                recExchagne = RECAMT;
                totrecCommaExchagne = SpellAmount.comma(recExchagne);


                decimal DUEAMT = Convert.ToDecimal(DataBinder.Eval(e.Row.DataItem, "DUEAMT").ToString());
                string DUEAMTAMNT = SpellAmount.comma(DUEAMT);
                e.Row.Cells[7].Text = DUEAMTAMNT;

                dueExchagne = DUEAMT;
                totdueCommaExchagne = SpellAmount.comma(dueExchagne);



            }
            else if (e.Row.RowType == DataControlRowType.Footer)
            {
                e.Row.Cells[0].Text = "Sub Total : ";
                e.Row.Cells[0].HorizontalAlign = HorizontalAlign.Center;
                e.Row.Cells[1].Text = totqtyCommaExchagne;
                e.Row.Cells[1].HorizontalAlign = HorizontalAlign.Center;
                e.Row.Cells[3].Text = totAmountCommaExchagne;
                e.Row.Cells[3].HorizontalAlign = HorizontalAlign.Right;
                e.Row.Cells[4].Text = totAmtCommaExchagne;
                e.Row.Cells[4].HorizontalAlign = HorizontalAlign.Right;
                
                e.Row.Cells[5].Text = totdiscountCommaExchagne;
                e.Row.Cells[5].HorizontalAlign = HorizontalAlign.Right;
                e.Row.Cells[6].Text = totnetCommaExchagne;
                e.Row.Cells[6].HorizontalAlign = HorizontalAlign.Right;
               
                e.Row.Cells[8].Text = totrecCommaExchagne;
                e.Row.Cells[8].HorizontalAlign = HorizontalAlign.Right;
                e.Row.Cells[7].Text = totdueCommaExchagne;
                e.Row.Cells[7].HorizontalAlign = HorizontalAlign.Right;


                dblSubGrandTotalqtyExchagne += qtyExchagne;
                SubgrandtotalqtyExchagne = dblSubGrandTotalqtyExchagne.ToString();

                dblGrandTotalqtyExchagne += qtyExchagne;
                grandtotalqtyExchagne = dblGrandTotalqtyExchagne.ToString();
                //lblTtlAmnt.Text = SpellAmount.comma(dblGrandTotalqty);



                dblSubGrandTotaltotAmtExchagne += totAmtExchagne;
                SubgrandtotaltotAmtExchagne = dblSubGrandTotaltotAmtExchagne.ToString();

                dblGrandTotaltotAmtExchagne += totAmtExchagne;
                grandtotalamtExchagne = dblGrandTotaltotAmtExchagne.ToString();
                //lblTtlAmnt.Text = SpellAmount.comma(dblGrandTotalAmount);

               
                dblSubGrandTotaldiscountExchagne += discountExchagne;
                SubgrandtotaldiscountExchagne = dblSubGrandTotaldiscountExchagne.ToString();

                dblGrandTotaldiscountExchagne += discountExchagne;
                grandtotaldiscountExchagne = dblGrandTotaldiscountExchagne.ToString();

                dblSubGrandTotalnetExchagne += netExchagne;
                SubgrandtotalnetExchagne = dblSubGrandTotalnetExchagne.ToString();

                dblGrandTotalnetExchagne += netExchagne;
                grandtotalnetExchagne = dblGrandTotalnetExchagne.ToString();

              
                dblSubGrandTotalrecExchagne += recExchagne;
                SubgrandtotalrecExchagne = dblSubGrandTotalrecExchagne.ToString();

                dblGrandTotalrecExchagne += recExchagne;
                grandtotalrecExchagne = dblGrandTotalrecExchagne.ToString();

                dblSubGrandTotaldueExchagne += dueExchagne;
                SubgrandtotaldueExchagne = dblSubGrandTotaldueExchagne.ToString();

                dblGrandTotaldueExchagne += dueExchagne;
                grandtotaldueExchagne = dblGrandTotaldueExchagne.ToString();



                e.Row.Font.Bold = true;
                qtyExchagne = 0;
                totAmtExchagne = 0;
                discountExchagne = 0;
                netExchagne = 0;
                totAmountExchagne = 0;
                recExchagne = 0;
                dueExchagne = 0;
            }



        }

       
        public void showrepeat2()
        {
            SqlConnection conn = new SqlConnection(dbFunctions.connection);
            string FROMDT = Session["FromDate"].ToString();
            string TODT = Session["ToDate"].ToString();
            string store = Session["StoreId"].ToString();
            DateTime FRDT = DateTime.Parse(FROMDT, dateformat, System.Globalization.DateTimeStyles.AssumeLocal);
            string Date1 = FRDT.ToString("yyyy/MM/dd");
            DateTime ToDT = DateTime.Parse(TODT, dateformat, System.Globalization.DateTimeStyles.AssumeLocal);
            string Date2 = ToDT.ToString("yyyy/MM/dd");
            conn.Open();
            SqlCommand cmd = new SqlCommand(@"SELECT DISTINCT CONVERT(NVARCHAR,TRANSDT,103) AS TRANSDT , TRANSDT AS DT  FROM STK_TRANSMST WHERE TRANSTP='IRTS' AND UNITTO=@UNITTO AND  TRANSDT BETWEEN @d1 AND @d2 ORDER BY DT", conn);
            cmd.Parameters.Clear();
            cmd.Parameters.AddWithValue("@UNITTO", store);
            cmd.Parameters.AddWithValue("@d1", Date1);
            cmd.Parameters.AddWithValue("@d2", Date2);
            SqlDataAdapter da = new SqlDataAdapter(cmd);
            DataSet ds = new DataSet();
            da.Fill(ds);
            conn.Close();
            if (ds.Tables[0].Rows.Count > 0)
            {
                Repeater3.DataSource = ds;
                Repeater3.DataBind();
                Repeater3.Visible = true;


            }
            else
            {
                Repeater3.DataSource = ds;
                Repeater3.DataBind();
                Repeater3.Visible = true;
            }
        }
        protected void Repeater3_ItemDataBound(object sender, RepeaterItemEventArgs e)
        {
            //Label lblText = (Label)e.Item.FindControl("lblText");
            Label lbldate = (Label)e.Item.FindControl("lbldate");
            Label lblAmount = (Label)e.Item.FindControl("lblAmount");
            DateTime DT = DateTime.Parse(lbldate.Text, dateformat, System.Globalization.DateTimeStyles.AssumeLocal);
            string Date = DT.ToString("yyyy/MM/dd");
            Repeater Repeater4 = (Repeater)(e.Item.FindControl("Repeater4"));

            Label lblSubTotQty = (Label)(e.Item.FindControl("lblSubTotQty"));
            Label lblSubTotAmount = (Label)(e.Item.FindControl("lblSubTotAmount"));
            Label lblSubVat = (Label)(e.Item.FindControl("lblSubVat"));
            Label lblSubDiscount = (Label)(e.Item.FindControl("lblSubDiscount"));
            Label lblSubNet = (Label)(e.Item.FindControl("lblSubNet"));
            Label lblSubGift = (Label)(e.Item.FindControl("lblSubGift"));
            Label lblSubRec = (Label)(e.Item.FindControl("lblSubRec"));
            Label lblSubDue = (Label)(e.Item.FindControl("lblSubDue"));

            DataTable DT1 = LoadData3(Date);
            if (DT1.Rows.Count > 0)
            {
                Repeater4.DataSource = DT1;
                Repeater4.DataBind();

                lblSubTotQty.Text = SubgrandtotalqtyExchagne;
                lblSubTotAmount.Text = SubgrandtotaltotAmtExchagne;
                lblSubDiscount.Text = SubgrandtotaldiscountExchagne;
                lblSubNet.Text = SubgrandtotalnetExchagne;
                lblSubRec.Text = SubgrandtotalrecExchagne;
                lblSubDue.Text = SubgrandtotaldueExchagne;

                dblSubGrandTotalAmountExchagne = 0;
                dblSubGrandTotalqtyExchagne = 0;
                dblSubGrandTotaltotAmtExchagne = 0;
                dblGrandTotalAmountExchagne = 0;
                dblSubGrandTotaldiscountExchagne = 0;
                dblSubGrandTotalnetExchagne = 0;
                dblSubGrandTotalrecExchagne = 0;
                dblSubGrandTotaldueExchagne = 0;

            }
            else
            {
                //lblText.Visible = false;
                lbldate.Visible = false;
                Repeater4.Visible = false;
            }

        }
        private DataTable LoadData3(string DT1)
        {
            string store = Session["StoreId"].ToString();
            SqlConnection conn = new SqlConnection(dbFunctions.connection);
            DataTable dtGrid = new DataTable();
            conn.Open();
            SqlCommand cmd = new SqlCommand(@"SELECT TRANSNO, CONVERT(NVARCHAR(10),TRANSDT,103) as TRANSDT FROM STK_TRANSMST WHERE TRANSTP='IRTS' AND TRANSDT=@DATE  AND UNITTO=@UNITTO ORDER BY TRANSNO", conn);
            cmd.Parameters.AddWithValue("@DATE", DT1);
            cmd.Parameters.AddWithValue("@UNITTO", store);
            SqlDataAdapter adapter = new SqlDataAdapter(cmd);
            adapter.Fill(dtGrid);
            conn.Close();
            return dtGrid;
        }

        protected void Repeater4_ItemDataBound(object sender, RepeaterItemEventArgs e)
        {
            // Repeater Repeater1= (Repeater)(e.Item.FindControl("Repeater1"));
            Label lbldate1 = (Label)e.Item.FindControl("lbldate1");
            Label lblTransDT = (Label)e.Item.FindControl("lblTransDT");
            //Label lblHead = (Label)e.Item.FindControl("lblHead");

            //string Head = lblHead.Text;
            DateTime DT = DateTime.Parse(lblTransDT.Text, dateformat, System.Globalization.DateTimeStyles.AssumeLocal);
            string Date = DT.ToString("yyyy/MM/dd");
            string TransNO = lbldate1.Text;
            GridView gv_TransEx = (GridView)e.Item.FindControl("gv_TransEx");
            DataTable Head2 = LoadData4(TransNO, Date);
            if (Head2.Rows.Count > 0)
            {
                gv_TransEx.DataSource = Head2;
                gv_TransEx.DataBind();
                MergeRows(gv_TransEx);
            }
            else
            {
                //lblText.Visible = false;
                //lblHead.Visible = false;
                gv_TransEx.Visible = false;
            }
        }
        private DataTable LoadData4(string TransNO, string TransDT)
        {
            string store = Session["StoreId"].ToString();
            SqlConnection conn = new SqlConnection(dbFunctions.connection);
            DataTable dtGrid = new DataTable();
            conn.Open();
            SqlCommand cmd = new SqlCommand(@"SELECT STK_TRANSMST.TRANSNO,  dbo.STK_TRANS.ITEMID+' - '+ dbo.STK_ITEM.ITEMNM +' - '+ dbo.STK_ITEM.AUTHOR AS ITEMNM, SUM(STK_TRANS.QTY) AS QTY, STK_TRANS.RATE, SUM(STK_TRANS.AMOUNT) AS AMOUNT, STK_TRANSMST.TOTAMT, 
                                    dbo.STK_TRANSMST.DISCOUNT, dbo.STK_TRANSMST.NETAMT, dbo.STK_TRANSMST.TOTDED,  dbo.STK_TRANSMST.NETREC, dbo.STK_TRANSMST.CREDITAMT AS DUEAMT
                                    FROM dbo.STK_TRANSMST INNER JOIN
                                    dbo.STK_TRANS ON dbo.STK_TRANSMST.TRANSTP = dbo.STK_TRANS.TRANSTP AND dbo.STK_TRANSMST.TRANSNO = dbo.STK_TRANS.TRANSNO AND 
                                    dbo.STK_TRANSMST.TRANSDT = dbo.STK_TRANS.TRANSDT INNER JOIN
                                    dbo.STK_ITEM ON dbo.STK_TRANS.ITEMID = dbo.STK_ITEM.ITEMID
                                    WHERE dbo.STK_TRANSMST.TRANSTP = 'IRTS'
                                    AND STK_TRANSMST.TRANSDT = @DATE AND STK_TRANSMST.TRANSNO=@TRANSNO  AND STK_TRANSMST.UNITTO=@UNITTO
                                    GROUP BY STK_TRANSMST.TRANSDT, STK_TRANSMST.TRANSNO,STK_TRANS.ITEMID,STK_ITEM.ITEMNM,
                                    STK_TRANS.RATE,STK_TRANSMST.TOTAMT,STK_TRANSMST.DISCOUNT,STK_TRANSMST.NETAMT,STK_TRANSMST.NETREC,dbo.STK_TRANSMST.TOTDED,STK_TRANSMST.CREDITAMT,STK_ITEM.AUTHOR 
                                    ORDER BY STK_TRANSMST.TRANSDT, STK_TRANSMST.TRANSNO,STK_ITEM.ITEMNM", conn);
            cmd.Parameters.AddWithValue("@DATE", TransDT);
            cmd.Parameters.AddWithValue("@UNITTO", store);
            cmd.Parameters.AddWithValue("@TRANSNO", TransNO);
            SqlDataAdapter adapter = new SqlDataAdapter(cmd);
            adapter.Fill(dtGrid);
            conn.Close();
            return dtGrid;
        }





        /*************************************** DUE DUE DUE DUE *******************************************/
        /*************************************** DUE DUE DUE DUE *******************************************/
        /*************************************** DUE DUE DUE DUE *******************************************/
        /*************************************** DUE DUE DUE DUE *******************************************/
        /*************************************** DUE DUE DUE DUE *******************************************/
        /*************************************** DUE DUE DUE DUE *******************************************/
        /*************************************** DUE DUE DUE DUE *******************************************/
        /*************************************** DUE DUE DUE DUE *******************************************/




//        decimal NetAmtGrandTotDue = 0;
//        decimal DueAmtGrandTotDue = 0;
//        decimal RecAmtGrandTotDue = 0;


//        string NetAmtGrandTotCommaDue = "";
//        string DueAmtGrandTotCommaDue = "";
//        string RecAmtGrandTotCommaDue = "";
        


//        public void showGrid()
//        {
//            string connectionString = ConfigurationManager.ConnectionStrings["AslDbContext"].ConnectionString;
//            SqlConnection conn = new SqlConnection(connectionString);

//            string FROMDT = Session["FromDate"].ToString();
//            string TODT = Session["ToDate"].ToString();
//            string store = Session["StoreId"].ToString();
//            DateTime FRDT = DateTime.Parse(FROMDT, dateformat, System.Globalization.DateTimeStyles.AssumeLocal);
//            string fDt = FRDT.ToString("yyyy/MM/dd");
//            DateTime ToDT = DateTime.Parse(TODT, dateformat, System.Globalization.DateTimeStyles.AssumeLocal);
//            string tDt = ToDT.ToString("yyyy/MM/dd");

//            string query = "";
//            conn.Open();

//            query = @"SELECT  CONVERT(NVARCHAR(10),TRANSDT,103) as TRANSDT , TRANSDT AS DT ,  TRANSNO, CONVERT(NVARCHAR(10),MEMODT,103) as MEMODT, MEMONO, NETAMT, DUEAMT, RECAMT FROM STK_COLLECTD  
//                    WHERE UNITID=@UNITID AND TRANSDT BETWEEN @FDT AND @TDT ORDER BY DT, TRANSNO";

            
//            SqlCommand cmd = new SqlCommand(query, conn);

//            cmd.Parameters.AddWithValue("UNITID", store);
//            cmd.Parameters.AddWithValue("FDT", fDt);
//            cmd.Parameters.AddWithValue("TDT", tDt);

//            SqlDataAdapter da = new SqlDataAdapter(cmd);
//            DataSet ds = new DataSet();
//            da.Fill(ds);
//            conn.Close();
//            if (ds.Tables[0].Rows.Count > 0)
//            {
//                GridView1.DataSource = ds;
//                GridView1.DataBind();
//                GridView1.Visible = true;
//            }
//            else
//            {
//                //Label1.Visible = true;
//                //Label1.Text = "No Data Found.";
//            }
//        }


//        protected void GridView1_RowCreated(object sender, GridViewRowEventArgs e)
//        {
//            bool IsSubTotalRowNeedToAdd = false;
//            //bool IsGrandTotalRowNeedtoAdd = false;
//            if ((strPreviousRowID != string.Empty) && (DataBinder.Eval(e.Row.DataItem, "TRANSDT") != null))
//                if (strPreviousRowID != DataBinder.Eval(e.Row.DataItem, "TRANSDT").ToString())
//                    IsSubTotalRowNeedToAdd = true;
//            if ((strPreviousRowID != string.Empty) && (DataBinder.Eval(e.Row.DataItem, "TRANSDT") == null))
//            {
//                IsSubTotalRowNeedToAdd = true;
//                //    IsGrandTotalRowNeedtoAdd = true;
//                intSubTotalIndex = 0;
//            }
//            #region Inserting first Row and populating fist Group Header details
//            if ((strPreviousRowID == string.Empty) && (DataBinder.Eval(e.Row.DataItem, "TRANSDT") != null))
//            {
//                GridView GridView1 = (GridView)sender;
//                GridViewRow row = new GridViewRow(0, 0, DataControlRowType.DataRow, DataControlRowState.Insert);
//                TableCell cell = new TableCell();
//                cell.Text = DataBinder.Eval(e.Row.DataItem, "TRANSDT").ToString();
//                cell.ColumnSpan = 7;
//                cell.CssClass = "GroupHeaderStyle";
//                row.Cells.Add(cell);
//                GridView1.Controls[0].Controls.AddAt(e.Row.RowIndex + intSubTotalIndex, row);
//                intSubTotalIndex++;
//            }
//            #endregion
//            if (IsSubTotalRowNeedToAdd)
//            {
//                //    #region Adding Sub Total Row
//                GridView GridView1 = (GridView)sender;
//                //    // Creating a Row          
//                GridViewRow row = new GridViewRow(0, 0, DataControlRowType.DataRow, DataControlRowState.Insert);
//                //Adding Total Cell          
//                TableCell cell = new TableCell();

//                GridView1.Controls[0].Controls.AddAt(e.Row.RowIndex + intSubTotalIndex, row);
//                intSubTotalIndex++;
//                //#endregion
//                #region Adding Next Group Header Details
//                if (DataBinder.Eval(e.Row.DataItem, "TRANSDT") != null)
//                {
//                    row = new GridViewRow(0, 0, DataControlRowType.DataRow, DataControlRowState.Insert);
//                    cell = new TableCell();
//                    cell.Text = DataBinder.Eval(e.Row.DataItem, "TRANSDT").ToString();
//                    cell.ColumnSpan = 7;
//                    cell.CssClass = "GroupHeaderStyle";
//                    row.Cells.Add(cell);
//                    GridView1.Controls[0].Controls.AddAt(e.Row.RowIndex + intSubTotalIndex, row);
//                    intSubTotalIndex++;
//                }
//                #endregion

//            }

//        }

//        protected void GridView1_RowDataBound(object sender, GridViewRowEventArgs e)
//        {
//            // This is for cumulating the values       
//            if (e.Row.RowType == DataControlRowType.DataRow)
//            {
//                strPreviousRowID = DataBinder.Eval(e.Row.DataItem, "TRANSDT").ToString();


//                string TRANSNO = DataBinder.Eval(e.Row.DataItem, "TRANSNO").ToString();
//                e.Row.Cells[1].Text = "&nbsp;" + TRANSNO;

//                string MEMODT = DataBinder.Eval(e.Row.DataItem, "MEMODT").ToString();
//                e.Row.Cells[2].Text = "&nbsp;" + MEMODT;

//                string MEMONO = DataBinder.Eval(e.Row.DataItem, "MEMONO").ToString();
//                e.Row.Cells[3].Text = "&nbsp;" + MEMONO;

//                decimal NETAMT = Convert.ToDecimal(DataBinder.Eval(e.Row.DataItem, "NETAMT").ToString());
//                string NetAmount = SpellAmount.comma(NETAMT);
//                e.Row.Cells[4].Text = "&nbsp;" + NetAmount;

//                NetAmtGrandTotDue += NETAMT;
//                NetAmtGrandTotCommaDue = SpellAmount.comma(NetAmtGrandTotDue);



//                decimal DUEAMT = Convert.ToDecimal(DataBinder.Eval(e.Row.DataItem, "DUEAMT").ToString());
//                string DueAmount = SpellAmount.comma(DUEAMT);
//                e.Row.Cells[5].Text = "&nbsp;" + DueAmount;

//                DueAmtGrandTotDue += DUEAMT;
//                DueAmtGrandTotCommaDue = SpellAmount.comma(DueAmtGrandTotDue);



//                decimal RECAMT = Convert.ToDecimal(DataBinder.Eval(e.Row.DataItem, "RECAMT").ToString());
//                string RecAmount = SpellAmount.comma(RECAMT);
//                e.Row.Cells[6].Text = "&nbsp;" + RecAmount;

//                RecAmtGrandTotDue += RECAMT;
//                RecAmtGrandTotCommaDue = SpellAmount.comma(RecAmtGrandTotDue);

//            }

//            else if (e.Row.RowType == DataControlRowType.Footer)
//            {
//                e.Row.Cells[3].Text = "TOTAL :";
//                e.Row.Cells[3].HorizontalAlign = HorizontalAlign.Right;
//                //e.Row.Cells[2].Text = NetAmtGrandTotCommaDue;
//                //e.Row.Cells[2].HorizontalAlign = HorizontalAlign.Right;
//                //e.Row.Cells[3].Text = DueAmtGrandTotCommaDue;
//                //e.Row.Cells[3].HorizontalAlign = HorizontalAlign.Right;
//                e.Row.Cells[6].Text = RecAmtGrandTotCommaDue;
//                e.Row.Cells[6].HorizontalAlign = HorizontalAlign.Right;
                
//                e.Row.Font.Bold = true;
//            }

//            ShowHeader(GridView1);
//        }
        private void ShowHeader(GridView grid)
        {
            if (grid.Rows.Count > 0)
            {
                grid.UseAccessibleHeader = true;
                grid.HeaderRow.TableSection = TableRowSection.TableHeader;
            }
        }
    }
}