using System.ComponentModel.DataAnnotations;

namespace InventoryIT.Model
{
    public class HistoryModel
    {
        [Key]
        public int HistoryModelID {  get; set; }
        public string Description { get; set; }
        public DateTime CreationDate { get; set; }
        public int? ComputerModelID { get; set; }
        public ComputerModel? ComputerModel { get; set; }
        public int? SmartPhoneModelId { get; set; }
        public SmartPhoneModel? SmartPhoneModel { get; set; }

    }
}
