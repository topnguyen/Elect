namespace Elect.Test.Core.StringUtils
{
    [TestClass]
    public class StringHelperUnitTest
    {
        [TestMethod]
        public void Generate_WorksWithAllOptions()
        {
            var result = StringHelper.Generate(10, true, true, true, true);
            Assert.AreEqual(10, result.Length);
        }
        [TestMethod]
        public void GenerateRandom_ThrowsOnInvalidLength()
        {
            Assert.ThrowsException<System.ArgumentOutOfRangeException>(() => StringHelper.GenerateRandom(-1, 'a'));
        }
        [TestMethod]
        public void GenerateRandom_ThrowsOnEmptyChars()
        {
            Assert.ThrowsException<System.ArgumentOutOfRangeException>(() => StringHelper.GenerateRandom(5));
        }
        [TestMethod]
        public void GenerateRandom_ThrowsOnTooManyChars()
        {
            // Create 257 unique chars
            var chars = Enumerable.Range(0, 257).Select(i => (char)i).ToArray();
            Assert.ThrowsException<System.ArgumentException>(() => StringHelper.GenerateRandom(5, chars));
        }
        [TestMethod]
        public void GenerateRandom_ProducesCorrectLength()
        {
            var result = StringHelper.GenerateRandom(8, 'a', 'b', 'c');
            Assert.AreEqual(8, result.Length);
            Assert.IsTrue(result.All(c => c == 'a' || c == 'b' || c == 'c'));
        }
        [TestMethod]
        public void Normalize_RemovesAccentsAndEdgeCases()
        {
            Assert.AreEqual("CAFE", StringHelper.Normalize("Café"));
            Assert.AreEqual("A", StringHelper.Normalize("à"));
            Assert.AreEqual("", StringHelper.Normalize(null));
            Assert.AreEqual("", StringHelper.Normalize("   "));
        }
        [TestMethod]
        public void ConvertEdgeCases_HandlesAllCases()
        {
            Assert.AreEqual("a", StringHelper.ConvertEdgeCases('á'));
            Assert.AreEqual("e", StringHelper.ConvertEdgeCases('ë'));
            Assert.AreEqual("i", StringHelper.ConvertEdgeCases('í'));
            Assert.AreEqual("o", StringHelper.ConvertEdgeCases('ö'));
            Assert.AreEqual("u", StringHelper.ConvertEdgeCases('ú'));
            Assert.AreEqual("c", StringHelper.ConvertEdgeCases('č'));
            Assert.AreEqual("z", StringHelper.ConvertEdgeCases('ž'));
            Assert.AreEqual("n", StringHelper.ConvertEdgeCases('ń'));
            Assert.AreEqual("y", StringHelper.ConvertEdgeCases('ý'));
            Assert.AreEqual("g", StringHelper.ConvertEdgeCases('ğ'));
            Assert.AreEqual("r", StringHelper.ConvertEdgeCases('ř'));
            Assert.AreEqual("l", StringHelper.ConvertEdgeCases('ľ'));
            Assert.AreEqual("d", StringHelper.ConvertEdgeCases('ď'));
            Assert.AreEqual("t", StringHelper.ConvertEdgeCases('ť'));
            Assert.AreEqual("ss", StringHelper.ConvertEdgeCases('ß'));
            Assert.AreEqual("th", StringHelper.ConvertEdgeCases('Þ'));
            Assert.AreEqual("h", StringHelper.ConvertEdgeCases('ĥ'));
            Assert.AreEqual("j", StringHelper.ConvertEdgeCases('ĵ'));
            Assert.AreEqual("b", StringHelper.ConvertEdgeCases('b'));
        }
        [TestMethod]
        public void RemoveAccents_RemovesDiacritics()
        {
            Assert.AreEqual("Cafe", StringHelper.RemoveAccents("Café"));
            Assert.AreEqual("a", StringHelper.RemoveAccents("à"));
            Assert.IsNull(StringHelper.RemoveAccents(null));
            Assert.AreEqual("   ", StringHelper.RemoveAccents("   "));
        }
        [TestMethod]
        public void IsBase64_WorksCorrectly()
        {
            var valid = System.Convert.ToBase64String(System.Text.Encoding.UTF8.GetBytes("test"));
            Assert.IsTrue(StringHelper.IsBase64(valid));
            Assert.IsFalse(StringHelper.IsBase64("notbase64"));
            Assert.IsFalse(StringHelper.IsBase64(null));
            Assert.IsFalse(StringHelper.IsBase64("   "));
        }
    }
}
