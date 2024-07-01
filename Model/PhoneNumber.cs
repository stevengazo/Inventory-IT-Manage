using System.ComponentModel.DataAnnotations;

namespace InventoryIT.Model
{
    public class PhoneNumber
    {
        [Key]
        public int PhoneNumberId { get; set; }
        public int Number { get; set; }
        public DateTime StartDate { get; set; }
        public string Operator { get; set; }
        public string Type { get; set; }
        public string Description { get; set; }
        public float Cost { get; set; }
        public DateTime ExpireDate { get; set; }
        public bool IsActive { get; set; }
        public ICollection<Phone_Number_User_Model>? Phone_Number_User { get; set; }
    }
}
