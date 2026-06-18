using Backend.Modules.Auth.Application.DTOs;

namespace Backend.Modules.Auth.Application.Interfaces
{


    public interface IRefreshTokenService
    {

        Task<TokenRenewalResult?> RenewAccessTokenAsync(string sessionId);



    }




}