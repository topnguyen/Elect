#region	License
//--------------------------------------------------
// <License>
//     <Copyright> 2018 © Top Nguyen </Copyright>
//     <Url> http://topnguyen.net/ </Url>
//     <Author> Top </Author>
//     <Project> Elect </Project>
//     <File>
//         <Name> ActionHelper.cs </Name>
//         <Created> 06/05/2018 12:35:24 PM </Created>
//         <Key> 8ef82399-0ad3-4be2-a2b2-d80cabd85b52 </Key>
//     </File>
//     <Summary>
//         ActionHelper.cs is a part of Elect
//     </Summary>
// <License>
//--------------------------------------------------
#endregion License

using System;

namespace Elect.Core.ActionUtils
{
    public class ActionHelper
    {
        /// <summary>
        ///     Get instance of T from Action 
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="action"></param>
        /// <returns></returns>
        public static T GetValue<T>(Action<T> action) where T : class, new()
        {
            T obj = (T)Activator.CreateInstance(typeof(T));

            action.DynamicInvoke(obj);

            return obj;
        }
    }
}