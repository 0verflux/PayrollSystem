using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using PayrollSystem.Logic;
using PayrollSystem.Logic.Application;
using PayrollSystem.Logic.Contracts;
using PayrollSystem.Logic.Domain.Employees;
using PayrollSystem.Logic.Domain.PayrollEntries;
using PayrollSystem.Logic.Domain.SalaryAdjustments;
using PayrollSystem.UI.Contracts.Services;
using PayrollSystem.UI.Contracts.Views;
using PayrollSystem.UI.Services;
using PayrollSystem.UI.ViewModels;
using PayrollSystem.UI.Views;
using System.Globalization;
using System.IO;
using System.Reflection;
using System.Windows;
using System.Windows.Markup;

namespace PayrollSystem.UI
{
    /// <summary>
    /// Interaction logic for App.xaml
    /// </summary>
    public partial class App : Application
    {
        private IHost host;

        public T GetService<T>()
            where T : class
        {
            return host.Services.GetService(typeof(T)) as T;
        }

        public App()
        {
            FrameworkElement.LanguageProperty.OverrideMetadata(typeof(FrameworkElement), new FrameworkPropertyMetadata(XmlLanguage.GetLanguage(CultureInfo.CurrentCulture.IetfLanguageTag)));
        }

        private async void OnStartup(object sender, StartupEventArgs e)
        {
            var appLocation = Path.GetDirectoryName(Assembly.GetEntryAssembly().Location);

            host = Host.CreateDefaultBuilder(e.Args)
                .ConfigureAppConfiguration(c =>
                {
                    c.SetBasePath(appLocation);
                })
                .ConfigureServices(ConfigureServices)
                .Build();

            await host.StartAsync();
        }

        private async void OnExit(object sender, ExitEventArgs e)
        {
            await host.StopAsync();
            host.Dispose();
            host = null;
        }

        private void ConfigureServices(HostBuilderContext context, IServiceCollection services)
        {
            services.AddHostedService<ApplicationHostService>();

            services.AddSingleton<INavigationService, NavigationService>();
            services.AddSingleton<IPageService, PageService>();

            services.AddDependency(@"Data Source = FRANCIS-PC\FDEMIN; Initial Catalog = PayrollDB; Integrated Security = True;");

            services.AddTransient<IShellWindow, ShellWindow>();
            services.AddTransient<ShellViewModel>();

            services.AddTransient<EmployeePage>();
            services.AddTransient<EmployeeViewModel>();

            services.AddTransient<ModifyEmployeePage>();
            services.AddTransient<ModifyEmployeeViewModel>();

            services.AddTransient<PositionPage>();
            services.AddTransient<PositionViewModel>();

            services.AddTransient<ModifyPositionPage>();
            services.AddTransient<ModifyPositionViewModel>();

            services.AddTransient<SalaryAdjustmentPage>();
            services.AddTransient<SalaryAdjustmentViewModel>();

            services.AddTransient<ModifySalaryAdjustmentPage>();
            services.AddTransient<ModifySalaryAdjustmentViewModel>();

            services.AddTransient<PayrollEntrySelectPage>();
            services.AddTransient<PayrollEntrySelectViewModel>();

            services.AddTransient<PayrollEntryFillPage>();
            services.AddTransient<PayrollEntryFillViewModel>();

            services.AddTransient<PayrollReportsPage>();
            services.AddTransient<PayrollReportsViewModel>();
        }
    }
}
