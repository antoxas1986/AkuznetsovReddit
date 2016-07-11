using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Web;

namespace AkuznetsovReddit.Web.Models
{
    public class TopicVm
    {
        public bool IsActive { get; set; }
        public int TopicId { get; set; }

        [DisplayName("Topic name")]
        public string TopicName { get; set; }
    }
}
