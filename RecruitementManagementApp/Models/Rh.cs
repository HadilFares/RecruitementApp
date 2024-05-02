using System.ComponentModel.DataAnnotations;

namespace RecruitementManagementApp.Models
{
    public class Rh
    {
        [Key]
        public int IdRh { get; set; }
        [Required]
        [Display(Name = "CompanyName")]
        public string Name { get; set; }
        [Required]
        public string adresse { get; set; }
        
        [Required] 
        public string  Numero { get; set; }
 
        public virtual ICollection<Formation>? Formations { get; set; }


    }
}
