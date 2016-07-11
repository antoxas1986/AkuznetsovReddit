using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AkuznetsovReddit.Core.Models
{
    public class TopicDto
    {
        public bool IsActive { get; set; }
        public int TopicId { get; set; }
        public string TopicName { get; set; }
    }
}
