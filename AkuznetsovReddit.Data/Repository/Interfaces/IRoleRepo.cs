using AkuznetsovReddit.Core.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AkuznetsovReddit.Data.Repository.Interfaces
{
    /// <summary>
    /// Api to work with Role model
    /// </summary>
    public interface IRoleRepo
    {
        /// <summary>
        /// Gets all roles.
        /// </summary>
        /// <returns></returns>
        ICollection<RoleDto> GetAllRoles();

        /// <summary>
        /// Gets the role by identifier.
        /// </summary>
        /// <param name="id">The identifier.</param>
        /// <returns></returns>
        RoleDto GetRoleById(int id);

        /// <summary>
        /// Gets the name of the role by role name.
        /// </summary>
        /// <param name="roleName">Name of the role.</param>
        /// <returns></returns>
        RoleDto GetRoleByRoleName(string roleName);
    }
}
