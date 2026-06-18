

using Backend.Modules.Auth.Application.DTOs;
using Backend.Modules.Auth.Domain.Entities;

namespace Backend.Modules.Auth.Application.Interfaces
{
    public interface IUserService
    {
        Task<IEnumerable<UserDto>> Get();

        Task<UserDto> GetById(int id);


        Task<UserDto> Add(UserInsertDto dto);

    }
}