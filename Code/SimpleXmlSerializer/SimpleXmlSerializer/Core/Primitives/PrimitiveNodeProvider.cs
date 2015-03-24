using System;
using System.Collections.Generic;
using SimpleXmlSerializer.Core.Serializers;
using SimpleXmlSerializer.Utils;

namespace SimpleXmlSerializer.Core
{
    public class PrimitiveNodeProvider : IPrimitiveNodeProvider
    {
        private readonly Dictionary<Type, IPrimitiveSerializer> primitiveSerializers;

        public PrimitiveNodeProvider(IFormatProvider formatProvider)
        {
            primitiveSerializers = new Dictionary<Type, IPrimitiveSerializer>
                {
                    { typeof(char), new CharSerializer() },
                    { typeof(string), new StringSerializer() },
                    { typeof(byte), new ByteSerializer(formatProvider) },
                    { typeof(int), new IntSerializer(formatProvider) },
                    { typeof(long), new LongSerializer(formatProvider) },
                    { typeof(float), new FloatSerializer(formatProvider) },
                    { typeof(double), new DoubleSerializer(formatProvider) },
                    { typeof(decimal), new DecimalSerializer(formatProvider) },
                    { typeof(bool), new BoolSerializer() },
                    { typeof(TimeSpan), new TimeSpanSerializer() },
                    { typeof(DateTime), new DateTimeSerializer() },
                    { typeof(DateTimeOffset), new DateTimeOffsetSerializer() },
                };
        }

        public bool TryGetDescription(Type type, out PrimitiveNodeDescription primitiveDescription)
        {
            IPrimitiveSerializer primitiveSerializer;
            if (primitiveSerializers.TryGetValue(type, out primitiveSerializer))
            {
                primitiveDescription = new PrimitiveNodeDescription(primitiveSerializer);
                return true;
            }

            if (type.IsEnum)
            {
                primitiveDescription = new PrimitiveNodeDescription(new EnumSerializer(type));
                return true;
            }

            if (type.IsGenericType)
            {
                var genericTypeDefinition = type.GetGenericTypeDefinition();
                if (genericTypeDefinition == typeof(Nullable<>))
                {
                    return TryGetDescription(type.GetGenericArguments()[0], out primitiveDescription);
                }
            }

            primitiveDescription = null;
            return false;
        }

        public void SetPrimitiveSerializer(Type type, IPrimitiveSerializer primitiveSerializer)
        {
            if (type == null)
            {
                throw new ArgumentNullException("type");
            }
            if (primitiveSerializer == null)
            {
                throw new ArgumentNullException("primitiveSerializer");
            }

            primitiveSerializers[type] = primitiveSerializer;
        }
    }
}