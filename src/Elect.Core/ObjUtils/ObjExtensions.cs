#region	License
//--------------------------------------------------
// <License>
//     <Copyright> 2018 © Top Nguyen </Copyright>
//     <Url> http://topnguyen.net/ </Url>
//     <Author> Top </Author>
//     <Project> Elect </Project>
//     <File>
//         <Name> ObjExtensions.cs </Name>
//         <Created> 15/03/2018 7:13:05 PM </Created>
//         <Key> 2a1a0206-6010-46e8-af43-1b3054ef2d16 </Key>
//     </File>
//     <Summary>
//         ObjExtensions.cs is a part of Elect
//     </Summary>
// <License>
//--------------------------------------------------
#endregion License

namespace Elect.Core.ObjUtils
{
    public static class ObjExtensions
    {
        public static string ToJsonString(this object obj)
        {
            return ObjHelper.ToJsonString(obj);
        }

        public static T Clone<T>(T obj)
        {
            return ObjHelper.Clone(obj);
        }

        public static T ConvertTo<T>(this object obj)
        {
            return ObjHelper.ConvertTo<T>(obj);
        }

        public static T WithoutRefLoop<T>(this T obj)
        {
            return ObjHelper.WithoutRefLoop(obj);
        }

        public static T WithoutVirtualProp<T>(this T obj)
        {
            return ObjHelper.WithoutVirtualProp(obj);
        }
    }
}