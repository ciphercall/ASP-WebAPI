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
    public partial class rptSalaryAdditionDeduction : System.Web.UI.Page
    {
        IFormatProvider dateformat = new System.Globalization.CultureInfo("fr-FR", true);
        
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

        decimal CONVEYamount = 0;
        string CONVEYamountcomma = "0";

        decimal MOBILEamount = 0;
        string MOBILEamountcomma = "0";

        decimal DUEADJamount = 0;
        string DUEADJamountcomma = "0";

        decimal COMMISSIONamount = 0;
        string COMMISSIONamountcomma = "0";

        decimal TOTADDamount = 0;
        string TOTADDamountcomma = "0";

        decimal ADVANCEamount = 0;
        string ADVANCEamountcomma = "0";

        decimal PFDEDamount = 0;
        string PFDEDamountcomma = "0";

        decimal STAMPamount = 0;
        string STAMPamountcomma = "0";

        decimal FOODINGamount = 0;
        string FOODINGamountcomma = "0";

        decimal FINEamount = 0;
        string FINEamountcomma = "0";

        decimal PAYADJamount = 0;
        string PAYADJamountcomma = "0";
        
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                try
                {
                    string uTp = HttpContext.Current.Session["USERTYPE"].ToString();
                   // string brCD = HttpContext.Current.Session["BrCD"].ToString();

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

                    lblheadmn.Text = "SALARY ADDITION DEDUCTION SHEET";

                    var printDate = dbFunctions.Timezone(DateTime.Now);
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
            lblStore.Text = branchNm.ToUpper();

            DateTime dateFR = DateTime.Parse(monthyear, dateformat, System.Globalization.DateTimeStyles.AssumeLocal);
            lblMonth.Text = dateFR.ToString("MMMM yyyy").ToUpper();
            conn.Open();

            var query = "";

            if (branchid == "ALL")
            {
                query = @"SELECT HR_SALDRCR.COMPID, HR_SALDRCR.EMPID, HR_EMP.EMPNM, HR_SALDRCR.TRANSMY, 
            HR_SALDRCR.ALWDAILY, HR_SALDRCR.BONUSP, HR_SALDRCR.BONUSF, HR_SALDRCR.INCENTIVE, HR_SALDRCR.PFADD, 
            HR_SALDRCR.CONVEY, HR_SALDRCR.MOBILE, HR_SALDRCR.DUEADJ, HR_SALDRCR.COMMISSION, HR_SALDRCR.ADVANCE, 
            HR_SALDRCR.PFDED, HR_SALDRCR.STAMP, HR_SALDRCR.FOODING, HR_SALDRCR.FINE, HR_SALDRCR.PAYADJ
            FROM HR_SALDRCR INNER JOIN HR_EMP ON HR_SALDRCR.EMPID = HR_EMP.EMPID 
            WHERE  HR_SALDRCR.TRANSMY='" + monthyear + "' ORDER BY HR_EMP.EMPNM";
            }
            else
            {
                query = @"SELECT HR_SALDRCR.COMPID, HR_SALDRCR.EMPID, HR_EMP.EMPNM, HR_SALDRCR.TRANSMY, 
            HR_SALDRCR.ALWDAILY, HR_SALDRCR.BONUSP, HR_SALDRCR.BONUSF, HR_SALDRCR.INCENTIVE, HR_SALDRCR.PFADD, 
            HR_SALDRCR.CONVEY, HR_SALDRCR.MOBILE, HR_SALDRCR.DUEADJ, HR_SALDRCR.COMMISSION, HR_SALDRCR.ADVANCE, 
            HR_SALDRCR.PFDED, HR_SALDRCR.STAMP, HR_SALDRCR.FOODING, HR_SALDRCR.FINE, HR_SALDRCR.PAYADJ
            FROM HR_SALDRCR INNER JOIN HR_EMP ON HR_SALDRCR.EMPID = HR_EMP.EMPID 
            WHERE  HR_SALDRCR.TRANSMY='" + monthyear + "' AND HR_SALDRCR.BRANCHCD='" + branchid + "' ORDER BY HR_EMP.EMPNM";
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


                decimal ALWDAILY = Convert.ToDecimal(DataBinder.Eval(e.Row.DataItem, "ALWDAILY").ToString());
                string ALWDAILYString = SpellAmount.comma(ALWDAILY);
                e.Row.Cells[1].Text = ALWDAILYString;

                decimal BONUSP = Convert.ToDecimal(DataBinder.Eval(e.Row.DataItem, "BONUSP").ToString());
                string BONUSPString = SpellAmount.comma(BONUSP);
                e.Row.Cells[2].Text = BONUSPString;

                decimal BONUSF = Convert.ToDecimal(DataBinder.Eval(e.Row.DataItem, "BONUSF").ToString());
                string BONUSFString = SpellAmount.comma(BONUSF);
                e.Row.Cells[3].Text = BONUSFString;

                decimal INCENTIVE = Convert.ToDecimal(DataBinder.Eval(e.Row.DataItem, "INCENTIVE").ToString());
                string INCENTIVEString = SpellAmount.comma(INCENTIVE);
                e.Row.Cells[4].Text = INCENTIVEString;

                decimal PFADD = Convert.ToDecimal(DataBinder.Eval(e.Row.DataItem, "PFADD").ToString());
                string PFADDString = SpellAmount.comma(PFADD);
                e.Row.Cells[5].Text = PFADDString;

                decimal CONVEY = Convert.ToDecimal(DataBinder.Eval(e.Row.DataItem, "CONVEY").ToString());
                string CONVEYString = SpellAmount.comma(CONVEY);
                e.Row.Cells[6].Text = CONVEYString;

                decimal MOBILE = Convert.ToDecimal(DataBinder.Eval(e.Row.DataItem, "MOBILE").ToString());
                string MOBILEString = SpellAmount.comma(MOBILE);
                e.Row.Cells[7].Text = MOBILEString;

                decimal DUEADJ = Convert.ToDecimal(DataBinder.Eval(e.Row.DataItem, "DUEADJ").ToString());
                string DUEADJString = SpellAmount.comma(DUEADJ);
                e.Row.Cells[8].Text = DUEADJString;

                decimal COMMISSION = Convert.ToDecimal(DataBinder.Eval(e.Row.DataItem, "COMMISSION").ToString());
                string COMMISSIONString = SpellAmount.comma(COMMISSION);
                e.Row.Cells[9].Text = COMMISSIONString;
                

                decimal ADVANCE = Convert.ToDecimal(DataBinder.Eval(e.Row.DataItem, "ADVANCE").ToString());
                string ADVANCEString = SpellAmount.comma(ADVANCE);
                e.Row.Cells[10].Text = ADVANCEString;

                decimal PFDED = Convert.ToDecimal(DataBinder.Eval(e.Row.DataItem, "PFDED").ToString());
                string PFDEDString = SpellAmount.comma(PFDED);
                e.Row.Cells[11].Text = PFDEDString;

                decimal STAMP = Convert.ToDecimal(DataBinder.Eval(e.Row.DataItem, "STAMP").ToString());
                string STAMPString = SpellAmount.comma(STAMP);
                e.Row.Cells[12].Text = STAMPString;

                decimal FOODING = Convert.ToDecimal(DataBinder.Eval(e.Row.DataItem, "FOODING").ToString());
                string FOODINGString = SpellAmount.comma(FOODING);
                e.Row.Cells[13].Text = FOODINGString;

                decimal FINE = Convert.ToDecimal(DataBinder.Eval(e.Row.DataItem, "FINE").ToString());
                string FINEString = SpellAmount.comma(FINE);
                e.Row.Cells[14].Text = FINEString;

                decimal PAYADJ = Convert.ToDecimal(DataBinder.Eval(e.Row.DataItem, "PAYADJ").ToString());
                string PAYADJString = SpellAmount.comma(PAYADJ);
                e.Row.Cells[15].Text = PAYADJString;
                
                

                ALWDAILYamount += ALWDAILY;
                ALWDAILYamountcomma = SpellAmount.comma(ALWDAILYamount);

                BONUSPamount += BONUSP;
                BONUSPamountcomma = SpellAmount.comma(BONUSPamount);

                BONUSFamount += BONUSF;
                BONUSFamountcomma = SpellAmount.comma(BONUSFamount);

                INCENTIVEamount += INCENTIVE;
                INCENTIVEamountcomma = SpellAmount.comma(INCENTIVEamount);

                PFADDamount += PFADD;
                PFADDamountcomma = SpellAmount.comma(PFADDamount);

                CONVEYamount += CONVEY;
                CONVEYamountcomma = SpellAmount.comma(CONVEYamount);

                MOBILEamount += MOBILE;
                MOBILEamountcomma = SpellAmount.comma(MOBILEamount);

                DUEADJamount += DUEADJ;
                DUEADJamountcomma = SpellAmount.comma(DUEADJamount);

                COMMISSIONamount += COMMISSION;
                COMMISSIONamountcomma = SpellAmount.comma(COMMISSIONamount);
                

                ADVANCEamount += ADVANCE;
                ADVANCEamountcomma = SpellAmount.comma(ADVANCEamount);

                PFDEDamount += PFDED;
                PFDEDamountcomma = SpellAmount.comma(PFDEDamount);

                STAMPamount += STAMP;
                STAMPamountcomma = SpellAmount.comma(STAMPamount);

                FOODINGamount += FOODING;
                FOODINGamountcomma = SpellAmount.comma(FOODINGamount);

                FINEamount += FINE;
                FINEamountcomma = SpellAmount.comma(FINEamount);

                PAYADJamount += PAYADJ;
                PAYADJamountcomma = SpellAmount.comma(PAYADJamount);
                
                
            }
            else if (e.Row.RowType == DataControlRowType.Footer)
            {
                e.Row.Cells[0].Text = "Total";
                e.Row.Cells[0].HorizontalAlign = HorizontalAlign.Right;

               

                e.Row.Cells[1].Text = ALWDAILYamountcomma;
                e.Row.Cells[1].HorizontalAlign = HorizontalAlign.Right;

                e.Row.Cells[2].Text = BONUSPamountcomma;
                e.Row.Cells[2].HorizontalAlign = HorizontalAlign.Right;

                e.Row.Cells[3].Text = BONUSFamountcomma;
                e.Row.Cells[3].HorizontalAlign = HorizontalAlign.Right;

                e.Row.Cells[4].Text = INCENTIVEamountcomma;
                e.Row.Cells[4].HorizontalAlign = HorizontalAlign.Right;

                e.Row.Cells[5].Text = PFADDamountcomma;
                e.Row.Cells[5].HorizontalAlign = HorizontalAlign.Right;

                e.Row.Cells[6].Text = CONVEYamountcomma;
                e.Row.Cells[6].HorizontalAlign = HorizontalAlign.Right;

                e.Row.Cells[7].Text = MOBILEamountcomma;
                e.Row.Cells[7].HorizontalAlign = HorizontalAlign.Right;

                e.Row.Cells[8].Text = DUEADJamountcomma;
                e.Row.Cells[8].HorizontalAlign = HorizontalAlign.Right;

                e.Row.Cells[9].Text = COMMISSIONamountcomma;
                e.Row.Cells[9].HorizontalAlign = HorizontalAlign.Right;

                e.Row.Cells[10].Text = ADVANCEamountcomma;
                e.Row.Cells[10].HorizontalAlign = HorizontalAlign.Right;

                e.Row.Cells[11].Text = PFDEDamountcomma;
                e.Row.Cells[11].HorizontalAlign = HorizontalAlign.Right;

                e.Row.Cells[12].Text = STAMPamountcomma;
                e.Row.Cells[12].HorizontalAlign = HorizontalAlign.Right;

                e.Row.Cells[13].Text = FOODINGamountcomma;
                e.Row.Cells[13].HorizontalAlign = HorizontalAlign.Right;

                e.Row.Cells[14].Text = FINEamountcomma;
                e.Row.Cells[14].HorizontalAlign = HorizontalAlign.Right;

                e.Row.Cells[15].Text = PAYADJamountcomma;
                e.Row.Cells[15].HorizontalAlign = HorizontalAlign.Right;

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