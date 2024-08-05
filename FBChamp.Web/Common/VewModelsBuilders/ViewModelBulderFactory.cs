using FBChamp.Core.UnitOfWork;
using FBChamp.Web.Common.Interfaces;
using FBChamp.Web.Common.VewModelsBuilders.Admin;
using FBChamp.Web.Common.VewModelsBuilders.Shared;

namespace FBChamp.Web.Common.VewModelsBuilders
{
    public class ViewModelBuldersFactory : IViewModelBuildersFactory
    {
        private readonly IUnitOfWork _unitOfWorks;
        private PlayerCreateEditModelBuilder _playerViewEditModelBuilder;
        private TeamsPageModelBuilder _teamsPageModelBuilder;
        private TeamCreateEditModelBuilder _teamViewEditModelBuilder;
        private PlayersPageModelBuilder _playersPageModelBuilder;
        private PlayerViewModelBuilder _playerViewModelBuilder;
        private PlayerAssignModelBuilder _playerAssighModelBuilder;
        private CoachesPageModelBuilder _coachesPageModelBuilder;
        private CoachViewModelBuilder _coachViewModelBuilder;
        private CoachAssignModelBuilder _coachAssighModelBuilder;
        private CoachCreateEditModelBuilder _coachCreateEditModelBuilder;

        public ViewModelBuldersFactory(IUnitOfWork unitOfWork)
        {
            _unitOfWorks = unitOfWork;
        }
        public IViewModelBuilder GetBuilder(string builderType) => builderType switch
        {
            "TeamsPageModel" => _teamsPageModelBuilder ??= new TeamsPageModelBuilder(_unitOfWorks),
            "TeamCreateEditModel" => _teamViewEditModelBuilder ??= new TeamCreateEditModelBuilder(_unitOfWorks, this),
            "PlayersPageModel" => _playersPageModelBuilder ??= new PlayersPageModelBuilder(_unitOfWorks),
            "PlayerViewModel" => _playerViewModelBuilder ??= new PlayerViewModelBuilder(_unitOfWorks),
            "PlayerAssignModel" => _playerAssighModelBuilder ??= new PlayerAssignModelBuilder(_unitOfWorks),
            "PlayerCreateEditModel" => _playerViewEditModelBuilder ??= new PlayerCreateEditModelBuilder(_unitOfWorks),
            "CoachesPageModel" => _coachesPageModelBuilder ??= new CoachesPageModelBuilder(_unitOfWorks, this),
            "CoachViewModel" => _coachViewModelBuilder ??= new CoachViewModelBuilder(_unitOfWorks),
            "CoachAssignModel" => _coachAssighModelBuilder ??= new CoachAssignModelBuilder(_unitOfWorks, this),
            "CoachCreateEditModel" => _coachCreateEditModelBuilder ??= new CoachCreateEditModelBuilder(_unitOfWorks),
            _ => null
        };
    }
}