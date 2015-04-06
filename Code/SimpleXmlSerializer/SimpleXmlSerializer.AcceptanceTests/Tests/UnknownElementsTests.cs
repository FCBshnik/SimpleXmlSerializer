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
            DeserializeAndAssert(CompositeWithPrimitives.One, "unknownXmlElement");
        }

        [TestMethod]
        public void UnknownXmlAttribute_NoError()
        {
            DeserializeAndAssert(CompositeWithPrimitives.One, "unknownXmlAttribute");
        }

        [TestMethod]
        public void UnknownXmlElementWithinCollection_NoError()
        {
            DeserializeAndAssert(new List<string> { "One", "Two" }, "unknownXmlElementWithinCollection");
        }
    }
}