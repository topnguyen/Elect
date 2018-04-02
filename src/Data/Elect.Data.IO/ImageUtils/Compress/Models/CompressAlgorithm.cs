#region	License
//--------------------------------------------------
// <License>
//     <Copyright> 2018 © Top Nguyen </Copyright>
//     <Url> http://topnguyen.net/ </Url>
//     <Author> Top </Author>
//     <Project> Elect </Project>
//     <File>
//         <Name> CompressAlgorithm.cs </Name>
//         <Created> 02/04/2018 8:34:26 PM </Created>
//         <Key> 7c5b151f-4505-41b4-bacc-e2e89117bc7a </Key>
//     </File>
//     <Summary>
//         CompressAlgorithm.cs is a part of Elect
//     </Summary>
// <License>
//--------------------------------------------------
#endregion License

using System.ComponentModel;

namespace Elect.Data.IO.ImageUtils.Compress.Models
{
    internal enum CompressAlgorithm
    {
        [Description("Png lossless algorithm")]
        PngPrimary,

        [Description("Png 256 bit color algorithm")]
        PngSecondary,

        [Description("Jpeg optmize algorithm")]
        Jpeg,

        [Description("Gif lossy algorithm")]
        Gif
    }
}