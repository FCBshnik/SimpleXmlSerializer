using SimpleXmlSerializer.Core.Complex;
using SimpleXmlSerializer.Core.Naming;
using SimpleXmlSerializer.Core.Visitors;

namespace SimpleXmlSerializer.Core.Nodes
{
    public class ComplexNode : INode
    {
        public object Value { get; set; }

        public NodeName Name { get; set; }

        public ComplexDescription TypeDescription { get; set; }

        public void Accept(INodeVisitor visitor)
        {
            visitor.Visit(this);
        }
    }
}