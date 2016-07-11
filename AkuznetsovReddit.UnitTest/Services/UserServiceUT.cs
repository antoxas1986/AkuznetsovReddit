using AkuznetsovReddit.Core.Models;
using AkuznetsovReddit.Data.Repository.Interfaces;
using AkuznetsovReddit.Services;
using Moq;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xunit;

namespace AkuznetsovReddit.UnitTest.Services
{
    public class UserServiceUT
    {
        private UserService _sut;
        private Mock<IUserRepo> _userRepo;

        public UserServiceUT()
        {
            _userRepo = new Mock<IUserRepo>();
            _sut = new UserService(_userRepo.Object);
        }

        [Fact]
        public void GetAllUsers_Valid()
        {
            ICollection<UserDto> _users = new List<UserDto>()
            {
                new UserDto(),
                new UserDto()
            };
            _userRepo.Setup(u => u.GetAllUsers()).Returns(_users);
            ICollection<UserDto> list = _sut.GetAllUsers();
            Assert.Equal(list.Count, _users.Count);
        }

        [Fact]
        public void GetUserByUserId_Valid()
        {
            UserDto _user = new UserDto()
            {
                UserId = 1,
                UserName = "Test"
            };
            _userRepo.Setup(u => u.GetUserByUserId(It.IsAny<int>())).Returns(_user);
            UserDto actual = _sut.GetUserByUserId(1);
            Assert.Equal(actual.UserName, _user.UserName);
        }
    }
}
