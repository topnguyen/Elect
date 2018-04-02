#region	License
//--------------------------------------------------
// <License>
//     <Copyright> 2018 © Top Nguyen </Copyright>
//     <Url> http://topnguyen.net/ </Url>
//     <Author> Top </Author>
//     <Project> Elect </Project>
//     <File>
//         <Name> ImageModel.cs </Name>
//         <Created> 02/04/2018 8:29:00 PM </Created>
//         <Key> 07fda2b0-1a44-4252-a038-f7737cd6640b </Key>
//     </File>
//     <Summary>
//         ImageModel.cs is a part of Elect
//     </Summary>
// <License>
//--------------------------------------------------
#endregion License

namespace Elect.Data.IO.ImageUtils.Models
{
    public class ImageModel
    {
        public string MimeType { get; set; }

        public string Extension { get; set; }

        public string DominantHexColor { get; set; }

        public int WidthPx { get; set; }

        public int HeightPx { get; set; }
    }
}