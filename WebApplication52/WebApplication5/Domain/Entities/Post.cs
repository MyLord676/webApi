using System.ComponentModel.DataAnnotations;
using System.Data.SqlTypes;

namespace WebApplication5.Domain.Entities
{
    public class Post
    {
        [Required]
        [Key]
        public int Id { get; set; }
        public string? Title { get; set; } = string.Empty;
        public string? Text { get; set; } = string.Empty;
        public string? PhotoPath { get; set; }
        public int? OfficeId { get; set; }
        public DateTime DateOfCreation { get; set; }
        public Post(DateTime dateOfCreation)
        {
            DateOfCreation = dateOfCreation;
        }
    }
}
