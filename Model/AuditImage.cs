namespace InventoryIT.Model
{
    public class AuditImage
    {
        public int AuditImageId { get; set; }
        public string FileName { get; set; }
        public DateTime UploadDate { get; set; }
        public string ContentType { get; set; }
        public string PathFile { get; set; }

        // Relación con la auditoría
        public int AuditId { get; set; }
        public Audit Audit { get; set; }

    }
}
