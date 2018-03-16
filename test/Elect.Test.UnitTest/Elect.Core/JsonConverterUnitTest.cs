#region	License
//--------------------------------------------------
// <License>
//     <Copyright> 2018 © Top Nguyen </Copyright>
//     <Url> http://topnguyen.net/ </Url>
//     <Author> Top </Author>
//     <Project> Elect </Project>
//     <File>
//         <Name> JsonConverterUnitTest.cs </Name>
//         <Created> 16/03/2018 2:56:09 PM </Created>
//         <Key> ddb7b002-ea56-4bf8-8b2f-cfbe5958c9bd </Key>
//     </File>
//     <Summary>
//         JsonConverterUnitTest.cs is a part of Elect
//     </Summary>
// <License>
//--------------------------------------------------
#endregion License

using Elect.Core.JsonConverters;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Newtonsoft.Json;

namespace Elect.Test.UnitTest.Elect.Core
{
    [TestClass]
    public class JsonConverterUnitTest
    {
        [TestMethod]
        public void ObjectToArrayJsonConverterTest()
        {
            var sampleObj = new SampleClassToArray
            {
                StringProp = "string value",
                IntProp = 9,
                SampleProp = new SampleClassToArray
                {
                    StringProp = "string value 1",
                    IntProp = 10,
                    SampleProp = null
                }
            };

            var result = JsonConvert.SerializeObject(sampleObj);
        }

        [JsonConverter(typeof(ObjectToArrayConverter))]
        public class SampleClassToArray
        {
            public string StringProp { get; set; }

            public int IntProp { get; set; }

            public SampleClassToArray SampleProp { get; set; }
        }
    }
}