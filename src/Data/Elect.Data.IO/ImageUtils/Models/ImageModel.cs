namespace Elect.Data.IO.ImageUtils.Models
{
    public class ImageModel : ElectDisposableModel
    {
        public string MimeType { get; set; }
        public string Extension { get; set; }
        public string DominantHexColor { get; set; }
        public int WidthPx { get; set; }
        public int HeightPx { get; set; }
    }
}
