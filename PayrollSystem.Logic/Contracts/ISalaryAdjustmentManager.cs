using PayrollSystem.Logic.Domain.SalaryAdjustmentDetails.DTOs;
using System.Collections.Generic;

namespace PayrollSystem.Logic.Contracts
{
    public interface ISalaryAdjustmentManager
    {
        void CreateSalaryAdjustment(SalaryAdjustmentDTO salaryAdjustmentDTO);
        void UpdateSalaryAdjustment(SalaryAdjustmentDTO salaryAdjustmentDTO);
        void DeleteSalaryAdjustment(SalaryAdjustmentDTO salaryAdjustmentDTO);
        IEnumerable<SalaryAdjustmentDTO> GetAllSalaryAdjustments();
        bool Exists(int id, string code);
        IEnumerable<SalaryAdjustmentDTO> GetSalaryAdjustments(string search = null);
    }
}
