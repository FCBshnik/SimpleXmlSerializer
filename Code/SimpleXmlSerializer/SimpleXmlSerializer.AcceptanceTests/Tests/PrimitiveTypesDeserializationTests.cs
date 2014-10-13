using System;
using System.IO;
using System.Reflection;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using SimpleXmlSerializer.AcceptanceTests.Utils;

namespace SimpleXmlSerializer.AcceptanceTests.Tests
{
    [TestClass]
    public class PrimitiveTypesDeserializationTests
    {
        private const string AssetsDirectory = "Assets\\Primitives";

        private static readonly XmlSerializer serializer = new XmlSerializer();

        [TestMethod]
        public void Deserialize_String()
        {
            DeserializeAndAssert("value");
        }

        [TestMethod]
        public void Deserialize_Int()
        {
            DeserializeAndAssert(13);
        }

        [TestMethod]
        public void Deserialize_Lond()
        {
            DeserializeAndAssert(13L);
        }

        [TestMethod]
        public void Deserialize_Bool()
        {
            DeserializeAndAssert(true);
        }

        [TestMethod]
        public void Deserialize_TimeSpan()
        {
            DeserializeAndAssert(new TimeSpan(1, 2, 3, 4));
        }

        [TestMethod]
        public void Deserialize_DateTime()
        {
            DeserializeAndAssert(new DateTime(2001, 02, 03, 04, 05, 06, 07, DateTimeKind.Utc));
        }

        [TestMethod]
        public void Deserialize_Enum()
        {
            DeserializeAndAssert(BindingFlags.Public | BindingFlags.Instance | BindingFlags.SetProperty | BindingFlags.GetProperty);
        }

        private void DeserializeAndAssert(object expected)
        {
            var path = Path.Combine(AssetsDirectory, expected.GetType().Name.ToLowerInvariant() + ".xml");
            serializer.DeserializeAndAssertObject(expected, path);
        }
    }
}