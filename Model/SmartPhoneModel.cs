using System.ComponentModel.DataAnnotations;

namespace InventoryIT.Model
{
    public class SmartPhoneModel
    {
        [Key]
        public int SmartPhoneModelId { get; set; }
        public string PhoneNumber { get; set; }
        public DateTime AdquisitionDate { get; set; }
        public string PhoneModel {  get; set; } 
        public string PhoneSerial { get; set; }
        public string Cost { get; set; }    
        public string Description { get; set; }
        public string Status { get; set; }  
        public bool isActive { get; set; }
        public int BrandId { get; set; }
        public Brand Brand { get; set; }
        public ICollection<HistoryModel>? History { get; set; }  
        public ICollection<Phone_Number_User_Model>? Phone_Number_User_s { get; set; }

    }
}
