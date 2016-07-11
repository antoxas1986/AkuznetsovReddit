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
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web.Mvc;
using Xunit;

namespace AkuznetsovReddit.UnitTest.Web.Controllers
{
    public class PostControllerUT : HttpContextHelper
    {
        private PostsController _cut;
        private Mock<IPostsService> _postsService;
        private Mock<ITopicService> _topicService;
        private Mock<IUserService> _userService;

        public PostControllerUT()
        {
            _postsService = new Mock<IPostsService>();
            _topicService = new Mock<ITopicService>();
            _userService = new Mock<IUserService>();
            _cut = new PostsController(_postsService.Object, _topicService.Object, _userService.Object);
            AutoMapperConfig.Execute();
        }

        [Fact]
        public void Create_Invalid_HasError_Post()
        {
            _postsService.Setup(c => c.CreatePost(It.IsAny<PostFullDto>(), It.IsAny<ValidationMessageList>()))
                .Callback((PostFullDto post, ValidationMessageList inMessages) => inMessages.Add(new ValidationMessage(MessageTypes.Error, ErrorMessages.NO_POST)));
            ViewResult actual = _cut.Create(It.IsAny<PostFullVm>()) as ViewResult;
            string error = actual.ViewData.ModelState.Where(m => m.Key == string.Empty)
                .Select(m => m.Value).FirstOrDefault().Errors.Select(e => e.ErrorMessage).FirstOrDefault();
            Assert.Equal("", actual.ViewName);
            Assert.Equal(error, ErrorMessages.NO_POST);
        }

        [Fact]
        public void Create_Invalid_ModelState_Post()
        {
            _cut.ModelState.AddModelError("error", "test error");

            ViewResult actual = _cut.Create(It.IsAny<PostFullVm>()) as ViewResult;
            Assert.Equal("Create", actual.ViewName);
        }

        [Fact]
        public void Create_Valid()
        {
            ViewResult actual = _cut.Create(It.IsAny<int>()) as ViewResult;
            Assert.Equal("", actual.ViewName);
        }

        [Fact]
        public void Create_Valid_Post()
        {
            RedirectToRouteResult actual = _cut.Create(new PostFullVm()) as RedirectToRouteResult;
            Assert.True(actual.RouteValues.ContainsValue("Index"));
        }

        [Fact]
        public void Delete_Invalid_Get()
        {
            _postsService.Setup(c => c.GetFullPostByPostId(It.IsAny<int>(), It.IsAny<ValidationMessageList>()))
                .Callback((int inId, ValidationMessageList inMessages) => inMessages.Add(new ValidationMessage(MessageTypes.Error, ErrorMessages.NO_POST)));

            HttpNotFoundResult actual = _cut.Delete(1) as HttpNotFoundResult;
            Assert.Equal("404", actual.StatusCode.ToString());
        }

        [Fact]
        public void Delete_Valid_Get()
        {
            _postsService.Setup(c => c.GetFullPostByPostId(It.IsAny<int>(), It.IsAny<ValidationMessageList>())).Returns(new PostFullDto());
            RedirectToRouteResult actual = _cut.Delete(1) as RedirectToRouteResult;
            Assert.True(actual.RouteValues.ContainsValue("Index"));
            _postsService.Verify(p => p.DeletePost(It.IsAny<PostFullDto>(), It.IsAny<ValidationMessageList>()), Times.AtLeastOnce);
        }

        [Fact]
        public void Details_Invalid()
        {
            _postsService.Setup(c => c.GetFullPostByPostId(It.IsAny<int>(), It.IsAny<ValidationMessageList>()))
                .Callback((int inId, ValidationMessageList inMessages) => inMessages.Add(new ValidationMessage(MessageTypes.Error, ErrorMessages.NO_POST)));

            HttpNotFoundResult actual = _cut.Details(1) as HttpNotFoundResult;

            Assert.Equal("404", actual.StatusCode.ToString());
        }

        [Fact]
        public void Details_Valid()
        {
            PostFullDto post = new PostFullDto() { UserId = 1 };
            _postsService.Setup(c => c.GetFullPostByPostId(It.IsAny<int>(), It.IsAny<ValidationMessageList>())).Returns(post);
            ViewResult actual = _cut.Details(1) as ViewResult;
            Assert.Equal("", actual.ViewName);
        }

        [Fact]
        public void Edit_Invalid_Get()
        {
            _postsService.Setup(c => c.GetFullPostByPostId(It.IsAny<int>(), It.IsAny<ValidationMessageList>()))
                .Callback((int inId, ValidationMessageList inMessages) => inMessages.Add(new ValidationMessage(MessageTypes.Error, ErrorMessages.NO_POST)));

            HttpNotFoundResult actual = _cut.Edit(1, 1) as HttpNotFoundResult;

            Assert.Equal("404", actual.StatusCode.ToString());
        }

        [Fact]
        public void Edit_Invalid_HasError_Post()
        {
            _postsService.Setup(c => c.EditPost(It.IsAny<PostFullDto>(), It.IsAny<ValidationMessageList>()))
                .Callback((PostFullDto post, ValidationMessageList inMessages) => inMessages.Add(new ValidationMessage(MessageTypes.Error, ErrorMessages.NO_POST)));

            ViewResult actual = _cut.Edit(It.IsAny<PostFullVm>()) as ViewResult;
            string error = actual.ViewData.ModelState.Where(m => m.Key == string.Empty)
               .Select(m => m.Value).FirstOrDefault().Errors.Select(e => e.ErrorMessage).FirstOrDefault();
            Assert.Equal("Edit", actual.ViewName);
            Assert.Equal(error, ErrorMessages.NO_POST);
        }

        [Fact]
        public void Edit_Invalid_ModelState_Post()
        {
            _cut.ModelState.AddModelError("error", "test error");

            ViewResult actual = _cut.Edit(It.IsAny<PostFullVm>()) as ViewResult;
            Assert.Equal("", actual.ViewName);
        }

        [Fact]
        public void Edit_Invalid_NonAdmin()
        {
            PostFullDto post = new PostFullDto() { UserId = 1 };
            UserDto user = new UserDto()
            {
                Role = new RoleDto() { RoleName = Roles.USER },
                UserId = 2
            };
            SessionManager.User = user;
            _postsService.Setup(c => c.GetFullPostByPostId(It.IsAny<int>(), It.IsAny<ValidationMessageList>())).Returns(post);
            RedirectToRouteResult actual = _cut.Edit(1, 1) as RedirectToRouteResult;
            Assert.True(actual.RouteValues.ContainsValue("Forbidden"));
        }

        [Fact]
        public void Edit_Valid_Get()
        {
            PostFullDto post = new PostFullDto() { UserId = 1 };
            UserDto user = new UserDto()
            {
                Role = new RoleDto() { RoleName = Roles.ADMIN },
                UserId = 1
            };
            SessionManager.User = user;
            _postsService.Setup(c => c.GetFullPostByPostId(It.IsAny<int>(), It.IsAny<ValidationMessageList>())).Returns(post);
            ViewResult actual = _cut.Edit(1, 1) as ViewResult;
            Assert.Equal("", actual.ViewName);
        }

        [Fact]
        public void Edit_Valid_Post()
        {
            PostFullVm post = new PostFullVm() { PostId = 1 };
            _postsService.Setup(c => c.GetFullPostByPostId(It.IsAny<int>(), It.IsAny<ValidationMessageList>())).Returns(new PostFullDto());
            RedirectToRouteResult actual = _cut.Edit(post) as RedirectToRouteResult;
            Assert.True(actual.RouteValues.ContainsValue("Details"));
        }

        [Fact]
        public void Index_Valid_Admin()
        {
            UserDto user = new UserDto()
            {
                Role = new RoleDto() { RoleName = Roles.ADMIN },
                UserId = 1
            };
            SessionManager.User = user;
            ViewResult actual = _cut.Index(1) as ViewResult;
            Assert.Equal("", actual.ViewName);
        }

        [Fact]
        public void Index_Valid_User()
        {
            UserDto user = new UserDto()
            {
                Role = new RoleDto() { RoleName = Roles.USER },
                UserId = 1
            };
            SessionManager.User = user;
            ViewResult actual = _cut.Index(1) as ViewResult;
            Assert.Equal("", actual.ViewName);
        }

        [Fact]
        public void Restore_Invalid()
        {
            _postsService.Setup(c => c.RestorePost(It.IsAny<int>(), It.IsAny<ValidationMessageList>()))
               .Callback((int id, ValidationMessageList inMessages) => inMessages.Add(new ValidationMessage(MessageTypes.Error, ErrorMessages.NO_POST)));

            ViewResult actual = _cut.Restore(It.IsAny<int>(), It.IsAny<int>()) as ViewResult;
            string error = actual.ViewData.ModelState.Where(m => m.Key == string.Empty)
               .Select(m => m.Value).FirstOrDefault().Errors.Select(e => e.ErrorMessage).FirstOrDefault();
            Assert.Equal("Index", actual.ViewName);
            Assert.Equal(error, ErrorMessages.NO_POST);
        }

        [Fact]
        public void Restore_Valid()
        {
            RedirectToRouteResult actual = _cut.Restore(1, 1) as RedirectToRouteResult;
            Assert.True(actual.RouteValues.ContainsValue("Index"));
        }
    }
}
