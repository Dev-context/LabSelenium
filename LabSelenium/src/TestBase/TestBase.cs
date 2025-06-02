using LabSelenium.src.Config;
using OpenQA.Selenium;

namespace LabSelenium.src.TestBase;

public class TestBase
{
    public static readonly Browser BrowserManager  = new();
    private static string UrlConfigKey = "UrlBase";
    private static readonly SingletonAppSettings ConfigManager = SingletonAppSettings.GetInstance();
    public static IWebDriver _driver = BrowserManager.SetBrowser();


    [SetUp]
    public void Setup()
    {
  
        ConfigManager.SettingsAtribute = UrlConfigKey;
        string url = ConfigManager.DefineJsonPath();
        _driver.Navigate().GoToUrl(url);
        Console.WriteLine("Teste Iniciado ");
    }

    [OneTimeTearDown]
    public void Cleanup()
    {
        _driver?.Quit();
        _driver?.Dispose();
        _driver = null;
        Console.Write("Close");
    }
}