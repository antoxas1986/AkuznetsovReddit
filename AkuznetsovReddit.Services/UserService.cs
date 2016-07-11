using AkuznetsovReddit.Core.Models;
using AkuznetsovReddit.Data.Repository.Interfaces;
using AkuznetsovReddit.Services.Interfaces;
using System.Collections.Generic;

namespace AkuznetsovReddit.Services
{
    /// <summary>
    /// Implementation for IUserService Interface
    /// </summary>
    /// <seealso cref="AkuznetsovReddit.Services.Interfaces.IUserService" />
    public class UserService : IUserService
    {
        private IUserRepo _userRepo;

        public UserService(IUserRepo userRepo)
        {
            _userRepo = userRepo;
        }

        /// <summary>
        /// Gets all users.
        /// </summary>
        /// <returns></returns>
        public ICollection<UserDto> GetAllUsers()
        {
            return _userRepo.GetAllUsers();
        }

        /// <summary>
        /// Gets the user by user identifier.
        /// </summary>
        /// <param name="id">The identifier.</param>
        /// <returns></returns>
        public UserDto GetUserByUserId(int id)
        {
            return _userRepo.GetUserByUserId(id);
        }
    }
}
