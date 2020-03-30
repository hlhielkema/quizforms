using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using Newtonsoft.Json.Serialization;
using QuizForms.Data.Models.Livestream;
using System;

namespace QuizForms.Data.Deserializers
{
    internal sealed class LivestreamDeserializer : JsonConverter<Livestream>
    {
        private class BaseSpecifiedConcreteClassConverter : DefaultContractResolver
        {
            protected override JsonConverter ResolveContractConverter(Type objectType)
            {
                if (typeof(Livestream).IsAssignableFrom(objectType) && !objectType.IsAbstract)
                    // pretend Livestream is not specified to avoid a stack overflow
                    return null;

                return base.ResolveContractConverter(objectType);
            }
        }

        static JsonSerializerSettings SpecifiedSubclassConversion = new JsonSerializerSettings()
        {
            ContractResolver = new BaseSpecifiedConcreteClassConverter()
        };

        public override bool CanRead => true;

        public override bool CanWrite => false;

        public override Livestream ReadJson(JsonReader reader, Type objectType, Livestream existingValue, bool hasExistingValue, JsonSerializer serializer)
        {
            JObject updateObject = JObject.Load(reader);
            string livestreamType = updateObject["type"].Value<string>();

            switch (livestreamType)
            {
                case "twitch":
                    return JsonConvert.DeserializeObject<TwitchLivestream>(updateObject.ToString(), SpecifiedSubclassConversion);
                case "youtube":
                    return JsonConvert.DeserializeObject<YoutubeLivestream>(updateObject.ToString(), SpecifiedSubclassConversion);
                default:
                    throw new NotSupportedException();
            }
        }

        public override void WriteJson(JsonWriter writer, Livestream value, JsonSerializer serializer)
            => throw new NotSupportedException();
    }
}
