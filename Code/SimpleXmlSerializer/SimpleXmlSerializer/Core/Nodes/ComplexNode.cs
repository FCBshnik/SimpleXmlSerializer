using System;
using SimpleXmlSerializer.Utils;

namespace SimpleXmlSerializer.Core
{
    internal class ComplexNode : INode
    {
        public ComplexNode(ComplexNodeDescription description)
        {
            if (description == null)
            {
                throw new ArgumentNullException("description");
            }
            Description = description;
        }

        public ComplexNodeDescription Description { get; private set; }

        public object Value { get; set; }

        public NodeName Name { get; set; }

        public void Accept(INodeVisitor visitor)
        {
            visitor.Visit(this);
        }

        public object Clone()
        {
            return new ComplexNode(Description);
        }

        public override string ToString()
        {
            return string.Format("Complex: {0}", Name);
        }
    }
}