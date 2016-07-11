using AkuznetsovReddit.Core.Models;
using AkuznetsovReddit.Data.Entities;
using System.Collections.Generic;

namespace AkuznetsovReddit.Data.Repository.Interfaces
{
    /// <summary>
    /// Interface to work with posts
    /// </summary>
    public interface IPostsRepo
    {
        /// <summary>
        /// Creates the post.
        /// </summary>
        /// <param name="dto">The dto.</param>
        void CreatePost(Post dto);

        /// <summary>
        /// Edits the post.
        /// </summary>
        /// <param name="dto">The dto.</param>
        /// <param name="messages">The messages.</param>
        void EditPost(PostFullDto dto, ValidationMessageList messages);

        /// <summary>
        /// Gets for admin posts by post identifier.
        /// </summary>
        /// <param name="id">The identifier.</param>
        /// <param name="messages">The messages.</param>
        /// <returns></returns>
        PostFullDto GetForAdminPostsByPostId(int id, ValidationMessageList messages);

        /// <summary>
        /// Gets the post by post identifier.
        /// </summary>
        /// <param name="id">The identifier.</param>
        /// <param name="messages">The messages.</param>
        /// <returns></returns>
        PostFullDto GetPostByPostId(int id, ValidationMessageList messages);
    }
}
