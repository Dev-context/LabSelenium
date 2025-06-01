using LabSelenium.Config;
using LabSelenium.Config.Browser;
using OpenQA.Selenium;

namespace LabSelenium.TestBase;

public class TestBase
{
    public readonly Browser BrowserManager  = new Browser();
    private static string UrlConfigKey = "UrlBase";
    private static readonly SingletonAppSettings ConfigManager = SingletonAppSettings.GetInstance();
    private IWebDriver _driver;


    [SetUp]
    public void Setup()
    {
        _driver = BrowserManager.SetBrowser();
        ConfigManager.SettingsAtribute = UrlConfigKey;
        string url = ConfigManager.DefineJsonPath();
        _driver.Navigate().GoToUrl(url);
        Console.WriteLine("Teste Iniciado ");
    }

    [TearDown]
    public void TearDown()
    {
        _driver?.Quit();
    }
}