using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace AkuznetsovReddit.Web.Models
{
    public class LoginVm
    {
        [Required]
        [DataType(DataType.Password)]
        public string Password { get; set; }

        [Required]
        [DisplayName("Username")]
        public string UserName { get; set; }
    }
}
