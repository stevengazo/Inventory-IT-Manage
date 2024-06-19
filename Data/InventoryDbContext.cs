using InventoryIT.Model;
using Microsoft.EntityFrameworkCore;

namespace InventoryIT.Data
{
    public class InventoryDbContext : DbContext
    {
        public DbSet<Brand> Brand { get; set; }
        public DbSet<ComputerModel> Computer { get; set; }
        public DbSet<Departament> Departament { get; set; }
        public DbSet<Employee> Employee { get; set; }
        public DbSet<HistoryModel> History { get; set; }
        public DbSet<Phone_Number_User_Model> Phone_Number_User { get; set; }
        public DbSet<PhoneNumber> PhoneNumber { get; set; }
        
        public DbSet<SmartPhoneModel> SmartPhone { get; set; }

        public InventoryDbContext(DbContextOptions<InventoryDbContext> options) : base(options) 
        {
           
        }
    }
}
