using AventStack.ExtentReports;
using AventStack.ExtentReports.Reporter;
using OpenQA.Selenium;
using Serilog;

namespace LabSelenium.src.Hooks;

internal class ExtentReportManager
{
    private static ExtentReports _extent;
    private static ExtentSparkReporter _sparkReporter;
    private static ExtentTest ?_test;
    private static bool _reportInitialized = false;
    private static System.AppDomain  _rootDirectory= AppDomain.CurrentDomain;
    private static string _reportPath ="";


    [OneTimeSetUp]
    public static void InitReport()
    {
        var reportPath = Path.Combine(_rootDirectory.BaseDirectory, "ExtentReport.html");
        var _extentReportPath = Path.Combine(_rootDirectory.BaseDirectory, "spark-config.json");

      


        _sparkReporter = new ExtentSparkReporter(reportPath);
        _sparkReporter.LoadJSONConfig(_extentReportPath);


        _extent = new ExtentReports();
        _extent.AttachReporter(_sparkReporter);


        Log.Logger = new LoggerConfiguration()
        .MinimumLevel.Debug()
        .WriteTo.Console()
        .WriteTo.File($"logs/RunLog{TestContext.CurrentContext.Test.Name}_{DateTime.Now:yyyyMMdd_HHmmss}.log", rollingInterval: RollingInterval.Day)
        .CreateLogger();

        Log.Information("---------------------------BEGIN---------------------------------------");
        Log.Information("___SauceLab Test Framework");
        Log.Information($"\t___Test {TestContext.CurrentContext.Test.Name}");

    }

    public static void CreateTest()
    {
        string baseName = TestContext.CurrentContext.Test.Name;
        string timeSuffix = DateTime.Now.ToString("HHmmss_fff"); // hora:minuto:segundo:milissegundo
        string uniqueName = $"{baseName}_{timeSuffix}";

        _test = _extent.CreateTest(uniqueName);
        Log.Information("▶ Iniciando teste: {TestName}", uniqueName);
    }

    public static void LogPass(string message)
    {
        _test.Log(Status.Pass, message);
        Log.Debug($"{Status.Pass} {message}");

    }

    public static void LogFail(string message)
    {
        _test.Log(Status.Fail, message);
        Log.Debug($"❌ {Status.Fail} {message}");
    }

    public static void FlushReport()
    {

        _extent.Flush();
        Log.Information("\n📄 Extent report saved to: {Path}", _reportPath);
        Log.Information("----------------🛑 End of test suite-------------------");
        Log.CloseAndFlush();
    }
    
}