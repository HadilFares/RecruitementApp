using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace RecruitementManagementApp.Models
{
    public class Employee
    {
        [Key]
        public int IdEmployee { get; set; }

        [Required(ErrorMessage = "Please enter the Date of Birth.")]
        [Display(Name = "Date of Birth")]
        [DataType(DataType.Date)]
        public DateTime? DateNaiss { get; set; }


        [Required]
        public string Email { get; set; }

        
        public ICollection<EmployeeFormation> employeeFormations { get; set; }


       
        

    }

}
