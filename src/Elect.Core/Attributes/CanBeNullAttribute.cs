#region	License
//--------------------------------------------------
// <License>
//     <Copyright> 2018 © Top Nguyen </Copyright>
//     <Url> http://topnguyen.net/ </Url>
//     <Author> Top </Author>
//     <Project> Elect </Project>
//     <File>
//         <Name> CanBeNullAttribute.cs </Name>
//         <Created> 15/03/2018 5:16:16 PM </Created>
//         <Key> 34c5a6b6-0ed7-4b29-92c1-809de8225d32 </Key>
//     </File>
//     <Summary>
//         CanBeNullAttribute.cs is a part of Elect
//     </Summary>
// <License>
//--------------------------------------------------
#endregion License

using System;

namespace Elect.Core.Attributes
{
    /// <summary>
    ///     Indicates that the value of the marked element could be <c> null </c> sometimes, so the
    ///     check for <c> null </c> is necessary before its usage
    /// </summary>
    [AttributeUsage(AttributeTargets.Method | AttributeTargets.Parameter | AttributeTargets.Property | AttributeTargets.Delegate | AttributeTargets.Field)]
    public sealed class CanBeNullAttribute : Attribute { }
}