using SimpleXmlSerializer.Utils;

namespace SimpleXmlSerializer.Core
{
    public class CollectionNode : INode
    {
        public CollectionNode(CollectionNodeDescription description)
        {
            Preconditions.NotNull(description, "description");
            Description = description;
        }

        public CollectionNodeDescription Description { get; private set; }

        public object Value { get; set; }

        public NodeName Name { get; set; }

        public void Accept(INodeVisitor visitor)
        {
            visitor.Visit(this);
        }
    }
}