using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace eventhub.Models
{
    public class User
    {
        [Key]
        public int UserID { get; set; }

        [Column(TypeName = "VarChar")]
        [MaxLength(50)]
        [Required]
        public string Username { get; set; }

        [Column(TypeName = "VarChar")]
        [MaxLength(300)]
        [Required]
        public string Password { get; set; }

        [Column(TypeName = "VarChar")]
        [MaxLength(300)]
        [Required]
        public string Email { get; set; }

        [Column(TypeName = "VarChar")]
        [MaxLength(100)]
        public string Location { get; set; }

        [Column(TypeName = "VarChar")]
        [MaxLength(5000)]
        public string Interests { get; set; }

        [Column(TypeName = "VarChar")]
        [MaxLength(50)]
        [Required]
        public string FirstName { get; set; }

        [Column(TypeName = "VarChar")]
        [MaxLength(50)]
        [Required]
        public string LastName { get; set; }

        [Required]
        public DateTime BirthDate { get; set; }

        [Column(TypeName = "VarChar")]
        [MaxLength(10)]
        public string Gender { get; set; }

        [Column(TypeName = "VarChar")]
        [MaxLength(20)]
        public string PhoneNumber { get; set; }

        [Column(TypeName = "VarChar")]
        [MaxLength(300)]
        public string ProfilePicture { get; set; }

        [Required]
        public int Role { get; set; } // 0 = No Access, 1 = View Only, 2 = Edit but cannot delete, 3 = Full Access
    }
}
