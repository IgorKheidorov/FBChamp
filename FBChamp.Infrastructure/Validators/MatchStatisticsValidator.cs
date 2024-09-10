using FBChamp.Core.DataValidator;
using FBChamp.Core.Entities;
using FBChamp.Core.Entities.Soccer;
using FBChamp.Core.UnitOfWork;

namespace FBChamp.Infrastructure.Validators;

public class MatchStatisticsValidator : IValidateEntity
{
    private IUnitOfWork _unitOfWork;

    public MatchStatisticsValidator(IUnitOfWork unitOfWork)
    {
        _unitOfWork = unitOfWork;
    }

    public Type GetValidatedType() => typeof(MatchStatistics);

    public CRUDResult Validate(Entity entity) =>
       entity is not MatchStatistics matchStatistics ? CRUDResult.InvalidOperation :
       !_unitOfWork.Exists(matchStatistics.MatchId, typeof(Match)) || matchStatistics.NumberOfWatchers < 0 ? CRUDResult.EntityValidationFailed :
       CRUDResult.Success;
}
