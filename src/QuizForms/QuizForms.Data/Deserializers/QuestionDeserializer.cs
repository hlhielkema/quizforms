using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using Newtonsoft.Json.Serialization;
using QuizForms.Data.Models.Questions;
using System;

namespace QuizForms.Data.Deserializers
{
    internal sealed class QuestionDeserializer : JsonConverter<Question>
    {
        private class BaseSpecifiedConcreteClassConverter : DefaultContractResolver
        {
            protected override JsonConverter ResolveContractConverter(Type objectType)
            {
                if (typeof(Question).IsAssignableFrom(objectType) && !objectType.IsAbstract)
                    // pretend Question is not specified to avoid a stack overflow
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

        public override Question ReadJson(JsonReader reader, Type objectType, Question existingValue, bool hasExistingValue, JsonSerializer serializer)
        {
            JObject updateObject = JObject.Load(reader);
            string questionType = updateObject["type"].Value<string>();

            switch (questionType)
            {
                case "multiple-choice":
                    return JsonConvert.DeserializeObject<MultipleChoiceQuestion>(updateObject.ToString(), SpecifiedSubclassConversion);
                case "text":
                    return JsonConvert.DeserializeObject<TextQuestion>(updateObject.ToString(), SpecifiedSubclassConversion);                
                default:
                    throw new NotSupportedException();
            }
        }

        public override void WriteJson(JsonWriter writer, Question value, JsonSerializer serializer)
            => throw new NotSupportedException();
    }
}
