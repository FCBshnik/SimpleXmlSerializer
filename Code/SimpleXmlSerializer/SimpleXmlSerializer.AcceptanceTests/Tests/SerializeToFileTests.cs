using System.IO;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using SimpleXmlSerializer.Extensions;

namespace SimpleXmlSerializer.IntegrationTests.Tests
{
    [TestClass]
    public class SerializeToFileTests : TestsBase
    {
        [TestMethod]
        public void SerializeToFile()
        {
            Serializer.SerializeToFile("value", "value.xml");

            var actual = File.ReadAllText("value.xml");
            var expected = File.ReadAllText(GetXmlFilePath("obj"));

            Assert.AreEqual(expected, actual);
        }

        [TestMethod]
        public void DeserializeFromFile()
        {
            Serializer.SerializeToFile("value", "value.xml");

            var actual = Serializer.DeserializeFromFile(typeof(string), "value.xml");
            var expected = "value";

            Assert.AreEqual(expected, actual);
        }
    }
}