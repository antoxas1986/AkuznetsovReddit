using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AkuznetsovReddit.Core.Models
{
    public class UserWithUserCredDto : UserDto
    {
        public UserCredDto UserCred { get; set; }
    }
}
