namespace Elect.Data.IO.ImageUtils.CompressUtils.Models
{
    public class ImageCompressedModel : EventArgs
    {
        public MemoryStream ResultFileStream { get; set; } = new MemoryStream();
        /// <inheritdoc />
        /// <summary>
        ///     Initializes a new instance of the
        ///     <see cref="ImageCompressedModel" /> class.
        /// </summary>
        /// <param name="filePath">               The result file name. </param>
        /// <param name="fileSizeBeforeCompress">
        ///     The original file fileSizeBeforeCompress in bytes.
        /// </param>
        public ImageCompressedModel(string filePath, long fileSizeBeforeCompress)
        {
            OriginalFileSize = fileSizeBeforeCompress;
            FileInfo result = new FileInfo(filePath);
            if (!result.Exists)
            {
                return;
            }
            CompressedFileSize = result.Length;
        }
        /// <summary>
        ///     Gets or sets the original file size in bytes. 
        /// </summary>
        public CompressImageType FileType { get; set; }
        public string FileExtension => FileType.AsString(EnumFormat.Description);
        /// <summary>
        ///     Gets or sets the original file size in bytes. 
        /// </summary>
        public long OriginalFileSize { get; set; }
        /// <summary>
        ///     Gets or sets the result file size in bytes. 
        /// </summary>
        public long CompressedFileSize { get; set; }
        /// <summary>
        ///     Gets or sets the total time took. 
        /// </summary>
        public long TotalMillisecondsTook { get; set; }
        /// <summary>
        ///     Gets the difference in file size in bytes. 
        /// </summary>
        public long BytesSaving => OriginalFileSize - CompressedFileSize;
        /// <summary>
        ///     Gets the difference in file size as a percentage. 
        /// </summary>
        public double PercentSaving =>
            BytesSaving == 0 ? 0 : Math.Round(((double) BytesSaving / OriginalFileSize) * 100, 2);
        public int QualityPercent { get; set; }
        /// <summary>
        ///     Returns a string that represents the current object. 
        /// </summary>
        /// <returns> A string that represents the current object. </returns>
        public override string ToString()
        {
            var str = $"{OriginalFileSize:N} bytes => {CompressedFileSize:N} bytes. " +
                      $"Saved {BytesSaving} bytes ({PercentSaving:0.##} %). " +
                      $"Took: {TotalMillisecondsTook:N} ms. " +
                      $"Compressed Image is {QualityPercent} % Quality of the Original.";
            return str;
        }
    }
}
