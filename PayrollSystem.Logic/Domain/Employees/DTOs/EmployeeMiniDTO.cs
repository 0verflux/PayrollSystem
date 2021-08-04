using PayrollSystem.Logic.Domain.PayrollEntries;
using PayrollSystem.Logic.Domain.PayrollEntries.DTOs;
using PayrollSystem.Logic.Domain.Positions.DTOs;
using System.Collections.Generic;

namespace PayrollSystem.Logic.Domain.Employees.DTOs
{
    public class EmployeeMiniDTO
    {
        public int ID { get; set; }
        public string Name { get; set; }
        public PositionMiniDTO Position { get; set; }
    }
}
