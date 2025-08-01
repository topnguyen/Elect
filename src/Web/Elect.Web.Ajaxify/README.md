﻿![Logo](../../../Logo.png)
# Elect.Web.Ajaxify
> Author [**Top Nguyen**](http://topnguyen.com)

## Overview
 - Make your regular website (multiple page) to single page by ajax approach.
 - The main idea is fetch HTML by Ajax Request then replace the DOM by the content.
 - **Support SEO** by change title, push history (by History.js), server render content (by default of multiple page).

## Installation
```xml
<!-- jQuery -->
<script src="https://ajax.googleapis.com/ajax/libs/jquery/3.3.1/jquery.min.js"></script>

<!-- History.js -->
<script src="~/jquery.history.js"></script>

<!-- Elect.Web.Ajaxify -->
<script src="~/elect.web.ajaxify.min.js"></script>
```

## Usage
 - The idea of Ajaxify is replace existing DOM by new HTML and keep "master layout" HTML, so you need to define your master layout as below examples structure.
 - When you finish install the Elect.Web.Ajaxify, It auto enable and change all your Http Redirect by Anchor Tag to Ajaxify concept.
 - For the Anchor Tag you would like to force redirect (reload as multiple concept) add class `no-ajaxify` to the Anchor Element.
 - To Detect Ajax request by Elect.Web.Ajaxify, please check the header key "X-Requested-With" and value exactly equal to "Elect.Web.Ajaxify".

### Example HTML
```html
<!DOCTYPE html>
<html lang="en-US">
    <head>
      <!-- TODO Meta Tags -->
      <!-- TODO Styles -->
     
      <!-- jQuery -->
      <script src="https://ajax.googleapis.com/ajax/libs/jquery/3.3.1/jquery.min.js"></script>
      
      <!-- History.js -->
      <script src="~/jquery.history.js"></script>
      
      <!-- Elect.Web.Ajaxify -->
      <script src="~/elect.web.ajaxify.min.js"></script>
      
      <!-- TODO Head Scripts -->
    </head>
    <body class="document-body">
        <div id="document-content">
            <!-- TODO HTML Body -->
            <div class="document-script">
                <div class="document-main-script-start"></div>
                <!-- TODO Page Scripts-->
                <div class="document-main-script-end"></div>
            </div>
        </div>
    </body>
</html>
```

### Example Razor

```razor
@using Elect.Web.Models
@{
    var isAjaxify = string.Compare(Context.Request.Headers[HeaderKey.XRequestedWith], "Elect.Web.Ajaxify", StringComparison.OrdinalIgnoreCase) == 0;
}
@if (isAjaxify)
{
 <text>
    <html>
    <head>
       @{
           await Html.RenderPartialAsync("_Meta").ConfigureAwait(true);
       }
    </head>
    <body class="document-body">
        <div id="document-content">
            @RenderBody()
            <div class="document-script">
                <div class="document-main-script-start"></div>
                @{
                     await Html.RenderPartialAsync("Assets/_BottomScript").ConfigureAwait(true)    ;
                }
                @await RenderSectionAsync("Scripts", false).ConfigureAwait(true)
                <div class="document-main-script-end"></div>
            </div>
        </div>
    </body>
    </html>
 </text>
}
else
{
 <text>
    <!DOCTYPE html>
    <html lang="en-US">
    <head>
        @{
            await Html.RenderPartialAsync("_Meta").ConfigureAwait(true);
            await Html.RenderPartialAsync("_Favicon").ConfigureAwait(true);
        }
        
        <!-- jQuery -->
        <script src="https://ajax.googleapis.com/ajax/libs/jquery/3.3.1/jquery.min.js"></script>
        
        <!-- History.js -->
        <script src="~/jquery.history.js"></script>
        
        <!-- Elect.Web.Ajaxify -->
        <script src="~/elect.web.ajaxify.min.js"></script>
        
        @{
            await Html.RenderPartialAsync("Assets/_TopScript").ConfigureAwait(true);
            await Html.RenderPartialAsync("Assets/_Style").ConfigureAwait(true);
            await RenderSectionAsync("Styles", false).ConfigureAwait(true);
        }
    </head>
    <body class="document-body">
        <div id="document-content">
            @RenderBody()
            <div class="document-script">
                <div class="document-main-script-start"></div>
                @{
                    await Html.RenderPartialAsync("Assets/_BottomScript").ConfigureAwait(true)    ;
                }
                @await RenderSectionAsync("Scripts", false).ConfigureAwait(true)
                <div class="document-main-script-end"></div>
            </div>
        </div>
    </body>
    </html>
 </text>
}
```

## License
Elect.Web.Ajaxify is licensed under the [MIT License](../../../LICENSE).
