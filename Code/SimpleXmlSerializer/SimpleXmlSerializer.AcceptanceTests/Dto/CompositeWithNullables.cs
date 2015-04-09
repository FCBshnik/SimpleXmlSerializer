using System.Reflection;

namespace SimpleXmlSerializer.IntegrationTests.Dto
{
    public class CompositeWithNullables
    {
        public int? Int { get; set; }

        public BindingFlags? Enum { get; set; }
    }
}