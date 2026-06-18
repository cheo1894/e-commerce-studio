namespace Backend.Modules.Auth.Application.DTOs
{



    public class RefreshTokenDTO
    {
        public string RefreshToken { get; set; } = string.Empty;
        public string SessionId { get; set; } = string.Empty;
    }

}