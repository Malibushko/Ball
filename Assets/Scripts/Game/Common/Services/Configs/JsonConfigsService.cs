using System;
using Game.Common.Services.Assets;
using Game.Common.Services.Configs.Converters;
using Newtonsoft.Json;
using UnityEngine;

namespace Game.Common.Services.Configs
{
    public class JsonConfigsService : IConfigsService
    {
        public const string ConfigPathIndicator = "@ref:";

        private IAssetsService _assetsService;
        private JsonReferenceConverter _referenceConverter;

        public JsonConfigsService(IAssetsService assetsService)
        {
            _assetsService = assetsService;
            _referenceConverter = new JsonReferenceConverter(this, ConfigPathIndicator);
        }

        public object Load(string path)
        {
            return Load<object>(path);
        }

        public T Load<T>(object config) where T : class
        {
            if (config is T configObject)
                return configObject;

            if (config is string configPath)
                return Load<T>(configPath);
            
            return null;
        }

        public T Load<T>(string path) where T : class
        {
            TextAsset jsonTextFile = _assetsService.Load<TextAsset>(UnwrapPath(path));
            if (jsonTextFile)
            {            
                JsonSerializerSettings settings = new JsonSerializerSettings
                {
                    Converters = new JsonConverter[] { _referenceConverter }
                };
                
                T config = JsonConvert.DeserializeObject<T>(jsonTextFile.text, settings);

                if (config != null)
                {
                    return config;
                }
            }
            
            return null;
        }
        
        internal object Load(string path, Type objectType)
        {
            TextAsset jsonTextFile = _assetsService.Load<TextAsset>(UnwrapPath(path));
            if (jsonTextFile)
            {
                JsonSerializerSettings settings = new JsonSerializerSettings
                {
                    Converters = new JsonConverter[] { _referenceConverter }
                };
                
                return JsonConvert.DeserializeObject(jsonTextFile.text, objectType, settings);
            }
            
            return null;
        }
        
        private string UnwrapPath(string path)
        {
            if (path.StartsWith(ConfigPathIndicator))
                return path.Substring(ConfigPathIndicator.Length);
            
            return path;
        }
    }
}