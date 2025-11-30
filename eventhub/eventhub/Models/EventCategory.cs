using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Data.Entity;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
namespace eventhub.Models
{
    public class EventCategory
    {
        [Key]
        public int EventCategoryID { get; set; }
        [Column(TypeName = "VarChar")]
        public string EventCategoryName { get; set; }
    }
}