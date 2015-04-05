using System.Reflection;

namespace SimpleXmlSerializer.AcceptanceTests.Dto
{
    public class CompositeWithNullables
    {
        public int? Int { get; set; }

        public BindingFlags? Enum { get; set; }
    }
}