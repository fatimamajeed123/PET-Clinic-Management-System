using PetManagementSystem.Models;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace PetManagementSystem.ViewModels
{
    public class CreateAppointmentViewModel
    {
        [Required]
        [Display(Name = "Appointment Date")]
        public DateTime AppointmentDate { get; set; }

        [Required]
        [Display(Name = "Pet")]
        public int PetId { get; set; }


        [Required]
        [Display(Name = "User")]
        public int UserID { get; set; } // Add this property

        // Additional properties as needed

        // Properties to hold dropdown options
        public List<Pet> Pets { get; set; }
        public Pet Pet { get; set; }
        public List<Users> Vets { get; set; }
        public List<Users> Users { get; set; } // Add this property
    }
}
