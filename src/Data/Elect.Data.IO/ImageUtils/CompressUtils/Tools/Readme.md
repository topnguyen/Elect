## Mapping

- Elect_ImageCompressor_JPEG
    + Original Tool: cjpeg
        * libjpeg-62.dll = library for cjpeg for windows
    + Description: Lossy for JPEG
    + Website: https://github.com/mozilla/mozjpeg
        * https://imagecompressor.io/blog/mozjpeg-guide/
    + Version: 3.3.1 x64
    + Windows package: Elect_ImageCompressor_JPEG.exe

- Elect_ImageCompressor_JPEG_Lossless
    + Original Tool: jpegtran
    + Description: Lossless for JPEG
    + Website: https://github.com/mozilla/mozjpeg
        * https://imagecompressor.io/blog/mozjpeg-guide/
    + Version: 3.3.1 x64
    + Windows package: Elect_ImageCompressor_JPEG_Lossless.exe
---

- Elect_ImageCompressor_PNG
    + Original Tool: pngquant
    + Description: Lossy for PNG
        * BUT PNG format is a Lossless format for image.
    + Website: https://pngquant.org/
    + Version: 2.12.5
    + MacOS package: Elect_ImageCompressor_PNG
    + Windows package: Elect_ImageCompressor_PNG.exe

---
    
- Elect_ImageCompressor_GIF = gifsicle
    + Original Tool: gifsicle
    + Description: Lossy for GIF
    + Website: http://www.lcdf.org/gifsicle/
    + Version: 1.92 x64
    + MacOS package: Elect_ImageCompressor_GIF
        * How to Build and get command line tool for Linux
            + Get Github Repo
            + `cd` to the Github Folder
            + Execute `/.configure`
            + Then `make install`
        
    + Windows package: Elect_ImageCompressor_GIF.exe