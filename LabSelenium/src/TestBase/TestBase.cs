using AventStack.ExtentReports;
using LabSelenium.src.Config;
using LabSelenium.src.Hooks;
using OpenQA.Selenium;
using Serilog;

namespace LabSelenium.src.TestBase;

public class TestBase
{
    public static readonly Browser BrowserManager = new();
    private static string UrlConfigKey = "UrlBase";
    private static readonly SingletonAppSettings ConfigManager = SingletonAppSettings.GetInstance();
    public static IWebDriver _driver = BrowserManager.SetBrowser();

    [OneTimeSetUp]
    public void OneTimeSetUp()
    {
        ConfigManager.SettingsAtribute = UrlConfigKey;
        ExtentReportManager.InitReport();

    }

    [SetUp]
    public void Setup()
    {
        string url = ConfigManager.DefineJsonPath();
        _driver.Navigate().GoToUrl(url);
        string currentTest = TestContext.CurrentContext.Test.Name;
        ExtentReportManager.CreateTest();
        Log.Information("▶ Starting test: {testName}", currentTest);
        Log.Debug("Setting Browser By paramiters {url}", url);
    }

    [TearDown]
    public void Cleanup()
    {
        var result = TestContext.CurrentContext.Result;

        if (result.Outcome.Status == NUnit.Framework.Interfaces.TestStatus.Failed)
        {
            string? errorMessage = result.Message;
            string? stackTrace = result.StackTrace;

            ExtentReportManager.LogFail("\n❌ Test failed: " + TestContext.CurrentContext.Result.Message);
            ExtentReportManager.LogFail($"\n❌ Error:{errorMessage}");
            ExtentReportManager.LogFail($"\n🧵 StackTrace: {stackTrace}");
        }
        else
        {
            ExtentReportManager.LogPass("✅ Test passed successfully.");
        }

        ExtentReportManager.FlushReport();
        _driver?.Quit();
        _driver = null;
    }

    [OneTimeTearDown]
    public void OneTimeTearDown()
    {
    
        _driver?.Dispose();
   


    }
}