using APIWEB.src.Features.User.Domain.Ports;
using UserEntity = APIWEB.src.Features.User.Domain.Entity.User;
using System.Threading.Tasks;

namespace APIWEB.src.Features.User.Application.ports
{
    public class FindUserByIdUseCase : IFindUserByIdUseCase
    {
        private readonly IUserRepository _userRepository;

        public FindUserByIdUseCase(IUserRepository userRepository)
        {
            _userRepository = userRepository;
        }

        public async Task<UserEntity> Execute(string id)
        {
            return await _userRepository.findById(id)
            ?? throw new Exception("User not found");
        }
    }
}