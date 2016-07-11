using AkuznetsovReddit.Services.Interfaces;
using System.Web.Helpers;

namespace AkuznetsovReddit.Service
{
    /// <summary>
    /// Implementation of ICryptoService Interface
    /// </summary>
    /// <seealso cref="AkuznetsovReddit.Services.Interfaces.ICryptoService" />
    public class CryptoService : ICryptoService
    {
        /// <summary>
        /// Gets the hash for password.
        /// </summary>
        /// <param name="password">The password.</param>
        /// <returns></returns>
        public string GetHash(string password)
        {
            var hashed = Crypto.Hash(password, "SHA256");
            return hashed;
        }
    }
}
