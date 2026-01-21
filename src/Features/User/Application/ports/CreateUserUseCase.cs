using APIWEB.src.Features.User.Domain.Ports;
using UserEntity = APIWEB.src.Features.User.Domain.Entity.User;
using System.Threading.Tasks;
using APIWEB.src.Features.Auth.Domain.Ports;

namespace APIWEB.src.Features.User.Application.ports
{
    public class CreateUserUseCase : ICreateUserUseCase
    {
        private readonly IUserRepository _userRepository;
        private readonly IHashService _hashService;

        public CreateUserUseCase(IUserRepository userRepository, IHashService hashService)
        {
            _userRepository = userRepository;
            _hashService = hashService;
        }

        public async Task Execute(ICreateUserUseCase.Input input)
        {
            var userExisting = await _userRepository.findByEmail(input.Email);
            if (userExisting != null)
            {
                throw new InvalidOperationException("User with this email already exists.");
            }

            var now = DateTime.UtcNow;
            var user = new UserEntity
            {
                Id = Guid.NewGuid().ToString(),
                Name = input.Name,
                Email = input.Email,
                Password = input.Password,
                Salt = _hashService.HashPassword(input.Password, new byte[16]),
                CreatedAt = now,
                UpdatedAt = now
            };

            await _userRepository.save(user);
        }
    }
}