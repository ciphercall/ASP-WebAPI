using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data;
using System.Configuration;
using System.Data.Entity;
using System.Data.SqlClient;
using System.Web.Services;
using DynamicMenu;

namespace AlchemyAccounting.Info.Report
{
    public partial class EmployeeBasicInformationPrint : System.Web.UI.Page
    {
        IFormatProvider dateformat = new System.Globalization.CultureInfo("fr-FR", true);
        int intSubTotalIndex = 1;
        string strPreviousRowID = string.Empty;
        SqlConnection conn = new SqlConnection(dbFunctions.connection);
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                dbFunctions.lblAdd(@"SELECT COMPNM FROM ASL_COMPANY WHERE COMPID ='101'", lblCompanyNM);

                gridShow();
            }
        }
        private void gridShow()
        {

            conn.Open();
            string EMPID = Session["EMPID"].ToString();
            SqlCommand cmd = new SqlCommand(@"SELECT     HR_POST.POSTNM, HR_EMPSALARY.SALSTATUS, HR_EMPSALARY.BASICSAL, HR_EMPSALARY.HOUSERENT, HR_EMPSALARY.MEDICAL, HR_EMPSALARY.PFRATE, CONVERT(NVARCHAR(10),HR_EMPSALARY.PFEFDT,103) AS PFEFDT, 
                                             CONVERT(NVARCHAR(10),HR_EMPSALARY.JOBEFDT,103) AS JOBEFDT, CONVERT(NVARCHAR(10),HR_EMPSALARY.JOBETDT,103) AS JOBETDT
                                             FROM HR_POST INNER JOIN
                                             HR_EMPSALARY ON HR_POST.POSTID = HR_EMPSALARY.POSTID   WHERE HR_EMPSALARY.EMPID='" + EMPID + "'", conn);
            SqlCommand cmd1 = new SqlCommand(@"SELECT     HR_EMP.EMPNM, HR_EMP.GENDER, HR_DEPT.DEPTNM, CONVERT(NVARCHAR(10),HR_EMP.JOININGDT,103) AS JOININGDT, HR_EMP.EMPTP
                                               FROM HR_EMP INNER JOIN
                                               HR_DEPT ON HR_EMP.DEPTID = HR_DEPT.DEPTID WHERE HR_EMP.EMPID='" + EMPID + "'", conn);
            SqlDataReader rd = cmd1.ExecuteReader();
            while (rd.Read())
            {
                lblEmpNM.Text = rd["EMPNM"].ToString();
                lblGender.Text = rd["GENDER"].ToString();
                lblJoiningDT.Text = rd["JOININGDT"].ToString();
                lblDptNM.Text = rd["DEPTNM"].ToString();
                lblEmpTP.Text = rd["EMPTP"].ToString();
            }
            rd.Close();
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
                string POSTNM = DataBinder.Eval(e.Row.DataItem, "POSTNM").ToString();
                e.Row.Cells[0].Text = "&nbsp;" + POSTNM;

                string TNSTP = DataBinder.Eval(e.Row.DataItem, "SALSTATUS").ToString();
                e.Row.Cells[1].Text = "&nbsp;" + TNSTP;

                string BASICSAL = DataBinder.Eval(e.Row.DataItem, "BASICSAL").ToString();
                e.Row.Cells[2].Text = "&nbsp;" + BASICSAL;

                string HOUSERENT = DataBinder.Eval(e.Row.DataItem, "HOUSERENT").ToString();
                e.Row.Cells[3].Text = "&nbsp;" + HOUSERENT;

                string MEDICAL = DataBinder.Eval(e.Row.DataItem, "MEDICAL").ToString();
                e.Row.Cells[4].Text = "&nbsp;" + MEDICAL;

                string PFRATE = DataBinder.Eval(e.Row.DataItem, "PFRATE").ToString();
                e.Row.Cells[5].Text = "&nbsp;" + PFRATE;

                string PFEFDT = DataBinder.Eval(e.Row.DataItem, "PFEFDT").ToString();
                e.Row.Cells[6].Text = "&nbsp;" + PFEFDT;

                string JOBEFDT = DataBinder.Eval(e.Row.DataItem, "JOBEFDT").ToString();
                e.Row.Cells[7].Text = "&nbsp;" + JOBEFDT;

                string JOBETDT = DataBinder.Eval(e.Row.DataItem, "JOBETDT").ToString();
                e.Row.Cells[8].Text = "&nbsp;" + JOBETDT;
            }
        }
    }
}