#region	License
//--------------------------------------------------
// <License>
//     <Copyright> 2018 © Top Nguyen </Copyright>
//     <Url> http://topnguyen.net/ </Url>
//     <Author> Top </Author>
//     <Project> Elect </Project>
//     <File>
//         <Name> SiteMapItemActionModel.cs </Name>
//         <Created> 21/03/2018 2:29:04 PM </Created>
//         <Key> 91c55c23-4e03-43c5-8713-dac5b3fed625 </Key>
//     </File>
//     <Summary>
//         SiteMapItemActionModel.cs is a part of Elect
//     </Summary>
// <License>
//--------------------------------------------------
#endregion License

using System;
using System.Reflection;

namespace Elect.Web.SiteMap.Models
{
    public class SiteMapItemActionModel
    {
        public MethodInfo Action { get; set; }

        public Type Controller { get; set; }

        public double Priority { get; set; }

        public SiteMapItemFrequency Frequency { get; set; }
    }
}