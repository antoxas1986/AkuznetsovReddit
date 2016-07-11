using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Web;

namespace AkuznetsovReddit.Web.Models
{
    public class PostFullVm
    {
        [DisplayName("Date of Creation")]
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
