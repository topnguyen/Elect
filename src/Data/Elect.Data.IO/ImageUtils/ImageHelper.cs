using Font = SixLabors.Fonts.Font;

namespace Elect.Data.IO.ImageUtils
{
    public class ImageHelper
    {
        #region Image Info

        /// <summary>
        ///     <para> Get image info. </para>
        ///     <para> If not know mime type but valid image then return <see cref="ImageConstants.ImageMimeTypeUnknown" /> </para>
        ///     <para> Invalid image will be return <c> NULL </c> </para>
        /// </summary>
        /// <param name="base64"></param>
        public static ImageModel GetImageInfo(string base64)
        {
            byte[] bytes = Convert.FromBase64String(base64);
            return GetImageInfo(bytes);
        }

        /// <summary>
        ///     <para> Get image info. </para>
        ///     <para> If not know mime type but valid image then return <see cref="ImageConstants.ImageMimeTypeUnknown" /> </para>
        ///     <para> Invalid image will be return <c> NULL </c> </para>
        /// </summary>
        /// <param name="bytes"></param>
        public static ImageModel GetImageInfo(byte[] bytes)
        {
            using (var stream = new MemoryStream(bytes))
            {
                return GetImageInfo(stream);
            }
        }

        /// <summary>
        ///     <para> Get image info. </para>
        ///     <para> If not know mime type but valid image then return <see cref="ImageConstants.ImageMimeTypeUnknown" /> </para>
        ///     <para> Invalid image will be return <c> NULL </c> </para>
        /// </summary>
        /// <param name="imageStream"></param>
        public static ImageModel GetImageInfo(MemoryStream imageStream)
        {
            try
            {
                ImageModel imageModel = new ImageModel();
                // Check Vector image first, if image is vector then no info for width and height
                if (IsSvgImage(imageStream))
                {
                    imageModel.MimeType = "image/svg+xml";
                }
                else
                {
                    // Raster check (jpg, png, etc.)
                    using (var image = SixLabors.ImageSharp.Image.Load(imageStream.ToArray()))
                    {
                        // Get image mime type
                        bool isUnknownMimeType = true;
                        var format = image.Metadata.DecodedImageFormat;
                        if (format != null)
                        {
                            imageModel.MimeType = format.DefaultMimeType;
                            isUnknownMimeType = false;
                        }

                        if (isUnknownMimeType)
                        {
                            imageModel.MimeType = ImageConstants.ImageMimeTypeUnknown;
                        }

                        // Get width and height in pixel info
                        imageModel.WidthPx = image.Width;
                        imageModel.HeightPx = image.Height;
                    }
                }

                // Get others info
                imageModel.Extension = MimeTypeHelper.GetExtension(imageModel.MimeType);

                // Get image dominant color
                imageModel.DominantHexColor = ImageDominantColorHelper.GetHexCode(imageStream);

                return imageModel;
            }
            catch
            {
                return null;
            }
        }

        public static bool IsSvgImage(MemoryStream imageStream)
        {
            try
            {
                imageStream.Position = 0;
                byte[] bytes = imageStream.ToArray();
                var text = Encoding.UTF8.GetString(bytes);
                bool isSvgImage = text.StartsWith("<?xml ") || text.StartsWith("<svg ");
                return isSvgImage;
            }
            catch
            {
                return false;
            }
        }

        #endregion

        #region Text Image

        /// <summary>
        ///     Generate image from text (at center of the image)
        /// </summary>
        /// <param name="text">            Will be StringHelper.Normalize(text).First().ToString() </param>
        /// <param name="width">           Default is 50 px </param>
        /// <param name="height">          Default is 50 px </param>
        /// <param name="textColor">       Default is random color </param>
        /// <param name="backgroundColor"> Default is random color </param>
        /// <param name="font">
        ///     Default is new Font(FontFamily.GenericSansSerif, 10.0F, FontStyle.Bold)
        /// </param>
        /// <returns></returns>
        public static string GenerateTextImageBase64(string text, int width = 50, int height = 50,
            Color textColor = default, Color backgroundColor = default, Font font = null)
        {
            if (font == null)
            {
                font = new Font(SixLabors.Fonts.SystemFonts.Families.FirstOrDefault(), 10.0F,
                    SixLabors.Fonts.FontStyle.Bold);
            }

            if (textColor == default)
            {
                textColor = ColorHelper.GetRandom();
            }

            if (backgroundColor == default)
            {
                backgroundColor = ColorHelper.GetRandom();
            }

            // Generate Image
            var img = GenerateTextImage(text, width, height, textColor, backgroundColor, font);
            // Convert to image array
            using (MemoryStream memoryStream = new MemoryStream())
            {
                img.Save(memoryStream, new SixLabors.ImageSharp.Formats.Jpeg.JpegEncoder());

                var imageArray = memoryStream.ToArray();
                if (!imageArray.Any())
                {
                    return null;
                }

                var stringBase64 = Convert.ToBase64String(imageArray);
                return stringBase64;
            }
        }

        /// <summary>
        ///     Generate image from text (at center of the image)
        /// </summary>
        /// <param name="text">           </param>
        /// <param name="width">          </param>
        /// <param name="height">         </param>
        /// <param name="textColor">      </param>
        /// <param name="backgroundColor"></param>
        /// <param name="font">           </param>
        /// <returns></returns>
        public static Image GenerateTextImage(string text, int width, int height, Color textColor,
            Color backgroundColor, Font font)
        {
            CheckHelper.CheckNullOrWhiteSpace(text, nameof(text));

            // Create a new ImageSharp image
            var img = new Image<Rgba32>(width, height);

            // Paint the background
            img.Mutate(ctx => ctx.Fill(backgroundColor));

            // Load font
            var fontCollection = new FontCollection();
            var fontFamily = fontCollection.AddSystemFonts().Get(font.Name);
            var imageSharpFont = new Font(fontFamily, font.Size, SixLabors.Fonts.FontStyle.Bold);

            // Measure the text size
            var textOptions = new RichTextOptions(imageSharpFont)
            {
                HorizontalAlignment = HorizontalAlignment.Center,
                VerticalAlignment = VerticalAlignment.Center,
                Origin = new SixLabors.ImageSharp.PointF(width / 2f, height / 2f),
                WrappingLength = width
            };

            // Draw the text
            img.Mutate(ctx => ctx.DrawText(textOptions, text, textColor));

            // Return the image
            return img;
        }

        #endregion

        #region Base 64

        /// <summary>
        ///     Get image base64 for "src" of "img" element in HTML.
        /// </summary>
        /// <param name="base64">        </param>
        /// <param name="imageExtension"></param>
        /// <returns></returns>
        public static string GetImageBase64Format(string base64, string imageExtension = ".jpg")
        {
            var imageMimeType = MimeTypeHelper.GetMimeType(imageExtension);
            return $@"data:{imageMimeType};base64,{base64}";
        }

        /// <summary>
        ///     Get string base64 data from "src" of "img" element in HTML.
        /// </summary>
        /// <param name="imageBase64"></param>
        /// <returns></returns>
        public static string GetBase64Format(string imageBase64)
        {
            return imageBase64.Split(',').LastOrDefault();
        }

        #endregion

        #region Rotate

        /// <summary>
        ///     Rotate image by Exif Orientation.
        /// </summary>
        /// <param name="imageBase64"></param>
        /// <remarks>This one will fix auto rotate in image uploaded from Mobile Device (iOS, Android and so on).</remarks>
        public static string RotateByExifOrientation(string imageBase64)
        {
            var fileBytes = Convert.FromBase64String(imageBase64);
            var fixedAutoRotateFileBytes = RotateByExifOrientation(fileBytes);
            return fixedAutoRotateFileBytes.ToBase64();
        }

        /// <summary>
        ///     Rotate image by Exif Orientation.
        /// </summary>
        /// <param name="imageBytes"></param>
        /// <remarks>This one will fix auto rotate in image uploaded from Mobile Device (iOS, Android and so on).</remarks>
        public static byte[] RotateByExifOrientation(byte[] imageBytes)
        {
            using (var imageStream = new MemoryStream(imageBytes))
            {
                using (var image = Image.Load(imageStream.ToArray()))
                {
                    var fixedAutoRotateImage = RotateByExifOrientation(image);
                    using (var fixedAutoRotateImageStream = new MemoryStream())
                    {
                        var imageFormat = fixedAutoRotateImage.Metadata.DecodedImageFormat;
                        if (imageFormat != null)
                        {
                            fixedAutoRotateImage.Save(fixedAutoRotateImageStream, imageFormat);
                        }
                        else
                        {
                            fixedAutoRotateImage.Save(fixedAutoRotateImageStream,
                                new SixLabors.ImageSharp.Formats.Jpeg.JpegEncoder());
                        }

                        var fixedAutoRotateFileBytes = fixedAutoRotateImageStream.ToArray();
                        return fixedAutoRotateFileBytes;
                    }
                }
            }
        }

        /// <summary>
        ///     Rotate image by Exif Orientation.
        /// </summary>
        /// <param name="image"></param>
        /// <remarks>This one will fix auto rotate in image uploaded from Mobile Device (iOS, Android and so on).</remarks>
        public static Image RotateByExifOrientation(Image image)
        {
            var fixedAutoRotateImage = image.Clone();
            var exifProfile = fixedAutoRotateImage.Metadata.ExifProfile;
            if (exifProfile == null)
            {
                return fixedAutoRotateImage;
            }

            var orientationValue = 1;
            var orientationTag = SixLabors.ImageSharp.Metadata.Profiles.Exif.ExifTag.Orientation;
            var orientationEntry = exifProfile.Values.FirstOrDefault(e => e.Tag == orientationTag);
            if (orientationEntry != null && orientationEntry.GetValue() is ushort value)
            {
                orientationValue = value;
            }

            RotateMode rotateMode = RotateMode.None;
            FlipMode flipMode = FlipMode.None;

            switch (orientationValue)
            {
                case 1: // Normal
                default:
                    break;
                case 2: // Mirror horizontal
                    flipMode = FlipMode.Horizontal;
                    break;
                case 3: // Rotate 180
                    rotateMode = RotateMode.Rotate180;
                    break;
                case 4: // Mirror vertical
                    flipMode = FlipMode.Vertical;
                    break;
                case 5: // Mirror horizontal and rotate 270 CW
                    rotateMode = RotateMode.Rotate90;
                    flipMode = FlipMode.Horizontal;
                    break;
                case 6: // Rotate 90 CW
                    rotateMode = RotateMode.Rotate90;
                    break;
                case 7: // Mirror horizontal and rotate 90 CW
                    rotateMode = RotateMode.Rotate270;
                    flipMode = FlipMode.Horizontal;
                    break;
                case 8: // Rotate 270 CW
                    rotateMode = RotateMode.Rotate270;
                    break;
            }

            if (rotateMode != RotateMode.None || flipMode != FlipMode.None)
            {
                fixedAutoRotateImage.Mutate(x =>
                {
                    if (rotateMode != RotateMode.None)
                        x.Rotate(rotateMode);
                    if (flipMode != FlipMode.None)
                        x.Flip(flipMode);
                });
            }

            fixedAutoRotateImage.Metadata.ExifProfile?.RemoveValue(orientationTag);
            return fixedAutoRotateImage;
        }

        #endregion
    }
}