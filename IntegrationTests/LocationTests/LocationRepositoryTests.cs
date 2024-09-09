using FBChamp.Core.Entities.Soccer;
using FBChamp.Infrastructure;

namespace IntegrationTests.LocationTests
{
    [TestClass]
    public class LocationRepositoryTests
    {

        [TestMethod]
        public void Test_Location_Inflation_From_Json()
        {
            var unitOfWork = new UnitOfWork();
            Assert.IsTrue(unitOfWork.Exists(Guid.Parse("3b45cb09-8cc4-4f5f-bbb1-4a0622ff0a1a"), typeof(Location)), "Location repo not inflated correctly");
        }
    }
}
