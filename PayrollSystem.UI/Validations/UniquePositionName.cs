using PayrollSystem.Logic.Application;
using PayrollSystem.Logic.Contracts;
using System.ComponentModel.DataAnnotations;
using Microsoft.Extensions.DependencyInjection;
using PayrollSystem.Logic.Domain.Positions.DTOs;
using PayrollSystem.UI.ViewModels;
using PayrollSystem.UI.Contracts.Views;

namespace PayrollSystem.UI.Validations
{
    public class UniquePositionName : ValidationAttribute
    {
        protected override ValidationResult IsValid(object value, ValidationContext validationContext)
        {
            if (value is string name)
            {
                var boxedID = (validationContext.ObjectInstance as IValidationParameter).GetParameter();

                if(boxedID is int id)
                {
                    var manager = validationContext.GetRequiredService<IPositionManager>();

                    if (manager.PositionExists(id, name))
                    {
                        return new ValidationResult(ErrorMessage);
                    }
                }
            }

            return null;
        }
    }
}
