using Microsoft.Toolkit.Mvvm.ComponentModel;
using Microsoft.Toolkit.Mvvm.Input;
using Microsoft.Toolkit.Mvvm.Messaging;
using Microsoft.Toolkit.Mvvm.Messaging.Messages;
using PayrollSystem.Logic.Contracts;
using PayrollSystem.Logic.Domain.SalaryAdjustmentDetails.DTOs;
using PayrollSystem.UI.Messages;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PayrollSystem.UI.ViewModels.Entities
{
    public class SalaryAdjustmentDetailViewModel : ObservableObject, IRecipient<TotalWorkPayChangedMessage>
    {
        private readonly IPayrollManager payrollManager;
        private decimal totalWorkPay;

        public SalaryAdjustmentDetailDTO Self { get; set; }

        public string DetailedInformation
        {
            get
            {
                var code = Self.SalaryAdjustment.Code;
                var desc = Self.SalaryAdjustment.Description;

                return code + (!string.IsNullOrWhiteSpace(desc) ? $" ({desc})" : null);
            }
        }

        public float Percentage
        {
            get => Self.Percentage;
            set
            {
                if (SetProperty(Self.Percentage, value, Self, (e, v) => e.Percentage = v))
                {
                    ModifyValue();
                }
            }
        }

        public decimal Value
        {
            get => Self.Value;
            set
            {
                if (SetProperty(Self.Value, value, Self, (e, v) => e.Value = v))
                {
                    ModifyPercentage();
                }
            }
        }

        public SalaryAdjustmentDetailViewModel(IPayrollManager payrollManager, SalaryAdjustmentDTO salaryAdjustment) : this()
        {
            this.payrollManager = payrollManager;

            Self = new() { SalaryAdjustment = salaryAdjustment };
        }

        private SalaryAdjustmentDetailViewModel()
        {
            WeakReferenceMessenger.Default.Register(this);
        }

        private void ModifyPercentage()
        {
            if (Self.Value == 0m)
                return;

            Self.Percentage = payrollManager.CalculatePercentage(totalWorkPay, Self.Value);
            OnPropertyChanged(nameof(Percentage));
        }

        private void ModifyValue()
        {
            if (Self.Percentage == 0f)
                return;

            Self.Value = payrollManager.CalculateValue(totalWorkPay, Self.Percentage);
            OnPropertyChanged(nameof(Value));
        }

        public void Receive(TotalWorkPayChangedMessage message)
        {
            totalWorkPay = message.Value;
            ModifyPercentage();
            ModifyValue();
        }
    }
}
