namespace AkuznetsovReddit.Services.Interfaces
{
    /// <summary>
    /// Inteface to create hash for passwords
    /// </summary>
    public interface ICryptoService
    {
        /// <summary>
        /// Gets the hash for password.
        /// </summary>
        /// <param name="password">The password.</param>
        /// <returns></returns>
        string GetHash(string password);
    }
}
