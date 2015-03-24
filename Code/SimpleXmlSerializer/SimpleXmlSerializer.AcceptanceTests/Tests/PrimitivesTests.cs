using System;
using System.Reflection;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace SimpleXmlSerializer.AcceptanceTests.Tests
{
    [TestClass]
    public class PrimitivesTests : TestsBase
    {
        [TestMethod]
        public void Char()
        {
            ActAndAssert('v');
        }

        [TestMethod]
        public void String()
        {
            ActAndAssert("value");
        }

        [TestMethod]
        public void Byte()
        {
            ActAndAssert((byte)13);
        }

        [TestMethod]
        public void Int()
        {
            ActAndAssert(13);
        }

        [TestMethod]
        public void Long()
        {
            ActAndAssert(13L);
        }

        [TestMethod]
        public void Bool()
        {
            ActAndAssert(true);
        }

        [TestMethod]
        public void TimeSpan()
        {
            ActAndAssert(new TimeSpan(1, 2, 3, 4));
        }

        [TestMethod]
        public void DateTime()
        {
            ActAndAssert(new DateTime(2001, 02, 03, 04, 05, 06, 07, DateTimeKind.Utc));
        }

        [TestMethod]
        public void DateTimeOffset()
        {
            ActAndAssert(new DateTimeOffset(2001, 02, 03, 04, 05, 06, 07, System.TimeSpan.FromMinutes(1)));
        }

        [TestMethod]
        public void Enum()
        {
            ActAndAssert(BindingFlags.Public | BindingFlags.Instance | BindingFlags.SetProperty | BindingFlags.GetProperty, "enum");
        }

        [TestMethod]
        public void Uri()
        {
            ActAndAssert(new Uri("http://hostname/path?key1=value1&key2=value2"), "uri");
        }
    }
}