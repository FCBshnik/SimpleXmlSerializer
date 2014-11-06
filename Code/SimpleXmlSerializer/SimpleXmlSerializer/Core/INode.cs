using System;

namespace SimpleXmlSerializer.Core
{
    public interface INode : ICloneable
    {
        object Value { get; set; }

        NodeName Name { get; set; }

        void Accept(INodeVisitor visitor);
    }
}