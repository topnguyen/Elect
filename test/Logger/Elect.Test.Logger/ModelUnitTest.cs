using System;
using Elect.Logger.Models.Logging;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace Elect.Test.Logger
{
    [TestClass]
    public class ModelUnitTest
    {
        [TestMethod]
        public void ModelTestCase()
        {
            var model = new LogModel(new Exception("Sample Message"));
        }
    }
}