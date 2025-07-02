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
                    using (var image = Image.FromStream(imageStream))
                    {
                        // Get image mime type
                        bool isUnknownMimeType = true;
                        foreach (var imageCodecInfo in ImageCodecInfo.GetImageDecoders())
                        {
                            if (imageCodecInfo.FormatID == image.RawFormat.Guid)
                            {
                                imageModel.MimeType = imageCodecInfo.MimeType;
                                isUnknownMimeType = false;
                                break;
                            }
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
                using (var bitmap = new Bitmap(imageStream))
                {
                    imageModel.DominantHexColor = ImageDominantColorHelper.GetHexCode(bitmap);
                }
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
        public static string GenerateTextImageBase64(string text, int width = 50, int height = 50, Color textColor = default, Color backgroundColor = default, Font font = null)
        {
            if (font == null)
            {
                font = new Font(FontFamily.GenericSansSerif, 10.0F, FontStyle.Bold);
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
                img.Save(memoryStream, img.RawFormat);
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
        public static Image GenerateTextImage(string text, int width, int height, Color textColor, Color backgroundColor, Font font)
        {
            CheckHelper.CheckNullOrWhiteSpace(text, nameof(text));
            // Create a dummy bitmap just to get a graphics object
            Image img = new Bitmap(1, 1);
            Graphics drawing = Graphics.FromImage(img);
            // Measure the string to see how big the image needs to be
            drawing.MeasureString(text, font);
            // Free up the dummy image and old graphics object
            img.Dispose();
            drawing.Dispose();
            // Create a new image of the right size
            img = new Bitmap(height, width);
            drawing = Graphics.FromImage(img);
            // Paint the background
            drawing.Clear(backgroundColor);
            // Create a brush for the text
            Brush brush = new SolidBrush(textColor);
            // String alignment
            StringFormat stringFormat = new StringFormat
            {
                LineAlignment = StringAlignment.Center,
                Alignment = StringAlignment.Center
            };
            // Rectangular
            RectangleF rectangleF = new RectangleF(0, 0, img.Width, img.Height);
            // Draw text on image
            drawing.DrawString(text, font, brush, rectangleF, stringFormat);
            // Save drawing
            drawing.Save();
            // Dispose
            brush.Dispose();
            drawing.Dispose();
            // return image
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
                var originalImage = Image.FromStream(imageStream);
                var fixedAutoRotateImage = RotateByExifOrientation(originalImage);
                using (var fixedAutoRotateImageStream = new MemoryStream())
                {
                    fixedAutoRotateImage.Save(fixedAutoRotateImageStream, ImageFormat.Jpeg);
                    var fixedAutoRotateFileBytes = fixedAutoRotateImageStream.ToArray();
                    return fixedAutoRotateFileBytes;
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
            const int orientationId = 0x0112;
            var fixedAutoRotateImage = (Image)image.Clone();
            if (!fixedAutoRotateImage.PropertyIdList.Contains(orientationId))
            {
                return fixedAutoRotateImage;
            }
            int rotationValue = fixedAutoRotateImage.GetPropertyItem(orientationId).Value[0];
            var rotateFlipType = RotateFlipType.RotateNoneFlipNone;
            switch (rotationValue)
            {
                case 1: // landscape, do nothing
                default:
                {
                    break;
                }
                case 2:
                {
                    rotateFlipType = RotateFlipType.RotateNoneFlipX;
                    break;
                }
                case 3: // bottoms up
                {
                    rotateFlipType = RotateFlipType.Rotate180FlipNone;
                    break;
                }
                case 4:
                {
                    rotateFlipType = RotateFlipType.Rotate180FlipX;
                    break;
                }
                case 5:
                {
                    rotateFlipType = RotateFlipType.Rotate90FlipX;
                    break;
                }
                case 6: // rotated 90 left
                {
                    rotateFlipType = RotateFlipType.Rotate90FlipNone;
                    break;
                }
                case 7:
                {
                    rotateFlipType = RotateFlipType.Rotate270FlipX;
                    break;
                }
                case 8: // rotated 90 right
                {
                    rotateFlipType =RotateFlipType.Rotate270FlipNone;
                    break;
                }
            }
            if (rotateFlipType == RotateFlipType.RotateNoneFlipNone)
            {
                return fixedAutoRotateImage;
            }
            fixedAutoRotateImage.RotateFlip(rotateFlipType);
            fixedAutoRotateImage.RemovePropertyItem(orientationId);
            return fixedAutoRotateImage;
        }
        #endregion
    }
}
