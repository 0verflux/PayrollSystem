using PayrollSystem.Logic.Domain.PayrollEntries.DTOs;
using PayrollSystem.Logic.Domain.SalaryAdjustmentDetails.DTOs;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PayrollSystem.Logic.Contracts
{
    public interface IPayrollManager
    {
        void AddPayrollEntry(CreatePayrollEntryDTO createPayrollEntry, List<SalaryAdjustmentDetailDTO> salaryAdjustmentDetailList);
        float CalculatePercentage(decimal totalPay, decimal value);
        decimal CalculateValue(decimal totalPay, float percentage);
        decimal RecomputeTotalPay(decimal ratePerHour, float hoursWorked, float hoursOvertime);
    }
}
