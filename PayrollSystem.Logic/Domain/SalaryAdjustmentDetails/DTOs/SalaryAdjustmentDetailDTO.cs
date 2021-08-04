using PayrollSystem.Logic.Domain.SalaryAdjustments;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PayrollSystem.Logic.Domain.SalaryAdjustmentDetails.DTOs
{
    public class SalaryAdjustmentDetailDTO
    {
        public int ID { get; set; }
        public SalaryAdjustmentDTO SalaryAdjustment { get; set; }
        public float Percentage { get; set; }
        public decimal Value { get; set; }

    }
}
