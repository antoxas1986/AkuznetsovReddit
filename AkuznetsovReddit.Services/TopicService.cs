using AkuznetsovReddit.Core.Constants;
using AkuznetsovReddit.Core.Enums;
using AkuznetsovReddit.Core.Models;
using AkuznetsovReddit.Core.Utilities;
using AkuznetsovReddit.Data.Repository.Interfaces;
using AkuznetsovReddit.Services.Interfaces;
using System.Collections.Generic;

namespace AkuznetsovReddit.Services
{
    /// <summary>
    /// Implementation of ITopicService Interface
    /// </summary>
    /// <seealso cref="AkuznetsovReddit.Services.Interfaces.ITopicService" />
    public class TopicService : ITopicService
    {
        private ITopicRepo _topicRepo;
        private IUnitOfWork _unitOfWork;

        public TopicService(ITopicRepo topicRepo, IUnitOfWork unitOfWork)
        {
            _topicRepo = topicRepo;
            _unitOfWork = unitOfWork;
        }

        /// <summary>
        /// Creates the topic.
        /// </summary>
        /// <param name="model">The model.</param>
        /// <param name="messages">The messages.</param>
        public void CreateTopic(TopicDto model, ValidationMessageList messages)
        {
            model.IsActive = true;
            _topicRepo.CreateTopic(model);
            _unitOfWork.SaveChanges();
        }

        /// <summary>
        /// Deletes the topic.
        /// </summary>
        /// <param name="id">The identifier.</param>
        /// <param name="dto">The dto.</param>
        /// <param name="messages">The messages.</param>
        public void DeleteTopic(int id, TopicDto dto, ValidationMessageList messages)
        {
            TopicDto topic = _topicRepo.GetTopicByTopicId(id, messages);
            if (topic == null)
            {
                messages.Add(new ValidationMessage(MessageTypes.Error, ErrorMessages.NO_TOPIC));
                return;
            }
            topic.IsActive = false;
            _topicRepo.EditTopic(topic, messages);
            _unitOfWork.SaveChanges();
        }

        /// <summary>
        /// Edits the topic.
        /// </summary>
        /// <param name="id">The identifier.</param>
        /// <param name="dto">The dto.</param>
        /// <param name="messages">The messages.</param>
        public void EditTopic(int id, TopicDto dto, ValidationMessageList messages)
        {
            if (id != dto.TopicId)
            {
                messages.Add(new ValidationMessage(MessageTypes.Error, ErrorMessages.ID_NOT_MATCH));
                return;
            }

            _topicRepo.EditTopic(dto, messages);

            if (!messages.HasError)
            {
                _unitOfWork.SaveChanges();
            }
        }

        /// <summary>
        /// Gets all topics.
        /// </summary>
        /// <returns></returns>
        public ICollection<TopicDto> GetAllTopics()
        {
            UserDto user = SessionManager.User;
            if (user.Role.RoleName.Equals(Roles.ADMIN))
            {
                return _topicRepo.GetAdminAllTopic();
            }

            return _topicRepo.GetAllTopics();
        }

        /// <summary>
        /// Gets the topic by topic identifier.
        /// </summary>
        /// <param name="id">The identifier.</param>
        /// <param name="messages">The messages.</param>
        /// <returns></returns>
        public TopicDto GetTopicByTopicId(int id, ValidationMessageList messages)
        {
            return _topicRepo.GetTopicByTopicId(id, messages);
        }

        /// <summary>
        /// Gets the topic with posts by topic identifier.
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="id">The identifier.</param>
        /// <param name="messages">The messages.</param>
        /// <returns></returns>
        public T GetTopicWithPostsByTopicId<T>(int id, ValidationMessageList messages)
        {
            return _topicRepo.GetTopicWithPostsByTopicId<T>(id);
        }

        /// <summary>
        /// Restores the inactive topic.
        /// </summary>
        /// <param name="id">The identifier.</param>
        /// <param name="messages">The messages.</param>
        public void RestoreTopic(int id, ValidationMessageList messages)
        {
            TopicDto topic = _topicRepo.GetTopicByTopicId(id, messages);
            if (topic == null)
            {
                messages.Add(new ValidationMessage(MessageTypes.Error, ErrorMessages.NO_TOPIC));
                return;
            }
            topic.IsActive = true;
            _topicRepo.EditTopic(topic, messages);
            _unitOfWork.SaveChanges();
        }
    }
}
