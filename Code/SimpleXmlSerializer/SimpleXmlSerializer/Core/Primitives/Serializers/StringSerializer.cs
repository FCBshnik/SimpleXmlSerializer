﻿namespace SimpleXmlSerializer.Core.Primitives.Serializers
{
    public class StringSerializer : IPrimitiveSerializer
    {
        public string Serialize(object obj)
        {
            return (string)obj;
        }

        public object Deserialize(string value)
        {
            return value;
        }
    }
}