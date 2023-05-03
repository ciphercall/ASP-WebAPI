using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Data.SqlClient;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using AlchemyAccounting;

namespace DynamicMenu.Payroll.Report.Report
{
    public partial class rptAttendanceAndOtRegister : System.Web.UI.Page
    {
        IFormatProvider dateformat = new System.Globalization.CultureInfo("fr-FR", true);
        int intSubTotalIndex = 1;
        string strPreviousRowID = string.Empty;
        SqlConnection conn = new SqlConnection(dbFunctions.connection);
        protected void Page_Load(object sender, EventArgs e)
        {
            try
            {
                dbFunctions.lblAdd(@"SELECT COMPNM FROM ASL_COMPANY WHERE COMPID ='101'", lblCompanyName);
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
            string employyeId = Session["EmployyeId"].ToString();
            string employyeName = Session["EmployyeName"].ToString();
            string branchName = Session["BranchName"].ToString();

            DateTime dateFr = DateTime.Parse(frdate, dateformat, System.Globalization.DateTimeStyles.AssumeLocal);
            string frdt = dateFr.ToString("yyyy-MM-dd");
            DateTime dateTo = DateTime.Parse(todate, dateformat, System.Globalization.DateTimeStyles.AssumeLocal);
            string todt = dateTo.ToString("yyyy-MM-dd");

            lblFrDT.Text = frdate;
            lblTodate.Text = todate;
            lblEmployeeName.Text = employyeName;
            lblBranchName.Text = branchName;

            conn.Open();
            SqlCommand cmd = new SqlCommand(@"SELECT A.EMPID, EMPNM, CONVERT(NVARCHAR,TRANSDT,103) TRANSDT, SHIFTNM, BRANCHNM,CONVERT(varchar(15),CAST(TIMEFR AS TIME),100) TIMEFR, 
            CONVERT(varchar(15),CAST(TIMETO AS TIME),100) TIMETO, CONVERT(varchar(15),CAST(TIMEIN AS TIME),100) TIMEIN, 
            CONVERT(varchar(15),CAST(TIMEOUT AS TIME),100) TIMEOUT,
            SUBSTRING(CONVERT(varchar(15),OTHOUR),1,5) + ' MINUTES' OTHOUR
            FROM HR_ATREG AS A INNER JOIN HR_EMP B ON A.EMPID = B.EMPID
            INNER JOIN HR_SHIFT AS C ON A.SHIFTID = C.SHIFTID
            INNER JOIN ASL_BRANCH AS D ON B.COSTPID = D.BRANCHCD
            WHERE A.EMPID = " + employyeId + " AND TRANSDT BETWEEN '" + frdt + "' AND '" + todt + "' ORDER BY TRANSDT", conn);
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

                string EMPNM = DataBinder.Eval(e.Row.DataItem, "EMPNM").ToString();
                e.Row.Cells[0].Text = "&nbsp;" + EMPNM;

                string TRANSDT = DataBinder.Eval(e.Row.DataItem, "TRANSDT").ToString();
                e.Row.Cells[1].Text = "&nbsp;" + TRANSDT;

                string SHIFTNM = DataBinder.Eval(e.Row.DataItem, "SHIFTNM").ToString();
                e.Row.Cells[2].Text = "&nbsp;" + SHIFTNM;             

                string TIMEFR = DataBinder.Eval(e.Row.DataItem, "TIMEFR").ToString();
                e.Row.Cells[3].Text = "&nbsp;" + TIMEFR;

                string TIMETO = DataBinder.Eval(e.Row.DataItem, "TIMETO").ToString();
                e.Row.Cells[4].Text = "&nbsp;" + TIMETO;

                string TIMEIN = DataBinder.Eval(e.Row.DataItem, "TIMEIN").ToString();
                e.Row.Cells[5].Text = "&nbsp;" + TIMEIN;

                string TIMEOUT = DataBinder.Eval(e.Row.DataItem, "TIMEOUT").ToString();
                e.Row.Cells[6].Text = "&nbsp;" + TIMEOUT;

                //string OTDAY = DataBinder.Eval(e.Row.DataItem, "OTDAY").ToString();
                //e.Row.Cells[7].Text = "&nbsp;" + OTDAY;

                string OTHOUR = DataBinder.Eval(e.Row.DataItem, "OTHOUR").ToString();
                e.Row.Cells[7].Text = "&nbsp;" + OTHOUR;
            }
        }
    }
}