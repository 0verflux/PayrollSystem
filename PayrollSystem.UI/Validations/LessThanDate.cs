using PayrollSystem.UI.ViewModels;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PayrollSystem.UI.Validations
{
    public class LessThanDate : ValidationAttribute
    {
        protected override ValidationResult IsValid(object value, ValidationContext validationContext)
        {
            if(value is DateTime endDate)
            {
                var startDate = (validationContext.ObjectInstance as PayrollEntryFillViewModel).PayPeriodStartDate;

                if (startDate >= endDate)
                {
                    return new(ErrorMessage);
                }
            }

            return null;
        }
    }
}
