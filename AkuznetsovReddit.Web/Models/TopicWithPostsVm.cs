using System.Collections.Generic;

namespace AkuznetsovReddit.Web.Models
{
    public class TopicWithPostsVm : TopicVm
    {
        public ICollection<PostShortVm> Posts { get; set; }
    }
}
