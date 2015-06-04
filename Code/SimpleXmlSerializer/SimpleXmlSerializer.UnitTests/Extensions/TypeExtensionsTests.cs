using System.Collections.Generic;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace SimpleXmlSerializer.UnitTests.Extensions
{
    [TestClass]
    public class TypeExtensionsTests
    {
        [TestMethod]
        public void ImplementsGenericInterface_Implements_ReturnsTrue()
        {
            var actual = typeof(Dictionary<string, string>).ImplementsGenericInterface(typeof(IDictionary<string, string>));

            Assert.IsTrue(actual);
        }

        [TestMethod]
        public void ImplementsGenericInterface_ImplementsGenericTypeDefinition_ReturnsTrue()
        {
            var actual = typeof(Dictionary<string, string>).ImplementsGenericInterface(typeof(IDictionary<,>));

            Assert.IsTrue(actual);
        }

        [TestMethod]
        public void ImplementsGenericInterface_NotImplements_ReturnsFalse()
        {
            var actual = typeof(Dictionary<string, string>).ImplementsGenericInterface(typeof(IList<>));

            Assert.IsFalse(actual);
        }
    }
}