using System;
using System.Collections.Generic;
using System.Reflection;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using SimpleXmlSerializer.Core;

namespace SimpleXmlSerializer.UnitTests.Core.Complex
{
    [TestClass]
    public class ComplexNodeProviderTests
    {
        [ExpectedException(typeof(ArgumentException))]
        [TestMethod]
        public void GetDescription_NoParameterlessCtor_Throws()
        {
            var provider = new ComplexNodeProvider(new PublicPropertiesSelector());

            provider.GetDescription(typeof(ParameterlessCtorType));
        }

        [TestMethod]
        public void GetDescription_ValueType_ReturnsCorrectFactory()
        {
            var provider = new ComplexNodeProvider(new PublicPropertiesSelector());

            var description = provider.GetDescription(typeof(DateTime));
            var actual = description.Factory(new Dictionary<PropertyInfo, object>());

            Assert.AreEqual(new DateTime(), actual);
        }

        [TestMethod]
        public void GetDescription_ReferenceType_ReturnsCorrectFactory()
        {
            var provider = new ComplexNodeProvider(new PublicPropertiesSelector());

            var description = provider.GetDescription(typeof(ParameterlessType));
            var actual = description.Factory(new Dictionary<PropertyInfo, object>());

            Assert.IsNotNull(actual);
        }

        public class ParameterlessCtorType
        {
            public ParameterlessCtorType(object value)
            {
            }
        }

        public class ParameterlessType
        {
        }
    }
}