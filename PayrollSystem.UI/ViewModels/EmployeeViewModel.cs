using Microsoft.Toolkit.Mvvm.ComponentModel;
using Microsoft.Toolkit.Mvvm.Input;
using PayrollSystem.Logic.Contracts;
using PayrollSystem.Logic.Domain.Employees.DTOs;
using PayrollSystem.Logic.Domain.Positions.DTOs;
using PayrollSystem.UI.Common;
using PayrollSystem.UI.Contracts.Services;
using PayrollSystem.UI.Contracts.ViewModels;
using PayrollSystem.UI.Helpers;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Input;

namespace PayrollSystem.UI.ViewModels
{
    public class EmployeeViewModel : ObservableObject, INavigationAware
    {
        private readonly INavigationService navigationService;
        private readonly IEmployeeManager employeeManager;
        private readonly IPositionManager positionManager;

        private string searchedText;
        private int selectedPositionID;
        private EmployeeDTO selectedEmployee;

        public string SearchedText
        {
            get => searchedText;
            set => SetProperty(ref searchedText, value);
        }
        public int SelectedPositionID
        {
            get => selectedPositionID;
            set => SetProperty(ref selectedPositionID, value);
        }
        public EmployeeDTO SelectedEmployee
        {
            get => selectedEmployee;
            set
            {
                SetProperty(ref selectedEmployee, value);
                NotifyCanExecute();
            }
        }
        public List<PositionDTO> PositionList { get; }
        public ObservableCollection<EmployeeDTO> EmployeeList { get; private set; }

        public ICommand SearchBoxTextChangedCommand { get; }
        public ICommand SearchBoxOnEnterCommand { get; }
        public ICommand ClearSearchBoxCommand { get; }
        public ICommand FilterEmployeesCommand { get; }
        public ICommand AddEmployeeCommand { get; }
        public ICommand EditEmployeeCommand { get; }
        public ICommand DeleteEmployeeCommand { get; }
        public ICommand SetEmployeeEndDateCommand { get; }

        public EmployeeViewModel(INavigationService navigationService, IEmployeeManager employeeManager, IPositionManager positionManager) : this()
        {
            this.navigationService = navigationService;
            this.employeeManager = employeeManager;
            this.positionManager = positionManager;
        }

        private EmployeeViewModel()
        {
            SearchBoxOnEnterCommand = new RelayCommand(LoadEmployees);
            SearchBoxTextChangedCommand = new RelayCommand(LoadEmployees, CanFilterEmployeesOnTextChanged);
            FilterEmployeesCommand = new RelayCommand(LoadEmployees);
            ClearSearchBoxCommand = new RelayCommand(ClearSearchBox);
            AddEmployeeCommand = new RelayCommand(() => ModifyOnClick(ModifyState.Add));
            EditEmployeeCommand = new RelayCommand(() => ModifyOnClick(ModifyState.Edit), CanExecute);
            DeleteEmployeeCommand = new RelayCommand(DeleteEmployeeOnClick, CanExecute);
            SetEmployeeEndDateCommand = new RelayCommand(SetEmployeeEndDateOnClick, CanExecute);

            EmployeeList = new();
            PositionList = new();
        }

        private bool CanFilterEmployeesOnTextChanged() => (searchedText?.Length ?? default) == 0;
        private void ClearSearchBox() => SearchedText = string.Empty;
        private bool CanExecute() => selectedEmployee != null;

        private void ModifyOnClick(ModifyState state)
        {
            var employee = state switch
            {
                ModifyState.Add => null,
                ModifyState.Edit => selectedEmployee,
                _ => throw new NotImplementedException()
            };

            navigationService.NavigateTo(typeof(ModifyEmployeeViewModel).FullName, employee, true);
        }
        private void DeleteEmployeeOnClick()
        {
            if (AlertBox.ShowWarning(Properties.Resources.WarningDeleteEmployeeMessage) == MessageBoxResult.Yes)
            {
                try
                {
                    employeeManager.DeleteEmployee(selectedEmployee);
                    LoadEmployees();
                }
                catch (Exception ex)
                {
                    AlertBox.ShowError(ex);
                }
            }
        }
        private void SetEmployeeEndDateOnClick()
        {
            if (AlertBox.ShowWarning(Properties.Resources.WarningMarkEmployeeEndEmploymentMessage) == MessageBoxResult.Yes)
            {
                try
                {
                    employeeManager.MarkAsEndEmploymentByEmployee(selectedEmployee);
                    LoadEmployees();
                }
                catch (Exception ex)
                {
                    AlertBox.ShowError(ex);
                }
            }
        }

        private void LoadEmployees()
        {
            EmployeeList.Clear();

            var data = employeeManager.GetEmployees(searchedText, selectedPositionID, false, true);
            
            foreach(var item in data)
            {
                EmployeeList.Add(item);
            }
        }
        private void LoadPositions()
        {
            PositionList.Clear();
            
            PositionList.Add(new PositionDTO { ID = 0, Name = "All" });
            PositionList.Add(new PositionDTO { ID = -1, Name = "None" });
            PositionList.AddRange(positionManager.GetAllPositions());
            SelectedPositionID = PositionList.First().ID;
        }
        private void NotifyCanExecute()
        {
            (EditEmployeeCommand as RelayCommand).NotifyCanExecuteChanged();
            (DeleteEmployeeCommand as RelayCommand).NotifyCanExecuteChanged();
            (SetEmployeeEndDateCommand as RelayCommand).NotifyCanExecuteChanged();
        }

        #region INavigationAware Implementation
        public async void OnNavigatedTo(object parameter)
        {
            await Task.CompletedTask;
            LoadEmployees();
            LoadPositions();
            NotifyCanExecute();
        }
        public void OnNavigatedFrom()
        {

        }
        #endregion
    }
}
