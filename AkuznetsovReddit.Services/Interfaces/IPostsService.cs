using AkuznetsovReddit.Core.Models;

namespace AkuznetsovReddit.Services.Interfaces
{
    /// <summary>
    /// Api to work with Post service
    /// </summary>
    public interface IPostsService
    {
        /// <summary>
        /// Creates the post.
        /// </summary>
        /// <param name="dto">The dto.</param>
        /// <param name="messages">The messages.</param>
        void CreatePost(PostFullDto dto, ValidationMessageList messages);

        /// <summary>
        /// Deletes the post.
        /// </summary>
        /// <param name="postDto">The post dto.</param>
        /// <param name="messages">The messages.</param>
        void DeletePost(PostFullDto postDto, ValidationMessageList messages);

        /// <summary>
        /// Edits the post.
        /// </summary>
        /// <param name="dto">The dto.</param>
        /// <param name="messages">The messages.</param>
        void EditPost(PostFullDto dto, ValidationMessageList messages);

        /// <summary>
        /// Gets the full post by post identifier.
        /// </summary>
        /// <param name="id">The identifier.</param>
        /// <param name="messages">The messages.</param>
        /// <returns></returns>
        PostFullDto GetFullPostByPostId(int id, ValidationMessageList messages);

        /// <summary>
        /// Restores the post.
        /// </summary>
        /// <param name="id">The identifier.</param>
        /// <param name="messages">The messages.</param>
        void RestorePost(int id, ValidationMessageList messages);
    }
}
