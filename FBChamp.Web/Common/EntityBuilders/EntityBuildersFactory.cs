using FBChamp.Web.Common.VewModelsBuilders.Admin;
using FBChamp.Web.Common.VewModelsBuilders.Shared;
using FBChamp.Web.Common.VewModelsBuilders;
using FBChamp.Web.Common.EntityBuilders.Admin;
using FBChamp.Web.Common.Interfaces;
using FBChamp.Core.UnitOfWork;

namespace FBChamp.Web.Common.EntityBuilders;

public class EntityBuildersFactory : IEntityBuildersFactory
{
    private readonly IUnitOfWork _unitOfWorks;
    private TeamBuilder _teamsBuilder;
    private PlayerAssignmentInfoBuilder _playerAssignmentInfoBuilder;
    private PlayerBuilder _playerBuilder;
    private CoachBuilder _coachBuilder;
    private CoachAssignmentInfoBuilder _coachAssignmentInfoBuilder;

    public EntityBuildersFactory(IUnitOfWork unitOfWork)
    {
        _unitOfWorks = unitOfWork;
    }

    public IEntityBuilder GetBuilder(string viewModelType) => viewModelType switch
    {
        "Team" => _teamsBuilder ??= new TeamBuilder(_unitOfWorks),
        "PlayerAssignmentInfo" => _playerAssignmentInfoBuilder ??= new PlayerAssignmentInfoBuilder(_unitOfWorks),
        "Player" => _playerBuilder ??= new PlayerBuilder(_unitOfWorks),
        "Coach" => _coachBuilder ??= new CoachBuilder(_unitOfWorks),
        "CoachAssignmentInfo" => _coachAssignmentInfoBuilder ??= new CoachAssignmentInfoBuilder(_unitOfWorks),
        _ => null
    };

   
   
}
