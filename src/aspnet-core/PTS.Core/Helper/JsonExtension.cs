using System;
using Newtonsoft.Json;
using Newtonsoft.Json.Serialization;

namespace PTS.Shared.Helper
{
    public static class JsonExtension
    {
        public static string JsonSerializeObject(this object data, bool? useCamelCase = null, bool? ignoreNull = null)
        {
            var setting = new JsonSerializerSettings();
            if (useCamelCase.HasValue && useCamelCase.Value)
            {
                setting.ContractResolver = new DefaultContractResolver()
                {
                    NamingStrategy = new CamelCaseNamingStrategy()
                };
            }

            if (ignoreNull.HasValue)
                setting.NullValueHandling = ignoreNull.Value ? NullValueHandling.Ignore : NullValueHandling.Include;

            try
            {
                return JsonConvert.SerializeObject(data, setting);
            }
            catch
            {
                return null;
            }
        }

        public static string JsonSerializeObjectIgnoreNull(this object datas)
        {
            var setting = new JsonSerializerSettings
            {
                ContractResolver = new DefaultContractResolver() { NamingStrategy = new CamelCaseNamingStrategy() },
                NullValueHandling = NullValueHandling.Ignore
            };

            try
            {
                return JsonConvert.SerializeObject(datas, setting);
            }
            catch
            {
                return null;
            }
        }

        public static T JsonDeserializeObject<T>(this string str) where T : new()
        {
            try
            {
                return JsonConvert.DeserializeObject<T>(str);

            }
            catch (Exception ex)
            {
                return new T();
            }
        }

        public static T Copy<T>(this T source)
        {
            var serialized = JsonConvert.SerializeObject(source);
            return JsonConvert.DeserializeObject<T>(serialized);
        }
    }

}