namespace SimpleXmlSerializer.Core
{
    /// <summary>
    /// Represents name (element or attribute name) of node of object's graph in xml.
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

        public string ElementName { get; private set; }

        public string ItemName { get; private set; }

        public string AttributeName { get; private set; }

        /// <summary>
        /// Indicates if current node name is element name.
        /// </summary>
        public bool IsElement
        {
            get { return !string.IsNullOrEmpty(ElementName); }
        }

        /// <summary>
        /// Indicates if current node name is attribute name.
        /// </summary>
        public bool IsAttribute
        {
            get { return !string.IsNullOrEmpty(AttributeName); }
        }

        /// <summary>
        /// Indicates if current node name has name for item within collection.
        /// </summary>
        public bool IsItem
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