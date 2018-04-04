#region	License
//--------------------------------------------------
// <License>
//     <Copyright> 2018 © Top Nguyen </Copyright>
//     <Url> http://topnguyen.net/ </Url>
//     <Author> Top </Author>
//     <Project> Elect </Project>
//     <File>
//         <Name> CompressImageType.cs </Name>
//         <Created> 02/04/2018 8:34:44 PM </Created>
//         <Key> 9b513388-592a-440c-be37-d9db6aea1964 </Key>
//     </File>
//     <Summary>
//         CompressImageType.cs is a part of Elect
//     </Summary>
// <License>
//--------------------------------------------------
#endregion License

using System.ComponentModel;

namespace Elect.Data.IO.ImageUtils.CompressUtils.Models
{
    public enum CompressImageType
    {
        [Description("Invalid image format")]
        Invalid,

        [Description(".png")]
        Png,

        [Description(".jpeg")]
        Jpeg,

        [Description(".gif")]
        Gif
    }
}