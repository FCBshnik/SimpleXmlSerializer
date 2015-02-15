using System;

namespace SimpleXmlSerializer.Core
{
    /// <summary>
    /// Represents node of object's graph.
    /// </summary>
    internal interface INode : ICloneable
    {
        object Value { get; set; }

        NodeName Name { get; set; }

        void Accept(INodeVisitor visitor);
    }
}