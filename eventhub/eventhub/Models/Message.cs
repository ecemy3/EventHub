using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace eventhub.Models
{
    public class Message
    {
        [Key]
        public int MessageID { get; set; }
        public int EventID { get; set; }
        public virtual Event Event { get; set; }

    }
}
