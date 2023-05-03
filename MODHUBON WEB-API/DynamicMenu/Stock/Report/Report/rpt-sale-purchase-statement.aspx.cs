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
    public partial class rpt_sale_purchase_statement : System.Web.UI.Page
    {
        IFormatProvider dateformat = new System.Globalization.CultureInfo("fr-FR", true);

        // To keep track of the previous row Group Identifier    
        string strPreviousRowID = string.Empty;
        // To keep track the Index of Group Total    
        int intSubTotalIndex = 1;
        // To temporarily store Sub Total    
        decimal dblSubTotalQty = 0;
        decimal dblSubTotalAmount = 0;
        // To temporarily store Grand Total    
        decimal dblGrandTotalQty = 0;
        decimal dblGrandTotalAmount = 0;
        //string AmountComma = "";
        string dblSubTotalQtyComma = "0";
        string dblSubTotalAmountComma = "0";

        string dblGrandTotalQtyComma = "0";
        string dblGrandTotalAmountComma = "0";

        protected void Page_Load(object sender, EventArgs e)
        {
            if (Session["USERID"] == null)
            {
                Response.Redirect("~/Login/UI/Login.aspx");
            }
            else
            {
                string uTp = HttpContext.Current.Session["USERTYPE"].ToString();
                string brCD = HttpContext.Current.Session["BrCD"].ToString();
                SqlCommand cmd = new SqlCommand();
                if (uTp == "COMPADMIN")
                {
                    dbFunctions.lblAdd(@"SELECT COMPNM FROM ASL_COMPANY WHERE COMPID ='101'", lblCompanyNM);
                    dbFunctions.lblAdd(@"SELECT ADDRESS FROM ASL_COMPANY WHERE COMPID ='101'", lblAddress);
                }
                else
                {
                    dbFunctions.lblAdd(@"SELECT COMPNM FROM ASL_COMPANY WHERE COMPID ='" + brCD + "'", lblCompanyNM);
                    dbFunctions.lblAdd(@"SELECT ADDRESS FROM ASL_COMPANY WHERE COMPID ='" + brCD + "'", lblAddress);
                }

                DateTime PrintDate = dbFunctions.Timezone(DateTime.Now);
                string td = PrintDate.ToString("dd-MMM-yyyy hh:mm tt");
                lblPrintDate.Text = td;

                string type = Session["type"].ToString();
                string fdt = Session["fdt"].ToString();
                string tdt = Session["tdt"].ToString();

                lblType.Text = type;
                DateTime fd = DateTime.Parse(fdt, dateformat, System.Globalization.DateTimeStyles.AssumeLocal);
                string fdate = fd.ToString("dd-MMM-yy");
                lblFdt.Text = fdate;
                DateTime tod = DateTime.Parse(tdt, dateformat, System.Globalization.DateTimeStyles.AssumeLocal);
                string tdate = tod.ToString("dd-MMM-yy");
                lblTdt.Text = tdate;
                showGrid();
            }
        }

        public void showGrid()
        {
            string connectionString = ConfigurationManager.ConnectionStrings["AslDbContext"].ConnectionString;
            SqlConnection conn = new SqlConnection(connectionString);

            string type = Session["type"].ToString();
            string fdt = Session["fdt"].ToString();
            DateTime fd = DateTime.Parse(fdt, dateformat, System.Globalization.DateTimeStyles.AssumeLocal);
            string fdate = fd.ToString("yyyy-MM-dd");
            string tdt = Session["tdt"].ToString();
            DateTime tod = DateTime.Parse(tdt, dateformat, System.Globalization.DateTimeStyles.AssumeLocal);
            string tdate = tod.ToString("yyyy-MM-dd");

            string uTp = HttpContext.Current.Session["USERTYPE"].ToString();
            string brCD = HttpContext.Current.Session["BrCD"].ToString();

            lblStore.Text = "";
            dbFunctions.lblAdd("SELECT STOREID FROM STK_STORE WHERE COMPID ='" + brCD + "'", lblStore);

            SqlCommand cmd = new SqlCommand();
            conn.Open();
            if (uTp == "COMPADMIN")
            {
                if (type == "SALE")
                {
                    cmd = new SqlCommand(" SELECT 'DATE : ' + CONVERT(NVARCHAR(20),STK_TRANS.TRANSDT, 103) + ' ------ ' + 'BILL NO : ' + CONVERT(NVARCHAR(20),STK_TRANS.TRANSNO, 103) + ' ------ ' + 'PARTY NAME : ' + GL_ACCHART.ACCOUNTNM AS HEAD, STK_TRANS.TRANSDT, STK_TRANS.TRANSNO, STK_TRANS.STOREFR, STK_TRANS.PSID, STK_TRANS.ITEMID, STK_TRANS.QTY, STK_TRANS.RATE,  " +
                                     " GL_ACCHART.ACCOUNTNM, STK_STORE.STORENM, STK_ITEM.ITEMNM + ' || ' +STK_ITEM.ITEMCD AS ITEMNM, SUM(STK_TRANS.AMOUNT) AS AMOUNT FROM STK_TRANS INNER JOIN GL_ACCHART ON STK_TRANS.PSID = GL_ACCHART.ACCOUNTCD INNER JOIN STK_STORE ON STK_TRANS.STOREFR = STK_STORE.STOREID INNER JOIN STK_ITEM ON STK_TRANS.ITEMID = STK_ITEM.ITEMID " +
                                     " WHERE (STK_TRANS.TRANSTP = 'SALE') AND (STK_TRANS.TRANSDT BETWEEN '" + fdate + "' AND '" + tdate + "') GROUP BY 'DATE : ' + CONVERT(NVARCHAR(20),STK_TRANS.TRANSDT, 103) + ' ------ ' + 'BILL NO : ' + CONVERT(NVARCHAR(20),STK_TRANS.TRANSNO, 103) + ' ------ ' + 'PARTY NAME : ' + GL_ACCHART.ACCOUNTNM, STK_TRANS.TRANSDT, " +
                                     " STK_TRANS.TRANSNO, STK_TRANS.STOREFR, STK_TRANS.PSID, STK_TRANS.ITEMID, STK_TRANS.QTY, STK_TRANS.RATE, GL_ACCHART.ACCOUNTNM, STK_STORE.STORENM, STK_ITEM.ITEMNM + ' || ' +STK_ITEM.ITEMCD ORDER BY TRANSDT, TRANSNO, ACCOUNTNM", conn);
                }
                else
                {
                    cmd = new SqlCommand("SELECT 'DATE : ' + CONVERT(NVARCHAR(20), STK_TRANS.TRANSDT, 103) + ' -- ' + 'BILL NO : ' + CONVERT(NVARCHAR(20), STK_TRANS.TRANSNO, 103) + ' -- ' + 'PARTY NAME : ' + GL_ACCHART.ACCOUNTNM AS HEAD, STK_TRANS.TRANSDT, STK_TRANS.TRANSNO, STK_TRANS.PSID, STK_TRANS.ITEMID, STK_TRANS.QTY, STK_TRANS.RATE, GL_ACCHART.ACCOUNTNM, STK_STORE.STORENM, STK_ITEM.ITEMNM + ' || ' +STK_ITEM.ITEMCD AS ITEMNM, SUM(STK_TRANS.AMOUNT) AS AMOUNT,  " +
                                     " STK_TRANS.STORETO FROM STK_TRANS INNER JOIN GL_ACCHART ON STK_TRANS.PSID = GL_ACCHART.ACCOUNTCD INNER JOIN STK_ITEM ON STK_TRANS.ITEMID = STK_ITEM.ITEMID INNER JOIN STK_STORE ON STK_TRANS.STORETO = STK_STORE.STOREID WHERE (STK_TRANS.TRANSTP = 'BUY') AND (STK_TRANS.TRANSDT BETWEEN '" + fdate + "' AND '" + tdate + "')  " +
                                     " GROUP BY 'DATE : ' + CONVERT(NVARCHAR(20), STK_TRANS.TRANSDT, 103) + ' -- ' + 'BILL NO : ' + CONVERT(NVARCHAR(20), STK_TRANS.TRANSNO, 103) + ' -- ' + 'PARTY NAME : ' + GL_ACCHART.ACCOUNTNM, STK_TRANS.TRANSDT, STK_TRANS.TRANSNO, STK_TRANS.PSID, STK_TRANS.ITEMID, STK_TRANS.QTY, STK_TRANS.RATE, GL_ACCHART.ACCOUNTNM, STK_STORE.STORENM, STK_ITEM.ITEMNM + ' || ' +STK_ITEM.ITEMCD, STK_TRANS.STORETO ORDER BY STK_TRANS.TRANSDT, STK_TRANS.TRANSNO, GL_ACCHART.ACCOUNTNM", conn);
                }
            }
            else
            {
                if (type == "SALE")
                {
                    cmd = new SqlCommand(" SELECT 'DATE : ' + CONVERT(NVARCHAR(20),STK_TRANS.TRANSDT, 103) + ' ------ ' + 'BILL NO : ' + CONVERT(NVARCHAR(20),STK_TRANS.TRANSNO, 103) + ' ------ ' + 'PARTY NAME : ' + GL_ACCHART.ACCOUNTNM AS HEAD, STK_TRANS.TRANSDT, STK_TRANS.TRANSNO, STK_TRANS.STOREFR, STK_TRANS.PSID, STK_TRANS.ITEMID, STK_TRANS.QTY, STK_TRANS.RATE,  " +
                                     " GL_ACCHART.ACCOUNTNM, STK_STORE.STORENM, STK_ITEM.ITEMNM + ' || ' +STK_ITEM.ITEMCD AS ITEMNM, SUM(STK_TRANS.AMOUNT) AS AMOUNT FROM STK_TRANS INNER JOIN GL_ACCHART ON STK_TRANS.PSID = GL_ACCHART.ACCOUNTCD INNER JOIN STK_STORE ON STK_TRANS.STOREFR = STK_STORE.STOREID INNER JOIN STK_ITEM ON STK_TRANS.ITEMID = STK_ITEM.ITEMID " +
                                     " WHERE (STK_TRANS.TRANSTP = 'SALE') AND STK_STORE.STOREID ='" + lblStore.Text + "' AND (STK_TRANS.TRANSDT BETWEEN '" + fdate + "' AND '" + tdate + "') GROUP BY 'DATE : ' + CONVERT(NVARCHAR(20),STK_TRANS.TRANSDT, 103) + ' ------ ' + 'BILL NO : ' + CONVERT(NVARCHAR(20),STK_TRANS.TRANSNO, 103) + ' ------ ' + 'PARTY NAME : ' + GL_ACCHART.ACCOUNTNM, STK_TRANS.TRANSDT, " +
                                     " STK_TRANS.TRANSNO, STK_TRANS.STOREFR, STK_TRANS.PSID, STK_TRANS.ITEMID, STK_TRANS.QTY, STK_TRANS.RATE, GL_ACCHART.ACCOUNTNM, STK_STORE.STORENM, STK_ITEM.ITEMNM + ' || ' +STK_ITEM.ITEMCD ORDER BY TRANSDT, TRANSNO, ACCOUNTNM", conn);
                }
                else
                {
                    cmd = new SqlCommand("SELECT 'DATE : ' + CONVERT(NVARCHAR(20), STK_TRANS.TRANSDT, 103) + ' -- ' + 'BILL NO : ' + CONVERT(NVARCHAR(20), STK_TRANS.TRANSNO, 103) + ' -- ' + 'PARTY NAME : ' + GL_ACCHART.ACCOUNTNM AS HEAD, STK_TRANS.TRANSDT, STK_TRANS.TRANSNO, STK_TRANS.PSID, STK_TRANS.ITEMID, STK_TRANS.QTY, STK_TRANS.RATE, GL_ACCHART.ACCOUNTNM, STK_STORE.STORENM, STK_ITEM.ITEMNM + ' || ' +STK_ITEM.ITEMCD AS ITEMNM, SUM(STK_TRANS.AMOUNT) AS AMOUNT,  " +
                                     " STK_TRANS.STORETO FROM STK_TRANS INNER JOIN GL_ACCHART ON STK_TRANS.PSID = GL_ACCHART.ACCOUNTCD INNER JOIN STK_ITEM ON STK_TRANS.ITEMID = STK_ITEM.ITEMID INNER JOIN STK_STORE ON STK_TRANS.STORETO = STK_STORE.STOREID WHERE (STK_TRANS.TRANSTP = 'BUY') AND STK_STORE.STOREID ='" + lblStore.Text + "' AND (STK_TRANS.TRANSDT BETWEEN '" + fdate + "' AND '" + tdate + "')  " +
                                     " GROUP BY 'DATE : ' + CONVERT(NVARCHAR(20), STK_TRANS.TRANSDT, 103) + ' -- ' + 'BILL NO : ' + CONVERT(NVARCHAR(20), STK_TRANS.TRANSNO, 103) + ' -- ' + 'PARTY NAME : ' + GL_ACCHART.ACCOUNTNM, STK_TRANS.TRANSDT, STK_TRANS.TRANSNO, STK_TRANS.PSID, STK_TRANS.ITEMID, STK_TRANS.QTY, STK_TRANS.RATE, GL_ACCHART.ACCOUNTNM, STK_STORE.STORENM, STK_ITEM.ITEMNM + ' || ' +STK_ITEM.ITEMCD, STK_TRANS.STORETO ORDER BY STK_TRANS.TRANSDT, STK_TRANS.TRANSNO, GL_ACCHART.ACCOUNTNM", conn);
                }
            }
            SqlDataAdapter da = new SqlDataAdapter(cmd);
            DataSet ds = new DataSet();
            da.Fill(ds);
            conn.Close();
            if (ds.Tables[0].Rows.Count > 0)
            {
                gvRep.DataSource = ds;
                gvRep.DataBind();
                gvRep.Visible = true;
            }
            else
            {
                gvRep.DataSource = ds;
                gvRep.DataBind();
                gvRep.Visible = true;
            }
        }

        protected void gvRep_RowCreated(object sender, GridViewRowEventArgs e)
        {
            bool IsSubTotalRowNeedToAdd = false;
            bool IsGrandTotalRowNeedtoAdd = false;
            if ((strPreviousRowID != string.Empty) && (DataBinder.Eval(e.Row.DataItem, "HEAD") != null))
                if (strPreviousRowID != DataBinder.Eval(e.Row.DataItem, "HEAD").ToString())
                    IsSubTotalRowNeedToAdd = true;
            if ((strPreviousRowID != string.Empty) && (DataBinder.Eval(e.Row.DataItem, "HEAD") == null))
            {
                IsSubTotalRowNeedToAdd = true;
                IsGrandTotalRowNeedtoAdd = true;
                intSubTotalIndex = 0;
            }
            #region Inserting first Row and populating fist Group Header details
            if ((strPreviousRowID == string.Empty) && (DataBinder.Eval(e.Row.DataItem, "HEAD") != null))
            {
                GridView gvRep = (GridView)sender;
                GridViewRow row = new GridViewRow(0, 0, DataControlRowType.DataRow, DataControlRowState.Insert);
                TableCell cell = new TableCell();
                cell.Text = DataBinder.Eval(e.Row.DataItem, "HEAD").ToString();
                cell.ColumnSpan = 5;
                cell.CssClass = "gridHeadStyle";
                row.Cells.Add(cell);
                gvRep.Controls[0].Controls.AddAt(e.Row.RowIndex + intSubTotalIndex, row);
                intSubTotalIndex++;
            }
            #endregion
            if (IsSubTotalRowNeedToAdd)
            {
                #region Adding Sub Total Row
                GridView gvRep = (GridView)sender;
                // Creating a Row          
                GridViewRow row = new GridViewRow(0, 0, DataControlRowType.DataRow, DataControlRowState.Insert);
                //Adding Total Cell          
                TableCell cell = new TableCell();
                cell.Text = "Sub Total : ";
                cell.HorizontalAlign = HorizontalAlign.Right;
                cell.ColumnSpan = 2;
                cell.CssClass = "gridHeadStyle";
                row.Cells.Add(cell);

                //Adding Carton Column         
                cell = new TableCell();
                cell.Text = string.Format("{0:0.00}", dblSubTotalQtyComma);
                cell.HorizontalAlign = HorizontalAlign.Center;
                cell.CssClass = "gridHeadStyle";
                row.Cells.Add(cell);

                //Adding Pieces Column         
                cell = new TableCell();
                cell.HorizontalAlign = HorizontalAlign.Right;
                cell.CssClass = "gridHeadStyle";
                row.Cells.Add(cell);

                //Adding Amount Column         
                cell = new TableCell();
                cell.Text = string.Format("{0:0.00}", dblSubTotalAmountComma);
                cell.HorizontalAlign = HorizontalAlign.Right;
                cell.CssClass = "gridHeadStyle";
                row.Cells.Add(cell);
                //Adding the Row at the RowIndex position in the Grid      
                gvRep.Controls[0].Controls.AddAt(e.Row.RowIndex + intSubTotalIndex, row);
                intSubTotalIndex++;
                #endregion
                #region Adding Next Group Header Details
                if (DataBinder.Eval(e.Row.DataItem, "HEAD") != null)
                {
                    row = new GridViewRow(0, 0, DataControlRowType.DataRow, DataControlRowState.Insert);
                    cell = new TableCell();
                    cell.Text = DataBinder.Eval(e.Row.DataItem, "HEAD").ToString();
                    cell.ColumnSpan = 5;
                    cell.CssClass = "gridHeadStyle";
                    row.Cells.Add(cell);
                    gvRep.Controls[0].Controls.AddAt(e.Row.RowIndex + intSubTotalIndex, row);
                    intSubTotalIndex++;
                }
                #endregion
                #region Reseting the Sub Total Variables
                dblSubTotalQty = 0;
                dblSubTotalAmount = 0;
                #endregion
            }
            if (IsGrandTotalRowNeedtoAdd)
            {
                //#region Grand Total Row
                //GridView gvRep = (GridView)sender;
                //// Creating a Row      
                //GridViewRow row = new GridViewRow(0, 0, DataControlRowType.DataRow, DataControlRowState.Insert);
                ////Adding Total Cell           
                //TableCell cell = new TableCell();
                //cell.Text = "Store Wise Total";
                //cell.HorizontalAlign = HorizontalAlign.Left;
                ////cell.ColumnSpan = 2;
                //cell.CssClass = "GrandTotalRowStyle";
                //row.Cells.Add(cell);

                //////Adding Carton Qty Column          
                //cell = new TableCell();
                //cell.Text = string.Format("{0:0.00}", dblGrandTotalCartonQtyComma);
                //cell.HorizontalAlign = HorizontalAlign.Right;
                //cell.CssClass = "GrandTotalRowStyle";
                //row.Cells.Add(cell);

                //////Adding Pieces Column           
                //cell = new TableCell();
                //cell.Text = string.Format("{0:0.00}", dblGrandTotalPiecesComma);
                //cell.HorizontalAlign = HorizontalAlign.Right;
                //cell.CssClass = "GrandTotalRowStyle";
                //row.Cells.Add(cell);

                //////Adding CLQty Column          
                //cell = new TableCell();
                //cell.Text = string.Format("{0:0.00}", dblGrandTotalCLQtyComma);
                //cell.HorizontalAlign = HorizontalAlign.Right;
                //cell.CssClass = "GrandTotalRowStyle";
                //row.Cells.Add(cell);

                ////Adding Amount Column          
                //cell = new TableCell();
                //cell.Text = string.Format("{0:0.00}", dblGrandTotalBuyAmountComma);
                //cell.HorizontalAlign = HorizontalAlign.Right;
                //cell.CssClass = "GrandTotalRowStyle";
                //row.Cells.Add(cell);

                ////Adding Amount Column          
                //cell = new TableCell();
                //cell.Text = string.Format("{0:0.00}", dblGrandTotalSaleAmountComma);
                //cell.HorizontalAlign = HorizontalAlign.Right;
                //cell.CssClass = "GrandTotalRowStyle";
                //row.Cells.Add(cell);

                ////Adding the Row at the RowIndex position in the Grid     
                //gvRep.Controls[0].Controls.AddAt(e.Row.RowIndex, row);
                //#endregion
            }
        }

        protected void gvRep_RowDataBound(object sender, GridViewRowEventArgs e)
        {

            if (e.Row.RowType == DataControlRowType.DataRow)
            {
                strPreviousRowID = DataBinder.Eval(e.Row.DataItem, "HEAD").ToString();


                string ItemNM = DataBinder.Eval(e.Row.DataItem, "ITEMNM").ToString();
                e.Row.Cells[0].Text = "&nbsp;" + ItemNM;

                string STORENM = DataBinder.Eval(e.Row.DataItem, "STORENM").ToString();
                e.Row.Cells[1].Text = "&nbsp;" + STORENM;

                decimal QTY = Convert.ToDecimal(DataBinder.Eval(e.Row.DataItem, "QTY").ToString());
                string qt = SpellAmount.comma(QTY);
                e.Row.Cells[2].Text = qt;

                decimal RATE = Convert.ToDecimal(DataBinder.Eval(e.Row.DataItem, "RATE").ToString());
                string rt = SpellAmount.comma(RATE);
                e.Row.Cells[3].Text = rt;

                decimal AMOUNT = Convert.ToDecimal(DataBinder.Eval(e.Row.DataItem, "AMOUNT").ToString());
                string amt = SpellAmount.comma(AMOUNT);
                e.Row.Cells[4].Text = amt;

                // Cumulating Sub Total            
                dblSubTotalQty += QTY;
                dblSubTotalQtyComma = SpellAmount.comma(dblSubTotalQty);

                dblSubTotalAmount += AMOUNT;
                dblSubTotalAmountComma = SpellAmount.comma(dblSubTotalAmount);

                dblGrandTotalQty += QTY;
                dblGrandTotalQtyComma = SpellAmount.comma(dblGrandTotalQty);

                dblGrandTotalAmount += AMOUNT;
                dblGrandTotalAmountComma = SpellAmount.comma(dblGrandTotalAmount);

                // This is for cumulating the values  
                //if (e.Row.RowType == DataControlRowType.DataRow)
                //{
                //    e.Row.Attributes.Add("onmouseover", "this.style.backgroundColor='#ddd'");
                //    e.Row.Attributes.Add("onmouseout", "this.style.backgroundColor=''");
                //    e.Row.Attributes.Add("style", "cursor:pointer;");
                //    e.Row.Attributes["onclick"] = ClientScript.GetPostBackClientHyperlink(gvRep, "Select$" + e.Row.RowIndex);
                //}
            }
            else if (e.Row.RowType == DataControlRowType.Footer)
            {
                e.Row.Cells[1].Text = "Grand Total :   ";
                e.Row.Cells[1].HorizontalAlign = HorizontalAlign.Right;
                e.Row.Cells[2].Text = dblGrandTotalQtyComma;
                e.Row.Cells[2].HorizontalAlign = HorizontalAlign.Center;
                e.Row.Cells[4].Text = dblGrandTotalAmountComma;
                e.Row.Cells[4].HorizontalAlign = HorizontalAlign.Right;
                e.Row.Font.Bold = true;
            }

            ShowHeader(gvRep);
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