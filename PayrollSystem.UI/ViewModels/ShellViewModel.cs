using Microsoft.Toolkit.Mvvm.ComponentModel;
using Microsoft.Toolkit.Mvvm.Input;
using PayrollSystem.UI.Contracts.Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;

namespace PayrollSystem.UI.ViewModels
{
    public class ShellViewModel : ObservableObject
    {
        private readonly INavigationService navigationService;

        public ICommand NavigatePageCommand { get; }
        public ICommand LoadedCommand { get; }
        public ICommand UnloadedCommand { get; }

        public ShellViewModel(INavigationService navigationService) : this()
        {
            this.navigationService = navigationService;
        }

        private ShellViewModel()
        {
            NavigatePageCommand = new RelayCommand<Type>(NavigateTo);
            LoadedCommand = new RelayCommand(OnLoaded);
            UnloadedCommand = new RelayCommand(OnUnloaded);
        }

        private void OnNavigated(object sender, string viewModelName)
        {

        }

        private void NavigateTo(Type vmType)
        {
            navigationService.NavigateTo(pageKey: vmType.FullName, clearNavigation: true);
        }

        private void OnLoaded()
        {
            navigationService.Navigated += OnNavigated;
        }

        private void OnUnloaded()
        {
            navigationService.Navigated -= OnNavigated;
        }
    }
}
