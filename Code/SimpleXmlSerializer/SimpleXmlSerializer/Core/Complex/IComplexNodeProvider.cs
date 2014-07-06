using System;

namespace SimpleXmlSerializer.Core
{
    public interface IComplexNodeProvider
    {
        ComplexNodeDescription GetDescription(Type type);
    }
}