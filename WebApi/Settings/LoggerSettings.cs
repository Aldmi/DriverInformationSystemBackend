
namespace WebApi.Settings;

internal class LoggerSettings
{
    public FileSinkSetting? FileSinkSetting { get; }
    public SeqSinkSetting? SeqSinkSetting { get; }

    public LoggerSettings(FileSinkSetting? fileSinkSetting, SeqSinkSetting? seqSinkSetting)
    {
        FileSinkSetting = fileSinkSetting;

        SeqSinkSetting = seqSinkSetting;
    }

    public override string ToString()
    {
        var fileSinkSettingStr = FileSinkSetting == null ? string.Empty : $"FileSinkSetting:Enable={FileSinkSetting.Enable}";
        var seqSinkSettingStr = SeqSinkSetting == null ? string.Empty : $"SeqSinkSetting:Enable={SeqSinkSetting.Enable}, Uri={SeqSinkSetting.Uri}, ApiKey={SeqSinkSetting.ApiKey}";
        return $"Logger:<{fileSinkSettingStr}>\t<{seqSinkSettingStr}>";
    }
}

internal abstract class SinkSetting
{
    public bool Enable { get; }
    protected SinkSetting(bool enable)
    {
        Enable = enable;
    }
}
    
internal class FileSinkSetting : SinkSetting
{
    public FileSinkSetting(bool enable) : base(enable)
    {
    }
}
    

internal class SeqSinkSetting : SinkSetting
{
    public string ApiKey { get; }
    public string Uri { get; }
        
    public SeqSinkSetting(bool enable, string apiKey, string uri) : base(enable)
    {
        ApiKey = apiKey;
        Uri = uri;
    }
}