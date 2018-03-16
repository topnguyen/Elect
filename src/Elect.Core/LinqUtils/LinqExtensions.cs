#region	License
//--------------------------------------------------
// <License>
//     <Copyright> 2018 © Top Nguyen </Copyright>
//     <Url> http://topnguyen.net/ </Url>
//     <Author> Top </Author>
//     <Project> Elect </Project>
//     <File>
//         <Name> LinqExtensions.cs </Name>
//         <Created> 15/03/2018 6:47:49 PM </Created>
//         <Key> fda1b9c5-0690-4191-a1a3-6c9b390c9d56 </Key>
//     </File>
//     <Summary>
//         LinqExtensions.cs is a part of Elect
//     </Summary>
// <License>
//--------------------------------------------------
#endregion License

using System;
using System.Collections.Generic;
using System.Linq;

namespace Elect.Core.LinqUtils
{
    public static class LinqExtensions
    {
        public static IEnumerable<TSource> DistinctBy<TSource, TKey>(this IEnumerable<TSource> source, Func<TSource, TKey> keySelector)
        {
            var seenKeys = new HashSet<TKey>();

            foreach (var element in source)
            {
                if (seenKeys.Add(keySelector(element)))
                {
                    yield return element;
                }
            }
        }

        public static IEnumerable<TSource> RemoveWhere<TSource>(this IEnumerable<TSource> source, Predicate<TSource> predicate)
        {
            return source.Where(x => !predicate(x));
        }
    }
}