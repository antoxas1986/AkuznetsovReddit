using AkuznetsovReddit.Core.Constants;
using AkuznetsovReddit.Core.Models;
using AkuznetsovReddit.Core.Utilities;
using AkuznetsovReddit.Data.Repository.Interfaces;
using AkuznetsovReddit.Services.Interfaces;
using System.Collections.Generic;

namespace AkuznetsovReddit.Services
{
    /// <summary>
    /// Implementation of IRoleService Interface
    /// </summary>
    /// <seealso cref="AkuznetsovReddit.Services.Interfaces.IRoleService" />
    public class RoleService : IRoleService
    {
        private IRoleRepo _roleRepo;

        public RoleService(IRoleRepo roleRepo)
        {
            _roleRepo = roleRepo;
        }

        /// <summary>
        /// Gets all roles.
        /// </summary>
        /// <returns>
        /// ICollection
        /// </returns>
        public ICollection<RoleDto> GetAllRoles()
        {
            ICollection<RoleDto> list = (ICollection<RoleDto>)GlobalCachingProvider.Instance.GetItem(CacheKeys.ROLES);
            if (list == null)
            {
                list = _roleRepo.GetAllRoles();
                GlobalCachingProvider.Instance.AddItem(CacheKeys.ROLES, list);
            }
            return list;
        }

        /// <summary>
        /// Gets the role by identifier.
        /// </summary>
        /// <param name="id">The identifier.</param>
        /// <returns>
        /// RoleDTO
        /// </returns>
        public RoleDto GetRoleById(int id)
        {
            RoleDto role = (RoleDto)GlobalCachingProvider.Instance.GetItem(CacheKeys.ROLES + id);
            if (role == null)
            {
                role = _roleRepo.GetRoleById(id);
                if (role != null)
                {
                    GlobalCachingProvider.Instance.AddItem(CacheKeys.ROLES + id, role);
                }
            }
            return role;
        }

        /// <summary>
        /// Gets the name of the role by role name.
        /// </summary>
        /// <param name="roleName">Name of the role.</param>
        /// <returns></returns>
        public RoleDto GetRoleByRoleName(string roleName)
        {
            RoleDto role = (RoleDto)GlobalCachingProvider.Instance.GetItem(CacheKeys.ROLES + roleName);
            if (role == null)
            {
                role = _roleRepo.GetRoleByRoleName(roleName);
                if (role != null)
                {
                    GlobalCachingProvider.Instance.AddItem(CacheKeys.ROLES + roleName, role);
                }
            }
            return role;
        }
    }
}
