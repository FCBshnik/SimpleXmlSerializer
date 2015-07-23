using System;
using System.Collections.Generic;
using System.Reflection;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using SimpleXmlSerializer.Core;

namespace SimpleXmlSerializer.UnitTests.Core.Composite
{
    [TestClass]
    public class CompositeTypeProviderTests
    {
        [ExpectedException(typeof(ArgumentException))]
        [TestMethod]
        public void GetDescription_NoParameterlessCtor_Throws()
        {
            var provider = new CompositeTypeProvider(new PublicPropertiesProvider());

            CompositeTypeDescription description;
            provider.TryGetDescription(typeof(ParameternessCtorType), out description);
        }

        [TestMethod]
        public void GetDescription_ValueType_ReturnsCorrectFactory()
        {
            var provider = new CompositeTypeProvider(new PublicPropertiesProvider());

            CompositeTypeDescription description;
            provider.TryGetDescription(typeof(DateTime), out description);
            var actual = description.Factory(new Dictionary<PropertyInfo, object>());

            Assert.AreEqual(new DateTime(), actual);
        }

        [TestMethod]
        public void GetDescription_ReferenceType_ReturnsCorrectFactory()
        {
            var provider = new CompositeTypeProvider(new PublicPropertiesProvider());

            CompositeTypeDescription description;
            provider.TryGetDescription(typeof(ParameterlessCtorType), out description);
            var actual = description.Factory(new Dictionary<PropertyInfo, object>());

            Assert.IsInstanceOfType(actual, typeof(ParameterlessCtorType));
        }

        public class ParameternessCtorType
        {
            public ParameternessCtorType(object value)
            {
            }
        }

        public class ParameterlessCtorType
        {
        }
    }
}