using System.Runtime.Serialization;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using SimpleXmlSerializer.IntegrationTests.Dto;

namespace SimpleXmlSerializer.IntegrationTests.Tests
{
    [TestClass]
    public class CurcularDependencyTests : TestsBase
    {
        [ExpectedException(typeof(SerializationException))]
        [TestMethod]
        public void ThrowsWhenCucrularDependencyIsDetected()
        {
            Serializer = new XmlSerializer();

            Serializer.SerializeToString(CompositeWithCicularDependency.Create());
        }
    }
}