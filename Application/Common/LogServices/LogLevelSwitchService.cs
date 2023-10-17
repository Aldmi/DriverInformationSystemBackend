using Serilog.Core;
using Serilog.Events;

namespace Application.Common.LogServices;

public enum MessageType {None, IndexedObject, StringJson }

public class LogLevelSwitchService
{
    /// <summary>
    /// По умолчанию состояние SwitchLevel2ConsoleInfo
    /// </summary>
    public LogLevelSwitchService()
    {
        Console =  new LoggingLevelSwitch();
        StructureLog = new LoggingLevelSwitch();
        MinimumLevel = new LoggingLevelSwitch();
    }
    
    public LoggingLevelSwitch MinimumLevel { get; }
    public LoggingLevelSwitch Console { get; }
    public LoggingLevelSwitch StructureLog { get; }
    public MessageType MessageType { get; private set; }
    
    
    /// <summary>
    /// Пишем в Console.
    /// MinimumLevel = LogEventLevel.Debug
    /// </summary>
    public void SwitchLevel2ConsoleDebug()
    {
        MessageType = MessageType.StringJson;
        MinimumLevel.MinimumLevel = LogEventLevel.Debug;
        Console.MinimumLevel = LogEventLevel.Debug;
        StructureLog.MinimumLevel = LogEventLevel.Fatal;
    }
    
    /// <summary>
    /// Пишем в Console.
    /// MinimumLevel = LogEventLevel.Information
    /// </summary>
    public void SwitchLevel2ConsoleInfo()
    {
        MessageType = MessageType.StringJson;
        MinimumLevel.MinimumLevel = LogEventLevel.Information;
        Console.MinimumLevel = LogEventLevel.Information;
        StructureLog.MinimumLevel = LogEventLevel.Fatal;
    }
    
    
    
    /// <summary>
    /// Пишем в StructureLoging.
    /// MinimumLevel = LogEventLevel.Debug
    /// </summary>
    public void SwitchLevel2StructureLogingDebug()
    {
        MessageType = MessageType.IndexedObject;
        MinimumLevel.MinimumLevel = LogEventLevel.Debug;
        Console.MinimumLevel = LogEventLevel.Fatal;
        StructureLog.MinimumLevel = LogEventLevel.Debug;
    }
    
    /// <summary>
    /// Пишем в StructureLoging.
    /// MinimumLevel = LogEventLevel.Information
    /// </summary>
    public void SwitchLevel2StructureLogingInfo()
    {
        MessageType = MessageType.IndexedObject;
        MinimumLevel.MinimumLevel = LogEventLevel.Information;
        Console.MinimumLevel = LogEventLevel.Fatal;
        StructureLog.MinimumLevel = LogEventLevel.Information;
    }
    
    /// <summary>
    /// Пишем в StructureLoging Information
    /// Пишем в Console Warning.
    /// Production-ready режим
    /// </summary>
    public void SwitchLevel2StructureLogingInfoAndConsoleWarning()
    {
        MessageType = MessageType.IndexedObject;
        MinimumLevel.MinimumLevel = LogEventLevel.Information;
        Console.MinimumLevel = LogEventLevel.Warning;
        StructureLog.MinimumLevel = LogEventLevel.Information;
    }
    
    
    /// <summary>
    /// Пишем в StructureLoging Warning
    /// Пишем в Console Information.
    /// Production-ready режим
    /// </summary>
    public void SwitchLevel2StructureLogingWarningAndConsoleInfo()
    {
        MessageType = MessageType.IndexedObject;
        MinimumLevel.MinimumLevel = LogEventLevel.Information;
        Console.MinimumLevel = LogEventLevel.Information;
        StructureLog.MinimumLevel = LogEventLevel.Warning;
    }
    
    /// <summary>
    /// Пишем в StructureLoging Warning
    /// Пишем в Console Warning.
    /// </summary>
    public void SwitchLevel2StructureLogingWarningAndConsoleWarning()
    {
        MessageType = MessageType.IndexedObject;
        MinimumLevel.MinimumLevel = LogEventLevel.Warning;
        Console.MinimumLevel = LogEventLevel.Warning;
        StructureLog.MinimumLevel = LogEventLevel.Warning;
    }
    
    /// <summary>
    /// Пишем в StructureLoging Info
    /// Пишем в Console Info.
    /// </summary>
    public void SwitchLevel2StructureLogingInfoAndConsoleInfo()
    {
        MessageType = MessageType.IndexedObject;
        MinimumLevel.MinimumLevel = LogEventLevel.Information;
        Console.MinimumLevel = LogEventLevel.Information;
        StructureLog.MinimumLevel = LogEventLevel.Information;
    }
    
    public object GetLevelSwitcherReport()
    {
        var resp = new
        {
            MinLevel = MinimumLevel.MinimumLevel.ToString("G"),
            Type = MessageType.ToString("G"),
            ConsoleMinLevel = Console.MinimumLevel.ToString("G"),
            StructureMinLevel = StructureLog.MinimumLevel.ToString("G")
        };
        return resp;
    }
}




