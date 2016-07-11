using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AkuznetsovReddit.Core.Models
{
    public class UserCredDto
    {
        public int FailedLoginAttempts { get; set; }
        public bool IsDisabled { get; set; }
        public DateTime LastFailedAttempt { get; set; }
        public string Salt { get; set; }
    }
}
