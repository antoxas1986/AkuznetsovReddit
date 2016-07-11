using AkuznetsovReddit.Core.Constants;
using AkuznetsovReddit.Core.Enums;
using AkuznetsovReddit.Core.Models;
using AkuznetsovReddit.Core.Utilities;
using AkuznetsovReddit.Data.Entities;
using AkuznetsovReddit.Data.Repository.Interfaces;
using AkuznetsovReddit.Services.Interfaces;
using AutoMapper;
using System;

namespace AkuznetsovReddit.Services
{
    /// <summary>
    /// Implementation of IPostService Interface
    /// </summary>
    /// <seealso cref="AkuznetsovReddit.Services.Interfaces.IPostsService" />
    public class PostsService : IPostsService
    {
        private IPostsRepo _postsRepo;
        private IUnitOfWork _unitOfWork;

        public PostsService(IPostsRepo postsRepo, IUnitOfWork unitOfWork)
        {
            _postsRepo = postsRepo;
            _unitOfWork = unitOfWork;
        }

        /// <summary>
        /// Creates the post.
        /// </summary>
        /// <param name="dto">The dto.</param>
        /// <param name="messages">The messages.</param>
        public void CreatePost(PostFullDto dto, ValidationMessageList messages)
        {
            Post post = Mapper.Map<Post>(dto);

            post.UserId = SessionManager.User.UserId;
            post.ShortDescription = CreateShortDescription(post.Description);
            post.CreationDate = DateTime.Now.ToLongDateString();
            post.IsActive = true;

            _postsRepo.CreatePost(post);
            _unitOfWork.SaveChanges();
        }

        /// <summary>
        /// Deletes the post.
        /// </summary>
        /// <param name="postDto">The post dto.</param>
        /// <param name="messages">The messages.</param>
        public void DeletePost(PostFullDto postDto, ValidationMessageList messages)
        {
            postDto.IsActive = false;
            _postsRepo.EditPost(postDto, messages);

            if (!messages.HasError)
            {
                _unitOfWork.SaveChanges();
            }
        }

        /// <summary>
        /// Edits the post.
        /// </summary>
        /// <param name="dto">The dto.</param>
        /// <param name="messages">The messages.</param>
        public void EditPost(PostFullDto dto, ValidationMessageList messages)
        {
            dto.ShortDescription = CreateShortDescription(dto.Description);
            _postsRepo.EditPost(dto, messages);

            if (!messages.HasError)
            {
                _unitOfWork.SaveChanges();
            }
        }

        /// <summary>
        /// Gets the full post by post identifier.
        /// </summary>
        /// <param name="id">The identifier.</param>
        /// <param name="messages">The messages.</param>
        /// <returns></returns>
        public PostFullDto GetFullPostByPostId(int id, ValidationMessageList messages)
        {
            var user = SessionManager.User;
            if (user.Role.RoleName.Equals(Roles.ADMIN))
            {
                return _postsRepo.GetForAdminPostsByPostId(id, messages);
            }
            return _postsRepo.GetPostByPostId(id, messages);
        }

        /// <summary>
        /// Restores the inactive post.
        /// </summary>
        /// <param name="postId">The post identifier.</param>
        /// <param name="messages">The messages.</param>
        public void RestorePost(int postId, ValidationMessageList messages)
        {
            PostFullDto dto = _postsRepo.GetForAdminPostsByPostId(postId, messages);
            if (dto == null)
            {
                messages.Add(new ValidationMessage(MessageTypes.Error, ErrorMessages.NO_POST));
                return;
            }

            dto.IsActive = true;
            _postsRepo.EditPost(dto, messages);
            _unitOfWork.SaveChanges();
        }

        /// <summary>
        /// Creates the short description.
        /// </summary>
        /// <param name="description">The description.</param>
        /// <returns></returns>
        private string CreateShortDescription(string description)
        {
            if (description.Length < 150)
            {
                return description + "...";
            }
            return description.Substring(0, 150) + "...";
        }
    }
}
