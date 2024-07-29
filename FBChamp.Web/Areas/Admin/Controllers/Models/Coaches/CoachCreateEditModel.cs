using FBChamp.Core.DALModels;
using Microsoft.AspNetCore.Mvc;
using System.ComponentModel.DataAnnotations;

namespace FBChamp.Web.Areas.Admin.Controllers.Models.Coaches;

public class CoachCreateEditModel: EntityModel
{
    [HiddenInput][Display(Name = "Coach")] public Guid Id { get; set; }
    [HiddenInput] public string PhotoString { get; set; }

    [Required]
    public string Name { get; set; }

    [Required]
    [DataType(DataType.Date)]
    public DateTime BirthDate { get; set; }

    [DataType(DataType.Upload)]
    public IFormFile PhotoFile { get; set; }

    public string Description { get; set; }

    public byte[] Photo { get; set; }

    public override string FullName => Name;

    public CoachCreateEditModel()
    {
        BirthDate = new DateTime(1980, 1, 1);
    }

    public CoachCreateEditModel(CoachModel coach)
    {
        ArgumentNullException.ThrowIfNull(coach);
        Id = coach.Coach.Id;
        Name = coach.Coach.FullName;
        Description = coach.Coach.Description;
        BirthDate = coach.Coach.BirthDate;
        Photo = coach.Coach.Photo;
        PhotoString = coach.PhotoString;
    }
}
