#region	License

//--------------------------------------------------
// <License>
//     <Copyright> 2018 © Top Nguyen </Copyright>
//     <Url> http://topnguyen.com/ </Url>
//     <Author> Top </Author>
//     <Project> Elect </Project>
//     <File>
//         <Name> MvcOptionExtensions.cs </Name>
//         <Created> 24/03/2018 3:02:32 PM </Created>
//         <Key> 79021be3-5d89-4cf9-8d75-ab24ca537a4f </Key>
//     </File>
//     <Summary>
//         MvcOptionExtensions.cs is a part of Elect
//     </Summary>
// <License>
//--------------------------------------------------

#endregion License

using Elect.Web.DataTable.Processing.Request;
using Microsoft.AspNetCore.Mvc;
using System.Linq;

namespace Elect.Web.DataTable
{
    public static class MvcOptionExtensions
    {
        public static void AddDataTableModelBinder(this MvcOptions mvcOptions)
        {
            var isProviderAdded =
                mvcOptions.ModelBinderProviders.Any(x => x.GetType() == typeof(DataTablesModelBinderProvider));

            if (isProviderAdded) return;

            mvcOptions.ModelBinderProviders.Insert(0, new DataTablesModelBinderProvider());
        }
    }
}