using Microsoft.Extensions.Hosting;
using PayrollSystem.UI.Contracts.Activation;
using PayrollSystem.UI.Contracts.Services;
using PayrollSystem.UI.Contracts.Views;
using PayrollSystem.UI.ViewModels;
using PayrollSystem.UI.Views;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using System.Windows;
using Microsoft.Extensions.DependencyInjection;
using PayrollSystem.Logic.Contracts;

namespace PayrollSystem.UI.Services
{
    public class ApplicationHostService : IHostedService
    {
        private readonly IServiceProvider serviceProvider;
        private readonly INavigationService navigationService;
        private readonly IEnumerable<IActivationHandler> activationHandlers;
        private IShellWindow shellWindow;
        private bool _isInitialized;

        public ApplicationHostService(IServiceProvider serviceProvider, IEnumerable<IActivationHandler> activationHandlers, INavigationService navigationService)
        {
            this.serviceProvider = serviceProvider;
            this.activationHandlers = activationHandlers;
            this.navigationService = navigationService;
        }

        public async Task StartAsync(CancellationToken cancellationToken)
        {
            await InitializeAsync();

            await HandleActivationAsync();

            await StartupAsync();
            _isInitialized = true;
        }

        public async Task StopAsync(CancellationToken cancellationToken)
        {
            await Task.CompletedTask;
        }

        private async Task InitializeAsync()
        {
            if (!_isInitialized)
            {
                await Task.CompletedTask;
            }
        }

        private async Task StartupAsync()
        {
            if (!_isInitialized)
            {
                await Task.CompletedTask;
            }
        }

        private async Task HandleActivationAsync()
        {
            var activationHandler = activationHandlers.FirstOrDefault(h => h.CanHandle());

            if (activationHandler != null)
            {
                await activationHandler.HandleAsync();
            }

            await Task.CompletedTask;

            if (!Application.Current.Windows.OfType<IShellWindow>().Any())
            {
                shellWindow = serviceProvider.GetService<IShellWindow>();
                navigationService.Initialize(shellWindow.GetNavigationFrame());
                shellWindow.ShowWindow();
                navigationService.NavigateTo(typeof(EmployeeViewModel).FullName);

                await Task.CompletedTask;
            }
        }
    }
}
