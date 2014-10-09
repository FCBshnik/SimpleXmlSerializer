using System;

namespace SimpleXmlSerializer.Core
{
    public abstract class NodeVisitor
    {
        private readonly XmlSerializerSettings settings;

        protected NodeVisitor(XmlSerializerSettings settings)
        {
            this.settings = settings;
        }

        protected virtual INode GetNode(Type valueType)
        {
            INode node;

            CustomNodeDescription customNodeDescription;
            PrimitiveNodeDescription primitiveNodeDescription;
            CollectionNodeDescription collectionNodeDescription;

            if (settings.CustomProvider.TryGetDescription(valueType, out customNodeDescription))
            {
                node = new CustomNode(customNodeDescription);
            }
            else if (settings.PrimitiveProvider.TryGetDescription(valueType, out primitiveNodeDescription))
            {
                node = new PrimitiveNode(primitiveNodeDescription);
            }
            else if (settings.CollectionProvider.TryGetDescription(valueType, out collectionNodeDescription))
            {
                node = new CollectionNode(collectionNodeDescription);
            }
            else
            {
                var complexNodeDescription = settings.ComplexProvider.GetDescription(valueType);
                node = new ComplexNode(complexNodeDescription);
            }

            return node;
        }
    }
}