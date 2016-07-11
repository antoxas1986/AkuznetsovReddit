using AkuznetsovReddit.Core.Constants;
using AkuznetsovReddit.Core.Enums;
using AkuznetsovReddit.Core.Models;
using AkuznetsovReddit.Core.Utilities;
using AkuznetsovReddit.Data.Entities;
using AkuznetsovReddit.Data.Repository.Interfaces;
using AkuznetsovReddit.Services;
using AkuznetsovReddit.UnitTest.Helpers;
using AkuznetsovReddit.Web.Mappers;
using Moq;
using System.Collections.Generic;
using Xunit;

namespace AkuznetsovReddit.UnitTest.Services
{
    public class PostsServiceUT : HttpContextHelper
    {
        private Mock<IPostsRepo> _postRepo;
        private List<PostFullDto> _posts;
        private PostsService _sut;
        private Mock<IUnitOfWork> _unit;

        public PostsServiceUT()
        {
            _postRepo = new Mock<IPostsRepo>();
            _unit = new Mock<IUnitOfWork>();
            _sut = new PostsService(_postRepo.Object, _unit.Object);
            _posts = new List<PostFullDto>
            {
                new PostFullDto
                {
                    PostId = 1,
                    Description="Test"
                }
            };

            AutoMapperConfig.Execute();
        }

        [Fact]
        public void CreatePost_Valid()
        {
            SessionManager.User = new UserDto() { UserId = 1 };
            PostFullDto post = new PostFullDto
            {
                Description = "Test",
            };
            _sut.CreatePost(post, new ValidationMessageList());
            _postRepo.Verify(p => p.CreatePost(It.IsAny<Post>()), Times.Once);
            _unit.Verify(u => u.SaveChanges(), Times.Once);
        }

        [Fact]
        public void DeletePost_Invalid()
        {
            ValidationMessageList messages = new ValidationMessageList();
            PostFullDto post = new PostFullDto
            {
                PostId = 1,
                Description = "Test"
            };
            _postRepo.Setup(p => p.EditPost(It.IsAny<PostFullDto>(), messages))
                .Callback((PostFullDto p, ValidationMessageList ml) => ml.Add(new ValidationMessage(MessageTypes.Error, ErrorMessages.NO_POST)));
            _sut.DeletePost(post, messages);
            _unit.Verify(u => u.SaveChanges(), Times.Never);
        }

        [Fact]
        public void DeletePost_Valid()
        {
            _sut.DeletePost(new PostFullDto(), new ValidationMessageList());
            _postRepo.Verify(p => p.EditPost(It.IsAny<PostFullDto>(), It.IsAny<ValidationMessageList>()), Times.Once);
            _unit.Verify(u => u.SaveChanges(), Times.Once);
        }

        [Fact]
        public void EditPost_Invalid_Post()
        {
            ValidationMessageList messages = new ValidationMessageList();
            PostFullDto post = new PostFullDto
            {
                PostId = 1,
                Description = "Test"
            };
            _postRepo.Setup(p => p.EditPost(It.IsAny<PostFullDto>(), It.IsAny<ValidationMessageList>()))
                .Callback((PostFullDto p, ValidationMessageList list) => list.Add(new ValidationMessage(MessageTypes.Error, ErrorMessages.NO_POST)));
            _sut.EditPost(post, messages);
            Assert.Equal(messages.GetFirstErrorMsg, ErrorMessages.NO_POST);
            _unit.Verify(u => u.SaveChanges(), Times.Never);
        }

        [Fact]
        public void EditPost_Valid()
        {
            PostFullDto post = new PostFullDto
            {
                PostId = 1,
                Description = "Test very long description that takes more then 150 symbols to create this" +
                              " long description, that will used for this test and something else to make it bigger."
            };
            int a = post.Description.Length;
            _sut.EditPost(post, new ValidationMessageList());
            _unit.Verify(u => u.SaveChanges(), Times.Once);
        }

        [Fact]
        public void GetFullPostByPostId_Admin()
        {
            SessionManager.User = new UserDto
            {
                Role = new RoleDto
                {
                    RoleName = Roles.ADMIN
                }
            };
            ValidationMessageList messages = new ValidationMessageList();
            _sut.GetFullPostByPostId(1, messages);
            _postRepo.Verify(p => p.GetForAdminPostsByPostId(It.IsAny<int>(), It.IsAny<ValidationMessageList>()), Times.Once);
        }

        [Fact]
        public void GetFullPostByPostId_User()
        {
            SessionManager.User = new UserDto
            {
                Role = new RoleDto
                {
                    RoleName = Roles.USER
                }
            };
            ValidationMessageList messages = new ValidationMessageList();
            _sut.GetFullPostByPostId(1, messages);
            _postRepo.Verify(p => p.GetPostByPostId(It.IsAny<int>(), It.IsAny<ValidationMessageList>()), Times.Once);
        }

        [Fact]
        public void RestorePost_Invalid()
        {
            ValidationMessageList messages = new ValidationMessageList();
            _sut.RestorePost(It.IsAny<int>(), messages);
            Assert.Equal(ErrorMessages.NO_POST, messages.GetFirstErrorMsg);
        }

        [Fact]
        public void RestorePost_Valid()
        {
            _postRepo.Setup(p => p.GetForAdminPostsByPostId(It.IsAny<int>(), It.IsAny<ValidationMessageList>())).Returns(new PostFullDto());
            _sut.RestorePost(It.IsAny<int>(), new ValidationMessageList());
            _unit.Verify(u => u.SaveChanges(), Times.AtLeastOnce);
        }
    }
}
