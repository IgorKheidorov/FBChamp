using FBChamp.Core.Entities.Soccer;

namespace FBChamp.Core.DALModels;

public class GoalModel(
    Goal goal,
    Match match,
    Player goalAuthor,
    List<Player> assistants)
    : EntityModel
{
    public Goal Goal { get; set; } = goal;

    public Match Match { get; set; } = match;

    public Player GoalAuthor { get; set; } = goalAuthor;

    public List<Player> Assistants { get; set; } = assistants;
}