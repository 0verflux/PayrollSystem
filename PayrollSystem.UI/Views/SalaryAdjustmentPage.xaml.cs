using PayrollSystem.Logic.Contracts;
using PayrollSystem.Logic.Domain.SalaryAdjustmentDetails.DTOs;
using PayrollSystem.UI.Contracts.Services;
using PayrollSystem.UI.Contracts.ViewModels;
using PayrollSystem.UI.Helpers;
using PayrollSystem.UI.ViewModels;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;

namespace PayrollSystem.UI.Views
{
    /// <summary>
    /// Interaction logic for SalaryAdjustmentDetailPage.xaml
    /// </summary>
    public partial class SalaryAdjustmentPage : Page
    {
        public SalaryAdjustmentPage(SalaryAdjustmentViewModel viewModel)
        {
            InitializeComponent();
            DataContext = viewModel;
        }
    }
}
