namespace FBChamp.Core.UnitOfWork;

public enum CRUDResult
{
    Success,
    EntityValidationFailed,
    EntityAlreadyExists,
    InvalidOperation,
    ObjectDoesNotExists,
    Failed
}