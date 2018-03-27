#region	License
//--------------------------------------------------
// <License>
//     <Copyright> 2018 © Top Nguyen </Copyright>
//     <Url> http://topnguyen.net/ </Url>
//     <Author> Top </Author>
//     <Project> Elect </Project>
//     <File>
//         <Name> CheckHelper.cs </Name>
//         <Created> 15/03/2018 4:50:02 PM </Created>
//         <Key> 75aa0eec-eecf-47c5-bd1c-1be20c5b41e0 </Key>
//     </File>
//     <Summary>
//         CheckHelper.cs is a part of Elect
//     </Summary>
// <License>
//--------------------------------------------------
#endregion License

using System;

namespace Elect.Core.CheckUtils
{
    public static class CheckHelper
    {
        /// <exception cref="ArgumentNullException"></exception>
        public static void CheckNullOrWhiteSpace(string propertyValue, string propertyName)
        {
            if (string.IsNullOrWhiteSpace(propertyValue))
            {
                throw new ArgumentNullException($"{propertyName} cannot be null or empty or whitespace.", propertyName);
            }
        }
    }
}