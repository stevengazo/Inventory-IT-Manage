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
        public DbSet<PeripheralModel> Peripheral { get; set; }
        public DbSet<SmartPhoneModel> SmartPhone { get; set; }
        public DbSet<PhoneExtension> PhoneExtension { get; set; }
        public InventoryDbContext(DbContextOptions<InventoryDbContext> options) : base(options) 
        {
           
        }
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            List<Departament> departaments = new List<Departament>()
            {
                new()
                {
                    DepartamentID = 1,
                    Name= "Gerencia",
                },
                new()
                {
                    DepartamentID = 2,
                    Name= "Administracion",
                },
                new()
                {
                    DepartamentID = 3,
                    Name= "Financiero",
                },
                new()
                {
                    DepartamentID = 4,
                    Name= "Proyectos",
                },
                new()
                {
                    DepartamentID = 5,
                    Name= "Operativo",
                },
                new()
                {
                    DepartamentID = 6,
                    Name= "Arquitectura",
                },
                new()
                {
                    DepartamentID = 7,
                    Name= "Ventas",
                },
                new()
                {
                    DepartamentID = 8,
                    Name= "IT",
                }
            };
            modelBuilder.Entity<Departament>().HasData(departaments);
            List<Brand> brands = new List<Brand>()
            {
                new()
                {
                    BrandId = 1,
                    Name="Dell"
                },
                 new()
                {
                    BrandId = 2,
                    Name="Lenovo"
                },
                  new()
                {
                    BrandId = 3,
                    Name="HP"
                },
                   new()
                {
                    BrandId = 4,
                    Name="Epson"
                },
                    new()
                {
                    BrandId = 5,
                    Name="Ubiquiti"
                },
                     new()
                {
                    BrandId = 7,
                    Name="Cisco"
                },
                      new()
                {
                    BrandId = 8,
                    Name="Samsung"
                },
                       new()
                {
                    BrandId = 9,
                    Name="Xiaomi"
                },
                        new()
                {
                    BrandId = 10,
                    Name="Apple"
                },
                         new()
                {
                    BrandId = 11,
                    Name="Huawei"
                },
            };
            modelBuilder.Entity<Brand>().HasData(brands);

        }
    }
}
