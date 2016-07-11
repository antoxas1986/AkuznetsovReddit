using AkuznetsovReddit.Data.Context.Interfaces;
using AkuznetsovReddit.Data.Repository.Interfaces;
using System;

namespace AkuznetsovReddit.Data.Repository
{
    /// <summary>
    /// Implementation IUnitOfWork Interface
    /// </summary>
    /// <seealso cref="AkuznetsovReddit.Data.Repository.Interfaces.IUnitOfWork" />
    public class UnitOfWork : IUnitOfWork

    {
        private IRedditContext _db;
        private bool _disposed = false;

        public UnitOfWork(IRedditContext db)
        {
            _db = db;
        }

        /// <summary>
        /// Releases unmanaged and - optionally - managed resources.
        /// </summary>
        public void Dispose()
        {
            Dispose(true);
            GC.SuppressFinalize(this);
        }

        /// <summary>
        /// Saves the changes in Entity Framework.
        /// </summary>
        /// <returns></returns>
        public int SaveChanges()
        {
            return _db.SaveChanges();
        }

        protected virtual void Dispose(bool disposing)
        {
            if (!_disposed)
            {
                if (disposing)
                {
                    _db.Dispose();
                }
            }
            _disposed = true;
        }
    }
}
