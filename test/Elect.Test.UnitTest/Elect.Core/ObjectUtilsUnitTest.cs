#region	License
//--------------------------------------------------
// <License>
//     <Copyright> 2018 © Top Nguyen </Copyright>
//     <Url> http://topnguyen.net/ </Url>
//     <Author> Top </Author>
//     <Project> Elect </Project>
//     <File>
//         <Name> ObjectUtilsUnitTest.cs </Name>
//         <Created> 24/03/2018 11:06:46 PM </Created>
//         <Key> 097ad5ec-6fa4-4c10-b9c9-519e4434b29a </Key>
//     </File>
//     <Summary>
//         ObjectUtilsUnitTest.cs is a part of Elect
//     </Summary>
// <License>
//--------------------------------------------------
#endregion License

using Elect.Core.ObjUtils;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;

namespace Elect.Test.UnitTest.Elect.Core
{
    [TestClass]
    public class ObjectUtilsUnitTest
    {
        [TestMethod]
        public void ObjectHelperUnitTest()
        {
            DateTimeOffset sample = new DateTimeOffset();

            var newSample = ObjHelper.ReplaceNullOrDefault(sample, new DateTimeOffset(2018, 10, 10, 0, 0, 0, TimeSpan.Zero));

            Assert.AreEqual(newSample, new DateTimeOffset(2018, 10, 10, 0, 0, 0, TimeSpan.Zero));

            int intSample = 0;

            var newIntSample = ObjHelper.ReplaceNullOrDefault(intSample, 2);

            Assert.AreEqual(newIntSample, 2);
        }
    }
}