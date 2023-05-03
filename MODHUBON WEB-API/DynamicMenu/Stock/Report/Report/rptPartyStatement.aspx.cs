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
    public partial class rptPartyStatement : System.Web.UI.Page
    {
        IFormatProvider dateformat = new System.Globalization.CultureInfo("fr-FR", true);

        // To keep track of the previous row Group Identifier
        string strPreviousPartyRowID = string.Empty;
        string strPreviousInvoiceRowID = string.Empty;
        //string strPreviousAccountSectionRowID = string.Empty;

        string strPreviousRowTempParty = string.Empty; // First Level Grouping Track Id (Used to identify the group is getting changed)
        string strPreviousRowTempInvoice = string.Empty; // Second Level Grouping Track Id (Used

        string strPartyGroupHeaderText = string.Empty;
        string strINvoiceGroupHeaderText = string.Empty;

        // To keep track the Index of Group Total
        //int intSubTotalIndex = 1;

        // To temporarily store Sub Total    
        decimal dblInvoiceTotalAmount = 0;
        decimal dblPartyTotalAmount = 0;
        // To temporarily store Grand Total    
        //decimal dblGrandTotalAmount = 0;
        //string AmountComma = "";
        string dblInvoiceTotalAmountComma = "";
        string dblPartyTotalAmountComma = "";

        //string dblGrandTotalAmountComma = "";

        int iKeyIDCount = 0;
        int iRowsCount = 0;
        decimal iAddPrices = 0;
        string sKeyID = "";

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
                string From = Session["From"].ToString();
                string To = Session["To"].ToString();
                string PS =  Session["PS"].ToString();
                string PSCODE = Session["PSCODE"].ToString();

                if (Type == "SALE")
                {
                    lblType.Text = "SALES";
                }
                else
                    lblType.Text = Type;

                DateTime FDate = DateTime.Parse(From, dateformat, System.Globalization.DateTimeStyles.AssumeLocal);
                lblFDate.Text = FDate.ToString("dd-MMM-yyyy");

                DateTime TDate = DateTime.Parse(To, dateformat, System.Globalization.DateTimeStyles.AssumeLocal);
                lblTDate.Text = TDate.ToString("dd-MMM-yyyy");

                lblPartyName.Text = PS;

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
            string From = Session["From"].ToString();
            string To = Session["To"].ToString();
            string PS = Session["PS"].ToString();
            string PSCODE = Session["PSCODE"].ToString();

            DateTime FDate = DateTime.Parse(From, dateformat, System.Globalization.DateTimeStyles.AssumeLocal);
            string FDT = FDate.ToString("yyyy-MM-dd");

            DateTime TDate = DateTime.Parse(To, dateformat, System.Globalization.DateTimeStyles.AssumeLocal);
            string TDT = TDate.ToString("yyyy-MM-dd");

            conn.Open();
            SqlCommand cmd = new SqlCommand();
            cmd = new SqlCommand("", conn);

            if (Type == "SALE")
            {
                cmd.CommandType = CommandType.Text;
                cmd.CommandText = (" SELECT STK_TRANS.PSID, GL_ACCHART.ACCOUNTNM, CONVERT(NVARCHAR(20), STK_TRANS.TRANSDT, 103) AS TRANSDT, STK_TRANS.TRANSDT AS TD, STK_TRANS.TRANSNO AS INVOICE, STK_TRANS.ITEMID, STK_TRANS.QTY, " +
                                   " STK_TRANS.AMOUNT, STK_ITEM.ITEMNM + ' || ' + STK_ITEM.ITEMCD AS ITEMNM FROM STK_TRANS INNER JOIN GL_ACCHART ON STK_TRANS.PSID = GL_ACCHART.ACCOUNTCD INNER JOIN " +
                                   " STK_ITEM ON STK_TRANS.ITEMID = STK_ITEM.ITEMID " +
                                   " WHERE (STK_TRANS.TRANSTP = 'SALE') AND (STK_TRANS.TRANSDT BETWEEN '" + FDT + "' AND '" + TDT + "') AND (STK_TRANS.PSID = '" + PSCODE + "' ) order by PSID, TD, INVOICE");
            }

            else if (Type == "BUY")
            {
                cmd.CommandType = CommandType.Text;
                cmd.CommandText = (" SELECT STK_TRANS.PSID, GL_ACCHART.ACCOUNTNM, CONVERT(NVARCHAR(20), STK_TRANS.TRANSDT, 103) AS TRANSDT, STK_TRANS.TRANSDT AS TD, STK_TRANS.TRANSNO AS INVOICE, STK_TRANS.ITEMID, STK_TRANS.QTY,  " +
                                   " STK_TRANS.AMOUNT, STK_ITEM.ITEMNM + ' || ' + STK_ITEM.ITEMCD AS ITEMNM FROM STK_TRANS INNER JOIN GL_ACCHART ON STK_TRANS.PSID = GL_ACCHART.ACCOUNTCD INNER JOIN " +
                                   " STK_ITEM ON STK_TRANS.ITEMID = STK_ITEM.ITEMID " +
                                   " WHERE (STK_TRANS.TRANSTP = 'BUY') AND (STK_TRANS.TRANSDT BETWEEN '" + FDT + "' AND '" + TDT + "') AND (STK_TRANS.PSID = '" + PSCODE + "' ) order by PSID, TD, INVOICE");
            }

            SqlDataAdapter da = new SqlDataAdapter(cmd);
            DataTable ds = new DataTable();
            da.Fill(ds);
            conn.Close();
            if (ds.Rows.Count > 0)
            {
                //iRowsPartyCount = ds.Rows.Count - 1;
                iRowsCount = ds.Rows.Count - 1;
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
                string sName = "";

                string TRANSDT = DataBinder.Eval(e.Row.DataItem, "TRANSDT").ToString();
                e.Row.Cells[0].Text = TRANSDT;

                string INVOICE = DataBinder.Eval(e.Row.DataItem, "INVOICE").ToString();
                e.Row.Cells[1].Text = INVOICE;

                string ITEMNM = DataBinder.Eval(e.Row.DataItem, "ITEMNM").ToString();
                e.Row.Cells[2].Text = "&nbsp;" + ITEMNM;

                decimal QTY = Convert.ToDecimal(DataBinder.Eval(e.Row.DataItem, "QTY").ToString());
                string qt = SpellAmount.comma(QTY);
                e.Row.Cells[3].Text = qt;

                decimal AMOUNT = Convert.ToDecimal(DataBinder.Eval(e.Row.DataItem, "AMOUNT").ToString());
                string amnt = SpellAmount.comma(AMOUNT);
                e.Row.Cells[5].Text = amnt;

                string sinAMT = SpellAmount.comma(AMOUNT);

                decimal rt = (AMOUNT/QTY);
                string rate = SpellAmount.comma(rt);
                e.Row.Cells[4].Text = rate;

                if ((e.Row.RowIndex == 0))
                {
                    sName = "Sub Total : " + "&nbsp;" + "&nbsp;" + "&nbsp;" + "&nbsp;" + "&nbsp;" + "&nbsp;" + "&nbsp;" + "&nbsp;";
                    sKeyID = e.Row.Cells[1].Text;
                    iAddPrices = Convert.ToDecimal(e.Row.Cells[5].Text);
                }
                else
                {
                    sName = "Sub Total :" + "&nbsp;" + "&nbsp;" + "&nbsp;" + "&nbsp;" + "&nbsp;" + "&nbsp;" + "&nbsp;" + "&nbsp;";
                    if (sKeyID == (e.Row.Cells[1].Text))
                    {
                        iKeyIDCount = iKeyIDCount + 1;
                        iAddPrices += Convert.ToDecimal(e.Row.Cells[5].Text);

                        //dblInvoiceTotalAmountComma = SpellAmount.comma(iAddPrices);
                    }
                    else
                    {
                        iKeyIDCount = iKeyIDCount + 1;
                        sKeyID = e.Row.Cells[1].Text;
                        Table tblTemp = (Table)this.GridView1.Controls[0];
                        int intIndex = tblTemp.Rows.GetRowIndex(e.Row);
                        GridViewRow gvrTemp = new GridViewRow(intIndex, intIndex, DataControlRowType.Separator, DataControlRowState.Normal);
                        TableCell cellTemp = new TableCell();
                        //cellTemp.BackColor = System.Drawing.ColorTranslator.FromHtml((iKeyIDCount != 0) ? "#ffffff" : "#ffffff");
                        cellTemp.CssClass = "subTotalStyle";
                        cellTemp.ColumnSpan = GridView1.Columns.Count;
                        cellTemp.HorizontalAlign = HorizontalAlign.Right;
                        string subtotalcomma = SpellAmount.comma(iAddPrices);
                        cellTemp.Text = (iKeyIDCount != 0) ? sName + subtotalcomma : "";
                        cellTemp.Height = Unit.Pixel(14);
                        gvrTemp.Cells.Add(cellTemp);
                        tblTemp.Controls.AddAt(intIndex, gvrTemp);
                        iKeyIDCount = 0;
                        iAddPrices = Convert.ToDecimal(e.Row.Cells[5].Text); ;
                    }

                    if (iRowsCount == e.Row.RowIndex)
                    {
                        iKeyIDCount = iKeyIDCount + 1;
                        Table tblTemp = (Table)this.GridView1.Controls[0];
                        int intIndex = tblTemp.Rows.GetRowIndex(e.Row);
                        GridViewRow gvrTemp = new GridViewRow(intIndex + 1, intIndex + 1, DataControlRowType.Separator, DataControlRowState.Normal);
                        TableCell cellTemp = new TableCell();
                        //cellTemp.BackColor = System.Drawing.ColorTranslator.FromHtml((iKeyIDCount != 0) ? "#ffffff" : "#ffffff");
                        cellTemp.CssClass = "subTotalStyle";
                        cellTemp.ColumnSpan = GridView1.Columns.Count;
                        cellTemp.HorizontalAlign = HorizontalAlign.Right;
                        string subtotalcomma = SpellAmount.comma(iAddPrices);
                        cellTemp.Text = (iKeyIDCount != 0) ? sName + subtotalcomma : "";
                        cellTemp.Height = Unit.Pixel(14);
                        gvrTemp.Cells.Add(cellTemp);
                        tblTemp.Controls.AddAt(intIndex + 1, gvrTemp);
                        iKeyIDCount = 0;
                        iAddPrices = 0;
                    }

                }

                
                // Cumulating Sub Total            

                //if (strPreviousInvoiceRowID != string.Empty)
                //{
                    dblInvoiceTotalAmount += AMOUNT;
                    dblInvoiceTotalAmountComma = SpellAmount.comma(dblInvoiceTotalAmount);
                //}
                    dblPartyTotalAmount += AMOUNT;
                    dblPartyTotalAmountComma = SpellAmount.comma(dblPartyTotalAmount);

                // Cumulating Grand Total           
                //dblGrandTotalAmount += AMOUNT;
                //dblGrandTotalAmountComma = SpellAmount.comma(dblGrandTotalAmount);

                // This is for cumulating the values  
                //if (e.Row.RowType == DataControlRowType.DataRow)
                //{
                //    e.Row.Attributes.Add("onmouseover", "this.style.backgroundColor='#ddd'");
                //    e.Row.Attributes.Add("onmouseout", "this.style.backgroundColor=''");
                //    e.Row.Attributes.Add("style", "cursor:pointer;");
                //    e.Row.Attributes["onclick"] = ClientScript.GetPostBackClientHyperlink(GridView1, "Select$" + e.Row.RowIndex);
                //}
                //e.Row.Style.Add("display", "block");
                //e.Row.CssClass = "ExpandCollapse" + strPreviousPartyRowID;
            }

            else if (e.Row.RowType == DataControlRowType.Footer)
            {
                e.Row.Cells[4].Text = "Grand Total :   ";
                e.Row.Cells[4].HorizontalAlign = HorizontalAlign.Right;
                e.Row.Cells[5].Text = dblPartyTotalAmountComma;
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

        public static void MergeRows(GridView gridView)
        {
            //for (int rowIndex = gridView.Rows.Count - 2; rowIndex >= 0; rowIndex--)
            //{
            //    GridViewRow row = gridView.Rows[rowIndex];
            //    GridViewRow previousRow = gridView.Rows[rowIndex + 1];

            //    for (int i = 0; i < gridView.Rows.Count; i++)
            //    {
            //        if (row.Cells[i].Text == previousRow.Cells[i].Text)
            //        {
            //            row.Cells[i].RowSpan = previousRow.Cells[i].RowSpan < 2 ? 2 :
            //                               previousRow.Cells[i].RowSpan + 1;
            //            previousRow.Cells[i].Visible = false;
            //        }
            //    }
            //}



        }

        protected void GridView1_PreRender(object sender, EventArgs e)
        {
            rptPartyStatement.MergeRows(GridView1);
        }
        
    }
}