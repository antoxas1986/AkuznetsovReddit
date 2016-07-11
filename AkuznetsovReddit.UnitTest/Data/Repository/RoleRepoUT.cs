using AkuznetsovReddit.Core.Models;
using AkuznetsovReddit.Data.Context.Interfaces;
using AkuznetsovReddit.Data.Entities;
using AkuznetsovReddit.Data.Repository;
using AkuznetsovReddit.UnitTest.Helpers;
using AkuznetsovReddit.Web.Mappers;
using Moq;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xunit;

namespace AkuznetsovReddit.UnitTest.Data.Repository
{
    public class RoleRepoUT
    {
        private Mock<IRedditContext> _db;
        private DbSetHelper _helper;
        private List<Role> _roles;
        private Mock<DbSet<Role>> _roleSet;
        private RoleRepo _rut;

        public RoleRepoUT()
        {
            _db = new Mock<IRedditContext>();
            _helper = new DbSetHelper();
            _rut = new RoleRepo(_db.Object);
            _roles = new List<Role>
            {
                new Role
                {
                    RoleId = 1,
                    RoleName="Test",
                },
                new Role
                {
                    RoleId = 2,
                    RoleName="Test",
                }
            };

            _roleSet = _helper.GetDbSet(_roles);
            _db.Setup(c => c.Roles).Returns(_roleSet.Object);

            AutoMapperConfig.Execute();
        }

        [Fact]
        public void GetAllRoles_Valid()
        {
            ICollection<RoleDto> roles = _rut.GetAllRoles();
            Assert.Equal(roles.Count(), _roles.Count());
        }

        [Fact]
        public void GetRoleById_Valid()
        {
            RoleDto role = _rut.GetRoleById(1);
            Assert.Equal(role.RoleName, _roles[0].RoleName);
        }

        [Fact]
        public void GetRoleByRoleName_Valid()
        {
            RoleDto role = _rut.GetRoleByRoleName("Test");
            Assert.Equal(role.RoleId, _roles[0].RoleId);
        }
    }
}
