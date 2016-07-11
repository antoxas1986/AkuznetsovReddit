using AkuznetsovReddit.Core.Models;
using System.Collections.Generic;

namespace AkuznetsovReddit.Data.Repository.Interfaces
{
    /// <summary>
    /// Interface to work with topics.
    /// </summary>
    public interface ITopicRepo
    {
        /// <summary>
        /// Creates the topic.
        /// </summary>
        /// <param name="model">The model.</param>
        void CreateTopic(TopicDto model);

        /// <summary>
        /// Edits the topic.
        /// </summary>
        /// <param name="dto">The dto.</param>
        /// <param name="messages">The messages.</param>
        void EditTopic(TopicDto dto, ValidationMessageList messages);

        /// <summary>
        /// Gets the all topic including inactive topics for admin role.
        /// </summary>
        /// <returns></returns>
        ICollection<TopicDto> GetAdminAllTopic();

        /// <summary>
        /// Gets all active topics.
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
        /// <returns></returns>
        T GetTopicWithPostsByTopicId<T>(int id);
    }
}
