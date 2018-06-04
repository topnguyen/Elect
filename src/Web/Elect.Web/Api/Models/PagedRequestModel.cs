#region	License
//--------------------------------------------------
// <License>
//     <Copyright> 2018 © Top Nguyen </Copyright>
//     <Url> http://topnguyen.net/ </Url>
//     <Author> Top </Author>
//     <Project> Elect </Project>
//     <File>
//         <Name> PagedRequestModel.cs </Name>
//         <Created> 02/04/2018 10:07:20 AM </Created>
//         <Key> 53d22959-b43d-497e-9d9b-0fd311d09b96 </Key>
//     </File>
//     <Summary>
//         PagedRequestModel.cs is a part of Elect
//     </Summary>
// <License>
//--------------------------------------------------
#endregion License

namespace Elect.Web.Api.Models
{
    public class PagedRequestModel
    {
        public int Skip { get; set; } = 0;

        /// <summary>
        ///     Default is 10 item 
        /// </summary>
        public int Take { get; set; } = 10;
    }
}