using AutoMapper;
using PayrollSystem.Logic.Common;
using PayrollSystem.Logic.Domain;
using PayrollSystem.Logic.Domain.Employees;
using PayrollSystem.Logic.Domain.Employees.DTOs;
using PayrollSystem.Logic.Domain.PayrollEntries;
using PayrollSystem.Logic.Domain.PayrollEntries.DTOs;
using PayrollSystem.Logic.Domain.Positions;
using PayrollSystem.Logic.Domain.Positions.DTOs;
using PayrollSystem.Logic.Domain.SalaryAdjustmentDetails;
using PayrollSystem.Logic.Domain.SalaryAdjustmentDetails.DTOs;
using PayrollSystem.Logic.Domain.SalaryAdjustments;
using System;
using System.Collections.Generic;
using System.Linq;

namespace PayrollSystem.Logic.Mappers
{
    internal class AutoMapperProfile : Profile
    {
        public AutoMapperProfile()
        {
            AllowNullCollections = true;
            AllowNullDestinationValues = true;

            CreateMap<Employee, EmployeeDTO>()
                .ForMember(d => d.ID, o => o.MapFrom(s => s.ID))
                .ForMember(d => d.Name, o => o.MapFrom(s => s.PersonalInformation.FullName))
                .ForMember(d => d.Address, o => o.MapFrom(s => s.PersonalInformation.Address))
                .ForMember(d => d.Gender, o => o.MapFrom(s => s.PersonalInformation.Gender))
                .ForMember(d => d.BirthDate, o => o.MapFrom(s => s.PersonalInformation.BirthDate))
                .ForMember(d => d.EmploymentStartDate, o => o.MapFrom(s => s.EmploymentDate.Start))
                .ForMember(d => d.EmploymentEndDate, o => o.MapFrom(s => s.EmploymentDate.End))
                .ForMember(d => d.PositionID, o => o.MapFrom(s => s.Position.ID))
                .ForMember(d => d.Position, o => o.MapFrom(s => s.Position != null ? s.Position.Name : "(none)"))
                .ForMember(d => d.PayrollEntries, o => o.MapFrom(s => s.PayrollEntries.ToList()))
                .ReverseMap()
                .ForPath(d => d.PayrollEntries, o => o.Ignore())
                .ConvertUsing(s => EntityFactory.ConstructEmployee(s));

            CreateMap<Employee, EmployeeMiniDTO>()
                .ForMember(d => d.ID, o => o.MapFrom(s => s.ID))
                .ForMember(d => d.Name, o => o.MapFrom(s => s.PersonalInformation.FullName))
                .ForMember(d => d.Position, o => o.MapFrom(s => s.Position));

            CreateMap<Position, PositionDTO>()
                .ForMember(d => d.ID, o => o.MapFrom(s => s.ID))
                .ForMember(d => d.Name, o => o.MapFrom(s => s.Name))
                .ForMember(d => d.RatePerHour, o => o.MapFrom(s => s.RatePerHour))
                .ReverseMap()
                .ConvertUsing(s => EntityFactory.ConstructPosition(s));

            CreateMap<Position, PositionMiniDTO>()
                .ForMember(d => d.ID, o => o.MapFrom(s => s.ID))
                .ForMember(d => d.Name, o => o.MapFrom(s => s.Name))
                .ForMember(d => d.RatePerHour, o => o.MapFrom(s => s.RatePerHour))
                .ForMember(d => d.Employees, o => o.MapFrom(s => s.Employees));

            CreateMap<SalaryAdjustment, SalaryAdjustmentDTO>()
                .ForMember(d => d.ID, o => o.MapFrom(s => s.ID))
                .ForMember(d => d.Code, o => o.MapFrom(s => s.Code))
                .ForMember(d => d.Description, o => o.MapFrom(s => s.Description))
                .ReverseMap()
                .ConvertUsing(s => EntityFactory.ConstructSalaryAdjustment(s));

            CreateMap<SalaryAdjustmentDetail, SalaryAdjustmentDetailDTO>()
                .ForMember(d => d.ID, o => o.MapFrom(s => s.ID))
                .ForMember(d => d.SalaryAdjustment, o => o.MapFrom(s => s.SalaryAdjustment))
                .ForMember(d => d.Percentage, o => o.MapFrom(s => s.Percentage.Ratio))
                .ForMember(d => d.Value, o => o.MapFrom(s => s.Percentage.Value))
                .ReverseMap()
                .ConvertUsing(s => EntityFactory.ConstructSalaryAdjustmentDetail(s));

            CreateMap<CreatePayrollEntryDTO, PayrollEntry>()
                .ConvertUsing(s => EntityFactory.ConstructPayrollEntry(s));

            CreateMap<EmployeeMiniDTO, CreatePayrollEntryDTO>()
                .ForMember(d => d.EmployeeID, o => o.MapFrom(s => s.ID))
                .ForMember(d => d.EmployeeName, o => o.MapFrom(s => s.Name))
                .ForMember(d => d.CurrentPositionID, o => o.MapFrom(s => s.Position.ID))
                .ForMember(d => d.CurrentPositionName, o => o.MapFrom(s => s.Position.Name))
                .ForMember(d => d.RatePerHour, o => o.MapFrom(s => s.Position.RatePerHour))
                .ForMember(d => d.PayPeriodStart, o => o.MapFrom(s => DateTime.Now))
                .ForMember(d => d.PayPeriodEnd, o => o.MapFrom(s => DateTime.Now.AddMonths(1)));

            CreateMap<PayrollEntry, PayrollEntryDetailsDTO>()
                .ForMember(d => d.ID, o => o.MapFrom(s => s.ID))
                .ForMember(d => d.Position, o => o.MapFrom(s => s.CurrentPosition))
                .ForMember(d => d.HoursWorked, o => o.MapFrom(s => s.HoursWorked))
                .ForMember(d => d.HoursOvertime, o => o.MapFrom(s => s.HoursOvertime))
                .ForMember(d => d.PayPeriodStart, o => o.MapFrom(s => s.Date.Start))
                .ForMember(d => d.PayPeriodEnd, o => o.MapFrom(s => s.Date.End))
                .ForMember(d => d.SalaryAdjustmentDetails, o => o.MapFrom(s => s.SalaryAdjustmentDetails.ToList()));
        }
    }
}
