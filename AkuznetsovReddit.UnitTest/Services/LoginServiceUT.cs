using AkuznetsovReddit.Core.Constants;
using AkuznetsovReddit.Core.Enums;
using AkuznetsovReddit.Core.Models;
using AkuznetsovReddit.Core.Utilities;
using AkuznetsovReddit.Data.Repository.Interfaces;
using AkuznetsovReddit.Services;
using AkuznetsovReddit.Services.Interfaces;
using AkuznetsovReddit.UnitTest.Helpers;
using AkuznetsovReddit.Web.Mappers;
using Moq;
using System;
using Xunit;

namespace AkuznetsovReddit.UnitTest.Services
{
    public class LoginServiceUT : HttpContextHelper
    {
        private Mock<ICryptoService> _hash;
        private Mock<IRoleService> _roleService;
        private LoginService _sut;
        private Mock<IUnitOfWork> _unit;
        private Mock<IUserRepo> _userRepo;

        public LoginServiceUT()
        {
            _roleService = new Mock<IRoleService>();
            _userRepo = new Mock<IUserRepo>();
            _hash = new Mock<ICryptoService>();
            _unit = new Mock<IUnitOfWork>();
            _sut = new LoginService(_roleService.Object, _userRepo.Object, _hash.Object, _unit.Object);
            AutoMapperConfig.Execute();
        }

        [Fact]
        public void LoginUser_Invalid_Less30min()
        {
            ValidationMessageList messages = new ValidationMessageList();
            UserWithUserCredDto user = new UserWithUserCredDto()
            {
                UserCred = new UserCredDto() { IsDisabled = false, FailedLoginAttempts = 4, LastFailedAttempt = DateTime.Now }
            };
            _userRepo.Setup(u => u.GetUserWithUserCredByUserName(It.IsAny<string>())).Returns(user);
            _sut.LoginUser(new LoginDto(), messages);
            Assert.Equal(ErrorMessages.ACCOUNT_BLOCKED, messages.GetFirstErrorMsg);
        }

        [Fact]
        public void LoginUser_Invalid_NotAbleToLogin()
        {
            ValidationMessageList messages = new ValidationMessageList();
            UserWithUserCredDto user = new UserWithUserCredDto()
            {
                UserCred = new UserCredDto() { IsDisabled = true }
            };
            _userRepo.Setup(u => u.GetUserWithUserCredByUserName(It.IsAny<string>())).Returns(user);
            _sut.LoginUser(new LoginDto(), messages);
            Assert.Equal(ErrorMessages.ACCOUNT_DISABLED, messages.GetFirstErrorMsg);
        }

        [Fact]
        public void LoginUser_Invalid_Username()
        {
            ValidationMessageList messages = new ValidationMessageList();
            _sut.LoginUser(new LoginDto(), messages);
            Assert.Equal(ErrorMessages.BAD_CREDENTIALS, messages.GetFirstErrorMsg);
        }

        [Fact]
        public void LoginUser_Invalid_WrongPassword()
        {
            ValidationMessageList messages = new ValidationMessageList();
            UserDto userDto = new UserDto() { UserId = 1 };
            SessionManager.User = userDto;
            UserWithUserCredDto user = new UserWithUserCredDto()
            {
                UserId = 1,
                UserCred = new UserCredDto() { IsDisabled = false, Salt = "Test", FailedLoginAttempts = 0, LastFailedAttempt = DateTime.Now }
            };
            LoginDto login = new LoginDto() { Password = "Test", UserName = "Test" };
            _userRepo.Setup(u => u.GetUserWithUserCredByUserName(It.IsAny<string>())).Returns(user);
            _userRepo.Setup(u => u.GetUserByHashPassword(It.IsAny<string>(), It.IsAny<string>(), It.IsAny<ValidationMessageList>()))
                .Callback((string username, string password, ValidationMessageList list) => list.Add(new ValidationMessage(MessageTypes.Error, ErrorMessages.BAD_CREDENTIALS)));

            _sut.LoginUser(login, messages);
            Assert.Equal(messages.GetFirstErrorMsg, ErrorMessages.BAD_CREDENTIALS);
            _userRepo.Verify(u => u.EditUser(It.IsAny<UserWithUserCredDto>(), It.IsAny<ValidationMessageList>()), Times.AtLeastOnce);
            _unit.Verify(u => u.SaveChanges(), Times.AtLeastOnce);
        }

        [Fact]
        public void LoginUser_Valid()
        {
            ValidationMessageList messages = new ValidationMessageList();
            UserDto userDto = new UserDto() { UserId = 1 };
            SessionManager.User = userDto;
            UserWithUserCredDto user = new UserWithUserCredDto()
            {
                UserId = 1,
                UserCred = new UserCredDto() { IsDisabled = false, Salt = "Test", FailedLoginAttempts = 0, LastFailedAttempt = DateTime.Now }
            };
            LoginDto login = new LoginDto() { Password = "Test", UserName = "Test" };
            _userRepo.Setup(u => u.GetUserWithUserCredByUserName(It.IsAny<string>())).Returns(user);
            _userRepo.Setup(u => u.GetUserByHashPassword(It.IsAny<string>(), It.IsAny<string>(), It.IsAny<ValidationMessageList>()))
                .Returns(userDto);

            _sut.LoginUser(login, messages);
            Assert.Equal(userDto.UserId, SessionManager.User.UserId);
            _userRepo.Verify(u => u.EditUser(It.IsAny<UserWithUserCredDto>(), It.IsAny<ValidationMessageList>()), Times.AtLeastOnce);
            _unit.Verify(u => u.SaveChanges(), Times.AtLeastOnce);
        }

        [Fact]
        public void Logoff_Invalid()
        {
            SessionManager.User = null;
            _sut.Logoff();
            Assert.Null(SessionManager.User);
        }
    }
}
