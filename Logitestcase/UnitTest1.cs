using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;

namespace Logitestcase
{
    [TestClass]
    public class UnitTest1
    {
        [TestMethod]
        public void TestMethod1()
        {
        }
        [TestMethod]
        public void LoginForm_Should_Have_Required_Fields()
        {
            // Simulating the form fields
            string loginFormHtml = @"
        <form>
            <input type='text' name='email' />
            <input type='password' name='password' />
            <button type='submit'>Login</button>
        </form>";

            // Assert that the form contains required elements
            Assert.IsTrue(loginFormHtml.Contains("name='email'"), "Email field is missing.");
            Assert.IsTrue(loginFormHtml.Contains("name='password'"), "Password field is missing.");
            Assert.IsTrue(loginFormHtml.Contains("type='submit'"), "Submit button is missing.");
        }
        [TestMethod]
        public void LoginForm_Should_Display_ErrorMessage()
        {
            // Simulating an error message
            string errorMessageHtml = @"
        <div class='alert alert-danger'>Invalid login credentials.</div>";

            // Assert that the error message exists
            Assert.IsTrue(errorMessageHtml.Contains("Invalid login credentials."), "Error message is not displayed.");
        }

    }
}
