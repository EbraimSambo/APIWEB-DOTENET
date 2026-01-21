using APIWEB.src.Features.User.Domain.Ports;
using UserEntity = APIWEB.src.Features.User.Domain.Entity.User;
using APIWEB.src.Shared.Infrastructure.Configurations;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;

namespace APIWEB.src.Features.User.Adapters.Out.Persistence
{
    public class UserRepository: IUserRepository
    {
        private readonly AppDbContext _context;

        public UserRepository(AppDbContext context)
        {
            _context = context;
        }

        public async Task<UserEntity> save(UserEntity user)
        {
            await _context.Users.AddAsync(user);
            await _context.SaveChangesAsync();
            return user;
        }
        
        public async Task<UserEntity?> findByEmail(string email)
        {
            return await _context.Users
                .FirstOrDefaultAsync(u => u.Email == email);
        }
    }
}