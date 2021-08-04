using System;

namespace PayrollSystem.Logic.Exceptions
{
    public class EntityNotFoundException : ArgumentNullException
    {
        public EntityNotFoundException(string? paramName, bool showParameter = true) : base(message: $"Entity does not exist! {(showParameter ? $"(parameter name: {paramName})" : null)}", paramName: paramName)
        {

        }
    }
}
