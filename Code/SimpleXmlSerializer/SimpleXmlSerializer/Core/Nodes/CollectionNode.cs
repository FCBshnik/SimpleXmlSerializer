using System;
using SimpleXmlSerializer.Utils;

namespace SimpleXmlSerializer.Core
{
    internal class CollectionNode : INode
    {
        public CollectionNode(CollectionNodeDescription description)
        {
            if (description == null)
            {
                throw new ArgumentNullException("description");
            }
            Description = description;
        }

        public CollectionNodeDescription Description { get; private set; }

        public object Value { get; set; }

        public NodeName Name { get; set; }

        public void Accept(INodeVisitor visitor)
        {
            visitor.Visit(this);
        }

        public object Clone()
        {
            return new CollectionNode(Description);
        }

        public override string ToString()
        {
            return string.Format("Collection: {0}", Name);
        }
    }
}