namespace WebApi.WebApiServices;

public static class VersionService
{
    public static string GetVersion()
    {
        var aboutProgram = new
        {
            Ver= "1.0.1",
            Description= "- Add LogLevel managment. - Load Settings in Production from Enviroment",
            Commit= "ad51eef8faaf83a7311433a52598e43d7b06264a [ad51eef]",
            Date = "17 октября 2023г."
        };
       return $"Ver: '{aboutProgram.Ver}'\tDate: '{aboutProgram.Date}'\tDescription: '{aboutProgram.Description}'\t Git: '{aboutProgram.Commit}'\t";
    }
}