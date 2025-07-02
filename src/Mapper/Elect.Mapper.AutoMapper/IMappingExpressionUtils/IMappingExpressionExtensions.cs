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
