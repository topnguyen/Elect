#region	License

//--------------------------------------------------
// <License>
//     <Copyright> 2018 © Top Nguyen </Copyright>
//     <Url> http://topnguyen.net/ </Url>
//     <Author> Top </Author>
//     <Project> Elect </Project>
//     <File>
//         <Name> DateTimeFormatType.cs </Name>
//         <Created> 23/03/2018 4:06:36 PM </Created>
//         <Key> 863191e6-506c-48aa-8135-b03c87650c6e </Key>
//     </File>
//     <Summary>
//         DateTimeFormatType.cs is a part of Elect
//     </Summary>
// <License>
//--------------------------------------------------

#endregion License

namespace Elect.Web.DataTable.Models.Constants
{
    public enum DateTimeFormatType
    {
        /// <summary>
        ///     Try parse DateTime from any string format
        /// </summary>
        Auto,

        /// <summary>
        ///     Parse DateTime by specific/exactly format.
        /// </summary>
        Specific
    }
}