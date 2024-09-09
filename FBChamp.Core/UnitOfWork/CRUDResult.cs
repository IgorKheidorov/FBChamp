namespace FBChamp.Core.UnitOfWork;

public enum CRUDResult
{
    Success,
    EntityValidationFailed,
    InvalidOperation,
    ObjectDoesNotExists,
    Failed
}