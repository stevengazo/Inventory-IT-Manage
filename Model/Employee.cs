namespace InventoryIT.Model
{
    public class Employee
    {
        public int EmployeeId { get; set; }
        public string Name { get; set; }
        public string LastName { get; set; }
        public string SecondLastName { get; set; }
        public DateTime Birthday { get; set; }
        public ICollection<Phone_Number_User_Model> phone_Number_User_Models {  get; set; } 
        public ICollection<ComputerModel> computer_Models { get; set;}

        
    }
}
