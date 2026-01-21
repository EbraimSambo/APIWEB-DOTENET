using APIWEB.src.Features.User.Domain.Ports;
using UserEntity = APIWEB.src.Features.User.Domain.Entity.User;
using System.Threading.Tasks;
using APIWEB.src.Features.Auth.Domain.Ports;
using System.Security.Cryptography;

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
            var salt = RandomNumberGenerator.GetBytes(16);
            var hashedPassword = _hashService.HashPassword(input.Password, salt);
            
            var user = new UserEntity
            {
                Id = Guid.NewGuid().ToString(),
                Name = input.Name,
                Email = input.Email,
                Password = Convert.ToBase64String(hashedPassword),
                Salt = salt,
                CreatedAt = now,
                UpdatedAt = now
            };

            await _userRepository.save(user);
        }
    }
}