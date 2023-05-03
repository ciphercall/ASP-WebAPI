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
    public partial class HolidayInformationPrint : System.Web.UI.Page
    {
        IFormatProvider dateformat = new System.Globalization.CultureInfo("fr-FR", true);
        int intSubTotalIndex = 1;
        string strPreviousRowID = string.Empty;
        SqlConnection conn = new SqlConnection(dbFunctions.connection);
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                try
                {
                    lblFrDT.Text = Session["FrDT"].ToString();
                    lblToDT.Text = Session["ToDT"].ToString();
                    gridShow();
                }
                catch (Exception)
                {
                    //ignore
                }
            }
        }
        private void gridShow()
        {
            DateTime dateFR = DateTime.Parse(lblFrDT.Text, dateformat, System.Globalization.DateTimeStyles.AssumeLocal);
            DateTime dateTO = DateTime.Parse(lblToDT.Text, dateformat, System.Globalization.DateTimeStyles.AssumeLocal);

            string FRDT = dateFR.ToString("yyyy-MM-dd");
            string TODT = dateTO.ToString("yyyy-MM-dd");
            conn.Open();
            SqlCommand cmd = new SqlCommand(@"SELECT     CONVERT(NVARCHAR(10),HR_HOLIDAYS.TRANSDT,103) AS TRANSDT, HR_HDAYTP.HDAYNM, HR_HOLIDAYS.STATUS, HR_HOLIDAYS.REMARKS
                                              FROM  HR_HDAYTP INNER JOIN
                                              HR_HOLIDAYS ON HR_HDAYTP.HDAYID = HR_HOLIDAYS.HDAYID WHERE HR_HOLIDAYS.TRANSDT  >= '" + FRDT + "' AND HR_HOLIDAYS.TRANSDT  <= '" + TODT + "'", conn);
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
                string POSTNM = DataBinder.Eval(e.Row.DataItem, "TRANSDT").ToString();
                e.Row.Cells[0].Text = "&nbsp;" + POSTNM;

                string SALSTATUS = DataBinder.Eval(e.Row.DataItem, "HDAYNM").ToString();
                e.Row.Cells[1].Text = "&nbsp;" + SALSTATUS;

                string BASICSAL = DataBinder.Eval(e.Row.DataItem, "STATUS").ToString();
                e.Row.Cells[2].Text = "&nbsp;" + BASICSAL;

                string HOUSERENT = DataBinder.Eval(e.Row.DataItem, "REMARKS").ToString();
                e.Row.Cells[3].Text = "&nbsp;" + HOUSERENT;
            }
        }
    }
}