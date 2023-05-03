using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using AlchemyAccounting;

namespace DynamicMenu.Stock.Report.Report
{
    public partial class rptPartyList : System.Web.UI.Page
    {
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

                        ShowGrid();
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

        public void ShowGrid()
        {
            SqlConnection conn = new SqlConnection(dbFunctions.connection);

            conn.Open();
            SqlCommand cmd = new SqlCommand(@"SELECT COMPID, PARTYID, PARTYNM, PARTYNMB, ADDRESS, ADDRESSB, MOBNO1, MOBNO2, EMAILID, APNM, APCNO, REMARKS, 
            CASE WHEN STATUS='A' THEN 'ACTIVE' ELSE 'INACTIVE' END STATUS FROM STK_PARTY", conn);

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
                string PARTYID = DataBinder.Eval(e.Row.DataItem, "PARTYID").ToString();
                e.Row.Cells[0].Text = "&nbsp;" + PARTYID;

                string PARTYNM = DataBinder.Eval(e.Row.DataItem, "PARTYNM").ToString();
                e.Row.Cells[1].Text = "&nbsp;" + PARTYNM;

                string PARTYNMB = DataBinder.Eval(e.Row.DataItem, "PARTYNMB").ToString();
                e.Row.Cells[2].Text = "&nbsp;" + PARTYNMB;

                string ADDRESS = DataBinder.Eval(e.Row.DataItem, "ADDRESS").ToString();
                e.Row.Cells[3].Text = "&nbsp;" + ADDRESS;

                string ADDRESSB = DataBinder.Eval(e.Row.DataItem, "ADDRESSB").ToString();
                e.Row.Cells[4].Text = "&nbsp;" + ADDRESSB;

                string MOBNO1 = DataBinder.Eval(e.Row.DataItem, "MOBNO1").ToString();
                e.Row.Cells[5].Text = "&nbsp;" + MOBNO1;

                string MOBNO2 = DataBinder.Eval(e.Row.DataItem, "MOBNO2").ToString();
                e.Row.Cells[6].Text = "&nbsp;" + MOBNO2;

                string EMAILID = DataBinder.Eval(e.Row.DataItem, "EMAILID").ToString();
                e.Row.Cells[7].Text = "&nbsp;" + EMAILID;

                string APNM = DataBinder.Eval(e.Row.DataItem, "APNM").ToString();
                e.Row.Cells[8].Text = "&nbsp;" + APNM;

                string APCNO = DataBinder.Eval(e.Row.DataItem, "APCNO").ToString();
                e.Row.Cells[9].Text = "&nbsp;" + APCNO;

                string REMARKS = DataBinder.Eval(e.Row.DataItem, "REMARKS").ToString();
                e.Row.Cells[10].Text = "&nbsp;" + REMARKS;

                string STATUS = DataBinder.Eval(e.Row.DataItem, "STATUS").ToString();
                e.Row.Cells[11].Text = "&nbsp;" + STATUS;
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