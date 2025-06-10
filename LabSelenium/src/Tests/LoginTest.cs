using LabSelenium.src.Pages;
using OpenQA.Selenium;

namespace LabSelenium.src.Tests
{

    public class LoginTest : TestBase.TestBase
    {
        private readonly LoginPage _loginPage = new(_driver);

        
        [Test]
        public void SucessLogin()
        {      
            _loginPage.Login("standard_user", "secret_sauce");
            IWebElement productTitle = _driver.FindElement(By.CssSelector("span[data-test='title']"));
            Assert.That(productTitle.Text == "Products");
            
        }
    }
}