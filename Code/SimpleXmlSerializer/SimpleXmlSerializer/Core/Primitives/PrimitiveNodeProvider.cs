using System;
using System.Collections.Generic;
using SimpleXmlSerializer.Core.Serializers;

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
                    { typeof(short), new ShortSerializer(formatProvider) },
                    { typeof(ushort), new UshortSerializer(formatProvider) },
                    { typeof(byte), new ByteSerializer(formatProvider) },
                    { typeof(sbyte), new SbyteSerializer(formatProvider) },
                    { typeof(int), new IntSerializer(formatProvider) },
                    { typeof(uint), new UintSerializer(formatProvider) },
                    { typeof(long), new LongSerializer(formatProvider) },
                    { typeof(ulong), new UlongSerializer(formatProvider) },
                    { typeof(float), new FloatSerializer(formatProvider) },
                    { typeof(double), new DoubleSerializer(formatProvider) },
                    { typeof(decimal), new DecimalSerializer(formatProvider) },
                    { typeof(bool), new BoolSerializer() },
                    { typeof(TimeSpan), new TimeSpanSerializer() },
                    { typeof(DateTime), new DateTimeSerializer() },
                    { typeof(DateTimeOffset), new DateTimeOffsetSerializer() },
                    { typeof(Uri), new UriSerializer() },
                    { typeof(Guid), new GuidSerializer(string.Empty, formatProvider) },
                    { typeof(Type), new TypeSerializer() }
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

            // handle enum types
            if (type.IsEnum)
            {
                primitiveDescription = new PrimitiveNodeDescription(new EnumSerializer(type));
                return true;
            }

            // handle nullable types
            if (type.IsGenericType && type.GetGenericTypeDefinition() == typeof(Nullable<>))
            {
                return TryGetDescription(type.GetGenericArguments()[0], out primitiveDescription);
            }

            primitiveDescription = null;
            return false;
        }

        public void SetPrimitiveSerializer(Type type, IPrimitiveSerializer primitiveSerializer)
        {
            if (type == null)
                throw new ArgumentNullException("type");
            if (primitiveSerializer == null)
                throw new ArgumentNullException("primitiveSerializer");

            primitiveSerializers[type] = primitiveSerializer;
        }
    }
}