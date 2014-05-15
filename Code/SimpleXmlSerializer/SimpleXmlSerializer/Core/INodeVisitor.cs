using SimpleXmlSerializer.Core.Nodes;

namespace SimpleXmlSerializer.Core
{
    public interface INodeVisitor
    {
        void Visit(PrimitiveNode node);
        
        void Visit(CollectionNode node);
        
        void Visit(ComplexNode node);
        
        void Visit(CustomNode node);
    }
}