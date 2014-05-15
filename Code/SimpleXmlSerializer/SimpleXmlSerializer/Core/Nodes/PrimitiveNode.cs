namespace SimpleXmlSerializer.Core
{
    public class PrimitiveNode : INode
    {
        public object Value { get; set; }

        public NodeName Name { get; set; }

        public PrimitiveDescription TypeDescription { get; set; }

        public void Accept(INodeVisitor visitor)
        {
            visitor.Visit(this);
        }
    }
}