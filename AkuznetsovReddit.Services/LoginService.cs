using AkuznetsovReddit.Core.Constants;
using AkuznetsovReddit.Core.Enums;
using AkuznetsovReddit.Core.Models;
using AkuznetsovReddit.Core.Utilities;
using AkuznetsovReddit.Data.Repository.Interfaces;
using AkuznetsovReddit.Services.Interfaces;
using System;

namespace AkuznetsovReddit.Services
{
    /// <summary>
    /// Implementation for ILoginService Interface.
    /// </summary>
    /// <seealso cref="AkuznetsovReddit.Services.Interfaces.ILoginService" />
    public class LoginService : ILoginService
    {
        private ICryptoService _hash;
        private IRoleService _roleService;
        private IUnitOfWork _unit;
        private IUserRepo _userRepo;
        private UserDto user = null;
        private UserWithUserCredDto userSalt = null;

        public LoginService(IRoleService roleService, IUserRepo userRepo, ICryptoService hash, IUnitOfWork unit)
        {
            _roleService = roleService;
            _userRepo = userRepo;
            _hash = hash;
            _unit = unit;
        }

        /// <summary>
        /// Logins the user.
        /// </summary>
        /// <param name="login">The login.</param>
        /// <param name="messages">The messages.</param>
        public void LoginUser(LoginDto login, ValidationMessageList messages)
        {
            CheckIfUserExistByUserName(login, messages);
            if (!messages.HasError)
            {
                CkeckIfAbleToLogin(messages);
                if (!messages.HasError)
                {
                    FetchUserByUsernameHashPassword(login, messages);
                    if (!messages.HasError)
                    {
                        LoginUserToSession(messages);
                    }
                    else
                    {
                        userSalt.UserCred.FailedLoginAttempts++;
                        userSalt.UserCred.LastFailedAttempt = DateTime.Now;
                        _userRepo.EditUser(userSalt, messages);
                    }
                    _unit.SaveChanges();
                }
            }
        }

        /// <summary>
        /// Logoffs user form session.
        /// </summary>
        public void Logoff()
        {
            if (SessionManager.User != null)
            {
                SessionManager.Abandon();
            }
        }

        /// <summary>
        /// Checks if user exist by user name.
        /// </summary>
        /// <param name="login">The login.</param>
        /// <param name="messages">The messages.</param>
        private void CheckIfUserExistByUserName(LoginDto login, ValidationMessageList messages)
        {
            userSalt = _userRepo.GetUserWithUserCredByUserName(login.UserName);
            if (userSalt == null)
            {
                messages.Add(new ValidationMessage(MessageTypes.Error, ErrorMessages.BAD_CREDENTIALS));
            }
        }

        /// <summary>
        /// Ckecks if able to login.
        /// </summary>
        /// <param name="messages">The messages.</param>
        private void CkeckIfAbleToLogin(ValidationMessageList messages)
        {
            if (userSalt.UserCred.IsDisabled)
            {
                messages.Add(new ValidationMessage(MessageTypes.Error, ErrorMessages.ACCOUNT_DISABLED));
            }
            else if (IsNotAbleToLogin(userSalt))
            {
                messages.Add(new ValidationMessage(MessageTypes.Error, ErrorMessages.ACCOUNT_BLOCKED));
            }
        }

        /// <summary>
        /// Fetches the user by username and hash password.
        /// </summary>
        /// <param name="login">The login.</param>
        /// <param name="messages">The messages.</param>
        private void FetchUserByUsernameHashPassword(LoginDto login, ValidationMessageList messages)
        {
            string passwordHash = _hash.GetHash(login.Password + userSalt.UserCred.Salt);
            user = _userRepo.GetUserByHashPassword(login.UserName, passwordHash, messages);
        }

        /// <summary>
        /// Sub method to determine whether is able to login the specified
        /// user by 3 faild attempts and 30 min block.
        /// </summary>
        /// <param name="userSalt">The user salt.</param>
        /// <returns></returns>
        private bool IsNotAbleToLogin(UserWithUserCredDto userSalt)
        {
            TimeSpan span = new TimeSpan();
            bool isNotAbleToLogin = false;

            if (userSalt.UserCred.FailedLoginAttempts >= 3)
            {
                span = DateTime.Now - userSalt.UserCred.LastFailedAttempt;
                isNotAbleToLogin = span.Minutes < 30;
            }

            return isNotAbleToLogin;
        }

        /// <summary>
        /// Logins the user to session.
        /// </summary>
        /// <param name="messages">The messages.</param>
        private void LoginUserToSession(ValidationMessageList messages)
        {
            SessionManager.User = user;
            userSalt.UserCred.FailedLoginAttempts = 0;
            _userRepo.EditUser(userSalt, messages);
        }
    }
}
