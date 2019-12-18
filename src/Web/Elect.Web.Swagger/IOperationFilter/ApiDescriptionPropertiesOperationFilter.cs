#region	License

//--------------------------------------------------
// <License>
//     <Copyright> 2018 © Top Nguyen </Copyright>
//     <Url> http://topnguyen.com/ </Url>
//     <Author> Top </Author>
//     <Project> Elect </Project>
//     <File>
//         <Name> ApiDocGroupOperationFilter.cs </Name>
//         <Created> 10/04/2018 4:10:27 PM </Created>
//         <Key> 1e25bcf7-26c3-43ab-8936-019614e62f20 </Key>
//     </File>
//     <Summary>
//         ApiDocGroupOperationFilter.cs is a part of Elect
//     </Summary>
// <License>
//--------------------------------------------------

#endregion License

using Elect.Core.DictionaryUtils;
using Microsoft.OpenApi.Models;
using Swashbuckle.AspNetCore.SwaggerGen;

namespace Elect.Web.Swagger.IOperationFilter
{
    public class ApiDescriptionPropertiesOperationFilter : Swashbuckle.AspNetCore.SwaggerGen.IOperationFilter
    {
        public void Apply(OpenApiOperation operation, OperationFilterContext context)
        {
            if (!string.IsNullOrWhiteSpace(operation.OperationId))
            {
                context.ApiDescription.Properties.AddOrUpdate(nameof(operation.OperationId), operation.OperationId);
            }
            
            if (!string.IsNullOrWhiteSpace(operation.Summary))
            {
                context.ApiDescription.Properties.AddOrUpdate(nameof(operation.Summary), operation.Summary);
            }
            
            if (!string.IsNullOrWhiteSpace(operation.Description))
            {
                context.ApiDescription.Properties.AddOrUpdate(nameof(operation.Description), operation.Description);
            }
        }
    }
}