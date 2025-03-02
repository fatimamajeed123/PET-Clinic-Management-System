using System.ComponentModel.DataAnnotations;

namespace PetManagementSystem.Models
{
    public class Prescription
    {
        [Key]
        public int PrescriptionID { get; set; }
        public int AppointmentId { get; set; }      
        public string MedicationName { get; set; }
        public string Dosage { get; set; }
        public string Instructions { get; set; }

        public virtual Appointment Appointment { get; set; }
    }
}
