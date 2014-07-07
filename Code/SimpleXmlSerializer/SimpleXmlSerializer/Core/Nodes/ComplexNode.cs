using SimpleXmlSerializer.Utils;

namespace SimpleXmlSerializer.Core
{
    public class ComplexNode : INode
    {
        public ComplexNode(ComplexNodeDescription description)
        {
            Preconditions.NotNull(description, "description");
            Description = description;
        }

        public ComplexNodeDescription Description { get; private set; }

        public object Value { get; set; }

        public NodeName Name { get; set; }

        public void Accept(INodeVisitor visitor)
        {
            visitor.Visit(this);
        }
    }
}