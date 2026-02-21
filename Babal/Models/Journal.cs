using Babal.Models;
using System;
using System.ComponentModel.DataAnnotations;

namespace BabalSystem.Models
{
    public class Journal
    {
        [Key]
        public int Id { get; set; }

        [Required]
        public string Content { get; set; }

        public DateTime CreatedDate { get; set; } = DateTime.Now;

        // Bu notun hangi kullanıcıya ait olduğunu belirtmek için:
        public int UserId { get; set; }
        public virtual User User { get; set; }
    }
}