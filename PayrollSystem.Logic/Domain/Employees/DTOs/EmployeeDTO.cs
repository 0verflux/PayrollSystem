using PayrollSystem.Logic.Domain.PayrollEntries;
using PayrollSystem.Logic.Domain.PayrollEntries.DTOs;
using PayrollSystem.Logic.Domain.Positions.DTOs;
using System;
using System.Collections.Generic;

namespace PayrollSystem.Logic.Domain.Employees.DTOs
{
    public class EmployeeDTO
    {
        public int ID { get; set; }
        public string Name { get; set; }
        public string Address { get; set; }
        public char Gender { get; set; }
        public DateTime BirthDate { get; set; }
        public DateTime EmploymentStartDate { get; set; }
        public DateTime? EmploymentEndDate { get; set; }
        public int? PositionID { get; set; }
        public string? Position { get; set; }
        public List<PayrollEntryDetailsDTO> PayrollEntries { get; set; }
    }
}
