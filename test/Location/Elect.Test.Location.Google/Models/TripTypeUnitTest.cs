namespace Elect.Test.Location.Google.Models
{
    [TestClass]
    public class TripTypeUnitTest
    {
        [TestMethod]
        public void TripType_AZ_HasCorrectValue()
        {
            Assert.AreEqual(1, (int)TripType.AZ);
        }

        [TestMethod]
        public void TripType_RoundTrip_HasCorrectValue()
        {
            Assert.AreEqual(2, (int)TripType.RoundTrip);
        }

        [TestMethod]
        public void TripType_CanBeCastToInt()
        {
            TripType azType = TripType.AZ;
            TripType roundTripType = TripType.RoundTrip;
            
            Assert.AreEqual(1, (int)azType);
            Assert.AreEqual(2, (int)roundTripType);
        }

        [TestMethod]
        public void TripType_CanBeCastFromInt()
        {
            TripType azType = (TripType)1;
            TripType roundTripType = (TripType)2;
            
            Assert.AreEqual(TripType.AZ, azType);
            Assert.AreEqual(TripType.RoundTrip, roundTripType);
        }

        [TestMethod]
        public void TripType_ToString_WorksCorrectly()
        {
            Assert.AreEqual("AZ", TripType.AZ.ToString());
            Assert.AreEqual("RoundTrip", TripType.RoundTrip.ToString());
        }

        [TestMethod]
        public void TripType_EnumParse_WorksCorrectly()
        {
            TripType azType = Enum.Parse<TripType>("AZ");
            TripType roundTripType = Enum.Parse<TripType>("RoundTrip");
            
            Assert.AreEqual(TripType.AZ, azType);
            Assert.AreEqual(TripType.RoundTrip, roundTripType);
        }
    }
}