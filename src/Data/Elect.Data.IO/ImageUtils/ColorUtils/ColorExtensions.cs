#region	License
//--------------------------------------------------
// <License>
//     <Copyright> 2018 © Top Nguyen </Copyright>
//     <Url> http://topnguyen.com/ </Url>
//     <Author> Top </Author>
//     <Project> Elect </Project>
//     <File>
//         <Name> ColorExtensions.cs </Name>
//         <Created> 02/04/2018 8:28:19 PM </Created>
//         <Key> fb85bc9a-7a69-4cbb-a3e5-f4d390d245ef </Key>
//     </File>
//     <Summary>
//         ColorExtensions.cs is a part of Elect
//     </Summary>
// <License>
//--------------------------------------------------
#endregion License

namespace Elect.Data.IO.ImageUtils.ColorUtils
{
    public static class ColorExtensions
    {
        public static string ToHexCode(this System.Drawing.Color color)
        {
            return "#" + color.R.ToString("X2") + color.G.ToString("X2") + color.B.ToString("X2");
        }
    }
}