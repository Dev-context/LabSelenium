using OpenQA.Selenium;

namespace LabSelenium.src.Pages;
public class LoginPage
{
    private  IWebDriver driver;

    public LoginPage(IWebDriver driver) 
    {  
        this.driver = driver; 
    }

    IWebElement UserName =>driver.FindElement(By.Id("user-name"));
    IWebElement Password => driver.FindElement(By.Id("password"));
    IWebElement LoginButton => driver.FindElement(By.Id("login-button"));


    public void Login(string userName,string password)
    {
        UserName.SendKeys(userName);
        Password.SendKeys(password);
        LoginButton.Click();
    }

}
