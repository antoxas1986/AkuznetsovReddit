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
    /// Implementation of IUserRepo Interface
    /// </summary>
    /// <seealso cref="AkuznetsovReddit.Data.Repository.Interfaces.IUserRepo" />
    public class UserRepo : IUserRepo
    {
        private IRedditContext _db;

        public UserRepo(IRedditContext db)
        {
            _db = db;
        }

        /// <summary>
        /// Edits the user.
        /// </summary>
        /// <param name="userSalt">The user salt.</param>
        /// <param name="messages">The messages.</param>
        public void EditUser(UserWithUserCredDto userSalt, ValidationMessageList messages)
        {
            User user = _db.Users.Where(u => u.UserId == userSalt.UserId).FirstOrDefault();
            if (user == null)
            {
                messages.Add(new ValidationMessage(MessageTypes.Error, ErrorMessages.NO_USER));
            }
            Mapper.Map(userSalt, user);
        }

        /// <summary>
        /// Gets all users.
        /// </summary>
        /// <returns></returns>
        public ICollection<UserDto> GetAllUsers()
        {
            return _db.Users.ProjectTo<UserDto>().ToList();
        }

        /// <summary>
        /// Gets the user by hash password and username.
        /// </summary>
        /// <param name="username">The username.</param>
        /// <param name="passwordHash">The password hash.</param>
        /// <param name="messages">The messages.</param>
        /// <returns></returns>
        public UserDto GetUserByHashPassword(string username, string passwordHash, ValidationMessageList messages)
        {
            UserDto user = _db.Users.Where(
                u => u.UserName == username &&
                u.UserCred.PasswordHash == passwordHash).ProjectTo<UserDto>().FirstOrDefault();

            if (user == null)
            {
                messages.Add(new ValidationMessage(MessageTypes.Error, ErrorMessages.BAD_CREDENTIALS));
                return user;
            }

            return user;
        }

        /// <summary>
        /// Gets the user by user identifier.
        /// </summary>
        /// <param name="id">The identifier.</param>
        /// <returns></returns>
        public UserDto GetUserByUserId(int id)
        {
            return _db.Users.Where(u => u.UserId == id).ProjectTo<UserDto>().FirstOrDefault();
        }

        /// <summary>
        /// Gets the user by user name.
        /// </summary>
        /// <param name="username">The username.</param>
        /// <returns></returns>
        public UserDto GetUserByUserName(string username)
        {
            return _db.Users.Where(u => u.UserName == username).ProjectTo<UserDto>().FirstOrDefault();
        }

        /// <summary>
        /// Gets the user with user cred by user name.
        /// </summary>
        /// <param name="userName">Name of the user.</param>
        /// <returns></returns>
        public UserWithUserCredDto GetUserWithUserCredByUserName(string userName)
        {
            return _db.Users.Where(u => u.UserName == userName).ProjectTo<UserWithUserCredDto>().FirstOrDefault();
        }

        /// <summary>
        /// Registers the new user.
        /// </summary>
        /// <param name="user">The user.</param>
        public void RegisterNewUser(User user)
        {
            _db.Users.Add(user);
        }
    }
}
