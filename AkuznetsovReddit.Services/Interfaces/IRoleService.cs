using AkuznetsovReddit.Core.Models;
using System.Collections.Generic;

namespace AkuznetsovReddit.Services.Interfaces
{
    /// <summary>
    /// Api to work with Role service
    /// </summary>
    public interface IRoleService
    {
        /// <summary>
        /// Gets all roles.
        /// </summary>
        /// <returns>ICollection</returns>
        ICollection<RoleDto> GetAllRoles();

        /// <summary>
        /// Gets the role by identifier.
        /// </summary>
        /// <param name="id">The identifier.</param>
        /// <returns>RoleDTO</returns>
        RoleDto GetRoleById(int id);

        /// <summary>
        /// Gets the name of the role by role.
        /// </summary>
        /// <param name="roleName">Name of the role.</param>
        /// <returns></returns>
        RoleDto GetRoleByRoleName(string roleName);
    }
}
