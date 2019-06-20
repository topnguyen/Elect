#region	License

//--------------------------------------------------
// <License>
//     <Copyright> 2018 © Top Nguyen </Copyright>
//     <Url> http://topnguyen.com/ </Url>
//     <Author> Top </Author>
//     <Project> Elect </Project>
//     <File>
//         <Name> DataTableParamModel.cs </Name>
//         <Created> 23/03/2018 4:37:48 PM </Created>
//         <Key> 0e2cae5f-98a9-43cc-a6c2-3336f98e1366 </Key>
//     </File>
//     <Summary>
//         DataTableParamModel.cs is a part of Elect
//     </Summary>
// <License>
//--------------------------------------------------

#endregion License

using Elect.Web.DataTable.Models.Constants;
using Newtonsoft.Json;
using System.Collections.Generic;
using System.Runtime.InteropServices;
using Elect.Core.ObjUtils;

namespace Elect.Web.DataTable.Models.Request
{
    public class DataTableRequestModel : ElectDisposableModel
    {
        public DataTableRequestModel()
        {
            ColumnNames = new List<string>();
            ColReorderIndexs = new List<int>();
            ListIsSortable = new List<bool>();
            ListIsSearchable = new List<bool>();
            SearchValues = new List<string>();
            SortCol = new List<int>();
            SortDir = new List<string>();
            ListIsEscapeRegexColumn = new List<bool>();
        }

        public DataTableRequestModel(int columns)
        {
            Columns = columns;
            ColumnNames = new List<string>(columns);
            ColReorderIndexs = new List<int>();
            ListIsSortable = new List<bool>(columns);
            ListIsSearchable = new List<bool>(columns);
            SearchValues = new List<string>(columns);
            SortCol = new List<int>(columns);
            SortDir = new List<string>(columns);
            ListIsEscapeRegexColumn = new List<bool>(columns);
        }

        [JsonProperty(PropertyName = PropertyConstants.DisplayStart)]
        public int DisplayStart { get; set; }

        [JsonProperty(PropertyName = PropertyConstants.DisplayLength)]
        public int DisplayLength { get; set; }

        [JsonProperty(PropertyName = PropertyConstants.Columns)]
        public int Columns { get; set; }

        [JsonProperty(PropertyName = PropertyConstants.Search)]
        public string Search { get; set; }

        [JsonProperty(PropertyName = PropertyConstants.EscapeRegex)]
        public bool EscapeRegex { get; set; }

        [JsonProperty(PropertyName = PropertyConstants.SortingCols)]
        public int SortingCols { get; set; }

        [JsonProperty(PropertyName = PropertyConstants.Echo)]
        public int Echo { get; set; }

        [JsonProperty(PropertyName = PropertyConstants.ColumnNames)]
        public List<string> ColumnNames { get; set; }

        [JsonProperty(PropertyName = PropertyConstants.ColReorderIndexs)]
        public List<int> ColReorderIndexs { get; set; }

        [JsonProperty(PropertyName = PropertyConstants.Sortable)]
        public List<bool> ListIsSortable { get; set; }

        [JsonProperty(PropertyName = PropertyConstants.Searchable)]
        public List<bool> ListIsSearchable { get; set; }

        [JsonProperty(PropertyName = PropertyConstants.SearchValues)]
        public List<string> SearchValues { get; set; }

        [JsonProperty(PropertyName = PropertyConstants.SortCol)]
        public List<int> SortCol { get; set; }

        [JsonProperty(PropertyName = PropertyConstants.SortDir)]
        public List<string> SortDir { get; set; }

        [JsonProperty(PropertyName = PropertyConstants.EscapeRegexColumns)]
        public List<bool> ListIsEscapeRegexColumn { get; set; }

        /// <summary>
        ///     Store all information by key/name-value from client side 
        /// </summary>
        [JsonIgnore]
        public Dictionary<string, object> Data { get; set; }
    }
}