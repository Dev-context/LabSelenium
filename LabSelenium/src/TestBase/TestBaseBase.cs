using OpenQA.Selenium;

namespace LabSelenium.src.TestBase;

public class TestBaseBase
{
    public IWebDriver _driver;

    [TearDown]
    public void TearDown()
    {
        //_driver?.Quit();
        Console.Write("Close");
    }
}