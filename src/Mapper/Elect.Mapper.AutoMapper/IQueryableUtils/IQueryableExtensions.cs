namespace Elect.Mapper.AutoMapper.IQueryableUtils
{
    public static class IQueryableExtensions
    {
        /// <summary>
        ///     Extension method to project from a queryable using the provided mapping engine 
        /// </summary>
        /// <remarks> Projections are only calculated once and cached </remarks>
        /// <typeparam name="TDestination"> Destination type </typeparam>
        /// <param name="source">          Queryable source </param>
        /// <param name="membersToExpand"> Explicit members to expand </param>
        /// <returns>
        ///     Queryable result, use queryable extension methods to project and execute result
        /// </returns>
        public static IQueryable<TDestination> QueryTo<TDestination>(this IQueryable source, params Expression<Func<TDestination, object>>[] membersToExpand)
        {
            return IQueryableHelper.QueryTo(source, membersToExpand);
        }
        /// <summary>
        ///     Extension method to project from a queryable using the provided mapping engine 
        /// </summary>
        /// <remarks> Projections are only calculated once and cached </remarks>
        /// <typeparam name="TDestination"> Destination type </typeparam>
        /// <param name="source">          Queryable source </param>
        /// <param name="configurationProvider"></param>
        /// <param name="membersToExpand"> Explicit members to expand </param>
        /// <returns>
        ///     Queryable result, use queryable extension methods to project and execute result
        /// </returns>
        public static IQueryable<TDestination> QueryTo<TDestination>(this IQueryable source, IConfigurationProvider configurationProvider, params Expression<Func<TDestination, object>>[] membersToExpand)
        {
            return IQueryableHelper.QueryTo(source, configurationProvider, membersToExpand);
        }
        /// <summary>
        ///     Projects the source type to the destination type given the mapping configuration 
        /// </summary>
        /// <typeparam name="TDestination"> Destination type to map to </typeparam>
        /// <param name="source">          Queryable source </param>
        /// <param name="parameters">     
        ///     Optional parameter object for parameter mapping expressions
        /// </param>
        /// <param name="membersToExpand"> Explicit members to expand </param>
        /// <returns>
        ///     Queryable result, use queryable extension methods to project and execute result
        /// </returns>
        public static IQueryable<TDestination> QueryTo<TDestination>(this IQueryable source, IDictionary<string, object> parameters, params string[] membersToExpand)
        {
            return IQueryableHelper.QueryTo<TDestination>(source, parameters, membersToExpand);
        }
        /// <summary>
        ///     Projects the source type to the destination type given the mapping configuration 
        /// </summary>
        /// <typeparam name="TDestination"> Destination type to map to </typeparam>
        /// <param name="source">          Queryable source </param>
        /// <param name="configurationProvider"></param>
        /// <param name="parameters">     
        ///     Optional parameter object for parameter mapping expressions
        /// </param>
        /// <param name="membersToExpand"> Explicit members to expand </param>
        /// <returns>
        ///     Queryable result, use queryable extension methods to project and execute result
        /// </returns>
        public static IQueryable<TDestination> QueryTo<TDestination>(this IQueryable source, IConfigurationProvider configurationProvider, IDictionary<string, object> parameters, params string[] membersToExpand)
        {
            return IQueryableHelper.QueryTo<TDestination>(source, configurationProvider, parameters, membersToExpand);
        }
    }
}
