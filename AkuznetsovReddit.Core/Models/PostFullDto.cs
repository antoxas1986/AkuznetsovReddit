using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AkuznetsovReddit.Core.Models
{
    public class PostFullDto
    {
        public string CreationDate { get; set; }
        public string Description { get; set; }
        public bool IsActive { get; set; }
        public int PostId { get; set; }
        public string PostName { get; set; }
        public string ShortDescription { get; set; }
        public int TopicId { get; set; }
        public int UserId { get; set; }
    }
}
