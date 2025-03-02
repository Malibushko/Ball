using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using System;
using System.Reflection;

namespace Game.Common.Services.Configs.Converters
{
    public class JsonReferenceConverter : NonRecursiveConverter<JsonReferenceConverter>
    {
        private readonly JsonConfigsService _configsService;
        private string _configPathIndicator;

        public JsonReferenceConverter(JsonConfigsService configsService, string configPathIndicator)
        {
            _configsService = configsService;
            _configPathIndicator = configPathIndicator;
        }

        public override bool CanConvert(Type objectType)
        {
            return objectType.GetCustomAttribute<SharedConfig>() != null  && objectType != typeof(string);
        }
        
        protected override object OnReadJson(JsonReader reader, Type objectType, object existingValue, JsonSerializer serializer)
        {
            JToken token = JToken.Load(reader);
            if (token.Type == JTokenType.String)
            {
                string value = token.ToString();
                if (value.StartsWith(_configPathIndicator))
                    return _configsService.Load(value, objectType);
            }
            
            return token.ToObject(objectType, serializer);
        }

        protected override void OnWriteJson(JsonWriter writer, object value, JsonSerializer serializer)
        {    
            throw new NotImplementedException("The ConfigReferenceConverter only supports deserialization.");
        }
    }
}