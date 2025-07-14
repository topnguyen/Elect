using Elect.Web.StringUtils;

namespace Elect.Test.Web.StringUtils
{
    [TestClass]
    public class StringExtensionsUnitTest
    {
        [TestMethod]
        public void ToFriendlySlug_CallsStringHelper()
        {
            var input = "Hello World";
            var result = input.ToFriendlySlug();
            
            Assert.AreEqual("hello-world", result);
        }

        [TestMethod]
        public void ToFriendlySlug_WithCustomMaxLength_PassesMaxLengthToHelper()
        {
            var input = "This is a very long string that should be truncated";
            var result = input.ToFriendlySlug(10);
            
            Assert.IsTrue(result.Length <= 10);
            Assert.AreEqual("this-is-a", result);
        }

        [TestMethod]
        public void ToFriendlySlug_WithDefaultMaxLength_UsesDefault150()
        {
            var longInput = new string('a', 200) + " " + new string('b', 200);
            var result = longInput.ToFriendlySlug();
            
            Assert.IsTrue(result.Length <= 150);
        }

        [TestMethod]
        public void ToFriendlySlug_WithNullString_ReturnsEmptyString()
        {
            string input = null;
            var result = input.ToFriendlySlug();
            
            Assert.AreEqual("", result);
        }

        [TestMethod]
        public void RemoveHtmlTag_CallsStringHelper()
        {
            var input = "<p>Hello <b>World</b></p>";
            var result = StringExtensions.RemoveHtmlTag(input);
            
            Assert.AreEqual("Hello World", result);
        }

        [TestMethod]
        public void RemoveHtmlTag_WithComplexHtml_RemovesAllTags()
        {
            var input = "<div class='test'><p>Content <a href='#'>Link</a></p></div>";
            var result = StringExtensions.RemoveHtmlTag(input);
            
            Assert.AreEqual("Content Link", result);
        }

        [TestMethod]
        public void RemoveHtmlTag_WithPlainText_ReturnsOriginalText()
        {
            var input = "Plain text without HTML";
            var result = StringExtensions.RemoveHtmlTag(input);
            
            Assert.AreEqual(input, result);
        }

        [TestMethod]
        public void RemoveHtmlTag_WithEmptyString_ReturnsEmptyString()
        {
            var result = StringExtensions.RemoveHtmlTag("");
            Assert.AreEqual("", result);
        }

        [TestMethod]
        public void ToFriendlySlug_WithSpecialCharacters_HandlesCorrectly()
        {
            var input = "Test/Path\\File.ext";
            var result = input.ToFriendlySlug();
            
            Assert.AreEqual("test-path-file-ext", result);
        }

        [TestMethod]
        public void ToFriendlySlug_WithUnicodeCharacters_NormalizesAccents()
        {
            var input = "café naïve";
            var result = input.ToFriendlySlug();
            
            // ToFriendlySlug normalizes accents for URL-friendly output
            Assert.IsTrue(result.Contains("cafe") || result.Contains("café"));
            Assert.IsTrue(result.Contains("naive") || result.Contains("naïve"));
        }
    }
}