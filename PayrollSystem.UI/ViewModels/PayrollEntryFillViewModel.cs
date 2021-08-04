using AutoMapper;
using Microsoft.Toolkit.Mvvm.ComponentModel;
using Microsoft.Toolkit.Mvvm.Input;
using Microsoft.Toolkit.Mvvm.Messaging;
using PayrollSystem.Logic.Contracts;
using PayrollSystem.Logic.Domain.Employees.DTOs;
using PayrollSystem.Logic.Domain.PayrollEntries;
using PayrollSystem.Logic.Domain.PayrollEntries.DTOs;
using PayrollSystem.Logic.Domain.SalaryAdjustmentDetails.DTOs;
using PayrollSystem.UI.Contracts.Services;
using PayrollSystem.UI.Contracts.ViewModels;
using PayrollSystem.UI.Helpers;
using PayrollSystem.UI.Messages;
using PayrollSystem.UI.Validations;
using PayrollSystem.UI.ViewModels.Entities;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Input;

namespace PayrollSystem.UI.ViewModels
{
    public class PayrollEntryFillViewModel : ObservableValidator, INavigationAware
    {
        private readonly INavigationService navigationService;
        private readonly ISalaryAdjustmentManager salaryAdjustmentManager;
        private readonly IPayrollManager payrollManager;
        private readonly IMapper mapper;

        private decimal totalWorkPay;
        private decimal totalNetPay;
        private decimal totalAdjustments;
        private CreatePayrollEntryDTO payrollEntry;
        private SalaryAdjustmentDTO selectedSalaryAdjustment;
        private SalaryAdjustmentDetailViewModel selectedSalaryAdjustmentDetail;

        public decimal TotalAdjustments
        {
            get => totalAdjustments;
            set => SetProperty(ref totalAdjustments, value);
        }
        public decimal TotalNetPay
        {
            get => totalNetPay;
            set => SetProperty(ref totalNetPay, value);
        }
        public decimal TotalWorkPay
        {
            get
            {
                totalWorkPay = payrollManager.RecomputeTotalPay(RatePerHour, payrollEntry.HoursWorked, payrollEntry.HoursOvertime);
                WeakReferenceMessenger.Default.Send(new TotalWorkPayChangedMessage(totalWorkPay));
                return totalWorkPay;
            }
        }
        public string EmployeeName => payrollEntry.EmployeeName;
        public string CurrentPosition => payrollEntry.CurrentPositionName;
        public decimal RatePerHour => payrollEntry.RatePerHour;


        [NaturalNumber(ErrorMessage = "Hours Worked must be more than zero!")]
        public float HoursWorked
        {
            get => payrollEntry.HoursWorked;
            set
            {
                if (SetProperty(payrollEntry.HoursWorked, value, payrollEntry, (e, v) => e.HoursWorked = v, true))
                {
                    OnPropertyChanged(nameof(TotalWorkPay));
                }
            }
        }
        public float HoursOvertime
        {
            get => payrollEntry.HoursOvertime;
            set
            {
                if (SetProperty(payrollEntry.HoursOvertime, value, payrollEntry, (e, v) => e.HoursOvertime = v, true))
                {
                    OnPropertyChanged(nameof(TotalWorkPay));
                }
            }
        }
        public DateTime PayPeriodStartDate
        {
            get => payrollEntry.PayPeriodStart;
            set
            {
                SetProperty(payrollEntry.PayPeriodStart, value, payrollEntry, (e, v) => e.PayPeriodStart = v, true);
                PayPeriodEndDate = value.AddMonths(1);
            }
        }
        [LessThanDate(ErrorMessage = "Pay Period End Date should be more than the Start Date!")]
        public DateTime PayPeriodEndDate
        {
            get => payrollEntry.PayPeriodEnd;
            set => SetProperty(payrollEntry.PayPeriodEnd, value, payrollEntry, (e, v) => e.PayPeriodEnd = v, true);
        }
        

        public IRelayCommand GoBackCommand { get; }
        public IRelayCommand InsertSalaryAdjustmentCommand { get; }
        public IRelayCommand RemoveSalaryAdjustmentCommand { get; }
        public IRelayCommand AddPayrollEntryCommand { get; }

        public SalaryAdjustmentDTO SelectedSalaryAdjustment
        {
            get => selectedSalaryAdjustment;
            set
            {
                if (SetProperty(ref selectedSalaryAdjustment, value))
                {
                    InsertSalaryAdjustmentCommand.NotifyCanExecuteChanged();
                }
            }
        }
        public SalaryAdjustmentDetailViewModel SelectedSalaryAdjustmentDetail
        {
            get => selectedSalaryAdjustmentDetail;
            set
            {
                if (SetProperty(ref selectedSalaryAdjustmentDetail, value))
                {
                    RemoveSalaryAdjustmentCommand.NotifyCanExecuteChanged();
                }
            }
        }
        public ObservableCollection<SalaryAdjustmentDTO> SalaryAdjustmentList { get; set; }
        public BindingList<SalaryAdjustmentDetailViewModel> SalaryAdjustmentDetailList { get; }
        public PayrollEntryFillViewModel(INavigationService navigationService, IMapper mapper, IPayrollManager payrollManager, ISalaryAdjustmentManager salaryAdjustmentManager) : this()
        {
            this.navigationService = navigationService;
            this.mapper = mapper;
            this.payrollManager = payrollManager;
            this.salaryAdjustmentManager = salaryAdjustmentManager;
        }

        private PayrollEntryFillViewModel()
        {
            GoBackCommand = new RelayCommand(GoBack);
            InsertSalaryAdjustmentCommand = new RelayCommand(InsertSalaryAdjustment, CanInsertSalaryAdjustment);
            RemoveSalaryAdjustmentCommand = new RelayCommand(RemoveSalaryAdjustment, CanRemoveSalaryAdjustment);
            AddPayrollEntryCommand = new RelayCommand(AddPayrollEntry);

            payrollEntry = new();
            SalaryAdjustmentList = new();
            SalaryAdjustmentDetailList = new();

            SalaryAdjustmentDetailList.ListChanged += UpdateReports;
        }

        private bool CanInsertSalaryAdjustment()
        {
            return selectedSalaryAdjustment != null;
        }
        private bool CanRemoveSalaryAdjustment()
        {
            return selectedSalaryAdjustmentDetail != null;
        }

        private void AddPayrollEntry()
        {
            ValidateAllProperties();

            if (HasErrors)
                return;

            try
            {
                payrollManager.AddPayrollEntry(payrollEntry, SalaryAdjustmentDetailList.Select(e => e.Self).ToList());

                MessageBox.Show("Payroll Entry Added.");
                navigationService.NavigateTo(typeof(PayrollEntrySelectViewModel).FullName, clearNavigation: true);
            }
            catch (Exception ex)
            {
                AlertBox.ShowError(ex);
            }
        }

        private void UpdateReports(object s, ListChangedEventArgs e)
        {
            TotalAdjustments = SalaryAdjustmentDetailList.Sum(e => e.Value);
            TotalNetPay = totalWorkPay + totalAdjustments;
        }

        private void InsertSalaryAdjustment()
        {
            SalaryAdjustmentDetailList.Add(new(payrollManager, SelectedSalaryAdjustment));
            SalaryAdjustmentList.Remove(SelectedSalaryAdjustment);
            WeakReferenceMessenger.Default.Send(new TotalWorkPayChangedMessage(totalWorkPay));
        }

        private void RemoveSalaryAdjustment()
        {
            var salaryAdjustmentDetail = selectedSalaryAdjustmentDetail;
            SalaryAdjustmentDetailList.Remove(salaryAdjustmentDetail);
            SalaryAdjustmentList.Add(salaryAdjustmentDetail.Self.SalaryAdjustment);
        }

        private void GoBack()
        {
            navigationService.NavigateTo(typeof(PayrollEntrySelectViewModel).FullName, clearNavigation: true);
        }

        private void LoadSalaryAdjustments()
        {
            SalaryAdjustmentList.Clear();

            var data = salaryAdjustmentManager.GetAllSalaryAdjustments();

            foreach(var item in data)
            {
                SalaryAdjustmentList.Add(item);
            }
        }

        #region INavigationAware Implementation
        public void OnNavigatedTo(object parameter)
        {
            var employee = parameter as EmployeeMiniDTO;
            payrollEntry = mapper.Map<CreatePayrollEntryDTO>(employee);

            LoadSalaryAdjustments();
        }

        public void OnNavigatedFrom()
        {
            SalaryAdjustmentDetailList.ListChanged -= UpdateReports;
        }
        #endregion
    }
}
