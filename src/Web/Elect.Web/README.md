![Logo](../../../Logo.png)
# Elect.Web
> Author [**Top Nguyen**](http://topnguyen.com)

## Overview

- .Net Core Utilities methods for Web:
    + SiteMap Generation
    + API meta paged collection: generate next, previous, first, last URL for paged collection with items, total and additional data.
    + Best practice constants.
- Core of all Elect Web modules, standalize ecosystem.

## Installation
- Package Manager
```
PM> Install-Package Elect.Web
```
- .NET CLI
```
dotnet add package Elect.Web
```

See more information in [Nuget Package](https://www.nuget.org/packages/Elect.Web/).

## Usage

- SiteMap
    + Set `SiteMapItem` attribute for `Action` in `Controller` to mask the Action is Site Map Item.
```c#
public class HomeController : Controller
{
    [SiteMapItem(SiteMapItemFrequency.Hourly, 0.9)]
    public IActionResult Index()
    {
        return View();
    }

    [SiteMapItem(SiteMapItemFrequency.Weekly, 0.9)]
    public IActionResult About()
    {
        return View();
    }
}
```
    + Add `SiteMapController` to generate Site Map Index and Site Map Index Detail.
```c#
public class SiteMapController : Controller
{
    /// <summary>
    ///     Get all site map index 
    /// </summary>
    /// <returns></returns>
    [HttpGet]
    [Route("~/sitemap.xml")]
    public ContentResult Index()
    {
        var listSiteMapIndex = new List<SiteMapIndexItemModel>
        {
            new SiteMapIndexItemModel(Url.AbsoluteAction("Main", "SiteMap"))

            // Add more site map index if needed
        }.ToArray();

        var contentResult = new SiteMapIndexGenerator().GenerateContentResult(listSiteMapIndex);

        return contentResult;
    }

    /// <summary>
    ///     Get all site map item by attributes 
    /// </summary>
    /// <returns></returns>
    [HttpGet]
    [Route("~/sitemap/main.xml")]
    public ContentResult Main()
    {
        return SiteMapHelper.GetSiteMapContentResult(Url);
    }
}
```

- API meta paged collection
```c#
// 1. Service Layers

// Queryable Data
var query = users.AsQueryable();

// Paged Result
var total = query.Count();

var data = query.OrderBy(x => x.Id).Skip(model.Skip).Take(model.Take).ToList();

var pagedResult = new PagedResponseModel<UserModel>
{
    Total = total,
    Items = data,
    AdditionalData = new Dictionary<string, object>
    {
        {"Sum of Id", data.Select(x => x.Id).DefaultIfEmpty(0).Sum()}
        // More additional Data if need
    }
};

// 2. In Controller

// Generate Paged Meta

var pagedMeta = Url.GetPagedMeta(model, pagedResult);

return Ok(pagedMeta);
```

- HttpUtils
   + [HttpRequest Helper](HttpUtils/HttpRequestHelper.cs)
   + [HttpRequest Extensions](HttpUtils/HttpRequestExtensions.cs)

- StringUtils
   + [String Helper](StringUtils/StringHelper.cs)
   + [String Extensions](StringUtils/StringExtensions.cs)

- IUrlHelperUtils
   + [IUrlHelper Extensions](IUrlHelperUtils/IUrlHelperExtensions.cs)

## [View Sample](../../../samples/Web/Elect.Sample.Web/README.md)

## License
Elect.Web is licensed under the [MIT License](../../../LICENSE).