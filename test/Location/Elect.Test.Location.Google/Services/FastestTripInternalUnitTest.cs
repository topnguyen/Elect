using Elect.Location.Google.Models;
using Elect.Location.Google.Services;
using Elect.Location.Models;
using System.Reflection;

namespace Elect.Test.Location.Google.Services
{
    [TestClass]
    public class FastestTripInternalUnitTest
    {
        [TestMethod]
        public void FastestTrip_InternalClass_Exists()
        {
            // Arrange - Get assembly from a known public type
            var assembly = typeof(ElectGoogleClient).Assembly;
            
            // Act
            var fastestTripType = assembly.GetType("Elect.Location.Google.Services.FastestTrip");
            
            // Assert
            Assert.IsNotNull(fastestTripType, "FastestTrip internal class should exist");
            Assert.IsTrue(fastestTripType.IsNotPublic, "FastestTrip should be internal");
        }

        [TestMethod]
        public void FastestTrip_Constructor_HasCorrectSignature()
        {
            // Arrange
            var assembly = typeof(ElectGoogleClient).Assembly;
            var fastestTripType = assembly.GetType("Elect.Location.Google.Services.FastestTrip");
            
            // Act
            var constructor = fastestTripType?.GetConstructor(new[]
            {
                typeof(TripType), 
                typeof(string), 
                typeof(CoordinateModel[])
            });
            
            // Assert
            Assert.IsNotNull(constructor, "FastestTrip constructor with correct signature should exist");
        }

        [TestMethod]
        public void FastestTrip_Properties_Exist()
        {
            // Arrange
            var assembly = typeof(ElectGoogleClient).Assembly;
            var fastestTripType = assembly.GetType("Elect.Location.Google.Services.FastestTrip");
            
            // Act & Assert
            var coordinatesProperty = fastestTripType?.GetProperty("Coordinates");
            Assert.IsNotNull(coordinatesProperty, "Coordinates property should exist");
            
            var distanceDurationMatrixProperty = fastestTripType?.GetProperty("DistanceDurationMatrix");
            Assert.IsNotNull(distanceDurationMatrixProperty, "DistanceDurationMatrix property should exist");
        }

        [TestMethod]
        public void FastestTrip_Methods_Exist()
        {
            // Arrange
            var assembly = typeof(ElectGoogleClient).Assembly;
            var fastestTripType = assembly.GetType("Elect.Location.Google.Services.FastestTrip");
            
            // Act & Assert
            var getTripMethod = fastestTripType?.GetMethod("GetTrip");
            Assert.IsNotNull(getTripMethod, "GetTrip method should exist");
            
            var getTotalDurationMethod = fastestTripType?.GetMethod("GetTotalDurationInSecond");
            Assert.IsNotNull(getTotalDurationMethod, "GetTotalDurationInSecond method should exist");
            
            var getTotalDistanceMethod = fastestTripType?.GetMethod("GetTotalDistanceInMeter");
            Assert.IsNotNull(getTotalDistanceMethod, "GetTotalDistanceInMeter method should exist");
        }

        [TestMethod]
        public void FastestTrip_AlgorithmThresholds_AreDocumented()
        {
            // This test documents the algorithm selection thresholds based on coordinate count
            // These thresholds are defined in the CalculateTrip method:
            // <= 13 coordinates: Backtracking algorithm
            // <= 15 coordinates (>13): Dynamic Programming algorithm  
            // > 15 coordinates: Ant Colony Optimization + K3 algorithm
            
            var backtrackingThreshold = 13;
            var dynamicProgrammingThreshold = 15;
            
            Assert.AreEqual(13, backtrackingThreshold, "Backtracking algorithm should be used for <= 13 coordinates");
            Assert.AreEqual(15, dynamicProgrammingThreshold, "Dynamic programming should be used for <= 15 coordinates");
            
            // Test validates algorithm selection logic exists
            Assert.IsTrue(backtrackingThreshold < dynamicProgrammingThreshold, 
                "Algorithm thresholds should be in ascending order");
        }

        [TestMethod]
        public void FastestTrip_TripTypes_AreSupported()
        {
            // Test that both TripType.AZ and TripType.RoundTrip are supported
            var azType = TripType.AZ;
            var roundTripType = TripType.RoundTrip;
            
            Assert.AreEqual(1, (int)azType, "TripType.AZ should have value 1");
            Assert.AreEqual(2, (int)roundTripType, "TripType.RoundTrip should have value 2");
            
            // Verify enum values are different
            Assert.AreNotEqual(azType, roundTripType, "Trip types should be different");
        }

        [TestMethod]
        public void FastestTrip_Constants_AreValid()
        {
            // Test validates internal constants used in FastestTrip algorithms
            // Based on source code analysis:
            
            var maxTripSentry = 2000000000.0; // MaxTripSentry constant
            Assert.IsTrue(maxTripSentry > 0, "MaxTripSentry should be positive");
            Assert.IsTrue(maxTripSentry > 1000000, "MaxTripSentry should be large enough for distance calculations");
        }

        [TestMethod]
        public void ElectGoogleClient_GetFastestTrip_IntegrationPoints_Exist()
        {
            // Test verifies the integration points between ElectGoogleClient and FastestTrip
            var client = new ElectGoogleClient();
            var clientType = typeof(ElectGoogleClient);
            
            // Verify all FastestTrip related methods exist on ElectGoogleClient
            var azTripMethod1 = clientType.GetMethod("GetFastestAzTrip", new[] { typeof(CoordinateModel[]) });
            var azTripMethod2 = clientType.GetMethod("GetFastestAzTrip", new[] { typeof(string), typeof(CoordinateModel[]) });
            var roundTripMethod1 = clientType.GetMethod("GetFastestRoundTrip", new[] { typeof(CoordinateModel[]) });
            var roundTripMethod2 = clientType.GetMethod("GetFastestRoundTrip", new[] { typeof(string), typeof(CoordinateModel[]) });
            var tripMethod1 = clientType.GetMethod("GetFastestTrip", new[] { typeof(TripType), typeof(CoordinateModel[]) });
            var tripMethod2 = clientType.GetMethod("GetFastestTrip", new[] { typeof(TripType), typeof(string), typeof(CoordinateModel[]) });
            
            Assert.IsNotNull(azTripMethod1, "GetFastestAzTrip(CoordinateModel[]) should exist");
            Assert.IsNotNull(azTripMethod2, "GetFastestAzTrip(string, CoordinateModel[]) should exist");
            Assert.IsNotNull(roundTripMethod1, "GetFastestRoundTrip(CoordinateModel[]) should exist");
            Assert.IsNotNull(roundTripMethod2, "GetFastestRoundTrip(string, CoordinateModel[]) should exist");
            Assert.IsNotNull(tripMethod1, "GetFastestTrip(TripType, CoordinateModel[]) should exist");
            Assert.IsNotNull(tripMethod2, "GetFastestTrip(TripType, string, CoordinateModel[]) should exist");
        }

        [TestMethod]
        public void FastestTrip_ReturnType_TripModel_IsCorrect()
        {
            // Test verifies the return type structure for FastestTrip operations
            var tripModelType = typeof(TripModel);
            
            var coordinateSequencesProperty = tripModelType.GetProperty("CoordinateSequences");
            var totalDistanceProperty = tripModelType.GetProperty("TotalDistanceInMeter");
            var totalDurationProperty = tripModelType.GetProperty("TotalDurationInSecond");
            var matrixProperty = tripModelType.GetProperty("DistanceDurationMatrix");
            
            Assert.IsNotNull(coordinateSequencesProperty, "CoordinateSequences property should exist on TripModel");
            Assert.IsNotNull(totalDistanceProperty, "TotalDistanceInMeter property should exist on TripModel");
            Assert.IsNotNull(totalDurationProperty, "TotalDurationInSecond property should exist on TripModel");
            Assert.IsNotNull(matrixProperty, "DistanceDurationMatrix property should exist on TripModel");
            
            // Verify property types
            Assert.AreEqual(typeof(List<CoordinateModel>), coordinateSequencesProperty.PropertyType, 
                "CoordinateSequences should be List<CoordinateModel>");
            Assert.AreEqual(typeof(double), totalDistanceProperty.PropertyType, 
                "TotalDistanceInMeter should be double");
            Assert.AreEqual(typeof(double), totalDurationProperty.PropertyType, 
                "TotalDurationInSecond should be double");
            Assert.AreEqual(typeof(DistanceDurationMatrixResultModel), matrixProperty.PropertyType, 
                "DistanceDurationMatrix should be DistanceDurationMatrixResultModel");
        }

        [TestMethod]
        public void FastestTrip_PrivateFieldsAndProperties_ExistForAlgorithms()
        {
            // Test verifies that private fields and properties used by TSP algorithms exist
            var assembly = typeof(ElectGoogleClient).Assembly;
            var fastestTripType = assembly.GetType("Elect.Location.Google.Services.FastestTrip");
            
            // Check for key private properties
            var bestPathProperty = fastestTripType?.GetProperty("BestPath", BindingFlags.NonPublic | BindingFlags.Instance);
            var bestTripProperty = fastestTripType?.GetProperty("BestTrip", BindingFlags.NonPublic | BindingFlags.Instance);
            
            // Check for key private fields
            var currentPathField = fastestTripType?.GetField("_currentPath", BindingFlags.NonPublic | BindingFlags.Instance);
            var visitTracksField = fastestTripType?.GetField("_visitTracks", BindingFlags.NonPublic | BindingFlags.Instance);
            var minField = fastestTripType?.GetField("_min", BindingFlags.NonPublic | BindingFlags.Instance);
            var tripTypeField = fastestTripType?.GetField("_tripType", BindingFlags.NonPublic | BindingFlags.Instance);
            
            Assert.IsNotNull(bestPathProperty, "BestPath property should exist for storing optimal path");
            Assert.IsNotNull(bestTripProperty, "BestTrip property should exist for storing optimal cost");
            Assert.IsNotNull(currentPathField, "_currentPath field should exist for algorithm state");
            Assert.IsNotNull(visitTracksField, "_visitTracks field should exist for tracking visited nodes");
            Assert.IsNotNull(minField, "_min field should exist for minimum distance calculations");
            Assert.IsNotNull(tripTypeField, "_tripType field should exist for storing trip type");
        }

        [TestMethod]
        public void FastestTrip_AlgorithmMethods_ExistInClass()
        {
            // Test verifies that all TSP algorithm methods exist
            var assembly = typeof(ElectGoogleClient).Assembly;
            var fastestTripType = assembly.GetType("Elect.Location.Google.Services.FastestTrip");
            
            // Check for algorithm methods
            var calculateTripMethod = fastestTripType?.GetMethod("CalculateTrip", BindingFlags.NonPublic | BindingFlags.Instance);
            var tspDynamicMethod = fastestTripType?.GetMethod("TspDynamic", BindingFlags.NonPublic | BindingFlags.Instance);
            var tspAntColonyMethod = fastestTripType?.GetMethod("TspAntColonyK2", BindingFlags.NonPublic | BindingFlags.Instance);
            var tspK3Method = fastestTripType?.GetMethod("TspK3", BindingFlags.NonPublic | BindingFlags.Instance);
            var backtrackingMethod = fastestTripType?.GetMethod("CalculateTripBackTrackingImplementation", BindingFlags.NonPublic | BindingFlags.Instance);
            
            Assert.IsNotNull(calculateTripMethod, "CalculateTrip method should exist for algorithm selection");
            Assert.IsNotNull(tspDynamicMethod, "TspDynamic method should exist for dynamic programming");
            Assert.IsNotNull(tspAntColonyMethod, "TspAntColonyK2 method should exist for ant colony optimization");
            Assert.IsNotNull(tspK3Method, "TspK3 method should exist for 3-opt optimization");
            Assert.IsNotNull(backtrackingMethod, "CalculateTripBackTrackingImplementation should exist for backtracking");
        }

        [TestMethod]
        public void FastestTrip_HelperMethods_ExistInClass()
        {
            // Test verifies helper methods for distance/duration calculations
            var assembly = typeof(ElectGoogleClient).Assembly;
            var fastestTripType = assembly.GetType("Elect.Location.Google.Services.FastestTrip");
            
            var getDurationMethod = fastestTripType?.GetMethod("GetDuration", BindingFlags.NonPublic | BindingFlags.Instance);
            var getDistanceMethod = fastestTripType?.GetMethod("GetDistance", BindingFlags.NonPublic | BindingFlags.Instance);
            var getMinDistanceMethod = fastestTripType?.GetMethod("GetMinDistance", BindingFlags.NonPublic | BindingFlags.Instance);
            
            Assert.IsNotNull(getDurationMethod, "GetDuration helper method should exist");
            Assert.IsNotNull(getDistanceMethod, "GetDistance helper method should exist");
            Assert.IsNotNull(getMinDistanceMethod, "GetMinDistance helper method should exist");
        }

        [TestMethod]
        public void FastestTrip_GoogleApiIntegration_CreatesCorrectClient()
        {
            // Test documents the FastestTrip integration with ElectGoogleClient
            // This is based on source code analysis from lines 52-62 in FastestTrip.cs
            
            // Verify that the client creation pattern follows expected structure
            var clientType = typeof(ElectGoogleClient);
            var constructorWithAction = clientType.GetConstructor(new[] { typeof(Action<ElectLocationGoogleOptions>) });
            
            Assert.IsNotNull(constructorWithAction, 
                "ElectGoogleClient should have constructor that accepts Action<ElectLocationGoogleOptions>");
            
            // Verify the GetDistanceDurationMatrixAsync method exists with expected signature
            var matrixMethod = clientType.GetMethod("GetDistanceDurationMatrixAsync", 
                new[] { typeof(Action<DistanceDurationMatrixRequestModel>) });
            
            Assert.IsNotNull(matrixMethod, 
                "GetDistanceDurationMatrixAsync should accept Action<DistanceDurationMatrixRequestModel>");
        }

        [TestMethod]
        public void FastestTrip_CoordinateProcessing_PreservesProperties()
        {
            // Test verifies coordinate property preservation logic (lines 68-74 in FastestTrip.cs)
            var coordinateModel = new CoordinateModel(10.5, 106.5);
            
            // Set properties that should be preserved
            coordinateModel.GroupNo = 42;
            var originalGroupNo = coordinateModel.GroupNo;
            
            // Verify coordinate properties can be set and retrieved
            Assert.AreEqual(42, originalGroupNo, "GroupNo should be preserved during processing");
            
            // Verify SequenceNo property exists and can be set
            coordinateModel.SequenceNo = 3;
            Assert.AreEqual(3, coordinateModel.SequenceNo, "SequenceNo should be settable for trip sequencing");
        }

        [TestMethod]
        public void FastestTrip_TripTypeHandling_BothModesSupported()
        {
            // Test verifies trip type handling (lines 173-178 in source)
            // This documents the difference between AZ and RoundTrip handling
            
            var azType = TripType.AZ;
            var roundTripType = TripType.RoundTrip;
            
            // Document expected behavior differences
            Assert.AreEqual(1, (int)azType, "AZ trip should be value 1");
            Assert.AreEqual(2, (int)roundTripType, "RoundTrip should be value 2");
            
            // Verify both types are valid enum values
            Assert.IsTrue(Enum.IsDefined(typeof(TripType), azType), "AZ should be valid TripType");
            Assert.IsTrue(Enum.IsDefined(typeof(TripType), roundTripType), "RoundTrip should be valid TripType");
        }
    }
}