#region	License
//--------------------------------------------------
// <License>
//     <Copyright> 2018 © Top Nguyen </Copyright>
//     <Url> http://topnguyen.com/ </Url>
//     <Author> Top </Author>
//     <Project> Elect </Project>
//     <File>
//         <Name> ImageResizeHelper.cs </Name>
//         <Created> 04/04/2018 5:18:55 PM </Created>
//         <Key> bfe2914a-92c5-4628-97b5-ab2ca37ecf14 </Key>
//     </File>
//     <Summary>
//         ImageResizeHelper.cs is a part of Elect
//     </Summary>
// <License>
//--------------------------------------------------
#endregion License

using Elect.Data.IO.ImageUtils.ResizeUtils.Models;
using SixLabors.ImageSharp;
using SixLabors.ImageSharp.Processing;
using System.IO;

namespace Elect.Data.IO.ImageUtils.ResizeUtils
{
    public class ImageResizeHelper
    {
        public static byte[] Resize(string path, int newWidthPx, int newHeightPx, ResizeType resizeType = ResizeType.Max)
        {
            path = PathHelper.GetFullPath(path);

            byte[] imageBytes = File.ReadAllBytes(path);

            return Resize(imageBytes, newWidthPx, newHeightPx, resizeType);
        }

        public static byte[] Resize(byte[] imageBytes, int newWidthPx, int newHeightPx, ResizeType resizeType = ResizeType.Max)
        {
            using (MemoryStream inStream = new MemoryStream(imageBytes))
            {
                using (MemoryStream outStream = new MemoryStream())
                {
                    using (var image = Image.Load<SixLabors.ImageSharp.PixelFormats.Rgba32>(inStream, out var format))
                    {
                        image.Mutate(x => x.Resize(new ResizeOptions
                        {
                            Size = new SixLabors.Primitives.Size(newWidthPx, newHeightPx),

                            Mode = (ResizeMode)((int)resizeType)
                        }));

                        image.Save(outStream, format);

                        return outStream.ToArray();
                    }
                }
            }
        }
    }
}