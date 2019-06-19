using System.Linq;
using Swashbuckle.AspNetCore.Swagger;
using Swashbuckle.AspNetCore.SwaggerGen;

namespace Elect.Web.Swagger.IDocumentFilters
{
    public class SortTagsDocumentFilter : IDocumentFilter
    {
        public void Apply(SwaggerDocument swaggerDoc, DocumentFilterContext context)
        {
            swaggerDoc.Tags = swaggerDoc.Tags.OrderBy(x => x.Name).ToList();
        }
    }
}