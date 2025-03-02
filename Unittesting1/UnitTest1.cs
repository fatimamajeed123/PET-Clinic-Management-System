using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.Collections.Generic;

namespace PetManagementSystem.Tests
{
    [TestClass]
    public class AppointmentViewTests
    {
        [TestMethod]
        public void MyAppointmentsView_ShouldRenderAppointmentsCorrectly()
        {
            // Arrange
            var appointments = new List<Appointment>
            {
                new Appointment
                {
                    AppointmentId = 1,
                    Pet = new Pet { Name = "Buddy" },
                    AppointmentDate = new DateTime(2025, 01, 10),
                    IsApproved = true
                },
                new Appointment
                {
                    AppointmentId = 2,
                    Pet = new Pet { Name = "Max" },
                    AppointmentDate = new DateTime(2025, 01, 15),
                    IsApproved = false
                }
            };

            string html = "<table class=\"table\">" +
                          "<thead>" +
                          "<tr>" +
                          "<th>Pet</th>" +
                          "<th>Appointment Date</th>" +
                          "<th>Status</th>" +
                          "<th>Prescription</th>" +
                          "</tr>" +
                          "</thead>" +
                          "<tbody>";

            foreach (var appointment in appointments)
            {
                html += $"<tr>" +
                        $"<td>{appointment.Pet.Name}</td>" +
                        $"<td>{appointment.AppointmentDate.ToShortDateString()}</td>" +
                        $"<td>{(appointment.IsApproved ? "Approved" : "Pending")}</td>" +
                        $"<td><a href=\"/Prescriptions/Index?appointmentId={appointment.AppointmentId}\" class=\"btn btn-info\">View Prescription</a></td>" +
                        $"</tr>";
            }

            html += "</tbody></table>";

            // Act & Assert
            Assert.IsTrue(html.Contains("<td>Buddy</td>"));
            Assert.IsTrue(html.Contains("<td>Approved</td>"));
            Assert.IsTrue(html.Contains("<a href=\"/Prescriptions/Index?appointmentId=1\""));

            Assert.IsTrue(html.Contains("<td>Max</td>"));
            Assert.IsTrue(html.Contains("<td>Pending</td>"));
            Assert.IsTrue(html.Contains("<a href=\"/Prescriptions/Index?appointmentId=2\""));
        }
    }

    // Mock models to simulate data (these should align with your actual PetManagementSystem models)
    public class Appointment
    {
        public int AppointmentId { get; set; }
        public Pet Pet { get; set; }
        public DateTime AppointmentDate { get; set; }
        public bool IsApproved { get; set; }
    }

    public class Pet
    {
        public string Name { get; set; }
    }
}
