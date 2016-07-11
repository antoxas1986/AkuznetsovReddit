using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Web;

namespace AkuznetsovReddit.Web.Models
{
    public class RoleVm
    {
        public int RoleId { get; set; }

        [DisplayName("Role")]
        public string RoleName { get; set; }
    }
}
