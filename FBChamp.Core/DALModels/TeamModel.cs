using FBChamp.Core.Entities.Socker;
using System.Data;

namespace FBChamp.Core.DALModels;

public class TeamModel: EntityModel
{
    public Team Team { get; }
    public CoachModel Coach { get; }
    public IEnumerable<PlayerModel> Players { get; }
    public string PhotoString { get; }

    public TeamModel(Team team, CoachModel coach, IEnumerable<PlayerModel> players)
    {
        ArgumentNullException.ThrowIfNull(team);
        Team = team;
        Coach = coach;
        PhotoString = team.Photo is null ? string.Empty : Convert.ToBase64String(team.Photo);
    }

    public override IEnumerable<(string, string)> GetInformation() => new List<(string, string)>(){
            ("Team", Team.Name),
            ("Coach", Coach?.Coach.FullName),
            };

    public override string FullName => Team.Name;
    public override string GetPhoto() => PhotoString;
}
