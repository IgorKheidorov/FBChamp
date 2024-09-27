using FBChamp.Core.DALModels;
using FBChamp.Core.Entities.Soccer;
using FBChamp.Core.UnitOfWork;
using FBChamp.Web.Areas.Admin.Controllers.Models.Leagues;

namespace FBChamp.Web.Common.EntityBuilders.Admin;

public class LeagueBuilder(IUnitOfWork unitOfWork)
    : EntityBuilder(unitOfWork)
{
    public override CRUDResult CreateUpdate(EntityModel viewModel)
    {
        if(viewModel is not LegueCreateEditModel leagueModel)
        {
            return CRUDResult.Failed;
        }

        var photo = Convert.FromBase64String(leagueModel.PhotoString ?? "");

        if (leagueModel?.PhotoFile != null && leagueModel?.PhotoFile.Length > 0)
        {
            var stream = new MemoryStream();
            leagueModel.PhotoFile.CopyToAsync(stream).Wait();
            photo = stream.ToArray();
        }

        return UnitOfWork.Commit(new League(leagueModel.Id, leagueModel.FullName,
                                           photo, leagueModel.NumberOfTeams, 
                                           leagueModel.SeasonStartDate, leagueModel.SeasonFinishDate,
                                           leagueModel.Description));
    }

    public override CRUDResult Delete(Guid id) => UnitOfWork.Remove(id, typeof(League));
}
