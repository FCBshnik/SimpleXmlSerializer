namespace SimpleXmlSerializer.Core
{
    public interface INode
    {
        object Value { get; set; }

        NodeName Name { get; set; }

        void Accept(INodeVisitor visitor);
    }
}