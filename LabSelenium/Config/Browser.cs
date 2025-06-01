
using OpenQA.Selenium;
using OpenQA.Selenium.Chrome;
using System.Configuration;


namespace LabSelenium.Config.Browser;
public class Browser:BrowserFactory
{
    private  static string BrowserName = "chrome";
    protected  static String? BrowserName1 = ConfigurationManager.AppSettings["IsHeadless"]
        ?? "Not Found";
    public static IWebDriver? driver;
    public IWebDriver SetBrowser()
      
    {
        Console.WriteLine(BrowserName1 + " Navegador selecionado"   );
        if (BrowserName == "chrome")
        {
            var chromeOptions = BrowserArguments<ChromeOptions>();
            driver = new ChromeDriver(chromeOptions);
            return driver;
        }
        return driver;
    }
}
