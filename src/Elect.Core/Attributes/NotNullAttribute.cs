#region	License
//--------------------------------------------------
// <License>
//     <Copyright> 2018 © Top Nguyen </Copyright>
//     <Url> http://topnguyen.com/ </Url>
//     <Author> Top </Author>
//     <Project> Elect </Project>
//     <File>
//         <Name> NotNullAttribute.cs </Name>
//         <Created> 15/03/2018 5:14:35 PM </Created>
//         <Key> 77089f96-f926-413c-8473-d473ed75c9d3 </Key>
//     </File>
//     <Summary>
//         NotNullAttribute.cs is a part of Elect
//     </Summary>
// <License>
//--------------------------------------------------
#endregion License

using System;

namespace Elect.Core.Attributes
{
    /// <summary>
    ///     Indicates that the value of the marked element could never be <c> null </c> 
    /// </summary>
    [AttributeUsage(AttributeTargets.Method | AttributeTargets.Parameter | AttributeTargets.Property | AttributeTargets.Delegate | AttributeTargets.Field)]
    public sealed class NotNullAttribute : Attribute { }
}