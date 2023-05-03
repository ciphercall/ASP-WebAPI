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
    public partial class rptAttendanceOtPeriodic : System.Web.UI.Page
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
                lblDate.Text =dbFunctions.Timezone(DateTime.Now).ToString("dd-MMM-yyyy hh:mm tt");
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
            string branchID = Session["BranchId"].ToString();
            string branchnm = Session["BranchName"].ToString();
            
            var braquery = " ";
            if (branchnm == "ALL")
                braquery = " ";
            else
                braquery = $" AND D.BRANCHCD ='{branchID}' ";

            DateTime dateFr = DateTime.Parse(frdate, dateformat, System.Globalization.DateTimeStyles.AssumeLocal);
            string frdt = dateFr.ToString("yyyy-MM-dd");
            DateTime dateTo = DateTime.Parse(todate, dateformat, System.Globalization.DateTimeStyles.AssumeLocal);
            string todt = dateTo.ToString("yyyy-MM-dd");

            lblFrDT.Text = frdate;
            lblTodate.Text = todate;
            lblBranchName.Text = branchnm;


            conn.Open();
            SqlCommand cmd = new SqlCommand($@"SELECT A.EMPID, EMPNM, CONVERT(NVARCHAR,TRANSDT,103) TRANSDT, SHIFTNM, BRANCHNM, CONVERT(varchar(15),CAST(TIMEFR AS TIME),100) TIMEFR, 
            CONVERT(varchar(15),CAST(TIMETO AS TIME),100) TIMETO, CONVERT(varchar(15),CAST(TIMEIN AS TIME),100) TIMEIN, 
            CONVERT(varchar(15),CAST(TIMEOUT AS TIME),100) TIMEOUT, --CAST(OTDAY AS INT) OTDAY, 
            SUBSTRING(CONVERT(varchar(15),OTHOUR),1,5) + ' MINUTES' OTHOUR
            FROM HR_ATREG AS A INNER JOIN HR_EMP B ON A.EMPID = B.EMPID
            INNER JOIN HR_SHIFT AS C ON A.SHIFTID = C.SHIFTID
            INNER JOIN ASL_BRANCH AS D ON B.COSTPID = D.BRANCHCD
            WHERE TRANSDT BETWEEN '{frdt}' AND '{todt}' {braquery} ORDER BY A.TRANSDT,EMPNM", conn);
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
               
                strPreviousRowID = DataBinder.Eval(e.Row.DataItem,"TRANSDT").ToString();
                

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
                e.Row.Cells[8].Text = "&nbsp;" + OTHOUR;
            }
        }
        protected void GridView1_RowCreated(object sender, GridViewRowEventArgs e)
        {
            {

                bool IsSubTotalRowNeedToAdd = false;
                bool IsGrandTotalRowNeedtoAdd = false;
                if ((strPreviousRowID != string.Empty) && (DataBinder.Eval(e.Row.DataItem, "TRANSDT") != null))
                    if (strPreviousRowID != DataBinder.Eval(e.Row.DataItem, "TRANSDT").ToString())
                        IsSubTotalRowNeedToAdd = true;
                if ((strPreviousRowID != string.Empty) && (DataBinder.Eval(e.Row.DataItem, "TRANSDT") == null))
                {
                    IsSubTotalRowNeedToAdd = true;
                    IsGrandTotalRowNeedtoAdd = true;
                    intSubTotalIndex = 0;
                }
                #region Inserting first Row and populating fist Group Header details
                if ((strPreviousRowID == string.Empty) && (DataBinder.Eval(e.Row.DataItem, "TRANSDT") != null))
                {
                    GridView GridView1 = (GridView)sender;
                    GridViewRow row = new GridViewRow(0, 0, DataControlRowType.DataRow, DataControlRowState.Insert);
                    TableCell cell = new TableCell();
                    cell.Text = " " + DataBinder.Eval(e.Row.DataItem, "TRANSDT").ToString();
                    cell.Font.Bold = true;
                    cell.ColumnSpan = 9;
                    cell.CssClass = "GroupHeaderStyle";
                    row.Cells.Add(cell);
                    GridView1.Controls[0].Controls.AddAt(e.Row.RowIndex + intSubTotalIndex, row);
                    intSubTotalIndex++;
                }
                #endregion
                if (IsSubTotalRowNeedToAdd)
                {
                    #region Adding Sub Total Row
                    GridView GridView1 = (GridView)sender;
                    // Creating a Row          
                    GridViewRow row = new GridViewRow(0, 0, DataControlRowType.DataRow, DataControlRowState.Insert);

                    //Adding Total Cell          
                    TableCell cell = new TableCell();

                    //Adding the Row at the RowIndex position in the Grid      
                    GridView1.Controls[0].Controls.AddAt(e.Row.RowIndex + intSubTotalIndex, row);
                    intSubTotalIndex++;
                    #endregion
                    #region Adding Next Group Header Details
                    if (DataBinder.Eval(e.Row.DataItem, "TRANSDT") != null)
                    {
                        row = new GridViewRow(0, 0, DataControlRowType.DataRow, DataControlRowState.Insert);
                        cell = new TableCell();

                        cell.Text = " " + DataBinder.Eval(e.Row.DataItem, "TRANSDT").ToString();
                        cell.ColumnSpan = 9;
                        cell.Font.Bold = true;
                        cell.CssClass = "GroupHeaderStyle";
                        row.Cells.Add(cell);
                        GridView1.Controls[0].Controls.AddAt(e.Row.RowIndex + intSubTotalIndex, row);
                        intSubTotalIndex++;
                    }
                    #endregion

                }
                if (IsGrandTotalRowNeedtoAdd)
                {

                }


            }
        }
    }
}