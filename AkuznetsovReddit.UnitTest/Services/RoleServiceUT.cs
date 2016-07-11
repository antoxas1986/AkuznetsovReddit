using AkuznetsovReddit.Core.Models;
using AkuznetsovReddit.Data.Repository.Interfaces;
using AkuznetsovReddit.Services;
using AkuznetsovReddit.Web.Mappers;
using Moq;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xunit;

namespace AkuznetsovReddit.UnitTest.Services
{
    public class RoleServiceUT
    {
        private Mock<IRoleRepo> _roleRepo;
        private List<RoleDto> _roles;
        private RoleService _sut;

        public RoleServiceUT()
        {
            _roleRepo = new Mock<IRoleRepo>();
            _sut = new RoleService(_roleRepo.Object);
            _roles = new List<RoleDto>()
            {
                new RoleDto(),
                new RoleDto()
            };

            AutoMapperConfig.Execute();
        }

        [Fact]
        public void GetAllRoles_Invalid()
        {
            _roleRepo.Setup(r => r.GetAllRoles()).Returns(_roles);
            ICollection<RoleDto> list = _sut.GetAllRoles();
            Assert.Equal(list.Count, _roles.Count);
        }

        [Fact]
        public void GetRoleByRoleId_Valid()
        {
            RoleDto role = new RoleDto()
            {
                RoleId = 1,
                RoleName = "Test"
            };
            _roleRepo.Setup(r => r.GetRoleById(It.IsAny<int>())).Returns(role);
            RoleDto actual = _sut.GetRoleById(1);
            Assert.Equal(role.RoleName, actual.RoleName);
        }

        [Fact]
        public void GetRoleByRoleName_Valid()
        {
            RoleDto role = new RoleDto()
            {
                RoleId = 1,
                RoleName = "Test"
            };
            _roleRepo.Setup(r => r.GetRoleByRoleName(It.IsAny<string>())).Returns(role);
            RoleDto actual = _sut.GetRoleByRoleName("Test");
            Assert.Equal(role.RoleId, actual.RoleId);
        }
    }
}
