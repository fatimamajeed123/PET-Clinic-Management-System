using Microsoft.VisualStudio.TestTools.UnitTesting;
using OpenQA.Selenium;
using OpenQA.Selenium.Edge;
using System;
using System.Collections.Generic;

namespace PetManagementSystem.Tests
{
    [TestClass]
    public class AppointmentViewTests
    {
        private EdgeDriver _driver;

        [TestInitialize]
        public void EdgeDriverInitialize()
        {
            // Initialize Edge driver
            var options = new EdgeOptions
            {
                PageLoadStrategy = PageLoadStrategy.Normal
            };
            _driver = new EdgeDriver(options);
        }

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

            // Simulate a webpage
            string htmlPage = $"<html><body>{html}</body></html>";
            string filePath = System.IO.Path.GetFullPath("AppointmentsView.html");
            System.IO.File.WriteAllText(filePath, htmlPage);

            // Act
            _driver.Url = "file://" + filePath;
            var rows = _driver.FindElements(By.CssSelector("table.table tbody tr"));

            // Assert
            Assert.AreEqual(2, rows.Count);

            // Verify first appointment
            Assert.IsTrue(rows[0].Text.Contains("Buddy"));
            Assert.IsTrue(rows[0].Text.Contains("10/01/2025"));
            Assert.IsTrue(rows[0].Text.Contains("Approved"));
            Assert.IsTrue(rows[0].FindElement(By.CssSelector("a.btn-info")).GetAttribute("href").Contains("appointmentId=1"));

            // Verify second appointment
            Assert.IsTrue(rows[1].Text.Contains("Max"));
            Assert.IsTrue(rows[1].Text.Contains("15/01/2025"));
            Assert.IsTrue(rows[1].Text.Contains("Pending"));
            Assert.IsTrue(rows[1].FindElement(By.CssSelector("a.btn-info")).GetAttribute("href").Contains("appointmentId=2"));
        }

        [TestCleanup]
        public void EdgeDriverCleanup()
        {
            _driver.Quit();
        }

        // Mock models to simulate data
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
}