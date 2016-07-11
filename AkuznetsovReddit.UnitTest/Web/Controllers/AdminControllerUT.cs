using AkuznetsovReddit.Core.Models;
using AkuznetsovReddit.Core.Utilities;
using AkuznetsovReddit.Services.Interfaces;
using AkuznetsovReddit.UnitTest.Helpers;
using AkuznetsovReddit.Web.Controllers;
using AkuznetsovReddit.Web.Mappers;
using Moq;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web.Mvc;
using Xunit;

namespace AkuznetsovReddit.UnitTest.Web.Controllers
{
    public class AdminControllerUT : HttpContextHelper
    {
        private AdminController _cut;
        private Mock<IUserService> _userService;

        public AdminControllerUT()
        {
            _userService = new Mock<IUserService>();
            _cut = new AdminController(_userService.Object);
            AutoMapperConfig.Execute();
        }

        [Fact]
        public void Index_Valid()
        {
            SessionManager.User = new UserDto()
            {
                UserId = 2,
                UserName = "Test"
            };

            ViewResult actual = _cut.Index() as ViewResult;
            Assert.Equal("", actual.ViewName);
        }
    }
}
