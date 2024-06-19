namespace InventoryIT.Model
{
    public class Brand
    {
        public int BrandId { get; set; } 
        public string Name { get; set; }
        public ICollection<ComputerModel>? Computers { get; set; }
        public ICollection<SmartPhoneModel>? SmartPhones { get; set; }  
    }
}
