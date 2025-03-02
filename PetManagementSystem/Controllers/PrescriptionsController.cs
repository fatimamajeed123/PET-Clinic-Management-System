using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using PetManagementSystem.Models;
using System.Linq;
using Microsoft.AspNetCore.Http;

namespace PetManagementSystem.Controllers
{
    public class PrescriptionsController : Controller
    {
        private readonly ApplicationDbContext _context;

        public PrescriptionsController(ApplicationDbContext context)
        {
            _context = context;
        }

        // GET: Display the form for creating a prescription
        public IActionResult Create(int appointmentId)
        {
            var userId = HttpContext.Session.GetInt32("UserID");
            var appointment = _context.Appointments
                .Include(a => a.Pet)
                .Include(a => a.Vet)
                .Include(a => a.User)
                .FirstOrDefault(a => a.AppointmentId == appointmentId);

            if (appointment == null)
            {
                return NotFound();
            }

            var prescription = new Prescription { AppointmentId = appointmentId };
            return View(prescription);
        }

        // POST: Process the form submission for creating a prescription
        [HttpPost]
        public IActionResult Create(Prescription prescription)
        {
            if (true)
            {
                _context.Prescriptions.Add(prescription);
                _context.SaveChanges();

                return RedirectToAction("VetDashboard", "Appointments");
            }

            return View(prescription);
        }

        // GET: Display prescriptions for the logged-in pet owner
        public IActionResult Index()
        {
            var userId = HttpContext.Session.GetInt32("UserID");
            if (userId == null)
            {
                // Redirect to login page if user is not logged in
                return RedirectToAction("Login", "Users");
            }

            var prescriptions = _context.Prescriptions
                .Include(p => p.Appointment)
                .ThenInclude(a => a.Pet)
                .Where(p => p.Appointment.UserId == userId)
                .ToList();

            return View(prescriptions);
        }
        public IActionResult GetApp()
        {
            var userId = HttpContext.Session.GetInt32("UserID");
            if (userId == null)
            {
                // Redirect to login page if user is not logged in
                return RedirectToAction("Login", "Users");
            }

            var prescriptions = _context.Prescriptions
                .Include(p => p.Appointment)
                .ThenInclude(a => a.Pet)
                .OrderByDescending(p => p.PrescriptionID) // Assuming you want the most recent entries
                .Take(2)
                .ToList();

            return View("Index", prescriptions); // Ensure the view name is correct
        }



    }
}
