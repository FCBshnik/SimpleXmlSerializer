using System;

namespace SimpleXmlSerializer.Core
{
    public abstract class XmlVisitorBase
    {
        private readonly XmlSerializerSettings settings;

        protected XmlVisitorBase(XmlSerializerSettings settings)
        {
            this.settings = settings;
        }

        protected virtual INode GetNode(Type valueType)
        {
            INode node;

            CustomNodeDescription customNodeDescription;
            PrimitiveNodeDescription primitiveDescription;
            CollectionNodeDescription collectionDescription;

            if (settings.CustomProvider.TryGetDescription(valueType, out customNodeDescription))
            {
                node = new CustomNode
                {
                    Description = customNodeDescription
                };
            }
            else if (settings.PrimitiveProvider.TryGetDescription(valueType, out primitiveDescription))
            {
                node = new PrimitiveNode
                {
                    Description = primitiveDescription,
                };
            }
            else if (settings.CollectionProvider.TryGetDescription(valueType, out collectionDescription))
            {
                node = new CollectionNode
                {
                    Description = collectionDescription,
                };
            }
            else
            {
                node = new ComplexNode
                {
                    Description = settings.ComplexProvider.GetDescription(valueType),
                };
            }

            return node;
        }
    }
}