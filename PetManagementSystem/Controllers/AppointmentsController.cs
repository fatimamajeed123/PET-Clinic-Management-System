using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using PetManagementSystem.Models;
using PetManagementSystem.ViewModels;

namespace PetManagementSystem.Controllers
{
    public class AppointmentsController : Controller
    {
        //presumably provides access to the database context for interacting with the application's data.
        private readonly ApplicationDbContext _context;

        public AppointmentsController(ApplicationDbContext context)
        {
            _context = context;
        }
        //indicating that they are intended to render forms or display data to users, and they should be accessed via HTTP GET requests.
        [HttpGet]
        // GET: Display the form for creating a prescription
        public IActionResult OpenApp()
        {
            return RedirectToAction("Create");
        }

        // GET: Create Appointment
        [HttpGet]
        public IActionResult Create()
        {
            var userId = HttpContext.Session.GetInt32("UserID");

            if (userId == null)
            {
                return RedirectToAction("Login", "Account");
            }

            var viewModel = new CreateAppointmentViewModel
            {
                Pets = _context.Pets.Where(p => p.UserId == userId).ToList(), // Filter pets by logged-in user
                Vets = _context.Users.Where(u => u.UserType == "Vet").ToList() // Filter users with role "Vet"
            };

            return View(viewModel);
        }

        [HttpPost]
        public IActionResult Create(CreateAppointmentViewModel viewModel)
        {
            var userId = HttpContext.Session.GetInt32("UserID");

            if (userId == null)
            {
                return RedirectToAction("Login", "Account");
            }

            if (true)
            {
                var appointment = new Appointment
                {
                    AppointmentDate = viewModel.AppointmentDate,
                    UserId = Convert.ToInt32(userId),
                    PetId = viewModel.PetId,
                    VetId = viewModel.UserID,
                    IsApproved = false
                };

                _context.Appointments.Add(appointment);
                _context.SaveChanges();
                return RedirectToAction("Index", "Pets");
            }

            viewModel.Pets = _context.Pets.Where(p => p.UserId == userId).ToList(); // Ensure Pets list is repopulated
            viewModel.Vets = _context.Users.Where(u => u.UserType == "Vet").ToList(); // Ensure Vets list is repopulated
            return View(viewModel);
        }
        // GET: Approved Appointments
        public IActionResult ApprovedAppointments()
        {
            var approvedAppointments = _context.Appointments
                .Where(a => a.IsApproved)
                .Include(a => a.Pet)
                .Include(a => a.User)
                .ToList();

            return View(approvedAppointments);
        }

        // GET: My Appointments
        public IActionResult MyAppointments()
        {
            var userId = HttpContext.Session.GetInt32("UserID");
            var myAppointments = _context.Appointments
                .Where(a => a.UserId == userId)
                .Include(a => a.Pet)
                .Include(a => a.Vet)
                .ToList();

            return View(myAppointments);
        }

        [HttpGet]
        public IActionResult VetDashboard()
        {
            var vetId = HttpContext.Session.GetInt32("UserID");

            if (vetId == null)
            {
                return RedirectToAction("Login", "Account");
            }

            var appointments = _context.Appointments
                                       .Where(a => a.VetId == vetId) // Filter by logged-in vet
                                       .Include(a => a.User)
                                       .Include(a => a.Pet)
                                       .ToList();
            return View(appointments);
        }

        [HttpPost]
        public IActionResult Approve(int id)
        {
            var appointment = _context.Appointments.Find(id);
            if (appointment != null)
            {
                appointment.IsApproved = true;
                _context.SaveChanges();
            }
            return RedirectToAction("VetDashboard");
        }

        [HttpPost]
        public IActionResult Disapprove(int id)
        {
            var appointment = _context.Appointments.Find(id);
            if (appointment != null)
            {
                _context.Appointments.Remove(appointment);
                _context.SaveChanges();
            }
            return RedirectToAction("VetDashboard");
        }
    }
}
