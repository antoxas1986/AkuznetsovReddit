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
    public class PostsRepoUT
    {
        private Mock<IRedditContext> _db;
        private DbSetHelper _helper;
        private List<Post> _posts;
        private Mock<DbSet<Post>> _postSet;
        private PostsRepo _rut;

        public PostsRepoUT()
        {
            _db = new Mock<IRedditContext>();
            _helper = new DbSetHelper();
            _rut = new PostsRepo(_db.Object);
            _posts = new List<Post>
            {
                new Post
                {
                    PostId = 1,
                    Description="Test",
                    IsActive=true
                },
                new Post
                {
                    PostId = 2,
                    Description="Test",
                    IsActive=false
                },
                 new Post
                {
                    PostId = 3,
                    Description="Test",
                    IsActive=false
                }
            };

            _postSet = _helper.GetDbSet(_posts);
            _db.Setup(c => c.Posts).Returns(_postSet.Object);

            AutoMapperConfig.Execute();
        }

        [Fact]
        public void CreatePost_Post_Valid()
        {
            _rut.CreatePost(new Post());
            _postSet.Verify(p => p.Add(It.IsAny<Post>()), Times.Once);
        }

        [Fact]
        public void EditPost_Invalid()
        {
            ValidationMessageList messages = new ValidationMessageList();
            PostFullDto post = new PostFullDto
            {
                PostId = 99,
                Description = "Test",
                IsActive = false
            };
            _rut.EditPost(new PostFullDto(), messages);
            Assert.Equal(messages.GetFirstErrorMsg, ErrorMessages.NO_POST);
        }

        [Fact]
        public void EditPost_Valid()
        {
            ValidationMessageList messages = new ValidationMessageList();
            PostFullDto post = new PostFullDto
            {
                PostId = 1,
                Description = "Test",
                IsActive = true
            };
            _rut.EditPost(post, messages);
            Assert.Null(messages.GetFirstErrorMsg);
        }

        [Fact]
        public void GetForAdminPostByPostId_Valid()
        {
            PostFullDto post = _rut.GetForAdminPostsByPostId(1, new ValidationMessageList());
            Assert.Equal(post.PostId, _posts[0].PostId);
        }

        [Fact]
        public void GetPostByPostId_Valid()
        {
            PostFullDto post = _rut.GetPostByPostId(1, new ValidationMessageList());
            Assert.True(post.IsActive);
            //Make sure it`s not return Post with IsActive = false;
            PostFullDto post2 = _rut.GetPostByPostId(2, new ValidationMessageList());
            Assert.Null(post2);
        }
    }
}
