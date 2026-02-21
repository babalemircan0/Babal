using System.ComponentModel.DataAnnotations;

namespace Babal.Models
{
    public class DiaryEntry
    {
        public int UserId { get; set; } // Bu notun hangi kullanıcıya ait olduğunu belirleyen pasaport

        [Key]
        public int Id { get; set; }

        [Required]
        public string Title { get; set; }

        [Required]
        public string Content { get; set; }

        public DateTime CreatedDate { get; set; } = DateTime.Now;
    }
}