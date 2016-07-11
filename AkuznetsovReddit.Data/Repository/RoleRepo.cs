using AkuznetsovReddit.Core.Models;
using AkuznetsovReddit.Data.Context.Interfaces;
using AkuznetsovReddit.Data.Repository.Interfaces;
using AutoMapper.QueryableExtensions;
using System.Collections.Generic;
using System.Linq;

namespace AkuznetsovReddit.Data.Repository
{
    /// <summary>
    /// Implementation of IRoleRepo Interface
    /// </summary>
    /// <seealso cref="AkuznetsovReddit.Data.Repository.Interfaces.IRoleRepo" />
    public class RoleRepo : IRoleRepo
    {
        private IRedditContext _db;

        public RoleRepo(IRedditContext db)
        {
            _db = db;
        }

        /// <summary>
        /// Gets all roles.
        /// </summary>
        /// <returns></returns>
        public ICollection<RoleDto> GetAllRoles()
        {
            return _db.Roles.ProjectTo<RoleDto>().ToList();
        }

        /// <summary>
        /// Gets the role by identifier.
        /// </summary>
        /// <param name="id">The identifier.</param>
        /// <returns></returns>
        public RoleDto GetRoleById(int id)
        {
            return _db.Roles.Where(r => r.RoleId == id).ProjectTo<RoleDto>().FirstOrDefault();
        }

        /// <summary>
        /// Gets the name of the role by role.
        /// </summary>
        /// <param name="roleName">Name of the role.</param>
        /// <returns></returns>
        public RoleDto GetRoleByRoleName(string roleName)
        {
            return _db.Roles.Where(r => r.RoleName == roleName).ProjectTo<RoleDto>().FirstOrDefault();
        }
    }
}
