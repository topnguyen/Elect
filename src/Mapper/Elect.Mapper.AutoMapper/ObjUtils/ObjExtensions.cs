namespace Elect.Mapper.AutoMapper.ObjUtils
{
    public static class ObjExtensions
    {
        /// <summary>
        ///     Converts an object to another using AutoMapper library. Creates a new object of
        ///     <typeparamref name="TDestination" />. There must be a mapping between objects before
        ///     calling this method.
        /// </summary>
        /// <typeparam name="TDestination"> Type of the destination object </typeparam>
        /// <param name="source"> Source object </param>
        public static TDestination MapTo<TDestination>(this object source) where TDestination : class, new()
        {
            return ObjHelper.MapTo<TDestination>(source);
        }
        /// <summary>
        ///     Execute a mapping from the source object to the existing destination object There
        ///     must be a mapping between objects before calling this method.
        /// </summary>
        /// <typeparam name="TSource"> Source type </typeparam>
        /// <typeparam name="TDestination"> Destination type </typeparam>
        /// <param name="source">      Source object </param>
        /// <param name="destination"> Destination object </param>
        /// <returns></returns>
        public static TDestination MapTo<TSource, TDestination>(this TSource source, TDestination destination) where TDestination : class, new()
        {
            return ObjHelper.MapTo(source, destination);
        }
    }
}
