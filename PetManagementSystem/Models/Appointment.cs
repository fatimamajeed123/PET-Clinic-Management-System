using System;
using System.ComponentModel.DataAnnotations;

namespace PetManagementSystem.Models
{
    public class Appointment
    {
        public int AppointmentId { get; set; }
        public DateTime AppointmentDate { get; set; }
        public int UserId { get; set; }
        public int PetId { get; set; }
        public int VetId { get; set; }
        public bool IsApproved { get; set; }

        public virtual Users User { get; set; }
        public virtual Pet Pet { get; set; }
        public virtual Users Vet { get; set; }


        public bool IsFutureAppointment()
        {
            return AppointmentDate > DateTime.Now;
        }

    }
}
