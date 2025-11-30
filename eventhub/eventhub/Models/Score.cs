using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Web;

namespace eventhub.Models
{
    public class Score
    {
       [Key]
       public int scoreID { get; set; }
       public int score { get; set; }
       public int UserID { get; set; }
       public virtual User User { get; set; }
    }
}
