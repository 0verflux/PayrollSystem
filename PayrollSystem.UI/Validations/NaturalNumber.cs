using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PayrollSystem.UI.Validations
{
    public class NaturalNumber : ValidationAttribute
    {
        protected override ValidationResult IsValid(object value, ValidationContext validationContext)
        {
            var result = Convert.ToDecimal(value);

            if(result <= 0)
            {
                return new ValidationResult(ErrorMessage);
            }

            return null;
        }
    }
}
