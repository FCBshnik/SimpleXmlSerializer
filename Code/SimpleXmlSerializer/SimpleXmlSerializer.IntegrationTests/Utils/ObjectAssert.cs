﻿using System.Collections;
using System.Reflection;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace SimpleXmlSerializer.IntegrationTests.Utils
{
    public static class ObjectAssert
    {
        /// <summary>
        /// Performs deep equality comparison of objects.
        /// </summary>
        public static void AreEqual(object expected, object actual)
        {
            if (expected == null && actual == null)
            {
                return;
            }

            if (expected == null || actual == null)
            {
                Assert.Fail("Expected:\r\n {0} \r\nActual:\r\n{1}", expected ?? "[NULL]", actual ?? "[NULL]");
            }

            if (ReferenceEquals(expected, actual))
            {
                return;
            }

            var expectedCollection = expected as ICollection;
            var actualCollection = actual as ICollection;

            if (expectedCollection != null && actualCollection != null)
            {
                Assert.AreEqual(expectedCollection.Count, actualCollection.Count, "Count of collection elements does not match");

                var expectedList = new ArrayList(expectedCollection);
                var actualList = new ArrayList(actualCollection);

                for (var i = 0; i < expectedCollection.Count; i++)
                {
                    AreEqual(expectedList[i], actualList[i]);
                }

                return;
            }

            var expectedType = expected.GetType();
            var actualType = actual.GetType();
            if (expectedType == actualType)
            {
                // get overridden Equals method 
                var equalsMethod = expectedType.GetMethod("Equals", BindingFlags.DeclaredOnly | BindingFlags.Instance | BindingFlags.Public, null, new[]{ typeof(object) }, null);
                if (equalsMethod != null)
                {
                    Assert.IsTrue((bool)equalsMethod.Invoke(expected, new[] { actual }), "Expected:\r\n {0} \r\nActual:\r\n{1}", expected, actual);
                    return;
                }

                var properties = expectedType.GetProperties(BindingFlags.Instance | BindingFlags.Public |BindingFlags.SetProperty | BindingFlags.GetProperty);
                foreach (var propertyInfo in properties)
                {
                    AreEqual(propertyInfo.GetValue(expected, null), propertyInfo.GetValue(actual, null));
                }

                return;
            }

            Assert.AreEqual(expected, actual);
        }
    }
}