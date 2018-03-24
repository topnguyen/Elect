#region	License
//--------------------------------------------------
// <License>
//     <Copyright> 2018 © Top Nguyen </Copyright>
//     <Url> http://topnguyen.net/ </Url>
//     <Author> Top </Author>
//     <Project> Elect </Project>
//     <File>
//         <Name> LanguageModel.cs </Name>
//         <Created> 23/03/2018 4:33:51 PM </Created>
//         <Key> 5ec209c1-c10a-4562-b832-6cd159fb50ad </Key>
//     </File>
//     <Summary>
//         LanguageModel.cs is a part of Elect
//     </Summary>
// <License>
//--------------------------------------------------
#endregion License

using Elect.Web.DataTable.Models.Constants;
using Newtonsoft.Json;

namespace Elect.Web.DataTable.Models.Menu
{
    public class LanguageModel
    {
        [JsonProperty(PropertyName = PropertyConstants.Processing)]
        public string Processing { get; set; }

        [JsonProperty(PropertyName = PropertyConstants.LengthMenu)]
        public string LengthMenu { get; set; }

        [JsonProperty(PropertyName = PropertyConstants.ZeroRecords)]
        public string ZeroRecord { get; set; }

        [JsonProperty(PropertyName = PropertyConstants.Info)]
        public string Info { get; set; }

        [JsonProperty(PropertyName = PropertyConstants.InfoEmpty)]
        public string InfoEmpty { get; set; }

        [JsonProperty(PropertyName = PropertyConstants.InfoFiltered)]
        public string InfoFiltered { get; set; }

        [JsonProperty(PropertyName = PropertyConstants.InfoPostFix)]
        public string InfoPostFix { get; set; }

        [JsonProperty(PropertyName = PropertyConstants.Search)]
        public string Search { get; set; }

        [JsonProperty(PropertyName = PropertyConstants.Url)]
        public string Url { get; set; }

        [JsonProperty(PropertyName = PropertyConstants.Paginate)]
        public PaginateModel Paginate { get; set; }
    }
}