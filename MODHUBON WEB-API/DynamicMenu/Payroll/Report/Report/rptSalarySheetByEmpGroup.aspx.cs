using AlchemyAccounting;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace DynamicMenu.Payroll.Report
{
    public partial class rptSalarySheetByEmpGroup : System.Web.UI.Page
    {
        IFormatProvider dateformat = new System.Globalization.CultureInfo("fr-FR", true);
        string strPreviousRowID_OP = string.Empty;
        string strPreviousRowID_MREC = string.Empty;
        string strPreviousRowID_MPAY = string.Empty;
        string strPreviousRowID_CL = string.Empty;

        int intSubTotalIndex_OP = 1;
        int intSubTotalIndex_MREC = 1;
        int intSubTotalIndex_MPAY = 1;
        int intSubTotalIndex_CL = 1;

        // To temporarily store Sub Total    
        private decimal dblSubTotalBasicAMT = 0;
        private decimal ddlSubtotalHouseRentAMT = 0;
        private decimal ddlSubTotalMadicalAMT = 0;
        private decimal ddlSubTotalConveyAMT = 0;
        private decimal ddlSubTotalOthersAMT = 0;
        private decimal ddlSubTotalGrossAMT = 0;
        private decimal ddlSubTotalIncentiveAMT = 0;
        private decimal ddlSubTotalFoodingAMT = 0;
        private decimal ddlSubTotalAttendanceAMT = 0;
        private decimal ddlSubTotalOthourAMT = 0;
        private decimal ddlSubTotalOtAmountAMT = 0;
        private decimal ddlSubTotaltotAdditionAMT = 0;
        private decimal ddlSubTotalAbsenseAMT = 0;
        private decimal ddlSubTotaladvanceAMT = 0;
        private decimal ddlSubTotalfooddedAMT = 0;
        private decimal ddlSubTotalfineAMT = 0;
        private decimal ddlSubTotalincometaxAMT = 0;
        private decimal ddlSubTotaltotDeductionAMT = 0;
        private decimal ddlSubTotalNetPayAMT = 0;

        // To temporarily store Grand Total    
        private decimal ddlGrandTotalBasicAMT = 0;
        private decimal ddlGrandTotalHouseRentAMT = 0;
        private decimal ddlGrandTotalMadicalAMT = 0;
        private decimal ddlGrandTotalConveyAMT = 0;
        private decimal ddlGrandTotalOthersAMT = 0;
        private decimal ddlGrandTotalGrossAMT = 0;
        private decimal ddlGrandTotalIncentiveAMT = 0;
        private decimal ddlGrandTotalFoodingAMT = 0;
        private decimal ddlGrandTotalAttandanceAMT = 0;
        private decimal ddlGrandTotalOthourAMT = 0;
        private decimal ddlGrandTotalOtAmountAMT = 0;
        private decimal ddlGrandTotaltotAdditionAMT = 0;
        private decimal ddlGrandTotalAbsenseAMT = 0;
        private decimal ddlGrandTotaladvanceAMT = 0;
        private decimal ddlGrandTotalfooddedAMT = 0;
        private decimal ddlGrandTotalfineAMT = 0;
        private decimal ddlGrandTotalincometaxAMT = 0;
        private decimal ddlGrandTotaltotdeductionAMT = 0;
        private decimal ddlGrandTotalNetPayAMT = 0;


        //string sub total AmountComma = "";
        private String ddlSubTotalBasicAMTComma = "0";
        private string ddlSubTotalHouseRentAMTComma = "0";
        private string ddlSubTotalMadicalAMTComma = "0";
        private string ddlSubTotalConveyComma = "0";
        private string ddlSubTotalOthersComma = "0";
        private string ddlSubTotalGrossComma = "0";
        private string ddlSubTotalIncentiveComma = "0";
        private string ddlSubTotalFoodingComma = "0";
        private string ddlSubTotalAttandanceComma = "0";
        private string ddlSubTotalOthourComma = "0";
        private string ddlSubTotalOtAmountComma = "0";
        private string ddlSubTotaltotadditionComma = "0";
        private string ddlSubTotalAbsenseComma = "0";
        private string ddlSubTotaladvanceComma = "0";
        private string ddlSubTotalfooddedComma = "0";
        private string ddlSubTotalfineComma = "0";
        private string ddlSubTotalincometaxComma = "0";
        private string ddlSubTotaltotDeductionComma = "0";
        private string ddlSubTotalNetPayComma = "0";



        //string grand total AmountComma = "";
        private string ddlGrandTotalBasicAMTComma = "0";
        private string ddlGrandTotalHouseRentAMTComma = "0";
        private string ddlGrandTotalMadicalComma = "0";
        private string ddlGrandTotalConveyComma = "0";
        private string ddlGrandTotalOthersComma = "0";
        private string ddlGrandTotalGrossComma = "0";
        private string ddlGrandTotalIncentiveComma = "0";
        private string ddlGrandTotalFoodingComma = "0";
        private string ddlGrandTotalAttandanceComma = "0";
        private string ddlGrandTotalOthourComma = "0";
        private string ddlGrandTotalOtAmountComma = "0";
        private string ddlGrandTotalTotadditionComma = "0";
        private string ddlGrandTotalAbsenseComma = "0";
        private string ddlGrandTotaladvanceComma = "0";
        private string ddlGrandTotalfooddedComma = "0";
        private string ddlGrandTotalfineComma = "0";
        private string ddlGrandTotalincometaxComma = "0";
        private string ddlGrandTotaltotDeductionComma = "0";
        private string ddlGrandTotalNetPayComma = "0";




        protected void Page_Load(object sender, EventArgs e)
        {

            if (!IsPostBack)
            {
                try
                {
                    string uTp = HttpContext.Current.Session["USERTYPE"].ToString();
                    dbFunctions.lblAdd(@"SELECT COMPNM FROM ASL_COMPANY WHERE COMPID ='101'", lblCompNM);
                    dbFunctions.lblAdd(@"SELECT ADDRESS FROM ASL_COMPANY WHERE COMPID ='101'", lblAddress);
                    dbFunctions.lblAdd(@"SELECT CONTACTNO FROM ASL_COMPANY WHERE COMPID ='101'", lblContact);
                    lblheadmn.Text = "SALARY SHEET";

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


            DateTime dateFR = DateTime.Parse(monthyear, dateformat, System.Globalization.DateTimeStyles.AssumeLocal);
            lblMonth.Text = dateFR.ToString("MMMM yyyy").ToUpper();
            conn.Open();

            SqlCommand cmd = new SqlCommand($@"SELECT ROW_NUMBER() OVER(ORDER BY DEPTNM)AS SL,  DEPTNM, HR_EMP.EMPNM, HR_SALPAY.BASICSAL, HR_SALPAY.HOUSERENT, HR_SALPAY.MEDICAL, HR_SALPAY.TRANSPORT, HR_SALPAY.GROSSSAL, CAST(HR_SALPAY.DAYTOT AS INT) DAYTOT, 
                                CAST(HR_SALPAY.DAYWORK AS INT) DAYWORK, CAST(HR_SALPAY.DAYHOLI AS INT) DAYHOLI, 
                                CAST(HR_SALPAY.DAYPRE AS INT) DAYPRE, CAST(HR_SALPAY.DAYLEAVE AS INT) DAYLEAVE, CAST(HR_SALPAY.DAYABS AS INT) DAYABS, CAST(HR_SALPAY.OTDAY AS INT) OTDAY, 
                                CAST(HR_SALPAY.OTHOUR AS INT) OTHOUR, HR_SALPAY.ALWDAILYNW, HR_SALPAY.ALWDAILYOT, HR_SALPAY.BONUSP, HR_SALPAY.BONUSF, HR_SALPAY.INCENTIVE, 
                                HR_SALPAY.PFADD, HR_SALPAY.CONVEY, HR_SALPAY.MOBILE, HR_SALPAY.DUEADJ, HR_SALPAY.ALWDAILYOT, HR_SALPAY.TOTADD, HR_SALPAY.ADVANCE, HR_SALPAY.PFDED, HR_SALPAY.STAMP, 
                                HR_SALPAY.AMTOTDAYS, HR_SALPAY.AMTOTHOUR, HR_SALPAY.ABSDED, HR_SALPAY.FOODING,HR_SALPAY.FOODINGADD,HR_SALPAY.BONUSP,HR_SALPAY.ITAX, HR_SALPAY.FINEADJ, HR_SALPAY.TOTDED, HR_SALPAY.NETPAY, HR_POST.POSTNM
                                FROM HR_SALPAY INNER JOIN HR_POST ON HR_SALPAY.COMPID = HR_POST.COMPID AND HR_SALPAY.POSTID = HR_POST.POSTID 
                                INNER JOIN HR_EMP ON HR_SALPAY.EMPID = HR_EMP.EMPID
                                INNER JOIN HR_DEPT ON HR_DEPT.DEPTID = HR_EMP.DEPTID
                                WHERE (HR_SALPAY.TRANSMY = '{monthyear}') ORDER BY  DEPTNM,HR_EMP.COSTPID, HR_EMP.EMPID", conn);



            SqlDataAdapter da = new SqlDataAdapter(cmd);
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
        protected void GridView1_RowCreated(object sender, GridViewRowEventArgs e)
        {
            bool IsSubTotalRowNeedToAdd_OP = false;
            bool IsGrandTotalRowNeedtoAdd_OP = false;
            if ((strPreviousRowID_OP != string.Empty) && (DataBinder.Eval(e.Row.DataItem, "DEPTNM") != null))
                if (strPreviousRowID_OP != DataBinder.Eval(e.Row.DataItem, "DEPTNM").ToString())
                    IsSubTotalRowNeedToAdd_OP = true;
            if ((strPreviousRowID_OP != string.Empty) && (DataBinder.Eval(e.Row.DataItem, "DEPTNM") == null))
            {
                IsSubTotalRowNeedToAdd_OP = true;
                IsGrandTotalRowNeedtoAdd_OP = true;
                intSubTotalIndex_OP = 0;
            }
            //#region Inserting first Row and populating fist Group Header details
            if ((strPreviousRowID_OP == string.Empty) && (DataBinder.Eval(e.Row.DataItem, "DEPTNM") != null))
            {
                GridView GridView1 = (GridView)sender;
                GridViewRow row = new GridViewRow(0, 0, DataControlRowType.DataRow, DataControlRowState.Insert);
                TableCell cell = new TableCell();
                cell.Text = "&nbsp;" + DataBinder.Eval(e.Row.DataItem, "DEPTNM").ToString(); //////// Sub Header Name
                cell.ColumnSpan = 24;
                cell.CssClass = "GroupHeaderStyle";
                row.Cells.Add(cell);
                GridView1.Controls[0].Controls.AddAt(e.Row.RowIndex + intSubTotalIndex_OP, row);
                intSubTotalIndex_OP++;
            }
            //#endregion
            if (IsSubTotalRowNeedToAdd_OP)
            {
                //#region Adding Sub Total Row
                GridView GridView1 = (GridView)sender;
                // Creating a Row          
                GridViewRow row = new GridViewRow(0, 0, DataControlRowType.DataRow, DataControlRowState.Insert);
                //Adding Total Cell          
                TableCell cell = new TableCell();
                cell.Text = "Sub Total : ";
                cell.HorizontalAlign = HorizontalAlign.Right;
                cell.ColumnSpan = 3;
                cell.CssClass = "SubTotalRowStyle";
                row.Cells.Add(cell);

                //Adding DRAMT Column         
                cell = new TableCell();
                cell.Text = string.Format("{0:0.00}", ddlSubTotalBasicAMTComma);
                cell.HorizontalAlign = HorizontalAlign.Right;
                cell.CssClass = "SubTotalRowStyle";
                row.Cells.Add(cell);

                //Adding CRAMT Column         
                cell = new TableCell();
                cell.Text = string.Format("{0:0.00}", ddlSubTotalHouseRentAMTComma);
                cell.HorizontalAlign = HorizontalAlign.Right;
                cell.CssClass = "SubTotalRowStyle";
                row.Cells.Add(cell);

                //Adding CRAMT Column         
                cell = new TableCell();
                cell.Text = string.Format("{0:0.00}", ddlSubTotalMadicalAMTComma);
                cell.HorizontalAlign = HorizontalAlign.Right;
                cell.CssClass = "SubTotalRowStyle";
                row.Cells.Add(cell);

                //Adding CRAMT Column         
                cell = new TableCell();
                cell.Text = string.Format("{0:0.00}", ddlSubTotalConveyComma);
                cell.HorizontalAlign = HorizontalAlign.Right;
                cell.CssClass = "SubTotalRowStyle";
                row.Cells.Add(cell);

                //Adding CRAMT Column         
                cell = new TableCell();
                cell.Text = string.Format("{0:0.00}", ddlSubTotalOthersComma);
                cell.HorizontalAlign = HorizontalAlign.Right;
                cell.CssClass = "SubTotalRowStyle";
                row.Cells.Add(cell);


                //Adding CRAMT Column         
                cell = new TableCell();
                cell.Text = string.Format("{0:0.00}", ddlSubTotalGrossComma);
                cell.HorizontalAlign = HorizontalAlign.Right;
                cell.CssClass = "SubTotalRowStyle";
                row.Cells.Add(cell);

                //Adding CRAMT Column         
                cell = new TableCell();
                cell.Text = string.Format("{0:0.00}", ddlSubTotalIncentiveComma);
                cell.HorizontalAlign = HorizontalAlign.Right;
                cell.CssClass = "SubTotalRowStyle";
                row.Cells.Add(cell);

                //Adding CRAMT Column         
                cell = new TableCell();
                cell.Text = string.Format("{0:0.00}", ddlSubTotalFoodingComma);
                cell.HorizontalAlign = HorizontalAlign.Right;
                cell.CssClass = "SubTotalRowStyle";
                row.Cells.Add(cell);

                //Adding CRAMT Column         
                cell = new TableCell();
                cell.Text = string.Format("{0:0.00}", ddlSubTotalAttandanceComma);
                cell.HorizontalAlign = HorizontalAlign.Right;
                cell.CssClass = "SubTotalRowStyle";
                row.Cells.Add(cell);

                //Adding CRAMT Column         
                cell = new TableCell();
                cell.Text = string.Format("{0:0.00}", ddlSubTotalOthourComma);
                cell.HorizontalAlign = HorizontalAlign.Right;
                cell.CssClass = "SubTotalRowStyle";
                row.Cells.Add(cell);

                //Adding CRAMT Column         
                cell = new TableCell();
                cell.Text = string.Format("{0:0.00}", ddlSubTotalOtAmountComma);
                cell.HorizontalAlign = HorizontalAlign.Right;
                cell.CssClass = "SubTotalRowStyle";
                row.Cells.Add(cell);

                //Adding CRAMT Column         
                cell = new TableCell();
                cell.Text = string.Format("{0:0.00}", ddlSubTotaltotadditionComma);
                cell.HorizontalAlign = HorizontalAlign.Right;
                cell.CssClass = "SubTotalRowStyle";
                row.Cells.Add(cell);

                //Adding CRAMT Column         
                cell = new TableCell();
                cell.Text = string.Format("{0:0.00}", ddlSubTotalAbsenseComma);
                cell.HorizontalAlign = HorizontalAlign.Right;
                cell.CssClass = "SubTotalRowStyle";
                row.Cells.Add(cell);

                //Adding CRAMT Column         
                cell = new TableCell();
                cell.Text = string.Format("{0:0.00}", ddlSubTotaladvanceComma);
                cell.HorizontalAlign = HorizontalAlign.Right;
                cell.CssClass = "SubTotalRowStyle";
                row.Cells.Add(cell);

                //Adding CRAMT Column         
                cell = new TableCell();
                cell.Text = string.Format("{0:0.00}", ddlSubTotalfooddedComma);
                cell.HorizontalAlign = HorizontalAlign.Right;
                cell.CssClass = "SubTotalRowStyle";
                row.Cells.Add(cell);

                //Adding CRAMT Column         
                cell = new TableCell();
                cell.Text = string.Format("{0:0.00}", ddlSubTotalfineComma);
                cell.HorizontalAlign = HorizontalAlign.Right;
                cell.CssClass = "SubTotalRowStyle";
                row.Cells.Add(cell);

                //Adding CRAMT Column         
                cell = new TableCell();
                cell.Text = string.Format("{0:0.00}", ddlSubTotalincometaxComma);
                cell.HorizontalAlign = HorizontalAlign.Right;
                cell.CssClass = "SubTotalRowStyle";
                row.Cells.Add(cell);

                //Adding CRAMT Column         
                cell = new TableCell();
                cell.Text = string.Format("{0:0.00}", ddlSubTotaltotDeductionComma);
                cell.HorizontalAlign = HorizontalAlign.Right;
                cell.CssClass = "SubTotalRowStyle";
                row.Cells.Add(cell);

                //Adding CRAMT Column         
                cell = new TableCell();
                cell.Text = string.Format("{0:0.00}", ddlSubTotalNetPayComma);
                cell.HorizontalAlign = HorizontalAlign.Right;
                cell.CssClass = "SubTotalRowStyle";
                row.Cells.Add(cell);

                //Adding CRAMT Column         
                cell = new TableCell();
                // cell.Text = string.Format("{0:0.00}", ddlSubTotalNetPayComma);
                cell.HorizontalAlign = HorizontalAlign.Right;
                cell.CssClass = "SubTotalRowStyle";
                row.Cells.Add(cell);

                //Adding the Row at the RowIndex position in the Grid      
                GridView1.Controls[0].Controls.AddAt(e.Row.RowIndex + intSubTotalIndex_OP, row);
                intSubTotalIndex_OP++;
                // #endregion 
                #region Adding Next Group Header Details
                if (DataBinder.Eval(e.Row.DataItem, "DEPTNM") != null)
                {
                    row = new GridViewRow(0, 0, DataControlRowType.DataRow, DataControlRowState.Insert);
                    cell = new TableCell();
                    cell.Text = "&nbsp;" + DataBinder.Eval(e.Row.DataItem, "DEPTNM").ToString(); //////// Sub Header Name
                    cell.ColumnSpan = 24;
                    cell.CssClass = "GroupHeaderStyle";
                    row.Cells.Add(cell);
                    GridView1.Controls[0].Controls.AddAt(e.Row.RowIndex + intSubTotalIndex_OP, row);
                    intSubTotalIndex_OP++;
                }
                #endregion
                #region Reseting the Sub Total Variables
                // ddlGrandTotalBasicAMT = 0;
                //ddlGrandTotalHouseRentAMT = 0;
                //ddlGrandTotalMadicalAMT = 0;
                //ddlGrandTotalConveyAMT = 0;
                //ddlGrandTotalOthersAMT = 0;
                //ddlGrandTotalGrossAMT = 0;
                //ddlGrandTotaltotAdditionAMT = 0;
                //ddlGrandTotaltotdeductionAMT = 0;
                //ddlGrandTotalNetPayAMT = 0;
                //ddlGrandTotalIncentiveAMT = 0;

                dblSubTotalBasicAMT = 0;
                ddlSubtotalHouseRentAMT = 0;
                ddlSubTotalMadicalAMT = 0;
                ddlSubTotalConveyAMT = 0;
                ddlSubTotalOthersAMT = 0;
                ddlSubTotalGrossAMT = 0;
                ddlSubTotalIncentiveAMT = 0;
                ddlSubTotalFoodingAMT = 0;
                ddlSubTotalAttendanceAMT = 0;
                ddlSubTotalOthourAMT = 0;
                ddlSubTotalOtAmountAMT = 0;
                ddlSubTotaltotAdditionAMT = 0;
                ddlSubTotalAbsenseAMT = 0;
                ddlSubTotaladvanceAMT = 0;
                ddlSubTotalfooddedAMT = 0;
                ddlSubTotalfineAMT = 0;
                ddlSubTotalincometaxAMT = 0;
                ddlSubTotaltotDeductionAMT = 0;
                ddlSubTotalNetPayAMT = 0;

                #endregion
            }
            if (IsGrandTotalRowNeedtoAdd_OP)
            {
                ////#region Grand Total Row
                //GridView GridView1 = (GridView)sender;
                //// Creating a Row      
                //GridViewRow row = new GridViewRow(0, 0, DataControlRowType.DataRow, DataControlRowState.Insert);
                ////Adding Total Cell           
                //TableCell cell = new TableCell();
                //cell.Text = " Grand Total : ";
                //cell.HorizontalAlign = HorizontalAlign.Right;
                //cell.ColumnSpan = 3;
                //cell.CssClass = "GrandTotalRowStyle";
                //row.Cells.Add(cell);

                ////Adding DRAMT Column          
                //cell = new TableCell();
                ////cell.Text = string.Format("{0:0.00}", ddlGrandTotalBasicAMTComma);
                //cell.HorizontalAlign = HorizontalAlign.Right;
                //cell.CssClass = "GrandTotalRowStyle";
                //row.Cells.Add(cell);

                ////Adding CRAMT Column          
                //cell = new TableCell();
                ////cell.Text = string.Format("{0:0.00}", ddlGrandTotalHouseRentAMTComma);
                //cell.HorizontalAlign = HorizontalAlign.Right;
                //cell.CssClass = "GrandTotalRowStyle";
                //row.Cells.Add(cell);

                ////Adding CRAMT Column          
                //cell = new TableCell();
                //// cell.Text = string.Format("{0:0.00}", ddlGrandTotalMadicalComma);
                //cell.HorizontalAlign = HorizontalAlign.Right;
                //cell.CssClass = "GrandTotalRowStyle";
                //row.Cells.Add(cell);

                ////Adding CRAMT Column          
                //cell = new TableCell();
                ////cell.Text = string.Format("{0:0.00}", ddlGrandTotalConveyComma);
                //cell.HorizontalAlign = HorizontalAlign.Right;
                //cell.CssClass = "GrandTotalRowStyle";
                //row.Cells.Add(cell);

                ////Adding CRAMT Column          
                //cell = new TableCell();
                //// cell.Text = string.Format("{0:0.00}", ddlGrandTotalOthersComma);
                //cell.HorizontalAlign = HorizontalAlign.Right;
                //cell.CssClass = "GrandTotalRowStyle";
                //row.Cells.Add(cell);

                ////Adding CRAMT Column          
                //cell = new TableCell();
                //cell.Text = string.Format("{0:0.00}", ddlGrandTotalGrossComma);
                //cell.HorizontalAlign = HorizontalAlign.Right;
                //cell.CssClass = "GrandTotalRowStyle";
                //row.Cells.Add(cell);

                ////Adding CRAMT Column          
                //cell = new TableCell();
                //// cell.Text = string.Format("{0:0.00}", ddlGrandTotalOthersComma);
                //cell.HorizontalAlign = HorizontalAlign.Right;
                //cell.CssClass = "GrandTotalRowStyle";
                //row.Cells.Add(cell);

                ////Adding CRAMT Column          
                //cell = new TableCell();
                //// cell.Text = string.Format("{0:0.00}", ddlGrandTotalOthersComma);
                //cell.HorizontalAlign = HorizontalAlign.Right;
                //cell.CssClass = "GrandTotalRowStyle";
                //row.Cells.Add(cell);

                ////Adding CRAMT Column          
                //cell = new TableCell();
                //// cell.Text = string.Format("{0:0.00}", ddlGrandTotalOthersComma);
                //cell.HorizontalAlign = HorizontalAlign.Right;
                //cell.CssClass = "GrandTotalRowStyle";
                //row.Cells.Add(cell);

                ////Adding CRAMT Column          
                //cell = new TableCell();
                //// cell.Text = string.Format("{0:0.00}", ddlGrandTotalOthersComma);
                //cell.HorizontalAlign = HorizontalAlign.Right;
                //cell.CssClass = "GrandTotalRowStyle";
                //row.Cells.Add(cell);

                ////Adding CRAMT Column          
                //cell = new TableCell();
                //// cell.Text = string.Format("{0:0.00}", ddlGrandTotalOthersComma);
                //cell.HorizontalAlign = HorizontalAlign.Right;
                //cell.CssClass = "GrandTotalRowStyle";
                //row.Cells.Add(cell);

                ////Adding CRAMT Column          
                //cell = new TableCell();
                //cell.Text = string.Format("{0:0.00}", ddlGrandTotalTotadditionComma);
                //cell.HorizontalAlign = HorizontalAlign.Right;
                //cell.CssClass = "GrandTotalRowStyle";
                //row.Cells.Add(cell);

                ////Adding CRAMT Column          
                //cell = new TableCell();
                //// cell.Text = string.Format("{0:0.00}", ddlGrandTotalOthersComma);
                //cell.HorizontalAlign = HorizontalAlign.Right;
                //cell.CssClass = "GrandTotalRowStyle";
                //row.Cells.Add(cell);

                ////Adding CRAMT Column          
                //cell = new TableCell();
                //// cell.Text = string.Format("{0:0.00}", ddlGrandTotalOthersComma);
                //cell.HorizontalAlign = HorizontalAlign.Right;
                //cell.CssClass = "GrandTotalRowStyle";
                //row.Cells.Add(cell);

                ////Adding CRAMT Column          
                //cell = new TableCell();
                //// cell.Text = string.Format("{0:0.00}", ddlGrandTotalOthersComma);
                //cell.HorizontalAlign = HorizontalAlign.Right;
                //cell.CssClass = "GrandTotalRowStyle";
                //row.Cells.Add(cell);

                ////Adding CRAMT Column          
                //cell = new TableCell();
                //// cell.Text = string.Format("{0:0.00}", ddlGrandTotalOthersComma);
                //cell.HorizontalAlign = HorizontalAlign.Right;
                //cell.CssClass = "GrandTotalRowStyle";
                //row.Cells.Add(cell);

                ////Adding CRAMT Column          
                //cell = new TableCell();
                //// cell.Text = string.Format("{0:0.00}", ddlGrandTotalOthersComma);
                //cell.HorizontalAlign = HorizontalAlign.Right;
                //cell.CssClass = "GrandTotalRowStyle";
                //row.Cells.Add(cell);

                ////Adding CRAMT Column          
                //cell = new TableCell();
                //cell.Text = string.Format("{0:0.00}", ddlGrandTotaltotDeductionComma);
                //cell.HorizontalAlign = HorizontalAlign.Right;
                //cell.CssClass = "GrandTotalRowStyle";
                //row.Cells.Add(cell);

                ////Adding CRAMT Column          
                //cell = new TableCell();
                //cell.Text = string.Format("{0:0.00}", ddlGrandTotalNetPayComma);
                //cell.HorizontalAlign = HorizontalAlign.Right;
                //cell.CssClass = "GrandTotalRowStyle";
                //row.Cells.Add(cell);

                ////Adding CRAMT Column          
                //cell = new TableCell();
                ////cell.Text = string.Format("{0:0.00}", ddlGrandTotalIncentiveComma);
                //cell.HorizontalAlign = HorizontalAlign.Right;
                //cell.CssClass = "GrandTotalRowStyle";
                //row.Cells.Add(cell);

                ////Adding the Row at the RowIndex position in the Grid     
                //GridView1.Controls[0].Controls.AddAt(e.Row.RowIndex, row);
                ////#endregion
            }
        }

        /// <summary>    
        /// Event fires when data binds to each row   
        /// Used for calculating Group Total     
        /// </summary>   
        /// /// <param name="sender"></param>    
        /// <param name="e"></param>    
        protected void GridView1_RowDataBound(object sender, GridViewRowEventArgs e)
        {
            // This is for cumulating the values       
            if (e.Row.RowType == DataControlRowType.DataRow)
            {
                strPreviousRowID_OP = DataBinder.Eval(e.Row.DataItem, "DEPTNM").ToString();


                string SL = DataBinder.Eval(e.Row.DataItem, "SL").ToString();
                e.Row.Cells[0].Text = SL;

                var empnm = DataBinder.Eval(e.Row.DataItem, "EMPNM").ToString();
                e.Row.Cells[1].Text = "&nbsp;" + empnm;

                var postnm = DataBinder.Eval(e.Row.DataItem, "POSTNM").ToString();
                e.Row.Cells[2].Text = postnm;

                decimal BASICSAL = Convert.ToDecimal(DataBinder.Eval(e.Row.DataItem, "BASICSAL").ToString());
                string BASICSALString = SpellAmount.comma(BASICSAL);
                e.Row.Cells[3].Text = BASICSALString;

                decimal HOUSERENT = Convert.ToDecimal(DataBinder.Eval(e.Row.DataItem, "HOUSERENT").ToString());
                string HOUSERENTString = SpellAmount.comma(HOUSERENT);
                e.Row.Cells[4].Text = HOUSERENTString;

                decimal MEDICAL = Convert.ToDecimal(DataBinder.Eval(e.Row.DataItem, "MEDICAL").ToString());
                string MEDICALString = SpellAmount.comma(MEDICAL);
                e.Row.Cells[5].Text = MEDICALString;

                decimal TRANSPORT = Convert.ToDecimal(DataBinder.Eval(e.Row.DataItem, "TRANSPORT").ToString());
                string TRANSPORTString = SpellAmount.comma(TRANSPORT);
                e.Row.Cells[6].Text = TRANSPORTString;

                decimal STAMP = Convert.ToDecimal(DataBinder.Eval(e.Row.DataItem, "STAMP").ToString());
                string STAMPString = SpellAmount.comma(STAMP);
                e.Row.Cells[7].Text = STAMPString;

                decimal GROSSSAL = Convert.ToDecimal(DataBinder.Eval(e.Row.DataItem, "GROSSSAL").ToString());
                string GROSSSALString = SpellAmount.comma(GROSSSAL);
                e.Row.Cells[8].Text = GROSSSALString;

                decimal INCENTIVE = Convert.ToDecimal(DataBinder.Eval(e.Row.DataItem, "INCENTIVE").ToString());
                string INCENTIVEString = SpellAmount.comma(INCENTIVE);
                e.Row.Cells[9].Text = INCENTIVEString;

                decimal FOODINGADD = Convert.ToDecimal(DataBinder.Eval(e.Row.DataItem, "FOODINGADD").ToString());
                string FOODINGADDString = SpellAmount.comma(FOODINGADD);
                e.Row.Cells[10].Text = FOODINGADDString;

                decimal BONUSP = Convert.ToDecimal(DataBinder.Eval(e.Row.DataItem, "BONUSP").ToString());
                string BONUSPString = SpellAmount.comma(BONUSP);
                e.Row.Cells[11].Text = BONUSPString;

                decimal OTHOUR = Convert.ToDecimal(DataBinder.Eval(e.Row.DataItem, "OTHOUR").ToString());
                string OTHOURString = SpellAmount.comma(OTHOUR);
                e.Row.Cells[12].Text = OTHOURString;

                decimal AMTOTHOUR = Convert.ToDecimal(DataBinder.Eval(e.Row.DataItem, "AMTOTHOUR").ToString());
                string AMTOTHOURString = SpellAmount.comma(AMTOTHOUR);
                e.Row.Cells[13].Text = AMTOTHOURString;

                decimal TOTADD = Convert.ToDecimal(DataBinder.Eval(e.Row.DataItem, "TOTADD").ToString());
                string TOTADDString = SpellAmount.comma(TOTADD);
                e.Row.Cells[14].Text = TOTADDString;

                decimal ABSDED = Convert.ToDecimal(DataBinder.Eval(e.Row.DataItem, "ABSDED").ToString());
                string ABSDEDString = SpellAmount.comma(ABSDED);
                e.Row.Cells[15].Text = ABSDEDString;

                decimal ADVANCE = Convert.ToDecimal(DataBinder.Eval(e.Row.DataItem, "ADVANCE").ToString());
                string ADVANCEString = SpellAmount.comma(ADVANCE);
                e.Row.Cells[16].Text = ADVANCEString;

                decimal FOODING = Convert.ToDecimal(DataBinder.Eval(e.Row.DataItem, "FOODING").ToString());
                string FOODINGString = SpellAmount.comma(FOODING);
                e.Row.Cells[17].Text = FOODINGString;

                decimal FINEADJ = Convert.ToDecimal(DataBinder.Eval(e.Row.DataItem, "FINEADJ").ToString());
                string FINEADJString = SpellAmount.comma(FINEADJ);
                e.Row.Cells[18].Text = FINEADJString;



                decimal ITAX = Convert.ToDecimal(DataBinder.Eval(e.Row.DataItem, "ITAX").ToString());
                string ITAXString = SpellAmount.comma(ITAX);
                e.Row.Cells[19].Text = ITAXString;

                decimal TOTDED = Convert.ToDecimal(DataBinder.Eval(e.Row.DataItem, "TOTDED").ToString());
                string TOTDEDString = SpellAmount.comma(TOTDED);
                e.Row.Cells[20].Text = TOTDEDString;

                decimal NETPAY = Convert.ToDecimal(DataBinder.Eval(e.Row.DataItem, "NETPAY").ToString());
                string NETPAYString = SpellAmount.comma(NETPAY);
                e.Row.Cells[21].Text = NETPAYString;

                // Cumulating Sub Total            

                dblSubTotalBasicAMT += BASICSAL;
                ddlSubTotalBasicAMTComma = SpellAmount.comma(dblSubTotalBasicAMT);
                ddlSubtotalHouseRentAMT += HOUSERENT;
                ddlSubTotalHouseRentAMTComma = SpellAmount.comma(ddlSubtotalHouseRentAMT);
                ddlSubTotalMadicalAMT += MEDICAL;
                ddlSubTotalMadicalAMTComma = SpellAmount.comma(ddlSubTotalMadicalAMT);
                ddlSubTotalConveyAMT += TRANSPORT;
                ddlSubTotalConveyComma = SpellAmount.comma(ddlSubTotalConveyAMT);
                ddlSubTotalOthersAMT += STAMP;
                ddlSubTotalOthersComma = SpellAmount.comma(ddlSubTotalOthersAMT);
                ddlSubTotalGrossAMT += GROSSSAL;
                ddlSubTotalGrossComma = SpellAmount.comma(ddlSubTotalGrossAMT);
                ddlSubTotalIncentiveAMT += INCENTIVE;
                ddlSubTotalIncentiveComma = SpellAmount.comma(ddlSubTotalIncentiveAMT);
                ddlSubTotalFoodingAMT += FOODINGADD;
                ddlSubTotalFoodingComma = SpellAmount.comma(ddlSubTotalFoodingAMT);
                ddlSubTotalAttendanceAMT += BONUSP;
                ddlSubTotalAttandanceComma = SpellAmount.comma(ddlSubTotalAttendanceAMT);
                ddlSubTotalOthourAMT += OTHOUR;
                ddlSubTotalOthourComma = SpellAmount.comma(ddlSubTotalOthourAMT);
                ddlSubTotalOtAmountAMT += AMTOTHOUR;
                ddlSubTotalOtAmountComma = SpellAmount.comma(ddlSubTotalOtAmountAMT);
                ddlSubTotaltotAdditionAMT += TOTADD;
                ddlSubTotaltotadditionComma = SpellAmount.comma(ddlSubTotaltotAdditionAMT);
                ddlSubTotalAbsenseAMT += ABSDED;
                ddlSubTotalAbsenseComma = SpellAmount.comma(ddlSubTotalAbsenseAMT);
                ddlSubTotaladvanceAMT += ADVANCE;
                ddlSubTotaladvanceComma = SpellAmount.comma(ddlSubTotaladvanceAMT);
                ddlSubTotalfooddedAMT += FOODING;
                ddlSubTotalfooddedComma = SpellAmount.comma(ddlSubTotalfooddedAMT);
                ddlSubTotalfineAMT += FINEADJ;
                ddlSubTotalfineComma = SpellAmount.comma(ddlSubTotalfineAMT);
                ddlSubTotalincometaxAMT += ITAX;
                ddlSubTotalincometaxComma = SpellAmount.comma(ddlSubTotalincometaxAMT);
                ddlSubTotaltotDeductionAMT += TOTDED;
                ddlSubTotaltotDeductionComma = SpellAmount.comma(ddlSubTotaltotDeductionAMT);
                ddlSubTotalNetPayAMT += NETPAY;
                ddlSubTotalNetPayComma = SpellAmount.comma(ddlSubTotalNetPayAMT);



                // Cumulating Grand Total      

                ddlGrandTotalBasicAMT += BASICSAL;
                ddlGrandTotalBasicAMTComma = SpellAmount.comma(ddlGrandTotalBasicAMT);
                ddlGrandTotalHouseRentAMT += HOUSERENT;
                ddlGrandTotalHouseRentAMTComma = SpellAmount.comma(ddlGrandTotalHouseRentAMT);
                ddlGrandTotalMadicalAMT += HOUSERENT;
                ddlGrandTotalMadicalComma = SpellAmount.comma(ddlGrandTotalMadicalAMT);
                ddlGrandTotalConveyAMT += TRANSPORT;
                ddlGrandTotalConveyComma = SpellAmount.comma(ddlGrandTotalConveyAMT);
                ddlGrandTotalOthersAMT += STAMP;
                ddlGrandTotalOthersComma = SpellAmount.comma(ddlGrandTotalOthersAMT);
                ddlGrandTotalGrossAMT += GROSSSAL;
                ddlGrandTotalGrossComma = SpellAmount.comma(ddlGrandTotalGrossAMT);
                ddlGrandTotalIncentiveAMT += INCENTIVE;
                ddlGrandTotalIncentiveComma = SpellAmount.comma(ddlGrandTotalIncentiveAMT);
                ddlGrandTotalFoodingAMT += FOODINGADD;
                ddlGrandTotalFoodingComma = SpellAmount.comma(ddlGrandTotalFoodingAMT);
                ddlGrandTotalAttandanceAMT += BONUSP;
                ddlGrandTotalAttandanceComma = SpellAmount.comma(ddlGrandTotalAttandanceAMT);
                ddlGrandTotalOthourAMT += OTHOUR;
                ddlGrandTotalOthourComma = SpellAmount.comma(ddlGrandTotalOthourAMT);
                ddlGrandTotalOtAmountAMT += AMTOTHOUR;
                ddlGrandTotalOtAmountComma = SpellAmount.comma(ddlGrandTotalOtAmountAMT);
                ddlGrandTotaltotAdditionAMT += TOTADD;
                ddlGrandTotalTotadditionComma = SpellAmount.comma(ddlGrandTotaltotAdditionAMT);
                ddlGrandTotalAbsenseAMT += ABSDED;
                ddlGrandTotalAbsenseComma = SpellAmount.comma(ddlGrandTotalAbsenseAMT);
                ddlGrandTotaladvanceAMT += ADVANCE;
                ddlGrandTotaladvanceComma = SpellAmount.comma(ddlGrandTotaladvanceAMT);
                ddlGrandTotalfooddedAMT += FOODING;
                ddlGrandTotalfooddedComma = SpellAmount.comma(ddlGrandTotalfooddedAMT);
                ddlGrandTotalfineAMT += FINEADJ;
                ddlGrandTotalfineComma = SpellAmount.comma(ddlGrandTotalfineAMT);
                ddlGrandTotalincometaxAMT += ITAX;
                ddlGrandTotalincometaxComma = SpellAmount.comma(ddlGrandTotalincometaxAMT);
                ddlGrandTotaltotdeductionAMT += TOTDED;
                ddlGrandTotaltotDeductionComma = SpellAmount.comma(ddlGrandTotaltotdeductionAMT);
                ddlGrandTotalNetPayAMT += NETPAY;
                ddlGrandTotalNetPayComma = SpellAmount.comma(ddlGrandTotalNetPayAMT);


            }
            else if (e.Row.RowType == DataControlRowType.Footer)
            {
                e.Row.Cells[2].Text = "Grand Total";
                e.Row.Cells[2].HorizontalAlign = HorizontalAlign.Right;
                e.Row.Cells[3].Text = ddlGrandTotalBasicAMTComma;
                e.Row.Cells[3].HorizontalAlign = HorizontalAlign.Right;
                e.Row.Cells[4].Text = ddlGrandTotalHouseRentAMTComma;
                e.Row.Cells[4].HorizontalAlign = HorizontalAlign.Right;
                e.Row.Cells[5].Text = ddlGrandTotalMadicalComma;
                e.Row.Cells[5].HorizontalAlign = HorizontalAlign.Right;
                e.Row.Cells[6].Text = ddlGrandTotalConveyComma;
                e.Row.Cells[6].HorizontalAlign = HorizontalAlign.Right;
                e.Row.Cells[7].Text = ddlGrandTotalOthersComma;
                e.Row.Cells[7].HorizontalAlign = HorizontalAlign.Right;
                e.Row.Cells[8].Text = ddlGrandTotalGrossComma;
                e.Row.Cells[8].HorizontalAlign = HorizontalAlign.Right;
                e.Row.Cells[9].Text = ddlGrandTotalIncentiveComma;
                e.Row.Cells[9].HorizontalAlign = HorizontalAlign.Right;
                e.Row.Cells[10].Text = ddlGrandTotalFoodingComma;
                e.Row.Cells[10].HorizontalAlign = HorizontalAlign.Right;
                e.Row.Cells[11].Text = ddlGrandTotalAttandanceComma;
                e.Row.Cells[11].HorizontalAlign = HorizontalAlign.Right;
                e.Row.Cells[12].Text = ddlGrandTotalOthourComma;
                e.Row.Cells[12].HorizontalAlign = HorizontalAlign.Right;
                e.Row.Cells[13].Text = ddlGrandTotalOtAmountComma;
                e.Row.Cells[13].HorizontalAlign = HorizontalAlign.Right;
                e.Row.Cells[14].Text = ddlGrandTotalTotadditionComma;
                e.Row.Cells[14].HorizontalAlign = HorizontalAlign.Right;
                e.Row.Cells[15].Text = ddlGrandTotalAbsenseComma;
                e.Row.Cells[15].HorizontalAlign = HorizontalAlign.Right;
                e.Row.Cells[16].Text = ddlGrandTotaladvanceComma;
                e.Row.Cells[16].HorizontalAlign = HorizontalAlign.Right;
                e.Row.Cells[17].Text = ddlGrandTotalfooddedComma;
                e.Row.Cells[17].HorizontalAlign = HorizontalAlign.Right;
                e.Row.Cells[18].Text = ddlGrandTotalfineComma;
                e.Row.Cells[18].HorizontalAlign = HorizontalAlign.Right;
                e.Row.Cells[19].Text = ddlGrandTotalincometaxComma;
                e.Row.Cells[19].HorizontalAlign = HorizontalAlign.Right;
                e.Row.Cells[20].Text = ddlGrandTotaltotDeductionComma;
                e.Row.Cells[20].HorizontalAlign = HorizontalAlign.Right;
                e.Row.Cells[21].Text = ddlGrandTotalNetPayComma;
                e.Row.Cells[21].HorizontalAlign = HorizontalAlign.Right;

            }

            MakeGridViewPrinterFriendly(GridView1);
        }

        private void MakeGridViewPrinterFriendly(GridView grid)
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