using SimpleXmlSerializer.Core.Collections;
using SimpleXmlSerializer.Core.Naming;

namespace SimpleXmlSerializer.Core.Nodes
{
    public class CollectionNode : INode
    {
        public object Value { get; set; }

        public NodeName Name { get; set; }

        public CollectionDescription TypeDescription { get; set; }

        public void Accept(INodeVisitor visitor)
        {
            visitor.Visit(this);
        }
    }
}