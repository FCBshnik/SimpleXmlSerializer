using SimpleXmlSerializer.Utils;

namespace SimpleXmlSerializer.Core
{
    public class PrimitiveNode : INode
    {
        public PrimitiveNode(PrimitiveNodeDescription description)
        {
            Preconditions.NotNull(description, "description");
            Description = description;
        }

        public PrimitiveNodeDescription Description { get; private set; }

        public object Value { get; set; }

        public NodeName Name { get; set; }

        public void Accept(INodeVisitor visitor)
        {
            visitor.Visit(this);
        }
    }
}