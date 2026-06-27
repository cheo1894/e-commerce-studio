using Backend.Modules.Auth.Domain.Entities;
using Backend.Modules.Auth.Domain.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace Backend.Modules.Auth.Infrastructure.Persistence.Repositories
{
    public class RoleRepository : IRoleRepository
    {
        private AuthDbContext _context;
        public RoleRepository(AuthDbContext context)
        {
            _context = context;
        }


        public async Task<IEnumerable<Role>> Get() => await _context.Roles.ToListAsync();


        public async Task<Role> GetById(int id) => await _context.Roles.FindAsync(id);


        public async Task Add(Role role) => await _context.Roles.AddAsync(role);

        void IRoleRepository.Update(Role role)
        {
            _context.Roles.Attach(role);
            _context.Roles.Update(role).State = EntityState.Modified;
        }
        public async Task Save() => await _context.SaveChangesAsync();

    }
}