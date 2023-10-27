namespace WebApi.WebApiServices;

public static class VersionService
{
    public static string GetVersion()
    {
        var aboutProgram = new
        {
            Ver= "1.0.4",
            Description= "Add GetUowListByStationTagQuery and UpdateUowListByStationTagCommand with Validator",
            Commit= "258acd5f05b0f17c2349bb873b045cdc80cf53a2 [258acd5]",
            Date = "27 октября 2023г."
        };
       return $"Ver: '{aboutProgram.Ver}'\tDate: '{aboutProgram.Date}'\tDescription: '{aboutProgram.Description}'\t Git: '{aboutProgram.Commit}'\t";
    }
}