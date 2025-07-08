namespace Elect.Test.Core.Constants
{
    [TestClass]
    public class FormattingUnitTest
    {
        [TestMethod]
        public void DateTimeOffSetFormat_IsCorrect()
        {
            Assert.AreEqual("yyyy'-'MM'-'dd'T'HH':'mm':'ss.FFFFFFFK", Formatting.DateTimeOffSetFormat);
        }
        [TestMethod]
        public void JsonSerializerSettings_HasExpectedSettings()
        {
            var settings = Formatting.JsonSerializerSettings;
            Assert.AreEqual(Formatting.DateTimeOffSetFormat, settings.DateFormatString);
            Assert.AreEqual(Newtonsoft.Json.Formatting.None, settings.Formatting);
            Assert.AreEqual(NullValueHandling.Ignore, settings.NullValueHandling);
            Assert.AreEqual(MissingMemberHandling.Ignore, settings.MissingMemberHandling);
            Assert.AreEqual(ReferenceLoopHandling.Ignore, settings.ReferenceLoopHandling);
            Assert.AreEqual(DateFormatHandling.IsoDateFormat, settings.DateFormatHandling);
            Assert.IsNotNull(settings.ContractResolver);
        }
        [TestMethod]
        public void JsonDeserialization_WorksCorrectly()
        {
            var json = @"{""Name"":""Test"",""Age"":30}";
            var dict = JsonConvert.DeserializeObject<Dictionary<string, object>>(json);
            Assert.AreEqual("Test", dict["Name"].ToString());
            Assert.AreEqual(30, Convert.ToInt32(dict["Age"]));
        }
    }
}
