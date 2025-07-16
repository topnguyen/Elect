[TestClass]
public class ElectWebAjaxifyUnitTest
{
    [TestMethod]
    public void ElectWebAjaxify_ProjectStructure_ShouldHaveCorrectFiles()
    {
        // Find the solution root by looking for the .sln file
        var currentDir = Directory.GetCurrentDirectory();
        var solutionRoot = FindSolutionRoot(currentDir);
        var projectPath = Path.Combine(solutionRoot, "src", "Web", "Elect.Web.Ajaxify");
        
        Assert.IsTrue(Directory.Exists(projectPath), $"Elect.Web.Ajaxify project directory should exist at: {projectPath}");
        
        var csprojFile = Path.Combine(projectPath, "Elect.Web.Ajaxify.csproj");
        Assert.IsTrue(File.Exists(csprojFile), "Project file should exist");
        
        var globalUsingsFile = Path.Combine(projectPath, "GlobalUsings.cs");
        Assert.IsTrue(File.Exists(globalUsingsFile), "GlobalUsings.cs should exist");
        
        var readmeFile = Path.Combine(projectPath, "README.md");
        Assert.IsTrue(File.Exists(readmeFile), "README.md should exist");
    }

    [TestMethod]
    public void ElectWebAjaxify_ShouldHaveJavaScriptAssets()
    {
        var currentDir = Directory.GetCurrentDirectory();
        var solutionRoot = FindSolutionRoot(currentDir);
        var projectPath = Path.Combine(solutionRoot, "src", "Web", "Elect.Web.Ajaxify");
        
        var ajaxifyJs = Path.Combine(projectPath, "elect.web.ajaxify.js");
        Assert.IsTrue(File.Exists(ajaxifyJs), "elect.web.ajaxify.js should exist");
        
        var ajaxifyMinJs = Path.Combine(projectPath, "elect.web.ajaxify.min.js");
        Assert.IsTrue(File.Exists(ajaxifyMinJs), "elect.web.ajaxify.min.js should exist");
        
        var historyJs = Path.Combine(projectPath, "jquery.history.js");
        Assert.IsTrue(File.Exists(historyJs), "jquery.history.js should exist");
    }

    [TestMethod]
    public void ElectWebAjaxify_JavaScriptFiles_ShouldNotBeEmpty()
    {
        var currentDir = Directory.GetCurrentDirectory();
        var solutionRoot = FindSolutionRoot(currentDir);
        var projectPath = Path.Combine(solutionRoot, "src", "Web", "Elect.Web.Ajaxify");
        
        var ajaxifyJs = Path.Combine(projectPath, "elect.web.ajaxify.js");
        if (File.Exists(ajaxifyJs))
        {
            var fileInfo = new FileInfo(ajaxifyJs);
            Assert.IsTrue(fileInfo.Length > 0, "elect.web.ajaxify.js should not be empty");
        }
        
        var ajaxifyMinJs = Path.Combine(projectPath, "elect.web.ajaxify.min.js");
        if (File.Exists(ajaxifyMinJs))
        {
            var fileInfo = new FileInfo(ajaxifyMinJs);
            Assert.IsTrue(fileInfo.Length > 0, "elect.web.ajaxify.min.js should not be empty");
        }
    }

    [TestMethod]
    public void ElectWebAjaxify_MinifiedVersion_ShouldBeSmallerThanOriginal()
    {
        var currentDir = Directory.GetCurrentDirectory();
        var solutionRoot = FindSolutionRoot(currentDir);
        var projectPath = Path.Combine(solutionRoot, "src", "Web", "Elect.Web.Ajaxify");
        
        var ajaxifyJs = Path.Combine(projectPath, "elect.web.ajaxify.js");
        var ajaxifyMinJs = Path.Combine(projectPath, "elect.web.ajaxify.min.js");
        
        if (File.Exists(ajaxifyJs) && File.Exists(ajaxifyMinJs))
        {
            var originalSize = new FileInfo(ajaxifyJs).Length;
            var minifiedSize = new FileInfo(ajaxifyMinJs).Length;
            
            Assert.IsTrue(minifiedSize <= originalSize, 
                "Minified version should be smaller than or equal to original");
        }
    }

    private static string FindSolutionRoot(string startDirectory)
    {
        var current = new DirectoryInfo(startDirectory);
        while (current != null)
        {
            if (current.GetFiles("*.sln").Any())
            {
                return current.FullName;
            }
            current = current.Parent;
        }
        
        // Fallback - assume we're in solution root if no .sln found
        return startDirectory;
    }

    public TestContext TestContext { get; set; }
}