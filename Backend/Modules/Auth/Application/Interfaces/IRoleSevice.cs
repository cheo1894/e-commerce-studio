using Backend.Modules.Auth.Application.DTOs;
using Backend.Modules.Auth.Domain.Entities;

namespace Backend.Modules.Auth.Application.Interfaces
{
    public interface IRoleService
    {


        Task<IEnumerable<RoleDto>> Get();

        Task<RoleDto> GetById(int id);

        Task<RoleDto> Add(RoleInsertDto dto);

        Task<RoleDto> Update(int id, RoleUpdateDto dto);

        Task<bool> Delete(int id);


    }
}