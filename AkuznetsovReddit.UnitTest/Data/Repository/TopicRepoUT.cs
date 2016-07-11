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
    public class TopicRepoUT
    {
        private Mock<IRedditContext> _db;
        private DbSetHelper _helper;
        private TopicRepo _rut;
        private List<Topic> _topics;
        private Mock<DbSet<Topic>> _topicSet;

        public TopicRepoUT()
        {
            _db = new Mock<IRedditContext>();
            _helper = new DbSetHelper();
            _rut = new TopicRepo(_db.Object);
            _topics = new List<Topic>
            {
                new Topic
                {
                    TopicId = 1,
                    TopicName="Test",
                    IsActive = true,
                    Posts = new List<Post>
                    {
                        new Post
                        {
                            IsActive=true,
                            PostId=1,
                            Description="Test"
                        },
                        new Post
                        {
                            IsActive=false,
                            PostId =2,
                            Description="Falsy Test"
                        }
                    }
                },
                new Topic
                {
                    TopicId = 2,
                    TopicName="Test",
                    IsActive=false
                }
            };

            _topicSet = _helper.GetDbSet(_topics);
            _db.Setup(c => c.Topics).Returns(_topicSet.Object);

            AutoMapperConfig.Execute();
        }

        [Fact]
        public void CreateTopic_Void()
        {
            _rut.CreateTopic(new TopicDto());
            _topicSet.Verify(d => d.Add(It.IsAny<Topic>()), Times.Once);
        }

        [Fact]
        public void EditTopic_Invalid()
        {
            ValidationMessageList messages = new ValidationMessageList();
            TopicDto topic = new TopicDto
            {
                TopicId = 99,
                TopicName = "Test",
            };
            _rut.EditTopic(topic, messages);
            Assert.Equal(messages.GetFirstErrorMsg, ErrorMessages.NO_TOPIC);
        }

        [Fact]
        public void EditTopic_Valid()
        {
            ValidationMessageList messages = new ValidationMessageList();
            TopicDto topic = new TopicDto
            {
                TopicId = 1,
                TopicName = "Test",
            };
            _rut.EditTopic(topic, messages);
            Assert.Null(messages.GetFirstErrorMsg);
        }

        [Fact]
        public void GetAdminAllTopic_Valid()
        {
            ICollection<TopicDto> topics = _rut.GetAdminAllTopic();
            Assert.Equal(topics.Count(), _topics.Count());
        }

        [Fact]
        public void GetAllTopics_Valid()
        {
            ICollection<TopicDto> topics = _rut.GetAllTopics();
            Assert.Equal(topics.Count(), 1);
        }

        [Fact]
        public void GetTopicByTopicId_Valid()
        {
            TopicDto topic = _rut.GetTopicByTopicId(1, new ValidationMessageList());
            Assert.Equal(topic.TopicName, _topics[0].TopicName);
        }

        /// <summary>
        /// This test is to make sure Admin gets topic with ALL posts including inactive posts.
        /// </summary>
        [Fact]
        public void GetTopicWithPostsByTopicId_Admin_Valid()
        {
            AdminTopicWithPostsDto topic = _rut.GetTopicWithPostsByTopicId<AdminTopicWithPostsDto>(1);
            Assert.Equal(topic.TopicName, _topics[0].TopicName);
            Assert.Equal(topic.Posts.Count, 2);
        }

        /// <summary>
        /// This test is to make sure User gets topic with ONLY active posts.
        /// </summary>
        [Fact]
        public void GetTopicWithPostsByTopicId_Valid()
        {
            TopicWithPostsDto topic = _rut.GetTopicWithPostsByTopicId<TopicWithPostsDto>(1);
            Assert.Equal(topic.TopicName, _topics[0].TopicName);
            Assert.Equal(topic.Posts.Count, 1);
        }
    }
}
