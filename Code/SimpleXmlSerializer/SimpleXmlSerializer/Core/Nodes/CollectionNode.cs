﻿namespace SimpleXmlSerializer.Core
{
    public class CollectionNode : INode
    {
        public object Value { get; set; }

        public NodeName Name { get; set; }

        public CollectionNodeDescription Description { get; set; }

        public void Accept(INodeVisitor visitor)
        {
            visitor.Visit(this);
        }
    }
}