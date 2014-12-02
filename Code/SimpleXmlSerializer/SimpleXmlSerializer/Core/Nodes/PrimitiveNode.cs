using SimpleXmlSerializer.Utils;

namespace SimpleXmlSerializer.Core
{
    internal class PrimitiveNode : INode
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

        public object Clone()
        {
            return new PrimitiveNode(Description);
        }

        public override string ToString()
        {
            return string.Format("Primitive: {0}", Name);
        }
    }
}