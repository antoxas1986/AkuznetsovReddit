using System;

namespace AkuznetsovReddit.Data.Entities
{
    public class Post
    {
        public string CreationDate { get; set; }
        public string Description { get; set; }
        public bool IsActive { get; set; }
        public int PostId { get; set; }
        public string PostName { get; set; }
        public string ShortDescription { get; set; }
        public virtual Topic Topic { get; set; }
        public int TopicId { get; set; }
        public virtual User User { get; set; }
        public int UserId { get; set; }
    }
}
