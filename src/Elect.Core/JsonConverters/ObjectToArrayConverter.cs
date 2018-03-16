#region	License
//--------------------------------------------------
// <License>
//     <Copyright> 2018 © Top Nguyen </Copyright>
//     <Url> http://topnguyen.net/ </Url>
//     <Author> Top </Author>
//     <Project> Elect </Project>
//     <File>
//         <Name> ObjectToArrayConverter.cs </Name>
//         <Created> 16/03/2018 2:23:54 PM </Created>
//         <Key> f6fe3961-4066-4580-ba27-622f85175f4a </Key>
//     </File>
//     <Summary>
//         ObjectToArrayConverter.cs is a part of Elect
//     </Summary>
// <License>
//--------------------------------------------------
#endregion License

using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using Newtonsoft.Json.Serialization;
using System;
using System.Linq;

namespace Elect.Core.JsonConverters
{
    public class ObjectToArrayConverter : JsonConverter
    {
        public override bool CanConvert(Type objectType)
        {
            return true;
        }

        private static bool ShouldSkip(JsonProperty property)
        {
            return property.Ignored || !property.Readable || !property.Writable;
        }

        public override void WriteJson(JsonWriter writer, object value, JsonSerializer serializer)
        {
            var type = value.GetType();

            if (!(serializer.ContractResolver.ResolveContract(type) is JsonObjectContract contract))
            {
                throw new JsonSerializationException("Invalid type " + type.FullName);
            }

            var list = contract.Properties.Where(p => !ShouldSkip(p)).Select(p => p.ValueProvider.GetValue(value));

            serializer.Serialize(writer, list);
        }

        public override object ReadJson(JsonReader reader, Type objectType, object existingValue, JsonSerializer serializer)
        {
            if (reader.TokenType == JsonToken.Null)
            {
                return null;
            }

            var token = JToken.Load(reader);

            if (token.Type != JTokenType.Array)
            {
                throw new JsonSerializationException("Token was not an array");
            }

            if (!(serializer.ContractResolver.ResolveContract(objectType) is JsonObjectContract contract))
            {
                throw new JsonSerializationException("Invalid type " + objectType.FullName);
            }

            var value = existingValue ?? contract.DefaultCreator();

            foreach (var pair in contract.Properties.Where(p => !ShouldSkip(p)).Zip(token, (p, v) => new { Value = v, Property = p }))
            {
                var propertyValue = pair.Value.ToObject(pair.Property.PropertyType, serializer);

                pair.Property.ValueProvider.SetValue(value, propertyValue);
            }

            return value;
        }
    }
}