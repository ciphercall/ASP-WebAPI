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
    public partial class rptPartySumStatement : System.Web.UI.Page
    {
        IFormatProvider dateformat = new System.Globalization.CultureInfo("fr-FR", true);

        // To keep track of the previous row Group Identifier    
        string strPreviousRowID = string.Empty;
        // To keep track the Index of Group Total    
        int intSubTotalIndex = 1;

        decimal dblSubTotalQty = 0;
        decimal dblSubTotalAmount = 0;
        string dblSubTotalQtyComma = "";
        string dblSubTotalAmountComma = "";
        string dblSubTotalRateComma = "";

        decimal tot_Qty = 0;
        decimal tot_Amount = 0;

        string tot_QtyComma = "";
        string tot_AmountComma = "";

        protected void Page_Load(object sender, EventArgs e)
        {
            try
            {
                string uTp = HttpContext.Current.Session["USERTYPE"].ToString();
                string brCD = HttpContext.Current.Session["BrCD"].ToString();
                SqlCommand cmd = new SqlCommand();
                if (uTp == "COMPADMIN")
                {
                    dbFunctions.lblAdd(@"SELECT COMPNM FROM ASL_COMPANY WHERE COMPID ='101'", lblCompNM);
                    dbFunctions.lblAdd(@"SELECT ADDRESS FROM ASL_COMPANY WHERE COMPID ='101'", lblAddress);
                }
                else
                {
                    dbFunctions.lblAdd(@"SELECT COMPNM FROM ASL_COMPANY WHERE COMPID ='" + brCD + "'", lblCompNM);
                    dbFunctions.lblAdd(@"SELECT ADDRESS FROM ASL_COMPANY WHERE COMPID ='" + brCD + "'", lblAddress);
                }

                DateTime PrintDate = dbFunctions.Timezone(DateTime.Now);
                string td = PrintDate.ToString("dd-MMM-yyyy hh:mm tt");
                lblTime.Text = td;

                string Type = Session["Type"].ToString();
                string PS = Session["PS"].ToString();
                string PSCODE = Session["PSCODE"].ToString();
                string From = Session["From"].ToString();
                string To = Session["To"].ToString();

                lblPSName.Text = PS;

                if (Type == "SALE")
                {
                    lblType.Text = "SALES";
                }
                else
                    lblType.Text = Type;

                if (Type == "SALE")
                {
                    lblTypeHD.Text = "Party";
                }
                else
                    lblTypeHD.Text = "Supplier";

                DateTime FDate = DateTime.Parse(From, dateformat, System.Globalization.DateTimeStyles.AssumeLocal);
                lblFDate.Text = FDate.ToString("dd-MMM-yyyy");

                DateTime TDate = DateTime.Parse(To, dateformat, System.Globalization.DateTimeStyles.AssumeLocal);
                lblTDate.Text = TDate.ToString("dd-MMM-yyyy");

                showGrid();
            }
            catch (Exception ex)
            {
                Response.Write(ex.Message);
            }
        }

        public void showGrid()
        {
            string connectionString = ConfigurationManager.ConnectionStrings["AslDbContext"].ConnectionString;
            SqlConnection conn = new SqlConnection(connectionString);

            string Type = Session["Type"].ToString();
            string PS = Session["PS"].ToString();
            string PSCODE = Session["PSCODE"].ToString();
            string From = Session["From"].ToString();
            string To = Session["To"].ToString();

            DateTime FDate = DateTime.Parse(From, dateformat, System.Globalization.DateTimeStyles.AssumeLocal);
            string FDT = FDate.ToString("yyyy/MM/dd");

            DateTime TDate = DateTime.Parse(To, dateformat, System.Globalization.DateTimeStyles.AssumeLocal);
            string TDT = TDate.ToString("yyyy/MM/dd");

            conn.Open();
            SqlCommand cmd = new SqlCommand();
            cmd = new SqlCommand("", conn);

            if (Type == "SALE")
            {
                cmd.CommandType = CommandType.Text;
                cmd.CommandText = (" SELECT ROW_NUMBER() OVER(ORDER BY A.ITEMID)AS SL, A.ITEMID, A.ITEMNM,A.QTY,CONVERT(DECIMAL(18,2),(A.AMOUNT/A.QTY),103)AS RATE, A.AMOUNT, A.CATID,A.CATNM FROM " +
                                        " (SELECT STK_TRANS.ITEMID, (STK_ITEM.ITEMNM + ' || ' + STK_ITEM.ITEMCD) AS ITEMNM, SUM(STK_TRANS.QTY)AS QTY, SUM(STK_TRANS.AMOUNT)AS AMOUNT, STK_ITEMMST.CATID, STK_ITEMMST.CATNM " +
                                        " FROM STK_TRANS INNER JOIN STK_ITEM ON STK_TRANS.ITEMID = STK_ITEM.ITEMID INNER JOIN STK_ITEMMST ON STK_ITEM.CATID = STK_ITEMMST.CATID" +
                                        " WHERE (STK_TRANS.TRANSTP = 'SALE') AND (STK_TRANS.TRANSDT BETWEEN '" + FDT + "' AND '" + TDT + "') AND (STK_TRANS.PSID = '" + PSCODE + "') " +
                                        " GROUP BY STK_TRANS.ITEMID, (STK_ITEM.ITEMNM + ' || ' + STK_ITEM.ITEMCD),STK_ITEMMST.CATID, STK_ITEMMST.CATNM)AS A ORDER BY A.ITEMID");
            }

            else if (Type == "BUY")
            {
                cmd.CommandType = CommandType.Text;
                cmd.CommandText = (" SELECT ROW_NUMBER() OVER(ORDER BY A.ITEMID)AS SL,A.ITEMID,A.ITEMNM,A.QTY, CONVERT(DECIMAL(18,2),(A.AMOUNT/A.QTY),103)AS RATE,A.AMOUNT, A.CATID,A.CATNM FROM " +
                                        " (SELECT STK_TRANS.ITEMID, (STK_ITEM.ITEMNM + ' || ' + STK_ITEM.ITEMCD) AS ITEMNM, SUM(STK_TRANS.QTY) AS QTY, SUM(STK_TRANS.AMOUNT)AS AMOUNT, STK_ITEMMST.CATID, STK_ITEMMST.CATNM FROM " +
                                        " STK_TRANS INNER JOIN STK_ITEM ON STK_TRANS.ITEMID = STK_ITEM.ITEMID INNER JOIN STK_ITEMMST ON STK_ITEM.CATID = STK_ITEMMST.CATID WHERE (STK_TRANS.TRANSTP = 'BUY') AND " +
                                        " (STK_TRANS.TRANSDT BETWEEN '" + FDT + "' AND '" + TDT + "') AND (STK_TRANS.PSID = '" + PSCODE + "') " +
                                        " GROUP BY STK_TRANS.ITEMID, (STK_ITEM.ITEMNM + ' || ' + STK_ITEM.ITEMCD) ,STK_ITEMMST.CATID, STK_ITEMMST.CATNM)AS A ORDER BY ITEMID ");
            }

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
                GridView1.DataSource = ds;
                GridView1.DataBind();
                GridView1.Visible = true;
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
            if ((strPreviousRowID != string.Empty) && (DataBinder.Eval(e.Row.DataItem, "CATID") != null))
                if (strPreviousRowID != DataBinder.Eval(e.Row.DataItem, "CATID").ToString())
                    IsSubTotalRowNeedToAdd = true;
            if ((strPreviousRowID != string.Empty) && (DataBinder.Eval(e.Row.DataItem, "CATID") == null))
            {
                IsSubTotalRowNeedToAdd = true;
                IsGrandTotalRowNeedtoAdd = true;
                intSubTotalIndex = 0;
            }
            #region Inserting first Row and populating fist Group Header details
            if ((strPreviousRowID == string.Empty) && (DataBinder.Eval(e.Row.DataItem, "CATID") != null))
            {
                GridView GridView1 = (GridView)sender;
                GridViewRow row = new GridViewRow(0, 0, DataControlRowType.DataRow, DataControlRowState.Insert);
                TableCell cell = new TableCell();
                cell.Text = "Category Particulars : " + DataBinder.Eval(e.Row.DataItem, "CATNM").ToString();
                cell.ColumnSpan = 5;
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
                cell.Text = "CATEGORY WISE TOTAL :";
                cell.HorizontalAlign = HorizontalAlign.Left;
                //cell.ColumnSpan = 2;
                cell.CssClass = "SubTotalRowStyle";
                row.Cells.Add(cell);

                //Adding Carton Column         
                cell = new TableCell();
                cell.Text = string.Format("{0:0.00}", dblSubTotalQtyComma);
                cell.HorizontalAlign = HorizontalAlign.Right;
                cell.CssClass = "SubTotalRowStyle";
                row.Cells.Add(cell);

                ////Adding Pieces Column         
                //cell = new TableCell();
                //cell.Text = string.Format("{0:0.00}", dblSubTotalPiecesComma);
                //cell.HorizontalAlign = HorizontalAlign.Right;
                //cell.CssClass = "SubTotalRowStyle";
                //row.Cells.Add(cell);

                ////Adding CLQTY Column         
                //cell = new TableCell();
                //cell.Text = string.Format("{0:0.00}", dblSubTotalCLQtyComma);
                //cell.HorizontalAlign = HorizontalAlign.Right;
                //cell.CssClass = "SubTotalRowStyle";
                //row.Cells.Add(cell);

                ////Adding Amount Column         
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
                if (DataBinder.Eval(e.Row.DataItem, "CATID") != null)
                {
                    row = new GridViewRow(0, 0, DataControlRowType.DataRow, DataControlRowState.Insert);
                    cell = new TableCell();
                    cell.Text = "Category Particulars : " + DataBinder.Eval(e.Row.DataItem, "CATNM").ToString();
                    cell.ColumnSpan = 5;
                    cell.CssClass = "GroupHeaderStyle";
                    row.Cells.Add(cell);
                    GridView1.Controls[0].Controls.AddAt(e.Row.RowIndex + intSubTotalIndex, row);
                    intSubTotalIndex++;
                }
                #endregion
                #region Reseting the Sub Total Variables
                //dblSubTotalCartonQty = 0;
                //dblSubTotalPieces = 0;
                dblSubTotalQty = 0;
                dblSubTotalAmount = 0;
                #endregion
            }
            //if (IsGrandTotalRowNeedtoAdd)
            //{
            //    #region Grand Total Row
            //    GridView GridView1 = (GridView)sender;
            //    // Creating a Row      
            //    GridViewRow row = new GridViewRow(0, 0, DataControlRowType.DataRow, DataControlRowState.Insert);
            //    //Adding Total Cell           
            //    TableCell cell = new TableCell();
            //    cell.Text = "Store Wise Total";
            //    cell.HorizontalAlign = HorizontalAlign.Left;
            //    //cell.ColumnSpan = 2;
            //    cell.CssClass = "GrandTotalRowStyle";
            //    row.Cells.Add(cell);

            //    ////Adding Carton Qty Column          
            //    //cell = new TableCell();
            //    //cell.Text = string.Format("{0:0.00}", dblGrandTotalCartonQtyComma);
            //    //cell.HorizontalAlign = HorizontalAlign.Right;
            //    //cell.CssClass = "GrandTotalRowStyle";
            //    //row.Cells.Add(cell);

            //    ////Adding Pieces Column           
            //    //cell = new TableCell();
            //    //cell.Text = string.Format("{0:0.00}", dblGrandTotalPiecesComma);
            //    //cell.HorizontalAlign = HorizontalAlign.Right;
            //    //cell.CssClass = "GrandTotalRowStyle";
            //    //row.Cells.Add(cell);

            //    ////Adding CLQty Column          
            //    cell = new TableCell();
            //    cell.Text = string.Format("{0:0.00}", tot_QtyComma);
            //    cell.HorizontalAlign = HorizontalAlign.Right;
            //    cell.CssClass = "GrandTotalRowStyle";
            //    row.Cells.Add(cell);

            //    ////Adding Amount Column          
            //    //cell = new TableCell();
            //    //cell.Text = string.Format("{0:0.00}", dblGrandTotalRateComma);
            //    //cell.HorizontalAlign = HorizontalAlign.Right;
            //    //cell.CssClass = "GrandTotalRowStyle";
            //    //row.Cells.Add(cell);

            //    //Adding Amount Column          
            //    cell = new TableCell();
            //    cell.Text = string.Format("{0:0.00}", tot_AmountComma);
            //    cell.HorizontalAlign = HorizontalAlign.Right;
            //    cell.CssClass = "GrandTotalRowStyle";
            //    row.Cells.Add(cell);
            //    //Adding the Row at the RowIndex position in the Grid     
            //    GridView1.Controls[0].Controls.AddAt(e.Row.RowIndex, row);
            //    #endregion
            //}
        }



        protected void GridView1_RowDataBound(object sender, GridViewRowEventArgs e)
        {
            if (e.Row.RowType == DataControlRowType.DataRow)
            {
                strPreviousRowID = DataBinder.Eval(e.Row.DataItem, "CATID").ToString();

                //string SL = DataBinder.Eval(e.Row.DataItem, "SL").ToString();
                //e.Row.Cells[0].Text = SL;

                string ITEMNM = DataBinder.Eval(e.Row.DataItem, "ITEMNM").ToString();
                e.Row.Cells[0].Text = "&nbsp;" + ITEMNM;

                decimal QTY = Convert.ToDecimal(DataBinder.Eval(e.Row.DataItem, "QTY").ToString());
                string qt = SpellAmount.comma(QTY);
                e.Row.Cells[1].Text = qt + "&nbsp;";

                decimal RATE = Convert.ToDecimal(DataBinder.Eval(e.Row.DataItem, "RATE").ToString());
                string rte = SpellAmount.comma(RATE);
                e.Row.Cells[2].Text = rte + "&nbsp;";

                decimal AMOUNT = Convert.ToDecimal(DataBinder.Eval(e.Row.DataItem, "AMOUNT").ToString());
                string Amt = SpellAmount.comma(AMOUNT);
                e.Row.Cells[3].Text = Amt + "&nbsp;";

                dblSubTotalQty += QTY;
                dblSubTotalQtyComma = SpellAmount.comma(dblSubTotalQty);

                dblSubTotalAmount += AMOUNT;
                dblSubTotalAmountComma = SpellAmount.comma(dblSubTotalAmount);

                tot_Qty += QTY;
                tot_QtyComma = SpellAmount.comma(tot_Qty);

                tot_Amount += AMOUNT;
                tot_AmountComma = SpellAmount.comma(tot_Amount);
            }

            else if (e.Row.RowType == DataControlRowType.Footer)
            {
                e.Row.Cells[0].Text = "TOTAL :";
                e.Row.Cells[0].HorizontalAlign = HorizontalAlign.Right;
                e.Row.Cells[1].Text = tot_QtyComma;
                e.Row.Cells[1].HorizontalAlign = HorizontalAlign.Right;
                e.Row.Cells[3].Text = tot_AmountComma;
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

    }
}