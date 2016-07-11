using AkuznetsovReddit.Core.Constants;
using AkuznetsovReddit.Core.Enums;
using AkuznetsovReddit.Core.Models;
using AkuznetsovReddit.Core.Utilities;
using AkuznetsovReddit.Services.Interfaces;
using AkuznetsovReddit.UnitTest.Helpers;
using AkuznetsovReddit.Web.Controllers;
using AkuznetsovReddit.Web.Mappers;
using AkuznetsovReddit.Web.Models;
using Moq;
using System.Linq;
using System.Web.Mvc;
using Xunit;

namespace AkuznetsovReddit.UnitTest.Web.Controllers
{
    public class TopicControllerUT : HttpContextHelper
    {
        private TopicController _cut;
        private Mock<ITopicService> _topicService;

        public TopicControllerUT()
        {
            _topicService = new Mock<ITopicService>();
            _cut = new TopicController(_topicService.Object);
            AutoMapperConfig.Execute();
        }

        [Fact]
        public void Create_Invalid_HasError_Post()
        {
            _topicService.Setup(c => c.CreateTopic(It.IsAny<TopicDto>(), It.IsAny<ValidationMessageList>()))
                .Callback((TopicDto post, ValidationMessageList inMessages) => inMessages.Add(new ValidationMessage(MessageTypes.Error, ErrorMessages.NO_TOPIC)));
            ViewResult actual = _cut.Create(It.IsAny<TopicVm>()) as ViewResult;
            string error = actual.ViewData.ModelState.Where(m => m.Key == string.Empty)
                .Select(m => m.Value).FirstOrDefault().Errors.Select(e => e.ErrorMessage).FirstOrDefault();
            Assert.Equal("Create", actual.ViewName);
            Assert.Equal(error, ErrorMessages.NO_TOPIC);
        }

        [Fact]
        public void Create_Invalid_ModelState_Post()
        {
            _cut.ModelState.AddModelError("error", "test error");

            RedirectToRouteResult actual = _cut.Create(It.IsAny<TopicVm>()) as RedirectToRouteResult;
            Assert.True(actual.RouteValues.ContainsValue("Create"));
        }

        [Fact]
        public void Create_Valid()
        {
            ViewResult actual = _cut.Create() as ViewResult;
            Assert.Equal("", actual.ViewName);
        }

        [Fact]
        public void Create_Valid_Post()
        {
            RedirectToRouteResult actual = _cut.Create(new TopicVm()) as RedirectToRouteResult;
            Assert.True(actual.RouteValues.ContainsValue("Index"));
        }

        [Fact]
        public void Delete_Invalid_Get()
        {
            _topicService.Setup(c => c.GetTopicByTopicId(It.IsAny<int>(), It.IsAny<ValidationMessageList>()))
                .Callback((int inId, ValidationMessageList inMessages) => inMessages.Add(new ValidationMessage(MessageTypes.Error, ErrorMessages.NO_TOPIC)));

            ViewResult actual = _cut.Delete(It.IsAny<int>()) as ViewResult;
            Assert.Equal("Index", actual.ViewName);
        }

        [Fact]
        public void Delete_Invalid_Post()
        {
            _topicService.Setup(c => c.DeleteTopic(It.IsAny<int>(), It.IsAny<TopicDto>(), It.IsAny<ValidationMessageList>()))
                .Callback((int inId, TopicDto dto, ValidationMessageList inMessages) => inMessages.Add(new ValidationMessage(MessageTypes.Error, ErrorMessages.NO_TOPIC)));

            ViewResult actual = _cut.Delete(It.IsAny<int>(), new TopicVm()) as ViewResult;
            Assert.Equal("Delete", actual.ViewName);
        }

        [Fact]
        public void Delete_Valid_Get()
        {
            ViewResult actual = _cut.Delete(It.IsAny<int>()) as ViewResult;
            Assert.Equal("", actual.ViewName);
        }

        [Fact]
        public void Delete_Valid_Post()
        {
            RedirectToRouteResult actual = _cut.Delete(It.IsAny<int>(), new TopicVm()) as RedirectToRouteResult;
            Assert.True(actual.RouteValues.ContainsValue("Index"));
        }

        [Fact]
        public void Edit_Invalid_Get()
        {
            _topicService.Setup(c => c.GetTopicByTopicId(It.IsAny<int>(), It.IsAny<ValidationMessageList>()))
                .Callback((int inId, ValidationMessageList inMessages) => inMessages.Add(new ValidationMessage(MessageTypes.Error, ErrorMessages.NO_POST)));

            ViewResult actual = _cut.Edit(It.IsAny<int>()) as ViewResult;
            Assert.Equal("Edit", actual.ViewName);
        }

        [Fact]
        public void Edit_Invalid_HasError_Post()
        {
            _topicService.Setup(c => c.EditTopic(It.IsAny<int>(), It.IsAny<TopicDto>(), It.IsAny<ValidationMessageList>()))
                .Callback((int i, TopicDto topic, ValidationMessageList inMessages) => inMessages.Add(new ValidationMessage(MessageTypes.Error, ErrorMessages.NO_TOPIC)));

            ViewResult actual = _cut.Edit(It.IsAny<int>(), It.IsAny<TopicVm>()) as ViewResult;
            string error = actual.ViewData.ModelState.Where(m => m.Key == string.Empty)
               .Select(m => m.Value).FirstOrDefault().Errors.Select(e => e.ErrorMessage).FirstOrDefault();
            Assert.Equal("Edit", actual.ViewName);
            Assert.Equal(error, ErrorMessages.NO_TOPIC);
        }

        [Fact]
        public void Edit_Invalid_ModelState_Post()
        {
            _cut.ModelState.AddModelError("error", "test error");

            ViewResult actual = _cut.Edit(It.IsAny<int>(), It.IsAny<TopicVm>()) as ViewResult;
            Assert.Equal("Edit", actual.ViewName);
        }

        [Fact]
        public void Edit_Valid_Get()
        {
            SessionManager.User = new UserDto();
            ViewResult actual = _cut.Edit(It.IsAny<int>()) as ViewResult;
            Assert.Equal("", actual.ViewName);
        }

        [Fact]
        public void Edit_Valid_Post()
        {
            RedirectToRouteResult actual = _cut.Edit(It.IsAny<int>(), It.IsAny<TopicVm>()) as RedirectToRouteResult;
            Assert.True(actual.RouteValues.ContainsValue("Index"));
        }

        [Fact]
        public void Index_Valid()
        {
            ViewResult actual = _cut.Index() as ViewResult;
            Assert.Equal("", actual.ViewName);
        }

        [Fact]
        public void Restore_Invalid()
        {
            _topicService.Setup(c => c.RestoreTopic(It.IsAny<int>(), It.IsAny<ValidationMessageList>()))
               .Callback((int id, ValidationMessageList inMessages) => inMessages.Add(new ValidationMessage(MessageTypes.Error, ErrorMessages.NO_TOPIC)));

            ViewResult actual = _cut.Restore(It.IsAny<int>()) as ViewResult;
            string error = actual.ViewData.ModelState.Where(m => m.Key == string.Empty)
               .Select(m => m.Value).FirstOrDefault().Errors.Select(e => e.ErrorMessage).FirstOrDefault();
            Assert.Equal("", actual.ViewName);
            Assert.Equal(error, ErrorMessages.NO_TOPIC);
        }

        [Fact]
        public void Restore_Valid()
        {
            RedirectToRouteResult actual = _cut.Restore(It.IsAny<int>()) as RedirectToRouteResult;
            Assert.True(actual.RouteValues.ContainsValue("Index"));
        }
    }
}
