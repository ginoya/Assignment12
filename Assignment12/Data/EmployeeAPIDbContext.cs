using Assignment12.Models;
using Microsoft.EntityFrameworkCore;

namespace Assignment12.Data
{
    public class EmployeeAPIDbContext: DbContext  
    {
        public EmployeeAPIDbContext(DbContextOptions options):base(options) {
        }

        public DbSet<Employee> Employee {  get; set; }
    }
}
