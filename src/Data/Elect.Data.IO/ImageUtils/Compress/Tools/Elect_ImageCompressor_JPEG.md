## JPEG

cjpeg 3.1 - mozjpeg_3.1_x86 => to compress

```
  -quality N[,...]   Compression quality (0..100; 5-95 is useful range)
  -grayscale     Create monochrome JPEG file
  -rgb           Create RGB JPEG file
  -optimize      Optimize Huffman table (smaller file, but slow compression, enabled by default)
  -progressive   Create progressive JPEG file (enabled by default)
  -baseline      Create baseline JPEG file (disable progressive coding)
  -targa         Input file is Targa format (usually not needed)
  -revert        Revert to standard defaults (instead of mozjpeg defaults)
  -fastcrush     Disable progressive scan optimization
  -dc-scan-opt   DC scan optimization mode
                 - 0 One scan for all components
                 - 1 One scan per component (default)
                 - 2 Optimize between one scan for all components and one scan for 1st component
                     plus one scan for remaining components
  -notrellis     Disable trellis optimization
  -trellis-dc    Enable trellis optimization of DC coefficients (default)
  -notrellis-dc  Disable trellis optimization of DC coefficients
  -tune-psnr     Tune trellis optimization for PSNR
  -tune-hvs-psnr Tune trellis optimization for PSNR-HVS (default)
  -tune-ssim     Tune trellis optimization for SSIM
  -tune-ms-ssim  Tune trellis optimization for MS-SSIM
Switches for advanced users:
  -noovershoot   Disable black-on-white deringing via overshoot
  -arithmetic    Use arithmetic coding
  -dct int       Use integer DCT method (default)
  -dct fast      Use fast integer DCT (less accurate)
  -dct float     Use floating-point DCT method
  -quant-baseline Use 8-bit quantization table entries for baseline JPEG compatibility
  -quant-table N Use predefined quantization table N:
                 - 0 JPEG Annex K
                 - 1 Flat
                 - 2 Custom, tuned for MS-SSIM
                 - 3 ImageMagick table by N. Robidoux
                 - 4 Custom, tuned for PSNR-HVS
                 - 5 Table from paper by Klein, Silverstein and Carney
  -restart N     Set restart interval in rows, or in blocks with B
  -smooth N      Smooth dithered input (N=1..100 is strength)
  -maxmemory N   Maximum memory to use (in kbytes)
  -outfile name  Specify name for output file
  -memdst        Compress to memory instead of file (useful for benchmarking)
  -verbose  or  -debug   Emit debug output
  -version       Print version information and exit
Switches for wizards:
  -qtables file  Use quantization tables given in file
  -qslots N[,...]    Set component quantization tables
  -sample HxV[,...]  Set component sampling factors
  -scans file    Create multi-scan JPEG per script file

```

---

## JPEG Optimizer

jpg optimier => to oprimize

```
NAME
       TN_jpeg_compressor - utility to optimize/compress JPEG/JFIF files.


SYNOPSIS
       TN_jpeg_compressor [ options ] [ filenames ]


DESCRIPTION
       TN_jpeg_compressor  is  used  to  optimize/compress jpeg files. Program supports
       lossless optimization, which is based on optimizing the Huffman tables.
       And  so  called  "lossy"  optimization  where in addition to optimizing
       Huffman tables user can specify upperlimit for image quality.



OPTIONS
       Options may be either the traditional POSIX one letter options, or  the
       GNU style long options.  POSIX style options start with a single ``-'',
       while GNU long options start with ``--''.

       Options offered by TN_jpeg_compressor are the following:

       -d<path>, --dest=<path>
             Sets alternative destination directory where  to  save  optimized
             files  (default  is to overwrite the originals). Please note that
             unchanged files won't be added to the destination directory. This
             means  if  the  source  file can't be compressed, no file will be
             created in the destination path.

       -f, --force
             Force optimization, even if the result would be larger  than  the
             original file.

       -h, --help
             Displays short usage information and exits.

       -m<quality>, --max=<quality>
             Sets  the  maximum  image quality factor (disables lossless opti-
             mization mode, which is by default  enabled).  This  option  will
             reduce quality of those source files that were saved using higher
             quality setting.  While files that  already  have  lower  quality
             setting  will  be  compressed  using  the  lossless  optimization
             method.

             Valid values for quality parameter are: 0 - 100

       -n, --noaction
             Don't really optimize files, just print results.

       -S<size>, --size=<size>
             Try to optimize file  to  given  size  (disables  lossless  opti-
             mizaiont mode). Target size is specified either in kilobytes (1 -
             n) or as percentage (1% - 99%) of the original file size.

       -T<treshold>, --threshold=<treshold>
             Keep the file unchanged if the compression gain is lower than the
             threshold (%).

             Valid values for treshold are: 0 - 100

       -o, --overwrite
             Overwrite target file even if it exists (when using -d option).

       -p, --preserve
             Preserve file modification times.

       -q, --quiet
             Quiet mode.

       -t, --totals
             Print totals after processing all files.

       -v, --verbose
             Enables verbose mode (positively chatty).


       --all-normal
             Force all output files to be non-progressive. Can be used to con-
             vert all input files to progressive JPEGs when used with  --force
             option.

       --all-progressive
             Force  all output files to be progressive. Can be used to convert
             all input files to normal (non-progressive) JPEGs when used  with
             --force option.


       --strip-all
             Strip  all  (Comment  & Exif) markers from output file. (NOTE! by
             default only Comment & Exif markers are kept, everything else  is
             discarded)

       --strip-com
             Strip Comment (COM) markers from output file.

       --strip-exif
             Strip EXIF markers from output file.

       --strip-iptc
             Strip IPTC markers from output file.

       --strip-icc
             Strip ICC profiles from output file.



BUGS
       When  using  --size  option,  resulting  file is not always exactly the
       requested size. Workaround is to re-run  TN_jpeg_compressor  on  the  same  file
       again which often will result file closer to target size.
```