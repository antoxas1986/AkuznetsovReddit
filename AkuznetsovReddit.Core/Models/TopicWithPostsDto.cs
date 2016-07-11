using System.Collections.Generic;

namespace AkuznetsovReddit.Core.Models
{
    public class TopicWithPostsDto : TopicDto
    {
        public ICollection<PostShortDto> Posts { get; set; }
    }
}
