#region	License
//--------------------------------------------------
// <License>
//     <Copyright> 2018 © Top Nguyen </Copyright>
//     <Url> http://topnguyen.net/ </Url>
//     <Author> Top </Author>
//     <Project> Elect </Project>
//     <File>
//         <Name> ResponseOptionModel.cs </Name>
//         <Created> 23/03/2018 4:38:49 PM </Created>
//         <Key> 155c1b3d-8426-40d0-add5-6a9bdc567dee </Key>
//     </File>
//     <Summary>
//         ResponseOptionModel.cs is a part of Elect
//     </Summary>
// <License>
//--------------------------------------------------
#endregion License

using Elect.Web.DataTable.Models.Constants;
using System;
using System.Linq;

namespace Elect.Web.DataTable.Models.Response
{
    public class ResponseOptionModel
    {
        public virtual ArrayOutputType? ArrayOutputType { get; set; }

        public static ResponseOptionModel<TSource> For<TSource>(IQueryable<TSource> data, Action<ResponseOptionModel<TSource>> setOptions) where TSource : class
        {
            var responseOptions = new ResponseOptionModel<TSource>();

            setOptions(responseOptions);

            return responseOptions;
        }
    }
}