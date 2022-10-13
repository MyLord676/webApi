
using System.ComponentModel.DataAnnotations;

namespace WebApplication5.Domain.Entities
{
    public class PostWebApi
    {
        [Required]
        [Key]
        public int Id { get; set; }
        public string? Title { get; set; } = string.Empty;
        public string? Text { get; set; } = string.Empty;
        public string? PhotoBase64 { get; set; }
        public int? OfficeId { get; set; }
        public DateTime DateOfCreation { get; set; }
        public PostWebApi()
        {
            DateOfCreation = DateTime.UtcNow;
        }
    }
}