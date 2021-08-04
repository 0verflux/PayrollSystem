using PayrollSystem.Logic.Domain.SalaryAdjustmentDetails.DTOs;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PayrollSystem.Logic.Domain.PayrollEntries.DTOs
{
    public class CreatePayrollEntryDTO
    {
        public int ID { get; set; }
        public int EmployeeID { get; set; }
        public string EmployeeName { get; set; }
        public int CurrentPositionID { get; set; }
        public string CurrentPositionName { get; set; }
        public decimal RatePerHour { get; set; }
        public float HoursWorked { get; set; }
        public float HoursOvertime { get; set; }
        public DateTime PayPeriodStart { get; set; }
        public DateTime PayPeriodEnd { get; set; }
        public List<SalaryAdjustmentDetailDTO> SalaryAdjustmentDetails { get; set; }
    }
}
