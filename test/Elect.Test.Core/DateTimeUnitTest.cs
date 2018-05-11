using System;
using Elect.Core.DateTimeUtils;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace Elect.Test.Core
{
    [TestClass]
    public class DateTimeUnitTest
    {
        [TestMethod]
        public void WithTimeZone()
        {
            DateTime dateTime = DateTime.UtcNow;

            DateTimeOffset dateTimeOffSet = DateTimeHelper.WithTimeZone(dateTime, "SE Asia Standard Time");

            Assert.AreEqual(dateTimeOffSet.Offset, new TimeSpan(7, 0, 0));
        }
    }
}