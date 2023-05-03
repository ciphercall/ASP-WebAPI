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
    public partial class rptLeaveApplicationPeriodic : System.Web.UI.Page
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
            string employyeName = Session["EmployyeID"].ToString();
           // string employyeID = Session["EmployyeID"].ToString();

            //var empquery = " ";
            //if (employyeName != "")
            //    empquery = $" AND A.EMPID ='{employyeId}' ";
            //else empquery = " ";

            DateTime dateFr = DateTime.Parse(frdate, dateformat, System.Globalization.DateTimeStyles.AssumeLocal);
            string frdt = dateFr.ToString("yyyy-MM-dd");
            DateTime dateTo = DateTime.Parse(todate, dateformat, System.Globalization.DateTimeStyles.AssumeLocal);
            string todt = dateTo.ToString("yyyy-MM-dd");

            lblFrDT.Text = frdate;
            lblTodate.Text = todate;
            lblEmployeeName.Text = employyeName;

            var where = "";
            if (employyeName=="ALL")
            {
                where = " ";
            }
            else
            {
                where = " AND L.EMPID="+employyeName+"";
            }

            conn.Open();
            SqlCommand cmd = new SqlCommand(@"SELECT E.EMPNM,CONVERT(NVARCHAR,L.TRANSDT,103) TRANSDT,L.TRANSMY,L.TRANSNO,LEV.LEAVENM,CONVERT(NVARCHAR,L.LEAVEFR,103) LEAVEFR,CONVERT(NVARCHAR,L.LEAVETO,103) LEAVETO,L.LEAVEDAYS,L.REASON FROM HR_LAPPS L INNER JOIN HR_EMP E ON L.EMPID=E.EMPID
                                            INNER JOIN HR_LEAVE LEV ON L.LEAVEID=LEV.LEAVEID
                                            WHERE TRANSDT BETWEEN '" + frdt+"' AND '"+todt+"' "+where+" ORDER BY E.EMPNM", conn);
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
                strPreviousRowID = DataBinder.Eval(e.Row.DataItem, "EMPNM").ToString();


                string TRANSDT = DataBinder.Eval(e.Row.DataItem, "TRANSDT").ToString();
                e.Row.Cells[0].Text = "&nbsp;" + TRANSDT;

                string SHIFTNM = DataBinder.Eval(e.Row.DataItem, "TRANSMY").ToString();
                e.Row.Cells[1].Text = "&nbsp;" + SHIFTNM;

                string BRANCHNM = DataBinder.Eval(e.Row.DataItem, "LEAVENM").ToString();
                e.Row.Cells[2].Text = "&nbsp;" + BRANCHNM;

                string TIMEFR = DataBinder.Eval(e.Row.DataItem, "LEAVEFR").ToString();
                e.Row.Cells[3].Text = "&nbsp;" + TIMEFR;

                string TIMETO = DataBinder.Eval(e.Row.DataItem, "LEAVETO").ToString();
                e.Row.Cells[4].Text = "&nbsp;" + TIMETO;

                string TIMEIN = DataBinder.Eval(e.Row.DataItem, "LEAVEDAYS").ToString();
                e.Row.Cells[5].Text = "&nbsp;" + TIMEIN;

                string TIMEOUT = DataBinder.Eval(e.Row.DataItem, "REASON").ToString();
                e.Row.Cells[6].Text = "&nbsp;" + TIMEOUT;
            }
        }
        protected void GridView1_RowCreated(object sender, GridViewRowEventArgs e)
        {
            {

                bool IsSubTotalRowNeedToAdd = false;
                bool IsGrandTotalRowNeedtoAdd = false;
                if ((strPreviousRowID != string.Empty) && (DataBinder.Eval(e.Row.DataItem, "EMPNM") != null))
                    if (strPreviousRowID != DataBinder.Eval(e.Row.DataItem, "EMPNM").ToString())
                        IsSubTotalRowNeedToAdd = true;
                if ((strPreviousRowID != string.Empty) && (DataBinder.Eval(e.Row.DataItem, "EMPNM") == null))
                {
                    IsSubTotalRowNeedToAdd = true;
                    IsGrandTotalRowNeedtoAdd = true;
                    intSubTotalIndex = 0;
                }
                #region Inserting first Row and populating fist Group Header details
                if ((strPreviousRowID == string.Empty) && (DataBinder.Eval(e.Row.DataItem, "EMPNM") != null))
                {
                    GridView GridView1 = (GridView)sender;
                    GridViewRow row = new GridViewRow(0, 0, DataControlRowType.DataRow, DataControlRowState.Insert);
                    TableCell cell = new TableCell();
                    cell.Text = " " + DataBinder.Eval(e.Row.DataItem, "EMPNM").ToString();
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
                    if (DataBinder.Eval(e.Row.DataItem, "EMPNM") != null)
                    {
                        row = new GridViewRow(0, 0, DataControlRowType.DataRow, DataControlRowState.Insert);
                        cell = new TableCell();

                        cell.Text = " " + DataBinder.Eval(e.Row.DataItem, "EMPNM").ToString();
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