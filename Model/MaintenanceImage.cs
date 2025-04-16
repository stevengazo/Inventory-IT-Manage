namespace InventoryIT.Model
{
    public class MaintenanceImage
    {
        public int MaintenanceImageId { get; set; }
        public string FileName { get; set; }
        public DateTime UploadDate { get; set; }
        public string ContentType { get; set; }
        public string FilePath { get; set; }

        // Relación con el mantenimiento
        public int MaintenanceId { get; set; }
        public Maintenance Maintenance { get; set; }
    }
}
