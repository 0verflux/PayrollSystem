using PayrollSystem.Logic.Domain.Employees.DTOs;
using System.Collections.Generic;

namespace PayrollSystem.Logic.Domain.Positions.DTOs
{
    public class PositionMiniDTO
    {
        public int ID { get; set; }
        public string Name { get; set; }
        public decimal RatePerHour { get; set; }
        public List<EmployeeMiniDTO> Employees { get; set; }
    }
}
