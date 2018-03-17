#region	License
//--------------------------------------------------
// <License>
//     <Copyright> 2018 © Top Nguyen </Copyright>
//     <Url> http://topnguyen.net/ </Url>
//     <Author> Top </Author>
//     <Project> Elect </Project>
//     <File>
//         <Name> ObjHelper.cs </Name>
//         <Created> 16/03/2018 10:48:24 PM </Created>
//         <Key> 7fc7721b-d008-4192-aaaa-c8c99a52a9ea </Key>
//     </File>
//     <Summary>
//         ObjHelper.cs is a part of Elect
//     </Summary>
// <License>
//--------------------------------------------------
#endregion License

namespace Elect.Mapper.AutoMapper.ObjUtils
{
    public class ObjHelper
    {
        /// <summary>
        ///     Converts an object to another using AutoMapper library. Creates a new object of
        ///     <typeparamref name="TDestination" />. There must be a mapping between objects before
        ///     calling this method.
        /// </summary>
        /// <typeparam name="TDestination"> Type of the destination object </typeparam>
        /// <param name="source"> Source object </param>
        public static TDestination MapTo<TDestination>(object source) where TDestination : class, new()
        {
            return global::AutoMapper.Mapper.Map<TDestination>(source);
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
        public static TDestination MapTo<TSource, TDestination>(TSource source, TDestination destination) where TDestination : class, new()
        {
            return global::AutoMapper.Mapper.Map(source, destination);
        }
    }
}