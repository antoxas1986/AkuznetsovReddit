using AkuznetsovReddit.Core.Constants;
using AkuznetsovReddit.Core.Enums;
using AkuznetsovReddit.Core.Models;
using AkuznetsovReddit.Data.Context.Interfaces;
using AkuznetsovReddit.Data.Entities;
using AkuznetsovReddit.Data.Repository.Interfaces;
using AutoMapper;
using AutoMapper.QueryableExtensions;
using System.Linq;

namespace AkuznetsovReddit.Data.Repository
{
    /// <summary>
    /// Implementation of IPostRepo inteface
    /// </summary>
    /// <seealso cref="AkuznetsovReddit.Data.Repository.Interfaces.IPostsRepo" />
    public class PostsRepo : IPostsRepo
    {
        private IRedditContext _db;

        public PostsRepo(IRedditContext db)
        {
            _db = db;
        }

        /// <summary>
        /// Creates the post.
        /// </summary>
        /// <param name="dto">The dto.</param>
        public void CreatePost(Post dto)
        {
            _db.Posts.Add(dto);
        }

        /// <summary>
        /// Edits the post.
        /// </summary>
        /// <param name="dto">The dto.</param>
        /// <param name="messages">The messages.</param>
        public void EditPost(PostFullDto dto, ValidationMessageList messages)
        {
            Post post = _db.Posts.Where(c => c.PostId == dto.PostId).FirstOrDefault();
            if (post == null)
            {
                messages.Add(new ValidationMessage(MessageTypes.Error, ErrorMessages.NO_POST));
                return;
            }
            Mapper.Map(dto, post);
        }

        /// <summary>
        /// Gets for admin posts by post identifier.
        /// </summary>
        /// <param name="id">The identifier.</param>
        /// <param name="messages">The messages.</param>
        /// <returns></returns>
        public PostFullDto GetForAdminPostsByPostId(int id, ValidationMessageList messages)
        {
            PostFullDto post = _db.Posts.Where(p => p.PostId == id).ProjectTo<PostFullDto>().FirstOrDefault();
            if (post == null)
            {
                messages.Add(new ValidationMessage(MessageTypes.Error, ErrorMessages.NO_POST));
            }
            return post;
        }

        /// <summary>
        /// Gets the post by post identifier.
        /// </summary>
        /// <param name="id">The identifier.</param>
        /// <param name="messages">The messages.</param>
        /// <returns></returns>
        public PostFullDto GetPostByPostId(int id, ValidationMessageList messages)
        {
            PostFullDto post = _db.Posts.Where(p => p.PostId == id && p.IsActive == true).ProjectTo<PostFullDto>().FirstOrDefault();
            if (post == null)
            {
                messages.Add(new ValidationMessage(MessageTypes.Error, ErrorMessages.NO_POST));
            }
            return post;
        }
    }
}
