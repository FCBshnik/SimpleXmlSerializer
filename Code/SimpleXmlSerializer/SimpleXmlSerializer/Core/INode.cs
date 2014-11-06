using System;

namespace SimpleXmlSerializer.Core
{
    internal interface INode : ICloneable
    {
        object Value { get; set; }

        NodeName Name { get; set; }

        void Accept(INodeVisitor visitor);
    }
}