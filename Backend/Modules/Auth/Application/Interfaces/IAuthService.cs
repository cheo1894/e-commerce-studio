using Backend.Modules.Auth.Application.DTOs;
using Backend.Modules.Auth.Domain.Entities;

namespace Backend.Modules.Auth.Application.Interfaces
{


    public interface IAuthService
    {


        Task<AuthResponseDTO> Login(LoginUserDto dto);

        Task<bool> Logout(string sessionId);

        Task<UserDto> FindUserBySessionId(string sessionId);
    }
}