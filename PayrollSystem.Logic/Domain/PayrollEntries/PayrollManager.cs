using AutoMapper;
using Microsoft.EntityFrameworkCore;
using PayrollSystem.Logic.Contexts;
using PayrollSystem.Logic.Contracts;
using PayrollSystem.Logic.Domain.Employees;
using PayrollSystem.Logic.Domain.PayrollEntries.DTOs;
using PayrollSystem.Logic.Domain.Positions;
using PayrollSystem.Logic.Domain.Positions.DTOs;
using PayrollSystem.Logic.Domain.SalaryAdjustmentDetails;
using PayrollSystem.Logic.Domain.SalaryAdjustmentDetails.DTOs;
using System;
using System.Collections.Generic;
using System.Linq;

namespace PayrollSystem.Logic.Domain.PayrollEntries
{
    public class PayrollManager : IPayrollManager
    {
        private const decimal OVERTIME_FACTOR = 1.25m;

        private readonly IMapper mapper;
        private readonly IDbContextFactory<PayrollDBContext> contextFactory;

        public PayrollManager(IMapper mapper, IDbContextFactory<PayrollDBContext> contextFactory)
        {
            this.mapper = mapper;
            this.contextFactory = contextFactory;
        }

        public void AddPayrollEntry(CreatePayrollEntryDTO createPayrollEntry, List<SalaryAdjustmentDetailDTO> salaryAdjustmentDetails)
        {
            using var context = contextFactory.CreateDbContext();

            PayrollEntry payrollEntry = mapper.Map<CreatePayrollEntryDTO, PayrollEntry>(createPayrollEntry);
            List<SalaryAdjustmentDetail> salaryAdjustmentDetailList = mapper.Map<List<SalaryAdjustmentDetailDTO>, List<SalaryAdjustmentDetail>>(salaryAdjustmentDetails);

            var emp = ReconstructEmployee(context, payrollEntry.Employee);
            var pos = ReconstructPosition(context, payrollEntry.CurrentPosition);
            var sad = ReconstructSAD(context, salaryAdjustmentDetailList);

            payrollEntry.SetEmployee(emp);
            payrollEntry.SetPosition(pos);
            payrollEntry.SetSalaryAdjustmentDetails(sad);

            context.PayrollEntries.Add(payrollEntry);
            context.SaveChanges();
        }

        public decimal RecomputeTotalPay(decimal ratePerHour, float hoursWorked, float hoursOvertime)
        {
            return ratePerHour * ((decimal)hoursWorked + (OVERTIME_FACTOR * (decimal)hoursOvertime));
        }

        public float CalculatePercentage(decimal totalPay, decimal value)
        {
            if (totalPay == 0)
            {
                return 0;
            }

            return (float)(value / totalPay) * 100f;
        }

        public decimal CalculateValue(decimal totalPay, float percentage)
        {
            return totalPay * (decimal)(percentage / 100f);
        }

        private static Employee ReconstructEmployee(PayrollDBContext context, Employee employee)
        {
            return context.Employees.First(e => e == employee);
        }

        private static Position ReconstructPosition(PayrollDBContext context, Position position)
        {
            return context.Positions.First(e => e == position);
        }

        private static List<SalaryAdjustmentDetail> ReconstructSAD(PayrollDBContext context, List<SalaryAdjustmentDetail> sadList)
        {
            for(int i = 0; i < sadList.Count; i++)
            {
                sadList[i].SetSalaryAdjustment(context.SalaryAdjustments.First(e => e == sadList[i].SalaryAdjustment));
            }

            return sadList;
        }
    }
}
