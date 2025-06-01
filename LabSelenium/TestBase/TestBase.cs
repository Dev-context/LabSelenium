using LabSelenium.Config;
using LabSelenium.Config.Browser;
using OpenQA.Selenium;

namespace LabSelenium.TestBase;
public class TestBase
{

    public readonly Browser _browser = new();

    [SetUp]
    public void Setup()
    {
       
        _browser.SetBrowser().Navigate().GoToUrl("https://google.com");
        Console.WriteLine("Teste Iniciado ");
    }

    [TearDown]
    public void TearDown()
    {

    }
}
