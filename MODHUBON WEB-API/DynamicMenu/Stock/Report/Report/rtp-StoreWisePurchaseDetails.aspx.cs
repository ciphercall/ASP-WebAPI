using System;
using System.Collections.Generic;
using System.Configuration;
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
    public partial class rtp_StoreWisePurchaseDetails : System.Web.UI.Page
    {
        string strPreviousRowID = string.Empty;
        string strPreviousRowID2 = string.Empty;

        int intSubTotalIndex = 1;
        int intSubTotalIndex2 = 1;



        // To keep track the Index of Group Total    

        decimal dblSubTotalQty = 0;
        decimal dblGrandTotalQty = 0;
        string dblSubTotalCartonQtyComma = "0";
        string dblGrandTotalQtyComma = "0";

        decimal dblSubTotalAmount = 0;
        decimal dblGrandTotalAmount = 0;
        string dblSubTotalAmountComma = "0";
        string dblGrandTotalAmountComma = "0";

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
                    lblTime.Text = td;
                    string storeName = Session["StoreNm"].ToString();
                    string fdate = Session["FromDate"].ToString();
                    string todate = Session["ToDate"].ToString();
                    string typeName = Session["TYPEName"].ToString();
                    lblFromDate.Text = fdate;
                    lblToDate.Text = todate;
                    lblStore.Text = storeName;
                    lblType.Text = typeName;
                    showGrid();
                }
                catch
                {

                }
            }
        }
        public void showGrid()
        {
            string connectionString = ConfigurationManager.ConnectionStrings["AslDbContext"].ConnectionString;
            SqlConnection conn = new SqlConnection(connectionString);

            string storeid = Session["StoreId"].ToString();
            string fdate = Session["FromDate"].ToString();
            string todate = Session["ToDate"].ToString();
            string type = Session["TYPE"].ToString();

            string query = "";

            DateTime FromDt = DateTime.Parse(fdate, dateformat, System.Globalization.DateTimeStyles.AssumeLocal);
            DateTime ToDt = DateTime.Parse(todate, dateformat, System.Globalization.DateTimeStyles.AssumeLocal);
            conn.Open();
           
            if (type == "BUY")
                query = @"SELECT 'MEMO : '+A.TRANSNO+' DATE : '+A.DATED+' FROM : '+A.ACCOUNTNM AS HEAD,A.ITEMNM, A.ITEMID,  A.QTY, A.RATE, A.AMOUNT FROM (
SELECT        STK_TRANS.TRANSNO,  CONVERT(NVARCHAR,STK_TRANS.TRANSDT, 103) DATED, GL_ACCHART.ACCOUNTNM, 
                        STK_ITEM.ITEMNM, STK_TRANS.ITEMID,  STK_TRANS.QTY, STK_TRANS.RATE, STK_TRANS.AMOUNT
FROM            STK_TRANS INNER JOIN
                         STK_ITEM ON STK_TRANS.ITEMID = STK_ITEM.ITEMID INNER JOIN
                         STK_TRANSMST ON STK_TRANS.TRANSTP = STK_TRANSMST.TRANSTP AND STK_TRANS.TRANSDT = STK_TRANSMST.TRANSDT AND STK_TRANS.TRANSNO = STK_TRANSMST.TRANSNO INNER JOIN
                         GL_ACCHART ON STK_TRANSMST.PSID = GL_ACCHART.ACCOUNTCD 
WHERE        (STK_TRANS.TRANSTP = 'BUY') AND STK_TRANS.TRANSDT BETWEEN @FROMDATE AND @TODATE AND STK_TRANS.UNITTO=@Unit
) AS A GROUP BY 'MEMO : '+A.TRANSNO+' DATE : '+A.DATED+' FROM : '+A.ACCOUNTNM,A.ITEMNM, A.ITEMID,  A.QTY, A.RATE, A.AMOUNT";


            else if (type == "SALE")
                query = @"SELECT 'MEMO : '+A.TRANSNO+' DATE : '+A.DATED+' TO : '+A.CUSTNM AS HEAD,A.ITEMNM, A.ITEMID,  A.QTY, A.RATE, A.AMOUNT FROM (
SELECT        STK_TRANS.TRANSNO,  CONVERT(NVARCHAR,STK_TRANS.TRANSDT, 103) DATED, STK_TRANSMST.CUSTNM, 
                        STK_ITEM.ITEMNM, STK_TRANS.ITEMID,  STK_TRANS.QTY, STK_TRANS.RATE, STK_TRANS.AMOUNT
FROM            STK_TRANS INNER JOIN
                         STK_ITEM ON STK_TRANS.ITEMID = STK_ITEM.ITEMID INNER JOIN
                         STK_TRANSMST ON STK_TRANS.TRANSTP = STK_TRANSMST.TRANSTP AND STK_TRANS.TRANSDT = STK_TRANSMST.TRANSDT AND STK_TRANS.TRANSNO = STK_TRANSMST.TRANSNO 
WHERE        (STK_TRANS.TRANSTP = 'SALE') AND STK_TRANS.TRANSDT BETWEEN  @FROMDATE AND @TODATE AND STK_TRANS.UNITFR=@Unit
) AS A GROUP BY 'MEMO : '+A.TRANSNO+' DATE : '+A.DATED+' TO : '+A.CUSTNM,A.ITEMNM, A.ITEMID,  A.QTY, A.RATE, A.AMOUNT";


            else if (type == "IISS")
                query = @"SELECT 'MEMO : '+A.TRANSNO+' DATE : '+A.DATED+' TO : '+A.STORETO AS HEAD,A.ITEMNM, A.ITEMID,  A.QTY, A.RATE, A.AMOUNT FROM (
SELECT        STK_TRANS.TRANSNO, CONVERT(NVARCHAR, STK_TRANS.TRANSDT, 103) AS DATED, STK_TRANSMST.CUSTNM, STK_ITEM.ITEMNM, STK_TRANS.ITEMID, STK_TRANS.QTY, STK_TRANS.RATE, 
                         STK_TRANS.AMOUNT, STK_STORE.STORENM AS STORETO, STK_STORE_1.STORENM AS STOREFR
FROM            STK_TRANS INNER JOIN
                         STK_ITEM ON STK_TRANS.ITEMID = STK_ITEM.ITEMID INNER JOIN
                         STK_TRANSMST ON STK_TRANS.TRANSTP = STK_TRANSMST.TRANSTP AND STK_TRANS.TRANSDT = STK_TRANSMST.TRANSDT AND STK_TRANS.TRANSNO = STK_TRANSMST.TRANSNO INNER JOIN
                         STK_STORE AS STK_STORE_1 ON STK_TRANSMST.UNITFR = STK_STORE_1.STOREID AND STK_TRANS.UNITFR = STK_STORE_1.STOREID INNER JOIN
                         STK_STORE ON STK_TRANSMST.UNITTO = STK_STORE.STOREID AND STK_TRANS.UNITTO = STK_STORE.STOREID
WHERE        (STK_TRANS.TRANSTP = 'IISS') AND (STK_TRANS.TRANSDT BETWEEN @FROMDATE AND @TODATE) AND (STK_TRANS.UNITFR = @Unit)) AS A 
GROUP BY 'MEMO : '+A.TRANSNO+' DATE : '+A.DATED+' TO : '+A.STORETO,A.ITEMNM, A.ITEMID,  A.QTY, A.RATE, A.AMOUNT";

            else
                query = "";

            SqlCommand cmd = new SqlCommand(query, conn);

            cmd.Parameters.AddWithValue("@Unit", storeid);
            cmd.Parameters.AddWithValue("@FROMDATE", FromDt);
            cmd.Parameters.AddWithValue("@TODATE", ToDt);

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
        protected void GridView1_RowCreated(object sender, GridViewRowEventArgs e)
        {
            bool IsSubTotalRowNeedToAdd = false;
            bool IsGrandTotalRowNeedtoAdd = false;
            if ((strPreviousRowID != string.Empty) && (DataBinder.Eval(e.Row.DataItem, "HEAD") != null))
                if (strPreviousRowID != DataBinder.Eval(e.Row.DataItem, "HEAD").ToString())
                    IsSubTotalRowNeedToAdd = true;
            if ((strPreviousRowID != string.Empty) && (DataBinder.Eval(e.Row.DataItem, "HEAD") == null))
            {
                IsSubTotalRowNeedToAdd = true;
                //    IsGrandTotalRowNeedtoAdd = true;
                intSubTotalIndex = 0;
            }
            #region Inserting first Row and populating fist Group Header details
            if ((strPreviousRowID == string.Empty) && (DataBinder.Eval(e.Row.DataItem, "HEAD") != null))
            {
                GridView GridView1 = (GridView)sender;
                GridViewRow row = new GridViewRow(0, 0, DataControlRowType.DataRow, DataControlRowState.Insert);
                TableCell cell = new TableCell();
                cell.Text = "" + DataBinder.Eval(e.Row.DataItem, "HEAD").ToString();
                cell.ColumnSpan = 4;
                cell.CssClass = "GroupHeaderStyle";
                row.Cells.Add(cell);
                GridView1.Controls[0].Controls.AddAt(e.Row.RowIndex + intSubTotalIndex, row);
                intSubTotalIndex++;
            }
            #endregion
            if (IsSubTotalRowNeedToAdd)
            {
                //    #region Adding Sub Total Row
                GridView GridView1 = (GridView)sender;
                //    // Creating a Row          
                GridViewRow row = new GridViewRow(0, 0, DataControlRowType.DataRow, DataControlRowState.Insert);
                //Adding Total Cell          
                TableCell cell = new TableCell();
                cell.Text = "Memo No Wise Total";
                cell.HorizontalAlign = HorizontalAlign.Right;
                cell.ColumnSpan = 1;
                cell.CssClass = "SubTotalRowStyle";
                row.Cells.Add(cell);

                //Adding Carton Column         
                cell = new TableCell();
                cell.Text = string.Format("{0:0.00}", dblSubTotalCartonQtyComma);
                cell.HorizontalAlign = HorizontalAlign.Center;
                cell.CssClass = "SubTotalRowStyle";
                row.Cells.Add(cell);

                cell = new TableCell();
                cell.Text = "";
                cell.HorizontalAlign = HorizontalAlign.Center;
                cell.CssClass = "SubTotalRowStyle";
                row.Cells.Add(cell);


                //    //Adding Pieces Column         
                cell = new TableCell();
                cell.Text = string.Format("{0:0.00}", dblSubTotalAmountComma);
                cell.HorizontalAlign = HorizontalAlign.Center;
                cell.CssClass = "SubTotalRowStyleAmount";
                row.Cells.Add(cell);

                GridView1.Controls[0].Controls.AddAt(e.Row.RowIndex + intSubTotalIndex, row);
                intSubTotalIndex++;
                //#endregion
                #region Adding Next Group Header Details
                if (DataBinder.Eval(e.Row.DataItem, "HEAD") != null)
                {
                    row = new GridViewRow(0, 0, DataControlRowType.DataRow, DataControlRowState.Insert);
                    cell = new TableCell();
                    cell.Text = "" + DataBinder.Eval(e.Row.DataItem, "HEAD").ToString();
                    cell.ColumnSpan = 4;
                    cell.CssClass = "GroupHeaderStyle";
                    row.Cells.Add(cell);
                    GridView1.Controls[0].Controls.AddAt(e.Row.RowIndex + intSubTotalIndex, row);
                    intSubTotalIndex++;
                }
                #endregion
                #region Reseting the Sub Total Variables

                dblSubTotalQty = 0;
                dblSubTotalAmount = 0;
                #endregion
            }
        }

        protected void GridView1_RowDataBound(object sender, GridViewRowEventArgs e)
        {
            // This is for cumulating the values       
            if (e.Row.RowType == DataControlRowType.DataRow)
            {
                strPreviousRowID = DataBinder.Eval(e.Row.DataItem, "HEAD").ToString();


                string ItemNM = DataBinder.Eval(e.Row.DataItem, "ITEMNM").ToString();
                e.Row.Cells[0].Text = "&nbsp;" + ItemNM;


                decimal Qty = Convert.ToDecimal(DataBinder.Eval(e.Row.DataItem, "QTY").ToString());
                e.Row.Cells[1].Text = "&nbsp;" + Qty;

                dblSubTotalQty += Qty;
                dblSubTotalCartonQtyComma = SpellAmount.comma(dblSubTotalQty);
                dblGrandTotalQty += Qty;
                dblGrandTotalQtyComma = SpellAmount.comma(dblGrandTotalQty);

                string RATE = DataBinder.Eval(e.Row.DataItem, "RATE").ToString();
                e.Row.Cells[2].Text = "&nbsp;" + RATE;

                decimal AMOUNT = Convert.ToDecimal(DataBinder.Eval(e.Row.DataItem, "AMOUNT").ToString());
                e.Row.Cells[3].Text = "&nbsp;" + AMOUNT;

                dblSubTotalAmount += AMOUNT;
                dblSubTotalAmountComma = SpellAmount.comma(dblSubTotalAmount);
                dblGrandTotalAmount += AMOUNT;
                dblGrandTotalAmountComma = SpellAmount.comma(dblGrandTotalAmount);

            }
            // This is for cumulating the values  
            else if (e.Row.RowType == DataControlRowType.Footer)
            {
                e.Row.Cells[0].Text = "Total : ";
                e.Row.Cells[0].HorizontalAlign = HorizontalAlign.Right;
                e.Row.Cells[1].Text = dblGrandTotalQtyComma;
                e.Row.Cells[1].HorizontalAlign = HorizontalAlign.Center;
                e.Row.Cells[3].Text = dblGrandTotalAmountComma;
                e.Row.Cells[3].HorizontalAlign = HorizontalAlign.Right;

                e.Row.Font.Bold = true;
            }

            ShowHeader(GridView1);
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

        //protected void GridView1_RowCommand(object sender, GridViewCommandEventArgs e)
        //{
        //    if (e.CommandName == "Select")
        //    {
        //        string productID = GridView1.Rows[Convert.ToInt32(e.CommandArgument.ToString())].Cells[0].Text;
        //        string productName = GridView1.Rows[Convert.ToInt32(e.CommandArgument.ToString())].Cells[1].Text;
        //        //lblProductID.Text = productID;
        //        //lblProduct.Text = productName;
        //    }
        //}

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
