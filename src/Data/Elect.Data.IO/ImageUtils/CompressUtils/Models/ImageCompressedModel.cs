namespace Elect.Data.IO.ImageUtils.CompressUtils.Models
{
    public class ImageCompressedModel : EventArgs
    {
        public MemoryStream ResultFileStream { get; set; } = new MemoryStream();

        /// <summary>
        /// Initializes a new instance of the ImageCompressedModel class.
        /// </summary>
        public ImageCompressedModel()
        {
        }

        /// <summary>
        /// Gets or sets the image file type.
        /// </summary>
        public CompressImageType FileType { get; set; }

        /// <summary>
        /// Gets the file extension based on the file type.
        /// </summary>
        public string FileExtension => FileType switch
        {
            CompressImageType.Jpeg => ".jpeg",
            CompressImageType.Png => ".png",
            CompressImageType.Gif => ".gif",
            _ => "invalid"
        };

        /// <summary>
        /// Gets or sets the original file size in bytes.
        /// </summary>
        public long OriginalFileSize { get; set; }

        /// <summary>
        /// Gets or sets the compressed file size in bytes.
        /// </summary>
        public long CompressedFileSize { get; set; }

        /// <summary>
        /// Gets or sets the total time took for compression.
        /// </summary>
        public long TotalMillisecondsTook { get; set; }

        /// <summary>
        /// Gets the difference in file size in bytes.
        /// </summary>
        public long BytesSaving => OriginalFileSize - CompressedFileSize;

        /// <summary>
        /// Gets the difference in file size as a percentage.
        /// </summary>
        public double PercentSaving =>
            BytesSaving == 0 ? 0 : Math.Round(((double)BytesSaving / OriginalFileSize) * 100, 2);

        /// <summary>
        /// Gets or sets the quality percentage used for compression.
        /// </summary>
        public int QualityPercent { get; set; }

        /// <summary>
        /// Returns a string that represents the current object.
        /// </summary>
        /// <returns>A string that represents the current object.</returns>
        public override string ToString()
        {
            // Use invariant culture for cross-platform consistency
            var str = $"{OriginalFileSize.ToString("N", CultureInfo.InvariantCulture)} bytes => {CompressedFileSize.ToString("N", CultureInfo.InvariantCulture)} bytes. " +
                      $"Saved {BytesSaving} bytes ({PercentSaving.ToString("0.##", CultureInfo.InvariantCulture)}%). " +
                      $"Took: {TotalMillisecondsTook.ToString("N", CultureInfo.InvariantCulture)} ms. " +
                      $"Compressed Image is {QualityPercent}% Quality of the Original.";
            return str;
        }
    }
}