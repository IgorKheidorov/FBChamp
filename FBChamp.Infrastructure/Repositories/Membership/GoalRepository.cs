using FBChamp.Core.Entities.Soccer;
using FBChamp.Core.Repositories.Membership;

namespace FBChamp.Infrastructure.Repositories.Membership;

internal class GoalRepository : JSONRepository<Goal, Guid>, IGoalRepository
{
    protected override string JSONFileName => "Goals.json";
}