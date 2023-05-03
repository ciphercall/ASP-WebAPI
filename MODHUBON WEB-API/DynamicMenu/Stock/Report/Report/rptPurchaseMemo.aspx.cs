using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data;
using System.Configuration;
using AlchemyAccounting;
using System.Data.SqlClient;
using System.Web.Services;
using System.Text;
using System.Collections.Specialized;
using System.Threading;

namespace AlchemyAccounting.Stock.Report.Report
{
    public partial class rptPurchaseMemo : System.Web.UI.Page
    {
        IFormatProvider dateformat = new System.Globalization.CultureInfo("fr-FR", true);

        protected void Page_Load(object sender, EventArgs e)
        {
            try
            {
                string uTp = HttpContext.Current.Session["USERTYPE"].ToString();
                string brCD = HttpContext.Current.Session["BrCD"].ToString();

                if (uTp == "COMPADMIN")
                {
                    dbFunctions.lblAdd(@"SELECT COMPNM FROM ASL_COMPANY WHERE COMPID ='101'", lblCompNM);
                    dbFunctions.lblAdd(@"SELECT ADDRESS FROM ASL_COMPANY WHERE COMPID ='101'", lblAddress);
                    dbFunctions.lblAdd(@"SELECT CONTACTNO FROM ASL_COMPANY WHERE COMPID ='101'", lblContact);
                }
                else
                {
                    dbFunctions.lblAdd(@"SELECT COMPNM FROM ASL_COMPANY WHERE COMPID ='" + brCD + "'", lblCompNM);
                    dbFunctions.lblAdd(@"SELECT ADDRESS FROM ASL_COMPANY WHERE COMPID ='" + brCD + "'", lblAddress);
                    dbFunctions.lblAdd(@"SELECT CONTACTNO FROM ASL_COMPANY WHERE COMPID ='" + brCD + "'", lblContact);
                }

                DateTime PrintDate = dbFunctions.Timezone(DateTime.Now);
                string td = PrintDate.ToString("dd-MMM-yyyy hh:mm tt");
                lblTime.Text = td;

                string puchaseno = Session["TRANSNO"].ToString();
                string sotreid = Session["STOREID"].ToString();
                string storename = Session["STORENAME"].ToString();
                string date = Session["DATE"].ToString();

                lblInVNo.Text = puchaseno;
                lblStoreName.Text = storename;
                lblInVDT.Text = date;
                
                showGrid();
                dbFunctions.lblAdd(
                    "SELECT SUM(QTY) FROM STK_TRANS WHERE STK_TRANS.TRANSTP='BUY' and STK_TRANS.TRANSNO='" + puchaseno + "' AND STK_TRANS.UNITTO='" + sotreid + "'", lblTotQTy);

                dbFunctions.lblAdd(@"SELECT GL_ACCHART.ACCOUNTNM FROM STK_TRANSMST INNER JOIN
                         GL_ACCHART ON STK_TRANSMST.PSID = GL_ACCHART.ACCOUNTCD
                WHERE (STK_TRANSMST.TRANSTP = 'BUY') AND (STK_TRANSMST.TRANSNO = '" + puchaseno + "') AND (STK_TRANSMST.UNITTO = '" + sotreid + "')", lblPurchaseFrom);
                dbFunctions.lblAdd(@"SELECT TRRTSNO FROM STK_TRANSMST WHERE TRANSTP='BUY' and TRANSNO='" + puchaseno + "' AND UNITTO='" + sotreid + "'", lblSalesMemoNo);

                SqlConnection con=new SqlConnection(dbFunctions.connection);
                SqlCommand cmdCommand=new SqlCommand(@"SELECT REMARKS, TOTAMT, TPCOST, DISCOUNT, NETAMT FROM STK_TRANSMST 
                WHERE STK_TRANSMST.TRANSTP='BUY' and STK_TRANSMST.TRANSNO='"+ puchaseno + "' AND STK_TRANSMST.UNITTO='"+ sotreid + "'", con);
                con.Open();
                SqlDataReader drReader = cmdCommand.ExecuteReader();
                while (drReader.Read())
                {
                    lblRemarks.Text = drReader["REMARKS"].ToString();
                    lblTotAmount.Text = drReader["TOTAMT"].ToString();
                    lbltransportCose.Text = drReader["TPCOST"].ToString();
                    lblDiscount.Text = drReader["DISCOUNT"].ToString();
                    lblNetAmt.Text = drReader["NETAMT"].ToString();
                }
                drReader.Close();
                con.Close();

                lblInWords.Text = SpellAmount.MoneyConvFn(lblNetAmt.Text);

            }
            catch (Exception ex)
            {
                Response.Write(ex.Message);
            }
        }

        public void showGrid()
        {
            string connectionString = ConfigurationManager.ConnectionStrings["AslDbContext"].ConnectionString;
            SqlConnection conn = new SqlConnection(connectionString);
            string puchaseno = Session["TRANSNO"].ToString();
            string sotreid = Session["STOREID"].ToString();
            conn.Open();
            SqlCommand cmd = new SqlCommand(@"SELECT ROW_NUMBER() OVER(ORDER BY ITEMSL) AS SL,STK_TRANS.ITEMCD, STK_ITEM.ITEMNM, STK_TRANS.ITEMID, STK_TRANS.QTY, STK_TRANS.RATE, STK_TRANS.AMOUNT, STK_TRANS.ITEMSL
            FROM STK_TRANS INNER JOIN STK_ITEM ON STK_TRANS.ITEMID = STK_ITEM.ITEMID
            where STK_TRANS.TRANSTP='BUY' and STK_TRANS.TRANSNO='" + puchaseno + "' AND STK_TRANS.UNITTO='" + sotreid + "' order by STK_TRANS.ITEMSL", conn);
            
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
                decimal amNT = Convert.ToDecimal(DataBinder.Eval(e.Row.DataItem, "AMOUNT").ToString());
                string AMOUNT = SpellAmount.comma(amNT);
                e.Row.Cells[5].Text = AMOUNT;
            }
        }

    }
}