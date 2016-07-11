namespace AkuznetsovReddit.Core.Models
{
    public class UserDto
    {
        public RoleDto Role { get; set; }
        public int RoleId { get; set; }
        public int UserId { get; set; }
        public string UserName { get; set; }
    }
}
