using System;

namespace SimpleXmlSerializer.Core
{
    internal class CompositeNode : INode
    {
        public CompositeNode(CompositeTypeDescription description)
        {
            if (description == null)
                throw new ArgumentNullException("description");

            Description = description;
        }

        public CompositeTypeDescription Description { get; private set; }

        public object Value { get; set; }

        public NodeName Name { get; set; }

        public void Accept(INodeVisitor visitor)
        {
            visitor.Visit(this);
        }

        public object Clone()
        {
            return new CompositeNode(Description);
        }

        public override string ToString()
        {
            return string.Format("Name: {0}, Value: {1}", Name, Value);
        }
    }
}