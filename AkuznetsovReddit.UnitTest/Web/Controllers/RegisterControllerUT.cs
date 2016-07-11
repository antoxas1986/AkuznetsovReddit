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
    public class RegisterControllerUT
    {
        private RegisterController _cut;
        private Mock<IRegisterService> _registerService;

        public RegisterControllerUT()
        {
            _registerService = new Mock<IRegisterService>();
            _cut = new RegisterController(_registerService.Object);
            AutoMapperConfig.Execute();
        }

        [Fact]
        public void Index_Valid()
        {
            ViewResult actual = _cut.Index() as ViewResult;
            Assert.Equal("", actual.ViewName);
        }

        [Fact]
        public void Register_Invalid()
        {
            _registerService.Setup(c => c.RegisterUser(It.IsAny<RegisterDto>(), It.IsAny<ValidationMessageList>()))
                .Callback((RegisterDto dto, ValidationMessageList inMessages) => inMessages.Add(new ValidationMessage(MessageTypes.Error, ErrorMessages.NO_USER)));

            ViewResult actual = _cut.Register(new RegisterVm()) as ViewResult;
            string error = actual.ViewData.ModelState.Where(m => m.Key == string.Empty)
                .Select(m => m.Value).FirstOrDefault().Errors.Select(e => e.ErrorMessage).FirstOrDefault();
            Assert.Equal("Index", actual.ViewName);
            Assert.Equal(error, ErrorMessages.NO_USER);
        }

        [Fact]
        public void Register_Invalid_ModelState()
        {
            _cut.ModelState.AddModelError("error", "test error");
            RedirectToRouteResult actual = _cut.Register(new RegisterVm()) as RedirectToRouteResult;
            Assert.True(actual.RouteValues.ContainsValue("Index"));
        }

        [Fact]
        public void Register_Valid()
        {
            RedirectToRouteResult actual = _cut.Register(new RegisterVm()) as RedirectToRouteResult;
            Assert.True(actual.RouteValues.ContainsValue("Login"));
        }
    }
}
