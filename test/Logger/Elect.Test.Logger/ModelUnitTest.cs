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
            try
            {
                throw new Exception("Sample Message");
            }
            catch (Exception e)
            {
                var log = new LogModel(e);
            }
        }
    }
}