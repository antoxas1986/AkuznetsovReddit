using System;

namespace AkuznetsovReddit.Core.Models
{
    public class PostShortDto
    {
        public string CreationDate { get; set; }
        public bool IsActive { get; set; }
        public int PostId { get; set; }
        public string PostName { get; set; }
        public string ShortDescription { get; set; }
        public int TopicId { get; set; }
        public int UserId { get; set; }
    }
}
