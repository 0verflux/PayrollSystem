using PayrollSystem.UI.Contracts.Services;
using PayrollSystem.UI.Contracts.ViewModels;
using PayrollSystem.UI.ViewModels;
using System;
using System.Collections;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;

namespace PayrollSystem.UI.Views
{
    /// <summary>
    /// Interaction logic for PayrollEntryFillPage.xaml
    /// </summary>
    public partial class PayrollEntryFillPage : Page
    {
        public PayrollEntryFillPage(PayrollEntryFillViewModel viewModel)
        {
            InitializeComponent();
            DataContext = viewModel;
        }
    }
}
