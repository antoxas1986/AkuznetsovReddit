using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AkuznetsovReddit.Core.Models
{
    /// <summary>
    /// Base RoleDTO model
    /// </summary>
    public class RoleDto
    {
        public int RoleId { get; set; }
        public string RoleName { get; set; }
    }
}
