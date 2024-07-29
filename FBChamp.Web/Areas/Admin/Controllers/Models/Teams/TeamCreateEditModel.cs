using FBChamp.Core.DALModels;
using FBChamp.Core.Entities.Socker;
using FBChamp.Web.Areas.Admin.Controllers.Models.Coaches;
using FBChamp.Web.Areas.Admin.Controllers.Models.Players;
using Microsoft.AspNetCore.Mvc;
using System.ComponentModel.DataAnnotations;

namespace FBChamp.Web.Areas.Admin.Controllers.Models.Teams;

public class TeamCreateEditModel : EntityModel
{
    [HiddenInput][Display(Name = "TeamId")] public Guid Id { get; set; }
    [HiddenInput] public string PhotoString { get; set; }

    [Required]
    public string Name { get; set; }

    [DataType(DataType.Upload)]
    public IFormFile PhotoFile { get; set; }

    public byte[] Photo { get; set; }

    public string Description { get; set; }

    public EntityPageModel<PlayerModel> PlayersPageModel { get; set; }
    public EntityPageModel<CoachModel> CoachesPageModel { get; set; }

    public override string FullName => Name;

    public TeamCreateEditModel() 
    {
        Id = Guid.NewGuid();
        PlayersPageModel = new EntityPageModel<PlayerModel>();
        CoachesPageModel = new EntityPageModel<CoachModel>();
    }

    public TeamCreateEditModel(TeamModel team, EntityPageModel<PlayerModel> playersPageModel, EntityPageModel<CoachModel> coachesPageModel)
    {
        ArgumentNullException.ThrowIfNull(team);
        Id = team.Team.Id;
        Name = team.Team.Name;
        Description = team.Team.Description;
        PhotoString = team.PhotoString;
        PlayersPageModel = playersPageModel;
        CoachesPageModel = coachesPageModel;
    }
}
