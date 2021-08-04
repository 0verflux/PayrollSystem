using Microsoft.Toolkit.Mvvm.ComponentModel;
using Microsoft.Toolkit.Mvvm.Input;
using PayrollSystem.Logic.Contracts;
using PayrollSystem.Logic.Domain.SalaryAdjustmentDetails.DTOs;
using PayrollSystem.UI.Contracts.Services;
using PayrollSystem.UI.Contracts.ViewModels;
using PayrollSystem.UI.Contracts.Views;
using PayrollSystem.UI.Helpers;
using PayrollSystem.UI.Validations;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Input;

namespace PayrollSystem.UI.ViewModels
{
    public class ModifySalaryAdjustmentViewModel : ObservableValidator, INavigationAware, IValidationParameter
    {
        private readonly INavigationService navigationService;
        private readonly ISalaryAdjustmentManager salaryAdjustmentManager;

        private bool isModified;
        private string pageTitle;
        private SalaryAdjustmentDTO salaryAdjustment;

        public bool ModifyState { get; private set; }
        public string PageTitle
        {
            get => pageTitle;
            set => SetProperty(ref pageTitle, value);
        }

        [Required(ErrorMessage = "Please provide a codename.")]
        [UniqueSalaryAdustmentCode(ErrorMessage = "Codename already exist.")]
        public string SalaryAdjustmentCode
        {
            get => salaryAdjustment.Code;
            set => isModified = SetProperty(salaryAdjustment.Code, value, salaryAdjustment, (e, v) => e.Code = value, true);
        }
        public string SalaryAdjustmentDescription
        {
            get => salaryAdjustment.Description;
            set => isModified = SetProperty(salaryAdjustment.Description, value, salaryAdjustment, (e, v) => e.Description = v);
        }

        public ICommand CloseCommand { get; }
        public ICommand SaveCommand { get; }

        public ModifySalaryAdjustmentViewModel(INavigationService navigationService, ISalaryAdjustmentManager salaryAdjustmentManager, IServiceProvider serviceProvider) : this(serviceProvider)
        {
            this.navigationService = navigationService;
            this.salaryAdjustmentManager = salaryAdjustmentManager;
        }

        private ModifySalaryAdjustmentViewModel(IServiceProvider serviceProvider) : base(serviceProvider, null)
        {
            CloseCommand = new RelayCommand(CloseOnClick);
            SaveCommand = new RelayCommand(SaveOnClick);
            
            salaryAdjustment = new();
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

            NavigateBackToPositionPage();
        }

        private void SaveOnClick()
        {
            ValidateAllProperties();

            if (HasErrors)
                return;

            try
            {
                if (ModifyState)
                    salaryAdjustmentManager.UpdateSalaryAdjustment(salaryAdjustment);
                else
                    salaryAdjustmentManager.CreateSalaryAdjustment(salaryAdjustment);

                NavigateBackToPositionPage();
            }
            catch (Exception ex)
            {
                AlertBox.ShowError(ex);
            }
        }

        private void NavigateBackToPositionPage()
        {
            navigationService.NavigateTo(pageKey: typeof(SalaryAdjustmentViewModel).FullName, clearNavigation: true);
        }

        #region INavigationAware Implementation
        public async void OnNavigatedTo(object parameter)
        {
            await Task.CompletedTask;

            ModifyState = parameter is SalaryAdjustmentDTO;
            var selectedSalaryAdjustment = parameter as SalaryAdjustmentDTO;

            PageTitle = ModifyState ? Properties.Resources.EditSalaryAdjustmentPageTitle : Properties.Resources.AddSalaryAdjustmentPageTitle;
            salaryAdjustment = ModifyState ? selectedSalaryAdjustment : new();

            OnPropertyChanged(nameof(SalaryAdjustmentCode));
            OnPropertyChanged(nameof(SalaryAdjustmentDescription));
        }
        public void OnNavigatedFrom()
        {

        }

        public object? GetParameter()
        {
            return salaryAdjustment.ID;
        }

        #endregion
    }
}
