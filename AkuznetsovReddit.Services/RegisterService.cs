using AkuznetsovReddit.Core.Constants;
using AkuznetsovReddit.Core.Enums;
using AkuznetsovReddit.Core.Models;
using AkuznetsovReddit.Data.Entities;
using AkuznetsovReddit.Data.Repository.Interfaces;
using AkuznetsovReddit.Services.Interfaces;
using System;

namespace AkuznetsovReddit.Services
{
    /// <summary>
    /// Implementation of IRegisterService Interface
    /// </summary>
    /// <seealso cref="AkuznetsovReddit.Services.Interfaces.IRegisterService" />
    public class RegisterService : IRegisterService
    {
        private ICryptoService _hash;
        private IRoleService _roleService;
        private IUnitOfWork _unit;
        private IUserRepo _userRepo;

        public RegisterService(IUserRepo userRepo, IUnitOfWork unit, IRoleService roleService, ICryptoService hash)
        {
            _userRepo = userRepo;
            _unit = unit;
            _roleService = roleService;
            _hash = hash;
        }

        /// <summary>
        /// Registers the user.
        /// </summary>
        /// <param name="register">The register.</param>
        /// <param name="messages">The messages.</param>
        public void RegisterUser(RegisterDto register, ValidationMessageList messages)
        {
            bool isExist = CheckForExistingUserName(register.UserName);

            if (isExist)
            {
                messages.Add(new ValidationMessage(MessageTypes.Error, ErrorMessages.USERNAME_EXIST));
                return;
            }

            string salt = Guid.NewGuid().ToString();
            User user = new User
            {
                UserName = register.UserName,
                RoleId = _roleService.GetRoleByRoleName(Roles.USER).RoleId,
                UserCred = new UserCred
                {
                    PasswordHash = _hash.GetHash(register.Password + salt),
                    Salt = salt,
                    LastFailedAttempt = DateTime.Now
                }
            };
            _userRepo.RegisterNewUser(user);
            _unit.SaveChanges();
        }

        /// <summary>
        /// Checks the username if exist in database.
        /// </summary>
        /// <param name="userName">Name of the user.</param>
        /// <returns></returns>
        private bool CheckForExistingUserName(string userName)
        {
            return _userRepo.GetUserByUserName(userName) != null;
        }
    }
}
