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

namespace AlchemyAccounting.Stock.Report.Report
{
    public partial class rptClosingStock : System.Web.UI.Page
    {
        IFormatProvider dateformat = new System.Globalization.CultureInfo("fr-FR", true);

        // To keep track of the previous row Group Identifier    
        string strPreviousRowID = string.Empty;
        // To keep track the Index of Group Total    
        int intSubTotalIndex = 1;
        // To temporarily store Sub Total    
        decimal dblSubTotalCartonQty = 0;
        decimal dblSubTotalPieces = 0;
        decimal dblSubTotalCLQty = 0;
        //decimal dblSubTotalRate = 0;
        decimal dblSubTotalAmount = 0;
        // To temporarily store Grand Total    
        decimal dblGrandTotalCartonQty = 0;
        decimal dblGrandTotalPieces = 0;
        decimal dblGrandTotalCLQty = 0;
        //decimal dblGrandTotalRate = 0;
        decimal dblGrandTotalAmount = 0;
        //string AmountComma = "";
        string dblSubTotalCartonQtyComma = "0";
        string dblSubTotalPiecesComma = "0";
        string dblSubTotalCLQtyComma = "0";
        string dblSubTotalRateComma = "";
        string dblSubTotalAmountComma = "0";

        string dblGrandTotalCartonQtyComma = "0";
        string dblGrandTotalPiecesComma = "0";
        string dblGrandTotalCLQtyComma = "0";
        string dblGrandTotalRateComma = "";
        string dblGrandTotalAmountComma = "0";

        protected void Page_Load(object sender, EventArgs e)
        {
            try
            {
                string uTp = HttpContext.Current.Session["USERTYPE"].ToString();
                string brCD = HttpContext.Current.Session["BrCD"].ToString();
                SqlCommand cmd = new SqlCommand();

                dbFunctions.lblAdd(@"SELECT COMPNM FROM ASL_COMPANY WHERE COMPID ='101'", lblCompNM);
                dbFunctions.lblAdd(@"SELECT ADDRESS FROM ASL_COMPANY WHERE COMPID ='101'", lblAddress);


                DateTime PrintDate = dbFunctions.Timezone(DateTime.Now);
                string td = PrintDate.ToString("dd-MMM-yyyy hh:mm tt");
                lblTime.Text = td;

                string StoreNM = Session["StoreNM"].ToString();
                string StoreID = Session["StoreID"].ToString();
                string Date = Session["Date"].ToString();

                DateTime asON = DateTime.Parse(Date, dateformat, System.Globalization.DateTimeStyles.AssumeLocal);
                lblDate.Text = asON.ToString("dd-MMM-yyyy");
                lblStoreNM.Text = StoreNM;
                showGrid();
            }
            catch
            {
            }
        }

        public void showGrid()
        {
            string connectionString = ConfigurationManager.ConnectionStrings["AslDbContext"].ConnectionString;
            SqlConnection conn = new SqlConnection(connectionString);

            string StoreNM = Session["StoreNM"].ToString();
            string StoreID = Session["StoreID"].ToString();
            string Date = Session["Date"].ToString();
            DateTime asON = DateTime.Parse(Date, dateformat, System.Globalization.DateTimeStyles.AssumeLocal);
            string aOn = asON.ToString("yyyy/MM/dd");

            conn.Open();
            SqlCommand cmd = new SqlCommand(@"SELECT SUBSTRING(B.ITEMID,1,5) AS SUBID, STK_ITEM.AUTHOR, 
                    CASE WHEN STK_ITEM.VERSION=0 THEN '' ELSE CAST(VERSION AS NVARCHAR) +' Edition' end VERSION,
                    B.ITEMID, B.CLQTY, B.AVGRATE, (CASE WHEN B.CLQTY < 0 THEN 0.00 ELSE B.CLQTY * B.AVGRATE END) AS STOCKVALUE, 
                    STK_ITEMMST.SUBNM, STK_ITEM.ITEMNM , STK_ITEM.ITEMCD,  ISNULL(STK_ITEM.PQTY,0) AS PQTY, 
                    ISNULL((CASE WHEN STK_ITEM.PQTY = 0 THEN 0 ELSE FLOOR(B.CLQTY/PQTY)END),0) AS CARTONQTY, 
                    ISNULL((B.CLQTY - (CASE WHEN STK_ITEM.PQTY = 0 THEN 0 ELSE (FLOOR(B.CLQTY/PQTY)*PQTY) END)),0) AS PIECES  
                    FROM (SELECT  ITEMID, (SUM(INQTY) + SUM(BQTY) + SUM(SALERET)) - (SUM(OUTQTY) + SUM(SQTY) + SUM(PURCHASERET)) AS CLQTY, 
                    SUM(BQTY) AS BQTY, SUM(BAMT) AS BAMT, SUM(SQTY) AS SQTY,  SUM(SAMT) AS SAMT, 
                    (CASE WHEN SUM(BAMTR) = 0.00 THEN 0.00 ELSE CONVERT(decimal(18, 2), (SUM(BAMTR)) / SUM(BQTYR)) END) AS AVGRATE  
                    FROM ( 
                    SELECT  ITEMID, 0 AS BQTY, 0 AS BAMT, SUM(QTY) AS SQTY, SUM(AMOUNT) AS SAMT, 0 AS INQTY, 0 AS OUTQTY, 0 AS BQTYR, 
                    0 AS SALERET, 0 AS PURCHASERET, 0 AS BAMTR  FROM STK_TRANS AS STK_TRANS_1  
                    WHERE (TRANSDT <= @DATE) AND (TRANSTP = 'SALE') AND (UNITFR = @STOREID)  
                    GROUP BY  ITEMID   
                    UNION
                    SELECT ITEMID, 0 AS BQTY, 0 AS BAMT, 0 AS SQTY, 0 AS SAMT, 0 AS INQTY, 
                    0 AS OUTQTY, 0 AS BQTYR, SUM(QTY) AS SALERET, 0 AS PURCHASERET, 0 AS BAMTR  
                    FROM STK_TRANS AS STK_TRANS_2  
                    WHERE (TRANSDT <= @DATE) AND (TRANSTP = 'IRTS')  AND (UNITTO = @STOREID)  
                    GROUP BY  ITEMID  
                    UNION
                    SELECT  ITEMID, 0 AS BQTY, SUM(AMOUNT) AS BAMT, 0 AS SQTY, 0 AS SAMT, SUM(QTY) AS INQTY, 0 AS OUTQTY, 
                    0 AS BQTYR, 0 AS SALERET, 0 AS PURCHASERET, 0 AS BAMTR  FROM STK_TRANS  
                    WHERE (TRANSDT <= @DATE) AND (TRANSTP = 'BUY') AND (UNITTO = @STOREID) 
                    GROUP BY  ITEMID  
                    UNION  
                    SELECT  ITEMID, 0 AS BQTY, 0 AS BAMT, 0 AS SQTY, SUM(AMOUNT) AS SAMT, 0 AS INQTY, SUM(QTY) AS OUTQTY, 0 AS BQTYR, 
                    0 AS SALERET, 0 AS PURCHASERET, 0 AS BAMTR  FROM STK_TRANS AS STK_TRANS_1  
                    WHERE (TRANSDT <= @DATE) AND (TRANSTP = 'IISS') AND (UNITFR = @STOREID)  
                    GROUP BY  ITEMID   
                    UNION  
                    SELECT  ITEMID, 0 AS BQTY, 0 AS BAMT, 0 AS SQTY, 0 AS SAMT, 0 AS INQTY, 0 AS OUTQTY,SUM(QTY) AS BQTYR, 
                    0 AS SALERET, 0 AS PURCHASERET, SUM(AMOUNT) AS BAMTR  
                    FROM STK_TRANS  
                    WHERE (TRANSDT <= @DATE) AND (TRANSTP = 'BUY')   AND (UNITTO = @STOREID) 
                    GROUP BY  ITEMID ) AS A   GROUP BY  ITEMID) AS B INNER JOIN  STK_ITEM ON SUBSTRING(B.ITEMID,1,5) = STK_ITEM.SUBID AND 
                    B.ITEMID = STK_ITEM.ITEMID INNER JOIN  STK_ITEMMST ON STK_ITEM.SUBID = STK_ITEMMST.SUBID  
                    WHERE (B.CLQTY <> 0)
                    ", conn);
            cmd.Parameters.AddWithValue("@DATE", aOn);
            cmd.Parameters.AddWithValue("@STOREID", StoreID);
            cmd.CommandTimeout = 0;
            SqlDataAdapter da = new SqlDataAdapter(cmd);
            DataSet ds = new DataSet();
            da.Fill(ds);
            conn.Close();
            if (ds.Tables[0].Rows.Count > 0)
            {
                Label1.Visible = false;
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

        /// <summary>   
        /// /// Event fires for every row creation   
        /// /// Used for creating SubTotal row when next group starts by adding Group Total at previous row manually    
        /// </summary>    /// <param name="sender"></param>    /// <param name="e"></param>   
        protected void GridView1_RowCreated(object sender, GridViewRowEventArgs e)
        {
            bool IsSubTotalRowNeedToAdd = false;
            bool IsGrandTotalRowNeedtoAdd = false;
            if ((strPreviousRowID != string.Empty) && (DataBinder.Eval(e.Row.DataItem, "SUBID") != null))
                if (strPreviousRowID != DataBinder.Eval(e.Row.DataItem, "SUBID").ToString())
                    IsSubTotalRowNeedToAdd = true;
            if ((strPreviousRowID != string.Empty) && (DataBinder.Eval(e.Row.DataItem, "SUBID") == null))
            {
                IsSubTotalRowNeedToAdd = true;
                IsGrandTotalRowNeedtoAdd = true;
                intSubTotalIndex = 0;
            }
            #region Inserting first Row and populating fist Group Header details
            if ((strPreviousRowID == string.Empty) && (DataBinder.Eval(e.Row.DataItem, "SUBID") != null))
            {
                GridView GridView1 = (GridView)sender;
                GridViewRow row = new GridViewRow(0, 0, DataControlRowType.DataRow, DataControlRowState.Insert);
                TableCell cell = new TableCell();
                cell.Text = "Category Particulars : " + DataBinder.Eval(e.Row.DataItem, "SUBNM").ToString();
                cell.ColumnSpan = 7;
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

                cell = new TableCell();
                //cell.Text = string.Format("{0:0.00}", dblSubTotalCLQtyComma);
                cell.HorizontalAlign = HorizontalAlign.Center;
                cell.CssClass = "SubTotalRowStyle";
                row.Cells.Add(cell);


                cell = new TableCell();
                cell.Text = "";
                cell.HorizontalAlign = HorizontalAlign.Left;
                //cell.ColumnSpan = 2;
                cell.CssClass = "SubTotalRowStyle";
                row.Cells.Add(cell);

                cell = new TableCell();
                cell.Text = "";
                cell.HorizontalAlign = HorizontalAlign.Left;
                //cell.ColumnSpan = 2;
                cell.CssClass = "SubTotalRowStyle";
                row.Cells.Add(cell);

                cell = new TableCell();
                cell.Text = "Category Wise Total";
                cell.HorizontalAlign = HorizontalAlign.Left;
                //cell.ColumnSpan = 2;
                cell.CssClass = "SubTotalRowStyle";
                row.Cells.Add(cell);

                //Adding Carton Column         
                //cell = new TableCell();
                //cell.Text = string.Format("{0:0.00}", dblSubTotalCartonQtyComma);
                //cell.HorizontalAlign = HorizontalAlign.Right;
                //cell.CssClass = "SubTotalRowStyle";
                //row.Cells.Add(cell);

                //Adding Pieces Column         
                //cell = new TableCell();
                //cell.Text = string.Format("{0:0.00}", dblSubTotalPiecesComma);
                //cell.HorizontalAlign = HorizontalAlign.Right;
                //cell.CssClass = "SubTotalRowStyle";
                //row.Cells.Add(cell);

                //Adding CLQTY Column         
                cell = new TableCell();
                cell.Text = string.Format("{0:0.00}", dblSubTotalCLQtyComma);
                cell.HorizontalAlign = HorizontalAlign.Center;
                cell.CssClass = "SubTotalRowStyle";
                row.Cells.Add(cell);

                //Adding Amount Column         
                cell = new TableCell();
                cell.Text = string.Format("{0:0.00}", dblSubTotalRateComma);
                cell.HorizontalAlign = HorizontalAlign.Right;
                cell.CssClass = "SubTotalRowStyle";
                row.Cells.Add(cell);

                //Adding Amount Column         
                cell = new TableCell();
                cell.Text = string.Format("{0:0.00}", dblSubTotalAmountComma);
                cell.HorizontalAlign = HorizontalAlign.Right;
                cell.CssClass = "SubTotalRowStyle";
                row.Cells.Add(cell);
                //Adding the Row at the RowIndex position in the Grid      
                GridView1.Controls[0].Controls.AddAt(e.Row.RowIndex + intSubTotalIndex, row);
                intSubTotalIndex++;
                #endregion
                #region Adding Next Group Header Details
                if (DataBinder.Eval(e.Row.DataItem, "SUBID") != null)
                {
                    row = new GridViewRow(0, 0, DataControlRowType.DataRow, DataControlRowState.Insert);
                    cell = new TableCell();
                    cell.Text = "Category Particulars : " + DataBinder.Eval(e.Row.DataItem, "SUBNM").ToString();
                    cell.ColumnSpan = 6;
                    cell.CssClass = "GroupHeaderStyle";
                    row.Cells.Add(cell);
                    GridView1.Controls[0].Controls.AddAt(e.Row.RowIndex + intSubTotalIndex, row);
                    intSubTotalIndex++;
                }
                #endregion
                #region Reseting the Sub Total Variables
                dblSubTotalCartonQty = 0;
                dblSubTotalPieces = 0;
                dblSubTotalCLQty = 0;
                dblSubTotalAmount = 0;
                #endregion
            }
            if (IsGrandTotalRowNeedtoAdd)
            {
                #region Grand Total Row
                GridView GridView1 = (GridView)sender;
                // Creating a Row      
                GridViewRow row = new GridViewRow(0, 0, DataControlRowType.DataRow, DataControlRowState.Insert);
                //Adding Total Cell           
                TableCell cell = new TableCell();
                cell = new TableCell();
                //cell.Text = string.Format("{0:0.00}", dblGrandTotalCLQtyComma);
                cell.HorizontalAlign = HorizontalAlign.Right;
                cell.CssClass = "GrandTotalRowStyle";
                row.Cells.Add(cell);


                cell = new TableCell();
                cell.Text = "";
                cell.HorizontalAlign = HorizontalAlign.Left;
                //cell.ColumnSpan = 2;
                cell.CssClass = "GrandTotalRowStyle";
                row.Cells.Add(cell);

                cell = new TableCell();
                cell.Text = "";
                cell.HorizontalAlign = HorizontalAlign.Left;
                //cell.ColumnSpan = 2;
                cell.CssClass = "GrandTotalRowStyle";
                row.Cells.Add(cell);

                cell = new TableCell();
                cell.Text = "Store Wise Total";
                cell.HorizontalAlign = HorizontalAlign.Left;
                //cell.ColumnSpan = 2;
                cell.CssClass = "GrandTotalRowStyle";
                row.Cells.Add(cell);

                ////Adding Carton Qty Column          
                //cell = new TableCell();
                //cell.Text = string.Format("{0:0.00}", dblGrandTotalCartonQtyComma);
                //cell.HorizontalAlign = HorizontalAlign.Right;
                //cell.CssClass = "GrandTotalRowStyle";
                //row.Cells.Add(cell);

                ////Adding Pieces Column           
                //cell = new TableCell();
                //cell.Text = string.Format("{0:0.00}", dblGrandTotalPiecesComma);
                //cell.HorizontalAlign = HorizontalAlign.Right;
                //cell.CssClass = "GrandTotalRowStyle";
                //row.Cells.Add(cell);

                ////Adding CLQty Column          
                cell = new TableCell();
                cell.Text = string.Format("{0:0.00}", dblGrandTotalCLQtyComma);
                cell.HorizontalAlign = HorizontalAlign.Center;
                cell.CssClass = "GrandTotalRowStyle";
                row.Cells.Add(cell);

                //Adding Amount Column          
                cell = new TableCell();
                cell.Text = string.Format("{0:0.00}", dblGrandTotalRateComma);
                cell.HorizontalAlign = HorizontalAlign.Right;
                cell.CssClass = "GrandTotalRowStyle";
                row.Cells.Add(cell);

                //Adding Amount Column          
                cell = new TableCell();
                cell.Text = string.Format("{0:0.00}", dblGrandTotalAmountComma);
                cell.HorizontalAlign = HorizontalAlign.Right;
                cell.CssClass = "GrandTotalRowStyle";
                row.Cells.Add(cell);
                //Adding the Row at the RowIndex position in the Grid     
                GridView1.Controls[0].Controls.AddAt(e.Row.RowIndex, row);
                #endregion
            }
        }

        /// <summary>    
        /// Event fires when data binds to each row   
        /// Used for calculating Group Total     
        /// </summary>   
        /// /// <param name="sender"></param>    
        /// <param name="e"></param>    
        protected void GridView1_RowDataBound(object sender, GridViewRowEventArgs e)
        {
            // This is for cumulating the values       
            if (e.Row.RowType == DataControlRowType.DataRow)
            {
                strPreviousRowID = DataBinder.Eval(e.Row.DataItem, "SUBID").ToString();
                string ITEMCD = DataBinder.Eval(e.Row.DataItem, "ITEMCD").ToString();
                e.Row.Cells[0].Text = "&nbsp;" + ITEMCD;

                string ItemNM = DataBinder.Eval(e.Row.DataItem, "ITEMNM").ToString();
                e.Row.Cells[1].Text = "&nbsp;" + ItemNM;

                string author = DataBinder.Eval(e.Row.DataItem, "AUTHOR").ToString();
                e.Row.Cells[2].Text = "&nbsp;" + author;


                string version = DataBinder.Eval(e.Row.DataItem, "VERSION").ToString();
                e.Row.Cells[3].Text = "&nbsp;" + version;

                decimal Car_Qty = Convert.ToDecimal(DataBinder.Eval(e.Row.DataItem, "CARTONQTY").ToString());
                string CARTONQTY = SpellAmount.comma(Car_Qty);
                e.Row.Cells[4].Text = CARTONQTY;

                decimal dblCartonQty = Convert.ToDecimal(DataBinder.Eval(e.Row.DataItem, "CARTONQTY").ToString());

                decimal P_Qty = Convert.ToDecimal(DataBinder.Eval(e.Row.DataItem, "PIECES").ToString());
                string PIECES = SpellAmount.comma(P_Qty);
                e.Row.Cells[5].Text = PIECES;

                decimal dblPieces = Convert.ToDecimal(DataBinder.Eval(e.Row.DataItem, "PIECES").ToString());

                decimal CL_Qty = Convert.ToDecimal(DataBinder.Eval(e.Row.DataItem, "CLQTY").ToString());
                string CLQTY = SpellAmount.comma(CL_Qty);
                e.Row.Cells[6].Text = CLQTY;

                decimal dblCLQTY = Convert.ToDecimal(DataBinder.Eval(e.Row.DataItem, "CLQTY").ToString());

                decimal dblAmount = Convert.ToDecimal(DataBinder.Eval(e.Row.DataItem, "STOCKVALUE").ToString());

                decimal rt = (dblAmount / dblCLQTY);
                string RATE = SpellAmount.comma(rt);
                e.Row.Cells[7].Text = RATE;



                decimal Stk_V = Convert.ToDecimal(DataBinder.Eval(e.Row.DataItem, "STOCKVALUE").ToString());
                string STOCKVALUE = SpellAmount.comma(Stk_V);
                e.Row.Cells[8].Text = STOCKVALUE;


                // Cumulating Sub Total            
                dblSubTotalCartonQty += dblCartonQty;
                dblSubTotalCartonQtyComma = SpellAmount.comma(dblSubTotalCartonQty);
                dblSubTotalPieces += dblPieces;
                dblSubTotalPiecesComma = SpellAmount.comma(dblSubTotalPieces);
                dblSubTotalCLQty += dblCLQTY;
                dblSubTotalCLQtyComma = SpellAmount.comma(dblSubTotalCLQty);
                //dblSubTotalRate += rt;
                //dblSubTotalRateComma = SpellAmount.comma(dblSubTotalRate);
                dblSubTotalAmount += dblAmount;
                dblSubTotalAmountComma = SpellAmount.comma(dblSubTotalAmount);
                // Cumulating Grand Total           
                dblGrandTotalCartonQty += dblCartonQty;
                dblGrandTotalCartonQtyComma = SpellAmount.comma(dblGrandTotalCartonQty);
                dblGrandTotalPieces += dblPieces;
                dblGrandTotalPiecesComma = SpellAmount.comma(dblGrandTotalPieces);
                dblGrandTotalCLQty += dblCLQTY;
                dblGrandTotalCLQtyComma = SpellAmount.comma(dblGrandTotalCLQty);
                //dblGrandTotalRate += rt;
                //dblGrandTotalRateComma = SpellAmount.comma(dblGrandTotalRate);

                dblGrandTotalAmount += dblAmount;
                dblGrandTotalAmountComma = SpellAmount.comma(dblGrandTotalAmount);

                // This is for cumulating the values  
                //if (e.Row.RowType == DataControlRowType.DataRow)
                //{
                //    e.Row.Attributes.Add("onmouseover", "this.style.backgroundColor='#ddd'");
                //    e.Row.Attributes.Add("onmouseout", "this.style.backgroundColor=''");
                //    e.Row.Attributes.Add("style", "cursor:pointer;");
                //    e.Row.Attributes["onclick"] = ClientScript.GetPostBackClientHyperlink(GridView1, "Select$" + e.Row.RowIndex);
                //}
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