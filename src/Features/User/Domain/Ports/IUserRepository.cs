using UserEntity = APIWEB.src.Features.User.Domain.Entity.User;
using System.Threading.Tasks;

namespace APIWEB.src.Features.User.Domain.Ports
{
    public interface IUserRepository
    {
        Task<UserEntity> save(UserEntity user);

        Task<UserEntity?> findByEmail(string email);
    }
}