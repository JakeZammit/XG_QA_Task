using NUnit.Framework;
using OpenQA.Selenium;
using OpenQA.Selenium.Chrome;

namespace SauceDemoAutomationTests
{
    [TestFixture]
    public class SauceDemoTests
    {
        private ChromeDriver? driver;

        [SetUp]
        public void SetUp()
        {
            driver = new ChromeDriver();
            driver.Manage().Window.Maximize();
            driver.Navigate().GoToUrl("https://www.saucedemo.com/");
        }

        [Test]
        public void LoginTest_ValidUser()
        {
            //checks
            var usernameField = driver!.FindElement(By.Id("user-name"));
            var passwordField = driver.FindElement(By.Id("password"));
            var loginButton = driver.FindElement(By.Id("login-button"));

            //inputs
            usernameField.SendKeys("standard_user");
            passwordField.SendKeys("secret_sauce");
            loginButton.Click();

            //check the products page
            Assert.That(driver.Url, Does.Contain("inventory.html"), "Login failed");

            var inventoryContainer = driver.FindElement(By.ClassName("inventory_list"));
            Assert.That(inventoryContainer.Displayed, Is.True, "Inventory list not visible");
        }

        //Automatically closes the window
        [TearDown]
        public void TearDown()
        {
            if (driver != null)
            {
                //wait 10seconds to visually see test completed before automatically closing
                System.Threading.Thread.Sleep(10000);
                driver.Quit();
                driver.Dispose();
                driver = null;
            }
        }

    }
}
