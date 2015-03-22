using System.Runtime.Serialization;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using SimpleXmlSerializer.AcceptanceTests.Dto;
using SimpleXmlSerializer.Extensions;

namespace SimpleXmlSerializer.AcceptanceTests.Tests
{
    [TestClass]
    public class CurcularDependencyTests : TestsBase
    {
        [ExpectedException(typeof(SerializationException))]
        [TestMethod]
        public void ThrowsWhenCucrularDependencyIsDetected()
        {
            serializer = new XmlSerializer();

            serializer.SerializeToString(ComplexWithCicularDependency.Create());
        }
    }
}