using Microsoft.Toolkit.Mvvm.ComponentModel;
using Microsoft.Toolkit.Mvvm.Input;
using PayrollSystem.Logic.Contracts;
using PayrollSystem.Logic.Domain.Positions.DTOs;
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
    public class ModifyPositionViewModel : ObservableValidator, INavigationAware, IValidationParameter
    {
        private readonly INavigationService navigationService;
        private readonly IPositionManager positionManager;

        private bool isModified;
        private string pageTitle;
        private PositionDTO position;

        public bool ModifyState { get; private set; }
        public string PageTitle
        {
            get => pageTitle;
            set => SetProperty(ref pageTitle, value);
        }

        [Required(ErrorMessage = "Please provide a name.")]
        [UniquePositionName(ErrorMessage = "Position name already exist.")]
        public string PositionName
        {
            get => position.Name;
            set => isModified = SetProperty(position.Name, value, position, (e, v) => e.Name = value, true);
        }
        [Required(ErrorMessage = "Please provide a value.")]
        [NaturalNumber(ErrorMessage = "Value entered is less than or equal to zero.")]
        public decimal RatePerHour
        {
            get => position.RatePerHour;
            set => isModified = SetProperty(position.RatePerHour, value, position, (e, v) => e.RatePerHour = v, true);
        }

        public ICommand CloseCommand { get; }
        public ICommand SaveCommand { get; }

        public ModifyPositionViewModel(INavigationService navigationService, IPositionManager positionManager, IServiceProvider serviceProvider) : this(serviceProvider)
        {
            this.navigationService = navigationService;
            this.positionManager = positionManager;
        }

        private ModifyPositionViewModel(IServiceProvider serviceProvider) : base(serviceProvider, null)
        {
            CloseCommand = new RelayCommand(CloseOnClick);
            SaveCommand = new RelayCommand(SaveOnClick);
            
            position = new();
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
                    positionManager.UpdatePosition(position);
                else
                    positionManager.CreatePosition(position);

                NavigateBackToPositionPage();
            }
            catch (Exception ex)
            {
                AlertBox.ShowError(ex);
            }
        }

        private void OnPreviewTextInput(object s, TextCompositionEventArgs e)
        {
            e.Handled = !char.IsDigit(e.Text.Last()) && !(e.Text.Last() == '.');
        }

        private void NavigateBackToPositionPage()
        {
            navigationService.NavigateTo(pageKey: typeof(PositionViewModel).FullName, clearNavigation: true);
        }

        #region INavigationAware Implementation
        public async void OnNavigatedTo(object parameter)
        {
            await Task.CompletedTask;

            ModifyState = parameter is PositionDTO;
            var selectedPosition = parameter as PositionDTO;

            PageTitle = ModifyState ? Properties.Resources.EditPositionPageTitle : Properties.Resources.AddPositionPageTitle;
            position = ModifyState ? selectedPosition : new();

            OnPropertyChanged(nameof(PositionName));
            OnPropertyChanged(nameof(RatePerHour));
        }
        public void OnNavigatedFrom()
        {

        }

        public object? GetParameter()
        {
            return position.ID;
        }
        #endregion
    }
}
