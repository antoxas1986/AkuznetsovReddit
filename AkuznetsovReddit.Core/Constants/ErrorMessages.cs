using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AkuznetsovReddit.Core.Constants
{
    public class ErrorMessages
    {
        public const string ACCOUNT_BLOCKED = "You exceeded the number of login attempts. Your account blocked for 30 minutes. Check back later.";
        public const string ACCOUNT_DISABLED = "Your account is disabled. Contact customer service.";
        public const string BAD_CREDENTIALS = "Username or password is incorect!";
        public const string ID_NOT_MATCH = "Ids do not match.";
        public const string NO_POST = "Post not found.";
        public const string NO_ROLE = "The role you trying access do not exist.";
        public const string NO_TOPIC = "Topic not found.";
        public const string NO_USER = "User not found.";
        public const string USERNAME_EXIST = "This Username is taken.";
    }
}
