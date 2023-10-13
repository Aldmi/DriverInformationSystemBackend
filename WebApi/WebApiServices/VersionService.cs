
namespace WebApi.WebApiServices;

public static class VersionService
{
    public static string GetVersion()
    {
        var aboutProgram = new
        {
            Ver= "1.0.0",
            Description= "Add CORS with default Policy",
            Commit= "b0faecfd702c6dd80e32463741924dcc541b8c83 [b0faecf]",
            Date = "13 октября 2023 г."
        };
       return $"Ver: '{aboutProgram.Ver}'\tDate: '{aboutProgram.Date}'\tDescription: '{aboutProgram.Description}'\t Git: '{aboutProgram.Commit}'\t";
    }
}