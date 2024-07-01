using System.ComponentModel.DataAnnotations;

namespace InventoryIT.Model
{
    public class Employee
    {
        [Key]
        public int EmployeeId { get; set; }
        public int DNI { get; set; }
        public string Name { get; set; }
        public string LastName { get; set; }
        public string SecondLastName { get; set; }
        public string Email { get; set; }
        public int PhoneNumber { get; set; }
        public bool Fired { get; set; }
        public DateTime Birthday { get; set; }
        public ICollection<Phone_Number_User_Model>? phone_Number_User_Models { get; set; }
        public ICollection<ComputerModel>? computer_Models { get; set; }
        public ICollection<PhoneExtension>? phoneExtensions { get; set; }
        public ICollection<PeripheralModel>? peripherals { get; set; }

        public Departament? Departament { get; set; }
        public int? DepartamentID { get; set; }

    }
}
