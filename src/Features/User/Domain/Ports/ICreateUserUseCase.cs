using System.Threading.Tasks;

namespace APIWEB.src.Features.User.Domain.Ports
{
    public interface ICreateUserUseCase
    {
        record Input(string Name, string Email, string Password);
        Task Execute(Input input);
    }
}