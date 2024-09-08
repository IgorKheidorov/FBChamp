using FBChamp.Core.DataValidator;
using FBChamp.Core.Entities;
using FBChamp.Core.UnitOfWork;

namespace FBChamp.Infrastructure.Validators;

public class CRUDDataValidator : IValidateEntity
{
    private readonly IUnitOfWork _unitOfWork;

    private readonly List<IValidateEntity> _validators = new();

    public CRUDDataValidator(IUnitOfWork unitOfWork)
    {
        _unitOfWork = unitOfWork;
        _validators.Add(new TeamValidator());
        _validators.Add(new PlayerValidator(unitOfWork));
        _validators.Add(new PlayerAssignmentInfoValidator(_unitOfWork));
        _validators.Add(new LeagueValidators(_unitOfWork));
        _validators.Add(new CoachValidator());
        _validators.Add(new TeamAssignmentInfoValidator(_unitOfWork));
        _validators.Add(new StadiumValidator());
        _validators.Add(new CoachAssignmentInfoValidator(_unitOfWork));
    }

    public Type GetValidatedType() => typeof(CRUDDataValidator);

    public CRUDResult Validate(Entity entity) =>
        _validators.Where(x => x.GetValidatedType() == entity.GetType()).FirstOrDefault()?.Validate(entity) ??
        CRUDResult.Success;
}