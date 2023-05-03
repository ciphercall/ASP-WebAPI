using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using AlchemyAccounting;

namespace DynamicMenu.Accounts.Report.Report
{
    public partial class rptAccountsRecST : System.Web.UI.Page
    {
        IFormatProvider dateformat = new System.Globalization.CultureInfo("fr-FR", true);

        string strPreviousRowID = string.Empty;
        // To keep track the Index of Group Total    
        int intSubTotalIndex = 1;

        // To temporarily store Sub Total    
        decimal dblSubTotalQty = 0;
        decimal dblSubTotalAmount = 0;

        decimal dblSubTotalLG = 0;
        decimal dblSubTotalLC = 0;
        decimal dblSubTotalTOT = 0;
        decimal dblsl = 0;


        // To temporarily store Grand Total    
        decimal dblGrandTotalQty = 0;
        decimal dblGrandTotalAmount = 0;
        decimal dblGrandTotalLGAmount = 0;
        decimal dblGrandTotalLCAmount = 0;
        decimal dblGrandTotalTOTAmount = 0;

        //string AmountComma = "";
        string dblSubTotalQtyComma = "0";
        string dblSubTotalAmountComma = "0";
        string dblSubTotalLGComma = "0";
        string dblSubTotalLCComma = "0";
        string dblSubTotalTOTComma = "0";

        string dblGrandTotalQtyComma = "0";
        string dblGrandTotalAmountComma = "0";
        string dblGrandTotalLGComma = "0";
        string dblGrandTotalLCComma = "0";
        string dblGrandTotalTOTComma = "0";

        protected void Page_Load(object sender, EventArgs e)
        {
            if (Session["UserName"] == null)
            {
                Response.Redirect("~/Login/UI/Login.aspx");
            }
            else
            {
                string compid = Session["COMPANYID"].ToString();

                dbFunctions.lblAdd(@"SELECT COMPNM FROM ASL_COMPANY WHERE COMPID='" + compid + "'", lblCompanyNM);
                dbFunctions.lblAdd(@"SELECT ADDRESS FROM ASL_COMPANY WHERE COMPID='" + compid + "'", lblAddress);

              DateTime PrintDate = DateTime.Now;
                  // DateTime PrintDate = (DateTime.Parse(DateTime.Now.ToString(), dateformat, System.Globalization.DateTimeStyles.AssumeLocal));

                //string td = PrintDate.ToString("dd-MMM-yyyy hh:mm tt");
                //lblPrintDate.Text = td;

                lblPrintDate.Text = dbFunctions.Timezone(DateTime.Now).ToString("dd-MMM-yyyy hh:mm: tt");



                string Date = Session["Date"].ToString();


                DateTime date = DateTime.Parse(Date, dateformat, System.Globalization.DateTimeStyles.AssumeLocal);
                string fdate = date.ToString("dd-MMM-yy");
                lblFdt.Text = fdate;

                showGrid();
            }
        }

        public void showGrid()
        {
            string connectionString = ConfigurationManager.ConnectionStrings["AslDbContext"].ConnectionString;
            SqlConnection conn = new SqlConnection(connectionString);

            string date = Session["Date"].ToString();
            DateTime fd = DateTime.Parse(date, dateformat, System.Globalization.DateTimeStyles.AssumeLocal);
            string fdate = fd.ToString("yyyy-MM-dd");

            SqlCommand cmd = new SqlCommand();
            conn.Open();
            cmd = new SqlCommand("SELECT A.PARTYNM, SUM(ISNULL(LEDGERAMT,0)) LEDGERAMT, SUM(ISNULL(LCRCVAMT,0)) LCRCVAMT, " +
                                 "SUM(ISNULL(LEDGERAMT, 0)) + SUM(ISNULL(LCRCVAMT, 0)) TOTALAMT FROM( " +
                                 "SELECT ACCOUNTNM PARTYNM, SUM(ISNULL(DEBITAMT, 0)) - SUM(ISNULL(CREDITAMT, 0)) LEDGERAMT, 0 LCRCVAMT " +
                                 "FROM GL_ACCHART A LEFT OUTER JOIN GL_MASTER B ON A.ACCOUNTCD = B.DEBITCD " +
                                 "WHERE CONTROLCD = '102020100000' AND TRANSDT <= '" + fdate + "' GROUP BY ACCOUNTNM " +
                                 "UNION " +
                                 "SELECT ACCOUNTNM PARTYNM, 0 LEDGERAMT, SUM(ISNULL(DEBITAMT, 0)) - SUM(ISNULL(CREDITAMT, 0)) LCRCVAMT " +
                                 "FROM GL_ACCHART A INNER JOIN LC_BASIC B ON A.ACCOUNTCD = B.PARTYID AND B.LCTP = 'RECEIVABLE' " +
                                 "LEFT OUTER JOIN GL_MASTER C ON '1020203' + LCID = C.DEBITCD WHERE TRANSDT <= '" + fdate + "' GROUP BY ACCOUNTNM) A GROUP BY A.PARTYNM HAVING (SUM(ISNULL(LEDGERAMT, 0)) + SUM(ISNULL(LCRCVAMT, 0))) != 0 ", conn);

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

        private void LoadSerial()
        {
            int i = 1;

            foreach (GridViewRow grid in gvRep.Rows)
            {
                int a = i;
                string b = a.ToString();
                grid.Cells[0].Text = b;
                i++;
            }
        }



        protected void gvRep_RowDataBound(object sender, GridViewRowEventArgs e)
        {
            if (e.Row.RowType == DataControlRowType.DataRow)
            {
                LoadSerial();

                string PartyNM = DataBinder.Eval(e.Row.DataItem, "PARTYNM").ToString();
                e.Row.Cells[1].Text = "&nbsp;" + PartyNM;

                decimal LGAMOUNT = Convert.ToDecimal(DataBinder.Eval(e.Row.DataItem, "LEDGERAMT").ToString());
                string lgamt = SpellAmount.comma(LGAMOUNT);
                e.Row.Cells[2].Text = lgamt;


                decimal LCAMOUNT = Convert.ToDecimal(DataBinder.Eval(e.Row.DataItem, "LCRCVAMT").ToString());
                string lcamt = SpellAmount.comma(LCAMOUNT);
                e.Row.Cells[3].Text = lcamt;


                decimal TOTAMOUNT = Convert.ToDecimal(DataBinder.Eval(e.Row.DataItem, "TOTALAMT").ToString());
                string totamt = SpellAmount.comma(TOTAMOUNT);
                e.Row.Cells[4].Text = totamt;





                dblSubTotalLG += LGAMOUNT;
                dblSubTotalLGComma = SpellAmount.comma(dblSubTotalLG);

                dblSubTotalLC += LCAMOUNT;
                dblSubTotalLCComma = SpellAmount.comma(dblSubTotalLC);

                dblSubTotalTOT += TOTAMOUNT;
                dblSubTotalTOTComma = SpellAmount.comma(dblSubTotalTOT);



                dblGrandTotalLGAmount += LGAMOUNT;
                dblGrandTotalLGComma = SpellAmount.comma(dblGrandTotalLGAmount);

                dblGrandTotalLCAmount += LCAMOUNT;
                dblGrandTotalLCComma = SpellAmount.comma(dblGrandTotalLCAmount);

                dblGrandTotalTOTAmount += TOTAMOUNT;
                dblGrandTotalTOTComma = SpellAmount.comma(dblGrandTotalTOTAmount);



            }
            else if (e.Row.RowType == DataControlRowType.Footer)
            {
                LoadSerial();
                e.Row.Cells[1].Text = "Total :   ";
                e.Row.Cells[1].HorizontalAlign = HorizontalAlign.Right;
                e.Row.Cells[2].Text = dblGrandTotalLGComma;
                e.Row.Cells[2].HorizontalAlign = HorizontalAlign.Right;
                e.Row.Cells[3].Text = dblGrandTotalLCComma;
                e.Row.Cells[3].HorizontalAlign = HorizontalAlign.Right;
                e.Row.Cells[4].Text = dblGrandTotalTOTComma;
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