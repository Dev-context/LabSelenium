using LabSelenium.src.Pages;
using LabSelenium.src.TestBase;
using OpenQA.Selenium;

namespace LabSelenium
{
    public class Tests : TestBase
    {
        private LoginPage _loginPage = new(_driver);
        [Test]
        public void sucessLogin()
        {      
            _loginPage.Login("standard_user", "secret_sauce");
            IWebElement productTitle = _driver.FindElement(By.CssSelector("span[data-test='title']"));
            Assert.That(productTitle.Text == "Products");
        }
    }
}