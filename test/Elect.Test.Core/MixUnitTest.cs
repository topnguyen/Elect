using System;
using System.Collections.Generic;
using System.Linq;
using Elect.Core.LinqUtils;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace Elect.Test.Core
{
    [TestClass]
    public class MixUnitTest
    {
        [TestMethod]
        public void MixCase()
        {
            var items = new List<int> {1, 5, 7, 3, 10, 9, 6};

            var wherePrevious = items.WherePrevious((first, second) => second > first).ToList();

            var randomize = items.Randomize().ToList();

            var l = items.ToList();
        }
    }
}