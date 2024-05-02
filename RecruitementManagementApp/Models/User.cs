using System.ComponentModel;
using System.ComponentModel.DataAnnotations;

namespace RecruitementManagementApp.Models
{
    public class User
    {
        [Key]
        public int Id { get; set; }
        [Required]
        public string Name { get; set; }
        [Required]
        public string LastName { get; set; }
        [Required]
        public string Numero { get; set; }
        [EmailAddress]
        [Required]
        public string Email { get; set; }
        [PasswordPropertyText]
        [Required]
        public string Password { get; set; }
        [Required]
        public string Role { get; set; }
        public bool profilecompleted { get; set; }=false;
    }
}
