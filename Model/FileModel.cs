using System.ComponentModel.DataAnnotations;

namespace InventoryIT.Model
{
    public class FileModel
    {
        [Key]
        public int Id { get; set; }
        public string FileName { get; set; }
        public long FileSize { get; set; }
        public DateTime CreationDate { get; set; }
        public string ContentType { get; set; }
        public byte[] Content { get; set; }

        public int? ComputerModelID { get; set; }
        public ComputerModel? ComputerModel { get; set; }

    }
}
