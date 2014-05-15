using System;

namespace SimpleXmlSerializer.Core.Complex
{
    public interface IComplexProvider
    {
        ComplexDescription GetDescription(Type type);
    }
}