#region	License
//--------------------------------------------------
// <License>
//     <Copyright> 2018 © Top Nguyen </Copyright>
//     <Url> http://topnguyen.net/ </Url>
//     <Author> Top </Author>
//     <Project> Elect </Project>
//     <File>
//         <Name> ImageInfoModel.cs </Name>
//         <Created> 04/04/2018 5:56:33 PM </Created>
//         <Key> ded6dace-9cee-493f-80b8-4c8e23f7a0d4 </Key>
//     </File>
//     <Summary>
//         ImageInfoModel.cs is a part of Elect
//     </Summary>
// <License>
//--------------------------------------------------
#endregion License

namespace Elect.Data.IO.ImageUtils.Models
{
    public class ImageInfoModel
    {
        public string MimeType { get; set; }

        public string Extension { get; set; }

        public string DominantHexColor { get; set; }

        public int WidthPx { get; set; }

        public int HeightPx { get; set; }
    }
}