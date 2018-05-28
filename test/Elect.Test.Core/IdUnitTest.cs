using System;
using System.IO;
using Elect.Core.StringUtils;
using Elect.Data.IO;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace Elect.Test.Core
{
    [TestClass]
    public class IdUnitTest
    {
        [TestMethod]
        public void GenerateShortIdCase()
        {
            const uint originalId = 2028044070;

            var idShortString = IdHelper.ToShortString(originalId);

            var idShort = IdHelper.FromShortString(idShortString);

            Assert.AreEqual(idShort, originalId);
        }

        [TestMethod]
        public void GenerateLongIdCase()
        {
            // https://www.youtube.com/watch?v=HCUPVi7XDqo

            const string originalId = "HCUPVi7XDqo";

            var id = IdHelper.FromString(originalId);

            var idString = IdHelper.ToString(id);

            Assert.AreEqual(idString, originalId);
        }

        [TestMethod]
        public void GenerateLongIdsCase()
        {
            var idFilepath = "Ids.txt";
            idFilepath = PathHelper.GetFullPath(idFilepath);
            
            for (ulong i = 0; i < 100000; i++)
            {
                var idString = IdHelper.ToString(i);

                File.AppendAllText(idFilepath, idString + Environment.NewLine);
            }
        }
    }
}