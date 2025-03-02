using Microsoft.AspNetCore.Mvc;
using PetManagementSystem.Models;
using System.Linq;

namespace PetManagementSystem.Controllers
{
    public class UsersController : Controller
    {
        private readonly ApplicationDbContext _context;

        public UsersController(ApplicationDbContext context)
        {
            _context = context;
        }

        [HttpGet]
        public IActionResult Login()
        {
            return View();
        }

        [HttpPost]
        public IActionResult Login(string email, string password)
        {
            var user = _context.Users.SingleOrDefault(u => u.Email == email && u.Password == password);
            if (user != null)
            {
                // Set user ID in the session
                HttpContext.Session.SetInt32("UserID", user.UserID);

                if (user.UserType == "Pet Owner")
                {
                    // Redirect to the Create action of the PetsController
                    return RedirectToAction("Index", "Pets");
                }
                else if (user.UserType == "Admin")
                {
                    return RedirectToAction("AdminDashboard");
                }
                else if (user.UserType == "Vet")
                {
                    return RedirectToAction("VetDashboard", "Appointments");
                }
                // Redirect to appropriate page based on user type
            }

            ViewBag.ErrorMessage = "Invalid login attempt.";
            return View();
        }

        public IActionResult Logout()
        {
            return RedirectToAction("Login", "Users");
        }



        public IActionResult RegisterPetOwner()
        {
            return View();
        }

        [HttpPost]
        public IActionResult RegisterPetOwner(Users user)
        {
            if (true) // Ensure model state is valid before proceeding
            {
                user.UserType = "Pet Owner";
                _context.Users.Add(user);
                _context.SaveChanges();
                return RedirectToAction("Login");
            }

            // If model state is not valid, return to the register view with the submitted user
            return View(user);
        }



        [HttpGet]
        public IActionResult RegisterVet()
        {
            return View(new Users { UserType = "Vet" });
        }

        [HttpPost]
        public IActionResult RegisterVet(Users user)
        {
            if (true)
            {
                user.UserType = "Vet";
                _context.Users.Add(user);
                _context.SaveChanges();
                return RedirectToAction("Index", "Home");
            }
            return View(user);
        }

        // Admin Actions
        public IActionResult AdminDashboard()
        {
            return View();
        }

        public IActionResult PetOwnersList()
        {
            var petOwners = _context.Users.Where(u => u.UserType == "Pet Owner").ToList();
            return View(petOwners);
        }

        public IActionResult VetsList()
        {
            var vets = _context.Users.Where(u => u.UserType == "Vet").ToList();
            return View(vets);
        }

        // GET: Edit Pet Owner
        [HttpGet]
        public IActionResult EditPetOwner(int id)
        {
            var petOwner = _context.Users.FirstOrDefault(u => u.UserID == id);
            if (petOwner == null)
            {
                return NotFound();
            }

            return View(petOwner);
        }

        // POST: Edit Pet Owner
        [HttpPost]
        public IActionResult EditPetOwner(Users petOwner)
        {
            if (true)
            {
                var existingPetOwner = _context.Users.Find(petOwner.UserID);

                if (existingPetOwner != null)
                {
                    petOwner.UserType = "Pet Owner";
                    existingPetOwner.FirstName = petOwner.FirstName;
                    existingPetOwner.LastName = petOwner.LastName;
                    existingPetOwner.Email = petOwner.Email;
                    existingPetOwner.PhoneNo = petOwner.PhoneNo;
                    existingPetOwner.Password = petOwner.Password;

                    // Update the pet owner in the database
                    _context.Users.Update(existingPetOwner);
                    _context.SaveChanges();

                    return RedirectToAction("AdminDashboard"); // Redirect to the list of pet owners
                }
                else
                {
                    ModelState.AddModelError("", "Pet owner not found."); // Add error message if the pet owner is not found
                }
            }

            // If ModelState is not valid, return the edit view with validation errors
            return RedirectToAction("AdminDashboard");
        }


        [HttpGet]
        public IActionResult EditVet(int id)
        {
            var vet = _context.Users.FirstOrDefault(u => u.UserID == id);
            if (vet == null)
            {
                return NotFound();
            }

            return View(vet); // Pass the model to the view
        }


        [HttpPost]
        public IActionResult EditVet(Users vet)
        {
            if (true)
            {
                var existingVet = _context.Users.Find(vet.UserID);

                if (existingVet != null)
                {
                    existingVet.FirstName = vet.FirstName;
                    existingVet.LastName = vet.LastName;
                    existingVet.Email = vet.Email;
                    existingVet.PhoneNo = vet.PhoneNo;
                    existingVet.Password = vet.Password;
                    existingVet.UserType = "Vet";

                    // Update the vet in the database
                    _context.Users.Update(existingVet);
                    _context.SaveChanges();

                    return RedirectToAction("AdminDashboard"); // Redirect to the admin dashboard
                }
                else
                {
                    return NotFound(); // Return 404 if the vet is not found
                }
            }

            // If ModelState is not valid, return the edit view with validation errors
            return View(vet);
        }


        public IActionResult DeleteUser(int id)
        {
            var user = _context.Users.Find(id);
            if (user != null)
            {
                _context.Users.Remove(user);
                _context.SaveChanges();
            }
            return RedirectToAction(user.UserType == "PetOwner" ? "PetOwnersList" : "VetsList");
        }
    }
}
