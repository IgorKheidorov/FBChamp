using FBChamp.Core.Entities.Socker;

namespace FBChamp.Core.DALModels;

public class PlayerModel : EntityModel
{
    public Player Player { get; }
    public string Position { get; }
    public string CurrentTeam { get; }
    public string PlayingNumber { get; }
    public string PhotoString { get; } 
    public override string FullName => Player.FullName;

    public PlayerModel(Player player, string position, string currentTeam, string playingNumber)
    {
        ArgumentNullException.ThrowIfNull(player);
        Player = player;
        Position = position ?? "No position";
        CurrentTeam = currentTeam ?? "No assignment";
        PlayingNumber = playingNumber ?? "No assignment";
        PhotoString = player.Photo is null ? string.Empty : Convert.ToBase64String(player.Photo);      
    }

    public override IEnumerable<(string, string)> GetInformation() =>  new List<(string, string)>()
    { 
        ("BirthDate", Player.BirthDate.ToShortDateString()),
        ("Height", Player.Height.ToString()),
        ("Club", CurrentTeam),
        ("Playing number", PlayingNumber),
        ("Description", Player.Description)
    };

    public override string GetPhoto() => PhotoString;
}
