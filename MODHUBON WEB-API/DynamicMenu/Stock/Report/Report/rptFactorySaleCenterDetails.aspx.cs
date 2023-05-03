using System;
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
    public partial class rptFactorySaleCenterDetails : System.Web.UI.Page
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
                    string td = dbFunctions.Timezone(PrintDate).ToString("dd-MMM-yyyy hh:mm tt");
                    lblTime.Text = td;
                    string storeFrom = Session["StoreIdFr"].ToString();
                    string storeTo = Session["StoreIdTo"].ToString();
                    string From = Session["FromDt"].ToString();
                    string To = Session["ToDt"].ToString();

                    dbFunctions.lblAdd("Select STORENM from STK_STORE where STOREID='" + storeFrom + "'", lblStoreFr);
                    dbFunctions.lblAdd("Select STORENM from STK_STORE where STOREID='" + storeTo + "'", lblStoreTo);

                    DateTime FDate = DateTime.Parse(From, dateformat, System.Globalization.DateTimeStyles.AssumeLocal);
                    lblFromDate.Text = FDate.ToString("dd-MMM-yyyy");

                    DateTime TDate = DateTime.Parse(To, dateformat, System.Globalization.DateTimeStyles.AssumeLocal);
                    lblToDate.Text = TDate.ToString("dd-MMM-yyyy");
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

            string storeFrom = Session["StoreIdFr"].ToString();
            string storeTo = Session["StoreIdTo"].ToString();
            string From = Session["FromDt"].ToString();
            string To = Session["ToDt"].ToString();
            DateTime FDT = DateTime.Parse(From, dateformat, System.Globalization.DateTimeStyles.AssumeLocal);
            DateTime TDT = DateTime.Parse(To, dateformat, System.Globalization.DateTimeStyles.AssumeLocal);
            conn.Open();
            string query;

            query = @"SELECT (CONVERT(NVARCHAR,TRANSDT,103)+' Memo: '+CONVERT(NVARCHAR,TRANSNO)) AS TRANSNODT, STK_ITEM.ITEMID,STK_ITEM.ITEMNM, SUM(QTY) AS QTY, SUM(AMOUNT)AMOUNT,convert(decimal(18,2),(SUM(AMOUNT)/ SUM(QTY))) AS RATE
                        FROM STK_TRANS
                        INNER JOIN STK_ITEM ON STK_ITEM.ITEMID=STK_TRANS.ITEMID
                        WHERE TRANSTP IN ('IISS')
                        AND   UNITFR  = @STOREFR
                        AND   UNITTO  = @STORETO
                        AND   TRANSDT BETWEEN @FDT AND @TDT
                        GROUP BY STK_ITEM.ITEMID,STK_ITEM.ITEMNM,TRANSNO, TRANSDT
                        ORDER BY TRANSNODT, STK_ITEM.ITEMID";

            SqlCommand cmd = new SqlCommand(query, conn);

            cmd.Parameters.AddWithValue("@STOREFR", storeFrom);
            cmd.Parameters.AddWithValue("@STORETO", storeTo);
            cmd.Parameters.AddWithValue("@FDT", FDT);
            cmd.Parameters.AddWithValue("@TDT", TDT);

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
            if ((strPreviousRowID != string.Empty) && (DataBinder.Eval(e.Row.DataItem, "TRANSNODT") != null))
                if (strPreviousRowID != DataBinder.Eval(e.Row.DataItem, "TRANSNODT").ToString())
                    IsSubTotalRowNeedToAdd = true;
            if ((strPreviousRowID != string.Empty) && (DataBinder.Eval(e.Row.DataItem, "TRANSNODT") == null))
            {
                IsSubTotalRowNeedToAdd = true;
                //    IsGrandTotalRowNeedtoAdd = true;
                intSubTotalIndex = 0;
            }
            #region Inserting first Row and populating fist Group Header details
            if ((strPreviousRowID == string.Empty) && (DataBinder.Eval(e.Row.DataItem, "TRANSNODT") != null))
            {
                GridView GridView1 = (GridView)sender;
                GridViewRow row = new GridViewRow(0, 0, DataControlRowType.DataRow, DataControlRowState.Insert);
                TableCell cell = new TableCell();
                cell.Text = DataBinder.Eval(e.Row.DataItem, "TRANSNODT").ToString();
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
                cell.ColumnSpan = 2;
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
                cell.HorizontalAlign = HorizontalAlign.Right;
                cell.ColumnSpan = 1;
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
                if (DataBinder.Eval(e.Row.DataItem, "TRANSNODT") != null)
                {
                    row = new GridViewRow(0, 0, DataControlRowType.DataRow, DataControlRowState.Insert);
                    cell = new TableCell();
                    cell.Text = "" + DataBinder.Eval(e.Row.DataItem, "TRANSNODT").ToString();
                    cell.ColumnSpan = 6;
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
                strPreviousRowID = DataBinder.Eval(e.Row.DataItem, "TRANSNODT").ToString();


                string ItemNM = DataBinder.Eval(e.Row.DataItem, "ITEMNM").ToString();
                e.Row.Cells[1].Text = "&nbsp;" + ItemNM;


                decimal Qty = Convert.ToDecimal(DataBinder.Eval(e.Row.DataItem, "QTY").ToString());
                e.Row.Cells[2].Text = "&nbsp;" + Qty;

                dblSubTotalQty += Qty;
                dblSubTotalCartonQtyComma = SpellAmount.comma(dblSubTotalQty);

                decimal Rate = Convert.ToDecimal(DataBinder.Eval(e.Row.DataItem, "RATE").ToString());
                e.Row.Cells[3].Text = "&nbsp;" + Rate;


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
                e.Row.Cells[1].Text = "Total : ";
                e.Row.Cells[1].HorizontalAlign = HorizontalAlign.Right;
                e.Row.Cells[2].Text = dblGrandTotalQtyComma;
                e.Row.Cells[2].HorizontalAlign = HorizontalAlign.Center;
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