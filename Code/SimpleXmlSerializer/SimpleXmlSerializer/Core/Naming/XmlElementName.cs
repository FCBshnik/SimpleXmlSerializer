using System;

namespace SimpleXmlSerializer.Core
{
    /// <summary>
    /// Represents name for xml element.
    /// </summary>
    public class XmlElementName
    {
        private readonly string name;

        public XmlElementName(string name)
        {
            if (!IsValidName(name))
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

        public static bool IsValidName(string name)
        {
            // todo: validate xml element name
            return !string.IsNullOrWhiteSpace(name);
        }
    }
}