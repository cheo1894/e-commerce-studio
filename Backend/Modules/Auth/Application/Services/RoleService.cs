using Azure.Core.Pipeline;
using Backend.Modules.Auth.Application.DTOs;
using Backend.Modules.Auth.Application.Interfaces;
using Backend.Modules.Auth.Domain.Entities;
using Backend.Modules.Auth.Domain.Interfaces;
using Backend.Modules.Auth.Infrastructure.Persistence;
using Microsoft.EntityFrameworkCore;

namespace Backend.Modules.Auth.Application.Services
{
    public class RoleService : IRoleService
    {
        private readonly IRoleRepository _repo;
        public RoleService(IRoleRepository repo)
        {
            _repo = repo;
        }

        public async Task<IEnumerable<RoleDto>> Get()
        {
            var res = await _repo.Get();
            return res.Select(r => new RoleDto()
            {
                RoleId = r.RoleId,
                RoleName = r.RoleName,

            });
        }

        public async Task<RoleDto> GetById(int id)
        {
            var res = await _repo.GetById(id);
            if (res == null) return null;
            var roleDto = new RoleDto()
            {
                RoleId = res.RoleId,
                RoleName = res.RoleName,
            };
            return roleDto;
        }
        public async Task<RoleDto> Add(RoleInsertDto dto)
        {
            var role = new Role()
            {
                RoleName = dto.RoleName,
                Active = true
            };
            await _repo.Add(role);
            await _repo.Save();
            var roleDto = new RoleDto()
            {
                RoleId = role.RoleId,
                RoleName = role.RoleName
            };
            return roleDto;
        }
        public async Task<RoleDto> Update(int id, RoleUpdateDto dto)
        {
            var res = await _repo.GetById(id);
            if (res == null) return null;
            res.RoleName = dto.RoleName;
            _repo.Update(res);
            await _repo.Save();
            var roleDto = new RoleDto()
            {
                RoleId = res.RoleId,
                RoleName = res.RoleName
            };
            return roleDto;
        }

        public async Task<bool> Delete(int id)
        {
            var res = await _repo.GetById(id);
            if (res == null) return false;
            res.Active = false;
            _repo.Update(res);
            await _repo.Save();
            return true;
        }
    }
}