using Ardalis.GuardClauses;
using AutoMapper;
using Microsoft.EntityFrameworkCore;
using PayrollSystem.Logic.Contexts;
using PayrollSystem.Logic.Contracts;
using PayrollSystem.Logic.Domain.SalaryAdjustmentDetails.DTOs;
using PayrollSystem.Logic.Exceptions;
using PayrollSystem.Logic.Extensions;
using PayrollSystem.Logic.Specifications;
using System.Collections.Generic;
using System.Linq;

namespace PayrollSystem.Logic.Domain.SalaryAdjustments
{
    public class SalaryAdjustmentManager : ISalaryAdjustmentManager
    {
        private readonly IMapper mapper;
        private readonly IDbContextFactory<PayrollDBContext> contextFactory;

        public SalaryAdjustmentManager(IMapper mapper, IDbContextFactory<PayrollDBContext> contextFactory)
        {
            this.mapper = mapper;
            this.contextFactory = contextFactory;
        }

        public void CreateSalaryAdjustment(SalaryAdjustmentDTO salaryAdjustmentDTO)
        {
            using var context = contextFactory.CreateDbContext();
            
            var salaryAdjustment = mapper.Map<SalaryAdjustment>(salaryAdjustmentDTO);

            if (Exists(context, default, salaryAdjustment.Code))
                throw new EntityAlreadyExistsException("Salary Adjustment Code");

            context.SalaryAdjustments.Add(salaryAdjustment);
            context.SaveChanges();
        }

        public void UpdateSalaryAdjustment(SalaryAdjustmentDTO salaryAdjustmentDTO)
        {
            using var context = contextFactory.CreateDbContext();
            
            var salaryAdjustment = mapper.Map<SalaryAdjustment>(salaryAdjustmentDTO);

            if (Exists(context, salaryAdjustment.ID, salaryAdjustment.Code))
                throw new EntityAlreadyExistsException("Salary Adjustment Code");

            context.SalaryAdjustments.Update(salaryAdjustment);
            context.SaveChanges();
        }

        public void DeleteSalaryAdjustment(SalaryAdjustmentDTO salaryAdjustmentDTO)
        {
            using var context = contextFactory.CreateDbContext();
            
            var salaryAdjustment = mapper.Map<SalaryAdjustment>(salaryAdjustmentDTO);

            context.SalaryAdjustments.Remove(salaryAdjustment);
            context.SaveChanges();
        }

        public IEnumerable<SalaryAdjustmentDTO> GetAllSalaryAdjustments()
        {
            using var context = contextFactory.CreateDbContext();

            var result = context
                .SalaryAdjustments
                .ToList();

            return mapper.Map<List<SalaryAdjustment>, List<SalaryAdjustmentDTO>>(result);
        }

        public IEnumerable<SalaryAdjustmentDTO> GetSalaryAdjustments(string search = null)
        {
            using var context = contextFactory.CreateDbContext();

            var codeSpec = new SalaryAdjustmentCodeSpec(search);

            var result = context
                .SalaryAdjustments
                .Where(codeSpec)
                .ToList();

            return mapper.Map<List<SalaryAdjustment>, List<SalaryAdjustmentDTO>>(result);
        }

        public bool Exists(int id, string code)
        {
            if (string.IsNullOrWhiteSpace(code))
                return false;

            using var context = contextFactory.CreateDbContext();
            return Exists(context, id, code);
        }

        private static bool Exists(PayrollDBContext context, int id, string code)
        {
            var salaryAdjustmentExistSpec = new SalaryAdjustmentExistSpec(id, code);

            var result = context
                .SalaryAdjustments
                .Any(salaryAdjustmentExistSpec);

            return result;
        }
    }
}
