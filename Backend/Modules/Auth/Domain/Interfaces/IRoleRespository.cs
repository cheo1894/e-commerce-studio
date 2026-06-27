using Backend.Modules.Auth.Domain.Entities;

namespace Backend.Modules.Auth.Domain.Interfaces
{


    public interface IRoleRepository
    {
        Task<IEnumerable<Role>> Get();
        Task<Role> GetById(int id);
        Task Add(Role role);
        void Update(Role role);
        Task Save();
    }



}