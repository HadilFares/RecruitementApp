using System.ComponentModel.DataAnnotations;

namespace RecruitementManagementApp.Models
{
    public class Formation
    {
        [Key]
        public int IdFormation { get; set; }
        [Required]
        public string Title { get; set; }
        [Required]
        public string Description { get; set; }
        public bool published { get; set; }=false;
        public bool archived { get; set; } = false;
        public ICollection<EmployeeFormation> employeeFormations { get; set; }
        public int IdRh { get; set; }
        public virtual Rh? LeRh { get; set; }
    }
}
