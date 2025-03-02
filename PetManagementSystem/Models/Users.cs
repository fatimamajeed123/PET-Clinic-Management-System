using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace PetManagementSystem.Models
{
    public class Users
    {
        [Key]
        public int UserID { get; set; }
        public string UserType { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string Email { get; set; }
        public string PhoneNo { get; set; }
        public string Password { get; set; }

        public virtual ICollection<Appointment> Appointments { get; set; }
        public virtual ICollection<Appointment> VetAppointments { get; set; }
    }
}
