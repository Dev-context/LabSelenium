using OpenQA.Selenium;
using OpenQA.Selenium.Chrome;
using OpenQA.Selenium.Firefox;

namespace LabSelenium.Config;

public class BrowserFactory
{
    private  static string HeadlessConfigKey = "Agent:Headless";
    private static readonly SingletonAppSettings AppSettings = SingletonAppSettings.GetInstance();


    public static T BrowserArguments<T>() where T : DriverOptions, new()
    {
        
        var options = new T();

        AppSettings.SettingsAtribute = HeadlessConfigKey;
        var headlessValue = AppSettings.DefineJsonPath();
        Console.WriteLine($"Headless mode: {headlessValue} comp {String.Equals(headlessValue, "true", StringComparison.OrdinalIgnoreCase)}");

        if (String.Equals(headlessValue,"true",StringComparison.OrdinalIgnoreCase))
        {
            switch (options)
            {
                case ChromeOptions chromeOptions:
                    chromeOptions.AddArgument("--headless=new");
                    break;

                case FirefoxOptions firefoxOptions:
                    firefoxOptions.AddArgument("--headless");
                    break;
            }
        }
        return options;
    }
}