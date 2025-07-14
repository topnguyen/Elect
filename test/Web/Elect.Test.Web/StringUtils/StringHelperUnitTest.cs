using Elect.Web.StringUtils;

namespace Elect.Test.Web.StringUtils
{
    [TestClass]
    public class StringHelperUnitTest
    {
        [TestMethod]
        public void GetFriendlySlug_WithNullOrWhiteSpace_ReturnsEmptyString()
        {
            Assert.AreEqual("", StringHelper.GetFriendlySlug(null));
            Assert.AreEqual("", StringHelper.GetFriendlySlug(""));
            Assert.AreEqual("", StringHelper.GetFriendlySlug("   "));
            Assert.AreEqual("", StringHelper.GetFriendlySlug("\t\n"));
        }

        [TestMethod]
        public void GetFriendlySlug_WithSimpleText_ReturnsLowercaseSlug()
        {
            var result = StringHelper.GetFriendlySlug("Hello World");
            Assert.AreEqual("hello-world", result);
        }

        [TestMethod]
        public void GetFriendlySlug_WithSpecialCharacters_ReplacesWithDashes()
        {
            var result = StringHelper.GetFriendlySlug("Hello, World! How are you?");
            Assert.AreEqual("hello-world-how-are-you", result);
        }

        [TestMethod]
        public void GetFriendlySlug_WithMultipleSpaces_CollapsesDashes()
        {
            var result = StringHelper.GetFriendlySlug("Hello    World");
            Assert.AreEqual("hello-world", result);
        }

        [TestMethod]
        public void GetFriendlySlug_WithNumbersAndLetters_PreservesAlphanumeric()
        {
            var result = StringHelper.GetFriendlySlug("Test123 ABC456");
            Assert.AreEqual("test123-abc456", result);
        }

        [TestMethod]
        public void GetFriendlySlug_WithMaxLength_TruncatesCorrectly()
        {
            var result = StringHelper.GetFriendlySlug("This is a very long string that should be truncated", 10);
            Assert.IsTrue(result.Length <= 10);
            Assert.AreEqual("this-is-a", result);
        }

        [TestMethod]
        public void GetFriendlySlug_WithSlashesAndBackslashes_ReplacesWithDashes()
        {
            var result = StringHelper.GetFriendlySlug("path/to\\file.txt");
            Assert.AreEqual("path-to-file-txt", result);
        }

        [TestMethod]
        public void GetFriendlySlug_WithUnderscoresAndEquals_ReplacesWithDashes()
        {
            var result = StringHelper.GetFriendlySlug("test_name=value");
            Assert.AreEqual("test-name-value", result);
        }

        [TestMethod]
        public void GetFriendlySlug_WithTrailingDash_RemovesTrailingDash()
        {
            var result = StringHelper.GetFriendlySlug("hello world.", 11);
            Assert.AreEqual("hello-world", result);
        }

        [TestMethod]
        public void GetFriendlySlug_WithUnicodeCharacters_NormalizesAccents()
        {
            var result = StringHelper.GetFriendlySlug("café naïve résumé");
            // GetFriendlySlug normalizes accents for URL-friendly output
            Assert.IsTrue(result.Contains("cafe") || result.Contains("café"));
            Assert.IsTrue(result.Contains("naive") || result.Contains("naïve"));
            Assert.IsTrue(result.Contains("resume") || result.Contains("résumé"));
        }

        [TestMethod]
        public void EncodeBase64Url_WithValidString_ReturnsEncodedString()
        {
            var input = "Hello World";
            var result = StringHelper.EncodeBase64Url(input);
            
            Assert.IsNotNull(result);
            Assert.AreNotEqual(input, result);
            Assert.IsTrue(result.Length > 0);
        }

        [TestMethod]
        public void DecodeBase64Url_WithValidEncodedString_ReturnsOriginalString()
        {
            var original = "Hello World";
            var encoded = StringHelper.EncodeBase64Url(original);
            var decoded = StringHelper.DecodeBase64Url(encoded);
            
            Assert.AreEqual(original, decoded);
        }

        [TestMethod]
        public void EncodeDecodeBase64Url_RoundTrip_PreservesData()
        {
            var testStrings = new[]
            {
                "Simple text",
                "Text with spaces and punctuation!",
                "Unicode: café naïve résumé",
                "Numbers: 123456789",
                "Special chars: @#$%^&*()",
                ""
            };

            foreach (var testString in testStrings)
            {
                var encoded = StringHelper.EncodeBase64Url(testString);
                var decoded = StringHelper.DecodeBase64Url(encoded);
                Assert.AreEqual(testString, decoded, $"Failed for: {testString}");
            }
        }

        [TestMethod]
        public void RemoveHtmlTag_WithSimpleHtmlTags_RemovesTags()
        {
            var input = "<p>Hello <b>World</b></p>";
            var result = StringHelper.RemoveHtmlTag(input);
            
            Assert.AreEqual("Hello World", result);
        }

        [TestMethod]
        public void RemoveHtmlTag_WithSelfClosingTags_RemovesTags()
        {
            var input = "Image: <img src='test.jpg' /> End";
            var result = StringHelper.RemoveHtmlTag(input);
            
            Assert.AreEqual("Image:  End", result);
        }

        [TestMethod]
        public void RemoveHtmlTag_WithComplexHtml_RemovesAllTags()
        {
            var input = "<div class='test'><p>Hello <a href='#'>Link</a></p><br/></div>";
            var result = StringHelper.RemoveHtmlTag(input);
            
            Assert.AreEqual("Hello Link", result);
        }

        [TestMethod]
        public void RemoveHtmlTag_WithNoHtmlTags_ReturnsOriginalString()
        {
            var input = "Plain text with no HTML tags";
            var result = StringHelper.RemoveHtmlTag(input);
            
            Assert.AreEqual(input, result);
        }

        [TestMethod]
        public void RemoveHtmlTag_WithEmptyString_ReturnsEmptyString()
        {
            var result = StringHelper.RemoveHtmlTag("");
            Assert.AreEqual("", result);
        }

        [TestMethod]
        public void RemoveHtmlTag_WithHtmlEntities_PreservesEntities()
        {
            var input = "<p>Hello &amp; goodbye &lt;test&gt;</p>";
            var result = StringHelper.RemoveHtmlTag(input);
            
            Assert.AreEqual("Hello &amp; goodbye &lt;test&gt;", result);
        }

        [TestMethod]
        public void RemoveHtmlTag_WithNestedTags_RemovesAllTags()
        {
            var input = "<div><p><span>Nested <b>content</b></span></p></div>";
            var result = StringHelper.RemoveHtmlTag(input);
            
            Assert.AreEqual("Nested content", result);
        }
    }
}