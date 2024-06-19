using System.ComponentModel.DataAnnotations;

namespace InventoryIT.Model
{
    public class Departament
    {
        [Key]
        public int DepartamentID { get; set; }
        public string Name { get; set; }    
        public ICollection<Employee> Employees { get; set; }


    }
}
