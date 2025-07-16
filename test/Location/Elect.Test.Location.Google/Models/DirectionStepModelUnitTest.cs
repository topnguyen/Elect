namespace Elect.Test.Location.Google.Models
{
    [TestClass]
    public class DirectionStepModelUnitTest
    {
        [TestMethod]
        public void Index_GetSet_WorksCorrectly()
        {
            var step = new DirectionStepModel();
            var index = 5;
            
            step.Index = index;
            
            Assert.AreEqual(index, step.Index);
        }

        [TestMethod]
        public void Description_GetSet_WorksCorrectly()
        {
            var step = new DirectionStepModel();
            var description = "Turn left onto Main Street";
            
            step.Description = description;
            
            Assert.AreEqual(description, step.Description);
        }

        [TestMethod]
        public void Distance_GetSet_WorksCorrectly()
        {
            var step = new DirectionStepModel();
            var distance = 1500.75;
            
            step.Distance = distance;
            
            Assert.AreEqual(distance, step.Distance);
        }

        [TestMethod]
        public void DistanceText_GetSet_WorksCorrectly()
        {
            var step = new DirectionStepModel();
            var distanceText = "1.5 km";
            
            step.DistanceText = distanceText;
            
            Assert.AreEqual(distanceText, step.DistanceText);
        }

        [TestMethod]
        public void Duration_GetSet_WorksCorrectly()
        {
            var step = new DirectionStepModel();
            var duration = 180.5;
            
            step.Duration = duration;
            
            Assert.AreEqual(duration, step.Duration);
        }

        [TestMethod]
        public void DurationText_GetSet_WorksCorrectly()
        {
            var step = new DirectionStepModel();
            var durationText = "3 mins";
            
            step.DurationText = durationText;
            
            Assert.AreEqual(durationText, step.DurationText);
        }

        [TestMethod]
        public void DefaultValues_AreCorrect()
        {
            var step = new DirectionStepModel();
            
            Assert.AreEqual(0, step.Index);
            Assert.IsNull(step.Description);
            Assert.AreEqual(0.0, step.Distance);
            Assert.IsNull(step.DistanceText);
            Assert.AreEqual(0.0, step.Duration);
            Assert.IsNull(step.DurationText);
        }
    }
}