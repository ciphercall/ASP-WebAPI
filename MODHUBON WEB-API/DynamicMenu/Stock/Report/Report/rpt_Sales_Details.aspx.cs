using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Threading;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using AlchemyAccounting;

namespace DynamicMenu.Stock.Report.Report
{
    public partial class rpt_Sales_Details : System.Web.UI.Page
    {


        string strPreviousRowID = string.Empty;
        string strPreviousRowID2 = string.Empty;

        int intSubTotalIndex = 1;
        int intSubTotalIndex2 = 1;



        // To keep track the Index of Group Total    

        decimal dblSubTotalQty = 0;
        decimal dblSubTotalQty1 = 0;
        decimal dblGrandTotalQty1 = 0;
        decimal dblGrandTotalQty = 0;
     
        string dblSubTotalCartonQtyComma = "0";
        string dblSubTotalCartonQtyComma1 = "0";
        string dblGrandTotalQtyComma = "0";
        string dblGrandTotalQtyComma1 = "0";

        decimal dblSubTotalRate = 0;
        decimal dblGrandTotalRate = 0;
        string dblSubTotalCartonRateComma = "0";
        string dblGrandTotalRateComma = "0";

        decimal dblSubTotalAmount = 0;
        decimal dblSubTotalAmount2 = 0;
        decimal dblSubTotalAmount3 = 0;
        decimal dblSubTotalAmount4 = 0;
        decimal dblSubTotalAmount5 = 0;

        decimal dblGrandTotalAmount = 0;
        decimal dblGrandTotalAmount2 = 0;
        decimal dblGrandTotalAmount3 = 0;
        decimal dblGrandTotalAmount4 = 0;
        decimal dblGrandTotalAmount5 = 0;
     
        string dblSubTotalAmountomma = "0";
        string dblSubTotalAmountomma2 = "0";
        string dblSubTotalAmountomma3 = "0";
        string dblSubTotalAmountomma4 = "0";
        string dblSubTotalAmountomma5 = "0";

        string dblGrandTotalAmountComma = "0";
        string dblGrandTotalAmountComma2 = "0";
        string dblGrandTotalAmountComma3 = "0";
        string dblGrandTotalAmountComma4 = "0";
        string dblGrandTotalAmountComma5 = "0";

        IFormatProvider dateformat = new System.Globalization.CultureInfo("fr-FR", true);
        protected void Page_Load(object sender, EventArgs e)
        {

            if (Session["USERID"] == null)
            {
                Response.Redirect("~/Login/UI/Login.aspx");
            }
            else
            {
                try
                {
                    dbFunctions.lblAdd(@"SELECT COMPNM FROM ASL_COMPANY WHERE COMPID ='101'", lblCompNM);
                    dbFunctions.lblAdd(@"SELECT ADDRESS FROM ASL_COMPANY WHERE COMPID ='101'", lblAddress);

                    DateTime PrintDate = dbFunctions.Timezone(DateTime.Now);
                    string td = PrintDate.ToString("dd-MMM-yyyy hh:mm tt");
                   // lblTime.Text = td;

                    showGrid();
                    showGrid2();
                    showGrid3();
                    showGrid4();
                    showGrid5();
                }
                catch(Exception x)
                {
                    //ignore
                }
            }
        }
        public void showGrid()
        {
            SqlConnection conn = new SqlConnection(dbFunctions.connection);


            //Session["Date"] = txtTransDate.Text;
            //Session["PartyId"] = txtPartyName.Text;
            //Session["vehicleNO"] = txtVehicalsNo.Text;
            //Session["driverNM"] = txtdriverNM.Text;
            //Session["AssstNM"] = txtasstNM.Text;

            string memoNO = Session["MemoNO"].ToString();
            lblMemoNo.Text = memoNO;

            string partyid = Session["PartyId"].ToString();
            dbFunctions.lblAdd("SELECT PARTYNM FROM STK_PARTY WHERE PARTYID='"+partyid+"'", lblPartyNm);
            lblParty.Text = lblPartyNm.Text;

            string dateFrom = Session["Date"].ToString();
            DateTime datefrom = DateTime.Parse(dateFrom, dateformat, System.Globalization.DateTimeStyles.AssumeLocal);
            string from = datefrom.ToString("yyyy/MM/dd");
            lblToDate.Text = dateFrom;


            string VehiclNO = Session["vehicleNO"].ToString();
            lblvehilceNO.Text = VehiclNO;

            string driverNM = Session["driverNM"].ToString();
            lblDriverNM.Text = driverNM;

            string asstNM=Session["AssstNM"].ToString();
            lblasstNM.Text = asstNM;


            //string dateTo = Session["To"].ToString();
            //DateTime datato = DateTime.Parse(dateTo, dateformat, System.Globalization.DateTimeStyles.AssumeLocal);
            //string to = datato.ToString("yyyy/MM/dd");

            //        lblFromDate.Text = datefrom.ToString("dd-MM-yyyy");
            // lblToDate.Text = datato.ToString("dd-MM-yyyy");


            //'Order Date: ' + REPLACE(CONVERT(NVARCHAR, STK_TRANSMST.TRANSDT, 6), ' ', '-') + ', Time: ' + SUBSTRING(CONVERT(NVARCHAR, STK_TRANSMST.INTIME, 108), 0, 6)
            //+ ', Invoice No: ' + STK_TRANSMST.TRANSNO + ', Bill Date: ' +
            //REPLACE(CONVERT(NVARCHAR, STK_TRANSMST.BILLDT, 6), ' ', '-')  AS NAME,
            dbFunctions.lblAdd("SELECT SUM(ISNULL(OPAMT,0)) OPAMT FROM( " +
                               "SELECT SUM(ISNULL(DEBITAMT, 0)) - SUM(ISNULL(CREDITAMT, 0)) OPAMT, 0 CCHGAMT, 0 RECAMT, 0 OPAMTR " +
                               "FROM GL_MASTER WHERE DEBITCD = '1020201' + '"+partyid+"' AND TRANSDT < '"+ from + "' " +
                               "UNION SELECT 0 OPAMT, SUM(ISNULL(DEBITAMT, 0)) CCHGAMT, 0 RECAMT, 0 OPAMTR " +
                               "FROM GL_MASTER WHERE DEBITCD = '1020201' + '" + partyid + "' AND TRANSDT = '" + from + "' AND TRANSTP = 'JOUR' AND CREDITCD='302010100001' " +
                               "UNION SELECT 0 OPAMT, 0 CCHGAMT, SUM(ISNULL(DEBITAMT, 0)) RECAMT, 0 OPAMTR " +
                               "FROM GL_MASTER WHERE CREDITCD = '1020201' + '" + partyid + "' AND TRANSDT = '" + from + "' AND TRANSTP = 'MREC' " +
                               "UNION SELECT 0 OPAMT, 0 CCHGAMT, 0 RECAMT, (SUM(ISNULL(SALAMT, 0)) - SUM(ISNULL(RTSAMT, 0))) OPAMTR FROM( " +
                               "SELECT SUM(ISNULL(NETAMT, 0)) SALAMT, 0 RTSAMT " +
                               "FROM STK_TRANS WHERE TRANSTP = 'SALE' AND TRANSDT < '" + from + "' AND PSID = '20010' AND SUBSTRING(ITEMID, 1, 5) = '10110' " +
                               "UNION SELECT 0 SALAMT, SUM(ISNULL(NETAMT, 0)) RTSAMT " +
                               "FROM STK_TRANS WHERE TRANSTP = 'IRTS' AND TRANSDT < '" + from + "' AND PSID = '20010' AND SUBSTRING(ITEMID, 1, 5) = '10110') A) A", lblopamnt);


            dbFunctions.lblAdd("SELECT SUM(ISNULL(CCHGAMT,0)) CCHGAMT FROM( " +
                               "SELECT SUM(ISNULL(DEBITAMT, 0)) - SUM(ISNULL(CREDITAMT, 0)) OPAMT, 0 CCHGAMT, 0 RECAMT, 0 OPAMTR " +
                               "FROM GL_MASTER WHERE DEBITCD = '1020201' + '" + partyid + "' AND TRANSDT < '" + from + "' " +
                               "UNION SELECT 0 OPAMT, SUM(ISNULL(DEBITAMT, 0)) CCHGAMT, 0 RECAMT, 0 OPAMTR " +
                               "FROM GL_MASTER WHERE DEBITCD = '1020201' + '" + partyid + "' AND TRANSDT = '" + from + "' AND TRANSTP = 'JOUR' AND CREDITCD='302010100001' " +
                               "UNION SELECT 0 OPAMT, 0 CCHGAMT, SUM(ISNULL(DEBITAMT, 0)) RECAMT, 0 OPAMTR " +
                               "FROM GL_MASTER WHERE CREDITCD = '1020201' + '" + partyid + "' AND TRANSDT = '" + from + "' AND TRANSTP = 'MREC' " +
                               "UNION SELECT 0 OPAMT, 0 CCHGAMT, 0 RECAMT, (SUM(ISNULL(SALAMT, 0)) - SUM(ISNULL(RTSAMT, 0))) OPAMTR FROM( " +
                               "SELECT SUM(ISNULL(NETAMT, 0)) SALAMT, 0 RTSAMT " +
                               "FROM STK_TRANS WHERE TRANSTP = 'SALE' AND TRANSDT < '" + from + "' AND PSID = '20010' AND SUBSTRING(ITEMID, 1, 5) = '10110' " +
                               "UNION SELECT 0 SALAMT, SUM(ISNULL(NETAMT, 0)) RTSAMT " +
                               "FROM STK_TRANS WHERE TRANSTP = 'IRTS' AND TRANSDT < '" + from + "' AND PSID = '20010' AND SUBSTRING(ITEMID, 1, 5) = '10110') A) A", lblcost);
     


            //  decimal gross = Convert.ToDecimal(lblgross.Text);

          



            string query = "";


            conn.Open();

            query = $@"SELECT  ROW_NUMBER() OVER (ORDER BY A.TRANSSL) AS SL,  C.USERNM, A.TRANSSL, B.ITEMNM, B.ITEMNMB, 
            B.ITEMNMB AS Expr1, A.REMARKS, ISNULL(A.QTY, 0) AS QTY,A.RETRT,A.RATE, A.QTY*A.RATE AS AMOUNT
            FROM STK_TRANS AS A INNER JOIN
            STK_ITEM AS B ON A.ITEMID = B.ITEMID INNER JOIN
            STK_TRANSMST ON A.COMPID = STK_TRANSMST.COMPID AND A.TRANSTP = STK_TRANSMST.TRANSTP 
            AND A.TRANSYY = STK_TRANSMST.TRANSYY AND 
            A.TRANSNO = STK_TRANSMST.TRANSNO LEFT OUTER JOIN
            STK_USERPS AS C ON A.PSID = C.PSID AND A.USERID = C.USERCD
            WHERE STK_TRANSMST.TRANSTP = 'SALE' AND STK_TRANSMST.TRANSDT =  '{from}' AND STK_TRANSMST.TRANSNO='{memoNO}'  AND STK_TRANSMST.PSID = '{partyid}'
            ORDER BY STK_TRANSMST.TRANSDT, STK_TRANSMST.TRANSNO, STK_TRANSMST.BILLDT, TRANSSL";

            SqlCommand cmd = new SqlCommand(query, conn);

            SqlDataAdapter da = new SqlDataAdapter(cmd);
            DataSet ds = new DataSet();
            da.Fill(ds);
            conn.Close();
            if (ds.Tables[0].Rows.Count > 0)
            {
                GridView1.DataSource = ds;
                GridView1.DataBind();
                GridView1.Visible = true;

            }
            else
            {
                Label1.Visible = true;
                Label1.Text = "No Data Found.";
            }



        }
      

        protected void GridView1_OnRowCreated(object sender, GridViewRowEventArgs e)
        {
        //    bool IsSubTotalRowNeedToAdd = false;
        //    bool IsGrandTotalRowNeedtoAdd = false;
        //    if ((strPreviousRowID != string.Empty) && (DataBinder.Eval(e.Row.DataItem, "NAME") != null))
        //        if (strPreviousRowID != DataBinder.Eval(e.Row.DataItem, "NAME").ToString())
        //            IsSubTotalRowNeedToAdd = true;
        //    if ((strPreviousRowID != string.Empty) && (DataBinder.Eval(e.Row.DataItem, "NAME") == null))
        //    {
        //        IsSubTotalRowNeedToAdd = true;
        //        //    IsGrandTotalRowNeedtoAdd = true;
        //        intSubTotalIndex = 0;
        //    }
        //    #region Inserting first Row and populating fist Group Header details
        //    if ((strPreviousRowID == string.Empty) && (DataBinder.Eval(e.Row.DataItem, "NAME") != null))
        //    {
        //        GridView GridView1 = (GridView)sender;
        //        GridViewRow row = new GridViewRow(0, 0, DataControlRowType.DataRow, DataControlRowState.Insert);
        //        TableCell cell = new TableCell();
        //        cell.Text = "" + DataBinder.Eval(e.Row.DataItem, "NAME");
        //        cell.ColumnSpan = 5;
        //        cell.CssClass = "GroupHeaderStyle";
        //        row.Cells.Add(cell);
        //        GridView1.Controls[0].Controls.AddAt(e.Row.RowIndex + intSubTotalIndex, row);
        //        intSubTotalIndex++;
        //    }
        //    #endregion
        //    if (IsSubTotalRowNeedToAdd)
        //    {
        //        //    #region Adding Sub Total Row
        //        GridView GridView1 = (GridView)sender;
        //        //    // Creating a Row          
        //        GridViewRow row = new GridViewRow(0, 0, DataControlRowType.DataRow, DataControlRowState.Insert);
        //        //Adding Total Cell          
        //        TableCell cell = new TableCell();
        //        cell.Text = "Total";
        //        cell.HorizontalAlign = HorizontalAlign.Right;
        //        cell.ColumnSpan = 2;
        //        cell.CssClass = "SubTotalRowStyle";
        //        row.Cells.Add(cell);

        //        //Adding Carton Column         
        //        cell = new TableCell();
        //        cell.Text = string.Format("{0:0.00}", dblSubTotalCartonQtyComma);
        //        cell.HorizontalAlign = HorizontalAlign.Center;
        //        cell.CssClass = "SubTotalRowStyle";
        //        row.Cells.Add(cell);

        //        cell = new TableCell();
        //        cell.Text = "";
        //        cell.HorizontalAlign = HorizontalAlign.Center;
        //        cell.CssClass = "SubTotalRowStyle";
        //        row.Cells.Add(cell);

        //        cell = new TableCell();
        //        cell.Text = string.Format("{0:0.00}", dblSubTotalAmountomma);
        //        cell.HorizontalAlign = HorizontalAlign.Center;
        //        cell.CssClass = "SubTotalRowStyle";
        //        row.Cells.Add(cell);

        //        GridView1.Controls[0].Controls.AddAt(e.Row.RowIndex + intSubTotalIndex, row);
        //        intSubTotalIndex++;
        //        //#endregion
        //        #region Adding Next Group Header Details
        //        if (DataBinder.Eval(e.Row.DataItem, "NAME") != null)
        //        {
        //            row = new GridViewRow(0, 0, DataControlRowType.DataRow, DataControlRowState.Insert);
        //            cell = new TableCell();
        //            cell.Text = "" + DataBinder.Eval(e.Row.DataItem, "NAME").ToString();
        //            cell.ColumnSpan = 5;
        //            cell.CssClass = "GroupHeaderStyle";
        //            row.Cells.Add(cell);
        //            GridView1.Controls[0].Controls.AddAt(e.Row.RowIndex + intSubTotalIndex, row);
        //            intSubTotalIndex++;
        //        }
        //        #endregion
        //        #region Reseting the Sub Total Variables

        //        dblSubTotalQty = 0;
        //        #endregion
        //    }
        }
        protected void GridView1_OnRowDataBound(object sender, GridViewRowEventArgs e)
        {
            // This is for cumulating the values       
            if (e.Row.RowType == DataControlRowType.DataRow)
            {
                // strPreviousRowID = DataBinder.Eval(e.Row.DataItem, "NAME").ToString();

                string SL = DataBinder.Eval(e.Row.DataItem, "SL").ToString();
                e.Row.Cells[0].Text = "&nbsp;" + SL;

                string ItemNM = DataBinder.Eval(e.Row.DataItem, "ITEMNM").ToString();
                e.Row.Cells[1].Text = "&nbsp;" + ItemNM;

                string ITEMNMB = DataBinder.Eval(e.Row.DataItem, "ITEMNMB").ToString();
                e.Row.Cells[2].Text = "&nbsp;" + ITEMNMB;

                decimal Qty = Convert.ToDecimal(DataBinder.Eval(e.Row.DataItem, "QTY"));
                e.Row.Cells[3].Text = Qty + "&nbsp;";


                string RETRT = DataBinder.Eval(e.Row.DataItem, "RETRT").ToString();
                e.Row.Cells[5].Text = "&nbsp;" + RETRT;
                //decimal RETRT = Convert.ToDecimal(DataBinder.Eval(e.Row.DataItem, "RETRT"));
                //e.Row.Cells[5].Text = RETRT + "&nbsp;";

                decimal RATE = Convert.ToDecimal(DataBinder.Eval(e.Row.DataItem, "RATE"));
                e.Row.Cells[4].Text = RATE + "&nbsp;";
                
                decimal AMOUNT = Convert.ToDecimal(DataBinder.Eval(e.Row.DataItem, "AMOUNT").ToString());
                e.Row.Cells[6].Text = AMOUNT.ToString("F2") + "&nbsp;";

               // dblSubTotalQty += Qty;
               // dblSubTotalCartonQtyComma = SpellAmount.comma(dblSubTotalQty);
               // dblGrandTotalQty += Qty;
             //   dblGrandTotalQtyComma = SpellAmount.comma(dblGrandTotalQty);

                dblSubTotalAmount += Convert.ToDecimal(AMOUNT.ToString("f2"));
                dblSubTotalAmountomma = SpellAmount.comma(dblSubTotalAmount);
                dblGrandTotalAmount += Convert.ToDecimal(AMOUNT.ToString("f2"));
                dblGrandTotalAmountComma = SpellAmount.comma(dblGrandTotalAmount);

                lbltotalS.Text = dblGrandTotalAmountComma;

            }
            // This is for cumulating the values  
            else if (e.Row.RowType == DataControlRowType.Footer)
            {
                e.Row.Cells[2].Text = "Total : ";
                e.Row.Cells[2].HorizontalAlign = HorizontalAlign.Right;
               // e.Row.Cells[3].Text = dblGrandTotalQtyComma;
               // e.Row.Cells[3].HorizontalAlign = HorizontalAlign.Right;
                e.Row.Cells[6].Text = dblGrandTotalAmountComma;
                e.Row.Cells[6].HorizontalAlign = HorizontalAlign.Right;

                decimal gtotal = Convert.ToDecimal(e.Row.Cells[6].Text);
                decimal opam = Convert.ToDecimal(lblopamnt.Text);
                decimal cost = Convert.ToDecimal(lblcost.Text);
                lblgross.Text = (opam + cost + gtotal).ToString("f2");
                e.Row.Font.Bold = true;
            }

            ShowHeader(GridView1);
        }
        public void showGrid2()
        {
            SqlConnection conn = new SqlConnection(dbFunctions.connection);


            //Session["Date"] = txtTransDate.Text;
            //Session["PartyId"] = txtPartyName.Text;
            //Session["vehicleNO"] = txtVehicalsNo.Text;
            //Session["driverNM"] = txtdriverNM.Text;
            //Session["AssstNM"] = txtasstNM.Text;

            string memoNO = Session["MemoNO"].ToString();
            lblMemoNo.Text = memoNO;

            string partyid = Session["PartyId"].ToString();
            dbFunctions.lblAdd("SELECT PARTYNM FROM STK_PARTY WHERE PARTYID='" + partyid + "'", lblPartyNm);
            lblParty.Text = lblPartyNm.Text;

            string dateFrom = Session["Date"].ToString();
            DateTime datefrom = DateTime.Parse(dateFrom, dateformat, System.Globalization.DateTimeStyles.AssumeLocal);
            string from = datefrom.ToString("yyyy/MM/dd");

            string VehiclNO = Session["vehicleNO"].ToString();
            lblvehilceNO.Text = VehiclNO;

            string driverNM = Session["driverNM"].ToString();
            lblDriverNM.Text = driverNM;

            string asstNM = Session["AssstNM"].ToString();
            lblasstNM.Text = asstNM;

            dbFunctions.lblAdd("SELECT  SUM(ISNULL(RECAMT,0)) RECAMT FROM( " +
                               "SELECT SUM(ISNULL(DEBITAMT, 0)) - SUM(ISNULL(CREDITAMT, 0)) OPAMT, 0 CCHGAMT, 0 RECAMT, 0 OPAMTR " +
                               "FROM GL_MASTER WHERE DEBITCD = '1020201' + '" + partyid + "' AND TRANSDT < '" + from + "' " +
                               "UNION SELECT 0 OPAMT, SUM(ISNULL(DEBITAMT, 0)) CCHGAMT, 0 RECAMT, 0 OPAMTR " +
                               "FROM GL_MASTER WHERE DEBITCD = '1020201' + '" + partyid + "' AND TRANSDT = '" + from + "' AND TRANSTP = 'JOUR' AND CREDITCD='302010100001' " +
                               "UNION SELECT 0 OPAMT, 0 CCHGAMT, SUM(ISNULL(DEBITAMT, 0)) RECAMT, 0 OPAMTR " +
                               "FROM GL_MASTER WHERE CREDITCD = '1020201' + '" + partyid + "' AND TRANSDT = '" + from + "' AND TRANSTP = 'MREC' " +
                               "UNION SELECT 0 OPAMT, 0 CCHGAMT, 0 RECAMT, (SUM(ISNULL(SALAMT, 0)) - SUM(ISNULL(RTSAMT, 0))) OPAMTR FROM( " +
                               "SELECT SUM(ISNULL(NETAMT, 0)) SALAMT, 0 RTSAMT " +
                               "FROM STK_TRANS WHERE TRANSTP = 'SALE' AND TRANSDT < '" + from + "' AND PSID = '20010' AND SUBSTRING(ITEMID, 1, 5) = '10110' " +
                               "UNION SELECT 0 SALAMT, SUM(ISNULL(NETAMT, 0)) RTSAMT " +
                               "FROM STK_TRANS WHERE TRANSTP = 'IRTS' AND TRANSDT < '" + from + "' AND PSID = '20010' AND SUBSTRING(ITEMID, 1, 5) = '10110') A) A", lblrcvamnt);


            string query = "";


            conn.Open();

            query = $@"SELECT ROW_NUMBER() OVER (ORDER BY A.TRANSSL) AS SL, ITEMNMB ITEMNM_R,B.ITEMNM, ISNULL(QTY,0) QTY_R, ISNULL(RATE,0) RATE_R, ISNULL(NETAMT,0) AMOUNT_R
FROM STK_TRANS A INNER JOIN STK_ITEM B ON A.ITEMID = B.ITEMID
WHERE TRANSTP = 'IRTS' AND TRANSDT = '{from}' AND PSID = '{partyid}' AND SUBSTRING(A.ITEMID,1,5)<>'10110'
ORDER BY ITEMNMB";

            SqlCommand cmd = new SqlCommand(query, conn);

            SqlDataAdapter da = new SqlDataAdapter(cmd);
            DataSet ds = new DataSet();
            da.Fill(ds);
            conn.Close();
            if (ds.Tables[0].Rows.Count > 0)
            {
                GridView2.DataSource = ds;
                GridView2.DataBind();
                GridView2.Visible = true;

            }
            else
            {
                Label1.Visible = true;
                Label1.Text = "No Data Found.";
            }



        }

        protected void GridView2_OnRowDataBound(object sender, GridViewRowEventArgs e)
        {
            // This is for cumulating the values       
            if (e.Row.RowType == DataControlRowType.DataRow)
            {
                // strPreviousRowID = DataBinder.Eval(e.Row.DataItem, "NAME").ToString();

                string SL = DataBinder.Eval(e.Row.DataItem, "SL").ToString();
                e.Row.Cells[0].Text = "&nbsp;" + SL;

                string ITEMNMB = DataBinder.Eval(e.Row.DataItem, "ITEMNM").ToString();
                e.Row.Cells[1].Text = "&nbsp;" + ITEMNMB;

                string ItemNM = DataBinder.Eval(e.Row.DataItem, "ITEMNM_R").ToString();
                e.Row.Cells[2].Text = "&nbsp;" + ItemNM;

                

                decimal Qty = Convert.ToDecimal(DataBinder.Eval(e.Row.DataItem, "QTY_R"));
                e.Row.Cells[3].Text = Qty + "&nbsp;";

                //decimal RETRT = Convert.ToDecimal(DataBinder.Eval(e.Row.DataItem, "RETRT"));
                //e.Row.Cells[4].Text = RETRT + "&nbsp;";

                decimal RATE = Convert.ToDecimal(DataBinder.Eval(e.Row.DataItem, "RATE_R"));
                e.Row.Cells[4].Text = RATE + "&nbsp;";

                decimal AMOUNT = Convert.ToDecimal(DataBinder.Eval(e.Row.DataItem, "AMOUNT_R").ToString());
                e.Row.Cells[5].Text = AMOUNT.ToString("F2") + "&nbsp;";

                // dblSubTotalQty += Qty;
                // dblSubTotalCartonQtyComma = SpellAmount.comma(dblSubTotalQty);
                // dblGrandTotalQty += Qty;
                //   dblGrandTotalQtyComma = SpellAmount.comma(dblGrandTotalQty);

                dblSubTotalAmount2 += Convert.ToDecimal(AMOUNT.ToString("f2"));
                dblSubTotalAmountomma2 = SpellAmount.comma(dblSubTotalAmount2);
                dblGrandTotalAmount2 += Convert.ToDecimal(AMOUNT.ToString("f2"));
                dblGrandTotalAmountComma2 = SpellAmount.comma(dblGrandTotalAmount2);

            }
            // This is for cumulating the values  
            else if (e.Row.RowType == DataControlRowType.Footer)
            {
                e.Row.Cells[2].Text = "Total : ";
                e.Row.Cells[2].HorizontalAlign = HorizontalAlign.Right;
                // e.Row.Cells[3].Text = dblGrandTotalQtyComma;
                // e.Row.Cells[3].HorizontalAlign = HorizontalAlign.Right;
                e.Row.Cells[5].Text = dblGrandTotalAmountComma2;
                e.Row.Cells[5].HorizontalAlign = HorizontalAlign.Right;

                lblTotal.Text = e.Row.Cells[5].Text;
                decimal grosst = Convert.ToDecimal(lblgross.Text);
                decimal gt = Convert.ToDecimal(e.Row.Cells[5].Text);
                decimal rcvamnt = Convert.ToDecimal(lblrcvamnt.Text);

                lblnetamnt.Text = (grosst- gt-rcvamnt).ToString("f2");
                
                

                e.Row.Font.Bold = true;
            }

            ShowHeader(GridView2);

        }





        public void showGrid3()
        {
            SqlConnection conn = new SqlConnection(dbFunctions.connection);

            string memoNO = Session["MemoNO"].ToString();
            lblMemoNo.Text = memoNO;

            string partyid = Session["PartyId"].ToString();
            dbFunctions.lblAdd("SELECT PARTYNM FROM STK_PARTY WHERE PARTYID='" + partyid + "'", lblPartyNm);
            lblParty.Text = lblPartyNm.Text;

            string dateFrom = Session["Date"].ToString();
            DateTime datefrom = DateTime.Parse(dateFrom, dateformat, System.Globalization.DateTimeStyles.AssumeLocal);
            string from = datefrom.ToString("yyyy/MM/dd");

            string VehiclNO = Session["vehicleNO"].ToString();
            lblvehilceNO.Text = VehiclNO;

            string driverNM = Session["driverNM"].ToString();
            lblDriverNM.Text = driverNM;

            string asstNM = Session["AssstNM"].ToString();
            lblasstNM.Text = asstNM;



            dbFunctions.lblAdd("SELECT  SUM(ISNULL(OPAMTR,0)) OPAMTR FROM( " +
                               "SELECT SUM(ISNULL(DEBITAMT, 0)) - SUM(ISNULL(CREDITAMT, 0)) OPAMT, 0 CCHGAMT, 0 RECAMT, 0 OPAMTR " +
                               "FROM GL_MASTER WHERE DEBITCD = '1020201' + '" + partyid + "' AND TRANSDT < '" + from + "' " +
                               "UNION SELECT 0 OPAMT, SUM(ISNULL(DEBITAMT, 0)) CCHGAMT, 0 RECAMT, 0 OPAMTR " +
                               "FROM GL_MASTER WHERE DEBITCD = '1020201' + '" + partyid + "' AND TRANSDT = '" + from + "' AND TRANSTP = 'JOUR' AND CREDITCD='302010100001' " +
                               "UNION SELECT 0 OPAMT, 0 CCHGAMT, SUM(ISNULL(DEBITAMT, 0)) RECAMT, 0 OPAMTR " +
                               "FROM GL_MASTER WHERE CREDITCD = '1020201' + '" + partyid + "' AND TRANSDT = '" + from + "' AND TRANSTP = 'MREC' " +
                               "UNION SELECT 0 OPAMT, 0 CCHGAMT, 0 RECAMT, (SUM(ISNULL(SALAMT, 0)) - SUM(ISNULL(RTSAMT, 0))) OPAMTR FROM( " +
                               "SELECT SUM(ISNULL(NETAMT, 0)) SALAMT, 0 RTSAMT " +
                               "FROM STK_TRANS WHERE TRANSTP = 'SALE' AND TRANSDT < '" + from + "' AND PSID = '20010' AND SUBSTRING(ITEMID, 1, 5) = '10110' " +
                               "UNION SELECT 0 SALAMT, SUM(ISNULL(NETAMT, 0)) RTSAMT " +
                               "FROM STK_TRANS WHERE TRANSTP = 'IRTS' AND TRANSDT < '" + from + "' AND PSID = '20010' AND SUBSTRING(ITEMID, 1, 5) = '10110') A) A", lblopAmountR);

            string query = "";


            conn.Open();

            query = $@"SELECT ROW_NUMBER() OVER (ORDER BY A.TRANSSL) AS SL,ITEMNMB ITEMNM_TS,B.ITEMNM, ISNULL(QTY,0) QTY_TS, ISNULL(RATE,0) RATE_TS, ISNULL(NETAMT,0) AMOUNT_TS
FROM STK_TRANS A INNER JOIN STK_ITEM B ON A.ITEMID = B.ITEMID
WHERE TRANSTP = 'SALE' AND TRANSDT = '{from}' AND PSID = '{partyid}' AND SUBSTRING(A.ITEMID,1,5)='10110'
ORDER BY ITEMNMB";

            SqlCommand cmd = new SqlCommand(query, conn);

            SqlDataAdapter da = new SqlDataAdapter(cmd);
            DataSet ds = new DataSet();
            da.Fill(ds);
            conn.Close();
            if (ds.Tables[0].Rows.Count > 0)
            {
                GridView3.DataSource = ds;
                GridView3.DataBind();
                GridView3.Visible = true;

            }
            else
            {
                Label1.Visible = true;
                Label1.Text = "No Data Found.";
            }



        }



        protected void GridView3_OnRowDataBound(object sender, GridViewRowEventArgs e)
        {

            // This is for cumulating the values       
            if (e.Row.RowType == DataControlRowType.DataRow)
            {
                // strPreviousRowID = DataBinder.Eval(e.Row.DataItem, "NAME").ToString();

                string SL = DataBinder.Eval(e.Row.DataItem, "SL").ToString();
                e.Row.Cells[0].Text = "&nbsp;" + SL;

                string ITEMNMB = DataBinder.Eval(e.Row.DataItem, "ITEMNM").ToString();
                e.Row.Cells[1].Text = "&nbsp;" + ITEMNMB;

                string ItemNM = DataBinder.Eval(e.Row.DataItem, "ITEMNM_TS").ToString();
                e.Row.Cells[2].Text = "&nbsp;" + ItemNM;



                decimal Qty = Convert.ToDecimal(DataBinder.Eval(e.Row.DataItem, "QTY_TS"));
                e.Row.Cells[3].Text = Qty + "&nbsp;";

                //decimal RETRT = Convert.ToDecimal(DataBinder.Eval(e.Row.DataItem, "RETRT"));
                //e.Row.Cells[4].Text = RETRT + "&nbsp;";

                decimal RATE = Convert.ToDecimal(DataBinder.Eval(e.Row.DataItem, "RATE_TS"));
                e.Row.Cells[4].Text = RATE + "&nbsp;";

                decimal AMOUNT = Convert.ToDecimal(DataBinder.Eval(e.Row.DataItem, "AMOUNT_TS").ToString());
                e.Row.Cells[5].Text = AMOUNT.ToString("F2") + "&nbsp;";

                // dblSubTotalQty += Qty;
                // dblSubTotalCartonQtyComma = SpellAmount.comma(dblSubTotalQty);
                // dblGrandTotalQty += Qty;
                //   dblGrandTotalQtyComma = SpellAmount.comma(dblGrandTotalQty);

                dblSubTotalAmount3 += Convert.ToDecimal(AMOUNT.ToString("f2"));
                dblSubTotalAmountomma3 = SpellAmount.comma(dblSubTotalAmount2);
                dblGrandTotalAmount3 += Convert.ToDecimal(AMOUNT.ToString("f2"));
                dblGrandTotalAmountComma3 = SpellAmount.comma(dblGrandTotalAmount2);


                lbltotalOp.Text =dblGrandTotalAmountComma3;

            }
            // This is for cumulating the values  
            else if (e.Row.RowType == DataControlRowType.Footer)
            {
                e.Row.Cells[2].Text = "Total : ";
                e.Row.Cells[2].HorizontalAlign = HorizontalAlign.Right;
                // e.Row.Cells[3].Text = dblGrandTotalQtyComma;
                // e.Row.Cells[3].HorizontalAlign = HorizontalAlign.Right;
                e.Row.Cells[5].Text = dblGrandTotalAmountComma3;
                e.Row.Cells[5].HorizontalAlign = HorizontalAlign.Right;

             //   lbltotalOp.Text = e.Row.Cells[5].Text;



                e.Row.Font.Bold = true;
            }

            ShowHeader(GridView3);
        }


        

        public void showGrid4()
        {
            SqlConnection conn = new SqlConnection(dbFunctions.connection);

            string memoNO = Session["MemoNO"].ToString();
            lblMemoNo.Text = memoNO;

            string partyid = Session["PartyId"].ToString();
            dbFunctions.lblAdd("SELECT PARTYNM FROM STK_PARTY WHERE PARTYID='" + partyid + "'", lblPartyNm);
            lblParty.Text = lblPartyNm.Text;

            string dateFrom = Session["Date"].ToString();
            DateTime datefrom = DateTime.Parse(dateFrom, dateformat, System.Globalization.DateTimeStyles.AssumeLocal);
            string date = datefrom.ToString("yyyy/MM/dd");

            string VehiclNO = Session["vehicleNO"].ToString();
            lblvehilceNO.Text = VehiclNO;

            string driverNM = Session["driverNM"].ToString();
            lblDriverNM.Text = driverNM;

            string asstNM = Session["AssstNM"].ToString();
            lblasstNM.Text = asstNM;

           // dbFunctions.lblAdd("", lblCLAmnt);

            string query = "";


            conn.Open();

            query = $@"
SELECT ROW_NUMBER() OVER (ORDER BY A.TRANSSL) AS SL, ITEMNMB ITEMNM_TR,B.ITEMNM, ISNULL(QTY,0) QTY_TR, ISNULL(RATE,0) RATE_TR, ISNULL(NETAMT,0) AMOUNT_TR
FROM STK_TRANS A INNER JOIN STK_ITEM B ON A.ITEMID = B.ITEMID
WHERE TRANSTP = 'IRTS' AND TRANSDT = '{date}' AND PSID = '{partyid}' AND SUBSTRING(A.ITEMID,1,5)='10110'
ORDER BY ITEMNMB";

            SqlCommand cmd = new SqlCommand(query, conn);

            SqlDataAdapter da = new SqlDataAdapter(cmd);
            DataSet ds = new DataSet();
            da.Fill(ds);
            conn.Close();
            if (ds.Tables[0].Rows.Count > 0)
            {
                GridView4.DataSource = ds;
                GridView4.DataBind();
                GridView4.Visible = true;

            }
            else
            {
                Label1.Visible = true;
                Label1.Text = "No Data Found.";
            }



        }

        protected void GridView4_OnRowDataBound(object sender, GridViewRowEventArgs e)
        {
            // This is for cumulating the values       
            if (e.Row.RowType == DataControlRowType.DataRow)
            {
                // strPreviousRowID = DataBinder.Eval(e.Row.DataItem, "NAME").ToString();

                string SL = DataBinder.Eval(e.Row.DataItem, "SL").ToString();
                e.Row.Cells[0].Text = "&nbsp;" + SL;

                string ITEMNMB = DataBinder.Eval(e.Row.DataItem, "ITEMNM").ToString();
                e.Row.Cells[1].Text = "&nbsp;" + ITEMNMB;

                string ItemNM = DataBinder.Eval(e.Row.DataItem, "ITEMNM_TR").ToString();
                e.Row.Cells[2].Text = "&nbsp;" + ItemNM;



                decimal Qty = Convert.ToDecimal(DataBinder.Eval(e.Row.DataItem, "QTY_TR"));
                e.Row.Cells[3].Text = Qty + "&nbsp;";

                //decimal RETRT = Convert.ToDecimal(DataBinder.Eval(e.Row.DataItem, "RETRT"));
                //e.Row.Cells[4].Text = RETRT + "&nbsp;";

                decimal RATE = Convert.ToDecimal(DataBinder.Eval(e.Row.DataItem, "RATE_TR"));
                e.Row.Cells[4].Text = RATE + "&nbsp;";

                decimal AMOUNT = Convert.ToDecimal(DataBinder.Eval(e.Row.DataItem, "AMOUNT_TR").ToString());
                e.Row.Cells[5].Text = AMOUNT.ToString("F2") + "&nbsp;";

                // dblSubTotalQty += Qty;
                // dblSubTotalCartonQtyComma = SpellAmount.comma(dblSubTotalQty);
                // dblGrandTotalQty += Qty;
                //   dblGrandTotalQtyComma = SpellAmount.comma(dblGrandTotalQty);

                dblSubTotalAmount4 += Convert.ToDecimal(AMOUNT.ToString("f2"));
                dblSubTotalAmountomma4 = SpellAmount.comma(dblSubTotalAmount2);
                dblGrandTotalAmount4 += Convert.ToDecimal(AMOUNT.ToString("f2"));
                dblGrandTotalAmountComma4 = SpellAmount.comma(dblGrandTotalAmount2);

            }
            // This is for cumulating the values  
            else if (e.Row.RowType == DataControlRowType.Footer)
            {
                e.Row.Cells[2].Text = "Total : ";
                e.Row.Cells[2].HorizontalAlign = HorizontalAlign.Right;
                // e.Row.Cells[3].Text = dblGrandTotalQtyComma;
                // e.Row.Cells[3].HorizontalAlign = HorizontalAlign.Right;
                e.Row.Cells[5].Text = dblGrandTotalAmountComma4;
                e.Row.Cells[5].HorizontalAlign = HorizontalAlign.Right;


                decimal opamnt = Convert.ToDecimal(lblopAmountR.Text);
                decimal Groosamnt = Convert.ToDecimal(dblGrandTotalAmountComma3);
                decimal clgrand = Convert.ToDecimal(e.Row.Cells[5].Text);

                lblTo.Text = e.Row.Cells[5].Text;

                lblCLAmnt.Text = (opamnt + Groosamnt - clgrand).ToString("f2");

                e.Row.Font.Bold = true;
            }

            ShowHeader(GridView4);
        }


        public void showGrid5()
        {
            SqlConnection conn = new SqlConnection(dbFunctions.connection);

            string memoNO = Session["MemoNO"].ToString();
            lblMemoNo.Text = memoNO;

            string partyid = Session["PartyId"].ToString();
            dbFunctions.lblAdd("SELECT PARTYNM FROM STK_PARTY WHERE PARTYID='" + partyid + "'", lblPartyNm);
            lblParty.Text = lblPartyNm.Text;

            string dateFrom = Session["Date"].ToString();
            DateTime datefrom = DateTime.Parse(dateFrom, dateformat, System.Globalization.DateTimeStyles.AssumeLocal);
            string date = datefrom.ToString("yyyy/MM/dd");

            string VehiclNO = Session["vehicleNO"].ToString();
            lblvehilceNO.Text = VehiclNO;

            string driverNM = Session["driverNM"].ToString();
            lblDriverNM.Text = driverNM;

            string asstNM = Session["AssstNM"].ToString();
            lblasstNM.Text = asstNM;


            string query = "";


            conn.Open();

            query = $@"
SELECT ROW_NUMBER() OVER (ORDER BY ITEMNM) AS SL,ITEMNMB ITEMNM_RS,B.ITEMNM, (SUM(ISNULL(SALQTY,0))-SUM(ISNULL(RTSQTY,0))) QTY_RS, ISNULL(SALRT,0) RATE_RS, (SUM(ISNULL(SALQTY,0))-SUM(ISNULL(RTSQTY,0)))*ISNULL(SALRT,0) AMOUNT_RS FROM(
SELECT ITEMID, SUM(ISNULL(QTY,0)) SALQTY, 0 RTSQTY FROM STK_TRANS
WHERE TRANSTP = 'SALE' AND TRANSDT <= '{date}' AND PSID = '{partyid}' AND SUBSTRING(ITEMID,1,5)='10110' GROUP BY ITEMID
UNION SELECT ITEMID, 0 SALQTY, SUM(ISNULL(QTY,0)) RTSQTY FROM STK_TRANS
WHERE TRANSTP = 'IRTS' AND TRANSDT <= '{date}' AND PSID = '{partyid}' AND SUBSTRING(ITEMID,1,5)='10110' GROUP BY ITEMID
) A INNER JOIN STK_ITEM B ON A.ITEMID = B.ITEMID GROUP BY ITEMNMB,ITEMNM, SALRT
ORDER BY SL";

            SqlCommand cmd = new SqlCommand(query, conn);

            SqlDataAdapter da = new SqlDataAdapter(cmd);
            DataSet ds = new DataSet();
            da.Fill(ds);
            conn.Close();
            if (ds.Tables[0].Rows.Count > 0)
            {
                GridView5.DataSource = ds;
                GridView5.DataBind();
                GridView5.Visible = true;

            }
            else
            {
                Label1.Visible = true;
                Label1.Text = "No Data Found.";
            }



        }


        protected void GridView5_OnRowDataBound(object sender, GridViewRowEventArgs e)
        {

            // This is for cumulating the values       
            if (e.Row.RowType == DataControlRowType.DataRow)
            {
                // strPreviousRowID = DataBinder.Eval(e.Row.DataItem, "NAME").ToString();

                string SL = DataBinder.Eval(e.Row.DataItem, "SL").ToString();
                e.Row.Cells[0].Text = "&nbsp;" + SL;

                string ITEMNMB = DataBinder.Eval(e.Row.DataItem, "ITEMNM").ToString();
                e.Row.Cells[1].Text = "&nbsp;" + ITEMNMB;

                string ItemNM = DataBinder.Eval(e.Row.DataItem, "ITEMNM_RS").ToString();
                e.Row.Cells[2].Text = "&nbsp;" + ItemNM;



                decimal Qty = Convert.ToDecimal(DataBinder.Eval(e.Row.DataItem, "QTY_RS"));
                e.Row.Cells[3].Text = Qty + "&nbsp;";

                //decimal RETRT = Convert.ToDecimal(DataBinder.Eval(e.Row.DataItem, "RETRT"));
                //e.Row.Cells[4].Text = RETRT + "&nbsp;";

                decimal RATE = Convert.ToDecimal(DataBinder.Eval(e.Row.DataItem, "RATE_RS"));
                e.Row.Cells[4].Text = RATE + "&nbsp;";

                decimal AMOUNT = Convert.ToDecimal(DataBinder.Eval(e.Row.DataItem, "AMOUNT_RS").ToString());
                e.Row.Cells[5].Text = AMOUNT.ToString("F2") + "&nbsp;";

                // dblSubTotalQty += Qty;
                // dblSubTotalCartonQtyComma = SpellAmount.comma(dblSubTotalQty);
                // dblGrandTotalQty += Qty;
                //   dblGrandTotalQtyComma = SpellAmount.comma(dblGrandTotalQty);

                dblSubTotalAmount5 += Convert.ToDecimal(AMOUNT.ToString("f2"));
                dblSubTotalAmountomma5 = SpellAmount.comma(dblSubTotalAmount2);
                dblGrandTotalAmount5 += Convert.ToDecimal(AMOUNT.ToString("f2"));
                dblGrandTotalAmountComma5 = SpellAmount.comma(dblGrandTotalAmount2);

            }
            // This is for cumulating the values  
            else if (e.Row.RowType == DataControlRowType.Footer)
            {
                e.Row.Cells[2].Text = "Total : ";
                e.Row.Cells[2].HorizontalAlign = HorizontalAlign.Right;
                // e.Row.Cells[3].Text = dblGrandTotalQtyComma;
                // e.Row.Cells[3].HorizontalAlign = HorizontalAlign.Right;
                e.Row.Cells[5].Text = dblGrandTotalAmountComma4;
                e.Row.Cells[5].HorizontalAlign = HorizontalAlign.Right;

                lblnetTotal.Text = e.Row.Cells[5].Text;

                decimal netrcv = Convert.ToDecimal(lblnetamnt.Text);
                decimal clamount = Convert.ToDecimal(lblCLAmnt.Text);

                lblclAmountNT.Text = (netrcv - clamount).ToString("f2");


                e.Row.Font.Bold = true;
            }

            ShowHeader(GridView5);
        }
        private void ShowHeader(GridView grid)
        {
            if (grid.Rows.Count > 0)
            {
                grid.UseAccessibleHeader = true;
                grid.HeaderRow.TableSection = TableRowSection.TableHeader;
                //gridView.HeaderRow.Style["display"] = "table-header-group";
            }
        }




        string parseValueIntoCurrency(double number)
        {
            // set currency format
            string curCulture = Thread.CurrentThread.CurrentCulture.ToString();
            System.Globalization.NumberFormatInfo currencyFormat = new
                System.Globalization.CultureInfo(curCulture).NumberFormat;

            currencyFormat.CurrencyNegativePattern = 1;

            return number.ToString("c", currencyFormat);
        }


      
    }
}