using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace eventhub.Models
{
    public class EventMember
    {
        [Key]
        public int eventMemberID { get; set; }
        public int UserID { get; set; }
        public int ID { get; set; }
        public virtual Event Events { get; set; }
    }
}