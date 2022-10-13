//using Microsoft.AspNetCore.Identity;
//using System.ComponentModel.DataAnnotations;
//using System.Text.Json.Serialization;

//namespace WebApplication2.Domain.Entities
//{
//    public class User 
//    {
//        [Required]
//        [Key]
//        public int Id { get; private set; }
//        [Required]
//        public string NickName { get; set; }
//        [Required]
//        [JsonIgnore]
//        public string PasswordHash { get; set; }
//        [Required]
//        public bool IsAdmin { get; set; }
//        public int OfficeId { get; set; }
//        public User(string nickName, string passwordHash, int officeId, bool isAdmin = false)
//        {
//            NickName = nickName;
//            PasswordHash = passwordHash;
//            OfficeId = officeId;
//            IsAdmin = isAdmin;
//        }
//        public override int GetHashCode()
//        {
//            return Id;
//        }
//    }
//}
