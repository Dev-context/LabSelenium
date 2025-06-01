using Microsoft.Extensions.Configuration;

namespace LabSelenium.src.Config;

public sealed class SingletonAppSettings
{
    private static SingletonAppSettings _instance;
    private string _settingsAttribute = "";

    public static SingletonAppSettings GetInstance()
    {

        if (_instance == null)
        {
           
            return _instance = new SingletonAppSettings();
        }
        else
        {
            return _instance;
        }
    }

    public string SettingsAtribute
    {
        set => _settingsAttribute = value;

        get => _settingsAttribute;
    }

    public string DefineJsonPath()
    {
        var configuration = new ConfigurationBuilder()
            .SetBasePath(Directory.GetCurrentDirectory())
            .AddJsonFile("appsettings.json", optional: false)
            .Build();

        return configuration[_settingsAttribute] ??
            throw new Exception("Error to find variable, please, check the appsettings file");
    }
}