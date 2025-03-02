using System.ComponentModel.DataAnnotations;

public enum UserType
{
    [Display(Name = "Pet Owner")]
    PetOwner,
    [Display(Name = "Vet")]
    Vet,
    [Display(Name = "Admin")]
    Admin
}
