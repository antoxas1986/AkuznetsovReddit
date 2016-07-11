using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace AkuznetsovReddit.Data.Entities
{
    public class UserCred
    {
        public int FailedLoginAttempts { get; set; }
        public bool IsDisabled { get; set; }
        public DateTime LastFailedAttempt { get; set; }
        public string PasswordHash { get; set; }
        public string Salt { get; set; }
        public virtual User User { get; set; }

        [Key, ForeignKey("User")]
        public int UserId { get; set; }
    }
}
