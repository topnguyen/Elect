## PNG

[pngout](http://pngnq.sourceforge.net/)

```
 pngnq [-vfhV][-s sample factor][-e extension][-d dir][-n colours][-Q f|n][input files]
  options:
     -v Verbose mode. Prints status messages.
     -f Force ovewriting of files.
     -s Sample factor. The neuquant algorithm samples pixels stepping by this value.
     -n Number of colours the quantized image is to contain. Range: 2 to 256. Defaults to 256.
     -e Specifies the new filename extension. Defaults to -nq8.png. 
        Will drop .png from original filenames.
     -d Relocates the quantized files into this directory. 
        Otherwise output files stay in the same directory as the input files. 
     input files: The png files to be processed. Defaults to standard input if not specified.
     -Q Dithering method: f = Floyd Steinberg, n = None (default)
     -V Print version number and library versions.
     -h Print this help.
```


## PNG Optimizer

 A modified [pngnq](https://sourceforge.net/projects/pngnqs9/): convert png images to 256 colours.
 
```
	 pngnq-s9 Man Page
NAME
       pngnq-s9 - Quantize a PNG image to 256 or fewer colours using the Neuquant pro‐
       cedure.

SYNOPSIS
       pngnq OPTIONS INPUT-FILES

DESCRTPTION
       pngnq-s9 uses a neural network to choose the best combination  of  256  colours
       for  each  input file, and then redraws the input file using only those colours
       and writes the result as its output file.

       The output file name is the input file base name with a new extension appended,
       "-nq8.png" by default.

       If no input files are provided, input is read from stdin, and output is written
       to stdout.

       pngnq does not copy the structure of the original png file.   The  output  file
       will  always be indexed, and will not necessarily contain the same gamma, back‐
       ground colour or 'comment' information as the original.

OPTIONS
   Help and Information
       [-H] prints this help message.

       [-h] prints a shorter help message.

       [-V] prints version information, including library versions.

       [-v] prints verbose status messages.

   Basic Settings
       [-n NUMBER] Number of Colours
              The number of colours the quantized image is to contain, from 1  to  256
              (default).  Example: -n64 for 64 colours.

       [-s NUMBER] Sample Rate
              Tell pngnq to sample one in how many pixels from the input image.  Exam‐
              ple: -s3 samples one third of the image.  1 = slow, high quality;  10  =
              fast, lower quality.

   File Settings
       [-d DIRECTORY] Output Directory
              Stipulates  which directory the output files should be written to.  Nor‐
              mally each output file stays in the same directory as the  file  it  was
              derived from.  Example: -d ./results

       [-e EXTENSION] Output File Extension
              Specifies the new extension for quantized files.  Example: If your input
              file  is  myfile.png,  with  -e_LOW.png  the   output   file   will   be
              myfile_LOW.png. Defaults to -nq8.png

       [-f] Force Overwrite
              Forces  pngnq  to  overwrite  existing  files.  It is not recommended to
              overwrite an input file while it is in use.

   Palettes
       The user can optionally supply a palette of fixed colours to  be  used  in  the
       output  image.   The palette should be provided in the form of a png image that
       for each palette entry has exactly one pixel of that colour.   If  -n  requests
       more  colours  than  there  are  colours in the palette, then pngnq will freely
       select remaining colours as usual.  Only the colours that are  actually  needed
       will  be present in the output image, so using -n 240 with a 240 colour palette
       may result in an output file that uses only 150 colours.

       [-P PALETTE-FILENAME] User-Supplied Palette, Strict RGBA
              Uses the named palette, and keeps the exact RGBA values of  the  palette
              colours  in the output image.  (The palette colours are still gamma cor‐
              rected internally, but the procedure  is  perfectly  reversed  prior  to
              writing output.)

       [-p PALETTE-FILENAME] User-Supplied Palette, Nudge-able
              Uses  the  named  palette,  as  described  above,  except that this time
              pngnq's internal processing (mainly  gamma  correction)  is  allowed  to
              nudge the palette colours.

   Gamma
       pngnq  uses  gamma correction to help it choose and remap colours more intelli‐
       gently.  The gamma value can be specified in three ways:

       1)     Using -g or -G, in which case the same  gamma  value  is  used  for  all
              files.

       2)     In  the  absence  of  -g  or -G, if a supplied file contains an explicit
              gamma value (png gAMA), that value will be used for that file only.

       3)     In the absence of the above, we assume a gamma value of 1.8.

       To force gamma correction like a typical monitor, for example,  you  would  use
       -g2.2.

       pngnq  will not record the gamma value in the output file unless you use -G and
       provide your own explicit gamma setting.

       [-g NUMBER] Gamma Correction Value, Unrecorded
              Force the use of the supplied gamma correction value, (but don't  record
              it  in the output file).  Example: -g2.2 (monitor gamma).  Values in the
              range [0.1, 10] are accepted.

       [-G NUMBER] Gamma Correction Value, Recorded
              Force the use of the supplied gamma value, and record it in  the  output
              file  in a png gAMA chunk.  Example: -G1.0 (no gamma, recorded).  Values
              in the range [0.1, 10] are accepted.

   Transparency
       [-t NUMBER] Transparency Extenuation
              -t tries harder to keep alpha values of 255 and 0  exactly  accurate  in
              the  output  by using transparency extenuation when the quantized colour
              palette is first selected.  Example: -t8.  In general, 0  =  none,  8  =
              some, 15 = a lot.  Defaults to zero.

       [-T NUMBER] Transparency Extenuation with Strict Remapping
              -T  works  the  same  way  as  -t  when  the colour palette is initially
              selected.  But it then also tries to force alpha values of 255 and 0  to
              be  strictly retained in the output, even if that means making an other‐
              wise poor substitution - opaque red for opaque blue,  for  example.   -T
              will warn when forcing fails.

       [-A] Alpha Heuristic Off
              Turn  off the alpha colour importance heuristic. This heuristic improves
              images with semi-transparent areas, but can harm mostly grey images with
              a lot of transparency.

   Colour Selection
       [-C LETTER] Colour Space
              Selects  the  colour  space  used  for  internal processing (both colour
              selection and remapping).  Use -Cr for RGBA (default), or -Cy for  YUVA.
              Note  that  pngnq's default YUVA settings effectively allocate 8 bits of
              precision to each component - to alter the relative importance of Y,  U,
              V and A, use the sensitivity commands.

       [-u NUMBER] Un-isolate
              Un-isolates  distinct  but rarely used colours by the given factor.  Use
              -u when you notice small, important patches of colour going  missing  in
              the  output image. Values in the range [0.0, 100.0] are accepted.  7.0 =
              a little, 15.0 = some, 31.0 = a lot.  High values can result in degener‐
              ate  output.  When  -u is needed, try -u15.0 first, and work from there.
              Defaults to zero (no effect).

       [-x NUMBER] Exclusion Threshold
              Try to choose colours that differ by at least this amount  in  at  least
              one  component.  -x4.25 will choose colours about 4 steps apart, so RGBA
              (10,10,10,10)  and  (15,10,10,10)  could  both  be   chosen,   but   not
              (14,10,10,10)  as  well.   Values in the range [0.0, 32.0] are accpeted.
              Defaults to 0.5.  Use -x to push colours apart when you notice pngnq  is
              choosing too many similar colours.

       [-Q LETTER] Dither Mode
              Selects  either  Floyd-Steinberg  dithering (-Qf) or no dithering (-Qn),
              the default.  -Qf results in a default dithering  extent  equivalent  to
              -Q5, as described below.

              pngnq  tends to choose colours that result in less dithering than tradi‐
              tional quantizers.  When quantizing to a large number of colours this is
              usually a good thing, resulting in subtle dithering and smoother output.
              However, when quantizing to very few colours intense  dithering  may  be
              the best option, in which case pngnq's performance may be poor.

       [-Q NUMBER] Dither Mode and Persistence
              Turns  on Floyd-Steinberg dithering and specifies its persistence.  Per‐
              sistence values are integers in the range [1,10], -Q1 dithers with mini‐
              mal  peristence,  -Q10 with the maximum.  See above for more notes about
              dithering.

       [-L] Low Colour Mode
              Shorthand used to apply various settings  suited  to  quantizing  richly
              coloured  images to under 40 colours. -L overrides and can be overridden
              by other options, so the position of -L on the command line is  signifi‐
              cant.   Equivalent to -s1 -Cy -g1.0 -u15 -x3.125 -Q5 -0 0.5 -a 0.5 -R -0
              0.75 -a 0.75.  Not advised for images with soft chromatic variation.

   Sensitivity
       pngnq allows the individual components of the internal colour space to be given
       less  weight,  or  less  sensitivity,  in  calculations.   If  you need to show
       fine-grained variations in blue, for example, you could desensitise red,  green
       and alpha to achieve this.

       Valid  sensitivity  values  range  from 0.0625 (one-sixteenth sensitivity, much
       less accurate) to 1.0 (full sensitivity).

       Normally the same sensitivity settings are used  during  colour  selection  and
       input  image  remapping.   However  it  is  possible to change the settings for
       remapping only using -R.

       [-0 NUMBER] Sensitivity Reduction Factor for Red or Y
              Sets the sensitivity for component zero, (R in RGB, Y in YUV).  Example:
              -0 0.25 for one quarter the usual sensitivity.

       [-1 NUMBER] Sensitivity Reduction Factor for Green or U
              Sets  the sensitivity for component one, (G in RGB, U in YUV).  Example:
              -1 0.5 for half the usual sensitivity.

       [-2 NUMBER] Sensitivity Reduction Factor for Blue or V
              Sets the sensitivity for component two, (B in RGB, V in YUV).   Example:
              -2 1.0 for full sensitivity.

       [-a NUMBER] Sensitivity Reduction Factor for Alpha
              Sets  the  sensitivity for alpha.  Example: -a 0.0625 for minimal sensi‐
              tivity.

       [-R] Restrict Remaining Sensitivity Flags to Remapping
              Causes all following sensitivity flags (-0 -1 -2 -a) to  only  apply  to
              the  remapping  phase  of  processing,  not  the colour selection phase.
              Before -R, or when -R is not present, the  sensitivity  flags  apply  to
              both  colour  selection  and  remapping.   To choose colours with little
              regard to Y 'luminance', but then pay full attention to  Y  when  remap‐
              ping, you would use: -0 0.0625 -R -0 1.0

EXAMPLES
       Quantize   mypicture.png  down  to  256  colours  and  save  result  as  mypic‐
       ture-nq8.png:

              pngnq mypicture.png

       Quantize mypicture.png using 100 colours and processing  internally  using  the
       YUV colour space:

              pngnq -Cy -n100 mypicture.png

       Quantize  mypicture.png  with  reduced  sensitivity  to  alpha, but paying more
       attention to distinct yet infrequent  colours.   Write  the  result  to  mypic‐
       ture_new.png:

              pngnq -e"_new.png" -a0.5 -u8.0 mypicture.png

       Select  quantization colours for mypicture.png with blue (-2) and alpha (-a) at
       30% (0.3) sensitivity, but then remap (recolour) the input image with blue  and
       alpha at full sensitivity:

              pngnq -2 0.3 -a 0.3 -R -2 1.0 -a 1.0 mypicture.png

       Quantize  mypicture.png using only the 48 colours in mypalette.png.  Retain the
       exact RGBA values from the palette:

              pngnq -n48 -P mypalette.png mypicture.png

       Quantize mypicture.png using the 30 colours in mypalette.png plus 20 more  cho‐
       sen  by  the  the  program. Sample every input pixel for extra accuracy.  Don't
       necessarily retain the exact palette RGBA values if gamma or sensitivity reduc‐
       tion alters them:

              pngnq -s1 -n50 -p mypalette.png mypicture.png

RETURNS
       Zero  on success, EXIT_FAILURE for some errors affecting all input, or the num‐
       ber of input files affected by individual errors.

NOTES
       pngnq-s9 is used at your  own  risk,  and  carries  no  warranties  whatsoever.
       pngnq-s9 may make arbitrary assumptions in order to recover from errors such as
       quantization parameters being out of range or file names being too long.
```