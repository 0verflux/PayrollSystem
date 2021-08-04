using Microsoft.Toolkit.Mvvm.ComponentModel;
using Microsoft.Toolkit.Mvvm.Input;
using PayrollSystem.Logic.Contracts;
using PayrollSystem.Logic.Domain.Employees.DTOs;
using PayrollSystem.Logic.Domain.Positions.DTOs;
using PayrollSystem.UI.Contracts.Services;
using PayrollSystem.UI.Contracts.ViewModels;
using System.Collections.ObjectModel;
using System.Linq;
using System.Threading.Tasks;


namespace PayrollSystem.UI.ViewModels
{
    public class PayrollEntrySelectViewModel : ObservableObject, INavigationAware
    {
        private readonly INavigationService navigationService;
        private readonly IPositionManager positionManager;

        private EmployeeMiniDTO selectedEmployee;

        public EmployeeMiniDTO SelectedEmployee
        {
            get => selectedEmployee;
            set
            {
                SetProperty(ref selectedEmployee, value);
                NavigateToFillPayrollEntryCommand.NotifyCanExecuteChanged();
            }
        }

        public ObservableCollection<PositionMiniDTO> PositionList { get; }

        public RelayCommand NavigateToFillPayrollEntryCommand { get; }

        public PayrollEntrySelectViewModel(INavigationService navigationService, IPositionManager positionManager) : this()
        {
            this.navigationService = navigationService;
            this.positionManager = positionManager;
        }

        private PayrollEntrySelectViewModel()
        {
            NavigateToFillPayrollEntryCommand = new RelayCommand(NavigateToFillPayrollEntry, CanNavigate);
            PositionList = new();
        }

        private bool CanNavigate()
        {
            return selectedEmployee != null;
        }
        private void NavigateToFillPayrollEntry()
        {
            navigationService.NavigateTo(typeof(PayrollEntryFillViewModel).FullName, selectedEmployee, true);
        }

        private void LoadData()
        {
            PositionList.Clear();

            var data = positionManager.GetAllPositionsWithActiveEmployees();

            foreach(var item in data)
            {
                PositionList.Add(item);
            }
        }

        #region INavigationAware Implementation
        public async void OnNavigatedTo(object parameter)
        {
            await Task.CompletedTask;
            LoadData();
        }

        public void OnNavigatedFrom()
        {

        }
        #endregion
    }
}
