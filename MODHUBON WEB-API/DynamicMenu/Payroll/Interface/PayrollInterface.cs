using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace DynamicMenu.Payroll.Interface
{
    public class PayrollInterface
    {
        public int CmpID { get; set; }

        public string UserPC { get; set; }

        public string Username { get; set; }

        public string Ipaddress { get; set; }

        public DateTime ITime { get; set; }

        public int PostID { get; set; }

        public string Remarks { get; set; }

        public string PostNM { get; set; }

        public int UserID { get; set; }

        public string Ltude { get; set; }

        public string UPDUserPC { get; set; }

        public string UPDUsername { get; set; }

        public string UPDIpaddress { get; set; }

        public DateTime UPDTime { get; set; }

        public int UPDUserID { get; set; }

        public string UPDLtude { get; set; }

        public string DEPTNM { get; set; }

        public int DEPTID { get; set; }

        public int HDAYID { get; set; }

        public string HDAYNM { get; set; }

        public DateTime TransDT { get; set; }

        public string Status { get; set; }

        public int TransYr { get; set; }

        public int SHIFTID { get; set; }

        public string SHIFTNM { get; set; }

        public TimeSpan TimeFR { get; set; }

        public TimeSpan TimeTO { get; set; }

        /******************A********************/
        public TimeSpan LateFR { get; set; }
        public TimeSpan OTFrom { get; set; }

        public int LEAVEID { get; set; }

        public string LEAVENM { get; set; }

        public int LEAVEDD { get; set; }

        public int LEAVEYR { get; set; }

        public string EmpNM { get; set; }

        public string EmpGNM { get; set; }

        public string Email { get; set; }

        public string EmpMNM { get; set; }

        public string PreAdrs { get; set; }

        public string PerAdrs { get; set; }

        public string CNO { get; set; }

        public DateTime DOB { get; set; }

        public string Gndr { get; set; }

        public string BldGRP { get; set; }

        public string VotrID { get; set; }

        public string Ref1NM { get; set; }

        public string Ref1Desig { get; set; }

        public string Ref1Adrs { get; set; }

        public string Ref1CNO { get; set; }

        public string Ref2NM { get; set; }

        public string Ref2Desig { get; set; }

        public string Ref2Adrs { get; set; }

        public string Ref2CNO { get; set; }

        public DateTime JoinDT { get; set; }

        public string DPT { get; set; }

        public string EMPTP { get; set; }

        public string EMPIDM { get; set; }

        public string CRDNO { get; set; }

        public DateTime CRDISUDT { get; set; }

        public string Stats { get; set; }

        public int EMPID { get; set; }

        public int DPTID { get; set; }

        public object GEDR { get; set; }

        public decimal BasicSal { get; set; }

        public decimal HouseRent { get; set; }

        public decimal Medical { get; set; }

        public decimal TrnsPort { get; set; }

        public decimal Revenue { get; set; }

        public decimal PFRate { get; set; }

        public decimal OTRThour { get; set; }
        public decimal OTRTday { get; set; }

        public DateTime PFEffectFR { get; set; }

        public DateTime PFEffectTO { get; set; }

        public DateTime JOBEffectFR { get; set; }

        public DateTime JOBEffectTO { get; set; }
        public string CostPoolId { get; set; }
        public string Img { get; set; }
        public decimal Alwdaily { get; set; }
        public decimal Bonusp { get; set; }
        public decimal Bonusf { get; set; }
        public decimal Incentive { get; set; }
        public decimal Advance { get; set; }
        public decimal PfAdd { get; set; }
        public decimal Convey { get; set; }
        public decimal Mobile { get; set; }
        public decimal Dueadj { get; set; }
        public decimal Commission { get; set; }
        public decimal PfDed { get; set; }
        public decimal Stamp { get; set; }
        public decimal Fooding { get; set; }
        public decimal Fine { get; set; }
        public decimal Payadj { get; set; }
        public string TransMonthYear { get; set; }
        public Int64 BranchCode { get; set; }

        public decimal TransPort { get; set; }
        public decimal GrossSal { get; set; }
        public decimal DayTot { get; set; }
        public decimal DayWork { get; set; }
        public decimal DayHoli { get; set; }
        public decimal DayPre { get; set; }
        public decimal DayLeave { get; set; }
        public decimal DayAbs { get; set; }
        public decimal OtDay { get; set; }
        public decimal Othour { get; set; }
        public decimal TotAdd { get; set; }
        public decimal TotDed { get; set; }
        public decimal NetPay { get; set; }
        public DateTime InTime { get; set; }
        public DateTime OutTime { get; set; }
        public TimeSpan AttInTime { get; set; }
        public TimeSpan AttOutTime { get; set; }
        public TimeSpan attLate { get; set; }
        public TimeSpan attOTHour { get; set; }
        public TimeSpan ShiftFrTm { get; set; }
        public TimeSpan ShiftToTm { get; set; }
        public TimeSpan OtDifferenceTime { get; set; }
        public string EntryTypeIn { get; set; }

        public string EntryTypeOut { get; set; }
        public decimal AlwdailyOt { get; set; }
        public decimal AbsDed { get; set; }
        public decimal AmountTotalDays { get; set; }
        public decimal AmountTotalHours { get; set; }

        public decimal TotalMonthDay { get; set; }

        public decimal HoliDayOpen { get; set; }

        public decimal HoliDayClose { get; set; }
        public decimal TotalWorkDay  { get; set; }
        public decimal TotalPresentDay { get; set; }
        public decimal TotalOverTimeDay { get; set; }
        public decimal TotalWokeringkDayCal { get; set; }
        public decimal TotalOverTimeMinute { get; set; }
        public string Branch { get; set; }

        public decimal Foodinadd { get; set; }
        public decimal BonusP { get; set; }
        public decimal ITax { get; set; }

        /***************Emloyee Loan Start****************/
        public DateTime Date { get; set; }
        public string MonthYear { get; set; }
        public Int64 TransNo { get; set; }
        public Int64 EmployeeID { get; set; }
        public decimal LoanAM { get; set; }
        public decimal DeductionAM { get; set; }
        public DateTime fdt { get; set; }
        public DateTime tdt { get; set; }

        /***************Emloyee Loan End****************/

        /***************LEAVE APPLICATION MASTER START****************/
        public DateTime TRANSDT { get; set; }
        public string TRANSMY { get; set; }
        public Int64 TRANSNO { get; set; }

        /***************LEAVE APPLICATION MASTER END****************/

        /***************LEAVE APPLICATION TRANS START****************/
        //LEAVEID, LEAVEFR,LEAVETO,LEAVEDAYS,REASON

        public DateTime LEAVEFR { get; set; }
        public DateTime LEAVETO { get; set; }
        public Int64 LEAVEDAYS { get; set; }
        public string REASON { get; set; }
        public decimal PresentRT { get; internal set; }


        /***************LEAVE APPLICATION TRANS END****************/
    }
}