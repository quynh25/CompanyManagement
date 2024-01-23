using CompanyManagement.Models;
using Microsoft.EntityFrameworkCore;

namespace CompanyManagement.Data
{
    public class DataContext : DbContext
    {
        public DataContext(DbContextOptions<DataContext> options) : base(options) { }
        public DbSet<Center> Centers { get; set; }
        public DbSet<Company> Companys { get; set; }
        public DbSet<Derpartment> Derpartments { get; set; }
        public DbSet<Employee>Employees { get; set; }
        public DbSet<EmployeeProject> EmployeesProjects { get; set; }
        public DbSet<Project> Projects { get; set; }
       

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<EmployeeProject>()
                .HasKey(ep => new { ep.EmployeeId, ep.ProjectId });
            modelBuilder.Entity<EmployeeProject>()
                .HasOne(e => e.Employee)
                .WithMany(ep => ep.EmployeeProjects)
                .HasForeignKey(e => e.EmployeeId)
                .OnDelete(DeleteBehavior.Cascade);
            modelBuilder.Entity<EmployeeProject>()
                .HasOne(p => p.Project)
                .WithMany(ep => ep.EmployeeProjects)
                .HasForeignKey(p => p.ProjectId)
                .OnDelete(DeleteBehavior.NoAction);

        }
    }
}
