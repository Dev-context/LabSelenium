using OpenQA.Selenium;
using OpenQA.Selenium.Chrome;

namespace LabSelenium.Config.Browser;

public class Browser : BrowserFactory
{
    private static string BrowserName = "Agent:BrowserName";

    private SingletonAppSettings JsonBrowserName = SingletonAppSettings.GetInstance();
    public static IWebDriver? driver;

    public IWebDriver SetBrowser()

    {
        JsonBrowserName.SettingsAtribute = BrowserName;

        if (JsonBrowserName.DefineJsonPath() == "chrome")
        {
            var chromeOptions = BrowserArguments<ChromeOptions>();
            driver = new ChromeDriver(chromeOptions);
            return driver;
        }
        return driver;
    }
}