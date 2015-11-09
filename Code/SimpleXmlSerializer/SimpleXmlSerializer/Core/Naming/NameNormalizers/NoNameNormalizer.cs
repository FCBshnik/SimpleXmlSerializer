namespace SimpleXmlSerializer.Core
{
    public class NoNameNormalizer : INameNormalizer
    {
        public string NormalizeName(string name)
        {
            return name;
        }
    }
}