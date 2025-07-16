namespace Elect.Test.Location.Google.Models
{
    [TestClass]
    public class DistanceDurationMatrixResultModelUnitTest
    {
        [TestMethod]
        public void OriginAddresses_GetSet_WorksCorrectly()
        {
            var result = new DistanceDurationMatrixResultModel();
            var originAddresses = new[] { "Address 1", "Address 2" };
            
            result.OriginAddresses = originAddresses;
            
            Assert.AreEqual(originAddresses, result.OriginAddresses);
            Assert.AreEqual(2, result.OriginAddresses.Length);
        }

        [TestMethod]
        public void DestinationAddresses_GetSet_WorksCorrectly()
        {
            var result = new DistanceDurationMatrixResultModel();
            var destinationAddresses = new[] { "Dest 1", "Dest 2" };
            
            result.DestinationAddresses = destinationAddresses;
            
            Assert.AreEqual(destinationAddresses, result.DestinationAddresses);
            Assert.AreEqual(2, result.DestinationAddresses.Length);
        }

        [TestMethod]
        public void Rows_GetSet_WorksCorrectly()
        {
            var result = new DistanceDurationMatrixResultModel();
            var rows = new[]
            {
                new DistanceMatrixRowModel(),
                new DistanceMatrixRowModel()
            };
            
            result.Rows = rows;
            
            Assert.AreEqual(rows, result.Rows);
            Assert.AreEqual(2, result.Rows.Length);
        }

        [TestMethod]
        public void Status_GetSet_WorksCorrectly()
        {
            var result = new DistanceDurationMatrixResultModel();
            var status = "OK";
            
            result.Status = status;
            
            Assert.AreEqual(status, result.Status);
        }

        [TestMethod]
        public void DistanceMatrix_ComputesCorrectly()
        {
            var result = new DistanceDurationMatrixResultModel
            {
                OriginAddresses = new[] { "Origin1", "Origin2" },
                DestinationAddresses = new[] { "Dest1", "Dest2" },
                Rows = new[]
                {
                    new DistanceMatrixRowModel
                    {
                        Elements = new[]
                        {
                            new DistanceMatrixRowElementModel
                            {
                                Distance = new DistanceMatrixElementDistanceDataModel { Value = 1000 },
                                Duration = new DistanceMatrixElementDurationDataModel { Value = 300 }
                            },
                            new DistanceMatrixRowElementModel
                            {
                                Distance = new DistanceMatrixElementDistanceDataModel { Value = 2000 },
                                Duration = new DistanceMatrixElementDurationDataModel { Value = 600 }
                            }
                        }
                    },
                    new DistanceMatrixRowModel
                    {
                        Elements = new[]
                        {
                            new DistanceMatrixRowElementModel
                            {
                                Distance = new DistanceMatrixElementDistanceDataModel { Value = 1500 },
                                Duration = new DistanceMatrixElementDurationDataModel { Value = 450 }
                            },
                            new DistanceMatrixRowElementModel
                            {
                                Distance = new DistanceMatrixElementDistanceDataModel { Value = 2500 },
                                Duration = new DistanceMatrixElementDurationDataModel { Value = 750 }
                            }
                        }
                    }
                }
            };
            
            var distanceMatrix = result.DistanceMatrix;
            
            Assert.AreEqual(1000, distanceMatrix[0, 0]);
            Assert.AreEqual(2000, distanceMatrix[0, 1]);
            Assert.AreEqual(1500, distanceMatrix[1, 0]);
            Assert.AreEqual(2500, distanceMatrix[1, 1]);
        }

        [TestMethod]
        public void DurationMatrix_ComputesCorrectly()
        {
            var result = new DistanceDurationMatrixResultModel
            {
                OriginAddresses = new[] { "Origin1", "Origin2" },
                DestinationAddresses = new[] { "Dest1", "Dest2" },
                Rows = new[]
                {
                    new DistanceMatrixRowModel
                    {
                        Elements = new[]
                        {
                            new DistanceMatrixRowElementModel
                            {
                                Distance = new DistanceMatrixElementDistanceDataModel { Value = 1000 },
                                Duration = new DistanceMatrixElementDurationDataModel { Value = 300 }
                            },
                            new DistanceMatrixRowElementModel
                            {
                                Distance = new DistanceMatrixElementDistanceDataModel { Value = 2000 },
                                Duration = new DistanceMatrixElementDurationDataModel { Value = 600 }
                            }
                        }
                    },
                    new DistanceMatrixRowModel
                    {
                        Elements = new[]
                        {
                            new DistanceMatrixRowElementModel
                            {
                                Distance = new DistanceMatrixElementDistanceDataModel { Value = 1500 },
                                Duration = new DistanceMatrixElementDurationDataModel { Value = 450 }
                            },
                            new DistanceMatrixRowElementModel
                            {
                                Distance = new DistanceMatrixElementDistanceDataModel { Value = 2500 },
                                Duration = new DistanceMatrixElementDurationDataModel { Value = 750 }
                            }
                        }
                    }
                }
            };
            
            var durationMatrix = result.DurationMatrix;
            
            Assert.AreEqual(300, durationMatrix[0, 0]);
            Assert.AreEqual(600, durationMatrix[0, 1]);
            Assert.AreEqual(450, durationMatrix[1, 0]);
            Assert.AreEqual(750, durationMatrix[1, 1]);
        }

        [TestMethod]
        public void DistanceMatrix_CachesResult()
        {
            var result = new DistanceDurationMatrixResultModel
            {
                OriginAddresses = new[] { "Origin1" },
                DestinationAddresses = new[] { "Dest1" },
                Rows = new[]
                {
                    new DistanceMatrixRowModel
                    {
                        Elements = new[]
                        {
                            new DistanceMatrixRowElementModel
                            {
                                Distance = new DistanceMatrixElementDistanceDataModel { Value = 1000 },
                                Duration = new DistanceMatrixElementDurationDataModel { Value = 300 }
                            }
                        }
                    }
                }
            };
            
            var matrix1 = result.DistanceMatrix;
            var matrix2 = result.DistanceMatrix;
            
            Assert.AreSame(matrix1, matrix2);
        }

        [TestMethod]
        public void DurationMatrix_CachesResult()
        {
            var result = new DistanceDurationMatrixResultModel
            {
                OriginAddresses = new[] { "Origin1" },
                DestinationAddresses = new[] { "Dest1" },
                Rows = new[]
                {
                    new DistanceMatrixRowModel
                    {
                        Elements = new[]
                        {
                            new DistanceMatrixRowElementModel
                            {
                                Distance = new DistanceMatrixElementDistanceDataModel { Value = 1000 },
                                Duration = new DistanceMatrixElementDurationDataModel { Value = 300 }
                            }
                        }
                    }
                }
            };
            
            var matrix1 = result.DurationMatrix;
            var matrix2 = result.DurationMatrix;
            
            Assert.AreSame(matrix1, matrix2);
        }

        [TestMethod]
        public void DefaultValues_AreCorrect()
        {
            var result = new DistanceDurationMatrixResultModel();
            
            Assert.IsNull(result.OriginAddresses);
            Assert.IsNull(result.DestinationAddresses);
            Assert.IsNull(result.Rows);
            Assert.IsNull(result.Status);
        }
    }
}