namespace AkuznetsovReddit.Data.Migrations
{
    using Entities;
    using System;
    using System.Collections.Generic;
    using System.Data.Entity;
    using System.Data.Entity.Migrations;
    using System.Linq;

    internal sealed class Configuration : DbMigrationsConfiguration<AkuznetsovReddit.Data.Context.RedditContext>
    {
        public Configuration()
        {
            AutomaticMigrationsEnabled = true;
        }

        protected override void Seed(AkuznetsovReddit.Data.Context.RedditContext context)
        {
            List<Role> roles = new List<Role>() {
                new Role()
                {
                    RoleId = 1,
                    RoleName = "User"
                },
                new Role()
                {
                    RoleId = 2,
                    RoleName = "Admin"
                }
            };

            foreach (Role role in roles)
            {
                context.Roles.AddOrUpdate(role);
            }
            context.SaveChanges();

            List<Topic> topics = new List<Topic>()
            {
                new Topic() { TopicId = 1, TopicName = "Finance", IsActive = true },
                new Topic() { TopicId = 2, TopicName = "Cars", IsActive = true },
                new Topic() { TopicId = 3, TopicName = "Technologies", IsActive = true }
            };

            foreach (Topic topic in topics)
            {
                context.Topics.AddOrUpdate(topic);
            }
            context.SaveChanges();

            User user = new User() { UserId = 1, UserName = "admin", RoleId = 2 };
            context.Users.AddOrUpdate(user);
            context.SaveChanges();

            //password = "test"
            UserCred usercred = new UserCred()
            {
                UserId = 1,
                Salt = "5b7c1ead-c87d-429c-9949-69477a7d293c",
                PasswordHash = "1610FA495E5CAED5668D3D34E5DE69833A7DEE4A909C0BFADDD23470C2065B01",
                IsDisabled = false,
                FailedLoginAttempts = 0,
                LastFailedAttempt = DateTime.Now
            };
            context.UserCreds.AddOrUpdate(usercred);
            context.SaveChanges();

            List<Post> posts = new List<Post>()
            {
                new Post()
                {
                    PostId = 1,
                    CreationDate = "Thursday, July 7, 2016",
                    Description = "The autonomous vehicles can keep an eye out for bicycles in the dark, too. " +
                                  "Google is working to expand the capabilities of its self - driving vehicle fleet, and according " +
                                  "to its latest progress report(PDF), it's making strides in sharing the road with cyclists. One of the ways " +
                                  "the folks in Mountain View are doing that is by using onboard sensors to gauge and interpret a cyclist's " +
                                  "intent. Our sensors can detect a cyclist's hand signals as an indication of an intention to make a " +
                                  "turn or shift over, the company's June autonomous vehicle report reads. Cyclists often make hand " +
                                  "signals far in advance of a turn, and our software is designed to remember previous signals from a " +
                                  "rider so it can better anticipate a rider's turn down the road. Machine learning is helping there, " +
                                  "making sure that unicycles and fatbikes are recognized for what they are, and keeping the autonomous " +
                                  "vehicles out of the two - wheeled traffic's way. The report goes on to say that the vehicle software " +
                                  "is even getting advanced enough to take into account cyclists riding in the dark and avoiding accidents " +
                                  "with them. With how prevalent bicycles are becoming on our roads it's incredibly important " +
                                  "that these situations be figured out now-- much like autonomous honking.",
                    ShortDescription = "The autonomous vehicles can keep an eye out for bicycles in the dark, too. Google is working to expand the capabilities of its self - driving vehicle...",
                    IsActive = true,
                    PostName = "Google's self-driving cars can read cyclists' hand signals",
                    TopicId = 2,
                    UserId = 1,
                },
            };
            foreach (Post post in posts)
            {
                context.Posts.AddOrUpdate(post);
            }
            context.SaveChanges();
        }
    }
}
