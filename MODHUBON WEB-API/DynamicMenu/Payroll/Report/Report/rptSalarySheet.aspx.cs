using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using AlchemyAccounting;

namespace DynamicMenu.Payroll.Report.Report
{
    public partial class rptSalarySheet : System.Web.UI.Page
    {
        IFormatProvider dateformat = new System.Globalization.CultureInfo("fr-FR", true);

        decimal GROSSSALamount = 0;
        string GROSSSALamountcomma = "0";

        decimal FOODINGADDamount = 0;
        string FOODINGADDamountcomma = "0";
        
        decimal AMTOTHOURamount = 0;
        string AMTOTHOURamountcomma = "0";
        
        decimal TOTADDamount = 0;
        string TOTADDamountcomma = "0";
        
        decimal ABSDEDamount = 0;
        string ABSDEDamountcomma = "0";

        decimal STAMPamount = 0;
        string STAMPamountcomma = "0";

        decimal MEDICALamount = 0;
        string MEDICALamountcomma = "0";

        decimal FOODINGamount = 0;
        string FOODINGamountcomma = "0";

        decimal ADVANCEamount = 0;
        string ADVANCEamountcomma = "0";

        decimal FINEADJamount = 0;
        string FINEADJamountcomma = "0";

        decimal ITAXamount = 0;
        string ITAXamountcomma = "0";

        decimal TOTDEDamount = 0;
        string TOTDEDamountcomma = "0";

        decimal NETPAYamount = 0;
        string NETPAYamountcomma = "0";

        decimal OTHOURamount = 0;
        string OTHOURamountcomma = "0";

        decimal BASICSALamount = 0;
        string BASICSALamountcomma = "0";

        decimal HOUSERENTamount = 0;
        string HOUSERENTamountcomma = "0";
        

        decimal TRANSPORTamount = 0;
        string TRANSPORTamountcomma = "0";

        decimal ALWDAILYamount = 0;
        string ALWDAILYamountcomma = "0";

        decimal BONUSPamount = 0;
        string BONUSPamountcomma = "0";

        decimal BONUSFamount = 0;
        string BONUSFamountcomma = "0";

        decimal INCENTIVEamount = 0;
        string INCENTIVEamountcomma = "0";

        decimal PFADDamount = 0;
        string PFADDamountcomma = "0";

        
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                try
                {
                    string uTp = HttpContext.Current.Session["USERTYPE"].ToString();
                  //  string brCD = HttpContext.Current.Session["BrCD"].ToString();

                    //if (uTp == "COMPADMIN")
                    //{
                        dbFunctions.lblAdd(@"SELECT COMPNM FROM ASL_COMPANY WHERE COMPID ='101'", lblCompNM);
                        dbFunctions.lblAdd(@"SELECT ADDRESS FROM ASL_COMPANY WHERE COMPID ='101'", lblAddress);
                        dbFunctions.lblAdd(@"SELECT CONTACTNO FROM ASL_COMPANY WHERE COMPID ='101'", lblContact);
                    //}
                    //else
                    //{
                    //    dbFunctions.lblAdd(@"SELECT COMPNM FROM ASL_COMPANY WHERE COMPID ='" + brCD + "'", lblCompNM);
                    //    dbFunctions.lblAdd(@"SELECT ADDRESS FROM ASL_COMPANY WHERE COMPID ='" + brCD + "'", lblAddress);
                    //    dbFunctions.lblAdd(@"SELECT CONTACTNO FROM ASL_COMPANY WHERE COMPID ='" + brCD + "'", lblContact);
                    //}

                    lblheadmn.Text = "EMPLOYEE SALARY SHEET";

                    var printDate =dbFunctions.Timezone(DateTime.Now);
                    string td = printDate.ToString("dd-MMM-yyyy hh:mm tt");
                    lblTime.Text = td;

                    ShowGrid();
                }
                catch (Exception)
                {

                    throw;
                }
            }
        }
        public void ShowGrid()
        {
            SqlConnection conn = new SqlConnection(dbFunctions.connection);


            var monthyear = Session["MonthYear"].ToString();
            var branchid = Session["BranchId"].ToString();
            var branchNm = Session["BranchName"].ToString();
            var deptid = Session["DeptId"].ToString();
            var dptName = Session["DeptName"].ToString();

            lblStore.Text = branchNm.ToUpper();
            lbldtp.Text = dptName.ToUpper();

            DateTime dateFR = DateTime.Parse(monthyear, dateformat, System.Globalization.DateTimeStyles.AssumeLocal);
            lblMonth.Text = dateFR.ToString("MMMM yyyy").ToUpper();
            conn.Open();

            var query = "";

            if (branchid == "ALL" && deptid == "ALL")
            {
                query = @"SELECT        HR_EMP.EMPNM, HR_SALPAY.BASICSAL, HR_SALPAY.HOUSERENT, HR_SALPAY.MEDICAL, HR_SALPAY.TRANSPORT, HR_SALPAY.GROSSSAL, CAST(HR_SALPAY.DAYTOT AS INT) DAYTOT, 
                            CAST(HR_SALPAY.DAYWORK AS INT) DAYWORK, CAST(HR_SALPAY.DAYHOLI AS INT) DAYHOLI, 
                            CAST(HR_SALPAY.DAYPRE AS INT) DAYPRE, CAST(HR_SALPAY.DAYLEAVE AS INT) DAYLEAVE, CAST(HR_SALPAY.DAYABS AS INT) DAYABS, CAST(HR_SALPAY.OTDAY AS INT) OTDAY, 
                            CAST(HR_SALPAY.OTHOUR AS INT) OTHOUR, HR_SALPAY.ALWDAILYNW, HR_SALPAY.ALWDAILYOT, HR_SALPAY.BONUSP, HR_SALPAY.BONUSF, HR_SALPAY.INCENTIVE, 
                            HR_SALPAY.PFADD, HR_SALPAY.CONVEY, HR_SALPAY.MOBILE, HR_SALPAY.DUEADJ, HR_SALPAY.ALWDAILYOT, HR_SALPAY.TOTADD, HR_SALPAY.ADVANCE, HR_SALPAY.PFDED, HR_SALPAY.STAMP, 
                            HR_SALPAY.AMTOTDAYS, HR_SALPAY.AMTOTHOUR, HR_SALPAY.ABSDED, HR_SALPAY.FOODING,HR_SALPAY.FOODINGADD,HR_SALPAY.BONUSP,HR_SALPAY.ITAX, HR_SALPAY.FINEADJ, HR_SALPAY.TOTDED, HR_SALPAY.NETPAY, HR_POST.POSTNM
                            FROM HR_SALPAY INNER JOIN
                            HR_POST ON HR_SALPAY.COMPID = HR_POST.COMPID AND HR_SALPAY.POSTID = HR_POST.POSTID INNER JOIN
                            HR_EMP ON HR_SALPAY.EMPID = HR_EMP.EMPID
                            WHERE (HR_SALPAY.TRANSMY = '" + monthyear + "') ORDER BY  HR_EMP.COSTPID, HR_EMP.EMPID";
            }
            else if (branchid != "ALL" && deptid == "ALL")
            {
                query = @"SELECT        HR_EMP.EMPNM, HR_SALPAY.BASICSAL, HR_SALPAY.HOUSERENT, HR_SALPAY.MEDICAL, HR_SALPAY.TRANSPORT, HR_SALPAY.GROSSSAL, CAST(HR_SALPAY.DAYTOT AS INT) DAYTOT, 
                        CAST(HR_SALPAY.DAYWORK AS INT) DAYWORK, CAST(HR_SALPAY.DAYHOLI AS INT) DAYHOLI, 
                        CAST(HR_SALPAY.DAYPRE AS INT) DAYPRE, CAST(HR_SALPAY.DAYLEAVE AS INT) DAYLEAVE, CAST(HR_SALPAY.DAYABS AS INT) DAYABS, CAST(HR_SALPAY.OTDAY AS INT) OTDAY, 
                        CAST(HR_SALPAY.OTHOUR AS INT) OTHOUR, HR_SALPAY.ALWDAILYNW, HR_SALPAY.ALWDAILYOT, HR_SALPAY.BONUSP, HR_SALPAY.BONUSF, HR_SALPAY.INCENTIVE, 
                        HR_SALPAY.PFADD, HR_SALPAY.CONVEY, HR_SALPAY.MOBILE, HR_SALPAY.DUEADJ, HR_SALPAY.ALWDAILYOT, HR_SALPAY.TOTADD, HR_SALPAY.ADVANCE, HR_SALPAY.PFDED, HR_SALPAY.STAMP, 
                        HR_SALPAY.AMTOTDAYS, HR_SALPAY.AMTOTHOUR, HR_SALPAY.ABSDED, HR_SALPAY.FOODING,HR_SALPAY.FOODINGADD,HR_SALPAY.ITAX, HR_SALPAY.FINEADJ, HR_SALPAY.TOTDED, HR_SALPAY.NETPAY, HR_POST.POSTNM
                        FROM HR_SALPAY INNER JOIN
                        HR_POST ON HR_SALPAY.COMPID = HR_POST.COMPID AND HR_SALPAY.POSTID = HR_POST.POSTID INNER JOIN
                        HR_EMP ON HR_SALPAY.EMPID = HR_EMP.EMPID
                         WHERE (HR_SALPAY.TRANSMY = '" + monthyear + "') AND (HR_SALPAY.BRANCHCD = '" + branchid + "') ORDER BY  HR_EMP.EMPID";
            }
            else if (branchid == "ALL" && deptid != "ALL")
            {
                query = @"SELECT        HR_EMP.EMPNM, HR_SALPAY.BASICSAL, HR_SALPAY.HOUSERENT, HR_SALPAY.MEDICAL, HR_SALPAY.TRANSPORT, HR_SALPAY.GROSSSAL, CAST(HR_SALPAY.DAYTOT AS INT) DAYTOT, 
                        CAST(HR_SALPAY.DAYWORK AS INT) DAYWORK, CAST(HR_SALPAY.DAYHOLI AS INT) DAYHOLI, 
                        CAST(HR_SALPAY.DAYPRE AS INT) DAYPRE, CAST(HR_SALPAY.DAYLEAVE AS INT) DAYLEAVE, CAST(HR_SALPAY.DAYABS AS INT) DAYABS, CAST(HR_SALPAY.OTDAY AS INT) OTDAY, 
                        CAST(HR_SALPAY.OTHOUR AS INT) OTHOUR, HR_SALPAY.ALWDAILYNW, HR_SALPAY.ALWDAILYOT, HR_SALPAY.BONUSP, HR_SALPAY.BONUSF, HR_SALPAY.INCENTIVE, 
                        HR_SALPAY.PFADD, HR_SALPAY.CONVEY, HR_SALPAY.MOBILE, HR_SALPAY.DUEADJ, HR_SALPAY.ALWDAILYOT, HR_SALPAY.TOTADD, HR_SALPAY.ADVANCE, HR_SALPAY.PFDED, HR_SALPAY.STAMP, 
                        HR_SALPAY.AMTOTDAYS, HR_SALPAY.AMTOTHOUR, HR_SALPAY.ABSDED, HR_SALPAY.FOODING,HR_SALPAY.FOODINGADD,HR_SALPAY.ITAX, HR_SALPAY.FINEADJ, HR_SALPAY.TOTDED, HR_SALPAY.NETPAY, HR_POST.POSTNM
                        FROM HR_SALPAY INNER JOIN
                        HR_POST ON HR_SALPAY.COMPID = HR_POST.COMPID AND HR_SALPAY.POSTID = HR_POST.POSTID INNER JOIN
                        HR_EMP ON HR_SALPAY.EMPID = HR_EMP.EMPID
                        WHERE (HR_SALPAY.TRANSMY = '" + monthyear + "') AND (HR_EMP.DEPTID = '" + deptid + "') ORDER BY  HR_EMP.EMPID";
            }
            else
            {
                query = @"SELECT        HR_EMP.EMPNM, HR_SALPAY.BASICSAL, HR_SALPAY.HOUSERENT, HR_SALPAY.MEDICAL, HR_SALPAY.TRANSPORT, HR_SALPAY.GROSSSAL, CAST(HR_SALPAY.DAYTOT AS INT) DAYTOT, 
                        CAST(HR_SALPAY.DAYWORK AS INT) DAYWORK, CAST(HR_SALPAY.DAYHOLI AS INT) DAYHOLI, 
                        CAST(HR_SALPAY.DAYPRE AS INT) DAYPRE, CAST(HR_SALPAY.DAYLEAVE AS INT) DAYLEAVE, CAST(HR_SALPAY.DAYABS AS INT) DAYABS, CAST(HR_SALPAY.OTDAY AS INT) OTDAY, 
                        CAST(HR_SALPAY.OTHOUR AS INT) OTHOUR, HR_SALPAY.ALWDAILYNW, HR_SALPAY.ALWDAILYOT, HR_SALPAY.BONUSP, HR_SALPAY.BONUSF, HR_SALPAY.INCENTIVE, 
                        HR_SALPAY.PFADD, HR_SALPAY.CONVEY, HR_SALPAY.MOBILE, HR_SALPAY.DUEADJ, HR_SALPAY.ALWDAILYOT, HR_SALPAY.TOTADD, HR_SALPAY.ADVANCE, HR_SALPAY.PFDED, HR_SALPAY.STAMP, 
                        HR_SALPAY.AMTOTDAYS, HR_SALPAY.AMTOTHOUR, HR_SALPAY.ABSDED, HR_SALPAY.FOODING,HR_SALPAY.FOODINGADD,HR_SALPAY.ITAX, HR_SALPAY.FINEADJ, HR_SALPAY.TOTDED, HR_SALPAY.NETPAY, HR_POST.POSTNM
                        FROM HR_SALPAY INNER JOIN
                        HR_POST ON HR_SALPAY.COMPID = HR_POST.COMPID AND HR_SALPAY.POSTID = HR_POST.POSTID INNER JOIN
                        HR_EMP ON HR_SALPAY.EMPID = HR_EMP.EMPID
                        WHERE (HR_SALPAY.TRANSMY = '" + monthyear + "') AND (HR_EMP.DEPTID = '" + deptid + "') AND (HR_SALPAY.BRANCHCD = '" + branchid + "') ORDER BY  HR_EMP.EMPID";
            }
            var cmd = new SqlCommand(query, conn);

            cmd.Parameters.Clear();
            cmd.Parameters.AddWithValue("@monthyear", monthyear);
            cmd.Parameters.AddWithValue("@branchid", branchid);

            var da = new SqlDataAdapter(cmd);
            var ds = new DataSet();
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
                var empnm = DataBinder.Eval(e.Row.DataItem, "EMPNM").ToString();
                e.Row.Cells[0].Text = "&nbsp;" + empnm;

                var postnm = DataBinder.Eval(e.Row.DataItem, "POSTNM").ToString();
                e.Row.Cells[1].Text = postnm;
                
                decimal BASICSAL = Convert.ToDecimal(DataBinder.Eval(e.Row.DataItem, "BASICSAL").ToString());
                string BASICSALString = SpellAmount.comma(BASICSAL);
                e.Row.Cells[2].Text = BASICSALString;

                decimal HOUSERENT = Convert.ToDecimal(DataBinder.Eval(e.Row.DataItem, "HOUSERENT").ToString());
                string HOUSERENTString = SpellAmount.comma(HOUSERENT);
                e.Row.Cells[3].Text = HOUSERENTString;

                decimal MEDICAL = Convert.ToDecimal(DataBinder.Eval(e.Row.DataItem, "MEDICAL").ToString());
                string MEDICALString = SpellAmount.comma(MEDICAL);
                e.Row.Cells[4].Text = MEDICALString;

                decimal TRANSPORT = Convert.ToDecimal(DataBinder.Eval(e.Row.DataItem, "TRANSPORT").ToString());
                string TRANSPORTString = SpellAmount.comma(TRANSPORT);
                e.Row.Cells[5].Text = TRANSPORTString;

                decimal STAMP = Convert.ToDecimal(DataBinder.Eval(e.Row.DataItem, "STAMP").ToString());
                string STAMPString = SpellAmount.comma(STAMP);
                e.Row.Cells[6].Text = STAMPString;

                decimal GROSSSAL = Convert.ToDecimal(DataBinder.Eval(e.Row.DataItem, "GROSSSAL").ToString());
                string GROSSSALString = SpellAmount.comma(GROSSSAL);
                e.Row.Cells[7].Text = GROSSSALString;



                decimal INCENTIVE = Convert.ToDecimal(DataBinder.Eval(e.Row.DataItem, "INCENTIVE").ToString());
                string INCENTIVEString = SpellAmount.comma(INCENTIVE);
                e.Row.Cells[8].Text = INCENTIVEString;

                decimal FOODINGADD = Convert.ToDecimal(DataBinder.Eval(e.Row.DataItem, "FOODINGADD").ToString());
                string FOODINGADDString = SpellAmount.comma(FOODINGADD);
                e.Row.Cells[9].Text = FOODINGADDString;

                decimal BONUSP = Convert.ToDecimal(DataBinder.Eval(e.Row.DataItem, "BONUSP").ToString());
                string BONUSPString = SpellAmount.comma(BONUSP);
                e.Row.Cells[10].Text = BONUSPString;

                decimal OTHOUR = Convert.ToDecimal(DataBinder.Eval(e.Row.DataItem, "OTHOUR").ToString());
                string OTHOURString = SpellAmount.comma(OTHOUR);
                e.Row.Cells[11].Text = OTHOURString;

                decimal AMTOTHOUR = Convert.ToDecimal(DataBinder.Eval(e.Row.DataItem, "AMTOTHOUR").ToString());
                string AMTOTHOURString = SpellAmount.comma(AMTOTHOUR);
                e.Row.Cells[12].Text = AMTOTHOURString;

                decimal TOTADD = Convert.ToDecimal(DataBinder.Eval(e.Row.DataItem, "TOTADD").ToString());
                string TOTADDString = SpellAmount.comma(TOTADD);
                e.Row.Cells[13].Text = TOTADDString;

                


                decimal ABSDED = Convert.ToDecimal(DataBinder.Eval(e.Row.DataItem, "ABSDED").ToString());
                string ABSDEDString = SpellAmount.comma(ABSDED);
                e.Row.Cells[14].Text = ABSDEDString;

                decimal ADVANCE = Convert.ToDecimal(DataBinder.Eval(e.Row.DataItem, "ADVANCE").ToString());
                string ADVANCEString = SpellAmount.comma(ADVANCE);
                e.Row.Cells[15].Text = ADVANCEString;

                decimal FOODING = Convert.ToDecimal(DataBinder.Eval(e.Row.DataItem, "FOODING").ToString());
                string FOODINGString = SpellAmount.comma(FOODING);
                e.Row.Cells[16].Text = FOODINGString;

                decimal FINEADJ = Convert.ToDecimal(DataBinder.Eval(e.Row.DataItem, "FINEADJ").ToString());
                string FINEADJString = SpellAmount.comma(FINEADJ);
                e.Row.Cells[17].Text = FINEADJString;

                

                decimal ITAX = Convert.ToDecimal(DataBinder.Eval(e.Row.DataItem, "ITAX").ToString());
                string ITAXString = SpellAmount.comma(ITAX);
                e.Row.Cells[18].Text = ITAXString;

                decimal TOTDED = Convert.ToDecimal(DataBinder.Eval(e.Row.DataItem, "TOTDED").ToString());
                string TOTDEDString = SpellAmount.comma(TOTDED);
                e.Row.Cells[19].Text = TOTDEDString;

                decimal NETPAY = Convert.ToDecimal(DataBinder.Eval(e.Row.DataItem, "NETPAY").ToString());
                string NETPAYString = SpellAmount.comma(NETPAY);
                e.Row.Cells[20].Text = NETPAYString;


                
                GROSSSALamount += GROSSSAL;
                GROSSSALamountcomma = SpellAmount.comma(GROSSSALamount);
                BASICSALamount += BASICSAL;
                BASICSALamountcomma = SpellAmount.comma(BASICSALamount);
                HOUSERENTamount += HOUSERENT;
                HOUSERENTamountcomma = SpellAmount.comma(HOUSERENTamount);
                MEDICALamount += MEDICAL;
                MEDICALamountcomma = SpellAmount.comma(MEDICALamount);
                TRANSPORTamount += TRANSPORT;
                TRANSPORTamountcomma = SpellAmount.comma(TRANSPORTamount);
                INCENTIVEamount += INCENTIVE;
                INCENTIVEamountcomma = SpellAmount.comma(INCENTIVEamount);
                FOODINGADDamount += FOODINGADD;
                FOODINGADDamountcomma = SpellAmount.comma(FOODINGADDamount);
                BONUSPamount += BONUSP;
                BONUSPamountcomma = SpellAmount.comma(BONUSPamount);
                OTHOURamount += OTHOUR;
                OTHOURamountcomma = SpellAmount.comma(OTHOURamount);
                AMTOTHOURamount += AMTOTHOUR;
                AMTOTHOURamountcomma = SpellAmount.comma(AMTOTHOURamount);
                STAMPamount += STAMP;
                STAMPamountcomma = SpellAmount.comma(STAMPamount);
                TOTADDamount += TOTADD;
                TOTADDamountcomma = SpellAmount.comma(TOTADDamount);

                
                ABSDEDamount += ABSDED;
                ABSDEDamountcomma = SpellAmount.comma(ABSDEDamount);
                ADVANCEamount += ADVANCE;
                ADVANCEamountcomma = SpellAmount.comma(ADVANCEamount);
                FOODINGamount += FOODING;
                FOODINGamountcomma = SpellAmount.comma(FOODINGamount);
                FINEADJamount += FINEADJ;
                FINEADJamountcomma = SpellAmount.comma(FINEADJamount);
                ITAXamount += ITAX;
                ITAXamountcomma = SpellAmount.comma(ITAXamount);
                TOTDEDamount += TOTDED;
                TOTDEDamountcomma = SpellAmount.comma(TOTDEDamount);
                NETPAYamount += NETPAY;
                NETPAYamountcomma = SpellAmount.comma(NETPAYamount);

            }
            else if (e.Row.RowType == DataControlRowType.Footer)
            {
                e.Row.Cells[1].Text = "Total";
                e.Row.Cells[1].HorizontalAlign = HorizontalAlign.Right;
                e.Row.Cells[2].Text = BASICSALamountcomma;
                e.Row.Cells[2].HorizontalAlign = HorizontalAlign.Right;
                e.Row.Cells[3].Text = HOUSERENTamountcomma;
                e.Row.Cells[3].HorizontalAlign = HorizontalAlign.Right;
                e.Row.Cells[4].Text = MEDICALamountcomma;
                e.Row.Cells[4].HorizontalAlign = HorizontalAlign.Right;
                e.Row.Cells[5].Text = TRANSPORTamountcomma;
                e.Row.Cells[5].HorizontalAlign = HorizontalAlign.Right;
                e.Row.Cells[6].Text = STAMPamountcomma;
                e.Row.Cells[6].HorizontalAlign = HorizontalAlign.Right;
                e.Row.Cells[7].Text = GROSSSALamountcomma;
                e.Row.Cells[7].HorizontalAlign = HorizontalAlign.Right;

                e.Row.Cells[8].Text = INCENTIVEamountcomma;
                e.Row.Cells[8].HorizontalAlign = HorizontalAlign.Right;

                e.Row.Cells[9].Text = FOODINGADDamountcomma;
                e.Row.Cells[9].HorizontalAlign = HorizontalAlign.Right;
                e.Row.Cells[10].Text = BONUSPamountcomma;
                e.Row.Cells[10].HorizontalAlign = HorizontalAlign.Right;
                e.Row.Cells[11].Text = OTHOURamountcomma;
                e.Row.Cells[11].HorizontalAlign = HorizontalAlign.Right;
                e.Row.Cells[12].Text = AMTOTHOURamountcomma;
                e.Row.Cells[12].HorizontalAlign = HorizontalAlign.Right;

                

                e.Row.Cells[13].Text = TOTADDamountcomma;
                e.Row.Cells[13].HorizontalAlign = HorizontalAlign.Right;

                e.Row.Cells[14].Text = ABSDEDamountcomma;
                e.Row.Cells[14].HorizontalAlign = HorizontalAlign.Right;
                e.Row.Cells[15].Text = ADVANCEamountcomma;
                e.Row.Cells[15].HorizontalAlign = HorizontalAlign.Right;
                e.Row.Cells[16].Text = FOODINGamountcomma;
                e.Row.Cells[16].HorizontalAlign = HorizontalAlign.Right;
                e.Row.Cells[17].Text = FINEADJamountcomma;
                e.Row.Cells[17].HorizontalAlign = HorizontalAlign.Right;
                e.Row.Cells[18].Text = ITAXamountcomma;
                e.Row.Cells[18].HorizontalAlign = HorizontalAlign.Right;
                e.Row.Cells[19].Text = TOTDEDamountcomma;
                e.Row.Cells[19].HorizontalAlign = HorizontalAlign.Right;
                e.Row.Cells[20].Text = NETPAYamountcomma;
                e.Row.Cells[20].HorizontalAlign = HorizontalAlign.Right;


            }

            ShowHeader(GridView1);
        }

        private void ShowHeader(GridView grid)
        {
            if (grid.Rows.Count > 0)
            {
                grid.UseAccessibleHeader = true;
                grid.HeaderRow.TableSection = TableRowSection.TableHeader;
            }
        }


    }
}