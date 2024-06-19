using System.ComponentModel.DataAnnotations;

namespace InventoryIT.Model
{
    public class PeripheralModel
    {
        [Key]
        public int PeripheralModelId { get; set; }
        public string Name { get; set; }
        public string Type { get; set; }
        public string SerialNumber { get; set; }    
        public string Model {  get; set; }  
        public DateTime AdquisitionDate { get; set; }

    }
}
