using Application.Common.LogServices;
using Serilog;
using Serilog.Events;

using WebApi.Settings;

namespace WebApi.Extensions;

public static class SerilogExtensions
{
    /// <summary>
    /// Регистрируем Logger и LogLevelSwitchService как Singleton
    /// </summary>
    internal static IServiceCollection AddSerilogServices(this IServiceCollection services, LoggerSettings settings, IWebHostEnvironment env)
    {
        var levelSwitcher = new LogLevelSwitchService();
        
        //levelSwitcher.SwitchLevel2ConsoleDebug();
        levelSwitcher.SwitchLevel2ConsoleInfo();
        //levelSwitcher.SwitchLevel2StructureLogingInfo();
        
        var loggerConf = ConfigLogger(env.ApplicationName, settings, levelSwitcher);
        Log.Logger = loggerConf.CreateLogger();
        
        return services
            .AddSingleton(Log.Logger)
            .AddSingleton(levelSwitcher);
    }

        
    private static LoggerConfiguration ConfigLogger(string appName, LoggerSettings settings, LogLevelSwitchService logLevelSwitcher)
    {
        var loggerConf = new LoggerConfiguration()
            .MinimumLevel.ControlledBy(logLevelSwitcher.MinimumLevel)
            .MinimumLevel.Override("Microsoft", LogEventLevel.Information)
            .MinimumLevel.Override("System", LogEventLevel.Warning)
            .MinimumLevel.Override("Microsoft.AspNetCore", LogEventLevel.Information) 
            .Enrich.FromLogContext()
            .WriteTo.Console(
                outputTemplate: "[{Timestamp:HH:mm:ss} {Level:u3}]{Message:lj} {NewLine}{Exception}",
                levelSwitch: logLevelSwitcher.Console);
            
        if (settings.FileSinkSetting is {Enable: true})
        {
            loggerConf
                .WriteTo.File("logs/Main_Log.txt",
                    LogEventLevel.Information,
                    rollingInterval: RollingInterval.Day, //за 20 последних дней хранится Information лог (100МБ лимит размера файла)
                    retainedFileCountLimit: 20,
                    fileSizeLimitBytes: 100000000,
                    rollOnFileSizeLimit: true)

                .WriteTo.File(
                    "logs/Error_Log.txt", //за 20 последних дней хранится Error лог (100МБ лимит размера файла)
                    LogEventLevel.Error,
                    rollingInterval: RollingInterval.Day,
                    retainedFileCountLimit: 20,
                    fileSizeLimitBytes: 100000000,
                    rollOnFileSizeLimit: true)

                .WriteTo.File(
                    "logs/Debug/Debug_Log.txt", //За 48 последних часов переписывается Debug лог (100МБ лимит размера файла).
                    LogEventLevel.Debug,
                    rollingInterval: RollingInterval.Hour,
                    retainedFileCountLimit: 48,
                    fileSizeLimitBytes: 100000000,
                    rollOnFileSizeLimit: true,
                    shared: true);
        }

        
        if (settings.SeqSinkSetting is {Enable: true})
        {
            loggerConf.WriteTo.Seq(
                serverUrl: settings.SeqSinkSetting.Uri,
                apiKey: settings.SeqSinkSetting.ApiKey,
                controlLevelSwitch: logLevelSwitcher.StructureLog);
        }
            
        return loggerConf;
    }
}