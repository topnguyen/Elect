#region	License
//--------------------------------------------------
// <License>
//     <Copyright> 2018 © Top Nguyen </Copyright>
//     <Url> http://topnguyen.net/ </Url>
//     <Author> Top </Author>
//     <Project> Elect </Project>
//     <File>
//         <Name> UnixDateTimeJsonConverter.cs </Name>
//         <Created> 19/03/2018 9:40:30 PM </Created>
//         <Key> 4ff85618-5e5a-4935-96bb-0db736062803 </Key>
//     </File>
//     <Summary>
//         UnixDateTimeJsonConverter.cs is a part of Elect
//     </Summary>
// <License>
//--------------------------------------------------
#endregion License

using Newtonsoft.Json;
using Newtonsoft.Json.Converters;
using Newtonsoft.Json.Linq;
using System;
using System.Globalization;
using System.Linq;

namespace Elect.Notification.OneSignal.JsonConverter
{
    /// <summary>
    ///     Converter used to serialize UnixDateTimeEnum as string. 
    /// </summary>
    public class UnixDateTimeJsonConverter : StringEnumConverter
    {
        /// <summary>
        ///     Defines if converter can be used for de-serialization. 
        /// </summary>
        public override bool CanRead => true;

        /// <summary>
        ///     De-serializes object 
        /// </summary>
        /// <param name="reader">       </param>
        /// <param name="objectType">   </param>
        /// <param name="existingValue"></param>
        /// <param name="serializer">   </param>
        /// <returns></returns>
        public override object ReadJson(JsonReader reader, Type objectType, object existingValue,
            JsonSerializer serializer)
        {
            var isNullable = Nullable.GetUnderlyingType(objectType) != null;

            if (reader.TokenType == JsonToken.Null)
            {
                if (!isNullable)
                    throw new JsonSerializationException();
                return null;
            }

            var token = JToken.Load(reader);
            if (token.Type == JTokenType.String)
                token = string.Join(", ", token.ToString().Split(',').Select(s => s.Trim()).Select(s =>
                {
                    var unixTime = double.Parse(s);
                    var dateTime = UnixTimeStampToDateTime(unixTime);

                    return dateTime.ToString("s");
                }).ToArray());

            using (var subReader = token.CreateReader())
            {
                while (subReader.TokenType == JsonToken.None)
                    subReader.Read();
                return base.ReadJson(subReader, objectType, existingValue, serializer); // Use base class to convert
            }
        }

        /// <summary>
        ///     Serializes object 
        /// </summary>
        /// <param name="writer">    </param>
        /// <param name="value">     </param>
        /// <param name="serializer"></param>
        public override void WriteJson(JsonWriter writer, object value, JsonSerializer serializer)
        {
            var array = new JArray();
            using (var tempWriter = array.CreateWriter())
            {
                base.WriteJson(tempWriter, value, serializer);
            }
            var token = array.Single();

            if (token.Type == JTokenType.String && value != null)
            {
                token = string.Join(", ", token.ToString().Split(',').Select(s => s.Trim()).Select(s =>
                {
                    var dateTime = DateTime.Parse(s);
                    var unixTime = DateTimeToUnixTimeStamp(dateTime);

                    return unixTime.ToString(CultureInfo.InvariantCulture);
                }).ToArray());
            }

            token.WriteTo(writer);
        }

        /// <summary>
        ///     Defines if converter can be used for serialization. 
        /// </summary>
        /// <param name="objectType"></param>
        /// <returns></returns>
        public override bool CanConvert(Type objectType)
        {
            return objectType == typeof(DateTime);
        }

        private DateTime UnixTimeStampToDateTime(double unixTimeStamp)
        {
            // Unix timestamp is seconds past epoch
            var dtDateTime = new DateTime(1970, 1, 1, 0, 0, 0, 0, DateTimeKind.Utc);
            dtDateTime = dtDateTime.AddSeconds(unixTimeStamp);
            return dtDateTime;
        }

        private double DateTimeToUnixTimeStamp(DateTime dateTime)
        {
            // Unix timestamp is seconds past epoch
            var dtDateTime = new DateTime(1970, 1, 1, 0, 0, 0, 0, DateTimeKind.Utc);
            var elapsed = dateTime.Subtract(dtDateTime).TotalSeconds;

            return elapsed;
        }
    }
}