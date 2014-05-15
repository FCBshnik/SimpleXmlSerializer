namespace SimpleXmlSerializer.Core.Naming
{
    public class NoNamingConvention : INamingConvention
    {
        public string NormalizeName(string name)
        {
            return name;
        }
    }
}