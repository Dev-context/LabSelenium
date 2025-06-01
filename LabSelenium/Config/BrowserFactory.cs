

using OpenQA.Selenium;
using OpenQA.Selenium.Chrome;
using OpenQA.Selenium.Firefox;

namespace LabSelenium.Config;
public class BrowserFactory
{
    private readonly static bool opt = false;
    public static T BrowserArguments<T>()where T:DriverOptions,new()
    {
        var options=new T();

        if (opt)
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
