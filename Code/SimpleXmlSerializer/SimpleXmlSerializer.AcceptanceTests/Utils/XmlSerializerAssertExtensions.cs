using System.IO;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using SimpleXmlSerializer.Extensions;

namespace SimpleXmlSerializer.IntegrationTests.Utils
{
    public static class XmlSerializerAssertExtensions
    {
        public static void DeserializeAndAssert(this XmlSerializer serializer, object expected, string xmlPath)
        {
            var xml = File.ReadAllText(xmlPath);

            var actual = serializer.DeserializeFromString(expected.GetType(), xml);

            ObjectAssert.AreEqual(expected, actual);
        }

        public static void SerializeAndAssert(this XmlSerializer serializer, object obj, string expectedXmlPath)
        {
            var expected = File.ReadAllText(expectedXmlPath);

            var actual = serializer.SerializeToString(obj);

            Assert.AreEqual(expected, actual);
        }
    }
}