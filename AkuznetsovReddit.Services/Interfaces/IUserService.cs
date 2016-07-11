using AkuznetsovReddit.Core.Models;
using System.Collections.Generic;

namespace AkuznetsovReddit.Services.Interfaces
{
    /// <summary>
    /// Interface to work with User Service
    /// </summary>
    public interface IUserService
    {
        /// <summary>
        /// Gets all users.
        /// </summary>
        /// <returns></returns>
        ICollection<UserDto> GetAllUsers();

        /// <summary>
        /// Gets the user by user identifier.
        /// </summary>
        /// <param name="id">The identifier.</param>
        /// <returns></returns>
        UserDto GetUserByUserId(int id);
    }
}
