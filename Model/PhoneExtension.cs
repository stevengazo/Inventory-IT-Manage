using System.ComponentModel.DataAnnotations;

namespace InventoryIT.Model
{
    public class PhoneExtension
    {
        [Key]
        public int PhoneExtensionId { get; set; }
        public string? PhoneExtensionName { get; set;}
        public int Extension {  get; set;}
        public string Type { get; set;} 
        public string Description { get; set;}
        public string Forwardings { get; set;}
        public string? UserNumber {  get; set;}
        public string? Password { get; set;}
        public int PhoneNumberPBX { get; set;}
        public int? EmployeeId { get; set; }
        public Employee? Employee { get; set; }

    }
}
