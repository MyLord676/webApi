using System.ComponentModel.DataAnnotations;

namespace WebApplication5.Domain.Entities
{
    public class Office
    {
        [Required]
        [Key]
        public int Id { get; set; }
        [Required]
        public int City { get; set; }
        [Required]
        public string Adress { get; set; }
        public Office(string adress, int city)
        {
            Adress = adress;
            City = city;
        }
    }
}
