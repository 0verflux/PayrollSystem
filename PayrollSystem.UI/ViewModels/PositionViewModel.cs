using Microsoft.Toolkit.Mvvm.ComponentModel;
using Microsoft.Toolkit.Mvvm.Input;
using PayrollSystem.Logic.Contracts;
using PayrollSystem.Logic.Domain.Positions.DTOs;
using PayrollSystem.UI.Common;
using PayrollSystem.UI.Contracts.Services;
using PayrollSystem.UI.Contracts.ViewModels;
using PayrollSystem.UI.Helpers;
using System;
using System.Collections.ObjectModel;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Input;

namespace PayrollSystem.UI.ViewModels
{
    public class PositionViewModel : ObservableObject, INavigationAware
    {
        private readonly INavigationService navigationService;
        private readonly IPositionManager positionManager;

        private string searchedText;
        private PositionDTO selectedPosition;

        public string SearchedText
        {
            get => searchedText;
            set => SetProperty(ref searchedText, value);
        }
        public PositionDTO SelectedPosition
        {
            get => selectedPosition;
            set
            {
                SetProperty(ref selectedPosition, value);
                NotifyCanExecute();
            }
        }
        public ObservableCollection<PositionDTO> PositionList { get; }

        public ICommand SearchBoxTextChangedCommand { get; }
        public ICommand SearchBoxOnEnterCommand { get; }
        public ICommand ClearSearchBoxCommand { get; }
        public ICommand FilterPositionsCommand { get; }
        public ICommand AddPositionCommand { get; }
        public ICommand EditPositionCommand { get; }
        public ICommand DeletePositionCommand { get; }

        public PositionViewModel(INavigationService navigationService, IPositionManager positionManager) : this()
        {
            this.navigationService = navigationService;
            this.positionManager = positionManager;
        }

        private PositionViewModel()
        {
            SearchBoxOnEnterCommand = new RelayCommand(LoadPositions);
            SearchBoxTextChangedCommand = new RelayCommand(LoadPositions, CanFilterPositionsOnTextChanged);
            FilterPositionsCommand = new RelayCommand(LoadPositions);
            ClearSearchBoxCommand = new RelayCommand(ClearSearchBox);
            AddPositionCommand = new RelayCommand(() => ModifyOnClick(ModifyState.Add));
            EditPositionCommand = new RelayCommand(() => ModifyOnClick(ModifyState.Edit), CanExecute);
            DeletePositionCommand = new RelayCommand(DeletePositionOnClick, CanExecute);

            PositionList = new();
        }

        private bool CanFilterPositionsOnTextChanged() => (searchedText?.Length ?? default) == 0;
        private void ClearSearchBox() => SearchedText = string.Empty;
        private bool CanExecute() => selectedPosition != null;

        private void ModifyOnClick(ModifyState state)
        {
            var position = state switch
            {
                ModifyState.Add => null,
                ModifyState.Edit => selectedPosition,
                _ => throw new NotImplementedException()
            };

            navigationService.NavigateTo(typeof(ModifyPositionViewModel).FullName, position, true);
        }

        private void DeletePositionOnClick()
        {
            if (AlertBox.ShowWarning(Properties.Resources.WarningDeletePositionMessage) == MessageBoxResult.Yes)
            {
                try
                {
                    positionManager.DeletePosition(selectedPosition);
                    LoadPositions();
                }
                catch (Exception ex)
                {
                    AlertBox.ShowError(ex);
                }
            }
        }

        private void LoadPositions()
        {
            PositionList.Clear();

            var data = positionManager.GetPositions(searchedText);

            foreach (var item in data)
            {
                PositionList.Add(item);
            }
        }

        private void NotifyCanExecute()
        {
            (EditPositionCommand as RelayCommand).NotifyCanExecuteChanged();
            (DeletePositionCommand as RelayCommand).NotifyCanExecuteChanged();
        }

        #region INavigationAware Implementation
        public async void OnNavigatedTo(object parameter)
        {
            await Task.CompletedTask;
            LoadPositions();
            NotifyCanExecute();
        }
        public void OnNavigatedFrom()
        {

        }
        #endregion
    }
}
