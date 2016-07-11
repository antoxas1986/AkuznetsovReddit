using AkuznetsovReddit.Core.Models;
using AkuznetsovReddit.Data.Entities;
using System.Collections.Generic;

namespace AkuznetsovReddit.Data.Repository.Interfaces
{
    /// <summary>
    /// Inteface to works with User model.
    /// </summary>
    public interface IUserRepo
    {
        /// <summary>
        /// Edits the user.
        /// </summary>
        /// <param name="userSalt">The user salt.</param>
        /// <param name="messages">The messages.</param>
        void EditUser(UserWithUserCredDto userSalt, ValidationMessageList messages);

        /// <summary>
        /// Gets all users.
        /// </summary>
        /// <returns></returns>
        ICollection<UserDto> GetAllUsers();

        /// <summary>
        /// Gets the user by hash password.
        /// </summary>
        /// <param name="username">The username.</param>
        /// <param name="passwordHash">The password hash.</param>
        /// <param name="messages">The messages.</param>
        /// <returns></returns>
        UserDto GetUserByHashPassword(string username, string passwordHash, ValidationMessageList messages);

        /// <summary>
        /// Gets the user by user identifier.
        /// </summary>
        /// <param name="id">The identifier.</param>
        /// <returns></returns>
        UserDto GetUserByUserId(int id);

        /// <summary>
        /// Gets the name of the user by user.
        /// </summary>
        /// <param name="username">The username.</param>
        /// <returns></returns>
        UserDto GetUserByUserName(string username);

        /// <summary>
        /// Gets the user with user cred by user name.
        /// </summary>
        /// <param name="userName">Name of the user.</param>
        /// <returns></returns>
        UserWithUserCredDto GetUserWithUserCredByUserName(string userName);

        /// <summary>
        /// Registers the new user.
        /// </summary>
        /// <param name="user">The user.</param>
        void RegisterNewUser(User user);
    }
}
