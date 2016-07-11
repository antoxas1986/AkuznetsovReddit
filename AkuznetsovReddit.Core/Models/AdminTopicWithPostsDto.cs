using System.Collections.Generic;

namespace AkuznetsovReddit.Core.Models
{
    public class AdminTopicWithPostsDto : TopicDto
    {
        public ICollection<AdminPostShortDto> Posts { get; set; }
    }
}
