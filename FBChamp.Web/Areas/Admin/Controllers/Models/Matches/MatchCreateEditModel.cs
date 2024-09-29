using FBChamp.Core.DALModels;
using FBChamp.Core.Entities.Soccer.Enums;
using Microsoft.AspNetCore.Mvc;
using System.ComponentModel.DataAnnotations;

namespace FBChamp.Web.Areas.Admin.Controllers.Models.Matches;

public class MatchCreateEditModel : EntityModel
{
    [HiddenInput]
    [Display(Name = "MatchId")]
    public Guid Id { get; set; }

    [Required]
    public MatchStatus Status { get; set; }

    [Required]
    public Guid HostTeamId { get; set; }

    [Required]
    public Guid GuestTeamId { get; set; }

    [Required]
    [DataType(DataType.DateTime)]
    public DateTime StartTimeOfMatch { get; set; }

    [DataType(DataType.DateTime)]
    public DateTime FinishTimeOfMatch { get; set; }

    public MatchCreateEditModel()
    {
        Id = Guid.NewGuid();
        StartTimeOfMatch = new DateTime(2000, 1, 1);
        FinishTimeOfMatch = new DateTime(2000, 1, 1);
    }

    public MatchCreateEditModel(MatchModel match)
    {
        ArgumentNullException.ThrowIfNull(match);
        Id = match.Match.Id;
        Status = match.Match.Status;
        HostTeamId = match.Match.HostTeamId;
        GuestTeamId = match.Match.GuestTeamId;
        StartTimeOfMatch = match.Match.StartTimeOfMatch;
        FinishTimeOfMatch = match.Match.FinishTimeOfMatch;
    }
}