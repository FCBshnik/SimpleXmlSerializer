using System;

namespace SimpleXmlSerializer.Core
{
    public interface IComplexProvider
    {
        ComplexDescription GetDescription(Type type);
    }
}