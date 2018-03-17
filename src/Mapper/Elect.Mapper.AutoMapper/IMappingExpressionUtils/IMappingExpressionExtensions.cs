#region	License
//--------------------------------------------------
// <License>
//     <Copyright> 2018 © Top Nguyen </Copyright>
//     <Url> http://topnguyen.net/ </Url>
//     <Author> Top </Author>
//     <Project> Elect </Project>
//     <File>
//         <Name> IMappingExpressionExtensions.cs </Name>
//         <Created> 16/03/2018 10:41:08 PM </Created>
//         <Key> e4ef8096-ed75-4275-a233-2b458f741542 </Key>
//     </File>
//     <Summary>
//         IMappingExpressionExtensions.cs is a part of Elect
//     </Summary>
// <License>
//--------------------------------------------------
#endregion License

using AutoMapper;
using System.Linq;
using System.Reflection;

namespace Elect.Mapper.AutoMapper.IMappingExpressionUtils
{
    public static class IMappingExpressionExtensions
    {
        public static IMappingExpression<TSource, TDestination> IgnoreAllNonExisting<TSource, TDestination>(this IMappingExpression<TSource, TDestination> expression)
        {
            const BindingFlags flags = BindingFlags.Public | BindingFlags.Instance;

            var sourceType = typeof(TSource);

            var destinationProperties = typeof(TDestination).GetProperties(flags);

            foreach (var property in destinationProperties)
            {
                var propInfoSrc = sourceType.GetProperties().FirstOrDefault(p => p.Name == property.Name);

                if (propInfoSrc == null)
                {
                    expression.ForMember(property.Name, opt => opt.Ignore());
                }
            }
            return expression;
        }
    }
}