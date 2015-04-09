﻿using System;
using System.Reflection;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using SimpleXmlSerializer.IntegrationTests.Dto;

namespace SimpleXmlSerializer.IntegrationTests.Tests
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
        public void Sbyte()
        {
            ActAndAssert((sbyte)13);
        }

        [TestMethod]
        public void Short()
        {
            ActAndAssert((short)13);
        }

        [TestMethod]
        public void Ushort()
        {
            ActAndAssert((ushort)13);
        }

        [TestMethod]
        public void Int()
        {
            ActAndAssert(13);
        }

        [TestMethod]
        public void Uint()
        {
            ActAndAssert((uint)13);
        }

        [TestMethod]
        public void Long()
        {
            ActAndAssert(13L);
        }

        [TestMethod]
        public void Ulong()
        {
            ActAndAssert((ulong)13L);
        }

        [TestMethod]
        public void Float()
        {
            ActAndAssert(13.13f);
        }

        [TestMethod]
        public void Double()
        {
            ActAndAssert(13.13d);
        }

        [TestMethod]
        public void Decimal()
        {
            ActAndAssert(13.13m);
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
        public void Nullable()
        {
            ActAndAssert(new CompositeWithNullables { Int = 13, Enum = BindingFlags.Public }, "nullable");
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

        [TestMethod]
        public void Guid()
        {
            ActAndAssert(new Guid("7C5C2522-944D-4561-9F5F-D9D2C19F470A"), "guid");
        }

        [TestMethod]
        public void PrimitivesWithinComposite()
        {
            var value = new CompositeWithAllPrimitiveTypes
                {
                    Bool = true,
                    Byte = 13,
                    Char = 'c',
                    DateTime = new DateTime(2001, 02, 03, 04, 05, 06, 07, DateTimeKind.Utc),
                    DateTimeOffset = new DateTimeOffset(2001, 02, 03, 04, 05, 06, 07, System.TimeSpan.FromMinutes(1)),
                    Decimal = 13.13m,
                    Double = 13.13d,
                    Enum = BindingFlags.Public | BindingFlags.Instance | BindingFlags.SetProperty | BindingFlags.GetProperty,
                    Float = 13.13f,
                    Guid = new Guid("7C5C2522-944D-4561-9F5F-D9D2C19F470A"),
                    Int = 13,
                    Long = 13,
                    Short = 13,
                    String = "13",
                    TimeSpan = new TimeSpan(1, 2, 3, 4),
                    Type = typeof(Type),
                    Uri = new Uri("http://hostname/path?key1=value1&key2=value2"),
                };

            ActAndAssert(value, "primitivesWithinComposite");
        }
    }
}