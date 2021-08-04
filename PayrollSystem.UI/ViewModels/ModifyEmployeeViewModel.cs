using Microsoft.Toolkit.Mvvm.ComponentModel;
using Microsoft.Toolkit.Mvvm.Input;
using PayrollSystem.Logic.Contracts;
using PayrollSystem.Logic.Domain.Employees.DTOs;
using PayrollSystem.Logic.Domain.Positions.DTOs;
using PayrollSystem.UI.Contracts.Services;
using PayrollSystem.UI.Contracts.ViewModels;
using PayrollSystem.UI.Helpers;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Input;

namespace PayrollSystem.UI.ViewModels
{
    public class ModifyEmployeeViewModel : ObservableValidator, INavigationAware
    {
        private readonly INavigationService navigationService;
        private readonly IEmployeeManager employeeManager;
        private readonly IPositionManager positionManager;

        private bool isModified;
        private string pageTitle;
        private EmployeeDTO employee;

        public bool ModifyState { get; private set; }
        public string PageTitle
        {
            get => pageTitle;
            set => SetProperty(ref pageTitle, value);
        }
        [Required(ErrorMessage = "Please provide a Name.")]
        public string EmployeeName
        {
            get => employee.Name;
            set => isModified = SetProperty(employee.Name, value, employee, (e, v) => e.Name = v, true);
        }
        [Required(ErrorMessage = "Please provide an Address.")]
        public string EmployeeAddress
        {
            get => employee.Address;
            set => isModified = SetProperty(employee.Address, value, employee, (e, v) => e.Address = v, true);
        }
        public char EmployeeGender
        {
            get => employee.Gender;
            set => isModified = SetProperty(employee.Gender, value, employee, (e, v) => e.Gender = v, true);
        }
        [Required(ErrorMessage = "Please provide a valid Birth Date.")]
        public DateTime EmployeeBirthDate
        {
            get => employee.BirthDate;
            set => isModified = SetProperty(employee.BirthDate, value, employee, (e, v) => e.BirthDate = v, true);
        }
        [Required(ErrorMessage = "Please provide a valid Date.")]
        public DateTime EmploymentStartDate
        {
            get => employee.EmploymentStartDate;
            set => isModified = SetProperty(employee.EmploymentStartDate, value, employee, (e, v) => e.EmploymentStartDate = v, true);
        }
        public DateTime? EmploymentEndDate
        {
            get => employee.EmploymentEndDate;
            set => isModified = SetProperty(employee.EmploymentEndDate, value, employee, (e, v) => e.EmploymentEndDate = v);
        }
        public bool IsEmploymentLeave
        {
            get => employee.EmploymentEndDate != null;
            set
            {
                EmploymentEndDate = value ? employee.EmploymentEndDate ?? DateTime.Now : null;
                isModified = true;

                OnPropertyChanged();
            }
        }
        [Required(ErrorMessage = "Please provide a Position.")]
        public int? PositionID
        {
            get => employee.PositionID;
            set => isModified = SetProperty(employee.PositionID, value, employee, (e, v) => e.PositionID = v, true);
        }
        public List<PositionDTO> PositionList { get; }

        public ICommand CloseCommand { get; }
        public ICommand SaveCommand { get; }

        public ModifyEmployeeViewModel(INavigationService navigationService, IEmployeeManager employeeManager, IPositionManager positionManager) : this()
        {
            this.navigationService = navigationService;
            this.employeeManager = employeeManager;
            this.positionManager = positionManager;
        }

        private ModifyEmployeeViewModel()
        {
            CloseCommand = new RelayCommand(CloseOnClick);
            SaveCommand = new RelayCommand(SaveOnClick);

            PositionList = new();
            employee = new();

        }

        private void CloseOnClick()
        {
            if (isModified)
            {
                if (AlertBox.ShowWarning(Properties.Resources.WarningLeaveMessage) == MessageBoxResult.No)
                {
                    return;
                }
            }

            NavigateBackToEmployeePage();
        }

        private void SaveOnClick()
        {
            ValidateAllProperties();

            if(HasErrors)
                return;

            try
            {
                if (ModifyState)
                    employeeManager.UpdateEmployee(employee);
                else
                    employeeManager.CreateEmployee(employee);

                NavigateBackToEmployeePage();
            }
            catch(Exception ex)
            {
                AlertBox.ShowError(ex);
            }
        }

        private void LoadPositions()
        {
            PositionList.Clear();
            PositionList.AddRange(positionManager.GetAllPositions());
        }

        private void NavigateBackToEmployeePage()
        {
            navigationService.NavigateTo(pageKey: typeof(EmployeeViewModel).FullName, clearNavigation: true);
        }

        #region INavigationAware Implementation
        public async void OnNavigatedTo(object parameter)
        {
            await Task.CompletedTask;
            LoadPositions();

            ModifyState = parameter is EmployeeDTO;
            var selectedEmployee = parameter as EmployeeDTO;

            PageTitle = ModifyState ? Properties.Resources.EditEmployeePageTitle : Properties.Resources.AddEmployeePageTitle;
            employee = ModifyState ? selectedEmployee : new()
            {
                Gender = 'M',
                BirthDate = DateTime.Now,
                EmploymentStartDate = DateTime.Now
            };

            OnPropertyChanged(nameof(EmployeeName));
            OnPropertyChanged(nameof(EmployeeAddress));
            OnPropertyChanged(nameof(EmployeeGender));
            OnPropertyChanged(nameof(EmployeeBirthDate));
            OnPropertyChanged(nameof(EmploymentStartDate));
            OnPropertyChanged(nameof(EmploymentEndDate));
            OnPropertyChanged(nameof(IsEmploymentLeave));
            OnPropertyChanged(nameof(PositionID));
        }
        public void OnNavigatedFrom()
        {

        }
        #endregion
    }
}
