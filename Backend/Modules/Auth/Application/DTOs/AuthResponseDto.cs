namespace Backend.Modules.Auth.Application.DTOs
{


    public class AuthResponseDTO
    {
        public string RefreshToken { get; set; }
        public string SessionId { get; set; }
        public string Token { get; set; }
        public int UserId { get; set; }
        public string UserName { get; set; }
        public int RoleId { get; set; }

    }




}