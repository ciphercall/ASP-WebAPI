using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Threading;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
//using AjaxControlToolkit.HTMLEditor.ToolbarButton;
using AlchemyAccounting;

namespace DynamicMenu.Stock.Report.Report
{
    public partial class rptQtyOrderBillDifference : System.Web.UI.Page
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


        decimal dblSubTotalQTYS = 0;
        decimal dblGrandTotalQTYS = 0;
        string dblSubTotalQTYSComma = "0";
        string dblGrandTotalQTYSComma = "0";


        decimal dblSubTotalQtyD = 0;
        decimal dblGrandTotalQtyD = 0;
        string dblSubTotalCartonQtyCommaD = "0";
        string dblGrandTotalQtyCommaD = "0";

       
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

                    showGrid();
                }
                catch
                {

                }
            }
        }
        public void showGrid()
        {
            SqlConnection conn = new SqlConnection(dbFunctions.connection);

            string partyid = Session["PartyId"].ToString();
            lblParty.Text = Session["PartyName"].ToString();

            string dateFrom = Session["From"].ToString();
            DateTime datefrom = DateTime.Parse(dateFrom, dateformat, System.Globalization.DateTimeStyles.AssumeLocal);
            string from = datefrom.ToString("yyyy/MM/dd");

            string dateTo = Session["To"].ToString();
            DateTime datato = DateTime.Parse(dateTo, dateformat, System.Globalization.DateTimeStyles.AssumeLocal);
            string to = datato.ToString("yyyy/MM/dd");

            lblFromDate.Text = datefrom.ToString("dd-MM-yyyy");
            lblToDate.Text = datato.ToString("dd-MM-yyyy");

            string query = "";
            var party = "";
            if (partyid == "ALL")
            {
                party = " ";
            }
            else
            {
                party = "AND PSID = '" + partyid + "'";
            }
            conn.Open();

            query = $@"SELECT PARTYNM, ITEMNM, SUM(ISNULL(ORDERQTY,0)) ORDERQTY, SUM(ISNULL(SALEQTY,0)) SALEQTY, SUM(ISNULL(ORDERQTY,0)) - SUM(ISNULL(SALEQTY,0)) DIFFQTY
FROM(
SELECT PSID, ITEMID, SUM(ISNULL(QTY,0)) ORDERQTY, 0 SALEQTY
FROM STK_TRANS WHERE TRANSTP = 'IORD' AND BILLDT BETWEEN '{from}' AND '{to}'  {party}
GROUP BY PSID, ITEMID
UNION SELECT PSID, ITEMID, 0 ORDERQTY, SUM(ISNULL(QTY,0)) SALEQTY
FROM STK_TRANS WHERE TRANSTP = 'SALE' AND TRANSDT BETWEEN '{from}' AND '{to}'  {party} AND SUBSTRING(ITEMID,1,5)<>10110
GROUP BY PSID, ITEMID
) A INNER JOIN STK_PARTY B ON A.PSID = B.PARTYID
INNER JOIN STK_ITEM C ON A.ITEMID = C.ITEMID
GROUP BY PARTYNM, ITEMNM
ORDER BY PARTYNM, ITEMNM";

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

            bool IsSubTotalRowNeedToAdd = false;
            bool IsGrandTotalRowNeedtoAdd = false;
            if ((strPreviousRowID != string.Empty) && (DataBinder.Eval(e.Row.DataItem, "PARTYNM") != null))
                if (strPreviousRowID != DataBinder.Eval(e.Row.DataItem, "PARTYNM").ToString())
                    IsSubTotalRowNeedToAdd = true;
            if ((strPreviousRowID != string.Empty) && (DataBinder.Eval(e.Row.DataItem, "PARTYNM") == null))
            {
                IsSubTotalRowNeedToAdd = true;
                //    IsGrandTotalRowNeedtoAdd = true;
                intSubTotalIndex = 0;
            }
            #region Inserting first Row and populating fist Group Header details
            if ((strPreviousRowID == string.Empty) && (DataBinder.Eval(e.Row.DataItem, "PARTYNM") != null))
            {
                GridView GridView1 = (GridView)sender;
                GridViewRow row = new GridViewRow(0, 0, DataControlRowType.DataRow, DataControlRowState.Insert);
                TableCell cell = new TableCell();
                cell.Text = "" + DataBinder.Eval(e.Row.DataItem, "PARTYNM");
                cell.ColumnSpan = 4;
                cell.Font.Bold = true;
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
                cell.Text = "Party Wise Total";
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
                cell.Text = string.Format("{0:0.00}", dblSubTotalQTYSComma); ;
                cell.HorizontalAlign = HorizontalAlign.Center;
                cell.CssClass = "SubTotalRowStyle";
                row.Cells.Add(cell);

                cell = new TableCell();
                cell.Text = string.Format("{0:0.00}", dblSubTotalCartonQtyCommaD);
                cell.HorizontalAlign = HorizontalAlign.Center;
                cell.CssClass = "SubTotalRowStyle";
                row.Cells.Add(cell);

                GridView1.Controls[0].Controls.AddAt(e.Row.RowIndex + intSubTotalIndex, row);
                intSubTotalIndex++;
                //#endregion
                #region Adding Next Group Header Details
                if (DataBinder.Eval(e.Row.DataItem, "PARTYNM") != null)
                {
                    row = new GridViewRow(0, 0, DataControlRowType.DataRow, DataControlRowState.Insert);
                    cell = new TableCell();
                    cell.Text = "" + DataBinder.Eval(e.Row.DataItem, "PARTYNM").ToString();
                    cell.ColumnSpan = 4;
                    cell.Font.Bold = true;
                    cell.CssClass = "GroupHeaderStyle";
                    row.Cells.Add(cell);
                    GridView1.Controls[0].Controls.AddAt(e.Row.RowIndex + intSubTotalIndex, row);
                    intSubTotalIndex++;
                }
                #endregion
                #region Reseting the Sub Total Variables

                dblSubTotalQty = 0;
                dblSubTotalQtyD = 0;
                dblSubTotalQTYS = 0;
                #endregion
            }
        }
        protected void GridView1_OnRowDataBound(object sender, GridViewRowEventArgs e)
        {
            // This is for cumulating the values       
            if (e.Row.RowType == DataControlRowType.DataRow)
            {
                strPreviousRowID = DataBinder.Eval(e.Row.DataItem, "PARTYNM").ToString();

                string ItemNM = DataBinder.Eval(e.Row.DataItem, "ITEMNM").ToString();
                e.Row.Cells[0].Text = "&nbsp;" + ItemNM;

                //string ITEMNMB = DataBinder.Eval(e.Row.DataItem, "ITEMNMB").ToString();
                //e.Row.Cells[1].Text = "&nbsp;" + ITEMNMB;


                decimal ORDERQTY = Convert.ToDecimal(DataBinder.Eval(e.Row.DataItem, "ORDERQTY").ToString());
                e.Row.Cells[1].Text = ORDERQTY + "&nbsp;";

                decimal SALEQTY = Convert.ToDecimal(DataBinder.Eval(e.Row.DataItem, "SALEQTY").ToString());
                e.Row.Cells[2].Text = Convert.ToDecimal(SALEQTY).ToString("F2") + "&nbsp;";


                decimal DIFFQTY = Convert.ToDecimal(DataBinder.Eval(e.Row.DataItem, "DIFFQTY").ToString());
                e.Row.Cells[3].Text = DIFFQTY + "&nbsp;";




                dblSubTotalQty += ORDERQTY;
                dblSubTotalCartonQtyComma = SpellAmount.comma(dblSubTotalQty);
                dblGrandTotalQty += ORDERQTY;
                dblGrandTotalQtyComma = SpellAmount.comma(dblGrandTotalQty);


                dblSubTotalQTYS += SALEQTY;
                dblSubTotalQTYSComma = SpellAmount.comma(dblSubTotalQTYS);
                dblGrandTotalQTYS += SALEQTY;
                dblGrandTotalQTYSComma = SpellAmount.comma(dblGrandTotalQTYS);

                dblSubTotalQtyD += DIFFQTY;
                dblSubTotalCartonQtyCommaD = SpellAmount.comma(dblSubTotalQtyD);
                dblGrandTotalQtyD += DIFFQTY;
                dblGrandTotalQtyCommaD = SpellAmount.comma(dblGrandTotalQtyD);


            }
            // This is for cumulating the values  
            else if (e.Row.RowType == DataControlRowType.Footer)
            {
                e.Row.Cells[0].Text = "Total : ";
                e.Row.Cells[0].HorizontalAlign = HorizontalAlign.Right;
                e.Row.Cells[1].Text = dblGrandTotalQtyComma;
                e.Row.Cells[1].HorizontalAlign = HorizontalAlign.Center;
                e.Row.Cells[2].Text = dblGrandTotalQTYSComma;
                e.Row.Cells[2].HorizontalAlign = HorizontalAlign.Center;
                e.Row.Cells[3].Text = dblGrandTotalQtyCommaD;
                e.Row.Cells[3].HorizontalAlign = HorizontalAlign.Center;

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