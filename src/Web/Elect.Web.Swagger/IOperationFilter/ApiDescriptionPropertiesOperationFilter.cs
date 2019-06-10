#region	License

//--------------------------------------------------
// <License>
//     <Copyright> 2018 © Top Nguyen </Copyright>
//     <Url> http://topnguyen.net/ </Url>
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
using Swashbuckle.AspNetCore.Swagger;
using Swashbuckle.AspNetCore.SwaggerGen;

namespace Elect.Web.Swagger.IOperationFilter
{
    public class ApiDescriptionPropertiesOperationFilter : Swashbuckle.AspNetCore.SwaggerGen.IOperationFilter
    {
        public void Apply(Operation operation, OperationFilterContext context)
        {
            context.ApiDescription.Properties.AddOrUpdate(nameof(Operation.OperationId), operation.OperationId);
            context.ApiDescription.Properties.AddOrUpdate(nameof(Operation.Summary), operation.Summary);
            context.ApiDescription.Properties.AddOrUpdate(nameof(Operation.Description), operation.Description);
        }
    }
}