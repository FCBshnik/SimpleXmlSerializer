namespace SimpleXmlSerializer.Core.Naming
{
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

        public bool IsElement
        {
            get { return !string.IsNullOrEmpty(ElementName); }
        }

        public bool IsAttribute
        {
            get { return !string.IsNullOrEmpty(AttributeName); }
        }

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