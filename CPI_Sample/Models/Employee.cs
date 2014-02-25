using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Data;
using System.Data.Entity;

namespace CPI_Sample.Models
{
    public class Employee
    {
        [DatabaseGenerated(DatabaseGeneratedOption.None)]
        public int employeeId { get; set; }
        [StringLength(3), Required]
        public string employeeInitials { get; set; }
        [StringLength(50), Required]
        public string employeePhone { get; set; }
    }

    public class CPIDbContext : DbContext
    {
        public DbSet<Employee> Employees { get; set; }
        public DbSet<Customer> Customers { get; set; }
    }
}