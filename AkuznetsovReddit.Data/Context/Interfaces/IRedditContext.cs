using AkuznetsovReddit.Data.Entities;
using System;
using System.Data.Entity;

namespace AkuznetsovReddit.Data.Context.Interfaces
{
    public interface IRedditContext : IDisposable
    {
        DbSet<Post> Posts { get; set; }
        DbSet<Role> Roles { get; set; }
        DbSet<Topic> Topics { get; set; }
        DbSet<UserCred> UserCreds { get; set; }
        DbSet<User> Users { get; set; }

        int SaveChanges();
    }
}
