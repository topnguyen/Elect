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
