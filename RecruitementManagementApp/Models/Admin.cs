using System.ComponentModel.DataAnnotations;

namespace RecruitementManagementApp.Models
{
    public class Admin
    {
        [Key]
        public int IdAdmin { get; set; }

        [Required]
   
        public string Name { get; set; }
        [Required]
        public string Numero { get; set; }

        [Required]
        public string Email { get; set; }
    }
}
