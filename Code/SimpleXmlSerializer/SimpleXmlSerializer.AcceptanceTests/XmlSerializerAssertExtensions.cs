using System.IO;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using SimpleXmlSerializer.Extensions;

namespace SimpleXmlSerializer.AcceptanceTests
{
    public static class XmlSerializerAssertExtensions
    {
        public static void DeserializeAndAssertObject(this XmlSerializer serializer, object expected, string xmlPath)
        {
            var xml = File.ReadAllText(xmlPath);

            var actual = serializer.Deserialize(expected.GetType(), xml);

            ObjectAssert.AreEqual(expected, actual);
        }

        public static void SerializeAndAssertObject(this XmlSerializer serializer, object obj, string expectedXmlPath)
        {
            var expected = File.ReadAllText(expectedXmlPath);

            var actual = serializer.Serialize(obj);

            Assert.AreEqual(expected, actual);
        }
    }
}