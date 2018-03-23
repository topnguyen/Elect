#region	License
//--------------------------------------------------
// <License>
//     <Copyright> 2018 © Top Nguyen </Copyright>
//     <Url> http://topnguyen.net/ </Url>
//     <Author> Top </Author>
//     <Project> Elect </Project>
//     <File>
//         <Name> ResponseOptionModel_T_.cs </Name>
//         <Created> 23/03/2018 4:39:07 PM </Created>
//         <Key> 759c9c59-9e76-4bd7-87f9-7c59c037c8bd </Key>
//     </File>
//     <Summary>
//         ResponseOptionModel_T_.cs is a part of Elect
//     </Summary>
// <License>
//--------------------------------------------------
#endregion License

using Elect.Web.DataTable.Models.Constants;
using System;

namespace Elect.Web.DataTable.Models.Response
{
    public class ResponseOptionModel<TSource> : ResponseOptionModel
    {
        public Func<TSource, object> DtRowId
        {
            get => _dtRowId;
            set
            {
                _dtRowId = value;
                if (value != null)
                {
                    ArrayOutputType = Constants.ArrayOutputType.ArrayOfObjects;
                }
            }
        }

        private Func<TSource, object> _dtRowId;

        public override ArrayOutputType? ArrayOutputType
        {
            get => base.ArrayOutputType;
            set
            {
                if (DtRowId != null && value != Constants.ArrayOutputType.ArrayOfObjects)
                {
                    throw new ArgumentOutOfRangeException(nameof(ArrayOutputType), $"{nameof(ArrayOutputType)} must be {nameof(Constants.ArrayOutputType.ArrayOfObjects)} when {nameof(DtRowId)} is set");
                }
                base.ArrayOutputType = value;
            }
        }
    }
}