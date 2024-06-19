namespace InventoryIT.Model
{
    public class Phone_Number_User_Model
    {
       
        public int PhoneNumberId { get; set; }
        public PhoneNumber PhoneNumber { get; set; }
        public int SmartPhoneModelId { get; set; }
        public SmartPhoneModel PhoneNumberModel { get; set; }
        public int EmployeeId { get; set; }
        public Employee Employee { get; set; }  
    }
}
