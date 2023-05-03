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
    public partial class EmployeeReport : System.Web.UI.Page
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
            SqlCommand cmd = new SqlCommand(@"SELECT  HR_EMP.EMPID, HR_EMP.EMPNM, HR_EMP.GENDER, HR_EMP.CONTACTNO,CONVERT(NVARCHAR(10),HR_EMP.JOININGDT,103) AS JOININGDT, HR_EMP.EMPTP, HR_DEPT.DEPTNM
                                            FROM  HR_EMP INNER JOIN
                                             HR_DEPT ON HR_EMP.DEPTID = HR_DEPT.DEPTID", conn);
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
                string POSTNM = DataBinder.Eval(e.Row.DataItem, "EMPID").ToString();
                e.Row.Cells[0].Text = "&nbsp;" + POSTNM;

                string SALSTATUS = DataBinder.Eval(e.Row.DataItem, "EMPNM").ToString();
                e.Row.Cells[1].Text = "&nbsp;" + SALSTATUS;

                string BASICSAL = DataBinder.Eval(e.Row.DataItem, "GENDER").ToString();
                e.Row.Cells[2].Text = "&nbsp;" + BASICSAL;

                string HOUSERENT = DataBinder.Eval(e.Row.DataItem, "CONTACTNO").ToString();
                e.Row.Cells[3].Text = "&nbsp;" + HOUSERENT;

                string MEDICAL = DataBinder.Eval(e.Row.DataItem, "JOININGDT").ToString();
                e.Row.Cells[4].Text = "&nbsp;" + MEDICAL;

                string PFRATE = DataBinder.Eval(e.Row.DataItem, "EMPTP").ToString();
                e.Row.Cells[5].Text = "&nbsp;" + PFRATE;

                string PFEFDT = DataBinder.Eval(e.Row.DataItem, "DEPTNM").ToString();
                e.Row.Cells[6].Text = "&nbsp;" + PFEFDT;
            }
        }
    }
}