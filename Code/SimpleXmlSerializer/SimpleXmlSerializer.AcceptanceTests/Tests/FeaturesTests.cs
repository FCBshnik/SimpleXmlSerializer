﻿using System;
using System.Xml;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using SimpleXmlSerializer.AcceptanceTests.Dto;
using SimpleXmlSerializer.Core;

namespace SimpleXmlSerializer.AcceptanceTests.Tests
{
    [TestClass]
    public class FeaturesTests : TestsBase
    {
        [TestMethod]
        public void PrimitivesToAttributes()
        {
            var settings = new XmlSerializerSettingsBuilder().MapPrimitivesToAttributes().GetSettings();
            serializer = new XmlSerializer(settings);

            ActAndAssert(ComplexWithComplexes.Numbers, "primitivesToAttributes");
        }

        [TestMethod]
        public void PrimitivesToAttributesWithNull()
        {
            var settings = new XmlSerializerSettingsBuilder().MapPrimitivesToAttributes().GetSettings();
            serializer = new XmlSerializer(settings);

            var numbers = ComplexWithComplexes.Numbers;
            numbers.Two.String = null;
            ActAndAssert(numbers, "primitivesToAttributesWithNull");
        }

        [TestMethod]
        public void CustomSerializer()
        {
            var settings = new XmlSerializerSettingsBuilder()
                .AddCustomSerializer(typeof(ComplexWithPrimitives), new MyCustomSerializer())
                .GetSettings();
            serializer = new XmlSerializer(settings);

            ActAndAssert(ComplexWithComplexes.Numbers, "customSerializer");
        }
        
        public class MyCustomSerializer : ICustomSerializer
        {
            public void Serialize(object value, XmlWriter xmlWriter)
            {
                var complexWithPrimitives = (ComplexWithPrimitives)value;

                xmlWriter.WriteValue(string.Format("{0};{1}", complexWithPrimitives.String, complexWithPrimitives.Int));
            }

            public object Deserialize(XmlReader xmlReader)
            {
                var serializedValue = xmlReader.ReadElementString();

                var parts = serializedValue.Split(new[] { ";" }, StringSplitOptions.RemoveEmptyEntries);

                return new ComplexWithPrimitives { String = parts[0], Int = int.Parse(parts[1]) };
            }
        }
    }
}