using AkuznetsovReddit.Core.Constants;
using AkuznetsovReddit.Core.Enums;
using AkuznetsovReddit.Core.Models;
using AkuznetsovReddit.Services.Interfaces;
using AkuznetsovReddit.Web.Controllers;
using AkuznetsovReddit.Web.Mappers;
using AkuznetsovReddit.Web.Models;
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
    public class LoginControllerUT
    {
        private LoginController _cut;
        private Mock<ILoginService> _loginService;

        public LoginControllerUT()
        {
            _loginService = new Mock<ILoginService>();
            _cut = new LoginController(_loginService.Object);
            AutoMapperConfig.Execute();
        }

        [Fact]
        public void Index_Valid()
        {
            ViewResult actual = _cut.Index() as ViewResult;
            Assert.Equal("", actual.ViewName);
        }

        [Fact]
        public void Login_Invalid_Error()
        {
            ValidationMessageList messages = new ValidationMessageList();

            _loginService.Setup(l => l.LoginUser(It.IsAny<LoginDto>(), messages))
                .Callback((LoginDto login, ValidationMessageList list) => list.Add(new ValidationMessage(MessageTypes.Error, ErrorMessages.BAD_CREDENTIALS)));
            ViewResult actual = _cut.Login(new LoginVm()) as ViewResult;
            Assert.Equal("Index", actual.ViewName);
        }

        [Fact]
        public void Login_Invalid_ModelState()
        {
            _cut.ModelState.AddModelError("error", "test error");

            RedirectToRouteResult actual = _cut.Login(new LoginVm()) as RedirectToRouteResult;
            Assert.True(actual.RouteValues.ContainsValue("Index"));
        }

        [Fact]
        public void Login_Valid()
        {
            RedirectToRouteResult actual = _cut.Login(new LoginVm()) as RedirectToRouteResult;
            Assert.True(actual.RouteValues.ContainsValue("Topic"));
        }

        [Fact]
        public void Login2_Valid()
        {
            RedirectToRouteResult actual = _cut.Login2() as RedirectToRouteResult;
            _loginService.Verify(s => s.LoginUser(It.IsAny<LoginDto>(), It.IsAny<ValidationMessageList>()), Times.Once);

            Assert.True(actual.RouteValues.ContainsValue("Index"));
        }

        [Fact]
        public void Logoff_Valid()
        {
            RedirectToRouteResult actual = _cut.Logoff() as RedirectToRouteResult;
            _loginService.Verify(s => s.Logoff(), Times.Once);
            Assert.True(actual.RouteValues.ContainsValue("Index"));
        }
    }
}
