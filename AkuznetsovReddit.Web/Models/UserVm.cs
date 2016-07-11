using AkuznetsovReddit.Core.Models;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Web;

namespace AkuznetsovReddit.Web.Models
{
    public class UserVm
    {
        public RoleVm Role { get; set; }
        public int RoleId { get; set; }
        public int UserId { get; set; }

        [DisplayName("Username")]
        public string UserName { get; set; }
    }
}
