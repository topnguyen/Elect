using System;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;
using Elect.Core.SecurityUtils;
using Elect.Core.SecurityUtils.Algorithms;
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
    }
}