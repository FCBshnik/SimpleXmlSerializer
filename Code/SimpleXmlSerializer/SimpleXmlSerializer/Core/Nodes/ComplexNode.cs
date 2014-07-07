namespace SimpleXmlSerializer.Core
{
    public class ComplexNode : INode
    {
        public object Value { get; set; }

        public NodeName Name { get; set; }

        public ComplexNodeDescription Description { get; set; }

        public void Accept(INodeVisitor visitor)
        {
            visitor.Visit(this);
        }
    }
}