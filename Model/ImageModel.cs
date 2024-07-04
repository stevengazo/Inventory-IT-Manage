using System.ComponentModel.DataAnnotations;

namespace InventoryIT.Model
{
    public class ImageModel
    {
        [Key]
        public int ImageId { get; set; }
        public string FileName { get; set; }
        public long FileSize { get; set; }
        public DateTime CreationDate { get; set; }
        public string ContentType { get; set; }
        public byte[] Content { get; set; }

        public int? ComputerModelID { get; set; }
        public ComputerModel? ComputerModel { get; set; }

        public int? SmartPhoneModelId { get; set; }
        public SmartPhoneModel? SmartPhoneModel { get; set; }

        public int? PeripheralModelId { get; set; }
        public PeripheralModel? PeripheralModel { get; set; }

    }
}
