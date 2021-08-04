using PayrollSystem.Logic.Contracts;
using PayrollSystem.Logic.Domain.Employees.DTOs;
using PayrollSystem.Logic.Domain.Positions.DTOs;
using PayrollSystem.UI.Contracts.Services;
using PayrollSystem.UI.Contracts.ViewModels;
using PayrollSystem.UI.Helpers;
using PayrollSystem.UI.ViewModels;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Runtime.CompilerServices;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;

namespace PayrollSystem.UI.Views
{
    /// <summary>
    /// Interaction logic for ModifyEmployeePage.xaml
    /// </summary>
    public partial class ModifyEmployeePage : Page
    {
        public ModifyEmployeePage(ModifyEmployeeViewModel viewModel)
        {
            InitializeComponent();
            DataContext = viewModel;
        }
    }
}