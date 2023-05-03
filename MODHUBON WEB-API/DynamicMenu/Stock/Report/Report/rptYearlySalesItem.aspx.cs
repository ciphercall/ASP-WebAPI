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
    public partial class rptYearlySalesItem : System.Web.UI.Page
    {
        IFormatProvider dateformat = new System.Globalization.CultureInfo("fr-FR", true);

        decimal dblGrandTotalJanQty = 0;
        decimal dblGrandTotalFebQty = 0;
        decimal dblGrandTotalMarchQty = 0;
        decimal dblGrandTotalAprilQty = 0;
        decimal dblGrandTotalMayQty = 0;
        decimal dblGrandTotalJuneQty = 0;
        decimal dblGrandTotalJulyQty = 0;
        decimal dblGrandTotalAugustQty = 0;
        decimal dblGrandTotalSeptemberQty = 0;
        decimal dblGrandTotalOctoberQty = 0;
        decimal dblGrandTotalNovemberQty = 0;
        decimal dblGrandTotalDecemberQty = 0;
        decimal dblGrandTotalTotalQty = 0;
        //string AmountComma = "";

        string dblGrandTotalJanQtyComma = "";
        string dblGrandTotalFebQtyComma = "";
        string dblGrandTotalMarchQtyComma = "";
        string dblGrandTotalAprilQtyComma = "";
        string dblGrandTotalMayQtyComma = "";
        string dblGrandTotalJuneQtyComma = "";
        string dblGrandTotalJulyQtyComma = "";
        string dblGrandTotalAugustQtyComma = "";
        string dblGrandTotalSeptemberQtyComma = "";
        string dblGrandTotalOctoberQtyComma = "";
        string dblGrandTotalNovemberQtyComma = "";
        string dblGrandTotalDecemberQtyComma = "";
        string dblGrandTotalTotalQtyComma = "";

        protected void Page_Load(object sender, EventArgs e)
        {

            if (Session["UserName"] == null)
            {
                Response.Redirect("~/Login/UI/Login.aspx");
            }
            else
            {
                dbFunctions.lblAdd(@"SELECT COMPNM FROM ASL_COMPANY WHERE COMPID ='101'", lblCompNM);
                dbFunctions.lblAdd(@"SELECT ADDRESS FROM ASL_COMPANY WHERE COMPID ='101'", lblAddress);

                DateTime PrintDate = dbFunctions.Timezone(DateTime.Now);
                string td = PrintDate.ToString("dd-MMM-yyyy hh:mm tt");
                lblTime.Text = td;

                //Session["btnSearct"] = "Qty";
                //Session["PartyId"] = ddlPartyList.SelectedValue;
                //Session["PartyName"] = ddlPartyList.SelectedItem.Text;

                //Session["Year"] = txtFrom.Text;


                lblParty.Text = Session["PartyName"].ToString();
                lblDate.Text = Session["Year"].ToString();

                showGrid();
            }
        }
        public void showGrid()
        {
            SqlConnection conn = new SqlConnection(dbFunctions.connection);

            string partyid = Session["PartyId"].ToString();
            string year = Session["Year"].ToString();
          // string Qty= Session["btnqty"].ToString();
           string Search= Session["btnSearct"].ToString();



            string query = "";
            var party = "";
            if (partyid == "ALL")
            {
                party = " ";
            }
            else
            {
                party = "AND PSID = '" + partyid + "'";
            }
            conn.Open();
            if (Search== "Amount")
            {
                query = $@"SELECT ITEMNM, SUM(ISNULL(JANQTY,0)) JANQTY, SUM(ISNULL(FEBQTY,0)) FEBQTY, SUM(ISNULL(MARQTY,0)) MARQTY, SUM(ISNULL(APRQTY,0)) APRQTY, 
SUM(ISNULL(MAYQTY,0)) MAYQTY, SUM(ISNULL(JUNQTY,0)) JUNQTY, SUM(ISNULL(JULQTY,0)) JULQTY, SUM(ISNULL(AUGQTY,0)) AUGQTY, 
SUM(ISNULL(SEPQTY,0)) SEPQTY, SUM(ISNULL(OCTQTY,0)) OCTQTY, SUM(ISNULL(NOVQTY,0)) NOVQTY, SUM(ISNULL(DECQTY,0)) DECQTY,
SUM(ISNULL(JANQTY,0))+SUM(ISNULL(FEBQTY,0))+SUM(ISNULL(MARQTY,0))+SUM(ISNULL(APRQTY,0))+SUM(ISNULL(MAYQTY,0))+SUM(ISNULL(JUNQTY,0))+
SUM(ISNULL(JULQTY,0))+SUM(ISNULL(AUGQTY,0))+SUM(ISNULL(SEPQTY,0))+SUM(ISNULL(OCTQTY,0))+SUM(ISNULL(NOVQTY,0))+SUM(ISNULL(DECQTY,0)) TOTQTY
FROM(SELECT ITEMID, SUM(ISNULL(NETAMT,0)) JANQTY, 0 FEBQTY, 0 MARQTY, 0 APRQTY, 0 MAYQTY, 0 JUNQTY, 0 JULQTY, 0 AUGQTY, 0 SEPQTY, 0 OCTQTY, 0 NOVQTY, 0 DECQTY
FROM   STK_TRANS WHERE TRANSTP = 'SALE' AND CAST(CAST(TRANSDT as DATE) as VARCHAR(7)) = '{year}-01' {party}
AND ISNULL(QTY,0)!=0 GROUP BY ITEMID
UNION SELECT ITEMID, 0 JANQTY, SUM(ISNULL(NETAMT,0)) FEBQTY, 0 MARQTY, 0 APRQTY, 0 MAYQTY, 0 JUNQTY, 0 JULQTY, 0 AUGQTY, 0 SEPQTY, 0 OCTQTY, 0 NOVQTY, 0 DECQTY
FROM   STK_TRANS WHERE TRANSTP = 'SALE' AND CAST(CAST(TRANSDT as DATE) as VARCHAR(7)) = '{year}-02'  {party}
AND ISNULL(QTY,0)!=0 GROUP BY ITEMID
UNION SELECT ITEMID, 0 JANQTY, 0 FEBQTY, SUM(ISNULL(NETAMT,0)) MARQTY, 0 APRQTY, 0 MAYQTY, 0 JUNQTY, 0 JULQTY, 0 AUGQTY, 0 SEPQTY, 0 OCTQTY, 0 NOVQTY, 0 DECQTY
FROM   STK_TRANS WHERE TRANSTP = 'SALE' AND CAST(CAST(TRANSDT as DATE) as VARCHAR(7)) = '{year}-03'  {party}
AND ISNULL(QTY,0)!=0 GROUP BY ITEMID
UNION SELECT ITEMID, 0 JANQTY, 0 FEBQTY, 0 MARQTY, SUM(ISNULL(NETAMT,0)) APRQTY, 0 MAYQTY, 0 JUNQTY, 0 JULQTY, 0 AUGQTY, 0 SEPQTY, 0 OCTQTY, 0 NOVQTY, 0 DECQTY
FROM   STK_TRANS WHERE TRANSTP = 'SALE' AND CAST(CAST(TRANSDT as DATE) as VARCHAR(7)) = '{year}-04'  {party}
AND ISNULL(QTY,0)!=0 GROUP BY ITEMID
UNION SELECT ITEMID, 0 JANQTY, 0 FEBQTY, 0 MARQTY, 0 APRQTY, SUM(ISNULL(NETAMT,0)) MAYQTY, 0 JUNQTY, 0 JULQTY, 0 AUGQTY, 0 SEPQTY, 0 OCTQTY, 0 NOVQTY, 0 DECQTY
FROM   STK_TRANS WHERE TRANSTP = 'SALE' AND CAST(CAST(TRANSDT as DATE) as VARCHAR(7)) = '{year}-05'  {party}
AND ISNULL(QTY,0)!=0 GROUP BY ITEMID
UNION SELECT ITEMID, 0 JANQTY, 0 FEBQTY, 0 MARQTY, 0 APRQTY, 0 MAYQTY, SUM(ISNULL(NETAMT,0)) JUNQTY, 0 JULQTY, 0 AUGQTY, 0 SEPQTY, 0 OCTQTY, 0 NOVQTY, 0 DECQTY
FROM   STK_TRANS WHERE TRANSTP = 'SALE' AND CAST(CAST(TRANSDT as DATE) as VARCHAR(7)) = '{year}-06'  {party}
AND ISNULL(QTY,0)!=0 GROUP BY ITEMID
UNION SELECT ITEMID, 0 JANQTY, 0 FEBQTY, 0 MARQTY, 0 APRQTY, 0 MAYQTY, 0 JUNQTY, SUM(ISNULL(NETAMT,0)) JULQTY, 0 AUGQTY, 0 SEPQTY, 0 OCTQTY, 0 NOVQTY, 0 DECQTY
FROM   STK_TRANS WHERE TRANSTP = 'SALE' AND CAST(CAST(TRANSDT as DATE) as VARCHAR(7)) = '{year}-07' {party}
AND ISNULL(QTY,0)!=0 GROUP BY ITEMID
UNION SELECT ITEMID, 0 JANQTY, 0 FEBQTY, 0 MARQTY, 0 APRQTY, 0 MAYQTY, 0 JUNQTY, 0 JULQTY, SUM(ISNULL(NETAMT,0)) AUGQTY, 0 SEPQTY, 0 OCTQTY, 0 NOVQTY, 0 DECQTY
FROM   STK_TRANS WHERE TRANSTP = 'SALE' AND CAST(CAST(TRANSDT as DATE) as VARCHAR(7)) = '{year}-08' {party}
AND ISNULL(QTY,0)!=0 GROUP BY ITEMID
UNION SELECT ITEMID, 0 JANQTY, 0 FEBQTY, 0 MARQTY, 0 APRQTY, 0 MAYQTY, 0 JUNQTY, 0 JULQTY, 0 AUGQTY, SUM(ISNULL(NETAMT,0)) SEPQTY, 0 OCTQTY, 0 NOVQTY, 0 DECQTY
FROM   STK_TRANS WHERE TRANSTP = 'SALE' AND CAST(CAST(TRANSDT as DATE) as VARCHAR(7)) = '{year}-09' {party}
AND ISNULL(QTY,0)!=0 GROUP BY ITEMID
UNION SELECT ITEMID, 0 JANQTY, 0 FEBQTY, 0 MARQTY, 0 APRQTY, 0 MAYQTY, 0 JUNQTY, 0 JULQTY, 0 AUGQTY, 0 SEPQTY, SUM(ISNULL(NETAMT,0)) OCTQTY, 0 NOVQTY, 0 DECQTY
FROM   STK_TRANS WHERE TRANSTP = 'SALE' AND CAST(CAST(TRANSDT as DATE) as VARCHAR(7)) = '{year}-10' {party}
AND ISNULL(QTY,0)!=0 GROUP BY ITEMID
UNION SELECT ITEMID, 0 JANQTY, 0 FEBQTY, 0 MARQTY, 0 APRQTY, 0 MAYQTY, 0 JUNQTY, 0 JULQTY, 0 AUGQTY, 0 SEPQTY, 0 OCTQTY, SUM(ISNULL(NETAMT,0)) NOVQTY, 0 DECQTY
FROM   STK_TRANS WHERE TRANSTP = 'SALE' AND CAST(CAST(TRANSDT as DATE) as VARCHAR(7)) = '{year}-11' {party}
AND ISNULL(QTY,0)!=0 GROUP BY ITEMID
UNION SELECT ITEMID, 0 JANQTY, 0 FEBQTY, 0 MARQTY, 0 APRQTY, 0 MAYQTY, 0 JUNQTY, 0 JULQTY, 0 AUGQTY, 0 SEPQTY, 0 OCTQTY, 0 NOVQTY, SUM(ISNULL(NETAMT,0)) DECQTY
FROM   STK_TRANS WHERE TRANSTP = 'SALE' AND CAST(CAST(TRANSDT as DATE) as VARCHAR(7)) = '{year}-12' {party}
AND ISNULL(QTY,0)!=0 GROUP BY ITEMID
) A INNER JOIN STK_ITEM B ON A.ITEMID = B.ITEMID
GROUP BY ITEMNM";
            }
            else
            {
                query = $@"SELECT ITEMNM, SUM(ISNULL(JANQTY,0)) JANQTY, SUM(ISNULL(FEBQTY,0)) FEBQTY, SUM(ISNULL(MARQTY,0)) MARQTY, SUM(ISNULL(APRQTY,0)) APRQTY, 
SUM(ISNULL(MAYQTY,0)) MAYQTY, SUM(ISNULL(JUNQTY,0)) JUNQTY, SUM(ISNULL(JULQTY,0)) JULQTY, SUM(ISNULL(AUGQTY,0)) AUGQTY, 
SUM(ISNULL(SEPQTY,0)) SEPQTY, SUM(ISNULL(OCTQTY,0)) OCTQTY, SUM(ISNULL(NOVQTY,0)) NOVQTY, SUM(ISNULL(DECQTY,0)) DECQTY,
SUM(ISNULL(JANQTY,0))+SUM(ISNULL(FEBQTY,0))+SUM(ISNULL(MARQTY,0))+SUM(ISNULL(APRQTY,0))+SUM(ISNULL(MAYQTY,0))+SUM(ISNULL(JUNQTY,0))+
SUM(ISNULL(JULQTY,0))+SUM(ISNULL(AUGQTY,0))+SUM(ISNULL(SEPQTY,0))+SUM(ISNULL(OCTQTY,0))+SUM(ISNULL(NOVQTY,0))+SUM(ISNULL(DECQTY,0)) TOTQTY
FROM(SELECT ITEMID, SUM(ISNULL(QTY,0)) JANQTY, 0 FEBQTY, 0 MARQTY, 0 APRQTY, 0 MAYQTY, 0 JUNQTY, 0 JULQTY, 0 AUGQTY, 0 SEPQTY, 0 OCTQTY, 0 NOVQTY, 0 DECQTY
FROM   STK_TRANS WHERE TRANSTP = 'SALE' AND CAST(CAST(TRANSDT as DATE) as VARCHAR(7)) = '{year}-01' {party}
AND ISNULL(QTY,0)!=0 GROUP BY ITEMID
UNION SELECT ITEMID, 0 JANQTY, SUM(ISNULL(QTY,0)) FEBQTY, 0 MARQTY, 0 APRQTY, 0 MAYQTY, 0 JUNQTY, 0 JULQTY, 0 AUGQTY, 0 SEPQTY, 0 OCTQTY, 0 NOVQTY, 0 DECQTY
FROM   STK_TRANS WHERE TRANSTP = 'SALE' AND CAST(CAST(TRANSDT as DATE) as VARCHAR(7)) = '{year}-02' {party}
AND ISNULL(QTY,0)!=0 GROUP BY ITEMID
UNION SELECT ITEMID, 0 JANQTY, 0 FEBQTY, SUM(ISNULL(QTY,0)) MARQTY, 0 APRQTY, 0 MAYQTY, 0 JUNQTY, 0 JULQTY, 0 AUGQTY, 0 SEPQTY, 0 OCTQTY, 0 NOVQTY, 0 DECQTY
FROM   STK_TRANS WHERE TRANSTP = 'SALE' AND CAST(CAST(TRANSDT as DATE) as VARCHAR(7)) = '{year}-03' {party}
AND ISNULL(QTY,0)!=0 GROUP BY ITEMID
UNION SELECT ITEMID, 0 JANQTY, 0 FEBQTY, 0 MARQTY, SUM(ISNULL(QTY,0)) APRQTY, 0 MAYQTY, 0 JUNQTY, 0 JULQTY, 0 AUGQTY, 0 SEPQTY, 0 OCTQTY, 0 NOVQTY, 0 DECQTY
FROM   STK_TRANS WHERE TRANSTP = 'SALE' AND CAST(CAST(TRANSDT as DATE) as VARCHAR(7)) = '{year}-04' {party}
AND ISNULL(QTY,0)!=0 GROUP BY ITEMID
UNION SELECT ITEMID, 0 JANQTY, 0 FEBQTY, 0 MARQTY, 0 APRQTY, SUM(ISNULL(QTY,0)) MAYQTY, 0 JUNQTY, 0 JULQTY, 0 AUGQTY, 0 SEPQTY, 0 OCTQTY, 0 NOVQTY, 0 DECQTY
FROM   STK_TRANS WHERE TRANSTP = 'SALE' AND CAST(CAST(TRANSDT as DATE) as VARCHAR(7)) = '{year}-05' {party}
AND ISNULL(QTY,0)!=0 GROUP BY ITEMID
UNION SELECT ITEMID, 0 JANQTY, 0 FEBQTY, 0 MARQTY, 0 APRQTY, 0 MAYQTY, SUM(ISNULL(QTY,0)) JUNQTY, 0 JULQTY, 0 AUGQTY, 0 SEPQTY, 0 OCTQTY, 0 NOVQTY, 0 DECQTY
FROM   STK_TRANS WHERE TRANSTP = 'SALE' AND CAST(CAST(TRANSDT as DATE) as VARCHAR(7)) = '{year}-06' {party}
AND ISNULL(QTY,0)!=0 GROUP BY ITEMID
UNION SELECT ITEMID, 0 JANQTY, 0 FEBQTY, 0 MARQTY, 0 APRQTY, 0 MAYQTY, 0 JUNQTY, SUM(ISNULL(QTY,0)) JULQTY, 0 AUGQTY, 0 SEPQTY, 0 OCTQTY, 0 NOVQTY, 0 DECQTY
FROM   STK_TRANS WHERE TRANSTP = 'SALE' AND CAST(CAST(TRANSDT as DATE) as VARCHAR(7)) = '{year}-07' {party}
AND ISNULL(QTY,0)!=0 GROUP BY ITEMID
UNION SELECT ITEMID, 0 JANQTY, 0 FEBQTY, 0 MARQTY, 0 APRQTY, 0 MAYQTY, 0 JUNQTY, 0 JULQTY, SUM(ISNULL(QTY,0)) AUGQTY, 0 SEPQTY, 0 OCTQTY, 0 NOVQTY, 0 DECQTY
FROM   STK_TRANS WHERE TRANSTP = 'SALE' AND CAST(CAST(TRANSDT as DATE) as VARCHAR(7)) = '{year}-08' {party}
AND ISNULL(QTY,0)!=0 GROUP BY ITEMID
UNION SELECT ITEMID, 0 JANQTY, 0 FEBQTY, 0 MARQTY, 0 APRQTY, 0 MAYQTY, 0 JUNQTY, 0 JULQTY, 0 AUGQTY, SUM(ISNULL(QTY,0)) SEPQTY, 0 OCTQTY, 0 NOVQTY, 0 DECQTY
FROM   STK_TRANS WHERE TRANSTP = 'SALE' AND CAST(CAST(TRANSDT as DATE) as VARCHAR(7)) = '{year}-09' {party}
AND ISNULL(QTY,0)!=0 GROUP BY ITEMID
UNION SELECT ITEMID, 0 JANQTY, 0 FEBQTY, 0 MARQTY, 0 APRQTY, 0 MAYQTY, 0 JUNQTY, 0 JULQTY, 0 AUGQTY, 0 SEPQTY, SUM(ISNULL(QTY,0)) OCTQTY, 0 NOVQTY, 0 DECQTY
FROM   STK_TRANS WHERE TRANSTP = 'SALE' AND CAST(CAST(TRANSDT as DATE) as VARCHAR(7)) = '{year}-10' {party}
AND ISNULL(QTY,0)!=0 GROUP BY ITEMID
UNION SELECT ITEMID, 0 JANQTY, 0 FEBQTY, 0 MARQTY, 0 APRQTY, 0 MAYQTY, 0 JUNQTY, 0 JULQTY, 0 AUGQTY, 0 SEPQTY, 0 OCTQTY, SUM(ISNULL(QTY,0)) NOVQTY, 0 DECQTY
FROM   STK_TRANS WHERE TRANSTP = 'SALE' AND CAST(CAST(TRANSDT as DATE) as VARCHAR(7)) = '{year}-11' {party}
AND ISNULL(QTY,0)!=0 GROUP BY ITEMID
UNION SELECT ITEMID, 0 JANQTY, 0 FEBQTY, 0 MARQTY, 0 APRQTY, 0 MAYQTY, 0 JUNQTY, 0 JULQTY, 0 AUGQTY, 0 SEPQTY, 0 OCTQTY, 0 NOVQTY, SUM(ISNULL(QTY,0)) DECQTY
FROM   STK_TRANS WHERE TRANSTP = 'SALE' AND CAST(CAST(TRANSDT as DATE) as VARCHAR(7)) = '{year}-12' {party}
AND ISNULL(QTY,0)!=0 GROUP BY ITEMID
) A INNER JOIN STK_ITEM B ON A.ITEMID = B.ITEMID
GROUP BY ITEMNM
";

            }
           


            SqlCommand cmd = new SqlCommand(query, conn);

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
                //Label1.Visible = true;
                //Label1.Text = "No Data Found.";
            }

        }


        protected void GridView1_OnRowDataBound(object sender, GridViewRowEventArgs e)
        {
            // This is for cumulating the values       
            if (e.Row.RowType == DataControlRowType.DataRow)
            {
                string ItemNM = DataBinder.Eval(e.Row.DataItem, "ITEMNM").ToString();
                e.Row.Cells[0].Text = "&nbsp;" + ItemNM;

                //string MODEL = DataBinder.Eval(e.Row.DataItem, "MODEL").ToString();
                //e.Row.Cells[1].Text = "&nbsp;" + MODEL;

                //string BRAND = DataBinder.Eval(e.Row.DataItem, "BRAND").ToString();
                //e.Row.Cells[2].Text = "&nbsp;" + BRAND;

                decimal JANQTY = Convert.ToDecimal(DataBinder.Eval(e.Row.DataItem, "JANQTY").ToString());
                string janqty = SpellAmount.comma(JANQTY);
                e.Row.Cells[1].Text = janqty;

                dblGrandTotalJanQty += JANQTY;
                dblGrandTotalJanQtyComma = SpellAmount.comma(dblGrandTotalJanQty);


                decimal FEBQTY = Convert.ToDecimal(DataBinder.Eval(e.Row.DataItem, "FEBQTY").ToString());
                string febqty = SpellAmount.comma(FEBQTY);
                e.Row.Cells[2].Text = febqty;

                dblGrandTotalFebQty += FEBQTY;
                dblGrandTotalFebQtyComma = SpellAmount.comma(dblGrandTotalFebQty);


                decimal MARQTY = Convert.ToDecimal(DataBinder.Eval(e.Row.DataItem, "MARQTY").ToString());
                string marchqty = SpellAmount.comma(MARQTY);
                e.Row.Cells[3].Text = marchqty;

                dblGrandTotalMarchQty += MARQTY;
                dblGrandTotalMarchQtyComma = SpellAmount.comma(dblGrandTotalMarchQty);

                decimal APRQTY = Convert.ToDecimal(DataBinder.Eval(e.Row.DataItem, "APRQTY").ToString());
                string aprilqty = SpellAmount.comma(APRQTY);
                e.Row.Cells[4].Text = aprilqty;

                dblGrandTotalAprilQty += APRQTY;
                dblGrandTotalAprilQtyComma = SpellAmount.comma(dblGrandTotalAprilQty);


                decimal MAYQTY = Convert.ToDecimal(DataBinder.Eval(e.Row.DataItem, "MAYQTY").ToString());
                string mayqty = SpellAmount.comma(MAYQTY);
                e.Row.Cells[5].Text = mayqty;

                dblGrandTotalMayQty += MAYQTY;
                dblGrandTotalMayQtyComma = SpellAmount.comma(dblGrandTotalMayQty);


                decimal JUNQTY = Convert.ToDecimal(DataBinder.Eval(e.Row.DataItem, "JUNQTY").ToString());
                string juneqty = SpellAmount.comma(JUNQTY);
                e.Row.Cells[6].Text = juneqty;

                dblGrandTotalJuneQty += JUNQTY;
                dblGrandTotalJuneQtyComma = SpellAmount.comma(dblGrandTotalJuneQty);


                decimal JULQTY = Convert.ToDecimal(DataBinder.Eval(e.Row.DataItem, "JULQTY").ToString());
                string julyqty = SpellAmount.comma(JULQTY);
                e.Row.Cells[7].Text = julyqty;

                dblGrandTotalJulyQty += JULQTY;
                dblGrandTotalJulyQtyComma = SpellAmount.comma(dblGrandTotalJulyQty);


                decimal AUGQTY = Convert.ToDecimal(DataBinder.Eval(e.Row.DataItem, "AUGQTY").ToString());
                string augqty = SpellAmount.comma(AUGQTY);
                e.Row.Cells[8].Text = augqty;

                dblGrandTotalAugustQty += AUGQTY;
                dblGrandTotalAugustQtyComma = SpellAmount.comma(dblGrandTotalAugustQty);


                decimal SEPQTY = Convert.ToDecimal(DataBinder.Eval(e.Row.DataItem, "SEPQTY").ToString());
                string sepqty = SpellAmount.comma(SEPQTY);
                e.Row.Cells[9].Text = sepqty;

                dblGrandTotalSeptemberQty += SEPQTY;
                dblGrandTotalSeptemberQtyComma = SpellAmount.comma(dblGrandTotalSeptemberQty);


                decimal OCTQTY = Convert.ToDecimal(DataBinder.Eval(e.Row.DataItem, "OCTQTY").ToString());
                string octqty = SpellAmount.comma(OCTQTY);
                e.Row.Cells[10].Text = octqty;

                dblGrandTotalOctoberQty += OCTQTY;
                dblGrandTotalOctoberQtyComma = SpellAmount.comma(dblGrandTotalOctoberQty);



                decimal NOVQTY = Convert.ToDecimal(DataBinder.Eval(e.Row.DataItem, "NOVQTY").ToString());
                string novqty = SpellAmount.comma(NOVQTY);
                e.Row.Cells[11].Text = novqty;

                dblGrandTotalNovemberQty += NOVQTY;
                dblGrandTotalNovemberQtyComma = SpellAmount.comma(dblGrandTotalNovemberQty);



                decimal DECQTY = Convert.ToDecimal(DataBinder.Eval(e.Row.DataItem, "DECQTY").ToString());
                string decqty = SpellAmount.comma(DECQTY);
                e.Row.Cells[12].Text = decqty;

                dblGrandTotalDecemberQty += DECQTY;
                dblGrandTotalDecemberQtyComma = SpellAmount.comma(dblGrandTotalDecemberQty);



                decimal TOTQTY = Convert.ToDecimal(DataBinder.Eval(e.Row.DataItem, "TOTQTY").ToString());
                string totalqty = SpellAmount.comma(TOTQTY);
                e.Row.Cells[13].Text = totalqty;

                dblGrandTotalTotalQty += TOTQTY;
                dblGrandTotalTotalQtyComma = SpellAmount.comma(dblGrandTotalTotalQty);


            }
            else if (e.Row.RowType == DataControlRowType.Footer)
            {
                e.Row.Cells[0].Text = "TOTAL :";
                e.Row.Cells[0].HorizontalAlign = HorizontalAlign.Right;
                e.Row.Cells[1].Text = dblGrandTotalJanQtyComma;
                e.Row.Cells[1].HorizontalAlign = HorizontalAlign.Right;
                e.Row.Cells[2].Text = dblGrandTotalFebQtyComma;
                e.Row.Cells[2].HorizontalAlign = HorizontalAlign.Right;
                e.Row.Cells[3].Text = dblGrandTotalMarchQtyComma;
                e.Row.Cells[3].HorizontalAlign = HorizontalAlign.Right;
                e.Row.Cells[4].Text = dblGrandTotalAprilQtyComma;
                e.Row.Cells[4].HorizontalAlign = HorizontalAlign.Right;
                e.Row.Cells[5].Text = dblGrandTotalMayQtyComma;
                e.Row.Cells[5].HorizontalAlign = HorizontalAlign.Right;
                e.Row.Cells[6].Text = dblGrandTotalJuneQtyComma;
                e.Row.Cells[6].HorizontalAlign = HorizontalAlign.Right;
                e.Row.Cells[7].Text = dblGrandTotalJulyQtyComma;
                e.Row.Cells[7].HorizontalAlign = HorizontalAlign.Right;
                e.Row.Cells[8].Text = dblGrandTotalAugustQtyComma;
                e.Row.Cells[8].HorizontalAlign = HorizontalAlign.Right;
                e.Row.Cells[9].Text = dblGrandTotalSeptemberQtyComma;
                e.Row.Cells[9].HorizontalAlign = HorizontalAlign.Right;
                e.Row.Cells[10].Text = dblGrandTotalOctoberQtyComma;
                e.Row.Cells[10].HorizontalAlign = HorizontalAlign.Right;
                e.Row.Cells[11].Text = dblGrandTotalNovemberQtyComma;
                e.Row.Cells[11].HorizontalAlign = HorizontalAlign.Right;
                e.Row.Cells[12].Text = dblGrandTotalDecemberQtyComma;
                e.Row.Cells[12].HorizontalAlign = HorizontalAlign.Right;
                e.Row.Cells[13].Text = dblGrandTotalTotalQtyComma;
                e.Row.Cells[13].HorizontalAlign = HorizontalAlign.Right;

                e.Row.Font.Bold = true;
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