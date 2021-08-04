using PayrollSystem.Logic.Contracts;
using PayrollSystem.Logic.Domain.Positions.DTOs;
using PayrollSystem.UI.Contracts.Services;
using PayrollSystem.UI.Contracts.ViewModels;
using PayrollSystem.UI.Helpers;
using PayrollSystem.UI.ViewModels;
using System;
using System.Collections;
using System.Collections.Generic;
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
    /// Interaction logic for ModifyPositionPage.xaml
    /// </summary>
    public partial class ModifyPositionPage : Page
    {
        public ModifyPositionPage(ModifyPositionViewModel viewModel)
        {
            InitializeComponent();
            DataContext = viewModel;
        }
    }
}
