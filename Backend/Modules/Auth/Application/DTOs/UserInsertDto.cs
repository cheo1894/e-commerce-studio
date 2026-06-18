namespace Backend.Modules.Auth.Application.DTOs
{
    public class UserInsertDto
    {
        public string UserName { get; set; }
        public string password { get; set; }
        public bool Active { get; set; }
        public int RoleId { get; set; }
    }
}