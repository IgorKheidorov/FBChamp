using FBChamp.Core.DataValidator;
using FBChamp.Core.Entities;
using FBChamp.Core.UnitOfWork;

namespace FBChamp.Infrastructure.Validators;

public class CRUDDataValidator : IValidateEntity
{
    IUnitOfWork _unitOfWork;

    List<IValidateEntity> _validators = new List<IValidateEntity>();

    public CRUDDataValidator(IUnitOfWork unitOfWork)
    {
        _unitOfWork = unitOfWork;
        _validators.Add(new TeamValidator());
        _validators.Add(new PlayerValidator());
        _validators.Add(new PlayerAssignmentInfoValidator(_unitOfWork));
    }

    public Type GetValidatedType() => typeof(CRUDDataValidator);

    public CRUDResult Validate(Entity entity)=>
      _validators.Where(x => x.GetValidatedType() == entity.GetType()).FirstOrDefault()?.Validate(entity) ?? CRUDResult.Success;
    
}
