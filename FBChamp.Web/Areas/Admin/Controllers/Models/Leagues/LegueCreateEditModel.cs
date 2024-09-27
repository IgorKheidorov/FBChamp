using FBChamp.Core.DALModels;
using Microsoft.AspNetCore.Mvc;
using System.ComponentModel.DataAnnotations;

namespace FBChamp.Web.Areas.Admin.Controllers.Models.Leagues;

public class LegueCreateEditModel : EntityModel
{
    [HiddenInput]
    [Display(Name = "LeagueId")]
    public Guid Id { get; set; }

    [HiddenInput]
    public string PhotoString { get; set; }

    [DataType(DataType.Upload)]
    public IFormFile PhotoFile { get; set; }

    public byte[] Photo { get; set; }

    [Required]
    public string Name { get; set; }

    [Required]
    public int NumberOfTeams { get; set; }

    [Required]
    public DateTime SeasonStartDate { get; set; }

    [Required]
    public DateTime SeasonFinishDate { get; set; }

    public string Description { get; set; }

    public override string FullName => Name;

    public EntityPageModel<TeamModel> TeamsPageModel { get; set; }

    public LegueCreateEditModel()
    {
        Id = Guid.NewGuid();
        TeamsPageModel = new EntityPageModel<TeamModel>();
    }

    public LegueCreateEditModel(LeagueModel league, EntityPageModel<TeamModel> teamsPageModel)
    {
        ArgumentNullException.ThrowIfNull(league);
        Id = league.League.Id;
        PhotoString = league.PhotoString;
        Name = league.League.FullName;
        NumberOfTeams = league.League.NumberOfTeams;
        SeasonStartDate = league.League.SeasonStartDate;
        SeasonFinishDate = league.League.SeasonFinishDate;
        Description = league.League.Description;
        TeamsPageModel = teamsPageModel;
    }
}
