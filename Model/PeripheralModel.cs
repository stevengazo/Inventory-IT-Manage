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
        public float Cost { get; set; }
        public bool IsActive { get; set; }
        public string Description { get; set; }

        public int? BrandId { get; set; }
        public Brand? Brand { get; set; }

        public int? EmployeeId { get; set; }
        public Employee? Employee { get; set; }
        public ICollection<HistoryModel>? History { get; set; }
        public ICollection<FileModel>? Files { get; set; }
    }
}
