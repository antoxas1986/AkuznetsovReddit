using AkuznetsovReddit.Core.Constants;
using AkuznetsovReddit.Core.Models;
using AkuznetsovReddit.Core.Utilities;
using AkuznetsovReddit.Data.Repository.Interfaces;
using AkuznetsovReddit.Services;
using AkuznetsovReddit.UnitTest.Helpers;
using AkuznetsovReddit.Web.Mappers;
using Moq;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xunit;

namespace AkuznetsovReddit.UnitTest.Services
{
    public class TopicServiceUT : HttpContextHelper
    {
        private TopicService _sut;
        private Mock<ITopicRepo> _topicRepo;
        private Mock<IUnitOfWork> _unit;

        public TopicServiceUT()
        {
            _topicRepo = new Mock<ITopicRepo>();
            _unit = new Mock<IUnitOfWork>();
            _sut = new TopicService(_topicRepo.Object, _unit.Object);
            AutoMapperConfig.Execute();
        }

        [Fact]
        public void CreateTopic_Valid()
        {
            _sut.CreateTopic(new TopicDto(), new ValidationMessageList());
            _topicRepo.Verify(t => t.CreateTopic(It.IsAny<TopicDto>()), Times.AtLeastOnce);
            _unit.Verify(u => u.SaveChanges(), Times.AtLeastOnce);
        }

        [Fact]
        public void DeleteTopic_Invalid()
        {
            ValidationMessageList messages = new ValidationMessageList();
            _sut.DeleteTopic(1, new TopicDto(), messages);
            Assert.Equal(messages.GetFirstErrorMsg, ErrorMessages.NO_TOPIC);
        }

        [Fact]
        public void DeleteTopic_Valid()
        {
            _topicRepo.Setup(t => t.GetTopicByTopicId(It.IsAny<int>(), It.IsAny<ValidationMessageList>())).Returns(new TopicDto());
            _sut.DeleteTopic(1, new TopicDto(), new ValidationMessageList());
            _topicRepo.Verify(t => t.EditTopic(It.IsAny<TopicDto>(), It.IsAny<ValidationMessageList>()), Times.AtLeastOnce);
            _unit.Verify(u => u.SaveChanges(), Times.AtLeastOnce);
        }

        [Fact]
        public void EditTopic_Invalid_Ids()
        {
            ValidationMessageList messages = new ValidationMessageList();
            TopicDto topic = new TopicDto()
            {
                TopicId = 1
            };
            _sut.EditTopic(99, topic, messages);
            Assert.Equal(messages.GetFirstErrorMsg, ErrorMessages.ID_NOT_MATCH);
        }

        [Fact]
        public void EditTopic_Valid()
        {
            ValidationMessageList messages = new ValidationMessageList();
            TopicDto topic = new TopicDto()
            {
                TopicId = 1
            };
            _sut.EditTopic(1, topic, messages);
            Assert.Null(messages.GetFirstErrorMsg);
            _unit.Verify(u => u.SaveChanges(), Times.AtLeastOnce);
        }

        [Fact]
        public void GetTopicByTopicId_Admin()
        {
            SessionManager.User = new UserDto
            {
                Role = new RoleDto
                {
                    RoleName = Roles.ADMIN
                }
            };
            _sut.GetAllTopics();
            _topicRepo.Verify(t => t.GetAdminAllTopic(), Times.Once);
        }

        [Fact]
        public void GetTopicByTopicId_User()
        {
            SessionManager.User = new UserDto
            {
                Role = new RoleDto
                {
                    RoleName = Roles.USER
                }
            };
            _sut.GetAllTopics();
            _topicRepo.Verify(t => t.GetAllTopics(), Times.Once);
        }

        [Fact]
        public void GetTopicByTopicId_Valid()
        {
            TopicDto expected = new TopicDto()
            {
                TopicId = 1,
                IsActive = true,
                TopicName = "Test"
            };
            _topicRepo.Setup(t => t.GetTopicByTopicId(It.IsAny<int>(), It.IsAny<ValidationMessageList>())).Returns(expected);
            TopicDto actual = _sut.GetTopicByTopicId(1, new ValidationMessageList());
            Assert.Equal(expected.TopicName, actual.TopicName);
        }

        [Fact]
        public void GetTopicWithPostByTopicId_Valid()
        {
            TopicWithPostsDto expected = new TopicWithPostsDto()
            {
                TopicId = 1,
                IsActive = true,
                TopicName = "Test"
            };
            _topicRepo.Setup(t => t.GetTopicWithPostsByTopicId<TopicWithPostsDto>(It.IsAny<int>())).Returns(expected);
            TopicWithPostsDto actual = _sut.GetTopicWithPostsByTopicId<TopicWithPostsDto>(1, new ValidationMessageList());
            Assert.Equal(expected.TopicName, actual.TopicName);
        }

        [Fact]
        public void RestoreTopic_Invalid()
        {
            ValidationMessageList messages = new ValidationMessageList();
            _sut.RestoreTopic(1, messages);
            Assert.Equal(messages.GetFirstErrorMsg, ErrorMessages.NO_TOPIC);
        }

        [Fact]
        public void RestoreTopic_Valid()
        {
            _topicRepo.Setup(t => t.GetTopicByTopicId(It.IsAny<int>(), It.IsAny<ValidationMessageList>())).Returns(new TopicDto());
            _sut.RestoreTopic(1, new ValidationMessageList());
            _topicRepo.Verify(t => t.EditTopic(It.IsAny<TopicDto>(), It.IsAny<ValidationMessageList>()), Times.AtLeastOnce);
            _unit.Verify(u => u.SaveChanges(), Times.AtLeastOnce);
        }
    }
}
