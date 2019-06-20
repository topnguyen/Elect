#region	License
//--------------------------------------------------
// <License>
//     <Copyright> 2018 © Top Nguyen </Copyright>
//     <Url> http://topnguyen.com/ </Url>
//     <Author> Top </Author>
//     <Project> Elect </Project>
//     <File>
//         <Name> WithoutVirtualContractResolver.cs </Name>
//         <Created> 15/03/2018 8:21:24 PM </Created>
//         <Key> 6a7f2a2b-4c39-4406-9120-17c6d503c917 </Key>
//     </File>
//     <Summary>
//         WithoutVirtualContractResolver.cs is a part of Elect
//     </Summary>
// <License>
//--------------------------------------------------
#endregion License

using Newtonsoft.Json;
using Newtonsoft.Json.Serialization;
using System.Reflection;

namespace Elect.Core.JsonContractResolver
{
    public class WithoutVirtualContractResolver : DefaultContractResolver
    {
        protected override JsonProperty CreateProperty(MemberInfo member, MemberSerialization memberSerialization)
        {
            JsonProperty prop = base.CreateProperty(member, memberSerialization);

            var propInfo = member as PropertyInfo;

            if (propInfo == null)
            {
                return prop;
            }

            if (propInfo.GetMethod.IsVirtual && !propInfo.GetMethod.IsFinal)
            {
                prop.ShouldSerialize = obj => false;
            }
            return prop;
        }
    }
}