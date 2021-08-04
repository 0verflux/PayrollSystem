using Microsoft.Extensions.DependencyInjection;
using PayrollSystem.Logic.Contracts;
using PayrollSystem.UI.Contracts.Views;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PayrollSystem.UI.Validations
{
    class UniqueSalaryAdustmentCode : ValidationAttribute
    {
        protected override ValidationResult IsValid(object value, ValidationContext validationContext)
        {
            if (value is string name)
            {
                var boxedID = (validationContext.ObjectInstance as IValidationParameter).GetParameter();

                if (boxedID is int id)
                {
                    var manager = validationContext.GetRequiredService<ISalaryAdjustmentManager>();

                    if (manager.Exists(id, name))
                    {
                        return new ValidationResult(ErrorMessage);
                    }
                }
            }

            return null;
        }
    }
}
