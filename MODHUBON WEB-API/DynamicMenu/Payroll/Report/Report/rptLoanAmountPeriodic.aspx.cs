using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using AlchemyAccounting;

namespace DynamicMenu.Payroll.Report.Report
{
    public partial class rptLoanAmountPeriodic : System.Web.UI.Page
    {
        private IFormatProvider dateformat = new System.Globalization.CultureInfo("fr-FR", true);
        private int intSubTotalIndex = 1;
        private string strPreviousRowID = string.Empty;
        private SqlConnection conn = new SqlConnection(dbFunctions.connection);

        private decimal TOTamount = 0;
        private string TOTamountcomma = "0";

        private decimal LOANamount = 0;
        private string LOANamountcomma = "0";

        private decimal NETamount = 0;
        private string NETamountcomma = "0";
        protected void Page_Load(object sender, EventArgs e)
        {
            try
            {
                dbFunctions.lblAdd(@"SELECT COMPNM FROM ASL_COMPANY WHERE COMPID ='101'", lblCompanyName);
                dbFunctions.lblAdd(@"SELECT ADDRESS FROM ASL_COMPANY WHERE COMPID ='101'", lblAddress);

                lblDate.Text = dbFunctions.Timezone(DateTime.Now).ToString("dd-MMM-yyyy hh:mm tt");
                gridShow();
            }
            catch (Exception)
            {
                //ignore
            }
        }

        private void gridShow()
        {
            string frdate = Session["Fdate"].ToString();
            string todate = Session["Tdate"].ToString();

            DateTime transdateF = DateTime.Parse(frdate, dateformat, System.Globalization.DateTimeStyles.AssumeLocal);
            string frdt = transdateF.ToString("yyyy-MM-dd");

            DateTime transdateT = DateTime.Parse(todate, dateformat, System.Globalization.DateTimeStyles.AssumeLocal);
            string todt = transdateT.ToString("yyyy-MM-dd");
            string varYear2 = transdateF.ToString("yyyy");
            string mon2 = transdateF.ToString("MM").ToUpper();
            string dateFrom = varYear2 + "-" + mon2 + "-28";
            lblFrDT.Text = frdate;
            lblTodate.Text = todate;
            string varYear = transdateT.ToString("yyyy");
            string mon = transdateT.ToString("MM").ToUpper();
            string dateTo = varYear + "-" + mon + "-28";

            conn.Open();
            SqlCommand cmd =
                new SqlCommand($@"SELECT E.EMPNM,B.LOANAMT,ISNULL(BONUSF,0)+ISNULL(INCENTIVE,0)+ISNULL(CONVEY,0)+ISNULL(MOBILE,0)+ISNULL(DUEADJ,0)+ISNULL(ADVANCE,0)+ISNULL(FOODING,0)+ISNULL(FINEADJ,0)  NETAMT,
                            ISNULL(BONUSF,0)+ISNULL(INCENTIVE,0)+ISNULL(CONVEY,0)+ISNULL(MOBILE,0)+ISNULL(DUEADJ,0)+ISNULL(ADVANCE,0)+ISNULL(FOODING,0)+ISNULL(FINEADJ,0) -ISNULL(B.LOANAMT,0) TOTAMT
                            FROM HR_SALDRCR A LEFT OUTER JOIN HR_EMPLOAN B ON A.EMPID=B.EMPID
                            INNER JOIN HR_EMP E ON B.EMPID=E.EMPID WHERE B.TRANSDT BETWEEN '{dateFrom}' AND '{dateTo}'", conn);
            SqlDataAdapter da = new SqlDataAdapter(cmd);
            DataSet ds = new DataSet();
            da.Fill(ds);
            conn.Close();
            if (ds.Tables[0].Rows.Count > 0)
            {
                GridView1.DataSource = ds;
                GridView1.DataBind();
            }
            else
            {
                ds.Tables[0].Rows.Add(ds.Tables[0].NewRow());
                GridView1.DataSource = ds;
                GridView1.DataBind();
                int columncount = GridView1.Rows[0].Cells.Count;
                GridView1.Rows[0].Cells.Clear();
                GridView1.Rows[0].Cells.Add(new TableCell());
                GridView1.Rows[0].Cells[0].ColumnSpan = columncount;
                GridView1.Rows[0].Visible = false;
            }
        }

        protected void GridView1_RowDataBound(object sender, GridViewRowEventArgs e)
        {
            if (e.Row.RowType == DataControlRowType.DataRow)
            {
                //strPreviousRowID = DataBinder.Eval(e.Row.DataItem, "EMPNM").ToString();
                
                string EMPNM = DataBinder.Eval(e.Row.DataItem, "EMPNM").ToString();
                e.Row.Cells[0].Text = "&nbsp;" + EMPNM;

                decimal LOANAMT = Convert.ToDecimal(DataBinder.Eval(e.Row.DataItem, "LOANAMT").ToString());
                string LOANAMTSTRING = SpellAmount.comma(LOANAMT);
                e.Row.Cells[1].Text = LOANAMTSTRING;

                decimal NETAMT = Convert.ToDecimal(DataBinder.Eval(e.Row.DataItem, "NETAMT").ToString());
                string NETAMTSTRING = SpellAmount.comma(NETAMT);
                e.Row.Cells[2].Text = NETAMTSTRING;

                decimal TOTAMT = Convert.ToDecimal(DataBinder.Eval(e.Row.DataItem, "TOTAMT").ToString());
                string TOTAMTSTRING = SpellAmount.comma(TOTAMT);
                e.Row.Cells[3].Text = TOTAMTSTRING;



                TOTamount += TOTAMT;
                TOTamountcomma = SpellAmount.comma(TOTamount);

                LOANamount += LOANAMT;
                LOANamountcomma = SpellAmount.comma(LOANamount);

                NETamount += NETAMT;
                NETamountcomma = SpellAmount.comma(NETamount);

            }
            else if (e.Row.RowType == DataControlRowType.Footer)
            {
                e.Row.Cells[0].Text = "Total";
                e.Row.Cells[0].HorizontalAlign = HorizontalAlign.Right;
                e.Row.Cells[3].Text = TOTamountcomma;
                e.Row.Cells[3].HorizontalAlign = HorizontalAlign.Right;
                e.Row.Cells[1].Text = LOANamountcomma;
                e.Row.Cells[1].HorizontalAlign = HorizontalAlign.Right;
                e.Row.Cells[2].Text = NETamountcomma;
                e.Row.Cells[2].HorizontalAlign = HorizontalAlign.Right;


            }
        }
    }
}