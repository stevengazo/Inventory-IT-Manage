namespace InventoryIT.Model
{
    public class Audit
    {
        public int AuditId { get; set; }
        public DateTime AuditDate { get; set; } = DateTime.Now;
        public string AuditorName { get; set; }
        public string Comments { get; set; }
        public string GeneralCondition { get; set; } // Ejemplo: Bueno, Regular, Malo
        public bool IsClean { get; set; }
        public bool IsOperational { get; set; }
        public string MissingItems { get; set; } // Ejemplo: Cargador, Cable HDMI, etc.
        public string RecommendedActions { get; set; }

        // Relaciones opcionales según dispositivo auditado
        public int? ComputerModelID { get; set; }
        public ComputerModel Computer { get; set; }

        public int? SmartPhoneModelId { get; set; }
        public SmartPhoneModel SmartPhone { get; set; }

        public int? PeripheralModelId { get; set; }
        public PeripheralModel Peripheral { get; set; }

        // Colección de imágenes relacionadas con la auditoría
        public ICollection<AuditImage> Images { get; set; } = new List<AuditImage>();
    }
}
