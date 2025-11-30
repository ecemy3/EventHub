using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace eventhub.Models
{
    public class Event
    {
        [Key]
        public int ID { get; set; }

        [Column(TypeName = "VarChar")]
        [MaxLength(100)]
        [Required]
        public string EventName { get; set; }

        [Column(TypeName = "VarChar")]
        [MaxLength(500)]
        public string Description { get; set; }

        [Required]
        public DateTime Date { get; set; }

        [Column(TypeName = "VarChar")]
        [MaxLength(200)]
        [Required]
        public string Location { get; set; }
        [Column(TypeName = "VarChar")]
        [MaxLength(200)]
        [Required]
        public string EventBanner { get; set; }
        public int EventCategoryID { get; set;}
        public int UserID { get; set; }
        public int status { get; set; }
        public virtual EventCategory EventCategory { get; set; }
        public virtual User User { get; set; }

    }
}
