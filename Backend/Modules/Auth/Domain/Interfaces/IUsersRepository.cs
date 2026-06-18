using Backend.Modules.Auth.Domain.Entities;

namespace Backend.Modules.Auth.Domain.Interfaces
{
    public interface IUsersRepository
    {
        Task<IEnumerable<User>> Get();
        Task<User> GetById(int id);
        Task Add(User user);
    }

}