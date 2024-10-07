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
    public string HostTeamName { get; set; }

    [Required]
    public string GuestTeamName { get; set; }

    [Required]
    public string StadiumName { get; set; }

    [Required]
    public Guid LeagueId { get; set; }

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
        HostTeamName = match.HostTeam.FullName;
        GuestTeamName = match.GuestTeam.FullName;
        StadiumName = match.Stadium.FullName;
        LeagueId = match.Match.LeagueId;
        StartTimeOfMatch = match.Match.StartTimeOfMatch;
        FinishTimeOfMatch = match.Match.FinishTimeOfMatch;
    }
}