using FBChamp.Core.UnitOfWork;
using FBChamp.Web.Common.EntityBuilders.Admin;
using FBChamp.Web.Common.Interfaces;

namespace FBChamp.Web.Common.EntityBuilders;

public class EntityBuildersFactory(IUnitOfWork unitOfWork)
    : IEntityBuildersFactory
{
    private CoachAssignmentInfoBuilder _coachAssignmentInfoBuilder;
    private CoachBuilder _coachBuilder;
    private PlayerAssignmentInfoBuilder _playerAssignmentInfoBuilder;
    private PlayerBuilder _playerBuilder;
    private TeamBuilder _teamsBuilder;

    public IEntityBuilder GetBuilder(string viewModelType) =>
        viewModelType switch
        {
            "Team" => _teamsBuilder ??= new TeamBuilder(unitOfWork),
            "PlayerAssignmentInfo" => _playerAssignmentInfoBuilder ??= new PlayerAssignmentInfoBuilder(unitOfWork),
            "Player" => _playerBuilder ??= new PlayerBuilder(unitOfWork),
            "Coach" => _coachBuilder ??= new CoachBuilder(unitOfWork),
            "CoachAssignmentInfo" => _coachAssignmentInfoBuilder ??= new CoachAssignmentInfoBuilder(unitOfWork),
            _ => null
        };
}