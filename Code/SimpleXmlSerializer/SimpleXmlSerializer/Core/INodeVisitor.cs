namespace SimpleXmlSerializer.Core
{
    internal interface INodeVisitor
    {
        void Visit(PrimitiveNode node);

        void Visit(CollectionNode node);

        void Visit(ComplexNode node);
    }
}