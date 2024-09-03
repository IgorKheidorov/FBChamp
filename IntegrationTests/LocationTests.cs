using FBChamp.Core.DALModels;
using FBChamp.Core.Entities.Soccer;
using FBChamp.Core.Repositories.Membership;
using FBChamp.Core.UnitOfWork;
using FBChamp.Infrastructure;

namespace IntegrationTests
{
    public class LocationTests
    {
        [TestClass]
        public class LocationIntegrationTests
        {
            private UnitOfWork _unitOfWork = new UnitOfWork();

            [TestMethod]
            public void TestAddOrUpdateLocationAssignmentInfo()
            {
                var country = new Country { Id = Guid.NewGuid(), Name = "Test Country" };
                var locationAssignmentInfo = new LocationAssignmentInfo(Guid.NewGuid(), "Test City", country.Id, country);

                var result = _unitOfWork.Commit(locationAssignmentInfo);

                Assert.AreEqual(CRUDResult.Success, result);
                var retrievedLocation = _unitOfWork.GetLocationModel(locationAssignmentInfo.Id);
                Assert.IsNotNull(retrievedLocation);
                Assert.AreEqual("Test City", retrievedLocation.Location.City);
            }

            [TestMethod]
            public void TestGetAllLocationModels()
            {
                // Arrange
                var country = new Country { Id = Guid.NewGuid(), Name = "Test Country" };
                var locationAssignmentInfo1 = new LocationAssignmentInfo(Guid.NewGuid(), "Test City 1", country.Id, country);
                var locationAssignmentInfo2 = new LocationAssignmentInfo(Guid.NewGuid(), "Test City 2", country.Id, country);
                _unitOfWork.Commit(locationAssignmentInfo1);
                _unitOfWork.Commit(locationAssignmentInfo2);

                List<LocationModel> allLocations = _unitOfWork.GetAllLocationModels();

                Assert.AreEqual(2, allLocations.Count);
            }

            [TestMethod]
            public void TestRemoveLocationAssignmentInfo()
            {
                var country = new Country { Id = Guid.NewGuid(), Name = "Test Country" };
                var locationAssignmentInfo = new LocationAssignmentInfo(Guid.NewGuid(), "Test City", country.Id, country);
                _unitOfWork.Commit(locationAssignmentInfo);

                var removeResult = _unitOfWork.Remove(locationAssignmentInfo.Id, typeof(LocationAssignmentInfo));

                Assert.AreEqual(CRUDResult.Success, removeResult);
                var retrievedLocation = _unitOfWork.GetLocationModel(locationAssignmentInfo.Id);
                Assert.IsNull(retrievedLocation);
            }

            [TestCleanup]
            public void Cleanup()
            {
                _unitOfWork.Dispose();
            }
        }
    }
}
