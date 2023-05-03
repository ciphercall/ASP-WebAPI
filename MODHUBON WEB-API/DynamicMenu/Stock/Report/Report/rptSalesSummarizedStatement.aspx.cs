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
    public partial class rptSalesSummarizedStatement : System.Web.UI.Page
    {
        IFormatProvider dateformat = new System.Globalization.CultureInfo("fr-FR", true);

        
        decimal totalRETAMT_PS = 0;
        decimal totalRETAMT_BP = 0;
        decimal totalRETAMT_GP = 0;
        decimal totalSALAMT_P = 0;
        decimal totalNETAMT = 0;


        string totalRETAMT_PSString = "";
        string totalRETAMT_BPString = "";
        string totalRETAMT_GPString = "";
        string totalSALAMT_PString = "";
        string totalNETAMTString = "";
        protected void Page_Load(object sender, EventArgs e)
        {

            if (!IsPostBack)
                try
                {
                    dbFunctions.lblAdd(@"SELECT COMPNM FROM ASL_COMPANY WHERE COMPID ='101'", lblCompNM);
                    dbFunctions.lblAdd(@"SELECT ADDRESS FROM ASL_COMPANY WHERE COMPID ='101'", lblAddress);

                    DateTime PrintDate = dbFunctions.Timezone(DateTime.Now);
                    string td = PrintDate.ToString("dd-MMM-yyyy hh:mm tt");
                    lblTime.Text = td;

                    ShowGrid();
                }
                catch (Exception x)
                {
                    //ignore
                }

        }

        public void ShowGrid()
        {
            SqlConnection conn = new SqlConnection(dbFunctions.connection);

            string dateFrom = Session["From"].ToString();
            DateTime datefrom = DateTime.Parse(dateFrom, dateformat, System.Globalization.DateTimeStyles.AssumeLocal);
            string from = datefrom.ToString("yyyy/MM/dd");

            string dateTo = Session["To"].ToString();
            DateTime datato = DateTime.Parse(dateTo, dateformat, System.Globalization.DateTimeStyles.AssumeLocal);
            string to = datato.ToString("yyyy/MM/dd");


            lblDateFrom.Text = datefrom.ToString("dd-MM-yyyy");
            lblDateTo.Text = datato.ToString("dd-MM-yyyy");

            conn.Open();
            SqlCommand cmd = new SqlCommand($@"SELECT PARTYNM, SUM(ISNULL(SALAMT,0)) SALAMT_P, SUM(ISNULL(RETAMT_G,0)) RETAMT_GP, SUM(ISNULL(RETAMT_B,0)) RETAMT_BP, SUM(ISNULL(RETAMT_PS,0)) RETAMT_PS, 
(SUM(ISNULL(SALAMT,0)) - SUM(ISNULL(RETAMT_G,0)) + SUM(ISNULL(RETAMT_B,0)) - SUM(ISNULL(RETAMT_PS,0))) NETAMT FROM(
SELECT A.PSID, SUM(ISNULL(NETAMT,0)) SALAMT, 0 RETAMT_G, 0 RETAMT_B, 0 RETAMT_PS
FROM STK_TRANS A, STK_PARTY B
WHERE A.PSID = B.PARTYID AND A.TRANSTP = 'SALE' AND ISNULL(STATUS,'I')='A' AND TRANSDT BETWEEN '{from}' AND '{to}'
AND   SUBSTRING(ITEMID,1,3) NOT IN ('10110','10111') GROUP BY A.PSID
UNION SELECT A.PSID, 0 SALAMT, SUM(ISNULL(NETAMT,0)) RETAMT_G, 0 RETAMT_B, 0 RETAMT_PS
FROM STK_TRANS A, STK_PARTY B
WHERE A.PSID = B.PARTYID AND A.TRANSTP = 'IRTS' AND ISNULL(STATUS,'I')='A' AND TRANSDT BETWEEN '{from}' AND '{to}'
AND   SUBSTRING(ITEMID,1,3) NOT IN ('10110','10111') AND ISNULL(RETTP,'GOOD')='GOOD' GROUP BY A.PSID
UNION SELECT A.PSID, 0 SALAMT, 0 RETAMT_G, SUM(ISNULL(NETAMT,0)) RETAMT_B, 0 RETAMT_PS
FROM STK_TRANS A, STK_PARTY B
WHERE A.PSID = B.PARTYID AND A.TRANSTP = 'IRTS' AND ISNULL(STATUS,'I')='A' AND TRANSDT BETWEEN '{from}' AND '{to}'
AND   SUBSTRING(ITEMID,1,3) NOT IN ('10110','10111') AND ISNULL(RETTP,'GOOD')='BAD' GROUP BY A.PSID
UNION SELECT A.PSID, 0 SALAMT, 0 RETAMT_G, 0 RETAMT_B, SUM(ISNULL(NETAMT,0)) RETAMT_PS
FROM STK_TRANS A, STK_PARTY B
WHERE A.PSID = B.PARTYID AND ISNULL(STATUS,'I')='A' AND TRANSDT BETWEEN '{from}' AND '{to}'
AND   SUBSTRING(ITEMID,1,3) IN ('10111') GROUP BY A.PSID
) A INNER JOIN STK_PARTY B ON A.PSID = B.PARTYID 
WHERE PSID IS NOT NULL GROUP BY PARTYNM", conn);
            cmd.CommandTimeout = 0;
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


        protected void GridView1_OnRowDataBound(object sender, GridViewRowEventArgs e)
        {

            if (e.Row.RowType == DataControlRowType.DataRow)
            {
                string PARTYNM = DataBinder.Eval(e.Row.DataItem, "PARTYNM").ToString();
                e.Row.Cells[0].Text = "&nbsp;" + PARTYNM;


                decimal SALAMT_PP = Convert.ToDecimal(DataBinder.Eval(e.Row.DataItem, "SALAMT_P").ToString());
                string SALAMT_P = SpellAmount.comma(SALAMT_PP);
                e.Row.Cells[1].Text = SALAMT_P;

                decimal RETAMT_GPP = Convert.ToDecimal(DataBinder.Eval(e.Row.DataItem, "RETAMT_GP").ToString());
                string RETAMT_GP = SpellAmount.comma(RETAMT_GPP);
                e.Row.Cells[2].Text = RETAMT_GP;


                decimal RETAMT_BPP = Convert.ToDecimal(DataBinder.Eval(e.Row.DataItem, "RETAMT_BP").ToString());
                string RETAMT_BP = SpellAmount.comma(RETAMT_BPP);
                e.Row.Cells[3].Text = RETAMT_BP;



                decimal RETAMT_PSS = Convert.ToDecimal(DataBinder.Eval(e.Row.DataItem, "RETAMT_PS").ToString());
                string RETAMT_PS = SpellAmount.comma(RETAMT_PSS);
                e.Row.Cells[4].Text = RETAMT_PS;

                decimal NETAMTT = Convert.ToDecimal(DataBinder.Eval(e.Row.DataItem, "NETAMT").ToString());
                string NETAMT = SpellAmount.comma(NETAMTT);
                e.Row.Cells[5].Text = NETAMT;



                totalSALAMT_P += SALAMT_PP;
                totalSALAMT_PString = SpellAmount.comma(totalSALAMT_P);


                totalRETAMT_GP += RETAMT_GPP;
                totalRETAMT_GPString = SpellAmount.comma(totalRETAMT_GP);

                totalRETAMT_BP += RETAMT_BPP;
                totalRETAMT_BPString = SpellAmount.comma(totalRETAMT_BP);

                totalRETAMT_PS += RETAMT_PSS;
                totalRETAMT_PSString = SpellAmount.comma(totalRETAMT_PS);

                totalNETAMT += NETAMTT;
                totalNETAMTString = SpellAmount.comma(totalNETAMT);

            }

            else if (e.Row.RowType == DataControlRowType.Footer)
            {
                e.Row.Cells[0].Text = "TOTAL :";
                e.Row.Cells[0].HorizontalAlign = HorizontalAlign.Right;
                //e.Row.Cells[1].Text = Tot_CartonQtyComma;
                //e.Row.Cells[1].HorizontalAlign = HorizontalAlign.Right;
                e.Row.Cells[1].Text = totalSALAMT_PString;
                e.Row.Cells[1].HorizontalAlign = HorizontalAlign.Right;
                e.Row.Cells[2].Text = totalRETAMT_GPString;
                e.Row.Cells[2].HorizontalAlign = HorizontalAlign.Right;
                e.Row.Cells[3].Text = totalRETAMT_BPString;
                e.Row.Cells[3].HorizontalAlign = HorizontalAlign.Right;
                e.Row.Cells[4].Text = totalRETAMT_PSString;
                e.Row.Cells[4].HorizontalAlign = HorizontalAlign.Right;
                e.Row.Cells[5].Text = totalNETAMTString;
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
    }
}