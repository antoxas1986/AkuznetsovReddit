using AkuznetsovReddit.Data.Context.Interfaces;
using AkuznetsovReddit.Data.Entities;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AkuznetsovReddit.Data.Context
{
    /// <summary>
    /// Db context for application
    /// </summary>
    /// <seealso cref="System.Data.Entity.DbContext" />
    /// <seealso cref="AkuznetsovReddit.Data.Context.Interfaces.IRedditContext" />
    public class RedditContext : DbContext, IRedditContext
    {
        public RedditContext() : base("RedditContext")
        {
        }

        public DbSet<Post> Posts { get; set; }
        public DbSet<Role> Roles { get; set; }
        public DbSet<Topic> Topics { get; set; }
        public DbSet<UserCred> UserCreds { get; set; }
        public DbSet<User> Users { get; set; }
    }
}
