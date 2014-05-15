using System;
using System.Collections.Generic;
using SimpleXmlSerializer.Core.Primitives.Serializers;
using SimpleXmlSerializer.Utils;

namespace SimpleXmlSerializer.Core.Primitives
{
    public class PrimitiveProvider : IPrimitiveProvider
    {
        private readonly Dictionary<Type, IPrimitiveSerializer> primitiveSerializers;

        public PrimitiveProvider(IFormatProvider formatProvider)
        {
            primitiveSerializers = new Dictionary<Type, IPrimitiveSerializer>
                {
                    { typeof(string), new StringSerializer() },
                    { typeof(int), new IntSerializer(formatProvider) },
                    { typeof(long), new LongSerializer(formatProvider) },
                    { typeof(float), new FloatSerializer(formatProvider) },
                    { typeof(double), new DoubleSerializer(formatProvider) },
                    { typeof(decimal), new DecimalSerializer(formatProvider) },
                    { typeof(bool), new BoolSerializer() },
                    { typeof(DateTime), new DateTimeSerializer() },
                    { typeof(TimeSpan), new TimeSpanSerializer() },
                };
        }

        public bool TryGetPrimitiveDescription(Type type, out PrimitiveDescription primitiveDescription)
        {
            IPrimitiveSerializer primitiveSerializer;
            if (primitiveSerializers.TryGetValue(type, out primitiveSerializer))
            {
                primitiveDescription = new PrimitiveDescription(primitiveSerializer);
                return true;
            }

            if (type.IsEnum)
            {
                primitiveDescription = new PrimitiveDescription(new EnumSerializer(type));
                return true;
            }

            if (type.IsGenericType)
            {
                var genericTypeDefinition = type.GetGenericTypeDefinition();
                if (genericTypeDefinition == typeof(Nullable<>))
                {
                    return TryGetPrimitiveDescription(type.GetGenericArguments()[0], out primitiveDescription);
                }
            }

            primitiveDescription = null;
            return false;
        }

        public void AddPrimitiveSerializer(Type type, IPrimitiveSerializer primitiveSerializer)
        {
            Preconditions.NotNull(type, "type");
            Preconditions.NotNull(primitiveSerializer, "primitiveSerializer");

            primitiveSerializers[type] = primitiveSerializer;
        }
    }
}