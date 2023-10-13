using System.Text.Json;

namespace WebApi.WebApiServices;

public static class VersionService
{
    public static string GetVersion()
    {
        var ver = new
        {
            Ver= 1.0,
            Description="Базовая версия",
            Git=""
        };
       return $"Ver: '{ver.Ver}'\tDescription= '{ver.Description}'\t Git= '{ver.Git}'\t";
    }
}

