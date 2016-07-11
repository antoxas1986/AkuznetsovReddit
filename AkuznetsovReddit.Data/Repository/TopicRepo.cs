using AkuznetsovReddit.Core.Constants;
using AkuznetsovReddit.Core.Enums;
using AkuznetsovReddit.Core.Models;
using AkuznetsovReddit.Data.Context.Interfaces;
using AkuznetsovReddit.Data.Entities;
using AkuznetsovReddit.Data.Repository.Interfaces;
using AutoMapper;
using AutoMapper.QueryableExtensions;
using System;
using System.Collections.Generic;
using System.Linq;

namespace AkuznetsovReddit.Data.Repository
{
    /// <summary>
    /// Implementation of ITopicRepo Interface
    /// </summary>
    /// <seealso cref="AkuznetsovReddit.Data.Repository.Interfaces.ITopicRepo" />
    public class TopicRepo : ITopicRepo
    {
        private IRedditContext _db;

        public TopicRepo(IRedditContext db)
        {
            _db = db;
        }

        /// <summary>
        /// Creates the topic.
        /// </summary>
        /// <param name="model">The model.</param>
        public void CreateTopic(TopicDto model)
        {
            Topic topic = Mapper.Map<Topic>(model);
            _db.Topics.Add(topic);
        }

        /// <summary>
        /// Edits the topic.
        /// </summary>
        /// <param name="dto">The dto.</param>
        /// <param name="messages">The messages.</param>
        public void EditTopic(TopicDto dto, ValidationMessageList messages)
        {
            Topic topic = _db.Topics.Where(t => t.TopicId == dto.TopicId).FirstOrDefault();
            if (topic == null)
            {
                messages.Add(new ValidationMessage(MessageTypes.Error, ErrorMessages.NO_TOPIC));
            }
            Mapper.Map(dto, topic);
        }

        /// <summary>
        /// Gets the all topic including inactive topics for admin role.
        /// </summary>
        /// <returns></returns>
        public ICollection<TopicDto> GetAdminAllTopic()
        {
            return _db.Topics.ProjectTo<TopicDto>().OrderByDescending(o => o.IsActive).ThenBy(o => o.TopicName).ToList();
        }

        /// <summary>
        /// Gets all active topics.
        /// </summary>
        /// <returns></returns>
        public ICollection<TopicDto> GetAllTopics()
        {
            return _db.Topics.Where(t => t.IsActive == true).ProjectTo<TopicDto>().OrderBy(o => o.TopicName).ToList();
        }

        /// <summary>
        /// Gets the topic by topic identifier.
        /// </summary>
        /// <param name="id">The identifier.</param>
        /// <param name="messages">The messages.</param>
        /// <returns></returns>
        public TopicDto GetTopicByTopicId(int id, ValidationMessageList messages)
        {
            return _db.Topics.Where(t => t.TopicId == id).ProjectTo<TopicDto>().OrderBy(o => o.TopicName).FirstOrDefault();
        }

        /// <summary>
        /// Gets the topic with posts by topic identifier.
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="id">The identifier.</param>
        /// <returns></returns>
        public T GetTopicWithPostsByTopicId<T>(int id)
        {
            // Example to filter child collection by parametr
            // var topic = _db.Topics.Where(t => t.TopicId == id).Select(g => new TopicWithPostsDto
            // {
            //     TopicId = g.TopicId,
            //     Posts = g.Posts.Where(u => u.IsActive == true)
            // }).FirstOrDefault();

            return _db.Topics.Where(t => t.TopicId == id).ProjectTo<T>().FirstOrDefault();
        }
    }
}
