namespace Elect.Core.JsonConverters
{
    public class ObjectToArrayConverter : JsonConverter
    {
        public override bool CanConvert(Type objectType)
        {
            // Only convert user-defined classes (not collections, primitives, or framework types)
            return objectType.IsClass &&
                   !typeof(System.Collections.IEnumerable).IsAssignableFrom(objectType) &&
                   objectType != typeof(string) &&
                   !objectType.Namespace.StartsWith("Newtonsoft.Json");
        }
        private static bool ShouldSkip(JsonProperty property)
        {
            return property.Ignored || !property.Readable || !property.Writable;
        }
        public override void WriteJson(JsonWriter writer, object value, JsonSerializer serializer)
        {
            var type = value.GetType();
            JsonObjectContract contract = null;
            try
            {
                contract = serializer.ContractResolver.ResolveContract(type) as JsonObjectContract;
            }
            catch (Exception ex)
            {
                throw new JsonSerializationException("Invalid type " + type.FullName, ex);
            }
            if (contract == null)
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
            JsonObjectContract contract = null;
            try
            {
                contract = serializer.ContractResolver.ResolveContract(objectType) as JsonObjectContract;
            }
            catch (Exception ex)
            {
                throw new JsonSerializationException("Invalid type " + objectType.FullName, ex);
            }
            if (contract == null)
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
