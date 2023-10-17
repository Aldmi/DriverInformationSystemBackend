using System.Text.Json;


namespace WebApi.Settings;

public static class SettingsFactory
{
    private static readonly JsonSerializerOptions SerializerOptions = new JsonSerializerOptions(JsonSerializerDefaults.Web);
    
    internal static string GetDbConnectionString(IHostEnvironment env, IConfiguration conf)
    {
        var connectionStr = env.IsDevelopment() ? conf.GetConnectionString("Mongodb") : Environment.GetEnvironmentVariable("DbConnection");
        if (string.IsNullOrEmpty(connectionStr))
            throw new NullReferenceException($"Переменная Mongodb (строка подключения к БД) НЕ найдена. IsDevelopment= {env.IsDevelopment()}");
        
        return connectionStr;
    }

    
    internal static LoggerSettings? GetLoggerConfig(IHostEnvironment env, IConfiguration conf)
    {
        LoggerSettings? loggerSettings;
        if (env.IsDevelopment())
        {
            if (!bool.TryParse(conf["Logger:fileSinkSetting:enable"], out var fileSinkEnabel))
                throw new Exception($"Logger:fileSinkSetting:enable не удалось преобразовать к bool. IsDevelopment= {env.IsDevelopment()}");

            var fileSinkSett = new FileSinkSetting(fileSinkEnabel);
            
            if (!bool.TryParse(conf["Logger:seqSinkSetting:enable"], out var seqSinkEnable))
                throw new Exception($"Logger:seqSinkSetting:enable не удалось преобразовать к bool. IsDevelopment= {env.IsDevelopment()}");
                
            var seqApiKey = conf["Logger:seqSinkSetting:apiKey"];
            if(string.IsNullOrEmpty(seqApiKey))
                throw new Exception($"Logger:seqSinkSetting:apiKey не задан. IsDevelopment= {env.IsDevelopment()}");
                
            var seqUri = conf["Logger:seqSinkSetting:uri"];
            if(string.IsNullOrEmpty(seqUri))
                throw new Exception($"Logger:seqSinkSetting:uri не задан. IsDevelopment= {env.IsDevelopment()}");
                
            var seqSinkSetting = new SeqSinkSetting(seqSinkEnable, seqApiKey, seqUri);
                
            loggerSettings = new LoggerSettings(fileSinkSett, seqSinkSetting);
        }
        else
        {
            var loggerSett = Environment.GetEnvironmentVariable("LoggerSetting");
            if (string.IsNullOrEmpty(loggerSett))
                throw new Exception($"LoggerSetting Не задан в Enviroment переменной. IsDevelopment= {env.IsDevelopment()}");
             
            try
            {
                loggerSettings = JsonSerializer.Deserialize<LoggerSettings>(loggerSett, SerializerOptions);
            }
            catch (Exception ex)
            {
                throw new Exception($"Исключение при дессериализации настроек логера {ex}");
            }
        }
        return loggerSettings;
    }
}