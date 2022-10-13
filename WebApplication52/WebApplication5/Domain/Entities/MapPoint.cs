using System.ComponentModel.DataAnnotations;
using System.Data.SqlTypes;
using System.Xml.Linq;

namespace WebApplication5.Domain.Entities
{
    public class MapPoint
    {
        [Required]
        [Key]
        public int Id { get; set; }
        [Required]
        public string Lat { get; set; }
        [Required]
        public string Lng { get; set; }
        [Required]
        public int City { get; set; }
        public string? Title { get; set; } = string.Empty;
        public string? Text { get; set; } = string.Empty;
        public string? PhotoPath { get; set; } 
        [Required]
        public int Likes { get; set; }
        [Required]
        public int DisLikes { get; set; }
        [Required]
        public bool TinkoffCashBack { get; set; }
        [Required]
        public string Comments { get; set; } 
        [Required]
        public DateTime DateOfCreation { get; set; }
        public MapPoint(string lat, string lng, int city, string comments, DateTime dateOfCreation, bool tinkoffCashBack = false, int likes = 0, int disLikes = 0)
        {
            Lat = lat;
            Lng = lng;
            City = city;
            Likes = likes;
            DisLikes = disLikes;
            TinkoffCashBack = tinkoffCashBack;
            Comments = comments;
            DateOfCreation = dateOfCreation;
        }
    }
}
