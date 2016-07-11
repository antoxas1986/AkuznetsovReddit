namespace AkuznetsovReddit.Data.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class Initial : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.Posts",
                c => new
                    {
                        PostId = c.Int(nullable: false, identity: true),
                        CreationDate = c.String(),
                        Description = c.String(),
                        IsActive = c.Boolean(nullable: false),
                        PostName = c.String(),
                        ShortDescription = c.String(),
                        TopicId = c.Int(nullable: false),
                        UserId = c.Int(nullable: false),
                    })
                .PrimaryKey(t => t.PostId)
                .ForeignKey("dbo.Topics", t => t.TopicId, cascadeDelete: true)
                .ForeignKey("dbo.Users", t => t.UserId, cascadeDelete: true)
                .Index(t => t.TopicId)
                .Index(t => t.UserId);
            
            CreateTable(
                "dbo.Topics",
                c => new
                    {
                        TopicId = c.Int(nullable: false, identity: true),
                        IsActive = c.Boolean(nullable: false),
                        TopicName = c.String(),
                    })
                .PrimaryKey(t => t.TopicId);
            
            CreateTable(
                "dbo.Users",
                c => new
                    {
                        UserId = c.Int(nullable: false, identity: true),
                        RoleId = c.Int(nullable: false),
                        UserName = c.String(),
                    })
                .PrimaryKey(t => t.UserId)
                .ForeignKey("dbo.Roles", t => t.RoleId, cascadeDelete: true)
                .Index(t => t.RoleId);
            
            CreateTable(
                "dbo.Roles",
                c => new
                    {
                        RoleId = c.Int(nullable: false, identity: true),
                        RoleName = c.String(),
                    })
                .PrimaryKey(t => t.RoleId);
            
            CreateTable(
                "dbo.UserCreds",
                c => new
                    {
                        UserId = c.Int(nullable: false),
                        FailedLoginAttempts = c.Int(nullable: false),
                        IsDisabled = c.Boolean(nullable: false),
                        LastFailedAttempt = c.DateTime(nullable: false),
                        PasswordHash = c.String(),
                        Salt = c.String(),
                    })
                .PrimaryKey(t => t.UserId)
                .ForeignKey("dbo.Users", t => t.UserId)
                .Index(t => t.UserId);
            
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.UserCreds", "UserId", "dbo.Users");
            DropForeignKey("dbo.Users", "RoleId", "dbo.Roles");
            DropForeignKey("dbo.Posts", "UserId", "dbo.Users");
            DropForeignKey("dbo.Posts", "TopicId", "dbo.Topics");
            DropIndex("dbo.UserCreds", new[] { "UserId" });
            DropIndex("dbo.Users", new[] { "RoleId" });
            DropIndex("dbo.Posts", new[] { "UserId" });
            DropIndex("dbo.Posts", new[] { "TopicId" });
            DropTable("dbo.UserCreds");
            DropTable("dbo.Roles");
            DropTable("dbo.Users");
            DropTable("dbo.Topics");
            DropTable("dbo.Posts");
        }
    }
}
