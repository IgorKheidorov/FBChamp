using System.ComponentModel.DataAnnotations;
using FBChamp.Core.DALModels;
using Microsoft.AspNetCore.Mvc;

namespace FBChamp.Web.Areas.Admin.Controllers.Models.Coaches;

public class CoachAssignModel : EntityModel
{
    [HiddenInput]
    [Display(Name = "Coach")]
    public Guid Id { get; set; }

    [HiddenInput]
    [Display(Name = "Team")]
    public Guid TeamId { get; set; }

    public CoachModel CoachModel { get; }

    [Required]
    public string Role { get; set; }

    public override string FullName => CoachModel?.FullName ?? string.Empty;

    public CoachAssignModel()
    {
    }

    public CoachAssignModel(CoachModel viewModel, Guid teamId = default)
    {
        ArgumentNullException.ThrowIfNull(viewModel);
        TeamId = teamId;
        CoachModel = viewModel;
    }

    public override IEnumerable<(string, string)> GetInformation() => CoachModel.GetInformation();
}