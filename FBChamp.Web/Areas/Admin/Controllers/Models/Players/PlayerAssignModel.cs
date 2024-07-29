using FBChamp.Core.DALModels;
using Microsoft.AspNetCore.Mvc;
using System.ComponentModel.DataAnnotations;

namespace FBChamp.Web.Areas.Admin.Controllers.Models.Players;

public class PlayerAssignModel : EntityModel
{
    [HiddenInput][Display(Name = "Player")] public Guid Id { get; set; }
    [HiddenInput][Display(Name = "Team")] public Guid TeamId { get; set; }

    
    [Display(Name = "Number")]
    [Required]
    [Range(1, 99, ErrorMessage = "Please enter a valid playing number")]
    public uint PlayingNumber { get; set; }

    public PlayerModel PlayerModel { get; set; }

    public override string FullName => PlayerModel?.FullName ?? String.Empty;

    public PlayerAssignModel() { }
    public PlayerAssignModel(PlayerModel player, Guid teamId = default, uint playingNumber = 10)
    {
        Id = player.Player.Id;
        PlayerModel = player;
        TeamId = teamId;        
        PlayingNumber = playingNumber;        
    }
}
