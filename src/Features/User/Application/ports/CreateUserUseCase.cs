using APIWEB.src.Features.User.Domain.Ports;
using UserEntity = APIWEB.src.Features.User.Domain.Entity.User;
using System.Threading.Tasks;

namespace APIWEB.src.Features.User.Application.ports
{
    public class CreateUserUseCase : ICreateUserUseCase
    {
        private readonly IUserRepository _userRepository;

        public CreateUserUseCase(IUserRepository userRepository)
        {
            _userRepository = userRepository;
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
                CreatedAt = now,
                UpdatedAt = now
            };

            await _userRepository.save(user);
        }
    }
}