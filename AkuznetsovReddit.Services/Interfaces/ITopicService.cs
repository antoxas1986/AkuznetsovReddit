using AkuznetsovReddit.Core.Models;
using System.Collections.Generic;

namespace AkuznetsovReddit.Services.Interfaces
{
    /// <summary>
    /// Api to work with Topic Service
    /// </summary>
    public interface ITopicService
    {
        /// <summary>
        /// Creates the topic.
        /// </summary>
        /// <param name="model">The model.</param>
        /// <param name="messages">The messages.</param>
        void CreateTopic(TopicDto model, ValidationMessageList messages);

        /// <summary>
        /// Deletes the topic.
        /// </summary>
        /// <param name="id">The identifier.</param>
        /// <param name="dto">The dto.</param>
        /// <param name="messages">The messages.</param>
        void DeleteTopic(int id, TopicDto dto, ValidationMessageList messages);

        /// <summary>
        /// Edits the topic.
        /// </summary>
        /// <param name="id">The identifier.</param>
        /// <param name="dto">The dto.</param>
        /// <param name="messages">The messages.</param>
        void EditTopic(int id, TopicDto dto, ValidationMessageList messages);

        /// <summary>
        /// Gets all topics.
        /// </summary>
        /// <returns></returns>
        ICollection<TopicDto> GetAllTopics();

        /// <summary>
        /// Gets the topic by topic identifier.
        /// </summary>
        /// <param name="id">The identifier.</param>
        /// <param name="messages">The messages.</param>
        /// <returns></returns>
        TopicDto GetTopicByTopicId(int id, ValidationMessageList messages);

        /// <summary>
        /// Gets the topic with posts by topic identifier.
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="id">The identifier.</param>
        /// <param name="messages">The messages.</param>
        /// <returns></returns>
        T GetTopicWithPostsByTopicId<T>(int id, ValidationMessageList messages);

        /// <summary>
        /// Restores the inactive topic.
        /// </summary>
        /// <param name="id">The identifier.</param>
        /// <param name="messages">The messages.</param>
        void RestoreTopic(int id, ValidationMessageList messages);
    }
}
