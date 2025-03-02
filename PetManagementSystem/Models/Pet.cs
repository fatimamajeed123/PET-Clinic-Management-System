using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace PetManagementSystem.Models
{
    public class Pet
    {
        public int Id { get; set; }

        [Required]
        public string Name { get; set; }

        [Required]
        public int Age { get; set; }

        [Required]
        public string Species { get; set; }

        [Required]
        public string Gender { get; set; }

        [Required]
        public string Breed { get; set; }

        public int UserId { get; set; }

        public virtual Users User { get; set; }
        public virtual ICollection<Appointment> Appointments { get; set; }
    }
}
