using System.Collections.Generic;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using SimpleXmlSerializer.AcceptanceTests.Dto;

namespace SimpleXmlSerializer.AcceptanceTests.Tests
{
    [TestClass]
    public class UnknownElementsTests : TestsBase
    {
        [TestMethod]
        public void UnknownXmlElement_NoError()
        {
            serializer = new XmlSerializer();

            DeserializeAndAssert(ComplexWithPrimitives.One, "unknownXmlElement");
        }

        [TestMethod]
        public void UnknownXmlAttribute_NoError()
        {
            serializer = new XmlSerializer();

            DeserializeAndAssert(ComplexWithPrimitives.One, "unknownXmlAttribute");
        }

        [TestMethod]
        public void UnknownXmlElementWithinCollection_NoError()
        {
            serializer = new XmlSerializer();

            DeserializeAndAssert(new List<string> { "One", "Two" }, "unknownXmlElementWithinCollection");
        }
    }
}