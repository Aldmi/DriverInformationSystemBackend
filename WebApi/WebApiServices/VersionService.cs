namespace WebApi.WebApiServices;

public static class VersionService
{
    public static string GetVersion()
    {
        var aboutProgram = new
        {
            Ver= "1.0.2",
            Description= "- Add RouteMetroList CRUD Features",
            Commit= "508de926197ff590a2f9442d550f944f9256f2e9 [508de92]",
            Date = "22 октября 2023г."
        };
       return $"Ver: '{aboutProgram.Ver}'\tDate: '{aboutProgram.Date}'\tDescription: '{aboutProgram.Description}'\t Git: '{aboutProgram.Commit}'\t";
    }
}