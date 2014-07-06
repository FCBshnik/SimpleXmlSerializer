using System;
using System.Xml;
using SimpleXmlSerializer.AcceptanceTests.Dto.Football;
using SimpleXmlSerializer.Core;

namespace SimpleXmlSerializer.AcceptanceTests.Tests.Features
{
    public class CoachCustomSerializer : ICustomNodeSerializer
    {
        public void Serialize(object value, XmlWriter xmlWriter)
        {
            var coach = (Coach)value;

            xmlWriter.WriteValue(string.Format("{0};{1}", coach.Name, coach.Age));
        }

        public object Deserialize(XmlReader xmlReader)
        {
            var serializedValue = xmlReader.ReadElementString();

            var parts = serializedValue.Split(new[] {";"}, StringSplitOptions.RemoveEmptyEntries);

            return new Coach { Name = parts[0], Age = int.Parse(parts[1]) };
        }
    }
}