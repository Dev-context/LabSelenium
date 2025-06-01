using Microsoft.Extensions.Configuration;

namespace LabSelenium.Config;

public sealed class JsonFileConfig
{
    public static string DefineJsonPath(string key)
    {
        var configuration = new ConfigurationBuilder()
            .SetBasePath(Directory.GetCurrentDirectory())
            .AddJsonFile("appsettings.json", optional: false).Build();
        return configuration[key] ??
            throw new Exception("Error to find variable, please, check the appsettings file");
    }
}