using Backend.Modules.Auth.Domain.Interfaces;
using Backend.Modules.Auth.Domain.Entities;
using Backend.Modules.Auth.Infrastructure.Persistence;
using Microsoft.EntityFrameworkCore;

namespace Backend.Modules.Auth.Infrastructure.Persistence.Repositories
{



    public class UsersRepository : IUsersRepository
    {
        private AuthDbContext _context;
        public UsersRepository(AuthDbContext context)
        {
            _context = context;
        }

        public async Task<IEnumerable<User>> Get() => await _context.Users.Include(u => u.Role).ToListAsync();


        public async Task<User> GetById(int id) => await _context.Users.FindAsync(id);

        public async Task Add(User user)
        {
            await _context.Users.AddAsync(user);
            await _context.SaveChangesAsync();
        }

    }
}