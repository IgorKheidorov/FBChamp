using FBChamp.Core.Entities.Soccer;
using FBChamp.Core.UnitOfWork;
using FBChamp.Infrastructure.Validators;
using FBChamp.Infrastructure;

namespace IntegrationTests.PlayerMatchAssignmentTests
{
    [TestClass]
    public class PlayerMatchAsssignmentTest
    {

        [TestInitialize]
        public void Initialize()
        {
            Infrastructure.CleanUp();
        }

        [TestMethod]
        public void ValidatePlayerMatchAssignmentTest()
        {
            var unitOfWork = new UnitOfWork();
            var validator = new PlayerMatchAssignmentValidator(unitOfWork);
            var playerMatchAssignment = new PlayerMatchAssignment(Guid.NewGuid(), Guid.NewGuid(), DateTime.Now, DateTime.Now.AddHours(1), "Forward");

            var result = validator.Validate(playerMatchAssignment);

            Assert.AreEqual(CRUDResult.Success, result);
        }

        [TestMethod]
        public void ValidatePlayerMatchAssignmentInvalidTest()
        {
            var unitOfWork = new UnitOfWork();
            var validator = new PlayerMatchAssignmentValidator(unitOfWork);
            var playerMatchAssignment = new PlayerMatchAssignment(Guid.NewGuid(), Guid.NewGuid(), DateTime.Now.AddHours(1), DateTime.Now, "Forward");

            var result = validator.Validate(playerMatchAssignment);

            Assert.AreEqual(CRUDResult.EntityValidationFailed, result);
        }


        [TestMethod]
        public void AddPlayerMatchAssignmentTest()
        {
            var unitOfWork = new UnitOfWork();
            var playerMatchAssignment = new PlayerMatchAssignment(Guid.NewGuid(), Guid.NewGuid(), DateTime.Now, DateTime.Now.AddHours(1), "Forward");

            unitOfWork.Commit(playerMatchAssignment);

            var result = unitOfWork.GetPlayerMatchAssignmentModel(playerMatchAssignment.Id);

            Assert.IsNotNull(result);
            Assert.AreEqual(playerMatchAssignment.Id, result.PlayerMatchAssignment.Id);
        }

        [TestMethod]
        public void UpdatePlayerMatchAssignmentTest()
        {
            var unitOfWork = new UnitOfWork();
            var playerMatchAssignment = new PlayerMatchAssignment(Guid.NewGuid(), Guid.NewGuid(), DateTime.Now, DateTime.Now.AddHours(1), "Forward");

            unitOfWork.Commit(playerMatchAssignment);

            playerMatchAssignment.Role = "Defender";
            unitOfWork.Commit(playerMatchAssignment);

            var result = unitOfWork.GetPlayerMatchAssignmentModel(playerMatchAssignment.Id);

            Assert.IsNotNull(result);
            Assert.AreEqual("Defender", result.Role);
        }

        [TestMethod]
        public void RemovePlayerMatchAssignmentTest()
        {
            var unitOfWork = new UnitOfWork();
            var playerMatchAssignment = new PlayerMatchAssignment(Guid.NewGuid(), Guid.NewGuid(), DateTime.Now, DateTime.Now.AddHours(1), "Forward");

            unitOfWork.Commit(playerMatchAssignment);

            unitOfWork.Remove(playerMatchAssignment.Id, typeof(PlayerMatchAssignment));

            var result = unitOfWork.GetPlayerMatchAssignmentModel(playerMatchAssignment.Id);

            Assert.IsNull(result);
        }   
    }
}
