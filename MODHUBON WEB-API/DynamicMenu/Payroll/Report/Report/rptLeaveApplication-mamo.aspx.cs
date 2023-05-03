using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;
using System.EnterpriseServices;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using AlchemyAccounting;

namespace DynamicMenu.Payroll.Report.Report
{
    public partial class rptLeaveApplication_mamo : System.Web.UI.Page
    {
        IFormatProvider dateformat = new System.Globalization.CultureInfo("fr-FR", true);
        SqlConnection conn = new SqlConnection(dbFunctions.connection);
        protected void Page_Load(object sender, EventArgs e)
        {
            try
            {
                //string uTp = HttpContext.Current.Session["USERTYPE"].ToString();
                //string brCD = HttpContext.Current.Session["BrCD"].ToString();
                string cmpid = HttpContext.Current.Session["COMPANYID"].ToString();

                //if (uTp == "COMPADMIN")
                //{
                    dbFunctions.lblAdd(@"SELECT COMPNM FROM ASL_COMPANY WHERE COMPID ='"+cmpid+"'", lblCompNM);
                    dbFunctions.lblAdd(@"SELECT ADDRESS FROM ASL_COMPANY WHERE COMPID ='" + cmpid + "'", lblAddress);
                    dbFunctions.lblAdd(@"SELECT CONTACTNO FROM ASL_COMPANY WHERE COMPID ='" + cmpid + "'", lblContact);
                //}
                //else
                //{
                //    dbFunctions.lblAdd(@"SELECT COMPNM FROM ASL_COMPANY WHERE COMPID ='" + brCD + "'", lblCompNM);
                //    dbFunctions.lblAdd(@"SELECT ADDRESS FROM ASL_COMPANY WHERE COMPID ='" + brCD + "'", lblAddress);
                //    dbFunctions.lblAdd(@"SELECT CONTACTNO FROM ASL_COMPANY WHERE COMPID ='" + brCD + "'", lblContact);
                //}
                dbFunctions.lblAdd(@"SELECT CONTACTNO FROM ASL_COMPANY", lblContact);
                DateTime PrintDate = dbFunctions.Timezone(DateTime.Now);
                string td = Session["USERNAME"] + " <br/> " + PrintDate.ToString("dd-MMM-yyyy hh:mm tt");
                lblTime.Text = td;

                string date = Session["Date"].ToString();
                string monyear = Session["MonthYear"].ToString();
                string Transno = Session["Transno"].ToString();
                string Remarks = Session["Remarks"].ToString();
                string EMPNM = Session["EMPNM"].ToString();
                
                
                //lblEntryTime.Text = InDT1.ToString("dd-MMM-yyyy hh:mm tt");
                

                DateTime InDT = DateTime.Parse(date, dateformat, System.Globalization.DateTimeStyles.AssumeLocal);
                //lblInVDT.Text = InDT.ToString("dd-MMM-yyyy");
                lblInVNo.Text = Transno;
                //lblPoNo.Text = wono;
                //lblEMNM.Text = unitnm;
                //lblPurchaseFrom.Text = psname;
                ////lblPurchaseFor.Text = StoreNM_P;
                dbFunctions.lblAdd(@"SELECT E.EMPNM FROM HR_LAPPSMST L INNER JOIN HR_EMP E ON L.EMPID=E.EMPID
                                    WHERE TRANSNO='"+Transno+"'",lblEMNM);
                dbFunctions.lblAdd(@"SELECT '"+monyear+"' FROM HR_LAPPSMST WHERE TRANSNO='"+Transno+"'",lblMY);
                dbFunctions.lblAdd(@"SELECT '"+ Remarks + "' FROM HR_LAPPSMST WHERE TRANSNO='"+Transno+"'",lblRemarks);
                dbFunctions.lblAdd(@"SELECT '"+ date + "' FROM HR_LAPPSMST WHERE TRANSNO='"+Transno+"'",lblInVDT0);
                

               // lblRemarks.Text = Remarks;

                showGrid();
               
            }
            catch (Exception ex)
            {
                Response.Write(ex.Message);
            }
        }
        public void showGrid()
        {
            // DateTime dateFR = DateTime.Parse(lblFrDT.Text, dateformat, System.Globalization.DateTimeStyles.AssumeLocal);
            // DateTime dateTO = DateTime.Parse(lblToDT.Text, dateformat, System.Globalization.DateTimeStyles.AssumeLocal);

            //string FRDT = dateFR.ToString("yyyy-MM-dd");
            //string TODT = dateTO.ToString("yyyy-MM-dd");
            string date = Session["Date"].ToString();
            string monyear = Session["MonthYear"].ToString();
            string Transno = Session["Transno"].ToString();
            string Remarks = Session["Remarks"].ToString();
            string EMPNM = Session["EMPNM"].ToString();
            conn.Open();
            SqlCommand cmd = new SqlCommand(@"SELECT LEV.LEAVENM,CONVERT(NVARCHAR,L.LEAVEFR, 103) LEAVEFR,CONVERT(NVARCHAR,L.LEAVETO, 103) LEAVETO,L.LEAVEDAYS,L.REASON FROM HR_LAPPS L INNER JOIN HR_LEAVE LEV ON L.LEAVEID=LEV.LEAVEID
                                        WHERE TRANSNO='" + Transno+"' AND TRANSMY='"+monyear+"'", conn);
            SqlDataAdapter da = new SqlDataAdapter(cmd);
            DataSet ds = new DataSet();
            da.Fill(ds);
            conn.Close();
            if (ds.Tables[0].Rows.Count > 0)
            {
                GridView1.DataSource = ds;
                GridView1.DataBind();
                GridView1.Visible = true;
            }
            else
            {

            }
        }
        protected void GridView1_RowDataBound(object sender, GridViewRowEventArgs e)
        {
            if (e.Row.RowType == DataControlRowType.DataRow)
            {
                string LEAVENM = DataBinder.Eval(e.Row.DataItem, "LEAVENM").ToString();
                e.Row.Cells[0].Text = LEAVENM;

                string LEAVEFR = DataBinder.Eval(e.Row.DataItem, "LEAVEFR").ToString();
                e.Row.Cells[1].Text = LEAVEFR;

                string LEAVETO = DataBinder.Eval(e.Row.DataItem, "LEAVETO").ToString();
                e.Row.Cells[2].Text = LEAVETO;

                string LEAVEDAYS = DataBinder.Eval(e.Row.DataItem, "LEAVEDAYS").ToString();
                e.Row.Cells[3].Text = LEAVEDAYS;

                string REASON = DataBinder.Eval(e.Row.DataItem, "REASON").ToString();
                e.Row.Cells[4].Text = REASON;

            }
        }

        private void ShowHeader(GridView grid)
        {
            if (grid.Rows.Count > 0)
            {
                grid.UseAccessibleHeader = true;
                grid.HeaderRow.TableSection = TableRowSection.TableHeader;
                //gridView.HeaderRow.Style["display"] = "table-header-group";
            }
        }
    }
}