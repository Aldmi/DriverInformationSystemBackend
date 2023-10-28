namespace WebApi.WebApiServices;

public static class VersionService
{
    public static string GetVersion()
    {
        var aboutProgram = new
        {
            Ver= "1.0.5",
            Description= "Fix bug in UpdateCarrigeListValidator - uniqCarrigeNumber kenght 2..3",
            Commit= "db3dec86d31feae5ef8bd7e08c4d1d1473fcf580 [db3dec8]",
            Date = "28 октября 2023г."
        };
       return $"Ver: '{aboutProgram.Ver}'\tDate: '{aboutProgram.Date}'\tDescription: '{aboutProgram.Description}'\t Git: '{aboutProgram.Commit}'\t";
    }
}