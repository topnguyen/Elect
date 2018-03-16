﻿#region	License
//--------------------------------------------------
// <License>
//     <Copyright> 2018 © Top Nguyen </Copyright>
//     <Url> http://topnguyen.net/ </Url>
//     <Author> Top </Author>
//     <Project> Elect </Project>
//     <File>
//         <Name> StringWriterWithEncoding.cs </Name>
//         <Created> 16/03/2018 8:42:31 AM </Created>
//         <Key> 11332cf2-80fd-4472-ba45-47b1000f0b98 </Key>
//     </File>
//     <Summary>
//         StringWriterWithEncoding.cs is a part of Elect
//     </Summary>
// <License>
//--------------------------------------------------
#endregion License

using System;
using System.IO;
using System.Text;

namespace Elect.Core.StringUtils
{
    /// <summary>
    ///     <para>
    ///         The <see cref="T:System.IO.StringWriter" /> class always outputs UTF-16 encoded
    ///         strings. To use a different encoding, we must inherit from <see cref="T:System.IO.StringWriter" />.
    ///     </para>
    /// </summary>
    public class StringWriterWithEncoding : StringWriter
    {
        private readonly Encoding _encoding;

        public StringWriterWithEncoding()
        {
        }

        public StringWriterWithEncoding(IFormatProvider formatProvider) : base(formatProvider)
        {
        }

        public StringWriterWithEncoding(StringBuilder stringBuilder) : base(stringBuilder)
        {
        }

        public StringWriterWithEncoding(StringBuilder stringBuilder, IFormatProvider formatProvider) : base(stringBuilder, formatProvider)
        {
        }

        public StringWriterWithEncoding(Encoding encoding)
        {
            _encoding = encoding ?? throw new ArgumentNullException(nameof(encoding));
        }

        public StringWriterWithEncoding(IFormatProvider formatProvider, Encoding encoding) : base(formatProvider)
        {
            _encoding = encoding ?? throw new ArgumentNullException(nameof(encoding));
        }

        public StringWriterWithEncoding(StringBuilder stringBuilder, Encoding encoding) : base(stringBuilder)
        {
            _encoding = encoding ?? throw new ArgumentNullException(nameof(encoding));
        }

        public StringWriterWithEncoding(StringBuilder stringBuilder, IFormatProvider formatProvider, Encoding encoding) : base(stringBuilder, formatProvider)
        {
            _encoding = encoding ?? throw new ArgumentNullException(nameof(encoding));
        }

        public override Encoding Encoding => _encoding ?? base.Encoding;
    }
}