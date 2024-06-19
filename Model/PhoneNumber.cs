namespace InventoryIT.Model
{
    public class PhoneNumber
    {
        public int PhoneNumberId { get; set; }
        public int Number { get; set; }
        public string Operator { get; set; }
        public string Type { get; set; }
        public int IsActive { get; set; }
        public ICollection<Phone_Number_User_Model>?  Phone_Number_User { get; set; }
    }
}
