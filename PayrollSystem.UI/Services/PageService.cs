using Ardalis.GuardClauses;
using Microsoft.Toolkit.Mvvm.ComponentModel;
using PayrollSystem.UI.Contracts.Services;
using PayrollSystem.UI.Helpers;
using PayrollSystem.UI.ViewModels;
using PayrollSystem.UI.Views;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Windows.Controls;

namespace PayrollSystem.UI.Services
{
    public class PageService : IPageService
    {
        private readonly Dictionary<string, Type> pages = new();
        private readonly IServiceProvider serviceProvider;

        public PageService(IServiceProvider serviceProvider)
        {
            this.serviceProvider = serviceProvider;

            Configure<EmployeeViewModel, EmployeePage>();
            Configure<ModifyEmployeeViewModel, ModifyEmployeePage>();
            Configure<PositionViewModel, PositionPage>();
            Configure<ModifyPositionViewModel, ModifyPositionPage>();
            Configure<SalaryAdjustmentViewModel, SalaryAdjustmentPage>();
            Configure<ModifySalaryAdjustmentViewModel, ModifySalaryAdjustmentPage>();
            Configure<PayrollEntrySelectViewModel, PayrollEntrySelectPage>();
            Configure<PayrollEntryFillViewModel, PayrollEntryFillPage>();
            Configure<PayrollReportsViewModel, PayrollReportsPage>();
        }

        public Type GetPageType(string key)
        {
            Type pageType = null;
            lock (pages)
            {
                Guard.Against.InvalidPredicate(
                    predicate: !pages.TryGetValue(key, out pageType),
                    propertyName: nameof(key),
                    message: $"Page not found: {key}. Did you forget to call PageService.Configure?");
            }

            return pageType;
        }

        public Page GetPage(string key)
        {
            var pageType = GetPageType(key);
            return serviceProvider.GetService(pageType) as Page;
        }

        private void Configure<VM, V>()
            where VM : ObservableObject
            where V : Page
        {
            lock (pages)
            {
                var key = typeof(VM).FullName;
                Guard.Against.InvalidPredicate(pages.ContainsKey(key), nameof(key), $"The key {key} is already configured in PageService!");

                var type = typeof(V);
                Guard.Against.InvalidPredicate(pages.Any(p => p.Value == type), nameof(type), $"This type is already configured with key {pages.FirstOrDefault(p => p.Value == type).Key}");

                pages.Add(key, type);
            }
        }
    }
}
