## Important Note

All `.dll` and `.exe` files in `ElectImageCompressorTools` folder must set `Build Action` is `Embedded resource`

## Libs

- gifsicle - 1.8.8 http://www.lcdf.org/gifsicle/
- giflossy base gifsicle - https://github.com/pornel/giflossy/releases
- pngout - 13/02/2015 http://advsys.net/ken/utils.htm#pngout
- jpegoptim.exe - 1.4.3 https://github.com/vikas5914/jpegoptim-win32, https://www.kapadiya.net/jpegoptim-win32/binaries.html
- pngqn-s9 - 2.0.2  http://pngnq.sourceforge.net/
- pngquant - 2.8.1 https://pngquant.org/

## Mapping

- Elect_ImageCompressor_GIF = gifsicle
- Elect_ImageCompressor_JPEG = cjpeg
- libjpeg-62.dll = cjpeg lib
- Elect_ImageCompressor_JPEG_Optimize = jpegoptim
- Elect_ImageCompressor_PNG_Primary = pngquant
- Elect_ImageCompressor_PNG_Secondary = pngqn
- Elect_ImageCompressor_PNG_Optimize = pngout