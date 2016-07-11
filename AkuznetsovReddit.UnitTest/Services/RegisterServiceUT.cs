using AkuznetsovReddit.Core.Constants;
using AkuznetsovReddit.Core.Models;
using AkuznetsovReddit.Data.Entities;
using AkuznetsovReddit.Data.Repository.Interfaces;
using AkuznetsovReddit.Services;
using AkuznetsovReddit.Services.Interfaces;
using AkuznetsovReddit.Web.Mappers;
using Moq;
using Xunit;

namespace AkuznetsovReddit.UnitTest.Services
{
    public class RegisterServiceUT
    {
        private Mock<ICryptoService> _hash;
        private Mock<IRoleService> _roleService;
        private RegisterService _sut;
        private Mock<IUnitOfWork> _unit;
        private Mock<IUserRepo> _userRepo;

        public RegisterServiceUT()
        {
            _hash = new Mock<ICryptoService>();
            _roleService = new Mock<IRoleService>();
            _unit = new Mock<IUnitOfWork>();
            _userRepo = new Mock<IUserRepo>();
            _sut = new RegisterService(_userRepo.Object, _unit.Object, _roleService.Object, _hash.Object);
            AutoMapperConfig.Execute();
        }

        [Fact]
        public void RegisterUser_Invalid_Username()
        {
            ValidationMessageList messages = new ValidationMessageList();
            RegisterDto register = new RegisterDto
            {
                UserName = "Test",
                Password = "Test"
            };
            _userRepo.Setup(u => u.GetUserByUserName(It.IsAny<string>())).Returns(new UserDto());
            _sut.RegisterUser(register, messages);
            Assert.Equal(messages.GetFirstErrorMsg, ErrorMessages.USERNAME_EXIST);
        }

        [Fact]
        public void RegisterUser_Valid()
        {
            ValidationMessageList messages = new ValidationMessageList();
            RegisterDto register = new RegisterDto
            {
                UserName = "Test",
                Password = "Test"
            };
            RoleDto role = new RoleDto
            {
                RoleName = "User",
                RoleId = 1
            };
            _roleService.Setup(u => u.GetRoleByRoleName(It.IsAny<string>())).Returns(role);
            _sut.RegisterUser(register, messages);
            _userRepo.Verify(u => u.RegisterNewUser(It.IsAny<User>()), Times.AtLeastOnce);
            _unit.Verify(u => u.SaveChanges(), Times.AtLeastOnce);
        }
    }
}
