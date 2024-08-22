using System.ComponentModel.DataAnnotations;
using FBChamp.Core.DALModels;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;

namespace FBChamp.Web.Areas.Admin.Controllers.Models.Players;

public class PlayerCreateEditModel : EntityModel
{
    [HiddenInput]
    [Display(Name = "Player")]
    public Guid Id { get; set; }

    [HiddenInput]
    public string PhotoString { get; set; }

    [Required]
    public string Name { get; set; }

    [Required]
    [DataType(DataType.Date)]
    public DateTime BirthDate { get; set; }

    [Required]
    [Range(0.0, 250, ErrorMessage = "Please enter a valid height")]
    public float? Height { get; set; }

    [Required]
    public Guid PositionId { get; set; }

    public string Description { get; set; }

    [DataType(DataType.Upload)]
    public IFormFile PhotoFile { get; set; }

    public SelectList Positions { get; set; }

    public override string FullName => Name;

    public PlayerCreateEditModel()
    {
        BirthDate = new DateTime(1980, 1, 1);
    }

    public PlayerCreateEditModel(PlayerModel player)
    {
        ArgumentNullException.ThrowIfNull(player);
        Id = player.Player.Id;
        Name = player.Player.FullName;
        PositionId = player.Player.PositionId;
        Description = player.Player.Description;
        BirthDate = player.Player.BirthDate;
        Height = player.Player.Height;
        PhotoString = player.PhotoString;
    }

    public override IEnumerable<(string, string)> GetInformation() =>
        throw new NotImplementedException();
}