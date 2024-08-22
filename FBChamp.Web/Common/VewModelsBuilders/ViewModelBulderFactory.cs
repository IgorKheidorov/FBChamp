using FBChamp.Core.UnitOfWork;
using FBChamp.Web.Common.Interfaces;
using FBChamp.Web.Common.VewModelsBuilders.Admin;
using FBChamp.Web.Common.VewModelsBuilders.Shared;

namespace FBChamp.Web.Common.VewModelsBuilders;

public class ViewModelBuildersFactory(IUnitOfWork unitOfWork)
    : IViewModelBuildersFactory
{
    private CoachAssignModelBuilder _coachAssignModelBuilder;
    private CoachCreateEditModelBuilder _coachCreateEditModelBuilder;
    private CoachesPageModelBuilder _coachesPageModelBuilder;
    private CoachViewModelBuilder _coachViewModelBuilder;
    private PlayerAssignModelBuilder _playerAssignModelBuilder;
    private PlayersPageModelBuilder _playersPageModelBuilder;
    private PlayerCreateEditModelBuilder _playerViewEditModelBuilder;
    private PlayerViewModelBuilder _playerViewModelBuilder;
    private TeamsPageModelBuilder _teamsPageModelBuilder;
    private TeamCreateEditModelBuilder _teamViewEditModelBuilder;

    public IViewModelBuilder GetBuilder(string builderType) =>
        builderType switch
        {
            "TeamsPageModel" => _teamsPageModelBuilder ??= new TeamsPageModelBuilder(unitOfWork),
            "TeamCreateEditModel" => _teamViewEditModelBuilder ??= new TeamCreateEditModelBuilder(unitOfWork, this),
            "PlayersPageModel" => _playersPageModelBuilder ??= new PlayersPageModelBuilder(unitOfWork),
            "PlayerViewModel" => _playerViewModelBuilder ??= new PlayerViewModelBuilder(unitOfWork),
            "PlayerAssignModel" => _playerAssignModelBuilder ??= new PlayerAssignModelBuilder(unitOfWork),
            "PlayerCreateEditModel" => _playerViewEditModelBuilder ??= new PlayerCreateEditModelBuilder(unitOfWork),
            "CoachesPageModel" => _coachesPageModelBuilder ??= new CoachesPageModelBuilder(unitOfWork, this),
            "CoachViewModel" => _coachViewModelBuilder ??= new CoachViewModelBuilder(unitOfWork),
            "CoachAssignModel" => _coachAssignModelBuilder ??= new CoachAssignModelBuilder(unitOfWork, this),
            "CoachCreateEditModel" => _coachCreateEditModelBuilder ??= new CoachCreateEditModelBuilder(unitOfWork),
            _ => null
        };
}