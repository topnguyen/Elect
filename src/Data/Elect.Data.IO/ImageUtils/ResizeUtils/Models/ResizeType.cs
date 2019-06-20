#region	License
//--------------------------------------------------
// <License>
//     <Copyright> 2018 © Top Nguyen </Copyright>
//     <Url> http://topnguyen.com/ </Url>
//     <Author> Top </Author>
//     <Project> Elect </Project>
//     <File>
//         <Name> ResizeType.cs </Name>
//         <Created> 04/04/2018 5:19:11 PM </Created>
//         <Key> 2cccba72-70d0-4f63-823d-9393cfb8bd5a </Key>
//     </File>
//     <Summary>
//         ResizeType.cs is a part of Elect
//     </Summary>
// <License>
//--------------------------------------------------
#endregion License

namespace Elect.Data.IO.ImageUtils.ResizeUtils.Models
{
    /// <summary>
    ///     Enumerated resize modes to apply to resized images. 
    /// </summary>
    public enum ResizeType
    {
        /// <summary>
        ///     Crops the resized image to fit the bounds of its container. 
        /// </summary>
        Crop,

        /// <summary>
        ///     Pads the resized image to fit the bounds of its container. If only one dimension is
        ///     passed, will maintain the original aspect ratio.
        /// </summary>
        Pad,

        /// <summary>
        ///     Pads the image to fit the bound of the container without resizing the original
        ///     source. When downscaling, performs the same functionality as <see cref="ResizeType.Pad" />
        /// </summary>
        BoxPad,

        /// <summary>
        ///     Constrains the resized image to fit the bounds of its container maintaining the
        ///     original aspect ratio.
        /// </summary>
        Max,

        /// <summary>
        ///     Resizes the image until the shortest side reaches the set given dimension. Upscaling
        ///     is disabled in this mode and the original image will be returned if attempted.
        /// </summary>
        Min,

        /// <summary>
        ///     Stretches the resized image to fit the bounds of its container. 
        /// </summary>
        Stretch
    }
}