using FBChamp.Core.Entities.Soccer;

namespace FBChamp.Core.DALModels;

public class GoalModel : EntityModel
{
    public Goal Goal { get; set; }

    public GoalModel(Goal goal)
    {
        Goal = goal;
    }
}