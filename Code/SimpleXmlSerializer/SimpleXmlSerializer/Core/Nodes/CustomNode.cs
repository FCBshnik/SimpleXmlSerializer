using SimpleXmlSerializer.Utils;

namespace SimpleXmlSerializer.Core
{
    public class CustomNode : INode
    {
        public CustomNode(CustomNodeDescription description)
        {
            Preconditions.NotNull(description, "description");
            Description = description;
        }

        public CustomNodeDescription Description { get; private set; }

        public object Value { get; set; }

        public NodeName Name { get; set; }

        public void Accept(INodeVisitor visitor)
        {
            visitor.Visit(this);
        }

        public object Clone()
        {
            return new CustomNode(Description);
        }
    }
}