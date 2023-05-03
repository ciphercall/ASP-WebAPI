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
    public partial class yearlyLeaveReportPrint : System.Web.UI.Page
    {
        SqlConnection conn = new SqlConnection(dbFunctions.connection);
        protected void Page_Load(object sender, EventArgs e)
        {
            gridShow();
        }
        private void gridShow()
        {
            lblYR.Text = Session["YEAR"].ToString();
            conn.Open();
            SqlCommand cmd = new SqlCommand(@"SELECT     HR_LEAVE.LEAVENM, CONVERT(nvarchar(10),HR_LEAVEYY.LEAVEDD,103) AS LEAVEDD, HR_LEAVEYY.REMARKS
            FROM HR_LEAVEYY INNER JOIN
              HR_LEAVE ON HR_LEAVEYY.LEAVEID = HR_LEAVE.LEAVEID WHERE HR_LEAVEYY.LEAVEYY='" + lblYR.Text + "'", conn);
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
                string LEAVENM = DataBinder.Eval(e.Row.DataItem, "LEAVENM").ToString();
                e.Row.Cells[0].Text = "&nbsp;" + LEAVENM;

                string LEAVEDD = DataBinder.Eval(e.Row.DataItem, "LEAVEDD").ToString();
                e.Row.Cells[1].Text = "&nbsp;" + LEAVEDD;

                string REMARKS = DataBinder.Eval(e.Row.DataItem, "REMARKS").ToString();
                e.Row.Cells[2].Text = "&nbsp;" + REMARKS;
            }
        }

    }
}