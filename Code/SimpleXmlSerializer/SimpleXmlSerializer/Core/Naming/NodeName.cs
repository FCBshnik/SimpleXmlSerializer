namespace SimpleXmlSerializer.Core
{
    /// <summary>
    /// Represents name (element or attribute name) of node of object's graph in xml document.
    /// </summary>
    public class NodeName
    {
        public NodeName(string elementName)
            : this(elementName, null, null)
        {
        }

        public NodeName(string elementName, string itemName)
            : this(elementName, itemName, null)
        {
        }

        public NodeName(string elementName, string itemName, string attributeName)
        {
            ElementName = GetElementName(elementName);
            ItemName = GetElementName(itemName);
            AttributeName = GetElementName(attributeName);
        }

        public NodeName(XmlElementName elementName) 
            : this(elementName, null, null)
        {
        }

        public NodeName(XmlElementName elementName, XmlElementName itemName) 
            : this(elementName, itemName, null)
        {
        }

        public NodeName(XmlElementName elementName, XmlElementName itemName, XmlElementName attributeName)
        {
            ElementName = elementName;
            ItemName = itemName;
            AttributeName = attributeName;
        }

        /// <summary>
        /// Gets xml element name for property.
        /// </summary>
        public XmlElementName ElementName { get; private set; }

        /// <summary>
        /// Gets xml element name for item within collection.
        /// </summary>
        public XmlElementName ItemName { get; private set; }

        /// <summary>
        /// Gets xml attribute name for property.
        /// </summary>
        public XmlElementName AttributeName { get; private set; }

        /// <summary>
        /// Indicates if <see cref="ElementName"/> is not empty.
        /// </summary>
        public bool HasElementName
        {
            get { return ElementName != null; }
        }

        /// <summary>
        /// Indicates if <see cref="AttributeName"/> is not empty.
        /// </summary>
        public bool HasAttributeName
        {
            get { return AttributeName != null ; }
        }

        /// <summary>
        /// Indicates if <see cref="ItemName"/> is not empty.
        /// </summary>
        public bool HastItemName
        {
            get { return ItemName != null; }
        }

        public static NodeName Empty
        {
            get { return new NodeName((XmlElementName)null); }
        }

        public override string ToString()
        {
            return string.Format("ElementName: {0}, AttributeName: {1}, ItemName: {2}", ElementName, AttributeName, ItemName);
        }

        private static XmlElementName GetElementName(string name)
        {
            return XmlElementName.IsValidName(name) ? new XmlElementName(name) : null;
        }
    }
}