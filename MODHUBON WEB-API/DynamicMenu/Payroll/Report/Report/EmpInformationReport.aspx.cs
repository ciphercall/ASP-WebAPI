using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data;
using System.Configuration;
using System.Data.SqlClient;
using System.Web.Services;
using DynamicMenu;

namespace AlchemyAccounting.Info.Report
{
    public partial class EmpInformationReport : System.Web.UI.Page
    {
        IFormatProvider dateformat = new System.Globalization.CultureInfo("fr-FR", true);
        int intSubTotalIndex = 1;
        string strPreviousRowID = string.Empty;
        SqlConnection conn = new SqlConnection(dbFunctions.connection);
        protected void Page_Load(object sender, EventArgs e)
        {
            gridShow();
        }
        private void gridShow()
        {
            conn.Open();
            SqlCommand cmd = new SqlCommand(@"SELECT     'Name :'+HR_EMP.EMPNM +'  |  '+'Gender :'+HR_EMP.GENDER +'  |  '+'Department : '+HR_DEPT.DEPTNM +'  |  '+'Joining Date :'+CONVERT(NVARCHAR(10),
                      HR_EMP.JOININGDT,103) +'  | '+'Type : '+HR_EMP.EMPTP AS EmployeeInfo, HR_POST.POSTNM,HR_EMPSALARY.SALSTATUS ,
                      HR_EMPSALARY.BASICSAL, 
                      HR_EMPSALARY.HOUSERENT,HR_EMPSALARY.MEDICAL,HR_EMPSALARY.PFRATE,CONVERT(NVARCHAR(10),HR_EMPSALARY.PFEFDT,103) AS PFEFDT, CONVERT(NVARCHAR(10),HR_EMPSALARY.JOBEFDT,103) AS JOBEFDT, 
                      CONVERT(NVARCHAR(10),HR_EMPSALARY.JOBETDT,103) AS JOBETDT                   
                      FROM   HR_EMP INNER JOIN
                      HR_EMPSALARY ON HR_EMP.EMPID = HR_EMPSALARY.EMPID INNER JOIN
                      HR_DEPT ON HR_EMP.DEPTID = HR_DEPT.DEPTID INNER JOIN
                      HR_POST ON HR_EMPSALARY.POSTID = HR_POST.POSTID", conn);
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
                strPreviousRowID = DataBinder.Eval(e.Row.DataItem, "EmployeeInfo").ToString();

                string POSTNM = DataBinder.Eval(e.Row.DataItem, "POSTNM").ToString();
                e.Row.Cells[1].Text = "&nbsp;" + POSTNM;

                string SALSTATUS = DataBinder.Eval(e.Row.DataItem, "SALSTATUS").ToString();
                e.Row.Cells[2].Text = "&nbsp;" + SALSTATUS;

                string BASICSAL = DataBinder.Eval(e.Row.DataItem, "BASICSAL").ToString();
                e.Row.Cells[3].Text = "&nbsp;" + BASICSAL;

                string HOUSERENT = DataBinder.Eval(e.Row.DataItem, "HOUSERENT").ToString();
                e.Row.Cells[4].Text = "&nbsp;" + HOUSERENT;

                string MEDICAL = DataBinder.Eval(e.Row.DataItem, "MEDICAL").ToString();
                e.Row.Cells[5].Text = "&nbsp;" + MEDICAL;

                string PFRATE = DataBinder.Eval(e.Row.DataItem, "PFRATE").ToString();
                e.Row.Cells[6].Text = "&nbsp;" + PFRATE;

                string PFEFDT = DataBinder.Eval(e.Row.DataItem, "PFEFDT").ToString();
                e.Row.Cells[7].Text = "&nbsp;" + PFEFDT;

                string JOBEFDT = DataBinder.Eval(e.Row.DataItem, "JOBEFDT").ToString();
                e.Row.Cells[8].Text = "&nbsp;" + JOBEFDT;

                string JOBETDT = DataBinder.Eval(e.Row.DataItem, "JOBETDT").ToString();
                e.Row.Cells[9].Text = "&nbsp;" + JOBETDT;
            }
        }

        protected void GridView1_RowCreated(object sender, GridViewRowEventArgs e)
        {
            {

                bool IsSubTotalRowNeedToAdd = false;
                bool IsGrandTotalRowNeedtoAdd = false;
                if ((strPreviousRowID != string.Empty) && (DataBinder.Eval(e.Row.DataItem, "EmployeeInfo") != null))
                    if (strPreviousRowID != DataBinder.Eval(e.Row.DataItem, "EmployeeInfo").ToString())
                        IsSubTotalRowNeedToAdd = true;
                if ((strPreviousRowID != string.Empty) && (DataBinder.Eval(e.Row.DataItem, "EmployeeInfo") == null))
                {
                    IsSubTotalRowNeedToAdd = true;
                    IsGrandTotalRowNeedtoAdd = true;
                    intSubTotalIndex = 0;
                }
                #region Inserting first Row and populating fist Group Header details
                if ((strPreviousRowID == string.Empty) && (DataBinder.Eval(e.Row.DataItem, "EmployeeInfo") != null))
                {
                    GridView GridView1 = (GridView)sender;
                    GridViewRow row = new GridViewRow(0, 0, DataControlRowType.DataRow, DataControlRowState.Insert);
                    TableCell cell = new TableCell();
                    cell.Text = " " + DataBinder.Eval(e.Row.DataItem, "EmployeeInfo").ToString();
                    cell.Font.Bold = true;
                    cell.ColumnSpan = 10;
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
                    if (DataBinder.Eval(e.Row.DataItem, "EmployeeInfo") != null)
                    {
                        row = new GridViewRow(0, 0, DataControlRowType.DataRow, DataControlRowState.Insert);
                        cell = new TableCell();

                        cell.Text = " " + DataBinder.Eval(e.Row.DataItem, "EmployeeInfo").ToString();
                        cell.ColumnSpan =10;
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

        protected void GridView1_SelectedIndexChanged(object sender, EventArgs e)
        {

        }

    }
}