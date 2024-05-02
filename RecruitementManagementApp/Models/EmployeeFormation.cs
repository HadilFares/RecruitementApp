using System.ComponentModel.DataAnnotations;

namespace RecruitementManagementApp.Models
{

    public enum ApplicationStatus
    {
        EnAttente,   // Pending
        Acceptees,   // Accepted
        Rejetees     // Rejected
    }
    public class EmployeeFormation
    {
        [Key]
        public int IdEmployee { get; set; }
        public int IdFormation { get; set; }
        public ApplicationStatus Status { get; set; }
        public Employee Employee { get; set; }
        public Formation Formation { get; set; }
    }
}