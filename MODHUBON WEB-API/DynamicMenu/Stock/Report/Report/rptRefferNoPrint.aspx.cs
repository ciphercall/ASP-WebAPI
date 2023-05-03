using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using AlchemyAccounting;

namespace DynamicMenu.Stock.Report.Report
{
    public partial class rptRefferNoPrint : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            try
            {
                string uTp = HttpContext.Current.Session["USERTYPE"].ToString();
                string brCD = HttpContext.Current.Session["BrCD"].ToString();
                SqlCommand cmd = new SqlCommand();
                if (uTp == "COMPADMIN")
                {
                    dbFunctions.lblAdd(@"SELECT COMPNM FROM ASL_COMPANY WHERE COMPID ='101'", lblCompNM);
                    dbFunctions.lblAdd(@"SELECT ADDRESS FROM ASL_COMPANY WHERE COMPID ='101'", lblAddress);
                }
                else
                {
                    dbFunctions.lblAdd(@"SELECT COMPNM FROM ASL_COMPANY WHERE COMPID ='" + brCD + "'", lblCompNM);
                    dbFunctions.lblAdd(@"SELECT ADDRESS FROM ASL_COMPANY WHERE COMPID ='" + brCD + "'", lblAddress);
                }

                DateTime PrintDate = dbFunctions.Timezone(DateTime.Now);
                string td = PrintDate.ToString("dd-MMM-yyyy hh:mm tt");
                lblTime.Text = td;

                string ItemNM = Session["REFFERNO"].ToString();

                lblItemName.Text = ItemNM;
                showGrid();
            }
            catch
            {
            }
        }
        public void showGrid()
        {
            string connectionString = ConfigurationManager.ConnectionStrings["AslDbContext"].ConnectionString;
            SqlConnection conn = new SqlConnection(connectionString);

            string refferNo = Session["REFFERNO"].ToString();


            conn.Open();
            SqlCommand cmd = new SqlCommand(@"SELECT ITEMNM, ITEMCD, BUYRT, SALERT, MINSQTY FROM STK_ITEM 
                    WHERE REFNO=@REFNO", conn);
            cmd.Parameters.AddWithValue("@REFNO", refferNo);
            cmd.CommandTimeout = 0;
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
                GridView1.DataSource = ds;
                GridView1.DataBind();
                GridView1.Visible = true;
            }
        }

        protected void GridView1_RowDataBound(object sender, GridViewRowEventArgs e)
        {
            if (e.Row.RowType == DataControlRowType.DataRow)
            {
                string ITEMNM = DataBinder.Eval(e.Row.DataItem, "ITEMNM").ToString();
                e.Row.Cells[0].Text = "&nbsp;" + ITEMNM;

                string ITEMCD = DataBinder.Eval(e.Row.DataItem, "ITEMCD").ToString();
                e.Row.Cells[1].Text = "&nbsp;" + ITEMCD;

                string BUYRT = DataBinder.Eval(e.Row.DataItem, "BUYRT").ToString();
                e.Row.Cells[2].Text = "&nbsp;" + BUYRT;

                string SALERT = DataBinder.Eval(e.Row.DataItem, "SALERT").ToString();
                e.Row.Cells[3].Text = "&nbsp;" + SALERT;

                string MINSQTY = DataBinder.Eval(e.Row.DataItem, "MINSQTY").ToString();
                e.Row.Cells[4].Text = "&nbsp;" + MINSQTY;
            }

            else if (e.Row.RowType == DataControlRowType.Footer)
            {

            }

            ShowHeader(GridView1);
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