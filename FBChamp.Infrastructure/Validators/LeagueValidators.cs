using FBChamp.Core.DataValidator;
using FBChamp.Core.Entities;
using FBChamp.Core.Entities.Soccer;
using FBChamp.Core.UnitOfWork;
using FBChamp.Infrastructure.BusinessRules;

namespace FBChamp.Infrastructure.Validators;

public class LeagueValidators : IValidateEntity
{
    IUnitOfWork _unitOfWork;

    public LeagueValidators(IUnitOfWork unitOfWork)
    {
        _unitOfWork = unitOfWork;
    }

    public Type GetValidatedType() => typeof(League);

    public CRUDResult Validate(Entity entity) =>
    entity switch
    {
        League league when ValidateProperties(league) => CRUDResult.Success,
        League _ => CRUDResult.EntityValidationFailed,
        _ => CRUDResult.InvalidOperation
    };

    private bool ValidateProperties(League league) =>
     ValidateNameLength(league.FullName) &&
     ValidateSeasonDates(league.SeasonStartDate, league.SeasonFinishDate) &&
     !ValidateUniqueLeagueName(league.FullName);

    private bool ValidateNameLength(string name) =>
    name.Length < DataRestrictions.NameLengthMax;

    private bool ValidateSeasonDates(DateTime startDate, DateTime finishDate) =>
        startDate < finishDate;

    private bool ValidateUniqueLeagueName(string name) =>
        _unitOfWork.GetAllLeagueModels()
        .Any(l => l.FullName.Equals(name, StringComparison.OrdinalIgnoreCase));
}
