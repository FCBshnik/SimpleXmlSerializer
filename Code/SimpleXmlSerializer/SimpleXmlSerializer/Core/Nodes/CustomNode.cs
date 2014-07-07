﻿namespace SimpleXmlSerializer.Core
{
    public class CustomNode : INode
    {
        public object Value { get; set; }

        public NodeName Name { get; set; }

        public CustomNodeDescription Description { get; set; }

        public void Accept(INodeVisitor visitor)
        {
            visitor.Visit(this);
        }
    }
}