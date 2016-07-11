using AkuznetsovReddit.Core.Constants;
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
    public class UserRepoUT
    {
        private Mock<IRedditContext> _db;
        private DbSetHelper _helper;
        private UserRepo _rut;
        private List<User> _users;
        private Mock<DbSet<User>> _userSet;

        public UserRepoUT()
        {
            _db = new Mock<IRedditContext>();
            _helper = new DbSetHelper();
            _rut = new UserRepo(_db.Object);
            _users = new List<User>
            {
                new User
                {
                    UserId = 1,
                    UserName="Test",
                    UserCred = new UserCred
                    {
                        PasswordHash="Hash"
                    }
                },
                new User
                {
                    UserId = 2,
                    UserName="Test",
                }
            };

            _userSet = _helper.GetDbSet(_users);
            _db.Setup(c => c.Users).Returns(_userSet.Object);

            AutoMapperConfig.Execute();
        }

        [Fact]
        public void EditUser_Invalid()
        {
            ValidationMessageList messages = new ValidationMessageList();
            UserWithUserCredDto user = new UserWithUserCredDto() { UserId = 99 };
            _rut.EditUser(user, messages);
            Assert.Equal(messages.GetFirstErrorMsg, ErrorMessages.NO_USER);
        }

        [Fact]
        public void EditUser_Valid()
        {
            ValidationMessageList messages = new ValidationMessageList();
            UserWithUserCredDto user = new UserWithUserCredDto() { UserId = 1 };
            _rut.EditUser(user, messages);
            Assert.Null(messages.GetFirstErrorMsg);
        }

        [Fact]
        public void GetAllUsers()
        {
            ICollection<UserDto> list = _rut.GetAllUsers();
            Assert.Equal(list.Count, _users.Count);
        }

        [Fact]
        public void GetUserByHashPassword_Invalid()
        {
            ValidationMessageList messages = new ValidationMessageList();
            UserDto user = _rut.GetUserByHashPassword("wrong", "wrong", messages);
            Assert.Null(user);
            Assert.Equal(messages.GetFirstErrorMsg, ErrorMessages.BAD_CREDENTIALS);
        }

        [Fact]
        public void GetUserByHashPassword_Valid()
        {
            ValidationMessageList messages = new ValidationMessageList();
            UserDto user = _rut.GetUserByHashPassword("Test", "Hash", messages);
            Assert.Equal(user.UserId, _users[0].UserId);
            Assert.Null(messages.GetFirstErrorMsg);
        }

        [Fact]
        public void GetUserByUserId_Valid()
        {
            UserDto user = _rut.GetUserByUserId(1);
            Assert.Equal(user.UserName, _users[0].UserName);
        }

        [Fact]
        public void GetUserByUserName_Valid()
        {
            UserDto user = _rut.GetUserByUserName("Test");
            Assert.Equal(user.UserId, _users[0].UserId);
        }

        [Fact]
        public void GetUserWithUserCredByUserName_Valid()
        {
            UserWithUserCredDto actual = _rut.GetUserWithUserCredByUserName("Test");
            Assert.Equal(actual.UserId, _users[0].UserId);
        }

        [Fact]
        public void RegisterNewUser_Valid()
        {
            _rut.RegisterNewUser(new User());
            _userSet.Verify(u => u.Add(It.IsAny<User>()), Times.Once);
        }
    }
}
