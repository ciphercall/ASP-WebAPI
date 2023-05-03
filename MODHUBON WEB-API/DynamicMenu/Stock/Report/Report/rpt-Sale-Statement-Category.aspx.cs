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
    public partial class rpt_Sale_Statement_Category : System.Web.UI.Page
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
                    string uTp = HttpContext.Current.Session["USERTYPE"].ToString();
                    string brCD = HttpContext.Current.Session["BrCD"].ToString();
                    SqlCommand cmd = new SqlCommand();

                    dbFunctions.lblAdd(@"SELECT COMPNM FROM ASL_COMPANY WHERE COMPID ='101'", lblCompNM);
                    dbFunctions.lblAdd(@"SELECT ADDRESS FROM ASL_COMPANY WHERE COMPID ='101'", lblAddress);

                    DateTime PrintDate = dbFunctions.Timezone(DateTime.Now);
                    string td = PrintDate.ToString("dd-MMM-yyyy hh:mm tt");
                    lblTime.Text = td;

                    string fdate = Session["FromDate"].ToString();
                    string todate = Session["ToDate"].ToString();
                    lblFromDate.Text = fdate;
                    lblToDate.Text = todate;

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

            string type = Session["Type"].ToString();
            string fdate = Session["FromDate"].ToString();
            string todate = Session["ToDate"].ToString();

            DateTime FromDt = DateTime.Parse(fdate, dateformat, System.Globalization.DateTimeStyles.AssumeLocal);
            DateTime ToDt = DateTime.Parse(todate, dateformat, System.Globalization.DateTimeStyles.AssumeLocal);
            conn.Open();
            SqlCommand cmd = new SqlCommand(@"SELECT SUBSTRING(STK_TRANS.ITEMID,1,5) CATID, STK_ITEMMST.SUBNM, STK_TRANS.ITEMID +' - '+ STK_ITEM.ITEMNM AS ITEMNM,STK_ITEM.AUTHOR ,  SUM(QTY) AS QTY, SUM(AMOUNT) AS AMOUNT
                                            FROM dbo.STK_TRANS INNER JOIN STK_ITEM
                                            ON STK_TRANS.ITEMID = dbo.STK_ITEM.ITEMCD INNER JOIN
                                            STK_ITEMMST ON STK_ITEM.SUBID = STK_ITEMMST.SUBID
                                            WHERE STK_TRANS.TRANSTP = @TYPE AND STK_TRANS.TRANSDT BETWEEN @FROMDATE AND @TODATE
                                            GROUP BY SUBSTRING(STK_TRANS.ITEMID,1,5), STK_TRANS.ITEMID, ITEMNM, STK_ITEMMST.SUBNM,STK_ITEM.AUTHOR
                                            ORDER BY SUBSTRING(STK_TRANS.ITEMID,1,5), STK_TRANS.ITEMID", conn);

            cmd.Parameters.AddWithValue("@TYPE", type);
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
            if ((strPreviousRowID != string.Empty) && (DataBinder.Eval(e.Row.DataItem, "CATID") != null))
                if (strPreviousRowID != DataBinder.Eval(e.Row.DataItem, "CATID").ToString())
                    IsSubTotalRowNeedToAdd = true;
            if ((strPreviousRowID != string.Empty) && (DataBinder.Eval(e.Row.DataItem, "CATID") == null))
            {
                IsSubTotalRowNeedToAdd = true;
                //    IsGrandTotalRowNeedtoAdd = true;
                intSubTotalIndex = 0;
            }
            #region Inserting first Row and populating fist Group Header details
            if ((strPreviousRowID == string.Empty) && (DataBinder.Eval(e.Row.DataItem, "CATID") != null))
            {
                GridView GridView1 = (GridView)sender;
                GridViewRow row = new GridViewRow(0, 0, DataControlRowType.DataRow, DataControlRowState.Insert);
                TableCell cell = new TableCell();
                cell.Text = "Category : " + DataBinder.Eval(e.Row.DataItem, "SUBNM").ToString();
                cell.ColumnSpan = 5;
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
                cell.Text = "Category Wise Total";
                cell.HorizontalAlign = HorizontalAlign.Right;
                cell.ColumnSpan = 3;
                cell.CssClass = "SubTotalRowStyle";
                row.Cells.Add(cell);

                //Adding Carton Column         
                cell = new TableCell();
                cell.Text = string.Format("{0:0.00}", dblSubTotalCartonQtyComma);
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
                if (DataBinder.Eval(e.Row.DataItem, "CATID") != null)
                {
                    row = new GridViewRow(0, 0, DataControlRowType.DataRow, DataControlRowState.Insert);
                    cell = new TableCell();
                    cell.Text = "Category : " + DataBinder.Eval(e.Row.DataItem, "SUBNM").ToString();
                    cell.ColumnSpan = 5;
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
                strPreviousRowID = DataBinder.Eval(e.Row.DataItem, "CATID").ToString();


                string ItemNM = DataBinder.Eval(e.Row.DataItem, "ITEMNM").ToString();
                e.Row.Cells[1].Text = "&nbsp;" + ItemNM;

                string AUTHOR = DataBinder.Eval(e.Row.DataItem, "AUTHOR").ToString();
                e.Row.Cells[2].Text = "&nbsp;" + AUTHOR;

                decimal Qty = Convert.ToDecimal(DataBinder.Eval(e.Row.DataItem, "QTY").ToString());
                e.Row.Cells[3].Text = "&nbsp;" + Qty;

                dblSubTotalQty += Qty;
                dblSubTotalCartonQtyComma = SpellAmount.comma(dblSubTotalQty);

                dblGrandTotalQty += Qty;
                dblGrandTotalQtyComma = SpellAmount.comma(dblGrandTotalQty);

                decimal AMOUNT = Convert.ToDecimal(DataBinder.Eval(e.Row.DataItem, "AMOUNT").ToString());
                e.Row.Cells[4].Text = "&nbsp;" + AMOUNT;

                dblSubTotalAmount += AMOUNT;
                dblSubTotalAmountComma = SpellAmount.comma(dblSubTotalAmount);


                dblGrandTotalAmount += AMOUNT;
                dblGrandTotalAmountComma = SpellAmount.comma(dblGrandTotalAmount);

            }
            // This is for cumulating the values  
            else if (e.Row.RowType == DataControlRowType.Footer)
            {
                e.Row.Cells[2].Text = "Total : ";
                e.Row.Cells[2].HorizontalAlign = HorizontalAlign.Right;
                e.Row.Cells[3].Text = dblGrandTotalQtyComma;
                e.Row.Cells[3].HorizontalAlign = HorizontalAlign.Center;
                e.Row.Cells[4].Text = dblGrandTotalAmountComma;
                e.Row.Cells[4].HorizontalAlign = HorizontalAlign.Right;

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