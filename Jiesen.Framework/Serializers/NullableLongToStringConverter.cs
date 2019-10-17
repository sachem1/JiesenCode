﻿using System;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;

namespace Jiesen.Framework
{

    public class NullableLongToStringConverter : Newtonsoft.Json.JsonConverter
    {
        public override object ReadJson(JsonReader reader, Type objectType, object existingValue, JsonSerializer serializer)
        {
            JToken jt = JValue.ReadFrom(reader);

            return jt.Value<long?>();
        }

        public override bool CanConvert(Type objectType)
        {
            return typeof(long?) == objectType;
        }

        public override void WriteJson(JsonWriter writer, object value, JsonSerializer serializer)
        {
            serializer.Serialize(writer,value==null?"": value.ToString());
        }
    }

}
