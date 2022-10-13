using System.ComponentModel.DataAnnotations;

namespace WebApplication5.Domain.Entities
{
    public class City
    {
        [Required]
        [Key]
        public int Id { get; set; }
        [Required]
        public string Name { get; set; }

        public City(string name)
        {
            Name = name;
        }
    }
}
