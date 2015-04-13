using System;
using System.Reflection;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using SimpleXmlSerializer.Core.Serializers;
using SimpleXmlSerializer.IntegrationTests.Dto;

namespace SimpleXmlSerializer.IntegrationTests.Tests
{
    [TestClass]
    public class PrimitivesTests : TestsBase
    {
        [TestMethod]
        public void PrimitivesWithinComposite()
        {
            var value = new CompositeWithAllPrimitives
                {
                    Bool = true,
                    Byte = 13,
                    Sbyte = 13,
                    Char = 'c',
                    DateTime = new DateTime(2001, 02, 03, 04, 05, 06, 07, DateTimeKind.Utc),
                    DateTimeOffset = new DateTimeOffset(2001, 02, 03, 04, 05, 06, 07, TimeSpan.FromMinutes(1)),
                    Decimal = 13.13m,
                    Double = 13.13d,
                    Enum = BindingFlags.Public | BindingFlags.Instance | BindingFlags.SetProperty | BindingFlags.GetProperty,
                    Float = 13.13f,
                    Guid = new Guid("7C5C2522-944D-4561-9F5F-D9D2C19F470A"),
                    NullableInt = 13,
                    Int = 13,
                    Uint = 13,
                    Long = 13,
                    Ulong = 13,
                    Short = 13,
                    Ushort = 13,
                    String = "13",
                    TimeSpan = new TimeSpan(1, 2, 3, 4),
                    Type = typeof(Type),
                    Uri = new Uri("http://hostname/path?key1=value1&key2=value2"),
                };

            ActAndAssert(value, "primitives");
        }

        [TestMethod]
        public void Iso8601TimeSpan()
        {
            var settings = GetSettingsBuilder()
                .SetPrimitiveSerializer(typeof(TimeSpan), new Iso8601TimeSpanSerializer())
                .GetSettings();
            Serializer = new XmlSerializer(settings);

            ActAndAssert(new TimeSpan(1, 2, 3, 4), "iso8601timespan");
        }
    }
}