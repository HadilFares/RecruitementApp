using Microsoft.EntityFrameworkCore;
using System.Reflection.Metadata;

namespace RecruitementManagementApp.Models
{
    public class RhmanagementDbContext : DbContext
    {

        public RhmanagementDbContext(DbContextOptions options) : base(options)
        { }
        public virtual DbSet<Employee> Employees { get; set; }
        public virtual DbSet<Admin> Admins { get; set; }
        public virtual DbSet<Rh> RHs { get; set; }
        public virtual DbSet<User> Users { get; set; }
        public virtual DbSet<Formation> Formations { get; set; }
        public DbSet<EmployeeFormation> employeeFormation { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            

            
        }



    }
}
