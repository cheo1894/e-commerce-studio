namespace Backend.Modules.Auth.Application.DTOs
{
    public class RefreshTokenDataDTO
    {
        public string RefreshToken { get; set; }
        public DateTime ExpiryUtc { get; set; }
    }
}