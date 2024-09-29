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

        return UnitOfWork.Commit(new Match(matchModel.Id, Guid.NewGuid(), Guid.NewGuid(),
                                            matchModel.Status, matchModel.HostTeamId, matchModel.GuestTeamId,
                                            matchModel.StartTimeOfMatch, matchModel.FinishTimeOfMatch));
    }

    public override CRUDResult Delete(Guid id) => UnitOfWork.Remove(id, typeof(Match));
}