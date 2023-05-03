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
using System.Threading;
using AlchemyAccounting;

namespace DynamicMenu.Stock.Report.Report
{
    public partial class rptItem : System.Web.UI.Page
    {
        IFormatProvider dateformat = new System.Globalization.CultureInfo("fr-FR", true);

        // To keep track of the previous row Group Identifier    
        string strPreviousRowID = string.Empty;
        // To keep track the Index of Group Total    
        int intSubTotalIndex = 1;

        protected void Page_Load(object sender, EventArgs e)
        {

            if (Session["USERID"] == null)
            {
                Response.Redirect("~/Login/UI/Login.aspx");
            }
            else
            {
                const string formLink = "/Stock/Report/UI/PartyInformation.aspx";
                bool permission = UserPermissionChecker.checkParmit(formLink, "STATUS");
                if (permission == true)
                {
                    try
                    {
                        dbFunctions.lblAdd(@"SELECT COMPNM FROM ASL_COMPANY WHERE COMPID='101'", lblCompNM);
                        dbFunctions.lblAdd(@"SELECT ADDRESS FROM ASL_COMPANY WHERE COMPID='101'", lblAddress);

                        DateTime PrintDate = DateTime.Now;
                        string td = PrintDate.ToString("dd-MMM-yyyy hh:mm tt");
                        lblTime.Text = td;

                        showGrid();
                    }
                    catch
                    {

                    }
                }
                else
                {
                    Response.Redirect("../../DeshBoard/UI/Default.aspx");
                }
            }

        }

        public void showGrid()
        {
            SqlConnection conn = new SqlConnection(dbFunctions.connection);

            conn.Open();
            SqlCommand cmd = new SqlCommand(@" SELECT  STK_ITEMMST.CATNM+' - '+STK_ITEMMST.CATNMB AS CATNM , STK_ITEM.ITEMID, STK_ITEM.ITEMNM, STK_ITEM.ITEMNMB, 
            STK_ITEM.MUNIT,  STK_ITEM.BUYRT, STK_ITEM.SALRT,STK_ITEM.IMAGEPATH
            FROM STK_ITEMMST INNER JOIN STK_ITEM ON STK_ITEMMST.CATID = STK_ITEM.CATID
            ORDER BY STK_ITEMMST.CATID, STK_ITEM.ITEMID", conn);

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

        /// <summary>   
        /// /// Event fires for every row creation   
        /// /// Used for creating SubTotal row when next group starts by adding Group Total at previous row manually    
        /// </summary>    /// <param name="sender"></param>    /// <param name="e"></param>   
        protected void GridView1_RowCreated(object sender, GridViewRowEventArgs e)
        {
            bool IsSubTotalRowNeedToAdd = false;
            //bool IsGrandTotalRowNeedtoAdd = false;
            if ((strPreviousRowID != string.Empty) && (DataBinder.Eval(e.Row.DataItem, "CATNM") != null))
                if (strPreviousRowID != DataBinder.Eval(e.Row.DataItem, "CATNM").ToString())
                    IsSubTotalRowNeedToAdd = true;
            if ((strPreviousRowID != string.Empty) && (DataBinder.Eval(e.Row.DataItem, "CATNM") == null))
            {
                IsSubTotalRowNeedToAdd = true;
            //    IsGrandTotalRowNeedtoAdd = true;
                intSubTotalIndex = 0;
            }
            #region Inserting first Row and populating fist Group Header details
            if ((strPreviousRowID == string.Empty) && (DataBinder.Eval(e.Row.DataItem, "CATNM") != null))
            {
                GridView GridView1 = (GridView)sender;
                GridViewRow row = new GridViewRow(0, 0, DataControlRowType.DataRow, DataControlRowState.Insert);
                TableCell cell = new TableCell();
                cell.Text = "Category Particulars : " + DataBinder.Eval(e.Row.DataItem, "CATNM").ToString();
                cell.ColumnSpan = 7;
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
            //    cell.Text = "Category Wise Total";
            //    cell.HorizontalAlign = HorizontalAlign.Left;
            //    //cell.ColumnSpan = 2;
            //    cell.CssClass = "SubTotalRowStyle";
            //    row.Cells.Add(cell);

            //    //Adding Carton Column         
            //    cell = new TableCell();
            //    cell.Text = string.Format("{0:0.00}", dblSubTotalCartonQtyComma);
            //    cell.HorizontalAlign = HorizontalAlign.Right;
            //    cell.CssClass = "SubTotalRowStyle";
            //    row.Cells.Add(cell);

            //    //Adding Pieces Column         
            //    cell = new TableCell();
            //    cell.Text = string.Format("{0:0.00}", dblSubTotalPiecesComma);
            //    cell.HorizontalAlign = HorizontalAlign.Right;
            //    cell.CssClass = "SubTotalRowStyle";
            //    row.Cells.Add(cell);

            //    //Adding CLQTY Column         
            //    cell = new TableCell();
            //    cell.Text = string.Format("{0:0.00}", dblSubTotalCLQtyComma);
            //    cell.HorizontalAlign = HorizontalAlign.Right;
            //    cell.CssClass = "SubTotalRowStyle";
            //    row.Cells.Add(cell);

            //    //Adding Amount Column         
            //    cell = new TableCell();
            //    cell.Text = string.Format("{0:0.00}", dblSubTotalRateComma);
            //    cell.HorizontalAlign = HorizontalAlign.Right;
            //    cell.CssClass = "SubTotalRowStyle";
            //    row.Cells.Add(cell);

            //    //Adding Amount Column         
            //    cell = new TableCell();
            //    cell.Text = string.Format("{0:0.00}", dblSubTotalAmountComma);
            //    cell.HorizontalAlign = HorizontalAlign.Right;
            //    cell.CssClass = "SubTotalRowStyle";
            //    row.Cells.Add(cell);
                //Adding the Row at the RowIndex position in the Grid      
                GridView1.Controls[0].Controls.AddAt(e.Row.RowIndex + intSubTotalIndex, row);
                intSubTotalIndex++;
                //#endregion
            #region Adding Next Group Header Details
            if (DataBinder.Eval(e.Row.DataItem, "CATNM") != null)
            {
                row = new GridViewRow(0, 0, DataControlRowType.DataRow, DataControlRowState.Insert);
                cell = new TableCell();
                cell.Text = "Category Particulars : " + DataBinder.Eval(e.Row.DataItem, "CATNM").ToString();
                cell.ColumnSpan = 7;
                cell.CssClass = "GroupHeaderStyle";
                row.Cells.Add(cell);
                GridView1.Controls[0].Controls.AddAt(e.Row.RowIndex + intSubTotalIndex, row);
                intSubTotalIndex++;
            }
            #endregion
            //    #region Reseting the Sub Total Variables
            //    dblSubTotalCartonQty = 0;
            //    dblSubTotalPieces = 0;
            //    dblSubTotalCLQty = 0;
            //    dblSubTotalAmount = 0;
            //    #endregion
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
            //    cell = new TableCell();
            //    cell.Text = string.Format("{0:0.00}", dblGrandTotalCartonQtyComma);
            //    cell.HorizontalAlign = HorizontalAlign.Right;
            //    cell.CssClass = "GrandTotalRowStyle";
            //    row.Cells.Add(cell);

            //    ////Adding Pieces Column           
            //    cell = new TableCell();
            //    cell.Text = string.Format("{0:0.00}", dblGrandTotalPiecesComma);
            //    cell.HorizontalAlign = HorizontalAlign.Right;
            //    cell.CssClass = "GrandTotalRowStyle";
            //    row.Cells.Add(cell);

            //    ////Adding CLQty Column          
            //    cell = new TableCell();
            //    cell.Text = string.Format("{0:0.00}", dblGrandTotalCLQtyComma);
            //    cell.HorizontalAlign = HorizontalAlign.Right;
            //    cell.CssClass = "GrandTotalRowStyle";
            //    row.Cells.Add(cell);

            //    //Adding Amount Column          
            //    cell = new TableCell();
            //    cell.Text = string.Format("{0:0.00}", dblGrandTotalRateComma);
            //    cell.HorizontalAlign = HorizontalAlign.Right;
            //    cell.CssClass = "GrandTotalRowStyle";
            //    row.Cells.Add(cell);

            //    //Adding Amount Column          
            //    cell = new TableCell();
            //    cell.Text = string.Format("{0:0.00}", dblGrandTotalAmountComma);
            //    cell.HorizontalAlign = HorizontalAlign.Right;
            //    cell.CssClass = "GrandTotalRowStyle";
            //    row.Cells.Add(cell);
            //    //Adding the Row at the RowIndex position in the Grid     
            //    GridView1.Controls[0].Controls.AddAt(e.Row.RowIndex, row);
            //    #endregion
            //}
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
                strPreviousRowID = DataBinder.Eval(e.Row.DataItem, "CATNM").ToString();


                string ItemNM = DataBinder.Eval(e.Row.DataItem, "ITEMID").ToString();
                e.Row.Cells[0].Text = "&nbsp;" + ItemNM;

                string ITEMNM = DataBinder.Eval(e.Row.DataItem, "ITEMNM").ToString();
                e.Row.Cells[1].Text = "&nbsp;" + ITEMNM;

                string ITEMNMB = DataBinder.Eval(e.Row.DataItem, "ITEMNMB").ToString();
                e.Row.Cells[2].Text = "&nbsp;" + ITEMNMB;

                string MUNIT = DataBinder.Eval(e.Row.DataItem, "MUNIT").ToString();
                e.Row.Cells[3].Text = "&nbsp;" + MUNIT;

                string BUYRT = DataBinder.Eval(e.Row.DataItem, "BUYRT").ToString();
                e.Row.Cells[4].Text = "&nbsp;" + BUYRT;

                string SALRT = DataBinder.Eval(e.Row.DataItem, "SALRT").ToString();
                e.Row.Cells[5].Text = "&nbsp;" + SALRT;
                
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