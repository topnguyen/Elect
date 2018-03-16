#region	License
//--------------------------------------------------
// <License>
//     <Copyright> 2018 © Top Nguyen </Copyright>
//     <Url> http://topnguyen.net/ </Url>
//     <Author> Top </Author>
//     <Project> Elect </Project>
//     <File>
//         <Name> ActionExtensions.cs </Name>
//         <Created> 16/03/2018 2:15:17 PM </Created>
//         <Key> 53a0f116-01f0-46f8-9fa7-828eb22c12ad </Key>
//     </File>
//     <Summary>
//         ActionExtensions.cs is a part of Elect
//     </Summary>
// <License>
//--------------------------------------------------
#endregion License

using System;

namespace Elect.Core.ActionUtils
{
    public static class ActionExtensions
    {
        /// <summary>
        ///     Get instance of T from Action 
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="action"></param>
        /// <returns></returns>
        public static T GetValue<T>(this Action<T> action) where T : class, new()
        {
            T obj = (T)Activator.CreateInstance(typeof(T));

            action.DynamicInvoke(obj);

            return obj;
        }
    }
}