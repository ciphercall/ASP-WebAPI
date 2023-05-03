using System;

namespace DynamicMenu.Payroll.Interface
{
    public class AttendanceProcessModel
    {
        public string Date { get; set; }
        public string InTime { get; set; }
        public string OutTime { get; set; }
        public string EmployeeId { get; set; }
        public string ShiftId { get; set; }
    }
}