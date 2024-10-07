using FBChamp.Core.DALModels;
using FBChamp.Core.Entities.Soccer;
using FBChamp.Core.UnitOfWork;
using FBChamp.Web.Areas.Admin.Controllers.Models.Matches;

namespace FBChamp.Web.Common.EntityBuilders.Admin;

public class MatchBuilder(IUnitOfWork unitOfWork)
    : EntityBuilder(unitOfWork)
{
    public override CRUDResult CreateUpdate(EntityModel viewModel)
    {
        if (viewModel is not MatchCreateEditModel matchModel)
        {
            return CRUDResult.Failed;
        }

        var hostTeamId = UnitOfWork.GetTeamIdByName(matchModel.HostTeamName);
        var guestTeamId = UnitOfWork.GetTeamIdByName(matchModel.GuestTeamName);
        var stadiumId = UnitOfWork.GetStadiumIdByName(matchModel.StadiumName);

        return UnitOfWork.Commit(new Match(matchModel.Id, stadiumId, matchModel.LeagueId,
                                            matchModel.Status, hostTeamId, guestTeamId,
                                            matchModel.StartTimeOfMatch, matchModel.FinishTimeOfMatch));
    }

    public override CRUDResult Delete(Guid id) => UnitOfWork.Remove(id, typeof(Match));
}