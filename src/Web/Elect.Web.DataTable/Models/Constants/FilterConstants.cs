#region	License
//--------------------------------------------------
// <License>
//     <Copyright> 2018 © Top Nguyen </Copyright>
//     <Url> http://topnguyen.net/ </Url>
//     <Author> Top </Author>
//     <Project> Elect </Project>
//     <File>
//         <Name> FilterConstants.cs </Name>
//         <Created> 23/03/2018 4:01:22 PM </Created>
//         <Key> a86aaf9c-6807-4106-afbe-ec2a3e99ba4a </Key>
//     </File>
//     <Summary>
//         FilterConstants.cs is a part of Elect
//     </Summary>
// <License>
//--------------------------------------------------
#endregion License

namespace Elect.Web.DataTable.Models.Constants
{
    public class FilterConstants
    {
        #region Types

        public const string None = "none";
        public const string Select = "select";
        public const string Text = "text";
        public const string Checkbox = "checkbox";
        public const string Number = "number";
        public const string NumberRange = "number-range";
        public const string DateRange = "date-range";
        public const string DateTimeRange = "datetime-range";

        #endregion

        #region Property

        public const string Type = "type";
        public const string Values = "values";
        public const string PlaceHolder = "sPlaceHolder";
        public const string UseColVis = "bUseColVis";
        public const string Columns = "aoColumns";

        #endregion
    }
}