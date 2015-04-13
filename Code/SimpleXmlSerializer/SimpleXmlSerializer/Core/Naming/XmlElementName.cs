using System;

namespace SimpleXmlSerializer.Core
{
    /// <summary>
    /// Represents name of xml element.
    /// </summary>
    public class XmlElementName
    {
        private readonly string name;

        public XmlElementName(string name)
        {
            if (string.IsNullOrWhiteSpace(name))
                throw new ArgumentException("Name is not valid xml element name", "name");

            this.name = name;
        }

        public string Name
        {
            get { return name; }
        }

        public override string ToString()
        {
            return name;
        }
    }
}