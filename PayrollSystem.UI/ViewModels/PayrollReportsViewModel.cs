using Microsoft.Toolkit.Mvvm.ComponentModel;
using Microsoft.Toolkit.Mvvm.Input;
using PayrollSystem.Logic.Contracts;
using PayrollSystem.Logic.Domain.Employees.DTOs;
using PayrollSystem.Logic.Domain.PayrollEntries.DTOs;
using PayrollSystem.Logic.Domain.Positions.DTOs;
using PayrollSystem.UI.Contracts.ViewModels;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Threading.Tasks;

namespace PayrollSystem.UI.ViewModels
{
    public class PayrollReportsViewModel : ObservableObject, INavigationAware
    {
        private readonly IPositionManager positionManager;
        private readonly IEmployeeManager employeeManager;
        private readonly IPayrollManager payrollManager;

        private decimal totalWorkPay;
        private decimal totalNetPay;
        private string searchedText;
        private int selectedPositionID;
        private EmployeeDTO selectedEmployee;
        private PayrollEntryDetailsDTO selectedPayrollEntry;

        public decimal TotalWorkPay
        {
            get => totalWorkPay;
            set => SetProperty(ref totalWorkPay, value);
        }
        public decimal TotalNetPay
        {
            get => totalNetPay;
            set => SetProperty(ref totalNetPay, value);
        }

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
            set => SetProperty(ref selectedEmployee, value);
        }
        public PayrollEntryDetailsDTO SelectedPayrollEntry
        {
            get => selectedPayrollEntry;
            set
            {
                if (SetProperty(ref selectedPayrollEntry, value))
                {
                    if (selectedPayrollEntry != null)
                    {
                        TotalWorkPay = payrollManager.RecomputeTotalPay(selectedPayrollEntry.Position.RatePerHour, selectedPayrollEntry.HoursWorked, selectedPayrollEntry.HoursOvertime);
                        TotalNetPay = totalWorkPay + selectedPayrollEntry.SalaryAdjustmentDetails.Sum(e => e.Value);
                    }
                }
            }
        }
        public List<PositionDTO> PositionList { get; }
        public ObservableCollection<EmployeeDTO> EmployeeList { get; }

        public IRelayCommand SearchBoxTextChangedCommand { get; }
        public IRelayCommand SearchBoxOnEnterCommand { get; }
        public IRelayCommand ClearSearchBoxCommand { get; }
        public IRelayCommand FilterEmployeesCommand { get; }

        public PayrollReportsViewModel(IPositionManager positionManager, IEmployeeManager employeeManager, IPayrollManager payrollManager) : this()
        {
            this.positionManager = positionManager;
            this.employeeManager = employeeManager;
            this.payrollManager = payrollManager;
        }
        public PayrollReportsViewModel()
        {
            SearchBoxOnEnterCommand = new RelayCommand(LoadEmployees);
            SearchBoxTextChangedCommand = new RelayCommand(LoadEmployees, CanFilterEmployeesOnTextChanged);
            FilterEmployeesCommand = new RelayCommand(LoadEmployees);
            ClearSearchBoxCommand = new RelayCommand(ClearSearchBox);

            PositionList = new();
            EmployeeList = new();
        }

        private bool CanFilterEmployeesOnTextChanged() => (searchedText?.Length ?? default) == 0;
        private void ClearSearchBox() => SearchedText = string.Empty;

        private void LoadPositions()
        {
            PositionList.Clear();

            PositionList.Add(new PositionDTO { ID = 0, Name = "All" });
            PositionList.Add(new PositionDTO { ID = -1, Name = "None" });
            PositionList.AddRange(positionManager.GetAllPositions());
            SelectedPositionID = PositionList.First().ID;
        }
        private void LoadEmployees()
        {
            EmployeeList.Clear();
            var data = employeeManager.GetEmployees(searchedText, selectedPositionID, true, true, true);

            foreach (var item in data)
            {
                EmployeeList.Add(item);
            }
        }

        #region INavigationAware Implementation
        public async void OnNavigatedTo(object parameter)
        {
            await Task.CompletedTask;
            LoadPositions();
            LoadEmployees();
        }
        public void OnNavigatedFrom()
        {

        }
        #endregion
    }
}
