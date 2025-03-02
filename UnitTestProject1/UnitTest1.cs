using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.Collections.Generic;

namespace UnitTestProject1
{
    [TestClass]
    public class UnitTest1
    {
        [TestMethod]
        public void TestMethod1_ShouldRenderHtmlCorrectly()
        {
            // Arrange
            var testItems = new List<TestItem>
            {
                new TestItem
                {
                    ItemId = 1,
                    Name = "Item1",
                    Date = new DateTime(2025, 01, 10),
                    IsAvailable = true
                },
                new TestItem
                {
                    ItemId = 2,
                    Name = "Item2",
                    Date = new DateTime(2025, 01, 15),
                    IsAvailable = false
                }
            };

            string html = "<table class=\"test-table\">" +
                          "<thead>" +
                          "<tr>" +
                          "<th>Name</th>" +
                          "<th>Date</th>" +
                          "<th>Status</th>" +
                          "<th>Details</th>" +
                          "</tr>" +
                          "</thead>" +
                          "<tbody>";

            foreach (var item in testItems)
            {
                html += $"<tr>" +
                        $"<td>{item.Name}</td>" +
                        $"<td>{item.Date.ToShortDateString()}</td>" +
                        $"<td>{(item.IsAvailable ? "Available" : "Unavailable")}</td>" +
                        $"<td><a href=\"/Details/Index?itemId={item.ItemId}\" class=\"btn btn-info\">View Details</a></td>" +
                        $"</tr>";
            }

            html += "</tbody></table>";

            // Act & Assert
            Assert.IsTrue(html.Contains("<td>Item1</td>"));
            Assert.IsTrue(html.Contains("<td>Available</td>"));
            Assert.IsTrue(html.Contains("<a href=\"/Details/Index?itemId=1\""));

            Assert.IsTrue(html.Contains("<td>Item2</td>"));
            Assert.IsTrue(html.Contains("<td>Unavailable</td>"));
            Assert.IsTrue(html.Contains("<a href=\"/Details/Index?itemId=2\""));
        }

        // Mock models to simulate data
        public class TestItem
        {
            public int ItemId { get; set; }
            public string Name { get; set; }
            public DateTime Date { get; set; }
            public bool IsAvailable { get; set; }
        }
    }
}
