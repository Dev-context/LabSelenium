using OpenQA.Selenium;
using OpenQA.Selenium.Chrome;
using OpenQA.Selenium.Firefox;
using OpenQA.Selenium.IE;
namespace LabSelenium.src.Config;

public class Browser : BrowserFactory
{
    private static readonly string BrowserName = "Agent:BrowserName";

    private readonly SingletonAppSettings JsonBrowserName = SingletonAppSettings.GetInstance();
    public static IWebDriver? driver;

    public IWebDriver SetBrowser()

    {
     
        JsonBrowserName.SettingsAtribute = BrowserName;
        switch (JsonBrowserName.DefineJsonPath().ToUpper())
        {
            case var browser when browser == BrowserTypes.CHROME.ToString():
                var chromeOptions = BrowserArguments<ChromeOptions>();
                driver = new ChromeDriver(chromeOptions);
                return driver;

            case var browser when browser == BrowserTypes.FIREFOX.ToString():
                var firefoxOptions = BrowserArguments<FirefoxOptions>();
                driver = new FirefoxDriver(firefoxOptions);
                return driver;

            case var browser when browser == BrowserTypes.IE.ToString():
                var ieOptions = BrowserArguments<InternetExplorerOptions>();
                driver = new InternetExplorerDriver(ieOptions);
                return driver;

            default:
                throw new NotSupportedException("Unsupported browser specified in config.");
        }

    }
}