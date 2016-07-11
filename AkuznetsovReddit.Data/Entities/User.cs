using System.Collections.Generic;

namespace AkuznetsovReddit.Data.Entities
{
    public class User
    {
        public virtual ICollection<Post> Posts { get; set; }
        public virtual Role Role { get; set; }
        public int RoleId { get; set; }
        public virtual UserCred UserCred { get; set; }

        public int UserId { get; set; }
        public string UserName { get; set; }
    }
}
