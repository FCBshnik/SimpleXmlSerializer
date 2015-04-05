namespace SimpleXmlSerializer.Core
{
    /// <summary>
    /// Represents name (element or attribute name) of node of object's graph in xml document.
    /// </summary>
    public class NodeName
    {
        public NodeName(string elementName) : this(elementName, string.Empty, string.Empty)
        {
        }

        public NodeName(string elementName, string itemName) : this(elementName, itemName, string.Empty)
        {
        }

        public NodeName(string elementName, string itemName, string attributeName)
        {
            ElementName = elementName;
            ItemName = itemName;
            AttributeName = attributeName;
        }

        /// <summary>
        /// Gets xml element name for property.
        /// </summary>
        public string ElementName { get; private set; }

        /// <summary>
        /// Gets xml element name for item within collection.
        /// </summary>
        public string ItemName { get; private set; }

        /// <summary>
        /// Gets xml attribute name for property.
        /// </summary>
        public string AttributeName { get; private set; }

        /// <summary>
        /// Indicates if <see cref="ElementName"/> is not empty.
        /// </summary>
        public bool HasElementName
        {
            get { return !string.IsNullOrEmpty(ElementName); }
        }

        /// <summary>
        /// Indicates if <see cref="AttributeName"/> is not empty.
        /// </summary>
        public bool HasAttributeName
        {
            get { return !string.IsNullOrEmpty(AttributeName); }
        }

        /// <summary>
        /// Indicates if <see cref="ItemName"/> is not empty.
        /// </summary>
        public bool HastItemName
        {
            get { return !string.IsNullOrEmpty(ItemName); }
        }

        public static NodeName Empty
        {
            get { return new NodeName(string.Empty); }
        }

        public override string ToString()
        {
            return string.Format("ElementName: {0}, AttributeName: {1}, ItemName: {2}", ElementName, AttributeName, ItemName);
        }
    }
}