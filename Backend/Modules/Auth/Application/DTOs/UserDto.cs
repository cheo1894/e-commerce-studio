namespace Backend.Modules.Auth.Application.DTOs
{

    public class UserDto
    {
        public int UserId { get; set; }
        public string UserName { get; set; }
        public int RoleId { get; set; }
        public string? RoleName { get; set; }
    }



}