using System.ComponentModel.DataAnnotations;

namespace Babal.Models
{
    public class Agenda
    {
        [Key]
        public int Id { get; set; }

        // Hücre kimliği (Örn: cell-0-8)
        public string? CellId { get; set; }

        // Yazılan görev açıklaması
        public string? TaskDescription { get; set; }
    }
}