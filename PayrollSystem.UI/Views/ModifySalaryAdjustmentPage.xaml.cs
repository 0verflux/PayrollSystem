using PayrollSystem.Logic.Contracts;
using PayrollSystem.Logic.Domain.SalaryAdjustmentDetails.DTOs;
using PayrollSystem.UI.Contracts.Services;
using PayrollSystem.UI.Contracts.ViewModels;
using PayrollSystem.UI.Helpers;
using PayrollSystem.UI.ViewModels;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Runtime.CompilerServices;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;

namespace PayrollSystem.UI.Views
{
    /// <summary>
    /// Interaction logic for ModifySalaryAdjustmentPage.xaml
    /// </summary>
    public partial class ModifySalaryAdjustmentPage : Page
    {
        public ModifySalaryAdjustmentPage(ModifySalaryAdjustmentViewModel viewModel)
        {
            InitializeComponent();
            DataContext = viewModel;
        }
    }
}
