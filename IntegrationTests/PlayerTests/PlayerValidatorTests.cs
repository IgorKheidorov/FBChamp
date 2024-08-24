using FBChamp.Core.Entities.Soccer;
using FBChamp.Core.UnitOfWork;
using FBChamp.Infrastructure;

namespace IntegrationTests.PlayerTests
{
    [TestClass]
    public class PlayerValidatorTests
    {        
        [TestInitialize]
        public void Initialize()
        {
            Infrastructure.CleanUp();            
        }

        [TestMethod]
        [DataRow()]
        public void PlayerInvalidHeightTest() 
        {
            UnitOfWork unitOfWork = new UnitOfWork();
            var positionId = unitOfWork.GetAllPlayerPositions().First().Id;
            Player player = new Player(Guid.NewGuid(), "Name", DateTime.Now, 251, positionId, new byte[0], null);
            var actualResult = unitOfWork.Commit(player);
            Assert.AreEqual(CRUDResult.EntityValidationFailed, actualResult);
        }

        [TestMethod]
        public void PlayerValidHeightTest()
        {
            UnitOfWork unitOfWork = new UnitOfWork();
            Player player = new Player(new Guid(), "Name", DateTime.Now, 249, new Guid(), new byte[0], null);
            var actualResult = unitOfWork.Commit(player);
            Assert.AreEqual(CRUDResult.Success, actualResult);
        }
    }
}
