using AventStack.ExtentReports;
using AventStack.ExtentReports.Reporter;
using Serilog;

namespace LabSelenium.src.Hooks;

internal class ExtentReportManager
{
    private static ExtentReports _extent;
    private static ExtentSparkReporter _sparkReporter;
    private static ExtentTest ?_test;
    private static string reportPath = "";
    private static bool _reportInitialized = false;


    [OneTimeSetUp]
    public static void InitReport()
    {

        if (_reportInitialized) return;
        _reportInitialized = true;

        //reportPath = AppDomain.CurrentDomain.BaseDirectory+ "Reports/ExtentReport.html";
        reportPath = Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "Reports/ExtentReport.html");
        _sparkReporter = new ExtentSparkReporter(reportPath)
        {

            Config =
            {
                DocumentTitle="Sauce Labs Test Report",
                ReportName="Test Execution"
            }
        };


        _sparkReporter.Config.DocumentTitle = "Automation Test Report";
        _sparkReporter.Config.ReportName = "Test Execution Report";

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
        Log.Information("\n📄 Extent report saved to: {Path}", reportPath);
        Log.Information("----------------🛑 End of test suite-------------------");
        Log.CloseAndFlush();
    }
}