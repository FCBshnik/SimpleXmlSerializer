using System.Xml;
using System.Xml.Serialization;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace SimpleXmlSerializer.AcceptanceTests.Tests
{
    [TestClass]
    public class CdataSectionTests : TestsBase
    {
        [TestMethod]
        public void CdataSectionProperties()
        {
            //var value = new CdataSectionDto
            //    {
            //        NoCdata = "Simple text",
            //        Cdata = "<html>/<html>",
            //    };

            //ActAndAssert(value, "cdata");
        }

        public class CdataSectionDto
        {
            public string NoCdata { get; set; }

            [XmlElement(Type = typeof(XmlCDataSection))]
            public string Cdata { get; set; }
        }
    }
}