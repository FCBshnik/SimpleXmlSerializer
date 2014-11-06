using System;
using System.IO;
using System.Reflection;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using SimpleXmlSerializer.AcceptanceTests.Utils;

namespace SimpleXmlSerializer.AcceptanceTests.Tests
{
    [TestClass]
    public class PrimitivesTests
    {
        private const string AssetsDirectory = "Assets\\Primitives";

        private static readonly XmlSerializer serializer = new XmlSerializer();

        [TestMethod]
        public void String()
        {
            ActAndAssert("value");
        }

        [TestMethod]
        public void Int()
        {
            ActAndAssert(13);
        }

        [TestMethod]
        public void Long()
        {
            ActAndAssert(13L);
        }

        [TestMethod]
        public void Bool()
        {
            ActAndAssert(true);
        }

        [TestMethod]
        public void TimeSpan()
        {
            ActAndAssert(new TimeSpan(1, 2, 3, 4));
        }

        [TestMethod]
        public void DateTime()
        {
            ActAndAssert(new DateTime(2001, 02, 03, 04, 05, 06, 07, DateTimeKind.Utc));
        }

        [TestMethod]
        public void Enum()
        {
            ActAndAssert(BindingFlags.Public | BindingFlags.Instance | BindingFlags.SetProperty | BindingFlags.GetProperty);
        }

        private static void ActAndAssert(object obj)
        {
            var path = Path.Combine(AssetsDirectory, obj.GetType().Name.ToLowerInvariant() + ".xml");
            serializer.SerializeAndAssert(obj, path);
            serializer.DeserializeAndAssert(obj, path);
        }
    }
}