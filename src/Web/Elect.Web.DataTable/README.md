![Logo](../../../Logo.png)
# Elect.Web.DataTable
> Author [**Top Nguyen**](http://topnguyen.net)

## Overview

[jQuery DataTable](https://datatables.net/) generate by Asp Net Core Server.

- Support global and column filter.
- Support sort by column.
- Support column visible configuration.
- Support Localization.

## Installation
- Package Manager
```
PM> Install-Package Elect.Web.DataTable
```
- .NET CLI
```
dotnet add package Elect.Web.DataTable
```

See more information in [Nuget Package](https://www.nuget.org/packages/Elect.Web.DataTable/).

## Usage

1. Add service: `services.AddElectDataTable()`. You can config for Localization, DateTime format for DataTable by [`ElectDataTableOptions`](Models/Options/ElectDataTableOptions.cs) options. 

2. Config master UI and Layout for DataTable.
  - Copy DataTable HTML and JS config: [`_DataTableHtml.cshtml`](Assets/_DataTableHtml.cshtml) and [`_DataTableScript.cshtml`](Assets/_DataTableScript.cshtml) to `Views/Shared/Datatable`.
  - Copy [`jquery.dataTables.columnFilter.js`](Assets/jquery.dataTables.columnFilter.js) to `wwwroot`.
  - Feel free to change the config adapt with your UI.
  - Add require JS and CSS for Jquery and Jquery Datatable in your `_Layout.cshtml`
```html
<!-- Jquery -->
<script src="https://code.jquery.com/jquery-3.3.1.min.js" integrity="sha256-FgpCb/KJQlLNfOu91ta32o/NMZxltwRo8QtmkMRdAu8=" crossorigin="anonymous"></script>

<!-- DataTable -->
<style src="https://cdn.datatables.net/1.10.16/css/jquery.dataTables.min.css"></style>
<script src="https://cdn.datatables.net/1.10.16/js/jquery.dataTables.min.js"></script>

<!-- DataTable - Column Filter -->
<!-- This one copy from Assets folder -->
<script src="~/jquery.dataTables.columnFilter.js"></script>

<!-- DataTable - ColVis -->
<script src="https://cdn.datatables.net/buttons/1.5.1/js/buttons.colVis.min.js"></script>

<!-- DataTable - Buttons -->
<style src="https://cdn.datatables.net/buttons/1.5.1/css/buttons.dataTables.min.css"></style>
<script src="https://cdn.datatables.net/buttons/1.5.1/js/dataTables.buttons.min.js"></script>

<!-- Explore more DataTable Plugin in https://datatables.net/download/release -->
```

3. Config DataTable options for specific Model
  - Each DataTable will have a Model map with Class in C#.
  - Use [DataTable](Attributes/DataTableAttribute.cs) Attribute to config column in Model Property.
  - You can mask the property not generate to column in DataTable by [DataTableIgnore](Attributes/DataTableIgnoreAttribute.cs) Attribute
  - You can config the row id follow data of a property instead of default increase row id by [DataTableRowId](Attributes/DataTableRowIdAttribute.cs) Attribute.
```csharp
public class UserModel
{
    [DataTable(IsVisible = false)]
    [DataTableRowId]
    public int Id { get; set; }

    [DataTable(DisplayName = "Name")]
    public string FullName { get; set; }

    [DataTable(DisplayName = "Created At")]
    public DateTimeOffset CreatedTime { get; set; }

    [DataTable(DisplayName = "Actived")]
    public bool IsActive { get; set; }
}
```

4. Add `Action` to get Datatable Data in your `Controller`.
```csharp
    /// <summary>
    ///     Get Users DataTable 
    /// </summary>
    /// <returns></returns>
    [Route("datatable")]
    [HttpPost]
    public DataTableActionResult<UserModel> GetDataTable([FromForm] DataTableRequestModel model)
    {
        // 1. In Service Layer
        DataTableResponseModel<UserModel> response = GetDataTableResponse(model);

        // 2. In Controller

        var result = response.GetDataTableActionResult(x => new
        {
            IsActive = x.IsActive ? "Yes" : "No" // Transform Data before Response
        });

        return result;
    }

    private DataTableResponseModel<UserModel> GetDataTableResponse(DataTableRequestModel model)
    {
        // Sample Data
        var users = new List<UserModel>();

        for (int i = 0; i < 1000; i++)
        {
            users.Add(new UserModel
            {
                Id = i + 1,
                FullName = $"User {i + 1}",
                CreatedTime = DateTimeOffset.Now,
                IsActive = i  % 2 == 0
            });
        }

        // Queryable Data
        var query = users.AsQueryable();

        // Generate DataTable Response
        var result = query.GetDataTableResponse(model);

        return result;
    }
```

5. View [`Sample.cshtml`](Assets/Sample.cshtml) to known how to use [`_DataTableHtml.cshtml`](Assets/_DataTableHtml.cshtml) and [`_DataTableScript.cshtml`](Assets/_DataTableScript.cshtml) to generate configuration for Jquery Datatbase when response.

```csharp
@{
    // Get model
    var model = Html.DataTableModel(Guid.NewGuid().ToString("N"), (HomeController controller) => controller.GetDataTable(null));

    // Global Configuration
    model.IsDevelopMode = true;
    model.IsUseColumnFilter = true;
    model.PageSize = 10;
    model.BeforeSendFunctionName = "beforeSendHandle";

    // Additional Columns
    model.Columns.Add(new ColumnModel("Action", typeof(string))
    {
        DisplayName = "Action Col",
        IsSearchable = false,
        IsSortable = false,
        MRenderFunction = "actionColRender"
    });

    // Render Functions
    model.Columns.Single(x => x.Name == nameof(UserModel.IsActive)).MRenderFunction = "isActiveRender";
}

<h1>DataTables</h1>

<div>
    @await Html.PartialAsync("~/Views/Shared/DataTable/_DataTableHtml.cshtml", model).ConfigureAwait(true)
</div>

@await Html.PartialAsync("~/Views/Shared/DataTable/_DataTableScript.cshtml", model).ConfigureAwait(true)

<script type="text/javascript">
    function beforeSendHandle(data) {
        data.push({
            name: "newData",
            value: "test modify data before send"
        });
        console.log("before send handle: ", data);
    }

    function actionColRender(data, type, row) {
        return "<button>Acion for " + row[0] + "</button>";
    }

    function isActiveRender(data, type, row) {

        if (data === "Yes") {
            return data;
        }

        return "<span style='color:red'>" + data+"</span>";
    }
</script>
```

## Result

![Sample.png](../../../samples/Web/Elect.Sample.Web.DataTable/Sample.png)

## [View Sample](../../../samples/Web/Elect.Sample.Web.DataTable/README.md)

## License
Elect.Web.DataTable is licensed under the [MIT License](../../../LICENSE).