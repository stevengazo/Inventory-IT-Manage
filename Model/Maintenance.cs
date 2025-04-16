namespace InventoryIT.Model
{
    public class Maintenance
    {
        public int MaintenanceId { get; set; }
        public DateTime MaintenanceDate { get; set; }
        public string TechnicianName { get; set; }
        public string MaintenanceType { get; set; } // Ejemplo: Preventivo, Correctivo, Predictivo
        public string Description { get; set; }
        public decimal Cost { get; set; }
        public string Observations { get; set; }

        // Relaciones opcionales según dispositivo mantenido
        public int? ComputerModelID { get; set; }
        public ComputerModel Computer { get; set; }

        public int? SmartPhoneModelId { get; set; }
        public SmartPhoneModel SmartPhone { get; set; }

        public int? PeripheralModelId { get; set; }
        public PeripheralModel Peripheral { get; set; }

        // Fotos relacionadas al mantenimiento
        public ICollection<MaintenanceImage> Images { get; set; } = new List<MaintenanceImage>();
    }
}
