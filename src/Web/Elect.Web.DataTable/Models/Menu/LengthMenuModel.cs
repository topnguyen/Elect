#region	License
//--------------------------------------------------
// <License>
//     <Copyright> 2018 © Top Nguyen </Copyright>
//     <Url> http://topnguyen.net/ </Url>
//     <Author> Top </Author>
//     <Project> Elect </Project>
//     <File>
//         <Name> LengthMenuModel.cs </Name>
//         <Created> 23/03/2018 4:33:16 PM </Created>
//         <Key> 3a1e55d8-2db5-4013-bb46-3a18d453dcf5 </Key>
//     </File>
//     <Summary>
//         LengthMenuModel.cs is a part of Elect
//     </Summary>
// <License>
//--------------------------------------------------
#endregion License

using System;
using System.Collections.Generic;
using System.Linq;

namespace Elect.Web.DataTable.Models.Menu
{
    public class LengthMenuModel : List<Tuple<string, int>>
    {
        public override string ToString()
        {
            return "[[" + string.Join(", ", this.Select(pair => pair.Item2)) + "],[\"" + string.Join("\", \"", this.Select(pair => pair.Item1.Replace("\"", "\"\""))) + "\"]]";
        }
    }
}