using Microsoft.EntityFrameworkCore;
using Microsoft.Toolkit.Mvvm.ComponentModel;
using Microsoft.Toolkit.Mvvm.Input;
using PayrollSystem.Logic.Contracts;
using PayrollSystem.Logic.Domain.SalaryAdjustmentDetails.DTOs;
using PayrollSystem.UI.Common;
using PayrollSystem.UI.Contracts.Services;
using PayrollSystem.UI.Contracts.ViewModels;
using PayrollSystem.UI.Helpers;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Input;

namespace PayrollSystem.UI.ViewModels
{
    public class SalaryAdjustmentViewModel : ObservableObject, INavigationAware
    {
        private readonly INavigationService navigationService;
        private readonly ISalaryAdjustmentManager salaryAdjustmentManager;

        private string searchedText;
        private SalaryAdjustmentDTO selectedSalaryAdjustment;
        
        public string SearchedText
        {
            get => searchedText;
            set => SetProperty(ref searchedText, value);
        }
        public SalaryAdjustmentDTO SelectedSalaryAdjustment
        {
            get => selectedSalaryAdjustment;
            set
            {
                SetProperty(ref selectedSalaryAdjustment, value);
                NotifyCanExecute();
            }
        }
        public ObservableCollection<SalaryAdjustmentDTO> SalaryAdjustmentList { get; }

        public ICommand SearchBoxTextChangedCommand { get; }
        public ICommand SearchBoxOnEnterCommand { get; }
        public ICommand ClearSearchBoxCommand { get; }
        public ICommand FilterSalaryAdjustmentsCommand { get; }
        public ICommand AddSalaryAdjustmentCommand { get; }
        public ICommand EditSalaryAdjustmentCommand { get; }
        public ICommand DeleteSalaryAdjustmentCommand { get; }

        public SalaryAdjustmentViewModel(INavigationService navigationService, ISalaryAdjustmentManager salaryAdjustmentManager) : this()
        {
            this.navigationService = navigationService;
            this.salaryAdjustmentManager = salaryAdjustmentManager;
        }

        private SalaryAdjustmentViewModel()
        {
            SearchBoxOnEnterCommand = new RelayCommand(LoadSalaryAdjustments);
            SearchBoxTextChangedCommand = new RelayCommand(LoadSalaryAdjustments, CanFilterSalaryAdjustmentsOnTextChanged);
            FilterSalaryAdjustmentsCommand = new RelayCommand(LoadSalaryAdjustments);
            ClearSearchBoxCommand = new RelayCommand(ClearSearchBox);
            AddSalaryAdjustmentCommand = new RelayCommand(() => ModifyOnClick(ModifyState.Add));
            EditSalaryAdjustmentCommand = new RelayCommand(() => ModifyOnClick(ModifyState.Edit), CanExecute);
            DeleteSalaryAdjustmentCommand = new RelayCommand(DeleteSalaryAdjustmentOnClick, CanExecute);

            SalaryAdjustmentList = new();
        }

        private bool CanFilterSalaryAdjustmentsOnTextChanged() => (searchedText?.Length ?? default) == 0;
        private void ClearSearchBox() => SearchedText = string.Empty;
        private bool CanExecute() => selectedSalaryAdjustment != null;

        private void ModifyOnClick(ModifyState state)
        {
            var salaryAdjustment = state switch
            {
                ModifyState.Add => null,
                ModifyState.Edit => selectedSalaryAdjustment,
                _ => throw new NotImplementedException()
            };

            navigationService.NavigateTo(typeof(ModifySalaryAdjustmentViewModel).FullName, salaryAdjustment, true);
        }
        private void DeleteSalaryAdjustmentOnClick()
        {
            if (AlertBox.ShowWarning(Properties.Resources.WarningDeleteSalaryAdjustmentMessage) == MessageBoxResult.Yes)
            {
                try
                {
                    salaryAdjustmentManager.DeleteSalaryAdjustment(selectedSalaryAdjustment);
                    LoadSalaryAdjustments();
                }
                catch (Exception ex)
                {
                    AlertBox.ShowError(ex);
                }
            }
        }

        private void LoadSalaryAdjustments()
        {
            SalaryAdjustmentList.Clear();

            var data = salaryAdjustmentManager.GetSalaryAdjustments(searchedText);
            
            foreach(var item in data)
            {
                SalaryAdjustmentList.Add(item);
            }
        }
        private void NotifyCanExecute()
        {
            (EditSalaryAdjustmentCommand as RelayCommand).NotifyCanExecuteChanged();
            (DeleteSalaryAdjustmentCommand as RelayCommand).NotifyCanExecuteChanged();
        }

        #region INavigationAware Implementation
        public async void OnNavigatedTo(object parameter)
        {
            await Task.CompletedTask;
            LoadSalaryAdjustments();
            NotifyCanExecute();
        }
        public void OnNavigatedFrom()
        {

        }
        #endregion
    }
}
