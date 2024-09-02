using FBChamp.Core.Entities.Soccer;
using FBChamp.Core.UnitOfWork;
using FBChamp.Infrastructure;

namespace IntegrationTests.StadiumTests;

[TestClass]
public class StadiumValidatorTests
{
    [TestInitialize]
    public void Initialize()
    {
        Infrastructure.CleanUp();
    }

    [TestMethod]
    public void StadiumInvalidNameNullTest()
    {
        UnitOfWork unitOfWork = new UnitOfWork();
        Stadium stadium = new Stadium(new Guid(), null, new Guid());

        var actualResult = unitOfWork.Commit(stadium);
        Assert.AreEqual(CRUDResult.EntityValidationFailed, actualResult);
    }
}