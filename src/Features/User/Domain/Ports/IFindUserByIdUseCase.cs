using UserEntity = APIWEB.src.Features.User.Domain.Entity.User;
using System.Threading.Tasks;

namespace APIWEB.src.Features.User.Domain.Ports
{
    public interface IFindUserByIdUseCase
    {
         Task<UserEntity> Execute(string id);
    }
}