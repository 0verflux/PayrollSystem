using AutoMapper;
using Microsoft.EntityFrameworkCore;
using PayrollSystem.Logic.Contexts;
using PayrollSystem.Logic.Contracts;
using PayrollSystem.Logic.Domain.Employees.DTOs;
using PayrollSystem.Logic.Specifications;
using System;
using System.Collections.Generic;
using System.Linq;
namespace PayrollSystem.Logic.Domain.Employees
{
    public class EmployeeManager : IEmployeeManager
    {
        private readonly IMapper mapper;
        private readonly IDbContextFactory<PayrollDBContext> contextFactory;

        public EmployeeManager(IMapper mapper, IDbContextFactory<PayrollDBContext> contextFactory)
        {
            this.mapper = mapper;
            this.contextFactory = contextFactory;
        }

        public void CreateEmployee(EmployeeDTO employeeDTO)
        {
            using var context = contextFactory.CreateDbContext();

            var employee = ReconstructEmployee(context, mapper.Map<Employee>(employeeDTO));

            context.Employees.Add(employee);
            context.SaveChanges();
        }

        public void UpdateEmployee(EmployeeDTO employeeDTO)
        {
            using var context = contextFactory.CreateDbContext();

            var employee = ReconstructEmployee(context, mapper.Map<Employee>(employeeDTO));

            context.Employees.Update(employee);
            context.SaveChanges();
        }

        public void MarkAsEndEmploymentByEmployee(EmployeeDTO employeeDTO, DateTime? employmentEndDate = null)
        {
            using var context = contextFactory.CreateDbContext();
            
            var employee = ReconstructEmployee(context, mapper.Map<Employee>(employeeDTO));
            employee.SetEmploymentEndDate(employmentEndDate ?? DateTime.Now);

            context.Employees.Update(employee);
            context.SaveChanges();
        }

        public void DeleteEmployee(EmployeeDTO employeeDTO)
        {
            using var context = contextFactory.CreateDbContext();

            var employee = ReconstructEmployee(context, mapper.Map<Employee>(employeeDTO));

            context.Employees.Remove(employee);
            context.SaveChanges();
        }

        public List<EmployeeDTO> GetAllEmployees()
        {
            using var context = contextFactory.CreateDbContext();
            return mapper.Map<List<Employee>, List<EmployeeDTO>>(QueryEmployees(context, false, false).ToList());
        }
        public List<EmployeeDTO> GetEmployees(string search = null, int positionID = default, bool isActive = false, bool includePositions = false, bool includePayrollEntries = false)
        {
            using var context = contextFactory.CreateDbContext();

            var nameSpec = new EmployeeNameSpec(search);
            var positionSpec = new EmployeePositionIDSpec(positionID);
            var isActiveSpec = new EmployeeActiveSpec(isActive);

            return mapper.Map<List<Employee>, List<EmployeeDTO>>(QueryEmployees(context, includePositions, includePayrollEntries).Where(isActiveSpec.And(nameSpec).And(positionSpec)).ToList());

        }
        private static IQueryable<Employee> QueryEmployees(PayrollDBContext ctx, bool includePositions, bool includePayrollEntries)
        {
            var query = ctx.Employees.AsQueryable();

            if (includePositions)
            {
                query = query.Include(e => e.Position);
            }

            if (includePayrollEntries)
            {
                query = query.Include(e => e.PayrollEntries)
                             .ThenInclude(e => e.CurrentPosition)
                             .Include(e => e.PayrollEntries)
                             .ThenInclude(e => e.SalaryAdjustmentDetails)
                             .ThenInclude(e => e.SalaryAdjustment);
            }

            return query;
        }
        private static Employee ReconstructEmployee(PayrollDBContext ctx, Employee employee)
        {
            var postion = ctx.Positions.FirstOrDefault(e => e == employee.Position);
            employee.SetPosition(postion);

            return employee;
        }
    }
}
