using Microsoft.VisualStudio.TestTools.UnitTesting;
using SimpleXmlSerializer.Utils;

namespace SimpleXmlSerializer.UnitTests.Utils
{
    [TestClass]
    public class ExpressionUtilsTests
    {
        [TestMethod]
        public void GetFactory_TypeWithParameterlessConstructor_RetunrsCorrectFunc()
        {
            var factory = ExpressionUtils.GetFactory(typeof(TestEntity));
            var instance = factory();

            Assert.IsInstanceOfType(instance, typeof(TestEntity));
        }

        [TestMethod]
        public void GetPropertyGetter_PropertyOfReferenceType_RetunrsCorrectFunc()
        {
            var propertyInfo = typeof(TestEntity).GetProperty("Name");

            var propertyGetter = ExpressionUtils.GetPropertyGetter(propertyInfo);
            var actual = propertyGetter(new TestEntity { Name = "Gauss" });

            Assert.AreEqual("Gauss", actual);
        }

        [TestMethod]
        public void GetPropertyGetter_PropertyOfValueType_RetunrsCorrectFunc()
        {
            var propertyInfo = typeof(TestEntity).GetProperty("Age");

            var propertyGetter = ExpressionUtils.GetPropertyGetter(propertyInfo);
            var actual = propertyGetter(new TestEntity { Age = 10 });

            Assert.AreEqual(10, actual);
        }

        public class TestEntity
        {
            public string Name { get; set; }

            public int Age { get; set; }
        }
    }
}