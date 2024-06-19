using System.ComponentModel.DataAnnotations;

namespace InventoryIT.Model
{
    public class Brand
    {
        [Key]
        public int BrandId { get; set; } 
        public string Name { get; set; }
        public ICollection<ComputerModel>? Computers { get; set; }
        public ICollection<SmartPhoneModel>? SmartPhones { get; set; }  
    }
}
