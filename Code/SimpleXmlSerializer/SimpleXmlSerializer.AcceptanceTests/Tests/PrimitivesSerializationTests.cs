using System;
using System.IO;
using System.Reflection;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using SimpleXmlSerializer.AcceptanceTests.Utils;

namespace SimpleXmlSerializer.AcceptanceTests.Tests
{
    [TestClass]
    public class PrimitivesSerializationTests
    {
        private const string AssetsDirectory = "Assets\\Primitives";

        private static readonly XmlSerializer serializer = new XmlSerializer();

        [TestMethod]
        public void Serialize_String()
        {
            SerializeAndAssert("value");
        }

        [TestMethod]
        public void Serialize_Int()
        {
            SerializeAndAssert(13);
        }

        [TestMethod]
        public void Serialize_Long()
        {
            SerializeAndAssert(13L);
        }

        [TestMethod]
        public void Serialize_Bool()
        {
            SerializeAndAssert(true);
        }

        [TestMethod]
        public void Serialize_TimeSpan()
        {
            SerializeAndAssert(new TimeSpan(1, 2, 3, 4));
        }

        [TestMethod]
        public void Serialize_DateTime()
        {
            SerializeAndAssert(new DateTime(2001, 02, 03, 04, 05, 06, 07, DateTimeKind.Utc));
        }

        [TestMethod]
        public void Serialize_Enum()
        {
            SerializeAndAssert(BindingFlags.Public | BindingFlags.Instance | BindingFlags.SetProperty | BindingFlags.GetProperty);
        }

        private void SerializeAndAssert(object obj)
        {
            var path = Path.Combine(AssetsDirectory, obj.GetType().Name.ToLowerInvariant() + ".xml");

            serializer.SerializeAndAssertObject(obj, path);
        }
    }
}