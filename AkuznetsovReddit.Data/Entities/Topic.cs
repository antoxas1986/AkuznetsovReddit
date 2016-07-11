using System.Collections.Generic;

namespace AkuznetsovReddit.Data.Entities
{
    public class Topic
    {
        public bool IsActive { get; set; }
        public virtual ICollection<Post> Posts { get; set; }
        public int TopicId { get; set; }
        public string TopicName { get; set; }
    }
}
