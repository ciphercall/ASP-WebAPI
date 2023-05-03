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
using DynamicMenu;

namespace AlchemyAccounting.Stock.Report.Report
{
    public partial class rptTransferMemoEdit : System.Web.UI.Page
    {
        IFormatProvider dateformat = new System.Globalization.CultureInfo("fr-FR", true);

        protected void Page_Load(object sender, EventArgs e)
        {
            try
            {
                string uTp = HttpContext.Current.Session["USERTYPE"].ToString();
                string brCD = HttpContext.Current.Session["BrCD"].ToString();

                dbFunctions.lblAdd(@"SELECT COMPNM FROM ASL_COMPANY WHERE COMPID ='101'", lblCompNM);
                dbFunctions.lblAdd(@"SELECT ADDRESS FROM ASL_COMPANY WHERE COMPID ='101'", lblAddress);
                dbFunctions.lblAdd(@"SELECT CONTACTNO FROM ASL_COMPANY WHERE COMPID ='101'", lblContact);


                DateTime PrintDate = dbFunctions.Timezone(DateTime.Now);
                string td = PrintDate.ToString("dd-MMM-yyyy hh:mm tt");
                lblTime.Text = td;
                string InvDate_P = Session["InvDate_T"].ToString();
                //string InvNoEdit_P = Session["InvNoEdit_T"].ToString();
                string Memo_P = Session["Memo_T"].ToString();
                //string PartyNM_P = Session["PartyNM_T"].ToString();
                lblTransferFrom.Text = Session["StoreNM_T"].ToString();
                lblTransferTo.Text = Session["PartyNM_T"].ToString();

                DateTime InDT = DateTime.Parse(InvDate_P, dateformat, System.Globalization.DateTimeStyles.AssumeLocal);
                lblInVDT.Text = InDT.ToString("dd-MMM-yyyy");

                lblSalesMemoNo.Text = Memo_P;


                // lblPurchaseFrom.Text = PartyNM_P;
                showGrid();

                string transNo = Session["TRANSFERMEMO"].ToString();
                lblInVNo.Text = transNo;
                string inDate = InDT.ToString("yyyy-MM-dd");
                dbFunctions.lblAdd(@"SELECT SUM(QTY) AS AMT FROM STK_TRANSCS WHERE TRANSTP='IIFG' AND TRANSNO='" + transNo + "' AND TRANSDT='" + inDate + "'", lblTotQTy);
                dbFunctions.lblAdd(@"SELECT SUM(AMOUNT) AS AMT FROM STK_TRANSCS WHERE TRANSTP='IIFG' AND TRANSNO='" + transNo + "' AND TRANSDT='" + inDate + "'", lblTotAmount);
                lblInWords.Text = "";
                decimal dec;
                decimal parseAmount = decimal.Parse(lblTotAmount.Text);
                string lblAmount = parseAmount.ToString();
                Boolean ValidInput = Decimal.TryParse(lblTotAmount.Text, out dec);
                if (!ValidInput)
                {
                    lblInWords.ForeColor = System.Drawing.Color.Red;
                    lblInWords.Text = "Enter the Proper Amount...";
                    return;
                }
                if (lblTotAmount.Text.ToString().Trim() == "")
                {
                    lblInWords.ForeColor = System.Drawing.Color.Red;
                    lblInWords.Text = "Amount Cannot Be Empty...";
                    return;
                }
                else
                {
                    if (Convert.ToDecimal(lblTotAmount.Text) == 0)
                    {
                        lblInWords.ForeColor = System.Drawing.Color.Red;
                        lblInWords.Text = "Amount Cannot Be Empty...";
                        return;
                    }
                }

                string x1 = "";
                string x2 = "";

                if (lblAmount.Contains("."))
                {
                    x1 = lblAmount.Trim().Substring(0, lblAmount.Trim().IndexOf("."));
                    x2 = lblAmount.Trim().Substring(lblAmount.Trim().IndexOf(".") + 1);
                }
                else
                {
                    x1 = lblAmount.Trim();
                    x2 = "00";
                }

                if (x1.ToString().Trim() != "")
                {
                    x1 = Convert.ToInt64(x1.Trim()).ToString().Trim();
                }
                else
                {
                    x1 = "0";
                }

                lblAmount = x1 + "." + x2;

                if (x2.Length > 2)
                {
                    lblAmount = Math.Round(Convert.ToDouble(lblAmount), 2).ToString().Trim();
                }

                string AmtConv = SpellAmount.MoneyConvFn(lblAmount.Trim());

                lblInWords.Text = AmtConv.Trim();

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

            string InvDate_P = "";
            //string InvNoEdit_P = "";
            //string Memo_P = "";
            //string StoreNM_P = "";
            //string StoreID_P = "";
            //string PartyNM_P = "";
            //string PartyID_P = "";

            //string type = Session["Type"].ToString();


            InvDate_P = Session["InvDate_T"].ToString();
            //InvNoEdit_P = Session["InvNoEdit_P"].ToString();
            //Memo_P = Session["Memo_P"].ToString();
            //StoreNM_P = Session["StoreNM_P"].ToString();
            //StoreID_P = Session["StoreID_P"].ToString();
            //PartyNM_P = Session["PartyNM_P"].ToString();
            //PartyID_P = Session["PartyID_P"].ToString();
            string transNo = Session["TRANSFERMEMO"].ToString();

            DateTime InDT = DateTime.Parse(InvDate_P, dateformat, System.Globalization.DateTimeStyles.AssumeLocal);
            string inDate = InDT.ToString("yyyy/MM/dd");

            conn.Open();
            SqlCommand cmd = new SqlCommand(@"SELECT ROW_NUMBER() over (order by CAST(STK_TRANSCS.TRANSSL AS INT)) as SL, STK_ITEM.ITEMCD, 
            STK_ITEM.ITEMNM, STK_TRANSCS.QTY, STK_TRANSCS.RATE, STK_TRANSCS.AMOUNT
            FROM STK_TRANSCS INNER JOIN
            STK_TRANSMSTCS ON STK_TRANSCS.TRANSTP = STK_TRANSMSTCS.TRANSTP AND STK_TRANSCS.TRANSMY = STK_TRANSMSTCS.TRANSMY AND 
            STK_TRANSCS.TRANSNO = STK_TRANSMSTCS.TRANSNO INNER JOIN
            STK_ITEM ON STK_TRANSCS.ITEMID = STK_ITEM.ITEMID " +
          " WHERE STK_TRANSMSTCS.TRANSTP='IIFG' AND STK_TRANSMSTCS.TRANSNO='" + transNo + "' AND STK_TRANSMSTCS.TRANSDT='" + inDate + "'  " +
          " ORDER BY CAST(STK_TRANSCS.TRANSSL AS INT)", conn);
            SqlDataAdapter da = new SqlDataAdapter(cmd);
            DataSet ds = new DataSet();
            da.Fill(ds);
            conn.Close();
            if (ds.Tables[0].Rows.Count > 0)
            {
                GridView1.DataSource = ds;
                GridView1.DataBind();
                GridView1.Visible = true;

                //Decimal totQty = 0;
                //Decimal a = 0;
                //foreach (GridViewRow grid in GridView1.Rows)
                //{
                //    string Debit = grid.Cells[3].Text;
                //    totQty = Convert.ToDecimal(Debit);
                //    a += totQty;
                //    decimal tot = a;
                //    lblTotQTy.Text = tot.ToString();
                //}

                //Decimal totCqty = 0;
                //Decimal c = 0;
                //foreach (GridViewRow grid in GridView1.Rows)
                //{
                //    Label lblCarton = (Label)grid.FindControl("lblCarton");
                //    totCqty = decimal.Parse(lblCarton.Text);
                //    c += totCqty;
                //    decimal tot = c;
                //    lblTotCQTy.Text = SpellAmount.comma(tot);
                //}

                //Decimal totAmount = 0;
                //Decimal b = 0;
                //foreach (GridViewRow grid in GridView1.Rows)
                //{
                //    string Amnt = grid.Cells[5].Text;
                //    totAmount = decimal.Parse(Amnt);
                //    b += totAmount;
                //    decimal tot = b;
                //    lblTotAmount.Text = SpellAmount.comma(tot);
                //}
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