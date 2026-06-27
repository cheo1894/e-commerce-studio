using Backend.Modules.Auth.Application.Interfaces;
using Backend.Modules.Auth.Domain.Interfaces;
using Backend.Modules.Auth.Application.DTOs;
using Backend.Modules.Auth.Domain.Entities;
using Backend.Modules.Auth.Domain.Interfaces;


namespace Backend.Modules.Auth.Application.Services
{
    public class UsersService : IUserService
    {
        private IUsersRepository _repository;
        public UsersService(IUsersRepository repository)
        {
            _repository = repository;
        }
        public async Task<IEnumerable<UserDto>> Get()
        {
            var users = await _repository.Get();
            return users.Select(u => new UserDto()
            {
                RoleId = u.RoleId,
                UserId = u.UserId,
                UserName = u.UserName,
                RoleName = u.Role.RoleName

            }).ToList();
        }

        public async Task<UserDto> GetById(int id)
        {
            var user = await _repository.GetById(id);

            if (user == null)
            {
                return null;
            }
            return new UserDto()
            {

                RoleId = user.RoleId,
                UserId = user.UserId,
                UserName = user.UserName

            };
        }


        public async Task<UserDto> Add(UserInsertDto dto)
        {
            var user = new User()
            {
                UserName = dto.UserName,
                password = dto.password,
                RoleId = dto.RoleId,
                Active = true,
            };

            await _repository.Add(user);

            return new UserDto()
            {
                UserId = user.UserId,
                UserName = user.UserName,
                RoleId = user.RoleId,
            };
        }
    }

}