#region	License
//--------------------------------------------------
// <License>
//     <Copyright> 2018 © Top Nguyen </Copyright>
//     <Url> http://topnguyen.net/ </Url>
//     <Author> Top </Author>
//     <Project> Elect </Project>
//     <File>
//         <Name> ColorHelper.cs </Name>
//         <Created> 04/04/2018 5:42:54 PM </Created>
//         <Key> 7fb4f22d-a73b-4cf9-9e6c-cf448d33b4b8 </Key>
//     </File>
//     <Summary>
//         ColorHelper.cs is a part of Elect
//     </Summary>
// <License>
//--------------------------------------------------
#endregion License

using System;
using System.Drawing;

namespace Elect.Data.IO.ImageUtils.ColorUtils
{
    public class ColorHelper
    {
        public static Color GetRandom()
        {
            Random random = new Random();

            const int max = 256;

            var red = random.Next(max);

            var green = random.Next(max);

            var blue = random.Next(max);

            Color color = Color.FromArgb(red, green, blue);

            return color;
        }
    }
}