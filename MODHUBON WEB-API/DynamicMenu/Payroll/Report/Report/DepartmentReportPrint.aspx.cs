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
    public partial class DepartmentReportPrint : System.Web.UI.Page
    {
        SqlConnection conn = new SqlConnection(dbFunctions.connection);
        protected void Page_Load(object sender, EventArgs e)
        {
            gridShow();
        }
        private void gridShow()
        {

            conn.Open();
            SqlCommand cmd = new SqlCommand(@"SELECT     DEPTID, DEPTNM, REMARKS FROM HR_DEPT", conn);
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
                string DEPTID = DataBinder.Eval(e.Row.DataItem, "DEPTID").ToString();
                e.Row.Cells[0].Text = "&nbsp;" + DEPTID;

                string DEPTNM = DataBinder.Eval(e.Row.DataItem, "DEPTNM").ToString();
                e.Row.Cells[1].Text = "&nbsp;" + DEPTNM;

                string REMARKS = DataBinder.Eval(e.Row.DataItem, "REMARKS").ToString();
                e.Row.Cells[2].Text = "&nbsp;" + REMARKS;
            }
        }
    }
}