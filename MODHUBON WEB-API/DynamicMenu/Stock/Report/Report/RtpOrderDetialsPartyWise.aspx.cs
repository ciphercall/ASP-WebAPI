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
    public partial class RtpOrderDetialsPartyWise : System.Web.UI.Page
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

        decimal dblSubTotalRate = 0;
        decimal dblGrandTotalRate = 0;
        string dblSubTotalCartonRateComma = "0";
        string dblGrandTotalRateComma = "0";

        decimal dblSubTotalAmount = 0;
        decimal dblGrandTotalAmount = 0;
        string dblSubTotalAmountomma = "0";
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
          

            conn.Open();

            query = $@"SELECT 'Order Date: ' + REPLACE(CONVERT(NVARCHAR, STK_TRANSMST.TRANSDT, 6),' ','-') + ', Time: '+ SUBSTRING(CONVERT(NVARCHAR, STK_TRANSMST.INTIME, 108),0,6) 
            +', Invoice No: ' + STK_TRANSMST.TRANSNO + ', Bill Date: ' + 
            REPLACE(CONVERT(NVARCHAR, STK_TRANSMST.BILLDT, 6),' ','-')  AS NAME, 
            C.USERNM, A.TRANSSL, B.ITEMNM, B.ITEMNMB, 
            B.ITEMNMB AS Expr1, A.REMARKS, ISNULL(A.QTY, 0) AS QTY,A.RATE, A.QTY*A.RATE AS AMOUNT
            FROM STK_TRANS AS A INNER JOIN
            STK_ITEM AS B ON A.ITEMID = B.ITEMID INNER JOIN
            STK_TRANSMST ON A.COMPID = STK_TRANSMST.COMPID AND A.TRANSTP = STK_TRANSMST.TRANSTP 
            AND A.TRANSYY = STK_TRANSMST.TRANSYY AND 
            A.TRANSNO = STK_TRANSMST.TRANSNO LEFT OUTER JOIN
            STK_USERPS AS C ON A.PSID = C.PSID AND A.USERID = C.USERCD
            WHERE STK_TRANSMST.TRANSTP = 'IORD' AND STK_TRANSMST.TRANSDT BETWEEN '{from}' AND '{to}' AND STK_TRANSMST.PSID = '{partyid}'
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
        protected void GridView1_RowCreated(object sender, GridViewRowEventArgs e)
        {
            bool IsSubTotalRowNeedToAdd = false;
            bool IsGrandTotalRowNeedtoAdd = false;
            if ((strPreviousRowID != string.Empty) && (DataBinder.Eval(e.Row.DataItem, "NAME") != null))
                if (strPreviousRowID != DataBinder.Eval(e.Row.DataItem, "NAME").ToString())
                    IsSubTotalRowNeedToAdd = true;
            if ((strPreviousRowID != string.Empty) && (DataBinder.Eval(e.Row.DataItem, "NAME") == null))
            {
                IsSubTotalRowNeedToAdd = true;
                //    IsGrandTotalRowNeedtoAdd = true;
                intSubTotalIndex = 0;
            }
            #region Inserting first Row and populating fist Group Header details
            if ((strPreviousRowID == string.Empty) && (DataBinder.Eval(e.Row.DataItem, "NAME") != null))
            {
                GridView GridView1 = (GridView)sender;
                GridViewRow row = new GridViewRow(0, 0, DataControlRowType.DataRow, DataControlRowState.Insert);
                TableCell cell = new TableCell();
                cell.Text = "" + DataBinder.Eval(e.Row.DataItem, "NAME");
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
                cell.Text = "Party Wise Total";
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
                cell.HorizontalAlign = HorizontalAlign.Center;
                cell.CssClass = "SubTotalRowStyle";
                row.Cells.Add(cell);

                cell = new TableCell();
                cell.Text = string.Format("{0:0.00}", dblSubTotalAmountomma);
                cell.HorizontalAlign = HorizontalAlign.Center;
                cell.CssClass = "SubTotalRowStyle";
                row.Cells.Add(cell);

                GridView1.Controls[0].Controls.AddAt(e.Row.RowIndex + intSubTotalIndex, row);
                intSubTotalIndex++;
                //#endregion
                #region Adding Next Group Header Details
                if (DataBinder.Eval(e.Row.DataItem, "NAME") != null)
                {
                    row = new GridViewRow(0, 0, DataControlRowType.DataRow, DataControlRowState.Insert);
                    cell = new TableCell();
                    cell.Text = "" + DataBinder.Eval(e.Row.DataItem, "NAME").ToString();
                    cell.ColumnSpan = 5;
                    cell.CssClass = "GroupHeaderStyle";
                    row.Cells.Add(cell);
                    GridView1.Controls[0].Controls.AddAt(e.Row.RowIndex + intSubTotalIndex, row);
                    intSubTotalIndex++;
                }
                #endregion
                #region Reseting the Sub Total Variables

                dblSubTotalQty = 0;
                #endregion
            }
        }

        protected void GridView1_RowDataBound(object sender, GridViewRowEventArgs e)
        {
            // This is for cumulating the values       
            if (e.Row.RowType == DataControlRowType.DataRow)
            {
                strPreviousRowID = DataBinder.Eval(e.Row.DataItem, "NAME").ToString();

                string ItemNM = DataBinder.Eval(e.Row.DataItem, "ITEMNM").ToString();
                e.Row.Cells[0].Text = "&nbsp;" + ItemNM;

                string ITEMNMB = DataBinder.Eval(e.Row.DataItem, "ITEMNMB").ToString();
                e.Row.Cells[1].Text = "&nbsp;" + ITEMNMB;

                string REMARKS = DataBinder.Eval(e.Row.DataItem, "REMARKS").ToString();
                e.Row.Cells[2].Text = "&nbsp;" + REMARKS;

                decimal Qty = Convert.ToDecimal(DataBinder.Eval(e.Row.DataItem, "QTY"));
                e.Row.Cells[3].Text = Qty +"&nbsp;";

                decimal RATE = Convert.ToDecimal(DataBinder.Eval(e.Row.DataItem, "RATE"));
                e.Row.Cells[4].Text = RATE+ "&nbsp;" ;

                decimal AMOUNT = Convert.ToDecimal(DataBinder.Eval(e.Row.DataItem, "AMOUNT").ToString());
                e.Row.Cells[5].Text = AMOUNT.ToString("F2") + "&nbsp;";

                dblSubTotalQty += Qty;
                dblSubTotalCartonQtyComma = SpellAmount.comma(dblSubTotalQty);
                dblGrandTotalQty += Qty;
                dblGrandTotalQtyComma = SpellAmount.comma(dblGrandTotalQty);

                dblSubTotalAmount +=Convert.ToDecimal(AMOUNT.ToString("f2"));
                dblSubTotalAmountomma = SpellAmount.comma(dblSubTotalAmount);
                dblGrandTotalAmount+= Convert.ToDecimal(AMOUNT.ToString("f2"));
                dblGrandTotalAmountComma = SpellAmount.comma(dblGrandTotalAmount);

            }
            // This is for cumulating the values  
            else if (e.Row.RowType == DataControlRowType.Footer)
            {
                e.Row.Cells[2].Text = "Total : ";
                e.Row.Cells[2].HorizontalAlign = HorizontalAlign.Right;
                e.Row.Cells[3].Text = dblGrandTotalQtyComma;
                e.Row.Cells[3].HorizontalAlign = HorizontalAlign.Right;
                e.Row.Cells[5].Text = dblGrandTotalAmountComma;
                e.Row.Cells[5].HorizontalAlign = HorizontalAlign.Right;

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