namespace AkuznetsovReddit.Data.Repository.Interfaces
{
    /// <summary>
    /// Inteface for unitofwork
    /// </summary>
    public interface IUnitOfWork
    {
        /// <summary>
        /// Saves the changes in Entity Framework.
        /// </summary>
        /// <returns></returns>
        int SaveChanges();
    }
}
