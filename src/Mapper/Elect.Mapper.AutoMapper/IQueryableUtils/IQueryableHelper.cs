#region	License
//--------------------------------------------------
// <License>
//     <Copyright> 2018 © Top Nguyen </Copyright>
//     <Url> http://topnguyen.net/ </Url>
//     <Author> Top </Author>
//     <Project> Elect </Project>
//     <File>
//         <Name> IQueryableHelper.cs </Name>
//         <Created> 16/03/2018 10:45:37 PM </Created>
//         <Key> 6a5fb534-f11d-4116-b8ef-60fba78f75fa </Key>
//     </File>
//     <Summary>
//         IQueryableHelper.cs is a part of Elect
//     </Summary>
// <License>
//--------------------------------------------------
#endregion License

using AutoMapper.QueryableExtensions;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using AutoMapper;

namespace Elect.Mapper.AutoMapper.IQueryableUtils
{
    public class IQueryableHelper
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
        public static IQueryable<TDestination> QueryTo<TDestination>(IQueryable source, params Expression<Func<TDestination, object>>[] membersToExpand)
        {
            return QueryTo(source, global::AutoMapper.Mapper.Configuration, membersToExpand);
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
        public static IQueryable<TDestination> QueryTo<TDestination>(IQueryable source, IConfigurationProvider configurationProvider, params Expression<Func<TDestination, object>>[] membersToExpand)
        {
            return source.ProjectTo(configurationProvider, membersToExpand);
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
        public static IQueryable<TDestination> QueryTo<TDestination>(IQueryable source, IDictionary<string, object> parameters, params string[] membersToExpand)
        {
            return QueryTo<TDestination>(source, global::AutoMapper.Mapper.Configuration, parameters, membersToExpand);
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
        public static IQueryable<TDestination> QueryTo<TDestination>(IQueryable source, IConfigurationProvider configurationProvider, IDictionary<string, object> parameters, params string[] membersToExpand)
        {
            return source.ProjectTo<TDestination>(configurationProvider, parameters, membersToExpand);
        }
    }
}