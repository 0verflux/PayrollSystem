using PayrollSystem.Logic.Domain.Positions.DTOs;
using PayrollSystem.Logic.Domain.SalaryAdjustmentDetails.DTOs;
using System;
using System.Collections.Generic;

namespace PayrollSystem.Logic.Domain.PayrollEntries.DTOs
{
    public class PayrollEntryDetailsDTO
    {
        public int ID { get; set; }
        public PositionDTO Position { get; set; }
        public List<SalaryAdjustmentDetailDTO> SalaryAdjustmentDetails { get; set; }
        public float HoursWorked { get; set; }
        public float HoursOvertime { get; set; }
        public DateTime PayPeriodStart { get; set; }
        public DateTime PayPeriodEnd { get; set; }
    }
}