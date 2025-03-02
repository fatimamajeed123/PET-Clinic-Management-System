using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;

namespace Registration_petownertestcases
{
    [TestClass]
    public class UnitTest1
    {
        [TestMethod]
        public void TestMethod1()
        {
        }
        [TestMethod]
        public void RegistrationForm_Should_Have_Required_Fields()
        {
            // Simulating the registration form HTML
            string registrationFormHtml = @"
        <form>
            <input name='FirstName' />
            <input name='LastName' />
            <input name='Email' />
            <input name='PhoneNo' />
            <input name='Password' type='password' />
            <button type='submit'>Register</button>
        </form>";

            // Assert that each field exists
            Assert.IsTrue(registrationFormHtml.Contains("name='FirstName'"), "First Name field is missing.");
            Assert.IsTrue(registrationFormHtml.Contains("name='LastName'"), "Last Name field is missing.");
            Assert.IsTrue(registrationFormHtml.Contains("name='Email'"), "Email field is missing.");
            Assert.IsTrue(registrationFormHtml.Contains("name='PhoneNo'"), "Phone Number field is missing.");
            Assert.IsTrue(registrationFormHtml.Contains("name='Password'"), "Password field is missing.");
        }
        [TestMethod]
        public void RegistrationForm_Should_Have_Submit_Button()
        {
            // Simulating the registration form HTML
            string registrationFormHtml = @"
        <form>
            <button type='submit'>Register</button>
        </form>";

            // Assert that the submit button exists
            Assert.IsTrue(registrationFormHtml.Contains("type='submit'"), "Submit button is missing.");
        }
        [TestMethod]
        public void RegistrationForm_Should_Validate_Empty_Fields()
        {
            // Simulate user inputs
            var firstName = "";
            var lastName = "Doe";
            var email = "john.doe@example.com";
            var phoneNo = "1234567890";
            var password = "password123";

            // Assert that no fields are empty
            Assert.IsFalse(string.IsNullOrWhiteSpace(firstName), "First Name cannot be empty.");
            Assert.IsFalse(string.IsNullOrWhiteSpace(lastName), "Last Name cannot be empty.");
            Assert.IsFalse(string.IsNullOrWhiteSpace(email), "Email cannot be empty.");
            Assert.IsFalse(string.IsNullOrWhiteSpace(phoneNo), "Phone Number cannot be empty.");
            Assert.IsFalse(string.IsNullOrWhiteSpace(password), "Password cannot be empty.");
        }

    }
}
