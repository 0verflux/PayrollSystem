using System.Threading.Tasks;

namespace PayrollSystem.UI.Contracts.Activation
{
    public interface IActivationHandler
    {
        bool CanHandle();
        Task HandleAsync();
    }
}
