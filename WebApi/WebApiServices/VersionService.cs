namespace WebApi.WebApiServices;

public static class VersionService
{
    public static string GetVersion()
    {
        var aboutProgram = new
        {
            Ver= "1.0.4",
            Description= "Add GetUowListByStationTagQuery and UpdateUowListByStationTagCommand with Validator",
            Commit= "3635c44580b86da0087bcea20dc4f52b0d4d1e8 [c3635c4]",
            Date = "27 октября 2023г."
        };
       return $"Ver: '{aboutProgram.Ver}'\tDate: '{aboutProgram.Date}'\tDescription: '{aboutProgram.Description}'\t Git: '{aboutProgram.Commit}'\t";
    }
}