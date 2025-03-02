using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using PetManagementSystem.Models;
using System.Linq;
using System.Security.Claims;

namespace PetManagementSystem.Controllers
{
    public class PetsController : Controller
    {
        private readonly ApplicationDbContext _context;

        public PetsController(ApplicationDbContext context)
        {
            _context = context;
        }
        //GetPets method retrieves a list of all pets from the database and includes related user information using eager loading (Include). It then returns a view containing the list of pets.

        public IActionResult GetPets()
        {
            var pets = _context.Pets.Include(p => p.User).ToList();
            return View(pets);
        }

        public IActionResult Index()
        {
            var userId = HttpContext.Session.GetInt32("UserID");
            if (userId.HasValue)
            {
                var pets = _context.Pets.Where(p => p.UserId == userId).ToList();
                var isPetOwner = _context.Users.Any(u => u.UserID == userId && u.UserType == "Pet Owner");

                ViewBag.IsPetOwner = isPetOwner;

                return View(pets);
            }

            // If user ID is not available in the session, handle the error condition or redirect
            return RedirectToAction("Login", "Users");
        }

        public IActionResult PetOwnersList()
        {
            var petOwners = _context.Users.Where(u => u.UserType == "Pet Owner").ToList();
            return View(petOwners);
        }


        public IActionResult Create()
        {
            return View();
        }


        //Create method with [HttpPost] attribute is called when the form for creating a new pet is submitted. It retrieves the logged-in user's ID from the session, sets the UserId property of the pet, adds the pet to the database, and redirects to the Index action. If the user ID is not available, it displays an error message.
        [HttpPost]
        public IActionResult Create(Pet pet)
        {
            // Retrieve the logged-in user's ID from the session
            var userId = HttpContext.Session.GetInt32("UserID");

            // Validate and process the form submission
            if (userId.HasValue)
            {
                // Set the UserId property of the pet
                pet.UserId = userId.Value;

                // Add the pet to the database
                _context.Pets.Add(pet);
                _context.SaveChanges();

                return RedirectToAction("Index");
            }

            // If user ID is not available in the session, handle the error condition
            ViewBag.ErrorMessage = "User ID is not available.";
            return View(pet);
        }
    }

}
