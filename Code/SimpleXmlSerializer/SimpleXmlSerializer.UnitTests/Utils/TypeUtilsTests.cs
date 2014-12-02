using System.Collections.Generic;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using SimpleXmlSerializer.Utils;

namespace SimpleXmlSerializer.UnitTests.Utils
{
    [TestClass]
    public class TypeUtilsTests
    {
        [TestMethod]
        public void ImplementsGenericInterface_Implements_ReturnsTrue()
        {
            var actual = TypeUtils.ImplementsGenericInterface(typeof(Dictionary<string, string>), typeof(IDictionary<string, string>));

            Assert.IsTrue(actual);
        }

        [TestMethod]
        public void ImplementsGenericInterface_ImplementsGenericTypeDefinition_ReturnsTrue()
        {
            var actual = TypeUtils.ImplementsGenericInterface(typeof(Dictionary<string, string>), typeof(IDictionary<,>));

            Assert.IsTrue(actual);
        }

        [TestMethod]
        public void ImplementsGenericInterface_NotImplements_ReturnsFalse()
        {
            var actual = TypeUtils.ImplementsGenericInterface(typeof(Dictionary<string, string>), typeof(IList<>));

            Assert.IsFalse(actual);
        }
    }
}