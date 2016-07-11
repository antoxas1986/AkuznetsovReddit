using AkuznetsovReddit.Web.Controllers;
using AkuznetsovReddit.Web.Mappers;
using System.Web.Mvc;
using Xunit;

namespace AkuznetsovReddit.UnitTest.Web.Controllers
{
    public class ErrorControllerUT
    {
        private ErrorController _sut;

        public ErrorControllerUT()
        {
            _sut = new ErrorController();
            AutoMapperConfig.Execute();
        }

        [Fact]
        public void Forbidden_Valid()
        {
            ViewResult actual = _sut.Forbidden() as ViewResult;
            Assert.Equal("Forbidden", actual.ViewName);
        }

        [Fact]
        public void NotFound_Valid()
        {
            ViewResult actual = _sut.NotFound() as ViewResult;
            Assert.Equal("NotFound", actual.ViewName);
        }

        [Fact]
        public void ServerError_Valid()
        {
            ViewResult actual = _sut.ServerError() as ViewResult;
            Assert.Equal("ServerError", actual.ViewName);
        }
    }
}
