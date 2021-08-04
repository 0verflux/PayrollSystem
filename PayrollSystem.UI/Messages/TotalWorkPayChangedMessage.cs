using Microsoft.Toolkit.Mvvm.Messaging.Messages;

namespace PayrollSystem.UI.Messages
{
    public class TotalWorkPayChangedMessage : ValueChangedMessage<decimal>
    {
        public TotalWorkPayChangedMessage(decimal value) : base(value)
        {

        }
    }
}
