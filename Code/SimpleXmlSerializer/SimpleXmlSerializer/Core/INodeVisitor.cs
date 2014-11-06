namespace SimpleXmlSerializer.Core
{
    internal interface INodeVisitor
    {
        void Visit(CustomNode node);

        void Visit(PrimitiveNode node);

        void Visit(CollectionNode node);

        void Visit(ComplexNode node);
    }
}