using Microsoft.Toolkit.Mvvm.Messaging.Messages;
using PayrollSystem.Logic.Domain.Employees.DTOs;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PayrollSystem.UI.Messages
{
    public class SelectedEmployeeChangedMessage : ValueChangedMessage<EmployeeDTO>
    {
        public SelectedEmployeeChangedMessage(EmployeeDTO value) : base(value)
        {
        }
    }
}
