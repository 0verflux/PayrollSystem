using System;

namespace PayrollSystem.Logic.Exceptions
{
    public class EntityAlreadyExistsException : ArgumentException
    {
        public EntityAlreadyExistsException(string? paramName) 
            : base(message: $"Entity already exists! (Parameter Name: {paramName})", paramName: paramName)
        {

        }
    }
}
