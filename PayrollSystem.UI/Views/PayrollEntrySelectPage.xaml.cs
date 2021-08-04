using PayrollSystem.Logic.Domain.PayrollEntries;
using PayrollSystem.Logic.Domain.PayrollEntries.DTOs;
using PayrollSystem.UI.Contracts.Services;
using PayrollSystem.UI.Contracts.ViewModels;
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

namespace PayrollSystem.UI.Views
{
    /// <summary>
    /// Interaction logic for PayrollEntryPage.xaml
    /// </summary>
    public partial class PayrollEntrySelectPage : Page
    {
        public PayrollEntrySelectPage(PayrollEntrySelectViewModel viewModel)
        {
            InitializeComponent();
            DataContext = viewModel;
        }
    }
}
