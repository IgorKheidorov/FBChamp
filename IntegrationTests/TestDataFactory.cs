using FBChamp.Core.Entities.Soccer;
using FBChamp.Core.UnitOfWork;

namespace IntegrationTests;

public static class TestDataFactory
{
    public static League? League1 { get; private set; }
    public static League? League2 { get; private set; }

    public static void SeedEntity(IUnitOfWork _unitOfWork)
    {
        var photo = new byte[] { 137, 80, 78, 71, 13, 10, 26, 10 };

        League1 = new League(Guid.NewGuid(), "League1", photo, 5, DateTime.Now, DateTime.Now.AddMonths(6), "Description1");
        League2 = new League(Guid.NewGuid(), "League2", photo, 8, DateTime.Now, DateTime.Now.AddMonths(6), "Description2");

        _unitOfWork.Commit(League1);
        _unitOfWork.Commit(League2);
    }
}
