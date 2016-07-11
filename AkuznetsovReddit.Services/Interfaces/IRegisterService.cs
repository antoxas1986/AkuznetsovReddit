using AkuznetsovReddit.Core.Models;

namespace AkuznetsovReddit.Services.Interfaces
{
    /// <summary>
    /// Api to work with Register service
    /// </summary>
    public interface IRegisterService
    {
        /// <summary>
        /// Registers the user.
        /// </summary>
        /// <param name="register">The register.</param>
        /// <param name="messages">The messages.</param>
        void RegisterUser(RegisterDto register, ValidationMessageList messages);
    }
}
