[TestClass]
public class ElectServerInfoOptionsUnitTest
{
    [TestMethod]
    public void Constructor_ShouldInitializeWithDefaultValues()
    {
        var options = new ElectServerInfoOptions();

        Assert.AreEqual("cloudflare-nginx", options.ServerName);
        Assert.AreEqual("PHP/5.6.30", options.PoweredBy);
        Assert.IsNull(options.AuthorName);
        Assert.IsNull(options.AuthorWebsite);
        Assert.IsNull(options.AuthorEmail);
    }

    [TestMethod]
    public void ServerName_ShouldBeSettable()
    {
        var options = new ElectServerInfoOptions();
        var serverName = "custom-server";

        options.ServerName = serverName;

        Assert.AreEqual(serverName, options.ServerName);
    }

    [TestMethod]
    public void PoweredBy_ShouldBeSettable()
    {
        var options = new ElectServerInfoOptions();
        var poweredBy = "ASP.NET Core/9.0";

        options.PoweredBy = poweredBy;

        Assert.AreEqual(poweredBy, options.PoweredBy);
    }

    [TestMethod]
    public void AuthorName_ShouldBeSettable()
    {
        var options = new ElectServerInfoOptions();
        var authorName = "John Doe";

        options.AuthorName = authorName;

        Assert.AreEqual(authorName, options.AuthorName);
    }

    [TestMethod]
    public void AuthorWebsite_ShouldBeSettable()
    {
        var options = new ElectServerInfoOptions();
        var authorWebsite = "https://johndoe.com";

        options.AuthorWebsite = authorWebsite;

        Assert.AreEqual(authorWebsite, options.AuthorWebsite);
    }

    [TestMethod]
    public void AuthorEmail_ShouldBeSettable()
    {
        var options = new ElectServerInfoOptions();
        var authorEmail = "john@example.com";

        options.AuthorEmail = authorEmail;

        Assert.AreEqual(authorEmail, options.AuthorEmail);
    }

    [TestMethod]
    public void Properties_ShouldAllowEmptyStrings()
    {
        var options = new ElectServerInfoOptions
        {
            ServerName = "",
            PoweredBy = "",
            AuthorName = "",
            AuthorWebsite = "",
            AuthorEmail = ""
        };

        Assert.AreEqual("", options.ServerName);
        Assert.AreEqual("", options.PoweredBy);
        Assert.AreEqual("", options.AuthorName);
        Assert.AreEqual("", options.AuthorWebsite);
        Assert.AreEqual("", options.AuthorEmail);
    }

    [TestMethod]
    public void Properties_ShouldAllowNullValues()
    {
        var options = new ElectServerInfoOptions
        {
            ServerName = null,
            PoweredBy = null,
            AuthorName = null,
            AuthorWebsite = null,
            AuthorEmail = null
        };

        Assert.IsNull(options.ServerName);
        Assert.IsNull(options.PoweredBy);
        Assert.IsNull(options.AuthorName);
        Assert.IsNull(options.AuthorWebsite);
        Assert.IsNull(options.AuthorEmail);
    }

    [TestMethod]
    public void ServerName_DefaultValue_ShouldBeCloudflareNginx()
    {
        var options = new ElectServerInfoOptions();

        Assert.AreEqual("cloudflare-nginx", options.ServerName);
    }

    [TestMethod]
    public void PoweredBy_DefaultValue_ShouldBePhpVersion()
    {
        var options = new ElectServerInfoOptions();

        Assert.AreEqual("PHP/5.6.30", options.PoweredBy);
    }

    [TestMethod]
    public void AuthorProperties_DefaultValues_ShouldBeNull()
    {
        var options = new ElectServerInfoOptions();

        Assert.IsNull(options.AuthorName);
        Assert.IsNull(options.AuthorWebsite);
        Assert.IsNull(options.AuthorEmail);
    }
}