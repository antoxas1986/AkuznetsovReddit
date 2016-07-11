using AkuznetsovReddit.Core.Models;

namespace AkuznetsovReddit.Services.Interfaces
{
    /// <summary>
    /// Api to work with login service
    /// </summary>
    public interface ILoginService
    {
        /// <summary>
        /// Logins the user into session.
        /// </summary>
        /// <param name="user">The user.</param>
        /// <param name="messages">The validation messages.</param>
        void LoginUser(LoginDto user, ValidationMessageList messages);

        /// <summary>
        /// Logoffs user form session.
        /// </summary>
        void Logoff();
    }
}
