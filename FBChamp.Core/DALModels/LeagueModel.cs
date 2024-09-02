using FBChamp.Core.Entities.Soccer;

namespace FBChamp.Core.DALModels;

public class LeagueModel : EntityModel
{
    public League League { get; }

    public string PhotoString { get; }

    public IEnumerable<TeamModel> Teams { get; }

    public override string FullName => League.FullName;

    public LeagueModel(League league, IEnumerable<TeamModel> teams)
    {
        League = league;
        Teams = teams;
        PhotoString = league.Photo is null ? string.Empty : Convert.ToBase64String(league.Photo);
    }

    public override IEnumerable<(string, string)> GetInformation() => new List<(string, string)>()
    {
       ("FullName",League.FullName),
       ("NumberOfTeams",League.NumberOfTeams.ToString()),
       ("SeasonStartDate",League.SeasonStartDate.ToShortDateString()),
       ("SeasonFinishDate",League.SeasonFinishDate.ToShortDateString()),
       ("Description",League.Description),
    };

    public override string GetPhoto() => PhotoString;
}
