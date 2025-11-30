using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace eventhub.Models
{
    public class MessageDetail
    {
        [Key]
        public int MessageDetailID { get; set; }
        public int MessageID { get; set; }
        public int UserID { get; set; }
        [Column(TypeName="VarChar")]
        public string Comment { get; set; }
        public virtual Message Message { get; set; }
    }
}