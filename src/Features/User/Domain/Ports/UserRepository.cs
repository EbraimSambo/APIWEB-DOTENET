using APIWEB.src.Features.User.Domain.Entity;

namespace APIWEB.src.Features.User.Domain.Ports
{
    public interface UserRepository
    {
        User save(User user);
    }
}