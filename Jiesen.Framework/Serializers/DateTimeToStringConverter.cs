﻿using System;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;

namespace Jiesen.Framework
{
    public class DateTimeToStringConverter : Newtonsoft.Json.JsonConverter
    {
        public override object ReadJson(JsonReader reader, Type objectType, object existingValue, JsonSerializer serializer)
        {
            JToken jt = JValue.ReadFrom(reader);

            return jt.Value<DateTime>();
        }

        public override bool CanConvert(Type objectType)
        {
            return typeof(DateTime) == objectType;
        }

        public override void WriteJson(JsonWriter writer, object value, JsonSerializer serializer)
        {
            serializer.Serialize(writer, value.ToString());
        }
    }
}
