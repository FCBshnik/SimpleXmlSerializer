namespace SimpleXmlSerializer.Core
{
    public class NoNamingConvention : INamingConvention
    {
        public string NormalizeName(string name)
        {
            return name;
        }
    }
}