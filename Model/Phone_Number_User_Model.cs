using System.ComponentModel.DataAnnotations;

namespace InventoryIT.Model
{
    public class Phone_Number_User_Model
    {

        [Key]
        public int Id { get; set; }
        public int PhoneNumberId { get; set; }
        public DateTime CreationDate { get; set; }
        public PhoneNumber? PhoneNumber { get; set; }
        public int? SmartPhoneModelId { get; set; }
        public SmartPhoneModel? PhoneNumberModel { get; set; }
        public int? EmployeeId { get; set; }
        public Employee? Employee { get; set; }
        public bool IsActive { get; set; }
        public DateTime LastModification { get; set; }

    }
}
